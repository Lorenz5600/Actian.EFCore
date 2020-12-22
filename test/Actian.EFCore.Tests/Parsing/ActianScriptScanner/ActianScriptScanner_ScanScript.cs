using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Actian.EFCore.Parsing.Script;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Tests.Parsing.ActianScriptScanner
{
    public class ActianScriptScanner_ScanScript
    {
        private readonly ITestOutputHelper _output;

        public ActianScriptScanner_ScanScript(ITestOutputHelper output)
        {
            _output = output;
        }

        private static readonly Encoding UTF8 = new UTF8Encoding(true);
        private bool SaveActualAsExpected = false;

        [Theory]
        [InlineData("single")]
        [InlineData("multiple")]
        [InlineData("multiple-with-commands")]
        [InlineData("procedure")]
        [InlineData("procedure2")]
        [InlineData("with-include")]
        public void TestCase(string test)
        {
            var testCaseDir = GetTestCaseDir(test);
            Directory.CreateDirectory(testCaseDir);
            var scriptPath = Path.Combine(testCaseDir, "script.sql");
            var actualPath = Path.Combine(testCaseDir, "actual.sql");
            var expectedPath = Path.Combine(testCaseDir, "expected.sql");
            var diffPath = Path.Combine(testCaseDir, "diff.sql");

            if (!File.Exists(scriptPath))
                File.WriteAllText(scriptPath, "-- This should contain an SQL script to be scanned", UTF8);

            if (!File.Exists(expectedPath))
                File.WriteAllText(expectedPath, "-- This should contain the scanned scanned SQL script", UTF8);

            using (var runner = new TestActianScriptRunner(actualPath))
            {
                ActianScript.Execute(scriptPath, runner);
            }

            var actual = File.ReadAllText(actualPath, UTF8);

            if (SaveActualAsExpected)
            {
                File.WriteAllText(expectedPath, actual, UTF8);
            }
            var expected = File.ReadAllText(expectedPath, UTF8);

            var diff = InlineDiffBuilder.Diff(expected, actual);

            File.WriteAllText(diffPath, FullDiff(diff), UTF8);

            diff.Lines.Any(line => line.Type != ChangeType.Unchanged).Should().Be(false, FullDiff(diff));
        }

        private static string FullDiff(DiffPaneModel diff)
        {
            var text = new StringBuilder();
            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        text.Append("+ ");
                        break;
                    case ChangeType.Deleted:
                        text.Append("- ");
                        break;
                    default:
                        text.Append("  ");
                        break;
                }

                text.AppendLine(line.Text);
            }
            return text.ToString();
        }

        private static string GetTestCaseDir(string testCase, [CallerFilePath] string callerPath = "")
        {
            return Path.Combine(Path.GetDirectoryName(callerPath), "TestCases", testCase);
        }
    }
}
