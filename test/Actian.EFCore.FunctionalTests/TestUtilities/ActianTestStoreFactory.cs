using System;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Actian.EFCore.TestUtilities
{
    public class ActianTestStoreFactory : RelationalTestStoreFactory
    {
        public ActianTestStoreFactory(string dbmsUser)
        {
            _dbmsUser = dbmsUser;
        }

        private readonly string _dbmsUser;

        public override TestStore Create(string storeName)
            => ActianTestStore.Create(storeName, _dbmsUser);

        public override TestStore GetOrCreate(string storeName)
            => ActianTestStore.GetOrCreate(storeName, _dbmsUser);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkActian();
    }
}
