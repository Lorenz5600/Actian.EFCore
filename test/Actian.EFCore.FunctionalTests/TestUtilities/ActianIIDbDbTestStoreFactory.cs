using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.TestUtilities
{
    public class ActianIIDbDbTestStoreFactory : ActianTestStoreFactory
    {
        public const string Name = "iidbdb";
        public static readonly string NorthwindConnectionString = TestEnvironment.GetConnectionString(Name);
        public static new ActianIIDbDbTestStoreFactory Instance { get; } = new ActianIIDbDbTestStoreFactory();

        protected ActianIIDbDbTestStoreFactory()
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => ActianTestStore.GetOrCreateWithUser(Name, TestEnvironment.LoginUser);
    }
}
