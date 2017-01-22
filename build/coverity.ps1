param (
	[Parameter()]
	[string] $configuration,
	[Parameter()]
	[string] $preReleaseTag,
	[Parameter()]
	[string] $buildNumber,
	[Parameter()]
	[string] $gitExecutable,
	[Parameter()]
	[string] $dotNetExecutable
)


$buildDir = $PSScriptRoot
$baseDir  = Resolve-Path $PSScriptRoot\.. -Relative


if (-Not (Test-Path Artifacts))
{
    New-Item Artifacts -ItemType Directory > $null
}
      
if (Test-Path "Artifacts\Coverity")
{
    Remove-Item "Artifacts\Coverity\*" -Force -Recurse
} else {
    New-Item "Artifacts\Coverity" -ItemType Directory > $null
}


$buildArguments = "-taskList `"Build`" "

foreach ($parameterKey in $PSBoundParameters.Keys)
{
	$buildArguments += "-{0} `"{1}`" " -f $parameterKey,$PSBoundParameters[$parameterKey]
}


Invoke-Expression "cov-build.exe --dir $baseDir\Artifacts\Coverity powershell.exe `-NonInteractive `-File $buildDir\build.ps1 $buildArguments"

& "$baseDir\src\packages\PublishCoverity.0.11.0\tools\PublishCoverity.exe" compress -o "$baseDir\Artifacts\Coverity\Coverity.zip" -i "$baseDir\Artifacts\Coverity"
& "$baseDir\src\packages\PublishCoverity.0.11.0\tools\PublishCoverity.exe" publish -z "$baseDir\Artifacts\Coverity\Coverity.zip" -e "brandon.dahler@gmail.com" -r "brandondahler/Data.HashFunction" -t "$Env:COVERITY_TOKEN" --codeVersion "$preReleaseTag-$buildNumber"