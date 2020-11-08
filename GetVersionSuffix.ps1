if ($env:GITHUB_REF -match "^refs/heads/.+") {
  $Branch = $env:GITHUB_REF -replace "^refs/heads/", ""
}
elseif ($env:GITHUB_HEAD_REF) {
  $Branch = $env:GITHUB_HEAD_REF
}
else {
  $Branch = &git rev-parse --abbrev-ref HEAD
}

if ($Branch -eq "main") {
  $Branch = "beta"
}

$Timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$VersionSuffix = "$Branch-$Timestamp".ToLower()

Write-Host "Branch: $Branch"
Write-Host "Timestamp: $Timestamp"
Write-Host "Version suffix: $VersionSuffix"

if ($env:GITHUB_ENV) {
  Write-Host "In GitHub"
  "VersionSuffix=$VersionSuffix" | Out-File -FilePath $env:GITHUB_ENV -Encoding utf8 -Append
} else {
  Write-Host "Local"
  $env:VersionSuffix = $VersionSuffix
}
