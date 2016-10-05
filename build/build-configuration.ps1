$baseDir  = resolve-path $PSScriptRoot\..

properties {
	$majorVersion = "2.0"
	$majorWithReleaseVersion = "2.0.0"
	$nugetPrerelease = $null
	$version = GetVersion $majorWithReleaseVersion
	$packageIdBase = "System.Data.HashFunction"
	$signAssemblies = $false
	$signKeyPath = "$baseDir\Data.HashFunction.Production.pfx"
	$buildDocumentation = $false
	$buildNuGet = $true
	$treatWarningsAsErrors = $true
	$workingName = if ($workingName) {$workingName} else {"working"}
  
	$buildDir = "$baseDir\build"
	$sourceDir = "$baseDir\src"
	$toolsDir = "$baseDir\tools"
	$docDir = "$baseDir\doc"
	$releaseDir = "$baseDir\release"
	$workingDir = "$baseDir\$workingName"
	$workingSourceDir = "$workingDir\src"
	$builds = @(
		@{Name = "Interfaces";       Directory = "System.Data.HashFunction.Interfaces"},
		@{Name = "Core";             Directory = "System.Data.HashFunction.Core"},
								     
		@{Name = "BernsteinHash";    Directory = "System.Data.HashFunction.BernsteinHash"},
		@{Name = "Blake2";           Directory = "System.Data.HashFunction.Blake2"},
		@{Name = "BuzHash";          Directory = "System.Data.HashFunction.BuzHash"},
		@{Name = "CityHash";         Directory = "System.Data.HashFunction.CityHash"},
		@{Name = "CRC";              Directory = "System.Data.HashFunction.CRC"},
		@{Name = "ELF64";            Directory = "System.Data.HashFunction.ELF64"},
		@{Name = "FNV";              Directory = "System.Data.HashFunction.FNV"},
		@{Name = "HashAlgorithm";    Directory = "System.Data.HashFunction.HashAlgorithm"},
		@{Name = "Jenkins";          Directory = "System.Data.HashFunction.Jenkins"},
		@{Name = "MurmurHash";       Directory = "System.Data.HashFunction.MurmurHash"},
		@{Name = "Pearson";          Directory = "System.Data.HashFunction.Pearson"},
		@{Name = "SpookyHash";       Directory = "System.Data.HashFunction.SpookyHash"},
		@{Name = "xxHash";           Directory = "System.Data.HashFunction.xxHash"},
								     
		@{Name = "Test";             Directory = "System.Data.HashFunction.Test"}
	)
}

framework '4.0x86'


task default -depends Test


# Ensure a clean working directory
task Clean {
	Write-Host "Setting location to $baseDir"
	Set-Location $baseDir
  
	if (Test-Path -path $workingDir)
	{
		Write-Host "Deleting existing working directory $workingDir"
    
		Execute-Command -command { del $workingDir -Recurse -Force }
	}
  
	Write-Host "Creating working directory $workingDir"
	New-Item -Path $workingDir -ItemType Directory > $null
}

# Build each solution, optionally signed
Task Build-Common -depends Clean {
	$excludeMatches = @(
		"[\\/]bin([\\/]|$)", 
		"[\\/]obj([\\/]|$)", 
		"[\\/]TestResults([\\/]|$)", 
		"[\\/]packages([\\/]|$)", 
		"[\\/]\.vs([\\/]|$)", 
		"\.suo$", 
		"\.user$", 
		"\.lock\.json$")
	[regex] $excludeMatchRegEx = '(?i)' + ($excludeMatches –join "|" ) + ''

	Write-Host "Copying source to working source directory $workingSourceDir"
	
	Get-ChildItem -Path $sourceDir -Recurse -Exclude $exclude | 
		where { $_.FullName.Replace($sourceDir, "") -notmatch $excludeMatchRegEx} |
		Copy-Item -Destination {
			Join-Path $workingSourceDir $_.FullName.Substring($sourceDir.length)
		}


	Write-Host -ForegroundColor Green "Updating assembly version"
	Write-Host
	Update-AssemblyInfoFiles $workingSourceDir ($majorVersion + '.0.0') $version

	
	foreach ($build in $builds)
	{
		Update-Project $workingSourceDir\$($build.Directory)\project.json $signAssemblies
	}

}

