using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingres.Client;

namespace Actian.EFCore.Build
{
    public class IngresSession : IDisposable
    {
        private readonly LogConsole _console;
        private readonly bool _quiet;
        private readonly IngresConnectionStringBuilder _connectionStringBuilder;

        public IngresSession(string connectionString, LogConsole console, bool quiet = false)
        {
            _connectionStringBuilder = new IngresConnectionStringBuilder(connectionString);
            _console = console;
            _quiet = quiet;
            if (!_quiet)
            {
                _console.WriteLine($"Execute SQL in database {Database}");
                _console.WriteLine();
            }
        }

        public string Database => _connectionStringBuilder.Database;
        public string ConnectionString => _connectionStringBuilder.ConnectionString;

        public IngresSession WithDbmsUser(string user)
        {
            if (_connection != null)
                throw new Exception("Already connected");

            _connectionStringBuilder.DbmsUser = user;

            return this;
        }

        private IngresConnection _connection;
        public IngresConnection Connection
        {
            get
            {
                if (_connection is null)
                {
                    _connection = new IngresConnection(ConnectionString);
                    _connection.Open();
                }
                return _connection;
            }
        }

        public async Task<int> ExecuteAsync(string sql, bool ignoreErrors = false)
        {
            _console.Indent();
            var connected = false;
            try
            {
                using var cmd = Connection.CreateCommand();
                cmd.CommandText = Text.Normalize(sql);
                connected = true;
                if (!_quiet) _console.WriteLine(cmd.CommandText);
                var rows = await cmd.ExecuteNonQueryAsync();
                if (!_quiet) _console.WriteLine(rows == -1 ? "ok" : $"{rows} rows");
                return rows;
            }
            catch (Exception ex) when (!connected)
            {
                if (!_quiet) _console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex) when (ignoreErrors)
            {
                if (!_quiet) _console.WriteLine(ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                if (!_quiet) _console.WriteLine(ex.Message, stderr: true);
                throw;
            }
            finally
            {
                if (!_quiet) _console.WriteLine();
                _console.Outdent();
            }
        }

        public async Task<List<T>> SelectAsync<T>(string sql, Func<IngresDataReader, T> getRow, bool ignoreErrors = false)
        {
            _console.Indent();
            try
            {
                using var cmd = Connection.CreateCommand();
                cmd.CommandText = Text.Normalize(sql);
                if (!_quiet) _console.WriteLine(cmd.CommandText);

                var rows = new List<T>();

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    rows.Add(getRow(reader));
                }
                if (!_quiet) _console.WriteLine($"{rows.Count} rows");

                return rows;
            }
            catch (Exception ex) when (ignoreErrors)
            {
                if (!_quiet) _console.WriteLine(ex.Message);
                return new List<T>();
            }
            catch (Exception ex)
            {
                if (!_quiet) _console.WriteLine(ex.Message, stderr: true);
                throw;
            }
            finally
            {
                if (!_quiet) _console.WriteLine();
                _console.Outdent();
            }
        }

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _connection?.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
