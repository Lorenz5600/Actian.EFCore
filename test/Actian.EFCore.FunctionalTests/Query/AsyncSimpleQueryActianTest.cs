using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore.Query
{
    public class AsyncSimpleQueryActianTest : AsyncSimpleQueryTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public AsyncSimpleQueryActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override Task GroupBy_tracking_after_dispose()
        {
            return base.GroupBy_tracking_after_dispose();
        }

        public override Task Query_backed_by_database_view()
        {
            return base.Query_backed_by_database_view();
        }

        public override Task ToList_context_subquery_deadlock_issue()
        {
            return base.ToList_context_subquery_deadlock_issue();
        }

        public override Task ToArray_on_nav_subquery_in_projection()
        {
            return base.ToArray_on_nav_subquery_in_projection();
        }

        public override Task ToArray_on_nav_subquery_in_projection_nested()
        {
            return base.ToArray_on_nav_subquery_in_projection_nested();
        }

        public override Task ToList_on_nav_subquery_in_projection()
        {
            return base.ToList_on_nav_subquery_in_projection();
        }

        public override Task ToList_on_nav_subquery_with_predicate_in_projection()
        {
            return base.ToList_on_nav_subquery_with_predicate_in_projection();
        }

        public override Task Average_on_nav_subquery_in_projection()
        {
            return base.Average_on_nav_subquery_in_projection();
        }

        [ActianSkip(LongRunning)]
        public override Task ToListAsync_can_be_canceled()
        {
            return base.ToListAsync_can_be_canceled();
        }

        [ActianTodo]
        public override Task Mixed_sync_async_query()
        {
            return base.Mixed_sync_async_query();
        }

        public override Task LoadAsync_should_track_results()
        {
            return base.LoadAsync_should_track_results();
        }

        public override Task Mixed_sync_async_in_query_cache()
        {
            return base.Mixed_sync_async_in_query_cache();
        }

        public override Task Throws_on_concurrent_query_list()
        {
            return Task.CompletedTask;
        }

        public override Task Throws_on_concurrent_query_first()
        {
            return base.Throws_on_concurrent_query_first();
        }

        public override Task Concat_dbset()
        {
            return base.Concat_dbset();
        }

        public override Task Concat_simple()
        {
            return Task.CompletedTask;
        }

        public override Task Concat_non_entity()
        {
            return Task.CompletedTask;
        }

        public override Task Except_non_entity()
        {
            return base.Except_non_entity();
        }

        public override Task Intersect_non_entity()
        {
            return base.Intersect_non_entity();
        }

        public override Task Union_non_entity()
        {
            return base.Union_non_entity();
        }

        public override Task Select_bitwise_or()
        {
            return base.Select_bitwise_or();
        }

        public override Task Select_bitwise_or_multiple()
        {
            return base.Select_bitwise_or_multiple();
        }

        public override Task Select_bitwise_and()
        {
            return base.Select_bitwise_and();
        }

        public override Task Select_bitwise_and_or()
        {
            return base.Select_bitwise_and_or();
        }

        public override Task Select_bitwise_or_with_logical_or()
        {
            return base.Select_bitwise_or_with_logical_or();
        }

        public override Task Select_bitwise_and_with_logical_and()
        {
            return base.Select_bitwise_and_with_logical_and();
        }
    }
}
