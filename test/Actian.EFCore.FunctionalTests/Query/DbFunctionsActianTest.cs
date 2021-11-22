using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class DbFunctionsActianTest : DbFunctionsTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public DbFunctionsActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Like_literal()
        {
            base.Like_literal();
            AssertSql(@"
                SELECT COUNT(*)
                FROM ""Customers"" AS ""c""
                WHERE ""c"".""ContactName"" LIKE N'%M%'
            ");
        }

        public override void Like_identity()
        {
            base.Like_identity();
            AssertSql(@"
                SELECT COUNT(*)
                FROM ""Customers"" AS ""c""
                WHERE ""c"".""ContactName"" LIKE ""c"".""ContactName""
            ");
        }

        public override void Like_literal_with_escape()
        {
            base.Like_literal_with_escape();
            AssertSql(@"
                SELECT COUNT(*)
                FROM ""Customers"" AS ""c""
                WHERE ""c"".""ContactName"" LIKE N'!%' ESCAPE N'!'
            ");
        }
    }
}
