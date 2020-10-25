Param (
	[string] $package,
	[string] $apiKey,
	[string] $source = "nuget.org"
)

dotnet nuget push --api-key $apiKey --source $source $package