task Build-Debug -depends Build-Common {
	
	foreach ($build in $builds)
	{
		$name = $build.Name

		Write-Host -ForegroundColor Green "Building " $name
		Write-Host -ForegroundColor Green "Configuration Debug"
		Write-Host -ForegroundColor Green "Signed " $signAssemblies
		Write-Host -ForegroundColor Green "Key " $signKeyPath
		
		Update-Project $workingSourceDir\$($build.Directory)\project.json $signAssemblies

		& Invoke-Build $build "Debug"
	}
}

task Build-Release -depends Build-Common {
	
	foreach ($build in $builds)
	{
		$name = $build.Name

		Write-Host -ForegroundColor Green "Building " $name
		Write-Host -ForegroundColor Green "Configuration Debug"
		Write-Host -ForegroundColor Green "Signed " $signAssemblies
		Write-Host -ForegroundColor Green "Key " $signKeyPath
		
		Update-Project $workingSourceDir\$($build.Directory)\project.json $signAssemblies

		& Invoke-Build $build "Release"
	}
}


# Test each solution
task Test -depends Build-Debug {

	Write-Host -ForegroundColor Green "Testing"
		
	& Invoke-Tests @{Name = "Tests"; Directory = "System.Data.HashFunction.Test"} "Debug"
}

function Invoke-Build($build, $configuration)
{
	$name = $build.Name
	$directory = $build.Directory
	$projectPath = "$workingSourceDir\$directory\project.json"

	Write-Host -ForegroundColor Green "Restoring packages for $name"
	Write-Host
	exec { dotnet restore $projectPath | Out-Default }

	Write-Host -ForegroundColor Green "Building $projectPath $framework"
	exec { dotnet build $projectPath -c $configuration | Out-Default }
}


function Invoke-Tests($build, $configuration)
{
	$name = $build.Name
	$directory = $build.Directory

	exec { dotnet test "$workingSourceDir\$directory\project.json" -c $configuration | Out-Default }
}



function GetVersion($majorVersion)
{
    $now = [DateTime]::Now
    
    $year = $now.Year - 2000
    $month = $now.Month
    $totalMonthsSince2000 = ($year * 12) + $month
    $day = $now.Day
    $minor = "{0}{1:00}" -f $totalMonthsSince2000, $day
    
    $hour = $now.Hour
    $minute = $now.Minute
    $revision = "{0:00}{1:00}" -f $hour, $minute
    
    return $majorVersion + "." + $minor
}

function GetNuGetVersion()
{
  $nugetVersion = $majorWithReleaseVersion
  if ($nugetPrerelease -ne $null)
  {
    $nugetVersion = $nugetVersion + "-" + $nugetPrerelease
  }

  return $nugetVersion
}


function Update-AssemblyInfoFiles ([string] $workingSourceDir, [string] $assemblyVersionNumber, [string] $fileVersionNumber)
{
    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $assemblyVersionNumber + '")';
    $fileVersion = 'AssemblyFileVersion("' + $fileVersionNumber + '")';
    
    Get-ChildItem -Path $workingSourceDir -r -filter AssemblyInfo.cs | ForEach-Object {
        
        $filename = $_.Directory.ToString() + '\' + $_.Name
        Write-Host $filename
        $filename + ' -> ' + $version
    
        (Get-Content $filename) | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersion } |
            % {$_ -replace $fileVersionPattern, $fileVersion }
        } | Set-Content $filename
    }
}

function Update-Project {
	param (
		[string] $projectPath,
		[string] $sign
	)

	$file = switch($sign) { $true { $signKeyPath } default { $null } }

	$json = (Get-Content $projectPath) -join "`n" | ConvertFrom-Json

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

function Execute-Command($command) {
    $currentRetry = 0
    $success = $false

    do 
	{
        try
        {
            & $command
            $success = $true
        } catch [System.Exception] {
            if ($currentRetry -gt 5) {
                throw $_.Exception.ToString()
            } else {
                write-host "Retry $currentRetry"
                Start-Sleep -s 1
            }
            $currentRetry = $currentRetry + 1
        }
    } while (!$success)
}