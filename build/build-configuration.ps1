$baseDir  = Resolve-Path $PSScriptRoot\.. -Relative
$currentYear = [DateTime]::Today.Year

properties {
	$configuration = "Debug"
	$nuGetBaseUrl = "https://www.nuget.org/api/v2/"
	$preReleaseTag = "local"
	$signAssemblies = $false
	$signKeyPath = "$baseDir\Data.HashFunction.Production.pfx"
	$treatWarningsAsErrors = $true
  
	$gitExecutable = "git.exe"
	$nuGetExecutable = "nuget.exe"

	$artifactsDir = "$baseDir\Artifacts"
	$buildDir = "$baseDir\build"
	$sourceDir = "$baseDir\src"
	$toolsDir = "$baseDir\tools"
	$releaseDir = "$baseDir\release"
	$nuGetDir = "$baseDir\NuGet"

	$buildNumber = Read-BuildNumber "$buildDir\BuildNumber.txt"
}


Task Default -depends Validate,Build

Task Validate -depends Validate-Versions
Task Build -depends Build-Solution
Task Test -depends Test-Solution
Task Pack -depends Prepare-NuGet,Pack-NuGet

Task Resolve-Projects {
	$script:projects = [System.Collections.ArrayList]::new()
	
	$projectDirectories = Get-ChildItem "$sourceDir\System.Data.HashFunction.*" -Directory
	foreach ($projectDirectory in $projectDirectories)
	{
		$name = $projectDirectory.Name

		$script:projects.Add(@{
			Name = $name
			Path = "$sourceDir\$name"
			ProjectJsonPath = "$sourceDir\$name\project.json"
			Project = Get-Content "$sourceDir\$name\project.json" | ConvertFrom-Json
			NuGetPath = "$nuGetDir\$name"
			NuGetPackageName = $name
			SkipPackaging = $name -eq "System.Data.HashFunction.Test"
		})
	}
}

Task Resolve-Production-Versions -depends Resolve-Projects {
	$script:productionPackageVersions = @{}

	foreach ($project in $script:projects)
	{
		if ($project.SkipPackaging)
		{
			continue
		}

		$xmlDocument = [System.Xml.XmlDocument]::new()
			
		$versionResults = Invoke-WebRequest $($nuGetBaseUrl + "FindPackagesById()?Id='" + $project.NuGetPackageName + "'")
		$xmlDocument.LoadXml($versionResults.Content)

		$versions = @{}

		foreach ($entry in $xmlDocument.feed.entry)
		{
			$entryVcsRevision = $(Parse-VCS-Revision $entry.ReleaseNotes)
			$versions.Add($entry.properties.Version, $entryVcsRevision)
		}

		$script:productionPackageVersions.Add($project.Name, $versions)
	}
}

Task Validate-Versions -depends Resolve-Production-Versions {
	$anyVersionBumpRequired = $false

	foreach ($project in $script:projects)
	{
		if ($project.SkipPackaging)
		{
			continue
		}
		

		Write-Host $("Validating version for " + $project.Name + ".")


		$currentProjectVersion = $project.Project.version

		if ($script:productionPackageVersions.ContainsKey($project.NuGetPackageName))
		{
			$packageVersions = $script:productionPackageVersions[$project.NuGetPackageName]

			if ($packageVersions.ContainsKey($currentProjectVersion))
			{
				$packageRevision = $packageVersions[$currentProjectVersion]

				$fileChanges = @()

				if ($packageRevision -ne $null)
				{
					$fileChanges = $(& $gitExecutable diff $packageRevision $project.Path)
				}

				if ($fileChanges.Count -gt 0)
				{
					Write-Host "Changes made since last production deploy, version bump required." -ForegroundColor Red
					$anyVersionBumpRequired = $true
				}
			}
		}
	}

	if ($anyVersionBumpRequired)
	{
		throw "Version bump required for at least one project."
	}
}

