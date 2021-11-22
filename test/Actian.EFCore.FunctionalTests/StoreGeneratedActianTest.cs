using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable xUnit1024 // Test methods cannot have overloads
namespace Actian.EFCore
{
    public class StoreGeneratedActianTest : StoreGeneratedTestBase<StoreGeneratedActianTest.StoreGeneratedActianFixture>
    {
        public StoreGeneratedActianTest(StoreGeneratedActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Value_generation_throws_for_common_cases()
        {
            base.Value_generation_throws_for_common_cases();
        }

        [ActianTodo]
        public override void Value_generation_works_for_common_GUID_conversions()
        {
            base.Value_generation_works_for_common_GUID_conversions();
        }

        [ActianTodo]
        public override void Before_save_throw_always_throws_if_value_set(string propertyName)
        {
            base.Before_save_throw_always_throws_if_value_set(propertyName);
        }

        [ActianTodo]
        public override void Before_save_throw_ignores_value_if_not_set(string propertyName, string expectedValue)
        {
            base.Before_save_throw_ignores_value_if_not_set(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void Before_save_use_always_uses_value_if_set(string propertyName)
        {
            base.Before_save_use_always_uses_value_if_set(propertyName);
        }

        [ActianTodo]
        public override void Before_save_use_ignores_value_if_not_set(string propertyName, string expectedValue)
        {
            base.Before_save_use_ignores_value_if_not_set(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void Before_save_ignore_ignores_value_if_not_set(string propertyName, string expectedValue)
        {
            base.Before_save_ignore_ignores_value_if_not_set(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void Before_save_ignore_ignores_value_even_if_set(string propertyName, string expectedValue)
        {
            base.Before_save_ignore_ignores_value_even_if_set(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void After_save_throw_always_throws_if_value_modified(string propertyName)
        {
            base.After_save_throw_always_throws_if_value_modified(propertyName);
        }

        [ActianTodo]
        public override void After_save_throw_ignores_value_if_not_modified(string propertyName, string expectedValue)
        {
            base.After_save_throw_ignores_value_if_not_modified(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void After_save_ignore_ignores_value_if_not_modified(string propertyName, string expectedValue)
        {
            base.After_save_ignore_ignores_value_if_not_modified(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void After_save_ignore_ignores_value_even_if_modified(string propertyName, string expectedValue)
        {
            base.After_save_ignore_ignores_value_even_if_modified(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void After_save_use_ignores_value_if_not_modified(string propertyName, string expectedValue)
        {
            base.After_save_use_ignores_value_if_not_modified(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void After_save_use_uses_value_if_modified(string propertyName, string expectedValue)
        {
            base.After_save_use_uses_value_if_modified(propertyName, expectedValue);
        }

        [ActianTodo]
        public override void Identity_key_with_read_only_before_save_throws_if_explicit_values_set()
        {
            base.Identity_key_with_read_only_before_save_throws_if_explicit_values_set();
        }

        [ActianTodo]
        public override void Identity_property_on_Added_entity_with_temporary_value_gets_value_from_store()
        {
            base.Identity_property_on_Added_entity_with_temporary_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Identity_property_on_Added_entity_with_temporary_value_gets_value_from_store_even_if_same()
        {
            base.Identity_property_on_Added_entity_with_temporary_value_gets_value_from_store_even_if_same();
        }

        [ActianTodo]
        public override void Identity_property_on_Added_entity_with_default_value_gets_value_from_store()
        {
            base.Identity_property_on_Added_entity_with_default_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Identity_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set()
        {
            base.Identity_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set();
        }

        [ActianTodo]
        public override void Identity_property_on_Added_entity_can_have_value_set_explicitly()
        {
            base.Identity_property_on_Added_entity_can_have_value_set_explicitly();
        }

        [ActianTodo]
        public override void Identity_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state()
        {
            base.Identity_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state();
        }

        [ActianTodo]
        public override void Identity_property_on_Modified_entity_is_included_in_update_when_modified()
        {
            base.Identity_property_on_Modified_entity_is_included_in_update_when_modified();
        }

        [ActianTodo]
        public override void Identity_property_on_Modified_entity_is_not_included_in_update_when_not_modified()
        {
            base.Identity_property_on_Modified_entity_is_not_included_in_update_when_not_modified();
        }

        [ActianTodo]
        public override void Always_identity_property_on_Added_entity_with_temporary_value_gets_value_from_store()
        {
            base.Always_identity_property_on_Added_entity_with_temporary_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Always_identity_property_on_Added_entity_with_default_value_gets_value_from_store()
        {
            base.Always_identity_property_on_Added_entity_with_default_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Always_identity_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set()
        {
            base.Always_identity_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set();
        }

        [ActianTodo]
        public override void Always_identity_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state()
        {
            base.Always_identity_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state();
        }

        [ActianTodo]
        public override void Always_identity_property_on_Modified_entity_is_not_included_in_the_update_when_not_modified()
        {
            base.Always_identity_property_on_Modified_entity_is_not_included_in_the_update_when_not_modified();
        }

        [ActianTodo]
        public override void Computed_property_on_Added_entity_with_temporary_value_gets_value_from_store()
        {
            base.Computed_property_on_Added_entity_with_temporary_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Computed_property_on_Added_entity_with_default_value_gets_value_from_store()
        {
            base.Computed_property_on_Added_entity_with_default_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Computed_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set()
        {
            base.Computed_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set();
        }

        [ActianTodo]
        public override void Computed_property_on_Added_entity_can_have_value_set_explicitly()
        {
            base.Computed_property_on_Added_entity_can_have_value_set_explicitly();
        }

        [ActianTodo]
        public override void Computed_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state()
        {
            base.Computed_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state();
        }

        [ActianTodo]
        public override void Computed_property_on_Modified_entity_is_included_in_update_when_modified()
        {
            base.Computed_property_on_Modified_entity_is_included_in_update_when_modified();
        }

        [ActianTodo]
        public override void Computed_property_on_Modified_entity_is_read_from_store_when_not_modified()
        {
            base.Computed_property_on_Modified_entity_is_read_from_store_when_not_modified();
        }

        [ActianTodo]
        public override void Always_computed_property_on_Added_entity_with_temporary_value_gets_value_from_store()
        {
            base.Always_computed_property_on_Added_entity_with_temporary_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Always_computed_property_on_Added_entity_with_default_value_gets_value_from_store()
        {
            base.Always_computed_property_on_Added_entity_with_default_value_gets_value_from_store();
        }

        [ActianTodo]
        public override void Always_computed_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set()
        {
            base.Always_computed_property_on_Added_entity_with_read_only_before_save_throws_if_explicit_values_set();
        }

        [ActianTodo]
        public override void Always_computed_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state()
        {
            base.Always_computed_property_on_Modified_entity_with_read_only_after_save_throws_if_value_is_in_modified_state();
        }

        [ActianTodo]
        public override void Always_computed_property_on_Modified_entity_is_read_from_store_when_not_modified()
        {
            base.Always_computed_property_on_Modified_entity_is_read_from_store_when_not_modified();
        }

        [ActianTodo]
        public override void Fields_used_correctly_for_store_generated_values()
        {
            base.Fields_used_correctly_for_store_generated_values();
        }

        [ActianTodo]
        public override void Nullable_fields_get_defaults_when_not_set()
        {
            base.Nullable_fields_get_defaults_when_not_set();
        }

        [ActianTodo]
        public override void Nullable_fields_store_non_defaults_when_set()
        {
            base.Nullable_fields_store_non_defaults_when_set();
        }

        [ActianTodo]
        public override void Nullable_fields_store_any_value_when_set()
        {
            base.Nullable_fields_store_any_value_when_set();
        }

        [ActianTodo]
        [ConditionalFact]
        public new void Clearing_optional_FK_does_not_leave_temporary_value()
        {
            base.Clearing_optional_FK_does_not_leave_temporary_value();
        }


        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class StoreGeneratedActianFixture : StoreGeneratedFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => builder
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(
                        b => b.Default(WarningBehavior.Throw)
                            .Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning)
                            .Ignore(RelationalEventId.BoolWithDefaultWarning));

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                modelBuilder.Entity<Gumball>(
                    b =>
                    {
                        //b.Property(e => e.Id).UseIdentityColumn(); // TODO: Implement UseIdentityColumn()
                        b.Property(e => e.Identity).HasDefaultValue("Banana Joe");
                        b.Property(e => e.IdentityReadOnlyBeforeSave).HasDefaultValue("Doughnut Sheriff");
                        b.Property(e => e.IdentityReadOnlyAfterSave).HasDefaultValue("Anton");
                        b.Property(e => e.AlwaysIdentity).HasDefaultValue("Banana Joe");
                        b.Property(e => e.AlwaysIdentityReadOnlyBeforeSave).HasDefaultValue("Doughnut Sheriff");
                        b.Property(e => e.AlwaysIdentityReadOnlyAfterSave).HasDefaultValue("Anton");
                        b.Property(e => e.Computed).HasDefaultValue("Alan");
                        b.Property(e => e.ComputedReadOnlyBeforeSave).HasDefaultValue("Carmen");
                        b.Property(e => e.ComputedReadOnlyAfterSave).HasDefaultValue("Tina Rex");
                        b.Property(e => e.AlwaysComputed).HasDefaultValue("Alan");
                        b.Property(e => e.AlwaysComputedReadOnlyBeforeSave).HasDefaultValue("Carmen");
                        b.Property(e => e.AlwaysComputedReadOnlyAfterSave).HasDefaultValue("Tina Rex");
                    });

                modelBuilder.Entity<Anais>(
                    b =>
                    {
                        b.Property(e => e.OnAdd).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddUseBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddIgnoreBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddThrowBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddUseBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddIgnoreBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddThrowBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddUseBeforeThrowAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddIgnoreBeforeThrowAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddThrowBeforeThrowAfter).HasDefaultValue("Rabbit");

                        b.Property(e => e.OnAddOrUpdate).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateUseBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateIgnoreBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateThrowBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateUseBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateIgnoreBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateThrowBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateUseBeforeThrowAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateIgnoreBeforeThrowAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnAddOrUpdateThrowBeforeThrowAfter).HasDefaultValue("Rabbit");

                        b.Property(e => e.OnUpdate).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateUseBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateIgnoreBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateThrowBeforeUseAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateUseBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateIgnoreBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateThrowBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateUseBeforeThrowAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateIgnoreBeforeThrowAfter).HasDefaultValue("Rabbit");
                        b.Property(e => e.OnUpdateThrowBeforeThrowAfter).HasDefaultValue("Rabbit");
                    });

                modelBuilder.Entity<WithBackingFields>(
                    b =>
                    {
                        b.Property(e => e.NullableAsNonNullable).HasComputedColumnSql("1");
                        b.Property(e => e.NonNullableAsNullable).HasComputedColumnSql("1");
                    });

                modelBuilder.Entity<WithNullableBackingFields>(
                    b =>
                    {
                        b.Property(e => e.NullableBackedBool).HasDefaultValue(true);
                        b.Property(e => e.NullableBackedInt).HasDefaultValue(-1);
                    });

                base.OnModelCreating(modelBuilder, context);
            }
        }
    }
}
