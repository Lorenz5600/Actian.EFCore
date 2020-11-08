# Actian.EFCore

Entity Framework Core provider for Actian

## Installing Actian.EFCore

### Before installing

Actian.EFCore relies on Actian.Client version 3.0.0 and above to work. Actian.Client has not been published to [NuGet.org], so it will have to be added to a local or private NuGet feed, which the project has access to.

Actian.Client can be downloaded from [Customer Downloads].

For help setting up a local NuGet feed see: [Setting up Local NuGet Feeds | Microsoft Docs].

### Install using the .NET Core CLI

```
dotnet add package Actian.EFCore
```

### Install using the NuGet Package Manager Console in Visual Studio

```powershell
Install-Package Actian.EFCore
```

## Testing

The Actian.EFCore solution contains a number of automated tests. These tests can be run in Visual Studio or from the command line:

```
dotnet test
```

When testing Actian.EFCore test environments are used to enable testing with different Actian database servers.

A test environment is identified by a name (eg. `WIN64_INGRES_10_1_0`).

To run the tests with a specific test environment first define the environment variable `ACTIAN_EFCORE_ENVIRONMENT` to equal the test environment name. Eg.:
```
set ACTIAN_EFCORE_ENVIRONMENT=WIN64_INGRES_10_1_0
```

The tests will then run with the connection string defined in the environment variable `ACTIAN_EFCORE_<test environment name>` (eg. `ACTIAN_EFCORE_WIN64_INGRES_10_1_0`). The connection string for a test environment must include:
```
Persist Security Info=true
```

The database user specified in the connection string (default: `efcore_test`) should be the owner of the test database and have permission to create new database users. The tests will create the following database users, if the do not already exist:
- `efcore_test1`
- `efcore_test2`
- `efcore_test.2`

The database server for each test environment must have the following databased, owned by the user specified in the connection string (default: `efcore_test`):
- The database specified in the connection string (default: `efcore_test`)
- `efcore_databasemodelfactory`

These databases can be created using command like:

```
createdb -uefcore_test -n efcore_test
createdb -uefcore_test -n efcore_databasemodelfactory
```

At the moment the following test environments are defined:

### Default test environment

This test environment is used if the environment variable `ACTIAN_EFCORE_ENVIRONMENT` is not defined - typically when testing in Visual Studio.

- Name: `DEFAULT`
- Environment variable containing connection string: `ACTIAN_EFCORE_DEFAULT`
- Platform: Windows 64 bit
- Database server: ActianX 11.1.0

### Test environment `WIN64_INGRES_10_1_0`

This test environment is used in the continuous integration build script (`.github/workflows/build.yml`).

- Name: `WIN64_INGRES_10_1_0`
- Environment variable containing connection string: `ACTIAN_EFCORE_WIN64_INGRES_10_1_0`
- Platform: Windows 64 bit
- Database server: Ingres 10.1.0

### Test environment `WIN64_ACTIANX_11_1_0`

This test environment is used in the continuous integration build script (`.github/workflows/build.yml`).

- Name: `WIN64_ACTIANX_11_1_0`
- Environment variable containing connection string: `ACTIAN_EFCORE_WIN64_ACTIANX_11_1_0`
- Platform: Windows 64 bit
- Database server: ActianX 11.1.0

## Continous integration

Actian.EFCore is tested and built using build script `.github/workflows/build.yml`. A build is started when:

- Changes are pushed to branch `main`.
- Changes are pushed to a branch that has a pull request to branch `main`.

The tests are run with the following test environments:

- `WIN64_INGRES_10_1_0`
- `WIN64_ACTIANX_11_1_0`

The environment variable containing the connection string for each of these test environments must be specified for the tests to succeed.


[Customer Downloads]: https://esd.actian.com/
[Setting up Local NuGet Feeds | Microsoft Docs]: https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds
[NuGet.org]: https://www.nuget.org/
