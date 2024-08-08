using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.Query
{
    public class TPHFiltersInheritanceQueryActianFixture : TPHInheritanceQueryFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => ActianTestStoreFactory.Instance;
    }
}
