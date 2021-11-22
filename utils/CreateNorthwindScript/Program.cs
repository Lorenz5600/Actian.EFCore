using System;
using System.IO;
using System.Text;
using Actian.Scripts;

namespace CreateNorthwindScript
{
    class Program
    {
        static void Main()
        {
            try
            {
                NormalizeNorthwind();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private static void NormalizeNorthwind()
        {
            Console.WriteLine($"SolutionRoot:          {Helper.SolutionRoot}");
            Console.WriteLine($"FunctionalTestsDir:    {Helper.FunctionalTestsDir}");
            Console.WriteLine($"NorthwindSqlPath:      {Helper.NorthwindSqlPath}");
            Console.WriteLine($"NorthwindAsciiSqlPath: {Helper.NorthwindAsciiSqlPath}");
            Console.WriteLine($"Creating {Helper.NorthwindAsciiSqlPath}");
            var script = File.ReadAllText(Helper.NorthwindSqlPath);
            using var output = new StreamWriter(Helper.NorthwindAsciiSqlPath, false, Encoding.ASCII);
            ActianScripts.Normalize(script, output, options => options.WithScriptDirectory(Helper.NorthwindDir));
            Console.WriteLine($"Done!");
        }
    }
}
