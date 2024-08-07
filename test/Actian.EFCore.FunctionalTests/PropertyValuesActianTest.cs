using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore
{
    public class PropertyValuesActianTest : PropertyValuesTestBase<PropertyValuesActianTest.PropertyValuesActianFixture>
    {
        public PropertyValuesActianTest(PropertyValuesActianFixture fixture)
            : base(fixture)
        {
        }

        public class PropertyValuesActianFixture : PropertyValuesFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory
                => ActianTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<Building>()
                    .Property(b => b.Value).HasColumnType("decimal(18,2)");

                modelBuilder.Entity<CurrentEmployee>()
                    .Property(ce => ce.LeaveBalance).HasColumnType("decimal(18,2)");
            }
        }
    }
}
