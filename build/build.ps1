param (
	[Parameter()]
	[string] $taskList,
	[Parameter()]
	[string] $configuration = "Debug"
)

$buildDir = $PSScriptRoot

Import-Module $(Resolve-Path "packages\psake.4.6.0\tools\psake.psm1")

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