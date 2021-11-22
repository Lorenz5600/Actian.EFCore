using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class CompositeKeyEndToEndActianTest : CompositeKeyEndToEndTestBase<CompositeKeyEndToEndActianTest.CompositeKeyEndToEndActianFixture>
    {
        public CompositeKeyEndToEndActianTest(CompositeKeyEndToEndActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override Task Can_use_two_non_generated_integers_as_composite_key_end_to_end()
        {
            return base.Can_use_two_non_generated_integers_as_composite_key_end_to_end();
        }

        [ActianTodo]
        public override Task Can_use_generated_values_in_composite_key_end_to_end()
        {
            return base.Can_use_generated_values_in_composite_key_end_to_end();
        }

        [ActianTodo]
        public override Task Only_one_part_of_a_composite_key_needs_to_vary_for_uniqueness()
        {
            return base.Only_one_part_of_a_composite_key_needs_to_vary_for_uniqueness();
        }

        public class CompositeKeyEndToEndActianFixture : CompositeKeyEndToEndFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
        }
    }
}
