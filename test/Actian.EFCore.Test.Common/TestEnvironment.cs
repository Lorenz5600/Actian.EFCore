using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ingres.Client;
using Xunit.Abstractions;

namespace Actian.EFCore.TestUtilities
{
    public static class TestEnvironment
    {
        public static void Log(object testObject, ITestOutputHelper testOutputHelper)
        {
            LogInternal(GetImplementedClass(testObject?.GetType()), testOutputHelper);
        }

        private static Type GetImplementedClass(Type type, Assembly assembly = null)
        {
            if (type is null)
                return null;

            assembly ??= type.Assembly;

            if (type.Assembly != assembly)
                return type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            return GetImplementedClass(type.BaseType, assembly);
        }

        private static void LogInternal(Type implementedClass, ITestOutputHelper testOutputHelper)
        {
            testOutputHelper.WriteLine($"### Actian Server: {GetConnectionStringParameter(cb => cb.Server)}");
            testOutputHelper.WriteLine($"### Actian Server Port: {GetConnectionStringParameter(cb => cb.Port)}");
            testOutputHelper.WriteLine($"### Actian Server Version: {ActianServerVersion}");
            testOutputHelper.WriteLine($"### Actian Server Compatibilty: {ActianServerCompatibilty}");
            if (implementedClass != null)
            {
                testOutputHelper.WriteLine($"### Test class implements: {implementedClass.PrettyName(true)}");
            }
        }

        public static string EFCoreTestConnectionString
        {
            get
            {
                var connectionString = Environment.GetEnvironmentVariable($"ACTIAN_TEST_CONNECTION_STRING");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception($"Test connection string not found");
                return connectionString;
            }
        }

        public static string LoginUser => new IngresConnectionStringBuilder(EFCoreTestConnectionString).UserID;

        public static string OutputDirectory => Path.GetDirectoryName(typeof(TestEnvironment).GetTypeInfo().Assembly.Location);

        public static readonly TestDatabases TestDatabases = TestDatabases.Load(GetTestPath("test-databases.json"));

        private static T GetConnectionStringParameter<T>(Func<IngresConnectionStringBuilder, T> get)
            => get(new IngresConnectionStringBuilder(EFCoreTestConnectionString));

        public static string Server
            => GetConnectionStringParameter(c => c.Server);

        public static string Port
            => GetConnectionStringParameter(c => c.Port);

        public static string GetConnectionString(string database)
            => TestDatabases.GetConnectionString(database);

        public static string GetDatabaseName(string database)
            => TestDatabases.GetDatabaseName(database);

        private static readonly Lazy<string> _serverVersion = new Lazy<string>(() =>
        {
            using var connection = new IngresConnection(GetConnectionString("iidbdb"));
            connection.Open();
            return connection.ServerVersion;
        });

        public static string ServerVersion => _serverVersion.Value;

        private static readonly Lazy<ActianServerVersion> _actianServerVersion = new Lazy<ActianServerVersion>(() => ActianServerVersion.Parse(ServerVersion));
        public static ActianServerVersion ActianServerVersion => _actianServerVersion.Value;


        private static readonly Lazy<(string dbNameCase, string dbDelimitedCase)> _dbCasing = new Lazy<(string dbNameCase, string dbDelimitedCase)>(() =>
        {
            using var connection = new IngresConnection(GetConnectionString("iidbdb"));
            using var cmd = connection.CreateCommand();

            cmd.CommandText = "select dbmsinfo('db_name_case'), dbmsinfo('db_delimited_case')";

            connection.Open();

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return ("", "");

            var dbNameCase = reader.GetString(0)?.ToUpperInvariant();
            var dbDelimitedCase = reader.GetString(1)?.ToUpperInvariant();

            return (dbNameCase, dbDelimitedCase);
        });

        public static string ActianServerCompatibilty
        {
            get
            {
                var (dbNameCase, dbDelimitedCase) = _dbCasing.Value;

                if (dbNameCase == "UPPER" && dbDelimitedCase == "MIXED")
                    return "ANSI/ISO Entry SQL-92";

                if (dbNameCase == "LOWER" && dbDelimitedCase == "LOWER")
                    return "Ingres";

                return "Unknown";
            }
        }

        public static string ModifyConnectionString(string connectionString, Action<IngresConnectionStringBuilder> modify)
        {
            var cb = new IngresConnectionStringBuilder(connectionString);
            modify(cb);
            return cb.ConnectionString;
        }

        public static string MaskPasswords(string connectionString) => ModifyConnectionString(connectionString, cb =>
        {
            if (!string.IsNullOrEmpty(cb.Password))
                cb.Password = "xxxxx";
            if (!string.IsNullOrEmpty(cb.DbmsPassword))
                cb.DbmsPassword = "xxxxx";
            if (!string.IsNullOrEmpty(cb.RolePassword))
                cb.RolePassword = "xxxxx";
        });

        public static string ProjectDirectory => GetProjectDirectory();
        public static string SolutionDirectory => GetSolutionDirectory();
        public static string LogDirectory
        {
            get
            {
                var logDirectory = Path.Combine(SolutionDirectory, "Log");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                return logDirectory;
            }
        }

        public static bool IsCI => Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null;

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

        private static string GetSolutionDirectory([CallerFilePath] string callerPath = "")
        {
            string getSolutionDirectory(string dir)
            {
                if (File.Exists(Path.Combine(dir, "Actian.EFCore.sln")))
                    return dir;

                if (dir == Path.GetPathRoot(dir))
                    throw new Exception("Could not find the solution directory");

                return getSolutionDirectory(Path.GetDirectoryName(dir));
            }

            return getSolutionDirectory(Path.GetDirectoryName(callerPath));
        }

        private static string GetTestPath(string filename, [CallerFilePath] string callerPath = "")
        {
            return GetTestPathInternal(Path.GetDirectoryName(callerPath), filename);
        }

        private static string GetTestPathInternal(string dir, string filename)
        {
            if (File.Exists(Path.Combine(dir, filename)))
                return Path.Combine(dir, filename);

            if (dir == Path.GetPathRoot(dir))
                throw new Exception($"Could not find {Path.Combine(dir, filename)}");

            return GetTestPathInternal(Path.GetDirectoryName(dir), filename);
        }
    }
}
