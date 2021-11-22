using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class GearsOfWarQueryActianFixture : GearsOfWarQueryRelationalFixture, IActianSqlFixture
    {
        protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

        protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            return base.AddServices(serviceCollection);
        }

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
