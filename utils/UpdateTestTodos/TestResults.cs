using System.Collections.Generic;
using System.Linq;
using Actian.TestLoggers;

namespace UpdateTestTodos
{
    internal static class TestResults
    {
        public static IEnumerable<ActianTestResult> GetResults(Paths paths)
        {
            return Actian.TestLoggers.Program
                .ReadTestProjects(paths.ActianEFCoreFunctionalTestResults)
                .SelectMany(project => project.Results)
                .ToList();
        }
    }
}
