param (
	[Parameter()]
	[string] $taskList,
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

Import-Module $(Resolve-Path "$baseDir\src\packages\psake.4.6.0\tools\psake.psm1")

$properties = @{}
foreach ($parameterKey in $PSBoundParameters.Keys)
{
	if ($parameterKey -eq "taskList")
	{
		continue
	}
	
	$properties.Add($parameterKey, $PSBoundParameters[$parameterKey])
}

Invoke-psake "$buildDir\build-configuration.ps1" -taskList $taskList -properties $properties