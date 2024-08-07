using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore
{
    public class SeedingActianTest : SeedingTestBase
    {
        protected override TestStore TestStore
            => ActianTestStore.Create("SeedingTest");

        protected override SeedingContext CreateContextWithEmptyDatabase(string testId)
            => new SeedingActianContext(testId);

        protected class SeedingActianContext : SeedingContext
        {
            public SeedingActianContext(string testId)
                : base(testId)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseActian(TestEnvironment.GetConnectionString($"Seeds{TestId}"));
        }
    }
}
