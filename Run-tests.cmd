@echo off
setlocal

cd %~dp0

dotnet test Actian.EFCore.sln --configuration Release --verbosity quiet --logger "actian-json;LogFileName=dev.json"
