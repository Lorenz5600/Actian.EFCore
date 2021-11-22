using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class SeedingActianTest : SeedingTestBase
    {
        public SeedingActianTest(ITestOutputHelper testOutputHelper)
        {
            TestEnvironment.Log(this, testOutputHelper);
        }

        public override Task Seeding_does_not_leave_context_contaminated(bool async)
        {
            return base.Seeding_does_not_leave_context_contaminated(async);
        }

        protected override SeedingContext CreateContextWithEmptyDatabase(string testId)
        {
            var context = new SeedingActianContext(testId);
            context.Database.EnsureClean();
            return context;
        }

        protected class SeedingActianContext : SeedingContext
        {
            public SeedingActianContext(string testId) : base(testId) { }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseActian(TestEnvironment.GetConnectionString($"Seeds{TestId}"));
        }
    }
}
