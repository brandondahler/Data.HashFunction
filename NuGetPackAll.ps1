
foreach ($directory in (get-childitem -Directory -Exclude:.nuget,.git,packages,NuGet,Test,TestResults,Utilities | % { $_.Name }))
{
	$project = "$directory\Data.HashFunction.$directory.csproj";
	$nugetDirectory = "NuGet\$directory";
	
	& ${env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe $project /t:Rebuild `
		/p:TargetFrameworkVersion=v4.0 `
		/p:AssemblyOriginatorKeyFile=..\Data.HashFunction.Production.pfx `
		/p:SignAssembly=true `
		"/p:OutputPath=..\$nugetDirectory\lib\net40\" `
		/p:DebugType=none `
		/p:DefineConstants=`"TRACE`;NuGetDeploy`;NET40`";

	& ${env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe $project /t:Rebuild `
		/p:TargetFrameworkVersion=v4.5 `
		/p:AssemblyOriginatorKeyFile=..\Data.HashFunction.Production.pfx `
		/p:SignAssembly=true `
		"/p:OutputPath=..\$nugetDirectory\lib\net45\" `
		/p:DebugType=none `
		/p:DefineConstants=`"TRACE`;NuGetDeploy`";

	if ($directory -ne "Core")
	{
		& rm $nugetDirectory\lib\net40\System.Data.HashFunction.Core.*;
		& rm $nugetDirectory\lib\net45\System.Data.HashFunction.Core.*;
	}


	$fileInfo = [Diagnostics.FileVersionInfo]::GetVersionInfo(
		$(resolve-path "$nugetDirectory\lib\net45\System.Data.HashFunction.$directory.dll"));

	$properties = @{
		"id" = $fileInfo.ProductName;
		"version" = $fileInfo.ProductVersion;
		"title" = $fileInfo.FileDescription;
		"author" = $fileInfo.CompanyName;
		"description" = $fileInfo.Comments;
	};


	nuget pack $nugetDirectory\Data.HashFunction.$directory.nuspec `
		-BasePath $nugetDirectory `
		-Properties $(($properties.GetEnumerator() | %{"$($_.Key)=$($_.Value)"}) -join ";") `
		-OutputDirectory NuGet\
	;
}

