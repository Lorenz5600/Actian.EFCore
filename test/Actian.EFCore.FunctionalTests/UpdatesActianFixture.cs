using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.UpdatesModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore
{
    public class UpdatesActianFixture : UpdatesRelationalFixture, IActianSqlFixture
    {
        protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");

            modelBuilder
                .Entity<LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingCorrectly>()
                .Property(l => l.ProfileId3).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Profile>()
                .Property(l => l.Id3).HasColumnType("decimal(18,2)");

            ActianModelTestHelpers.MaxLengthStringKeys
                .Normalize(modelBuilder);

            //ActianModelTestHelpers.Guids
            //    .Normalize(modelBuilder);
        }
    }
}
