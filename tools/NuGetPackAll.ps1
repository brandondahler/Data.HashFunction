
foreach ($projectShortName in (get-childitem -Directory src\ -Exclude:.nuget,packages,Test,TestResults | % { $_.Name }))
{
  $projectName = "Data.HashFunction.$projectShortName";
  $projectFile = "$projectDirectory\$projectName.csproj"
	$projectDirectory = "src\$projectShortName";
	$projectNuGetDirectory = "NuGet\$projectShortName";
	
	& cp "$projectDirectory\bin\*" "$projectNuGetDirectory\lib\"
	
	if ($directory -ne "Core")
	{
		& rm "$projectNuGetDirectory\lib\**\System.Data.HashFunction.Core.*";
	}


	$fileInfo = [Diagnostics.FileVersionInfo]::GetVersionInfo(
		$(resolve-path "$nugetDirectory\lib\net45\System.$projectName.dll"));

	$properties = @{
		"id" = $fileInfo.ProductName;
		"version" = $fileInfo.ProductVersion;
		"title" = $fileInfo.FileDescription;
		"author" = $fileInfo.CompanyName;
		"description" = $fileInfo.Comments;
	};


	nuget pack "$projectNuGetDirectory\$projectName.nuspec" `
		-BasePath $projectNuGetDirectory `
		-Properties $(($properties.GetEnumerator() | %{"$($_.Key)=$($_.Value)"}) -join ";") `
		-OutputDirectory NuGet\
	;
}

