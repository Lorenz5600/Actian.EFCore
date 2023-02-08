using System.Data.Common;
using Ingres.Client;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianConnection : RelationalConnection, IActianConnection
    {
        // Compensate for slow Actian database creation (when and if database creation is implemented)
        private const int DefaultIIDbDbConnectionCommandTimeout = 60 * 5;

        public ActianConnection([NotNull] RelationalConnectionDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <inheritdoc />
        protected override DbConnection CreateDbConnection() => new IngresConnection(ConnectionString);

        /// <inheritdoc />
        public virtual IActianConnection CreateIIDbDbConnection()
        {
            var connectionString = new IngresConnectionStringBuilder(ConnectionString)
            {
                Database = "iidbdb",
                Pooling = false
            }.ToString();

            return new ActianConnection(Dependencies.With(new DbContextOptionsBuilder()
                .UseActian(
                    connectionString,
                    b => b.CommandTimeout(CommandTimeout ?? DefaultIIDbDbConnectionCommandTimeout)
                ).Options
            ));
        }

        /// <inheritdoc />
        public override bool IsMultipleActiveResultSetsEnabled => false;

        /// <inheritdoc />
        protected override bool SupportsAmbientTransactions => true;

        public override IDbContextTransaction UseTransaction(DbTransaction transaction)
        {
            return base.UseTransaction(transaction);
        }
    }
}
