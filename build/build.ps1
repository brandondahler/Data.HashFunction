$location = $PSScriptRoot

Start-Transcript "$PSScriptRoot\build.txt"
try
{
	Import-Module "$PSScriptRoot\..\tools\PSake\psake.psm1"
	Invoke-Expression "Invoke-psake -buildFile ""$PSScriptRoot\..\Build\build-configuration.ps1"" $args" 
} finally {
	Stop-Transcript
}

exit !($psake.build_success);