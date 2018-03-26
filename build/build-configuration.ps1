$baseDir  = Resolve-Path $PSScriptRoot\..
$currentYear = [DateTime]::Today.Year

properties {
	$configuration = "Debug"
	$nuGetBaseUrl = "https://www.nuget.org/api/v2/"
	$preReleaseTag = "local"
  
	$gitExecutable = "git"
	$dotNetExecutable = "dotnet.exe"

	$artifactsDir = "$baseDir\Artifacts"
	$buildDir = "$baseDir\build"
	$sourceDir = "$baseDir\src"
	$toolsDir = "$baseDir\tools"
	$releaseDir = "$baseDir\release"
	$nuGetDir = "$baseDir\NuGet"

	$openCoverExecutable = "$sourceDir\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe"

	$buildNumber = Read-BuildNumber "$buildDir\BuildNumber.txt"
}


Task Default -depends Validate,Build,Test

Task Validate -depends Validate-Versions
Task Build -depends Build-Solution
Task Pack -depends Pack-Solution
Task Test -depends Test-Solution

Task Load-Powershell-Dependencies {
	Add-Type -Path "$sourceDir\packages\NuGet.Core.2.14.0\lib\net40-Client\NuGet.Core.dll"
}

Task Determine-Version-Suffix {
	$script:versionSuffix = ""

	if ($preReleaseTag -ne "")
	{
		$script:versionSuffix = "$preReleaseTag-$buildNumber"
	}
}

Task Resolve-Projects -depends Load-Powershell-Dependencies,Determine-Version-Suffix {
	$script:projects = [System.Collections.ArrayList]::new()
	
	$projectDirectories = Get-ChildItem "$sourceDir\System.Data.HashFunction.*" -Directory
	foreach ($projectDirectory in $projectDirectories)
	{
		$name = $projectDirectory.Name
		$path = "$sourceDir\$name"
		$projectXmlPath = "$path\$name.csproj"

		[xml] $projectObject = Get-Content $projectXmlPath

    $dashedVersionSuffix = "";
    
    if ($dashedVersionSuffix -ne "")
    {
      $dashedVersionSuffix = "-$script:versionSuffix";
    }

		$project = New-Object –TypeName PSObject –Prop @{
			Name = $name
			Path = $path
			ProjectXmlPath = $projectXmlPath
			SemanticVersion = [NuGet.SemanticVersion]::new($(Select-Xml "/Project/PropertyGroup/VersionPrefix/text()" $projectObject).ToString() + $dashedVersionSuffix)
			NuGetPath = "$nuGetDir\$name"
			NuGetPackageName = $name
			SkipPackaging = $name.StartsWith("System.Data.HashFunction.Test")
			RunTests = $name.StartsWith("System.Data.HashFunction.Test")
		}

		$script:projects.Add($project) > $null
	}
}

