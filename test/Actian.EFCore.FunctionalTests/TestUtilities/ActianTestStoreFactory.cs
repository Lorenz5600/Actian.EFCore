using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Actian.EFCore.TestUtilities
{
    public class ActianTestStoreFactory : RelationalTestStoreFactory
    {
        protected ActianTestStoreFactory()
        {
        }

        public static ActianTestStoreFactory Instance { get; } = new ActianTestStoreFactory();

        public override TestStore Create(string storeName)
            => ActianTestStore.Create(storeName);

        public override TestStore GetOrCreate(string storeName)
            => ActianTestStore.GetOrCreate(storeName);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
        {
            new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseCreator, TestActianDatabaseCreator>();
            return serviceCollection.AddEntityFrameworkActian();
        }

        public override ListLoggerFactory CreateListLoggerFactory(Func<string, bool> shouldLogCategory)
        {
            return new ActianTestSqlLoggerFactory(shouldLogCategory);
        }
    }
}
