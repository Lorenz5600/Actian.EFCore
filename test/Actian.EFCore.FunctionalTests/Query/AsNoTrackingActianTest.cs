using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class AsNoTrackingActianTest : AsNoTrackingTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public AsNoTrackingActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Entity_not_added_to_state_manager(bool useParam)
        {
            base.Entity_not_added_to_state_manager(useParam);
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

        public override void Can_get_current_values()
        {
            base.Can_get_current_values();
        }

        public override void Include_reference_and_collection()
        {
            base.Include_reference_and_collection();
        }

        public override void Applied_after_navigation_expansion()
        {
            base.Applied_after_navigation_expansion();
        }

        public override void Where_simple_shadow()
        {
            base.Where_simple_shadow();
        }

        public override void Query_fast_path_when_ctor_binding()
        {
            base.Query_fast_path_when_ctor_binding();
        }

        public override Task Query_fast_path_when_ctor_binding_async()
        {
            return base.Query_fast_path_when_ctor_binding_async();
        }

        public override void SelectMany_simple()
        {
            base.SelectMany_simple();
        }
    }
}
