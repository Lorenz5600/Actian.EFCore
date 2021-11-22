using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class AsyncGearsOfWarQueryActianTest : AsyncGearsOfWarQueryTestBase<GearsOfWarQueryActianFixture>
    {
        public AsyncGearsOfWarQueryActianTest(GearsOfWarQueryActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override Task Include_with_group_by_on_entity_qsre()
        {
            return base.Include_with_group_by_on_entity_qsre();
        }

        [ActianTodo]
        public override Task Include_with_group_by_on_entity_qsre_with_composite_key()
        {
            return base.Include_with_group_by_on_entity_qsre_with_composite_key();
        }

        [ActianTodo]
        public override Task Include_with_group_by_on_entity_navigation()
        {
            return base.Include_with_group_by_on_entity_navigation();
        }

        [ActianTodo]
        public override Task Include_groupby_constant()
        {
            return base.Include_groupby_constant();
        }

        public override Task Cast_to_derived_type_causes_client_eval()
        {
            return base.Cast_to_derived_type_causes_client_eval();
        }

        public override Task Sum_with_no_data_nullable_double()
        {
            return base.Sum_with_no_data_nullable_double();
        }

        public override Task GroupBy_Select_sum()
        {
            return base.GroupBy_Select_sum();
        }
    }
}
