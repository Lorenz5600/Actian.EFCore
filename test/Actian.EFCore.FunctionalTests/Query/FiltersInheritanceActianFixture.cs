using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;

namespace Actian.EFCore.Query
{
    public class FiltersInheritanceActianFixture : InheritanceActianFixture
    {
        protected override bool EnableFilters => true;

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
