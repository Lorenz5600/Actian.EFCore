using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ingres.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0022 // Use block body for methods
// ReSharper disable SuggestBaseTypeForParameter
namespace Actian.EFCore.TestUtilities
{
    public class ActianTestStore : RelationalTestStore
    {
        public const int CommandTimeout = 300;

        public static ActianTestStore GetIIDbDb()
            => new ActianTestStore("iidbdb");

        public static ActianTestStore GetNorthwindStore()
            => (ActianTestStore)ActianNorthwindTestStoreFactory.Instance
                .GetOrCreate(ActianNorthwindTestStoreFactory.Name).Initialize(null, (Func<DbContext>)null);

        public static ActianTestStore GetOrCreate(string name)
            => new ActianTestStore(name);

        public static ActianTestStore GetOrCreateInitialized(string name)
            => new ActianTestStore(name).InitializeActian(null, (Func<DbContext>)null, null);

        public static ActianTestStore GetOrCreate(string name, string scriptPath)
            => new ActianTestStore(name, scriptPath: scriptPath);

        public static ActianTestStore Create(string name)
            => new ActianTestStore(name, shared: false);

        public static ActianTestStore CreateInitialized(string name)
            => new ActianTestStore(name, shared: false)
                .InitializeActian(null, (Func<DbContext>)null, null);

        private readonly string _scriptPath;

