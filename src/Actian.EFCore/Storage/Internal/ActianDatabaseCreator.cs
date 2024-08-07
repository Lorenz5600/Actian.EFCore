using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianDatabaseCreator : RelationalDatabaseCreator
    {
        public ActianDatabaseCreator(
            [NotNull] RelationalDatabaseCreatorDependencies dependencies,
            [NotNull] IActianConnection connection,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder)
            : base(dependencies)
        {
            Connection = connection;
            IIDbDbConnection = Connection.CreateIIDbDbConnection();
            RawSqlCommandBuilder = rawSqlCommandBuilder;
        }

        public virtual TimeSpan RetryDelay { get; set; } = TimeSpan.FromMilliseconds(500);
        public virtual TimeSpan RetryTimeout { get; set; } = TimeSpan.FromMinutes(1);

        public IActianConnection Connection { get; }
        public IActianConnection IIDbDbConnection { get; }
        public IRawSqlCommandBuilder RawSqlCommandBuilder { get; }

        private RelationalCommandParameterObject CommandParameters => new RelationalCommandParameterObject(
            Connection,
            null,
            null,
            Dependencies.CurrentContext.Context,
            Dependencies.CommandLogger
        );

        private RelationalCommandParameterObject IIDbDbCommandParameters => new RelationalCommandParameterObject(
            IIDbDbConnection,
            null,
            null,
            Dependencies.CurrentContext.Context,
            Dependencies.CommandLogger
        );

        private string DatabaseExistsSql => @$"
            select 1
              from $ingres.iidatabase_info
             where database_name = 'iidbdb'
        ";

        /// <inheritdoc />
        public override bool Exists()
        {
            return RawSqlCommandBuilder
                .Build(DatabaseExistsSql)
                .ExecuteScalar<int>(IIDbDbCommandParameters) == 1;
        }

        /// <inheritdoc />
        public override async Task<bool> ExistsAsync(CancellationToken cancellationToken = default)
        {
            return await RawSqlCommandBuilder
                .Build(DatabaseExistsSql)
                .ExecuteScalarAsync<int>(IIDbDbCommandParameters, cancellationToken) == 1;
        }

        /// <inheritdoc />
        public override void Create()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override void Delete()
        {
            throw new NotSupportedException();
        }

        private const string HasTablesSql = @"
            select distinct 1
              from $ingres.iitables
             where table_type = 'T'
               and system_use = 'U'
        ";

        /// <inheritdoc />
        public override bool HasTables()
        {
            return RawSqlCommandBuilder
                .Build(HasTablesSql)
                .ExecuteScalar<int?>(CommandParameters) == 1;
        }

        /// <inheritdoc />
        public override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default)
        {
            return await RawSqlCommandBuilder
                .Build(HasTablesSql)
                .ExecuteScalarAsync<int>(CommandParameters) == 1;
        }
    }
}
