using System.IO;
using System.Runtime.CompilerServices;

namespace TestConsoleApp
{
    public static class Helper
    {
        public static string GetSolutionRoot([CallerFilePath] string callerFilePath = null)
        {
            return Path.GetDirectoryName(Path.GetDirectoryName(callerFilePath));
        }

        public static string Read(string dir, string filename)
        {
            return File.ReadAllText(Path.Combine(dir, filename));
        }
    }
}
