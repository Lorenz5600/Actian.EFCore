using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ComplexNavigationsQueryActianFixture : ComplexNavigationsQueryRelationalFixtureBase, IActianSqlFixture
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
