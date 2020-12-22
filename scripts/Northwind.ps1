function script:exec {
  [CmdletBinding()]

  param(
    [Parameter(Position = 0, Mandatory = 1)][scriptblock]$cmd
  )
  & $cmd
  if ($lastexitcode -ne 0) {
    exit $lastexitcode
  }
}

$testDir = Join-Path $PSScriptRoot ..\test\Actian.EFCore.FunctionalTests -Resolve

Set-Location -Path $testDir

exec { destroydb -udbo efcore_northwind }
exec { createdb -n -udbo efcore_northwind }

cmd /c "sql -udbo efcore_northwind < Northwind.v10.sql"

