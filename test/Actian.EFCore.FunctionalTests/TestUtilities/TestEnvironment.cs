using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Actian.EFCore.Scaffolding.DatabaseModelFactory;
using Ingres.Client;

namespace Actian.EFCore.TestUtilities
{
    public static class TestEnvironment
    {
        public static string EFCoreTestConnectionString
        {
            get
            {
                var connectionString = Environment.GetEnvironmentVariable($"EFCORE_TEST_CONNECTION_STRING");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception($"Test connection string not found");
                return connectionString;
            }
        }

        private static readonly HashSet<string> ValidDatabases = new HashSet<string>(new[]
        {
            ActianDatabaseModelFixture.DatabaseName,
            ActianNorthwindTestStoreFactory.DatabaseName,
            "iidbdb"
        }, StringComparer.InvariantCultureIgnoreCase);

        public static string GetConnectionString(string database, string dbmsUser)
        {
            database = database.ToLowerInvariant();

            if (!ValidDatabases.Contains(database))
                throw new Exception($"Database {database} can not be used for tests");

            return new IngresConnectionStringBuilder(EFCoreTestConnectionString)
            {
                Database = database,
                DbmsUser = dbmsUser
            }.ConnectionString;
        }

        public static string LoginUser => new IngresConnectionStringBuilder(EFCoreTestConnectionString).UserID;

        private static readonly Lazy<string> _serverVersion = new Lazy<string>(() =>
        {
            using var connection = new IngresConnection(GetConnectionString("iidbdb", LoginUser));
            connection.Open();
            return connection.ServerVersion;
        });
        public static string ServerVersion => _serverVersion.Value;

        private static readonly Lazy<ActianServerVersion> _actianServerVersion = new Lazy<ActianServerVersion>(() => ActianServerVersion.Parse(ServerVersion));
        public static ActianServerVersion ActianServerVersion => _actianServerVersion.Value;

        public static string ProjectDirectory => GetProjectDirectory();
        public static string LogDirectory
        {
            get
            {
                var logDirectory = Path.Combine(ProjectDirectory, ".logs");
                Directory.CreateDirectory(logDirectory);
                return logDirectory;
            }
        }

        private static string GetProjectDirectory([CallerFilePath] string callerPath = "")
        {
            string getProjectDirectory(string dir)
            {
                if (File.Exists(Path.Combine(dir, "Actian.EFCore.FunctionalTests.csproj")))
                    return dir;

                if (dir == Path.GetPathRoot(dir))
                    throw new Exception("Could not find the project directory");

                return getProjectDirectory(Path.GetDirectoryName(dir));
            }

            return getProjectDirectory(Path.GetDirectoryName(callerPath));
        }
    }
}
