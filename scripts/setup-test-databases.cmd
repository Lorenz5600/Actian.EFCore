@echo off
setlocal

cd %~dp0\..\utils\Actian.EFCore.Build

dotnet run -- setup-test-databases
