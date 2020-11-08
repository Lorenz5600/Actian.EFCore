using Actian.EFCore.Infrastructure;

namespace Actian.EFCore.TestUtilities
{
    public static class ActianDbContextOptionsBuilderExtensions
    {
        public static ActianDbContextOptionsBuilder ApplyConfiguration(this ActianDbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ExecutionStrategy(d => new TestActianRetryingExecutionStrategy(d));

            optionsBuilder.CommandTimeout(ActianTestStore.CommandTimeout);

            return optionsBuilder;
        }
    }
}