task Build-Solution -depends Resolve-Projects {	
	$versionSuffix = ""

	if ($preReleaseTag -eq "")
	{
		$versionSuffix = "$preReleaseTag-$buildNumber"
	}

	$vcsRevision = Exec { & $gitExecutable rev-parse HEAD }
	try
	{
		foreach ($project in $script:projects)
		{
			$projectJsonPath = $project.Path + "\project.json"
			$oldProjectJsonPath = $project.Path + "\project.old.json"

			Copy-Item $projectJsonPath -Destination $oldProjectJsonPath -Force


			$updatedProject = $project.Project | ConvertTo-Json | ConvertFrom-Json

			if (!$project.SkipPackaging)
			{
				$updatedProject.packOptions.releaseNotes += "`nvcs-revision: $vcsRevision"
			}


			Set-Content $projectJsonPath -Value $(ConvertTo-Json $updatedProject) -Force
		}
	
		Exec { & dotnet.exe restore $sourceDir }
		
		foreach ($project in $script:projects)
		{
			Write-Host $project.Name
			if ($versionSuffix -ne "")
			{
				Exec { & dotnet.exe build $project.Path -c $configuration --version-suffix "$versionSuffix" | Write-Output }

			} else {
				Exec { & dotnet.exe build $project.Path -c $configuration | Write-Output }
			}
		}
	} finally {
		
		foreach ($project in $script:projects)
		{
			$projectJsonPath = $project.Path + "\project.json"
			$oldProjectJsonPath = $project.Path + "\project.old.json"

			if (Test-Path $oldProjectJsonPath)
			{
				Move-Item $oldProjectJsonPath -Destination $projectJsonPath -Force
			}
		}

		Exec { & dotnet.exe restore $sourceDir > $null }
	}
}

task Pack-NuGet -depends Relove-Projects {
	foreach ($project in $script:projects)
	{
		$versionSuffix = ""

		if ($preReleaseTag -eq "")
		{
			$versionSuffix = "$preReleaseTag-$buildNumber"
		}

		$updatedProjectPath = $project.Path + "\project.updated.json"


		Exec { & dotnet.exe pack $updatedProjectPath -c $configuration --version-suffix "$versionSuffix" -o $artifactsDir }
	}
}

function Read-BuildNumber {
	param (
		[string] $buildNumberFilePath
	)

	$buildNumber = 0

	if (-Not (Test-Path $buildNumberFilePath))
	{
		New-Item $buildNumberFilePath -ItemType File -Value $buildNumber
	}


	$buildNumber = [int]::Parse($(Get-Content $buildNumberFilePath -Raw)) + 1

	Set-Content $buildNumberFilePath -Value $buildNumber

	return $buildNumber
}

function Update-Project {
	param (
		[string] $projectPath,
		[string] $sign
	)

	$file = switch($sign) { $true { $signKeyPath } default { $null } }

	$json = (Get-Content $projectPath -Raw) | ConvertFrom-Json

	# Add/Update buildOptions
	if ($json.buildOptions -eq $null) 
	{
		Add-Member -InputObject $json -MemberType NoteProperty -Name "buildOptions" -Value $(New-Object -TypeName psobject) -Force
	}

	# Add/Update buildOptions.*
	$overrideOptions = @{"warningsAsErrors" = $true; "xmlDoc" = $true; "keyFile" = $file }
	$overrideOptions.Keys | % { 
		if ($(Get-Member -InputObject $json.buildOptions -MemberType NoteProperty -Name $_) -eq $null) 
		{
			Add-Member -InputObject $json.buildOptions -MemberType NoteProperty -Name $_ -Value $overrideOptions[$_] 
		} else {
			$json.buildOptions.$_ = $overrideOptions[$_];
		}
	}


	$json.version = GetNuGetVersion

	ConvertTo-Json $json -Depth 10 | Set-Content $projectPath
}

function Parse-VCS-Revision
{
	param (
		[string] $releaseNotes
	)

	[System.Text.RegularExpressions.Match] $match = [Regex]::Match($releaseNotes, "vcs-revision: ([0-9a-fA-F]{32})")

	if (!$match.Success)
	{
		return $null
	}

	return $match.Groups[1]
}

function Get-Description-FromAssemblyInfo
{
	param (
		$assemblyInfoPath
	)

	$assemblyInfoContent = Get-Content $assemblyInfoPath -Raw

	$match = [System.Text.RegularExpressions.Regex]::Match("\[assembly: AssemblyDescription\(`"([^`"]*)`"\)\]")

	if (!$match.Success)
	{
		return ""
	}

	return $match.Groups[1].Value
}