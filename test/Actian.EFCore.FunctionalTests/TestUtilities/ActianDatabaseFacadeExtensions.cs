using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Actian.EFCore.TestUtilities
{
    public static class ActianDatabaseFacadeExtensions
    {
        public static void EnsureClean(this DatabaseFacade databaseFacade)
            => databaseFacade.CreateExecutionStrategy()
                .Execute(databaseFacade, database => new ActianDatabaseCleaner().Clean(database));
    }
}
