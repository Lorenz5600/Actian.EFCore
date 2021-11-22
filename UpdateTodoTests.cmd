@echo off
setlocal

cd %~dp0

set DISABLE_ACTIAN_TODO=true

@REM dotnet test Actian.EFCore.sln --configuration Release --verbosity quiet --logger "actian-json;LogFileName=dev.json" --logger "console;verbosity=quiet"
dotnet test Actian.EFCore.sln --configuration Release --verbosity quiet --logger "actian-json;LogFileName=dev.json"

dotnet run --project utils\UpdateTestTodos\UpdateTestTodos.csproj
