using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class ChangeTrackingActianTest : ChangeTrackingTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public ChangeTrackingActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Entity_reverts_when_state_set_to_unchanged()
        {
            base.Entity_reverts_when_state_set_to_unchanged();
        }

        public override void Multiple_entities_can_revert()
        {
            base.Multiple_entities_can_revert();
        }

        public override void Entity_does_not_revert_when_attached_on_DbContext()
        {
            base.Entity_does_not_revert_when_attached_on_DbContext();
        }

        public override void Entity_does_not_revert_when_attached_on_DbSet()
        {
            base.Entity_does_not_revert_when_attached_on_DbSet();
        }

        [ActianTodo]
        public override void Entity_range_does_not_revert_when_attached_dbContext()
        {
            base.Entity_range_does_not_revert_when_attached_dbContext();
        }

        [ActianTodo]
        public override void Entity_range_does_not_revert_when_attached_dbSet()
        {
            base.Entity_range_does_not_revert_when_attached_dbSet();
        }

        [ActianTodo]
        public override void Can_disable_and_reenable_query_result_tracking()
        {
            base.Can_disable_and_reenable_query_result_tracking();
        }

        [ActianTodo]
        public override void Can_disable_and_reenable_query_result_tracking_starting_with_NoTracking()
        {
            base.Can_disable_and_reenable_query_result_tracking_starting_with_NoTracking();
        }

        public override void Can_disable_and_reenable_query_result_tracking_query_caching()
        {
            base.Can_disable_and_reenable_query_result_tracking_query_caching();
        }

        [ActianTodo]
        public override void Can_disable_and_reenable_query_result_tracking_query_caching_using_options()
        {
            base.Can_disable_and_reenable_query_result_tracking_query_caching_using_options();
        }

        public override void Can_disable_and_reenable_query_result_tracking_query_caching_single_context()
        {
            base.Can_disable_and_reenable_query_result_tracking_query_caching_single_context();
        }

        [ActianTodo]
        public override void AsTracking_switches_tracking_on_when_off_in_options()
        {
            base.AsTracking_switches_tracking_on_when_off_in_options();
        }

        public override void Precedence_of_tracking_modifiers()
        {
            base.Precedence_of_tracking_modifiers();
        }

        public override void Precedence_of_tracking_modifiers2()
        {
            base.Precedence_of_tracking_modifiers2();
        }

        public override void Precedence_of_tracking_modifiers3()
        {
            base.Precedence_of_tracking_modifiers3();
        }

        public override void Precedence_of_tracking_modifiers4()
        {
            base.Precedence_of_tracking_modifiers4();
        }

        public override void Precedence_of_tracking_modifiers5()
        {
            base.Precedence_of_tracking_modifiers5();
        }
    }
}
