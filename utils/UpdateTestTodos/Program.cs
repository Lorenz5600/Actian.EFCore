using UpdateTestTodos;

var paths = new Paths();
var testResults = TestResults.GetResults(paths);
var changeCount = 0;

foreach (var testFile in paths.GetTestFiles())
{
    UpdateActianTodoRewriter.Rewrite(testFile, testResults, ref changeCount);
}

