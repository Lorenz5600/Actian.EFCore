using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class ConferencePlannerActianTest : ConferencePlannerTestBase<ConferencePlannerActianTest.ConferencePlannerActianFixture>
    {
        public ConferencePlannerActianTest(ConferencePlannerActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override Task AttendeesController_Get()
        {
            return base.AttendeesController_Get();
        }

        [ActianTodo]
        public override Task AttendeesController_GetSessions()
        {
            return base.AttendeesController_GetSessions();
        }

        [ActianTodo]
        public override Task AttendeesController_Post_with_new_attendee()
        {
            return base.AttendeesController_Post_with_new_attendee();
        }

        [ActianTodo]
        public override Task AttendeesController_Post_with_existing_attendee()
        {
            return base.AttendeesController_Post_with_existing_attendee();
        }

        [ActianTodo]
        public override Task AttendeesController_AddSession()
        {
            return base.AttendeesController_AddSession();
        }

        [ActianTodo]
        public override Task AttendeesController_AddSession_bad_session()
        {
            return base.AttendeesController_AddSession_bad_session();
        }

        [ActianTodo]
        public override Task AttendeesController_AddSession_bad_attendee()
        {
            return base.AttendeesController_AddSession_bad_attendee();
        }

        [ActianTodo]
        public override Task AttendeesController_RemoveSession()
        {
            return base.AttendeesController_RemoveSession();
        }

        [ActianTodo]
        public override Task AttendeesController_RemoveSession_bad_session()
        {
            return base.AttendeesController_RemoveSession_bad_session();
        }

        [ActianTodo]
        public override Task AttendeesController_RemoveSession_bad_attendee()
        {
            return base.AttendeesController_RemoveSession_bad_attendee();
        }

        [ActianTodo]
        public override Task SearchController_Search(
            string searchTerm, int totalCount, int sessionCount)
        {
            return base.SearchController_Search(searchTerm, totalCount, sessionCount);
        }

        [ActianTodo]
        public override Task SessionsController_Get()
        {
            return base.SessionsController_Get();
        }

        [ActianTodo]
        public override Task SessionsController_Get_with_ID()
        {
            return base.SessionsController_Get_with_ID();
        }

        [ActianTodo]
        public override Task SessionsController_Get_with_bad_ID()
        {
            return base.SessionsController_Get_with_bad_ID();
        }

        [ActianTodo]
        public override Task SessionsController_Post()
        {
            return base.SessionsController_Post();
        }

        [ActianTodo]
        public override Task SessionsController_Put()
        {
            return base.SessionsController_Put();
        }

        [ActianTodo]
        public override Task SessionsController_Put_with_bad_ID()
        {
            return base.SessionsController_Put_with_bad_ID();
        }

        [ActianTodo]
        public override Task SessionsController_Delete()
        {
            return base.SessionsController_Delete();
        }

        [ActianTodo]
        public override Task SessionsController_Delete_with_bad_ID()
        {
            return base.SessionsController_Delete_with_bad_ID();
        }

        [ActianTodo]
        public override Task SpeakersController_GetSpeakers()
        {
            return base.SpeakersController_GetSpeakers();
        }

        [ActianTodo]
        public override Task SpeakersController_GetSpeaker_with_ID()
        {
            return base.SpeakersController_GetSpeaker_with_ID();
        }

        [ActianTodo]
        public override Task SpeakersController_GetSpeaker_with_bad_ID()
        {
            return base.SpeakersController_GetSpeaker_with_bad_ID();
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class ConferencePlannerActianFixture : ConferencePlannerFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
        }
    }
}