        private ActianTestStore(
            string name,
            string scriptPath = null,
            bool shared = true)
            : base(name, shared)
        {
            Console.WriteLine($"Creating ActianTestStore {name}");
            if (scriptPath != null)
            {
                _scriptPath = Path.Combine(Path.GetDirectoryName(typeof(ActianTestStore).GetTypeInfo().Assembly.Location), scriptPath);
            }

            ConnectionString = CreateConnectionString(Name);
            Connection = new IngresConnection(ConnectionString);

            Console.WriteLine($"Server version: {GetServerVersion()}");
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

        protected override void Initialize(Func<DbContext> createContext, Action<DbContext> seed, Action<DbContext> clean)
        {
            if (CreateDatabase(clean))
            {
                if (_scriptPath != null)
                {
                    ExecuteScript(_scriptPath);
                }
                else
                {
                    using var context = createContext();
                    context.Database.EnsureCreatedResiliently();
                    seed?.Invoke(context);
                }
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
            var statements = new List<string>();

            var tables = Query($@"
                select table_owner,
                       table_name
                  from $ingres.iitables
                 where table_type in ('T', 'V')
                   and system_use = 'U'
            ", reader =>
            {
                return new
                {
                    Schema = reader.GetFieldValue<string>(0),
                    Name = reader.GetFieldValue<string>(1)
                };
            });

            foreach (var table in tables)
            {
                statements.Add($"SET SESSION AUTHORIZATION {table.Schema};");
                statements.Add($"DROP {table.Name};");
            }

            var sequences = Query($@"
                select seq_owner,
                       seq_name
                  from $ingres.iisequences
            ", reader =>
            {
                return new
                {
                    Schema = reader.GetFieldValue<string>(0),
                    Name = reader.GetFieldValue<string>(1)
                };
            });

            foreach (var sequence in sequences)
            {
                statements.Add($"SET SESSION AUTHORIZATION {sequence.Schema};");
                statements.Add($"DROP SEQUENCE {sequence.Name};");
            }

            ExecuteStatementsIgnoreErrors(statements);
        }

        public void ExecuteScript(string scriptPath)
        {
            var script = File.ReadAllText(scriptPath);
            Execute(
                Connection, command =>
                {
                    foreach (var batch in
                        new Regex(@"^\\g", RegexOptions.IgnoreCase | RegexOptions.Multiline, TimeSpan.FromMilliseconds(1000.0))
                            .Split(script).Where(b => !string.IsNullOrEmpty(b)))
                    {
                        command.CommandText = batch;
                        command.ExecuteNonQuery();
                    }

                    return 0;
                }, "");
        }

        public override void OpenConnection()
            => new TestActianRetryingExecutionStrategy().Execute(Connection, connection => connection.Open());

        public override Task OpenConnectionAsync()
            => new TestActianRetryingExecutionStrategy().ExecuteAsync(Connection, connection => connection.OpenAsync());

        public T ExecuteScalar<T>(string sql, params object[] parameters)
            => ExecuteScalar<T>(Connection, sql, parameters);

        private static T ExecuteScalar<T>(DbConnection connection, string sql, params object[] parameters)
            => Execute(connection, command => command.ExecuteScalar<T>(), sql, false, parameters);

        public Task<T> ExecuteScalarAsync<T>(string sql, params object[] parameters)
            => ExecuteScalarAsync<T>(Connection, sql, parameters);

        private static Task<T> ExecuteScalarAsync<T>(DbConnection connection, string sql, IReadOnlyList<object> parameters = null)
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

        private static int ExecuteNonQuery(DbConnection connection, string sql, object[] parameters = null)
            => Execute(connection, command => command.ExecuteNonQuery(), sql, false, parameters);

        public Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters)
            => ExecuteNonQueryAsync(Connection, sql, parameters);

        private static Task<int> ExecuteNonQueryAsync(DbConnection connection, string sql, IReadOnlyList<object> parameters = null)
            => ExecuteAsync(connection, command => command.ExecuteNonQueryAsync(), sql, false, parameters);

        public IEnumerable<T> Query<T>(string sql, params object[] parameters)
            => Query<T>(Connection, sql, parameters);

        public IEnumerable<T> Query<T>(string sql, Func<DbDataReader, T> read, params object[] parameters)
            => Query<T>(Connection, sql, read, parameters);

        private static IEnumerable<T> Query<T>(DbConnection connection, string sql, Func<DbDataReader, T> read, object[] parameters = null)
            => Execute(
                connection, command =>
                {
                    using var dataReader = command.ExecuteReader();
                    var results = new List<T>();
                    while (dataReader.Read())
                    {
                        results.Add(read(dataReader));
                    }
                    return results;
                }, sql, false, parameters);

        private static IEnumerable<T> Query<T>(DbConnection connection, string sql, object[] parameters = null)
            => Execute(
                connection, command =>
                {
                    using var dataReader = command.ExecuteReader();
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

        private static Task<IEnumerable<T>> QueryAsync<T>(DbConnection connection, string sql, Func<DbDataReader, T> read, object[] parameters = null)
            => ExecuteAsync(
                connection, async command =>
                {
                    using var dataReader = await command.ExecuteReaderAsync();
                    var results = new List<T>();
                    while (await dataReader.ReadAsync())
                    {
                        results.Add(read(dataReader));
                    }
                    return results.AsEnumerable();
                }, sql, false, parameters);

        private static Task<IEnumerable<T>> QueryAsync<T>(DbConnection connection, string sql, object[] parameters = null)
            => ExecuteAsync(
                connection, async command =>
                {
                    using var dataReader = await command.ExecuteReaderAsync();

                    var results = Enumerable.Empty<T>();
                    while (await dataReader.ReadAsync())
                    {
                        results = results.Concat(new[] { await dataReader.GetFieldValueAsync<T>(0) });
                    }

                    return results;
                }, sql, false, parameters);

        private static T Execute<T>(
            DbConnection connection, Func<DbCommand, T> execute, string sql,
            bool useTransaction = false, object[] parameters = null)
            => new TestActianRetryingExecutionStrategy().Execute(
                new
                {
                    connection,
                    execute,
                    sql,
                    useTransaction,
                    parameters
                },
                state => ExecuteCommand(state.connection, state.execute, state.sql, state.useTransaction, state.parameters));

        private static T ExecuteCommand<T>(
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

        private static Task<T> ExecuteAsync<T>(
            DbConnection connection, Func<DbCommand, Task<T>> executeAsync, string sql,
            bool useTransaction = false, IReadOnlyList<object> parameters = null)
            => new TestActianRetryingExecutionStrategy().ExecuteAsync(
                new
                {
                    connection,
                    executeAsync,
                    sql,
                    useTransaction,
                    parameters
                },
                state => ExecuteCommandAsync(state.connection, state.executeAsync, state.sql, state.useTransaction, state.parameters));

        private static async Task<T> ExecuteCommandAsync<T>(
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
                        result = await executeAsync(command);
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

        private static readonly HashSet<string> ValidDatabases = new HashSet<string>(new[]
        {
            TestEnvironment.Database,
            "iidbdb",
            "EFCore_DatabaseModelFactory"
        }, StringComparer.InvariantCultureIgnoreCase);

        public static string CreateConnectionString(string name)
        {
            if (!ValidDatabases.Contains(name))
                throw new Exception($"Database {name} can not be used for tests");

            return new IngresConnectionStringBuilder(TestEnvironment.ConnectionString)
            {
                Database = name.ToLowerInvariant()
            }.ConnectionString;
        }
    }
}
