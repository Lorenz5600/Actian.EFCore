using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class AsyncFromSqlQueryActianTest : AsyncFromSqlQueryTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public AsyncFromSqlQueryActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override Task FromSqlRaw_queryable_simple()
        {
            return base.FromSqlRaw_queryable_simple();
        }

        public override Task FromSqlRaw_queryable_simple_columns_out_of_order()
        {
            return base.FromSqlRaw_queryable_simple_columns_out_of_order();
        }

        public override Task FromSqlRaw_queryable_simple_columns_out_of_order_and_extra_columns()
        {
            return base.FromSqlRaw_queryable_simple_columns_out_of_order_and_extra_columns();
        }

        public override Task FromSqlRaw_queryable_composed()
        {
            return base.FromSqlRaw_queryable_composed();
        }

        public override Task FromSqlRaw_queryable_multiple_composed()
        {
            return base.FromSqlRaw_queryable_multiple_composed();
        }

        public override Task FromSqlRaw_queryable_multiple_composed_with_closure_parameters()
        {
            return base.FromSqlRaw_queryable_multiple_composed_with_closure_parameters();
        }

        public override Task FromSqlRaw_queryable_multiple_composed_with_parameters_and_closure_parameters()
        {
            return base.FromSqlRaw_queryable_multiple_composed_with_parameters_and_closure_parameters();
        }

        public override Task FromSqlRaw_queryable_multiple_line_query()
        {
            return base.FromSqlRaw_queryable_multiple_line_query();
        }

        public override Task FromSqlRaw_queryable_composed_multiple_line_query()
        {
            return base.FromSqlRaw_queryable_composed_multiple_line_query();
        }

        public override Task FromSqlRaw_queryable_with_parameters()
        {
            return base.FromSqlRaw_queryable_with_parameters();
        }

        public override Task FromSqlRaw_queryable_with_parameters_and_closure()
        {
            return base.FromSqlRaw_queryable_with_parameters_and_closure();
        }

        public override Task FromSqlRaw_queryable_simple_cache_key_includes_query_string()
        {
            return base.FromSqlRaw_queryable_simple_cache_key_includes_query_string();
        }

        public override Task FromSqlRaw_queryable_with_parameters_cache_key_includes_parameters()
        {
            return base.FromSqlRaw_queryable_with_parameters_cache_key_includes_parameters();
        }

        public override Task FromSqlRaw_queryable_simple_as_no_tracking_not_composed()
        {
            return base.FromSqlRaw_queryable_simple_as_no_tracking_not_composed();
        }

        public override Task FromSqlRaw_queryable_simple_projection_not_composed()
        {
            return base.FromSqlRaw_queryable_simple_projection_not_composed();
        }

        public override Task FromSqlRaw_queryable_simple_include()
        {
            return base.FromSqlRaw_queryable_simple_include();
        }

        public override Task FromSqlRaw_queryable_simple_composed_include()
        {
            return base.FromSqlRaw_queryable_simple_composed_include();
        }

        public override Task FromSqlRaw_annotations_do_not_affect_successive_calls()
        {
            return base.FromSqlRaw_annotations_do_not_affect_successive_calls();
        }

        public override Task FromSqlRaw_composed_with_nullable_predicate()
        {
            return base.FromSqlRaw_composed_with_nullable_predicate();
        }

        public override Task Include_does_not_close_user_opened_connection_for_empty_result()
        {
            return base.Include_does_not_close_user_opened_connection_for_empty_result();
        }

        public override Task Include_closed_connection_opened_by_it_when_buffering()
        {
            return base.Include_closed_connection_opened_by_it_when_buffering();
        }
    }
}
