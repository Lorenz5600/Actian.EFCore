using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.Query
{
    public class NullSemanticsQueryActianFixture : NullSemanticsQueryRelationalFixture, IActianSqlFixture
    {
        protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
    }
}
