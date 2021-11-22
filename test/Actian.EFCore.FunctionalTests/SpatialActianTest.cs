using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class SpatialActianTest : SpatialTestBase<SpatialActianFixture>
    {
        public SpatialActianTest(SpatialActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Values_are_copied_into_change_tracker()
        {
            base.Values_are_copied_into_change_tracker();
        }

        [ActianTodo]
        public override void Values_arent_compared_by_reference()
        {
            base.Values_arent_compared_by_reference();
        }

        [ActianTodo]
        public override void Mutation_of_tracked_values_does_not_mutate_values_in_store()
        {
            base.Mutation_of_tracked_values_does_not_mutate_values_in_store();
        }

        [ActianTodo]
        public override void Translators_handle_static_members()
        {
            base.Translators_handle_static_members();
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());
    }
}
