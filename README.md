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

When running tests the database server to be used is specified by the environment variable `ACTIAN_TEST_CONNECTION_STRING`. This variable should contain an Actian client connection string specifying:
- The actian server
- The port
- The dabase user id
- The password for the dabase user id
- Persist Security Info=true

Example:
```
Server=actian-client-test;Port=II7;User ID=efcore_test;Password=efcore_test;Persist Security Info=true
```

The connection string does _not_ need to specify the database.

The database user specified in the connection string should:
- have permission to create new database users
- have permission to impersonate other database users

A number of databases, owned by the `"dbo"` user, should be created before running tests.

These databases can be created on the machine that hosts the database server:

```
scripts\setup-test-databases.cmd
```

The user that runs this script should:
- have permission to create new database users
- have permission to create new databases
- have permission to impersonate other database users

When running `scripts\setup-test-databases.cmd` the following users will be created:
- `"dbo"`
- `"db2"`
- `"db.2"`

## Continous integration

Actian.EFCore is tested and built using build script `.github/workflows/build.yml`. A build is started when:

- Changes are pushed to branch `main`.
- Changes are pushed to a branch that has a pull request to branch `main`.

The tests are run in each of the following environments:

### WIN64_INGRES_10_1_0, Ingres

- Windows Server 2019 64 bit
- Ingres server 10.1.0
- Compatibility: Ingres

### WIN64_INGRES_10_1_0, Ansi

- Windows Server 2019 64 bit
- Ingres server 10.1.0
- Compatibility: ANSI/ISO Entry SQL-92

### WIN64_ACTIANX_11_1_0, Ingres

- Windows Server 2019 64 bit
- ActianX server 11.1.0
- Compatibility: Ingres

### WIN64_ACTIANX_11_1_0, Ansi

- Windows Server 2019 64 bit
- ActianX server 11.1.0
- Compatibility: ANSI/ISO Entry SQL-92


[Customer Downloads]: https://esd.actian.com/
[Setting up Local NuGet Feeds | Microsoft Docs]: https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds
[NuGet.org]: https://www.nuget.org/
