using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.Query
{
    public class TPHInheritanceQueryActianFixture : TPHInheritanceQueryFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => ActianTestStoreFactory.Instance;
    }
}
