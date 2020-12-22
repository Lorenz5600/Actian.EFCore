using System;
using System.IO;
using System.Text;
using Actian.EFCore.Parsing.Script;

namespace Actian.EFCore.Tests.Parsing.ActianScriptScanner
{
    public class TestActianScriptRunner : IActianScriptRunner, IDisposable
    {
        public TestActianScriptRunner(string path)
        {
            actualFile = new StreamWriter(path, false, Encoding.UTF8) { NewLine = "\n" };
        }

        private readonly TextWriter actualFile;

        public void Execute(SqlStatement statement, bool ignoreErrors)
        {
            actualFile.WriteLine(new string('-', 120));
            actualFile.WriteLine($"-- Ignore errors: {ignoreErrors}");
            actualFile.WriteLine(new string('-', 120));
            actualFile.WriteLine();
            actualFile.WriteLine(statement);
            actualFile.WriteLine();
        }

        public void Dispose()
        {
            actualFile.Dispose();
        }
    }
}
