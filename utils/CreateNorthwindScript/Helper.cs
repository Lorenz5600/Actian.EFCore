using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace CreateNorthwindScript
{
    public static class Helper
    {
        public static string SolutionRoot => GetSolutionRoot();
        public static string FunctionalTestsDir => Path.Combine(SolutionRoot, "test", "Actian.EFCore.FunctionalTests");
        public static string NorthwindDir => Path.Combine(FunctionalTestsDir, "Northwind");
        public static string NorthwindSqlPath => Path.Combine(NorthwindDir, "Northwind.sql");
        public static string NorthwindAsciiSqlPath => Path.Combine(NorthwindDir, "Northwind.ascii.sql");

        private static string GetSolutionRoot([CallerFilePath] string path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Could not find solution root");

            if (File.Exists(Path.Combine(path, "Actian.EFCore.sln")))
                return path;

            return GetSolutionRoot(Path.GetDirectoryName(path));
        }
    }
}
