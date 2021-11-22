using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class AsTrackingActianTest : AsTrackingTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public AsTrackingActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Entity_added_to_state_manager(bool useParam)
        {
            base.Entity_added_to_state_manager(useParam);
        }

        public override void Applied_to_body_clause()
        {
            base.Applied_to_body_clause();
        }

        public override void Applied_to_multiple_body_clauses()
        {
            base.Applied_to_multiple_body_clauses();
        }

        public override void Applied_to_body_clause_with_projection()
        {
            base.Applied_to_body_clause_with_projection();
        }

        public override void Applied_to_projection()
        {
            base.Applied_to_projection();
        }
    }
}
