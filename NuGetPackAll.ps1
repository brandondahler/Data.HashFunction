foreach ($directory in get-childitem -Directory -Exclude:.nuget,.git,packages,Test,TestResults,Utilities)
{
	foreach ($project in get-childitem -File $directory\*.csproj)
	{
		nuget pack $project -Properties "Configuration=NuGetDeploy;SolutionDir=""$directory\..""" -IncludeReferencedProjects;
	}
}