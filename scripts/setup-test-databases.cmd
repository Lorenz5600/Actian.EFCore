@echo off

cd %~dp0\..\utils\Actian.EFCore.Build

dotnet run -- setup-test-databases
