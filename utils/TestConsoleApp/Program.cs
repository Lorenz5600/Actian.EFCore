using System;
using System.IO;
using System.Text;
using Actian.Scripts;
using Ingres.Client;

namespace TestConsoleApp
{
    class Program
    {
        static readonly string SolutionRoot = Helper.GetSolutionRoot();
        static readonly string FunctionalTestsDir = Path.Combine(SolutionRoot, "test", "Actian.EFCore.FunctionalTests");

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Specify script");
                Environment.Exit(1);
            }

            try
            {
                ExecuteScript(Path.Combine(TestEnvironment.ProjectDirectory, args[0]));
            }
            catch (Exception ex)
            {
                PrintException(ex);
                Environment.Exit(1);
            }
        }

        internal static void ExecuteScript(string scriptPath)
        {
            Console.WriteLine($"Running {scriptPath}");
            var filename = Path.GetFileNameWithoutExtension(scriptPath);
            var extension = Path.GetExtension(scriptPath);
            var logfilePath = Path.Combine(TestEnvironment.LogDirectory, $"{filename}.log{extension}");
            using var logFile = new StreamWriter(logfilePath, false, Encoding.UTF8) { NewLine = "\n" };
            using var log = new CompositeTextWriter(logFile, Console.Out);
            using var connection = new IngresConnection(TestEnvironment.GetConnectionString("EFCore_Northwind", @"""dbo"""));
            try
            {
                var script = File.ReadAllText(scriptPath);
                connection.Execute(script, options => options.AddLogger(log));
                log.WriteLine("-- SCRIPT SUCCEEDED");
                log.WriteLine();
            }
            catch
            {
                log.WriteLine("-- SCRIPT FAILED");
                log.WriteLine();
                throw;
            }
        }

        internal static void PrintException(Exception ex, bool first = true)
        {
            //if (ex is null)
            //    return;

            //if (!first)
            //{
            //    Console.WriteLine(new string('-', 100));
            //}
            //Console.WriteLine(ex.Message);
            //Console.WriteLine(ex.StackTrace);

            //PrintException(ex.InnerException, false);
        }
    }
}
