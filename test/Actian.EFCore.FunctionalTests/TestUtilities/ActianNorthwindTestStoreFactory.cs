using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.TestUtilities
{
    public class ActianNorthwindTestStoreFactory : ActianTestStoreFactory
    {
        public const string Name = "Northwind";
        public static readonly string NorthwindConnectionString = ActianTestStore.CreateConnectionString(Name);
        public static new ActianNorthwindTestStoreFactory Instance { get; } = new ActianNorthwindTestStoreFactory();

        protected ActianNorthwindTestStoreFactory()
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => ActianTestStore.GetOrCreate(Name, "Northwind.sql");
    }
}
