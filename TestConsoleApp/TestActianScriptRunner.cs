using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using Actian.EFCore.Parsing.Script;

namespace TestConsoleApp
{
    internal class TestActianScriptRunner : IActianScriptRunner, IDisposable
    {
        private readonly DbConnection Connection;
        private readonly DbCommand Command;
        private readonly TextWriter Log;
        private readonly int CommandTimeout;
        private readonly bool wasOpen;
        private bool first = true;

        public TestActianScriptRunner(DbConnection connection, TextWriter log, int commandTimeout = 300)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Log = log ?? throw new ArgumentNullException(nameof(log));
            CommandTimeout = commandTimeout;
            wasOpen = Connection.State == ConnectionState.Open;
            if (!wasOpen)
            {
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
                Connection.Open();
            }
            Command = connection.CreateCommand();
            Command.CommandTimeout = CommandTimeout;
        }

        public void Execute(SqlStatement statement, bool ignoreErrors)
        {
            if (first)
            {
                Log.WriteLine(new string('-', 120));
                Log.WriteLine();
            }
            first = false;

            Log.WriteLine(statement.Sql);
            try
            {
                if (statement.HasSql)
                {
                    Command.CommandText = statement.Sql;
                    Log.WriteLine();
                    var rows = Command.ExecuteNonQuery();
                    if (rows == -1)
                        Log.WriteLine($"-- OK");
                    else if (rows == 1)
                        Log.WriteLine($"-- 1 row");
                    else
                        Log.WriteLine($"-- {rows} rows");
                }
            }
            catch (Exception ex)
            {
                var lines = ex.Message.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                if (lines.Any())
                {
                    Log.WriteLine($"-- {lines.First()}");
                    foreach (var line in lines.Skip(1))
                    {
                        Log.WriteLine($"-- {line}");
                    }
                }
                else
                {
                    Log.WriteLine($"-- Error");
                }

                if (!ignoreErrors)
                    throw ex;
            }
            finally
            {
                Log.WriteLine();
                Log.WriteLine(new string('-', 120));
                Log.WriteLine();
                Log.Flush();
            }
        }

        public void Dispose()
        {
            Command.Dispose();
            if (!wasOpen && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
