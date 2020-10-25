Param (
	[string] $output = "$PSScriptRoot/artifacts",
	[string] $configuration = "Release"
)

dotnet pack --nologo --configuration $configuration --output $output "$PSScriptRoot/src/HappySkorpion.FioClient/HappySkorpion.FioClient.csproj"