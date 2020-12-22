using Actian.EFCore.TestUtilities;
using Xunit;

namespace Actian.EFCore
{
    public class NorthwindDatabaseTest
    {
        [ConditionalFact]
        public void Can_create_northwind_database()
        {
            using (ActianTestStore.GetNorthwindStore())
            {
            }
        }
    }
}
