using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.Query
{
    public class InheritanceActianFixture : InheritanceRelationalFixture, IActianSqlFixture
    {
        protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            ActianModelTestHelpers.MaxLengthStringKeys
                .Normalize(modelBuilder);

            ActianModelTestHelpers.Guids
                .Normalize(modelBuilder);
        }
    }
}
