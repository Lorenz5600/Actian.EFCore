@echo off

cd %~dp0

dotnet run --project utils/Actian.TestLoggers/Actian.TestLoggers.csproj -- --format md-files --title "Test Run for development" --output "TestResults/development" test/*/TestResults/**/*.json
