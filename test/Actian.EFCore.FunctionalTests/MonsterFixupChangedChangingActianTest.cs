using System;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class MonsterFixupChangedChangingActianTest : MonsterFixupTestBase<MonsterFixupChangedChangingActianTest.MonsterFixupChangedChangingActianFixture>
    {
        public MonsterFixupChangedChangingActianTest(MonsterFixupChangedChangingActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public override void Dispose()
        {
            Helpers.LogSql();
            base.Dispose();
        }

        [ActianTodo]
        public override void Can_build_monster_model_and_seed_data_using_FKs()
        {
            base.Can_build_monster_model_and_seed_data_using_FKs();
        }

        [ActianTodo]
        public override void Can_build_monster_model_and_seed_data_using_all_navigations()
        {
            base.Can_build_monster_model_and_seed_data_using_all_navigations();
        }

        [ActianTodo]
        public override void Can_build_monster_model_and_seed_data_using_dependent_navigations()
        {
            base.Can_build_monster_model_and_seed_data_using_dependent_navigations();
        }

        [ActianTodo]
        public override void Can_build_monster_model_and_seed_data_using_principal_navigations()
        {
            base.Can_build_monster_model_and_seed_data_using_principal_navigations();
        }

        [ActianTodo]
        public override void Can_build_monster_model_and_seed_data_using_navigations_with_deferred_add()
        {
            base.Can_build_monster_model_and_seed_data_using_navigations_with_deferred_add();
        }

        [ActianTodo]
        public override void One_to_many_fixup_happens_when_FKs_change_test()
        {
            base.One_to_many_fixup_happens_when_FKs_change_test();
        }

        [ActianTodo]
        public override void One_to_many_fixup_happens_when_reference_changes()
        {
            base.One_to_many_fixup_happens_when_reference_changes();
        }

        [ActianTodo]
        public override void One_to_many_fixup_happens_when_collection_changes()
        {
            base.One_to_many_fixup_happens_when_collection_changes();
        }

        [ActianTodo]
        public override void One_to_one_fixup_happens_when_FKs_change_test()
        {
            base.One_to_one_fixup_happens_when_FKs_change_test();
        }

        [ActianTodo]
        public override void One_to_one_fixup_happens_when_reference_change_test()
        {
            base.One_to_one_fixup_happens_when_reference_change_test();
        }

        [ActianTodo]
        public override void Composite_fixup_happens_when_FKs_change_test()
        {
            base.Composite_fixup_happens_when_FKs_change_test();
        }

        [ActianTodo]
        public override void Fixup_with_binary_keys_happens_when_FKs_or_navigations_change_test()
        {
            base.Fixup_with_binary_keys_happens_when_FKs_or_navigations_change_test();
        }

        public class MonsterFixupChangedChangingActianFixture : MonsterFixupChangedChangingFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override void OnModelCreating<TMessage, TProduct, TProductPhoto, TProductReview, TComputerDetail, TDimensions>(
                ModelBuilder builder)
            {
                base.OnModelCreating<TMessage, TProduct, TProductPhoto, TProductReview, TComputerDetail, TDimensions>(builder);

                //builder.Entity<TMessage>().Property(e => e.MessageId).UseIdentityColumn(); // Defined in SqlServerPropertyBuilderExtensions

                builder.Entity<TProduct>()
                    .OwnsOne(
                        c => (TDimensions)c.Dimensions, db =>
                        {
                            db.Property(d => d.Depth).HasColumnType("decimal(18,2)");
                            db.Property(d => d.Width).HasColumnType("decimal(18,2)");
                            db.Property(d => d.Height).HasColumnType("decimal(18,2)");
                        });

                //builder.Entity<TProductPhoto>().Property(e => e.PhotoId).UseIdentityColumn(); // Defined in SqlServerPropertyBuilderExtensions
                //builder.Entity<TProductReview>().Property(e => e.ReviewId).UseIdentityColumn(); // Defined in SqlServerPropertyBuilderExtensions

                builder.Entity<TComputerDetail>()
                    .OwnsOne(
                        c => (TDimensions)c.Dimensions, db =>
                        {
                            db.Property(d => d.Depth).HasColumnType("decimal(18,2)");
                            db.Property(d => d.Width).HasColumnType("decimal(18,2)");
                            db.Property(d => d.Height).HasColumnType("decimal(18,2)");
                        });

                ActianModelTestHelpers.MaxLengthStringKeys
                    .Normalize(builder);

                ActianModelTestHelpers.Guids
                    .Normalize(builder);
            }
        }
    }
}
