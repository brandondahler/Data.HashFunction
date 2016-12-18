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


Task Default -depends Validate,Build,Test

Task Validate -depends Validate-Versions
Task Build -depends Build-Solution
Task Test -depends Test-Solution

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
			RunTests = $name -eq "System.Data.HashFunction.Test"
		}) > $null
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

	if ($preReleaseTag -ne "")
	{
		$versionSuffix = "$preReleaseTag-$buildNumber"
	}

	$vcsRevision = Exec { & $gitExecutable rev-parse HEAD }


	if (Test-Path $artifactsDir)
	{
		Remove-Item "$artifactsDir\*" -Force
	}

	$allProjects = [System.Collections.ArrayList]::new()

	try
	{

		foreach ($project in $script:projects)
		{
			$projectJsonPath = $project.Path + "\project.json"
			$oldProjectJsonPath = $project.Path + "\project.old.json"


			if (!$project.SkipPackaging)
			{
				Copy-Item $projectJsonPath -Destination $oldProjectJsonPath -Force

				$updatedProject = $project.Project | ConvertTo-Json -Depth 100 | ConvertFrom-Json
				$updatedProject.packOptions.releaseNotes += "`nvcs-revision: $vcsRevision"
			
				Set-Content $projectJsonPath -Value $(ConvertTo-Json $updatedProject -Depth 100) -Force
			}

			$allProjects.Add($projectJsonPath) > $null
		}


		Exec { & dotnet.exe restore $allProjects > $null }
	
		if ($versionSuffix -ne "")
		{
			Exec { & dotnet.exe build $allProjects -c $configuration --version-suffix $versionSuffix }

		} else {
			Exec { & dotnet.exe build $allProjects -c $configuration }
		}
		
	} finally {
		foreach ($project in $script:projects)
		{
			$projectJsonPath = $project.Path + "\project.json"
			$oldProjectJsonPath = $project.Path + "\project.old.json"

			if ($versionSuffix -ne "")
			{
				Exec { & dotnet.exe pack "$projectJsonPath" -c $configuration --version-suffix "$versionSuffix" -o $artifactsDir }
			} else {
				Exec { & dotnet.exe pack "$projectJsonPath" -c $configuration -o $artifactsDir }
			}

			if (Test-Path $oldProjectJsonPath)
			{
				Move-Item $oldProjectJsonPath -Destination $projectJsonPath -Force
			}
		}
	}
}

task Test-Solution -depends Resolve-Projects {
	foreach ($project in $script:projects)
	{
		if ($project.RunTests)
		{
			$projectJsonPath = $project.Path + "\project.json"

			Exec { & dotnet.exe test "$projectJsonPath" }
		}
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
