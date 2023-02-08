using System.Threading.Tasks;
using Actian.EFCore.Infrastructure;
using Actian.EFCore.Storage.Internal;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class TransactionActianTest : TransactionTestBase<TransactionActianTest.TransactionActianFixture>
    {
        public TransactionActianTest(TransactionActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void SaveChanges_can_be_used_with_no_transaction()
        {
            base.SaveChanges_can_be_used_with_no_transaction();
        }

        [ActianTodo]
        public override Task SaveChangesAsync_can_be_used_with_no_transaction()
        {
            return base.SaveChangesAsync_can_be_used_with_no_transaction();
        }

        [ActianTodo]
        public override void SaveChanges_implicitly_starts_transaction()
        {
            base.SaveChanges_implicitly_starts_transaction();
        }

        [ActianTodo]
        public override Task SaveChangesAsync_implicitly_starts_transaction()
        {
            return base.SaveChangesAsync_implicitly_starts_transaction();
        }

        [ActianTodo]
        public override Task SaveChanges_uses_enlisted_transaction(bool async, bool autoTransactionsEnabled)
        {
            return base.SaveChanges_uses_enlisted_transaction(async, autoTransactionsEnabled);
        }

        [ActianTodo]
        public override Task SaveChanges_uses_enlisted_transaction_after_connection_closed(bool async, bool autoTransactionsEnabled)
        {
            return base.SaveChanges_uses_enlisted_transaction_after_connection_closed(async, autoTransactionsEnabled);
        }

        [ActianTodo]
        public override Task SaveChanges_uses_enlisted_transaction_connectionString(bool async, bool autoTransactionsEnabled)
        {
            return base.SaveChanges_uses_enlisted_transaction_connectionString(async, autoTransactionsEnabled);
        }

        [ActianTodo]
        public override Task SaveChanges_uses_ambient_transaction(bool async, bool autoTransactionsEnabled)
        {
            return base.SaveChanges_uses_ambient_transaction(async, autoTransactionsEnabled);
        }

        [ActianTodo]
        public override Task SaveChanges_uses_ambient_transaction_with_connectionString(bool async, bool autoTransactionsEnabled)
        {
            return base.SaveChanges_uses_ambient_transaction_with_connectionString(async, autoTransactionsEnabled);
        }

        [ActianTodo]
        public override void SaveChanges_throws_for_suppressed_ambient_transactions(bool connectionString)
        {
            base.SaveChanges_throws_for_suppressed_ambient_transactions(connectionString);
        }

        [ActianTodo]
        public override void SaveChanges_uses_enlisted_transaction_after_ambient_transaction()
        {
            base.SaveChanges_uses_enlisted_transaction_after_ambient_transaction();
        }

        [ActianTodo]
        public override void SaveChanges_does_not_close_connection_opened_by_user()
        {
            base.SaveChanges_does_not_close_connection_opened_by_user();
        }

        [ActianTodo]
        public override Task SaveChangesAsync_does_not_close_connection_opened_by_user()
        {
            return base.SaveChangesAsync_does_not_close_connection_opened_by_user();
        }

        [ActianTodo]
        public override void SaveChanges_uses_explicit_transaction_without_committing(bool autoTransaction)
        {
            base.SaveChanges_uses_explicit_transaction_without_committing(autoTransaction);
        }

        [ActianTodo]
        public override void SaveChanges_false_uses_explicit_transaction_without_committing_or_accepting_changes(bool autoTransaction)
        {
            base.SaveChanges_false_uses_explicit_transaction_without_committing_or_accepting_changes(autoTransaction);
        }

        [ActianTodo]
        public override Task SaveChangesAsync_uses_explicit_transaction_without_committing(bool autoTransaction)
        {
            return base.SaveChangesAsync_uses_explicit_transaction_without_committing(autoTransaction);
        }

        [ActianTodo]
        public override Task SaveChangesAsync_false_uses_explicit_transaction_without_committing_or_accepting_changes(
            bool autoTransaction)
        {
            return base.SaveChangesAsync_false_uses_explicit_transaction_without_committing_or_accepting_changes(autoTransaction);
        }

        [ActianTodo]
        public override void SaveChanges_uses_explicit_transaction_and_does_not_rollback_on_failure(bool autoTransaction)
        {
            base.SaveChanges_uses_explicit_transaction_and_does_not_rollback_on_failure(autoTransaction);
        }

        [ActianTodo]
        public override Task SaveChangesAsync_uses_explicit_transaction_and_does_not_rollback_on_failure(bool autoTransaction)
        {
            return base.SaveChangesAsync_uses_explicit_transaction_and_does_not_rollback_on_failure(autoTransaction);
        }

        [ActianTodo]
        public override Task RelationalTransaction_can_be_committed(bool autoTransaction)
        {
            return base.RelationalTransaction_can_be_committed(autoTransaction);
        }

        [ActianTodo]
        public override Task RelationalTransaction_can_be_committed_from_context(bool autoTransaction)
        {
            return base.RelationalTransaction_can_be_committed_from_context(autoTransaction);
        }

        [ActianTodo]
        public override Task RelationalTransaction_can_be_rolled_back(bool autoTransaction)
        {
            return base.RelationalTransaction_can_be_rolled_back(autoTransaction);
        }

        [ActianTodo]
        public override Task RelationalTransaction_can_be_rolled_back_from_context(bool autoTransaction)
        {
            return base.RelationalTransaction_can_be_rolled_back_from_context(autoTransaction);
        }

        [ActianTodo]
        public override void Query_uses_explicit_transaction(bool autoTransaction)
        {
            base.Query_uses_explicit_transaction(autoTransaction);
        }

        [ActianTodo]
        public override Task QueryAsync_uses_explicit_transaction(bool autoTransaction)
        {
            return base.QueryAsync_uses_explicit_transaction(autoTransaction);
        }

        [ActianTodo]
        public override Task Can_use_open_connection_with_started_transaction(bool autoTransaction)
        {
            return base.Can_use_open_connection_with_started_transaction(autoTransaction);
        }

        [ActianTodo]
        public override void UseTransaction_throws_if_mismatched_connection()
        {
            base.UseTransaction_throws_if_mismatched_connection();
        }

        [ActianTodo]
        public override void UseTransaction_throws_if_another_transaction_started()
        {
            base.UseTransaction_throws_if_another_transaction_started();
        }

        [ActianTodo]
        public override void UseTransaction_will_not_dispose_external_transaction()
        {
            base.UseTransaction_will_not_dispose_external_transaction();
        }

        [ActianTodo]
        public override void UseTransaction_throws_if_ambient_transaction_started()
        {
            base.UseTransaction_throws_if_ambient_transaction_started();
        }

        [ActianTodo]
        public override void UseTransaction_throws_if_enlisted_in_transaction()
        {
            base.UseTransaction_throws_if_enlisted_in_transaction();
        }

        [ActianTodo]
        public override void BeginTransaction_throws_if_another_transaction_started()
        {
            base.BeginTransaction_throws_if_another_transaction_started();
        }

        [ActianTodo]
        public override void BeginTransaction_throws_if_ambient_transaction_started()
        {
            base.BeginTransaction_throws_if_ambient_transaction_started();
        }

        [ActianTodo]
        public override void BeginTransaction_throws_if_enlisted_in_transaction()
        {
            base.BeginTransaction_throws_if_enlisted_in_transaction();
        }

        [ActianTodo]
        public override void BeginTransaction_can_be_used_after_ambient_transaction_ended()
        {
            base.BeginTransaction_can_be_used_after_ambient_transaction_ended();
        }

        [ActianTodo]
        public override void BeginTransaction_can_be_used_after_enlisted_transaction_ended()
        {
            base.BeginTransaction_can_be_used_after_enlisted_transaction_ended();
        }

        [ActianTodo]
        public override void BeginTransaction_can_be_used_after_another_transaction_if_connection_closed()
        {
            base.BeginTransaction_can_be_used_after_another_transaction_if_connection_closed();
        }

        [ActianTodo]
        public override void BeginTransaction_can_be_used_after_enlisted_transaction_if_connection_closed()
        {
            base.BeginTransaction_can_be_used_after_enlisted_transaction_if_connection_closed();
        }

        [ActianTodo]
        public override void EnlistTransaction_throws_if_another_transaction_started()
        {
            base.EnlistTransaction_throws_if_another_transaction_started();
        }

        [ActianTodo]
        public override void EnlistTransaction_throws_if_ambient_transaction_started()
        {
            base.EnlistTransaction_throws_if_ambient_transaction_started();
        }

        [ActianTodo]
        public override Task Externally_closed_connections_are_handled_correctly(bool async)
        {
            return base.Externally_closed_connections_are_handled_correctly(async);
        }

        protected override bool SnapshotSupported => true;

        protected override bool AmbientTransactionsSupported => true;

        protected override DbContext CreateContextWithConnectionString()
        {
            var store = Fixture.TestStore as ActianTestStore;
            var options = Fixture.AddOptions(
                    new DbContextOptionsBuilder()
                        .ConfigureActianTestWarnings()
                        .UseActian(
                            TestStore.ConnectionString,
                            b => b.ApplyConfiguration().ExecutionStrategy(c => new ActianExecutionStrategy(c))))
                .UseInternalServiceProvider(Fixture.ServiceProvider);

            return new DbContext(options.Options);
        }

        public class TransactionActianFixture : TransactionFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override void Seed(PoolableDbContext context)
            {
                base.Seed(context);

                context.Database.ExecuteSqlRaw("ALTER DATABASE [" + StoreName + "] SET ALLOW_SNAPSHOT_ISOLATION ON");
                context.Database.ExecuteSqlRaw("ALTER DATABASE [" + StoreName + "] SET READ_COMMITTED_SNAPSHOT ON");
            }

            public override void Reseed()
            {
                using (var context = CreateContext())
                {
                    context.Set<TransactionCustomer>().RemoveRange(context.Set<TransactionCustomer>());
                    context.SaveChanges();

                    base.Seed(context);
                }
            }

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            {
                new ActianDbContextOptionsBuilder(
                        base.AddOptions(builder))
                    .ExecutionStrategy(c => new ActianExecutionStrategy(c));
                return builder;
            }
        }
    }
}
