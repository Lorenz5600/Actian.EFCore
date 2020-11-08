.\GetVersionSuffix.ps1

Remove-Item nupkg -Recurse -ErrorAction Ignore
dotnet pack src/Actian.EFCore/Actian.EFCore.csproj --configuration Release --output nupkg --version-suffix $env:VersionSuffix
