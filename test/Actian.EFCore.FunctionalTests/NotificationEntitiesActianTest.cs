using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class NotificationEntitiesActianTest : NotificationEntitiesTestBase<NotificationEntitiesActianTest.NotificationEntitiesActianFixture>
    {
        public NotificationEntitiesActianTest(NotificationEntitiesActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Include_brings_entities_referenced_from_already_tracked_notification_entities_as_Unchanged()
        {
            base.Include_brings_entities_referenced_from_already_tracked_notification_entities_as_Unchanged();
        }

        public override void Include_brings_collections_referenced_from_already_tracked_notification_entities_as_Unchanged()
        {
            base.Include_brings_collections_referenced_from_already_tracked_notification_entities_as_Unchanged();
        }

        public class NotificationEntitiesActianFixture : NotificationEntitiesFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
        }
    }
}