Task Resolve-Production-Versions -depends Load-Powershell-Dependencies,Resolve-Projects {
	foreach ($project in $script:projects)
	{
		if ($project.SkipPackaging)
		{
			continue
		}
			
		$versionResults = Invoke-RestMethod -Method Get $($nuGetBaseUrl + "FindPackagesById()?Id='" + $project.NuGetPackageName + "'&`$format=xml")

		$versions = New-Object -TypeName PSObject -Property @{
			Production = $(New-Object -TypeName PSObject -Property @{
				SemanticVersion = $null
				VcsRevision = $null
			})
			PreRelease = $(New-Object -TypeName PSObject -Property @{
				SemanticVersion = $null
				VcsRevision = $null
			}) 
		}

		foreach ($entry in $versionResults)
		{
			$entrySemanticVersion = [NuGet.SemanticVersion]::new($entry.properties.Version)
			$entryVcsRevision = Parse-VCS-Revision $entry.properties.ReleaseNotes

			$isLatestVersion = [Boolean]::Parse($entry.properties.IsLatestVersion.InnerText);
			$isAbsoluteLatestVersion = [Boolean]::Parse($entry.properties.IsLatestVersion.InnerText);

			if ($isLatestVersion)
			{
				$versions.Production.SemanticVersion = $entrySemanticVersion
				$versions.Production.VcsRevision = $entryVcsRevision
			}

			if ($isAbsoluteLatestVersion)
			{
				$versions.PreRelease.SemanticVersion = $entrySemanticVersion
				$versions.PreRelease.VcsRevision = $entryVcsRevision
			}
		}

	    Add-Member -InputObject $project -MemberType NoteProperty -Name "Versions" -Value $versions
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


		$packageRevision = $project.Production.VcsRevision

		if ($project.Versions.Production.Version -ne $null -and $project.Versions.Production.SemanticVersion.Version -gt $project.SemanticVersion.Version)
		{
			Write-Host "Newer production version already exists, version bump required." -ForegroundColor Red
			$anyVersionBumpRequired = $true;

		} else  {
			if ($packageRevision -ne $null)
			{
				$fileChanges = $(& $gitExecutable diff $packageRevision $project.Path)

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

task Build-Solution -depends Resolve-Projects,Determine-Version-Suffix {	
	Exec { & $dotNetExecutable restore "$sourceDir" }
	
	if ($preReleaseTag -ne "")
	{
		Exec { & $dotNetExecutable build "$sourceDir" -c $configuration --version-suffix $script:versionSuffix }

	} else {
		Exec { & $dotNetExecutable build "$sourceDir" -c $configuration }
	}
}

task Pack-Solution -depends Resolve-Projects,Resolve-Production-Versions,Determine-Version-Suffix {
	Write-Host "Build Number: $buildNumber"
	if (-Not (Test-Path $artifactsDir))
	{
		New-Item $artifactsDir -ItemType Directory > $null
	}

	if (Test-Path "$artifactsDir\Packages")
	{
		Remove-Item "$artifactsDir\Packages\*" -Force
	} else {
		New-Item "$artifactsDir\Packages" -ItemType Directory > $null
	}
	
	foreach ($project in $script:projects)
	{
		if ($project.SkipPackaging)
		{
			continue
		}
		
		
		if ($script:versionSuffix -ne "")
		{
			Exec { & $dotNetExecutable pack $project.ProjectXmlPath -c $configuration --version-suffix $script:versionSuffix -o "$artifactsDir\Packages"  }

		} else {
			if ($project.Versions.Production.SemanticVersion.Version -eq $null -Or $project.Versions.Production.SemanticVersion.Version -lt $project.SemanticVersion.Version)
			{
				Exec { & $dotNetExecutable pack $project.ProjectXmlPath -c $configuration -o "$artifactsDir\Packages" }
			} else {
				Write-Host $project.Versions.PreRelease.SemanticVersion;
				Write-Host $project.Versions.Production.SemanticVersion;
			}
		}
	}
}

task Test-Solution -depends Resolve-Projects {
	
	if (-Not (Test-Path $artifactsDir))
	{
		New-Item $artifactsDir -ItemType Directory > $null
	}

	if (Test-Path "$artifactsDir\Coverage")
	{
		Remove-Item "$artifactsDir\Coverage\*" -Force
	} else {
		New-Item "$artifactsDir\Coverage" -ItemType Directory > $null
	}

	foreach ($project in $script:projects)
	{
		if ($project.RunTests)
		{
			Exec { & $openCoverExecutable "-target:$dotNetExecutable" $("`"-targetargs:test " + $project.ProjectXmlPath + " -c $configuration`"") -nodefaultfilters $("`"-filter:+[System.Data.HashFunction.*]* -[" + $project.Name + "]*`"") $("`"-output:$artifactsDir\Coverage\" + $project.Name + ".xml`"") -register:user -oldStyle }
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
		New-Item $buildNumberFilePath -ItemType File -Value $buildNumber > $null
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
