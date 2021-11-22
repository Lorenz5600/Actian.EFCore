using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Ingres.Client;

namespace Actian.EFCore.Build
{
    public static class Config
    {
        public static string ExecutingDirectory => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        public static string SolutionRoot => GetDirUp("Actian.EFCore.sln");
        public static string TestDatabasesPath => GetPathUp("test-databases.json");
        public static string FunctionalTestsDir => Path.Combine(SolutionRoot, "test", "Actian.EFCore.FunctionalTests");
        public static string NorthwindDir => Path.Combine(FunctionalTestsDir, "Northwind");
        public static string NorthwindSqlPath => Path.Combine(NorthwindDir, "Northwind.sql");
        public static string NorthwindAsciiSqlName => "Northwind.ascii.sql";
        public static string NorthwindAsciiSqlPath => Path.Combine(NorthwindDir, NorthwindAsciiSqlName);
        public static string InstallationPath => Environment.GetEnvironmentVariable("II_SYSTEM");
        public static string InstallationCode => GetInstallationCode();
        public static string DbUserId => "efcore_test";
        public static string DbUserPassword => "efcore_test";
        public static string BaseConnectionString => $@"Server=localhost;Port={InstallationCode}7;User ID={DbUserId};Password={DbUserPassword};Trim Chars=True;Persist Security Info=true";

        public static string GetConnectionString(string database)
        {
            return new IngresConnectionStringBuilder(BaseConnectionString)
            {
                Database = database
            }.ConnectionString;
        }

        public static string GetConnectionString(string database, string dbmsUser)
        {
            return new IngresConnectionStringBuilder(BaseConnectionString)
            {
                Database = database,
                DbmsUser = $@"""{dbmsUser}"""
            }.ConnectionString;
        }

        private static string GetDirUp(string filename)
            => Path.GetDirectoryName(GetPathUp(filename));

        private static string GetPathUp(string filename)
        {
            static string getPathUpInternal(string dir, string filename)
            {
                if (File.Exists(Path.Combine(dir, filename)))
                    return Path.Combine(dir, filename);

                if (dir == Path.GetPathRoot(dir))
                    throw new Exception($"Could not find {Path.Combine(dir, filename)}");

                return getPathUpInternal(Path.GetDirectoryName(dir), filename);
            }

            return getPathUpInternal(ExecutingDirectory, filename);
        }

        private static string GetInstallationCode()
        {
            var installationCodeRe = new Regex(@"^\s*ii\.[^.]*\.setup\.ii_installation:\s*(\S\S)\s*$", RegexOptions.Compiled);
            foreach (var line in File.ReadLines(Path.Combine(InstallationPath, "ingres", "files", "config.dat")))
            {
                var match = installationCodeRe.Match(line);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            return null;
        }
    }
}
