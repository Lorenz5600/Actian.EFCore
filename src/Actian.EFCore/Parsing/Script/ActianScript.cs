using System.Collections.Generic;

namespace Actian.EFCore.Parsing.Script
{
    public class ActianScript
    {
        public static void Execute(string path, IActianScriptRunner runner)
        {
            var script = new ActianScript(runner);
            foreach (var statement in ActianScriptScanner.ScanScriptFromFile(path))
            {
                script.Execute(statement);
            }
            script.ExecuteStatementCache();
        }

        public static void Execute(IEnumerable<ISqlStatement> statements, IActianScriptRunner runner)
        {
            var script = new ActianScript(runner);
            foreach (var statement in statements)
            {
                script.Execute(statement);
            }
            script.ExecuteStatementCache();
        }

        private ActianScript(IActianScriptRunner runner)
        {
            Runner = runner;
        }

        public List<SqlStatement> StatementCache { get; } = new List<SqlStatement>();
        public bool Continue { get; set; } = false;
        public IActianScriptRunner Runner { get; }

        public void Execute(ISqlStatement statement)
        {
            if (statement is SqlStatement sqlStatement)
                StatementCache.Add(sqlStatement);

            if (statement is SqlCommand command)
                ExecuteSqlCommand(command);
        }

        private void ExecuteSqlCommand(SqlCommand command)
        {
            switch (command.Command)
            {
                case "go":
                    ExecuteStatementCache();
                    break;
                case "continue":
                    Continue = true;
                    break;
                case "nocontinue":
                    Continue = false;
                    break;
            }
        }

        private void ExecuteStatementCache()
        {
            foreach (var sqlStatement in StatementCache)
            {
                Runner.Execute(sqlStatement, Continue);
            }
            StatementCache.Clear();
        }
    }
}
