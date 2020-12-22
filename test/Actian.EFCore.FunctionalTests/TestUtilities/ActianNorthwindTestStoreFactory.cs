using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.TestUtilities
{
    public class ActianNorthwindTestStoreFactory : ActianTestStoreFactory
    {
        public const string DatabaseName = "EFCore_Northwind";
        public const string DbmsUser = @"""dbo""";
        public static readonly string NorthwindConnectionString = TestEnvironment.GetConnectionString(DatabaseName, DbmsUser);

        public ActianNorthwindTestStoreFactory()
            : base(DbmsUser)
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => ActianTestStore.GetOrCreate(DatabaseName, DbmsUser, "Northwind.sql");
    }
}
