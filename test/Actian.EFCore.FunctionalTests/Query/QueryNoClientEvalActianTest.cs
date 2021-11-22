using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class QueryNoClientEvalActianTest : QueryNoClientEvalTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public QueryNoClientEvalActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Throws_when_where()
        {
            base.Throws_when_where();
        }

        public override void Throws_when_orderby()
        {
            base.Throws_when_orderby();
        }

        public override void Throws_when_orderby_multiple()
        {
            base.Throws_when_orderby_multiple();
        }

        public override void Throws_when_where_subquery_correlated()
        {
            base.Throws_when_where_subquery_correlated();
        }

        public override void Throws_when_all()
        {
            base.Throws_when_all();
        }

        public override void Throws_when_from_sql_composed()
        {
            base.Throws_when_from_sql_composed();
        }

        public override void Doesnt_throw_when_from_sql_not_composed()
        {
            base.Doesnt_throw_when_from_sql_not_composed();
        }

        public override void Throws_when_subquery_main_from_clause()
        {
            base.Throws_when_subquery_main_from_clause();
        }

        public override void Throws_when_select_many()
        {
            base.Throws_when_select_many();
        }

        public override void Throws_when_join()
        {
            base.Throws_when_join();
        }

        public override void Throws_when_group_join()
        {
            base.Throws_when_group_join();
        }

        [ActianTodo]
        public override void Throws_when_group_by()
        {
            base.Throws_when_group_by();
        }

        public override void Throws_when_first()
        {
            base.Throws_when_first();
        }

        public override void Throws_when_single()
        {
            base.Throws_when_single();
        }

        public override void Throws_when_first_or_default()
        {
            base.Throws_when_first_or_default();
        }

        public override void Throws_when_single_or_default()
        {
            base.Throws_when_single_or_default();
        }
    }
}
