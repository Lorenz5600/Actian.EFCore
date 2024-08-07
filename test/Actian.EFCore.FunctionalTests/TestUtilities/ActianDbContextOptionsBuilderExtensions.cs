using Actian.EFCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Actian.EFCore.TestUtilities
{
    public static class ActianDbContextOptionsBuilderExtensions
    {
        public static ActianDbContextOptionsBuilder ApplyConfiguration(this ActianDbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);

            optionsBuilder.ExecutionStrategy(d => new TestActianRetryingExecutionStrategy(d));

            optionsBuilder.CommandTimeout(ActianTestStore.CommandTimeout);

            return optionsBuilder;
        }
    }
}
