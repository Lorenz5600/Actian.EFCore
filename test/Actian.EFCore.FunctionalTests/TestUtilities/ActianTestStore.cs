using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Actian.EFCore.Parsing.Script;
using Actian.EFCore.Storage.Internal;
using Ingres.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

#pragma warning disable IDE0022 // Use block body for methods
// ReSharper disable SuggestBaseTypeForParameter
namespace Actian.EFCore.TestUtilities
{
    public class ActianTestStore : RelationalTestStore
    {
        private static readonly ActianSqlGenerationHelper SqlGenerationHelper = new ActianSqlGenerationHelper(new RelationalSqlGenerationHelperDependencies());

        public const int CommandTimeout = 300;

        public static ActianTestStore GetIIDbDb()
            => new ActianTestStore("iidbdb", TestEnvironment.LoginUser);

        public static ActianTestStore GetNorthwindStore()
            => (ActianTestStore)new ActianNorthwindTestStoreFactory()
                .GetOrCreate(ActianNorthwindTestStoreFactory.DatabaseName).Initialize(null, (Func<DbContext>)null);

        public static ActianTestStore GetOrCreate(string name, string dbmsUser)
            => new ActianTestStore(name, dbmsUser);

        public static ActianTestStore GetOrCreateInitialized(string name, string dbmsUser)
            => new ActianTestStore(name, dbmsUser).InitializeActian(null, (Func<DbContext>)null, null);

        public static ActianTestStore GetOrCreate(string name, string dbmsUser, string scriptPath)
            => new ActianTestStore(name, dbmsUser, scriptPath: scriptPath);

        public static ActianTestStore Create(string name, string dbmsUser)
            => new ActianTestStore(name, dbmsUser, shared: false);

        public static ActianTestStore CreateInitialized(string name, string dbmsUser)
            => new ActianTestStore(name, dbmsUser, shared: false)
                .InitializeActian(null, (Func<DbContext>)null, null);

        private readonly string _scriptPath;

        private ActianTestStore(
            string name,
            string dbmsUser,
            string scriptPath = null,
            bool shared = true)
            : base(name, shared)
        {
            Console.WriteLine($"Creating ActianTestStore {name}");
            DbmsUser = dbmsUser;
            if (scriptPath != null)
            {
                _scriptPath = Path.Combine(Path.GetDirectoryName(typeof(ActianTestStore).GetTypeInfo().Assembly.Location), scriptPath);
            }

            ConnectionString = TestEnvironment.GetConnectionString(Name, DbmsUser);
            Connection = new IngresConnection(ConnectionString);

            Console.WriteLine($"Server version: {TestEnvironment.ActianServerVersion}");
        }

        public ITestOutputHelper Output { get; private set; }
        public string DbmsUser { get; }

        public void SetOutput(ITestOutputHelper output)
        {
            Output = output;
        }

        public ActianTestStore InitializeActian(
            IServiceProvider serviceProvider, Func<DbContext> createContext, Action<DbContext> seed)
            => (ActianTestStore)Initialize(serviceProvider, createContext, seed);

        public ActianTestStore InitializeActian(
            IServiceProvider serviceProvider, Func<ActianTestStore, DbContext> createContext, Action<DbContext> seed)
            => InitializeActian(serviceProvider, () => createContext(this), seed);

        public IngresConnection GetIIDbDbConnection()
        {
            return new IngresConnection(new IngresConnectionStringBuilder(Connection.ConnectionString) { Database = "iidbdb" }.ConnectionString);
        }

        private static readonly HashSet<(Type, string)> _scriptExecuted = new HashSet<(Type, string)>();
        protected override void Initialize(Func<DbContext> createContext, Action<DbContext> seed, Action<DbContext> clean)
        {
            if (CreateDatabase(clean))
            {
                if (_scriptPath != null)
                {
                    _scriptExecuted.Add((GetType(), _scriptPath));
                    ExecuteScript(_scriptPath);
                }
                else
                {
                    using var context = createContext();
                    context.Database.EnsureCreatedResiliently();
                    seed?.Invoke(context);
                }
            }
            else if (_scriptPath != null && !_scriptExecuted.Contains((GetType(), _scriptPath)))
            {
                _scriptExecuted.Add((GetType(), _scriptPath));
                ExecuteScript(_scriptPath);
            }
        }

        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
            => builder.UseActian(Connection, b => b.ApplyConfiguration());

