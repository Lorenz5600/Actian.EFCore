using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Actian.EFCore.TestUtilities
{
    public class ActianTestStoreFactory : RelationalTestStoreFactory
    {
        public static ActianTestStoreFactory Instance { get; } = new ActianTestStoreFactory();

        protected ActianTestStoreFactory()
        {
        }

        public override TestStore Create(string storeName)
            => ActianTestStore.Create(storeName);

        public override TestStore GetOrCreate(string storeName)
            => ActianTestStore.GetOrCreate(storeName);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkActian();
    }
}
