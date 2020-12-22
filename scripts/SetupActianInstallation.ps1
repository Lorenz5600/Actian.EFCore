$environmentVariables = @"
LIB=$env:II_SYSTEM\ingres\lib
INCLUDE=$env:II_SYSTEM\ingres\files
"@

$path = "$env:II_SYSTEM\ingres\bin;$env:II_SYSTEM\ingres\utility;$env:PATH"

Write-Host $environmentVariables
Write-Host "Path: $path"

$environmentVariables | Out-File -FilePath $env:GITHUB_ENV -Encoding utf8 -Append
$path | Out-File -FilePath $env:GITHUB_PATH -Encoding utf8 -Append