        private bool CreateDatabase(Action<DbContext> clean)
        {
            if (!DatabaseExists())
                throw new NotSupportedException($"Can not create Actian database {Connection.Database}");

            if (_scriptPath != null)
                return false;

            using var context = new DbContext(
                AddProviderOptions(
                    new DbContextOptionsBuilder().EnableServiceProviderCaching(false)
                ).Options
            );
            clean?.Invoke(context);
            Clean(context);
            return true;
        }

        public void DeleteDatabase()
        {
            throw new NotSupportedException("Can not delete Actian database {Name}");
        }

        private bool DatabaseExists()
        {
            using var iiDbDbConnection = GetIIDbDbConnection();
            return ExecuteScalar<int>(iiDbDbConnection, @$"
                select count(*)
                  from $ingres.iidatabase_info
                 where database_name = '{Connection.Database}'
            ") == 1;
        }

        //private void Clean(string name)
        //{
        //    var options = new DbContextOptionsBuilder()
        //        .UseActian(CreateConnectionString(name), b => b.ApplyConfiguration())
        //        .UseInternalServiceProvider(
        //            new ServiceCollection()
        //                .AddEntityFrameworkActian()
        //                .BuildServiceProvider())
        //        .Options;

        //    using var context = new DbContext(options);
        //    context.Database.EnsureClean();
        //}

        public void Clean()
        {
            var options = new DbContextOptionsBuilder()
                .UseActian(ConnectionString, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(
                    new ServiceCollection()
                        .AddEntityFrameworkActian()
                        .BuildServiceProvider())
                .Options;

            using var context = new DbContext(options);
            context.Database.EnsureClean();
        }

        public override void Clean(DbContext context)
            => context.Database.EnsureClean();

        public void CleanObjects()
        {
            Output?.WriteLine(new string('=', 80));
            Output?.WriteLine("Cleaning database objects");
            Output?.WriteLine(new string('-', 80));

            var statements = new List<string>();

            ForEach($@"
                select table_owner,
                       table_name
                  from $ingres.iitables
                 where table_type in ('T', 'V')
                   and system_use = 'U'
            ", reader =>
            {
                var schema = reader.GetFieldValue<string>(0)?.Trim();
                var name = reader.GetFieldValue<string>(1)?.Trim();

                statements.Add($@"SET SESSION AUTHORIZATION ""{schema}"";");
                statements.Add($@"DROP ""{name}"";");
            });


            ForEach($@"
                select seq_owner,
                       seq_name
                  from $ingres.iisequences
            ", reader =>
            {
                var schema = reader.GetFieldValue<string>(0)?.Trim();
                var name = reader.GetFieldValue<string>(1)?.Trim();

                statements.Add($@"SET SESSION AUTHORIZATION ""{schema}"";");
                statements.Add($@"DROP SEQUENCE ""{name}"";");
            });

            statements.Add($"SET SESSION AUTHORIZATION initial_user;");

            ExecuteStatementsIgnoreErrors(statements);

            Output?.WriteLine(new string('=', 80));
            Output?.WriteLine("");
            Output?.WriteLine("");
        }

        public void ExecuteScript(string scriptPath)
        {
            var filename = Path.GetFileNameWithoutExtension(scriptPath);
            var extension = Path.GetExtension(scriptPath);
            var logfile = Path.Combine(TestEnvironment.LogDirectory, $"{filename}.log{extension}");
            using var log = new StreamWriter(logfile, false, Encoding.UTF8) { NewLine = "\n" };
            using var runner = new TestActianScriptRunner(Connection, log);
            try
            {
                ActianScript.Execute(scriptPath, runner);
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

        public override void OpenConnection()
            => new TestActianRetryingExecutionStrategy(Name, DbmsUser).Execute(Connection, connection => connection.Open());

        public override Task OpenConnectionAsync()
            => new TestActianRetryingExecutionStrategy(Name, DbmsUser).ExecuteAsync(Connection, connection => connection.OpenAsync());

        public T ExecuteScalar<T>(string sql, params object[] parameters)
            => ExecuteScalar<T>(Connection, sql, parameters);

        private T ExecuteScalar<T>(DbConnection connection, string sql, params object[] parameters)
            => Execute(connection, command => command.ExecuteScalar<T>(), sql, false, parameters);

        public Task<T> ExecuteScalarAsync<T>(string sql, params object[] parameters)
            => ExecuteScalarAsync<T>(Connection, sql, parameters);

        private Task<T> ExecuteScalarAsync<T>(DbConnection connection, string sql, IReadOnlyList<object> parameters = null)
            => ExecuteAsync(connection, async command => await command.ExecuteScalarAsync<T>(), sql, false, parameters);

        public int ExecuteNonQuery(string sql, params object[] parameters)
            => ExecuteNonQuery(Connection, sql, parameters);

        public int ExecuteNonQuery(IEnumerable<string> statements, bool ignoreErrors = false)
        {
            Connection.Open();
            try
            {
                var rows = 0;
                if (statements != null)
                {
                    foreach (var statement in statements.Where(s => !string.IsNullOrWhiteSpace(s)))
                    {
                        try
                        {
                            rows += ExecuteNonQuery(Connection, statement);
                        }
                        catch when (ignoreErrors)
                        {
                            // Ignore
                        }
                    }
                }
                return rows;
            }
            finally
            {
                Connection.Close();
            }
        }

        public string GetServerVersion()
        {
            Connection.Open();
            try
            {
                return Connection.ServerVersion;
            }
            finally
            {
                Connection.Close();
            }
        }

        private static readonly Regex SqlStatementTerminatorRe = new Regex(@";\s*$|\\g", RegexOptions.Multiline);
        private static readonly Regex SqlCommentRe = new Regex(@"^\s*--");
        private static IEnumerable<string> SplitSqlStatements(IEnumerable<string> statements)
        {
            var sql = string.Join("\n", statements ?? Enumerable.Empty<string>())
                .Replace("\r", "")
                .Split('\n')
                .Where(line => !SqlCommentRe.IsMatch(line));
            return SqlStatementTerminatorRe
                .Split(string.Join('\n', sql))
                .Select(statement => statement.Trim())
                .Where(statement => !string.IsNullOrWhiteSpace(statement));
        }

        public int ExecuteStatements(params string[] statements)
        {
            return ExecuteStatements(statements.AsEnumerable());
        }

        public int ExecuteStatements(IEnumerable<string> statements)
        {
            return ExecuteNonQuery(SplitSqlStatements(statements), false);
        }

        public int ExecuteStatementsIgnoreErrors(params string[] statements)
        {
            return ExecuteStatementsIgnoreErrors(statements.AsEnumerable());
        }

        public int ExecuteStatementsIgnoreErrors(IEnumerable<string> statements)
        {
            return ExecuteNonQuery(SplitSqlStatements(statements), true);
        }

        private int ExecuteNonQuery(DbConnection connection, string sql, object[] parameters = null)
            => Execute(connection, command => Log(sql, () => command.ExecuteNonQuery()), sql, false, parameters);

        public Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters)
            => ExecuteNonQueryAsync(Connection, sql, parameters);

        private Task<int> ExecuteNonQueryAsync(DbConnection connection, string sql, IReadOnlyList<object> parameters = null)
            => ExecuteAsync(connection, command => LogAsync(sql, () => command.ExecuteNonQueryAsync()), sql, false, parameters);

        public IEnumerable<T> Query<T>(string sql, params object[] parameters)
            => Query<T>(Connection, sql, parameters);

        public IEnumerable<T> Query<T>(string sql, Func<DbDataReader, T> read, params object[] parameters)
            => Query(Connection, sql, read, parameters);

        private IEnumerable<T> Query<T>(DbConnection connection, string sql, Func<DbDataReader, T> read, object[] parameters = null)
            => Execute(
                connection, command =>
                {
                    using var dataReader = Log(sql, () => command.ExecuteReader());
                    var results = new List<T>();
                    while (dataReader.Read())
                    {
                        results.Add(read(dataReader));
                    }
                    return results;
                }, sql, false, parameters);

        private IEnumerable<T> Query<T>(DbConnection connection, string sql, object[] parameters = null)
            => Execute(
                connection, command =>
                {
                    using var dataReader = Log(sql, () => command.ExecuteReader());
                    var results = Enumerable.Empty<T>();
                    while (dataReader.Read())
                    {
                        results = results.Concat(new[] { dataReader.GetFieldValue<T>(0) });
                    }
                    return results;
                }, sql, false, parameters);

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, params object[] parameters)
            => QueryAsync<T>(Connection, sql, parameters);

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, Func<DbDataReader, T> read, params object[] parameters)
            => QueryAsync<T>(Connection, sql, read, parameters);

        private Task<IEnumerable<T>> QueryAsync<T>(DbConnection connection, string sql, Func<DbDataReader, T> read, object[] parameters = null)
            => ExecuteAsync(
                connection, async command =>
                {
                    using var dataReader = await LogAsync(sql, () => command.ExecuteReaderAsync());

                    var results = new List<T>();
                    while (await dataReader.ReadAsync())
                    {
                        results.Add(read(dataReader));
                    }
                    return results.AsEnumerable();
                }, sql, false, parameters);

        private Task<IEnumerable<T>> QueryAsync<T>(DbConnection connection, string sql, object[] parameters = null)
            => ExecuteAsync(
                connection, async command =>
                {
                    using var dataReader = await LogAsync(sql, () => command.ExecuteReaderAsync());

                    var results = Enumerable.Empty<T>();
                    while (await dataReader.ReadAsync())
                    {
                        results = results.Concat(new[] { await dataReader.GetFieldValueAsync<T>(0) });
                    }

                    return results;
                }, sql, false, parameters);

        public int ForEach(string sql, Action<DbDataReader> action, params object[] parameters)
            => ForEach(Connection, sql, action, parameters);

        private int ForEach(DbConnection connection, string sql, Action<DbDataReader> action, object[] parameters = null)
            => Execute(
                connection, command =>
                {
                    using var dataReader = Log(sql, () => command.ExecuteReader());
                    var rows = 0;
                    while (dataReader.Read())
                    {
                        action(dataReader);
                        rows += 1;
                    }
                    return rows;
                }, sql, false, parameters);

        private T Execute<T>(
            DbConnection connection, Func<DbCommand, T> execute, string sql,
            bool useTransaction = false, object[] parameters = null)
            => new TestActianRetryingExecutionStrategy(Name, DbmsUser).Execute(
                new
                {
                    connection,
                    execute,
                    sql,
                    useTransaction,
                    parameters
                },
                state => ExecuteCommand(state.connection, state.execute, state.sql, state.useTransaction, state.parameters));

        private T ExecuteCommand<T>(
            DbConnection connection, Func<DbCommand, T> execute, string sql, bool useTransaction, object[] parameters)
        {
            var wasOpen = connection.State == ConnectionState.Open;
            if (!wasOpen)
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                connection.Open();
            }
            try
            {
                using var transaction = useTransaction ? connection.BeginTransaction() : null;
                using var command = CreateCommand(connection, sql, parameters);
                command.Transaction = transaction;
                var result = execute(command);
                transaction?.Commit();
                return result;
            }
            finally
            {
                if (!wasOpen && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        private Task<T> ExecuteAsync<T>(
            DbConnection connection, Func<DbCommand, Task<T>> executeAsync, string sql,
            bool useTransaction = false, IReadOnlyList<object> parameters = null)
            => new TestActianRetryingExecutionStrategy(Name, DbmsUser).ExecuteAsync(
                new
                {
                    connection,
                    executeAsync,
                    sql,
                    useTransaction,
                    parameters
                },
                state => ExecuteCommandAsync(state.connection, state.executeAsync, state.sql, state.useTransaction, state.parameters));

        private async Task<T> ExecuteCommandAsync<T>(
            DbConnection connection, Func<DbCommand, Task<T>> executeAsync, string sql, bool useTransaction,
            IReadOnlyList<object> parameters)
        {
            if (connection.State != ConnectionState.Closed)
            {
                await connection.CloseAsync();
            }

            await connection.OpenAsync();
            try
            {
                using (var transaction = useTransaction ? await connection.BeginTransactionAsync() : null)
                {
                    T result;
                    using (var command = CreateCommand(connection, sql, parameters))
                    {
                        result = await LogAsync(sql, () => executeAsync(command));
                    }

                    if (transaction != null)
                    {
                        await transaction.CommitAsync();
                    }

                    return result;
                }
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    await connection.CloseAsync();
                }
            }
        }

        private T Log<T>(string sql, Func<T> action)
        {
            try
            {
                Output?.WriteLine(sql);
                Output?.WriteLine("");
                return action();
            }
            catch (Exception ex)
            {
                Output?.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
            finally
            {
                Output?.WriteLine("");
            }
        }

        private async Task<T> LogAsync<T>(string sql, Func<Task<T>> action)
        {
            try
            {
                Output?.WriteLine(sql);
                Output?.WriteLine("");
                return await action();
            }
            catch (Exception ex)
            {
                Output?.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
            finally
            {
                Output?.WriteLine("");
            }
        }

        private static DbCommand CreateCommand(
            DbConnection connection, string commandText, IReadOnlyList<object> parameters = null)
        {
            var command = (IngresCommand)connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandTimeout = CommandTimeout;

            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    command.Parameters.AddWithValue("p" + i, parameters[i]);
                }
            }

            return command;
        }
    }
}
