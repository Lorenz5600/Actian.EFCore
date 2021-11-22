using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class ActianMigrationsFixture : MigrationsFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

        //protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
        //{
        //    serviceCollection = base.AddServices(serviceCollection);

        //    new EntityFrameworkRelationalServicesBuilder(serviceCollection)
        //        .TryAdd<IDatabaseCreator, TestActianDatabaseCreator>();

        //    return serviceCollection;
        //}

        public override MigrationsContext CreateContext()
        {
            var options = AddOptions(
                    new DbContextOptionsBuilder()
                        .UseActian(TestStore.ConnectionString, b => b.ApplyConfiguration())
                )
                .UseInternalServiceProvider(ServiceProvider)
                .Options;
            return new MigrationsContext(options);
        }
    }
}
