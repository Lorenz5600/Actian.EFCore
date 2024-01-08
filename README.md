# Actian.EFCore

[Entity Framework Core](https://github.com/dotnet/efcore) provider for Actian X, Ingres, and Vector.

Also see previous https://github.com/ActianCorp/EntityFramework6.Ingres/

## Installing Actian.EFCore

### Before installing

Actian.EFCore relies on Actian.Client version 3.0.0 and above to work. Actian.Client has not been published to [NuGet.org], so it will have to be added to a local or private NuGet feed, which the project has access to.

Actian.Client can be downloaded from [Customer Downloads].

For help setting up a local NuGet feed see: [Setting up Local NuGet Feeds | Microsoft Docs].

Beta versions of Actian.EFCore can be installed from [Actian.EFCore Packages].

### Install using the .NET Core CLI

```
dotnet add package Actian.EFCore
```

### Install using the NuGet Package Manager Console in Visual Studio

```powershell
Install-Package Actian.EFCore
```

## Testing

The Actian.EFCore solution contains a number of automated tests.

### Configuration for tests

When running tests the database server to be used is specified by the environment variable `ACTIAN_TEST_CONNECTION_STRING`. This variable should contain an Actian client connection string specifying:

- The actian server
- The port
- The dabase user id
- The password for the dabase user id
- Persist Security Info=true

Example:
```
Server=actian-client-test;Port=II7;User ID=efcore_test;Password=xxxxxxxxxx;Persist Security Info=true
```

The connection string should _not_ specify the database.

The database user specified in the connection string should:
- have permission to create new database users
- have permission to impersonate other database users

### Preparing to test

A number of databases, owned by the `"dbo"` user, need to be created before running tests.

These databases can be created on the machine that hosts the database server by running the following script:

```
scripts\setup-test-databases.cmd
```

The environment variable `ACTIAN_TEST_CONNECTION_STRING` must be have a valid value for this to work.

The user that runs this script should:

- have permission to create new database users
- have permission to create new databases
- have permission to impersonate other database users

When running `scripts\setup-test-databases.cmd` the following users will be created:
- `"dbo"`
- `"db2"`
- `"db.2"`

### Running tests

These tests can be run in Visual Studio or from the command line:

```
dotnet test
```

The environment variable `ACTIAN_TEST_CONNECTION_STRING` must be have a valid value for this to work.

## Continous integration

Actian.EFCore is built and tested using build script `.github/workflows/build.yml`. A build is started when:

- Changes are pushed to branch `main`.
- Changes are pushed to a branch that has a pull request to branch `main`. _This has been disabled for now_

Test results are saved to branch [TestResults] branch of the [Actian.EFCore repository].

The tests are run in each of the following environments:

### WIN64_INGRES_11_2_0, Ingres

- Host: Actian1
- Windows Server 2022 64 bit
- Actian X server 11.2.0
- Compatibility: Ingres
- Installation: CI
- `ACTIAN_TEST_CONNECTION_STRING`:   
  "localhost;Port=CI7;User ID=efcore_test;Password=xxxxxxxxxx;Persist Security Info=true"
- Latest test results for main branch:   
  <https://github.com/2PS-Consulting/Actian.EFCore/blob/TestResults/Branch-main/localhost-CI7/Index.md>

### WIN64_INGRES_11_2_0, Ansi

- Host: Actian1
- Windows Server 2022 64 bit
- Actian X server 11.2.0
- Compatibility: ANSI/ISO Entry SQL-92
- Installation: CA
- `ACTIAN_TEST_CONNECTION_STRING`:   
  "localhost;Port=CA7;User ID=efcore_test;Password=xxxxxxxxxx;Persist Security Info=true"
- Latest test results for main branch:   
  <https://github.com/2PS-Consulting/Actian.EFCore/blob/TestResults/Branch-main/localhost-CA7/Index.md>


[Customer Downloads]: https://esd.actian.com/
[Setting up Local NuGet Feeds | Microsoft Docs]: https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds
[NuGet.org]: https://www.nuget.org/
[Actian.EFCore Packages]: https://github.com/2PS-Consulting/Actian.EFCore/pkgs/nuget/Actian.EFCore
[TestResults]: https://github.com/2PS-Consulting/Actian.EFCore/tree/TestResults
[Actian.EFCore repository]: https://github.com/2PS-Consulting/Actian.EFCore
