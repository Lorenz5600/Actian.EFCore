using System;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class DataAnnotationActianTest : DataAnnotationTestBase<DataAnnotationActianTest.DataAnnotationActianFixture>
    {
        protected DataAnnotationActianTest(DataAnnotationActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        [ActianTodo]
        public override void Explicit_configuration_on_derived_type_overrides_annotation_on_unmapped_base_type()
        {
            base.Explicit_configuration_on_derived_type_overrides_annotation_on_unmapped_base_type();
        }

        [ActianTodo]
        public override void Explicit_configuration_on_derived_type_overrides_annotation_on_mapped_base_type()
        {
            base.Explicit_configuration_on_derived_type_overrides_annotation_on_mapped_base_type();
        }

        [ActianTodo]
        public override void Explicit_configuration_on_derived_type_or_base_type_is_last_one_wins()
        {
            base.Explicit_configuration_on_derived_type_or_base_type_is_last_one_wins();
        }

        [ActianTodo]
        public override void Duplicate_column_order_is_ignored()
        {
            base.Duplicate_column_order_is_ignored();
        }

        [ActianTodo]
        public override ModelBuilder Non_public_annotations_are_enabled()
        {
            var modelBuilder = base.Non_public_annotations_are_enabled();
            var property = GetProperty<PrivateMemberAnnotationClass>(modelBuilder, "PersonFirstName");
            Assert.Equal("dsdsd", property.GetColumnName());
            Assert.Equal("nvarchar(128)", property.GetColumnType());
            return modelBuilder;
        }

        [ActianTodo]
        public override ModelBuilder Field_annotations_are_enabled()
        {
            var modelBuilder = base.Field_annotations_are_enabled();
            var property = GetProperty<FieldAnnotationClass>(modelBuilder, "_personFirstName");
            Assert.Equal("dsdsd", property.GetColumnName());
            Assert.Equal("nvarchar(128)", property.GetColumnType());
            return modelBuilder;
        }

        [ActianTodo]
        public override void NotMapped_should_propagate_down_inheritance_hierarchy()
        {
            base.NotMapped_should_propagate_down_inheritance_hierarchy();
        }

        [ActianTodo]
        public override void NotMapped_on_base_class_property_ignores_it()
        {
            base.NotMapped_on_base_class_property_ignores_it();
        }

        [ActianTodo]
        public override void NotMapped_on_base_class_property_and_overridden_property_ignores_them()
        {
            base.NotMapped_on_base_class_property_and_overridden_property_ignores_them();
        }

        [ActianTodo]
        public override void NotMapped_on_base_class_property_discovered_through_navigation_ignores_it()
        {
            base.NotMapped_on_base_class_property_discovered_through_navigation_ignores_it();
        }

        [ActianTodo]
        public override void NotMapped_on_overridden_property_is_ignored()
        {
            base.NotMapped_on_overridden_property_is_ignored();
        }

        [ActianTodo]
        public override void NotMapped_on_unmapped_derived_property_ignores_it()
        {
            base.NotMapped_on_unmapped_derived_property_ignores_it();
        }

        [ActianTodo]
        public override void NotMapped_on_abstract_base_class_property_ignores_it()
        {
            base.NotMapped_on_abstract_base_class_property_ignores_it();
        }

        [ActianTodo]
        public override void NotMapped_on_unmapped_base_class_property_and_overridden_property_ignores_it()
        {
            base.NotMapped_on_unmapped_base_class_property_and_overridden_property_ignores_it();
        }

        [ActianTodo]
        public override void NotMapped_on_unmapped_base_class_property_ignores_it()
        {
            base.NotMapped_on_unmapped_base_class_property_ignores_it();
        }

        [ActianTodo]
        public override void NotMapped_on_new_property_with_same_name_as_in_unmapped_base_class_ignores_it()
        {
            base.NotMapped_on_new_property_with_same_name_as_in_unmapped_base_class_ignores_it();
        }

        [ActianTodo]
        public override void StringLength_with_value_takes_precedence_over_MaxLength()
        {
            base.StringLength_with_value_takes_precedence_over_MaxLength();
        }

        [ActianTodo]
        public override void MaxLength_with_length_takes_precedence_over_StringLength()
        {
            base.MaxLength_with_length_takes_precedence_over_StringLength();
        }

        [ActianTodo]
        public override ModelBuilder Default_length_for_key_string_column()
        {
            return base.Default_length_for_key_string_column();
        }

        [ActianTodo]
        public override ModelBuilder Key_and_column_work_together()
        {
            var modelBuilder = base.Key_and_column_work_together();
            var relational = GetProperty<ColumnKeyAnnotationClass1>(modelBuilder, "PersonFirstName");
            Assert.Equal("dsdsd", relational.GetColumnName());
            Assert.Equal("nvarchar(128)", relational.GetColumnType());
            return modelBuilder;
        }

        [ActianTodo]
        public override ModelBuilder Key_and_MaxLength_64_produce_nvarchar_64()
        {
            var modelBuilder = base.Key_and_MaxLength_64_produce_nvarchar_64();
            var property = GetProperty<ColumnKeyAnnotationClass2>(modelBuilder, "PersonFirstName");
            var storeType = property.GetRelationalTypeMapping().StoreType;
            Assert.Equal("nvarchar(64)", storeType);
            return modelBuilder;
        }

        [ActianTodo]
        public override void Key_from_base_type_is_recognized()
        {
            base.Key_from_base_type_is_recognized();
        }

        [ActianTodo]
        public override void Key_from_base_type_is_recognized_if_base_discovered_first()
        {
            base.Key_from_base_type_is_recognized_if_base_discovered_first();
        }

        [ActianTodo]
        public override void Key_from_base_type_is_recognized_if_discovered_through_relationship()
        {
            base.Key_from_base_type_is_recognized_if_discovered_through_relationship();
        }

        [ActianTodo]
        public override void Key_on_nav_prop_is_ignored()
        {
            base.Key_on_nav_prop_is_ignored();
        }

        [ActianTodo]
        public override ModelBuilder Key_property_is_not_used_for_FK_when_set_by_annotation()
        {
            return base.Key_property_is_not_used_for_FK_when_set_by_annotation();
        }

        [ActianTodo]
        public override ModelBuilder Key_specified_on_multiple_properties_can_be_overridden()
        {
            return base.Key_specified_on_multiple_properties_can_be_overridden();
        }

        [ActianTodo]
        public override ModelBuilder DatabaseGeneratedOption_configures_the_property_correctly()
        {
            var modelBuilder = base.DatabaseGeneratedOption_configures_the_property_correctly();
            var identity = modelBuilder.Model.FindEntityType(typeof(GeneratedEntity)).FindProperty(nameof(GeneratedEntity.Identity));
            Assert.Equal(ActianValueGenerationStrategy.IdentityColumn, identity.GetValueGenerationStrategy());
            return modelBuilder;
        }

        [ActianTodo]
        public override ModelBuilder DatabaseGeneratedOption_Identity_does_not_throw_on_noninteger_properties()
        {
            var modelBuilder = base.DatabaseGeneratedOption_Identity_does_not_throw_on_noninteger_properties();
            var entity = modelBuilder.Model.FindEntityType(typeof(GeneratedEntityNonInteger));
            var stringProperty = entity.FindProperty(nameof(GeneratedEntityNonInteger.String));
            Assert.Equal(ActianValueGenerationStrategy.None, stringProperty.GetValueGenerationStrategy());
            var dateTimeProperty = entity.FindProperty(nameof(GeneratedEntityNonInteger.DateTime));
            Assert.Equal(ActianValueGenerationStrategy.None, dateTimeProperty.GetValueGenerationStrategy());
            var guidProperty = entity.FindProperty(nameof(GeneratedEntityNonInteger.Guid));
            Assert.Equal(ActianValueGenerationStrategy.None, guidProperty.GetValueGenerationStrategy());
            return modelBuilder;
        }

        [ActianTodo]
        public override ModelBuilder Timestamp_takes_precedence_over_MaxLength()
        {
            var modelBuilder = base.Timestamp_takes_precedence_over_MaxLength();
            var property = GetProperty<TimestampAndMaxlen>(modelBuilder, "MaxTimestamp");
            var storeType = property.GetRelationalTypeMapping().StoreType;
            Assert.Equal("rowversion", storeType);
            return modelBuilder;
        }

        [ActianTodo]
        public override void Annotation_in_derived_class_when_base_class_processed_after_derived_class()
        {
            base.Annotation_in_derived_class_when_base_class_processed_after_derived_class();
        }

        [ActianTodo]
        public override void Required_and_ForeignKey_to_Required()
        {
            base.Required_and_ForeignKey_to_Required();
        }

        [ActianTodo]
        public override void Required_to_Required_and_ForeignKey()
        {
            base.Required_to_Required_and_ForeignKey();
        }

        [ActianTodo]
        public override void Required_and_ForeignKey_to_Required_and_ForeignKey()
        {
            base.Required_and_ForeignKey_to_Required_and_ForeignKey();
        }

        [ActianTodo]
        public override void Required_and_ForeignKey_to_Required_and_ForeignKey_can_be_overridden()
        {
            base.Required_and_ForeignKey_to_Required_and_ForeignKey_can_be_overridden();
        }

        [ActianTodo]
        public override void ForeignKey_to_nothing()
        {
            base.ForeignKey_to_nothing();
        }

        [ActianTodo]
        public override void Required_and_ForeignKey_to_nothing()
        {
            base.Required_and_ForeignKey_to_nothing();
        }

        [ActianTodo]
        public override void Nothing_to_ForeignKey()
        {
            base.Nothing_to_ForeignKey();
        }

        [ActianTodo]
        public override void Nothing_to_Required_and_ForeignKey()
        {
            base.Nothing_to_Required_and_ForeignKey();
        }

        [ActianTodo]
        public override void ForeignKey_to_ForeignKey()
        {
            base.ForeignKey_to_ForeignKey();
        }

        [ActianTodo]
        public override void ForeignKey_to_ForeignKey_same_name()
        {
            base.ForeignKey_to_ForeignKey_same_name();
        }

        [ActianTodo]
        public override void ForeignKey_to_ForeignKey_same_name_one_shadow()
        {
            base.ForeignKey_to_ForeignKey_same_name_one_shadow();
        }

        [ActianTodo]
        public override void Shared_ForeignKey_to_different_principals()
        {
            base.Shared_ForeignKey_to_different_principals();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_configures_relationships_when_inverse_on_derived()
        {
            base.ForeignKeyAttribute_configures_relationships_when_inverse_on_derived();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_configures_two_self_referencing_relationships()
        {
            base.ForeignKeyAttribute_configures_two_self_referencing_relationships();
        }

        [ActianTodo]
        public override ModelBuilder TableNameAttribute_affects_table_name_in_TPH()
        {
            var modelBuilder = base.TableNameAttribute_affects_table_name_in_TPH();
            Assert.Equal("A", modelBuilder.Model.FindEntityType(typeof(TNAttrBase)).GetTableName());
            return modelBuilder;
        }

        [ActianTodo]
        public override void ConcurrencyCheckAttribute_throws_if_value_in_database_changed()
        {
            base.ConcurrencyCheckAttribute_throws_if_value_in_database_changed();
            AssertSql(
                @"
                    SELECT FIRST 1 ""s"".""UniqueNo"", ""s"".""MaxLengthProperty"", ""s"".""Name"", ""s"".""RowVersion"", ""t"".""UniqueNo"", ""t"".""AdditionalDetails_Name"", ""t0"".""UniqueNo"", ""t0"".""Details_Name""
                    FROM ""Sample"" AS ""s""
                    LEFT JOIN (
                        SELECT ""s0"".""UniqueNo"", ""s0"".""AdditionalDetails_Name""
                        FROM ""Sample"" AS ""s0""
                        WHERE ""s0"".""AdditionalDetails_Name"" IS NOT NULL
                    ) AS ""t"" ON ""s"".""UniqueNo"" = ""t"".""UniqueNo""
                    LEFT JOIN (
                        SELECT ""s1"".""UniqueNo"", ""s1"".""Details_Name""
                        FROM ""Sample"" AS ""s1""
                        WHERE ""s1"".""Details_Name"" IS NOT NULL
                    ) AS ""t0"" ON ""s"".""UniqueNo"" = ""t0"".""UniqueNo""
                    WHERE ""s"".""UniqueNo"" = 1
                ",
                @"
                    SELECT FIRST 1 ""s"".""UniqueNo"", ""s"".""MaxLengthProperty"", ""s"".""Name"", ""s"".""RowVersion"", ""t"".""UniqueNo"", ""t"".""AdditionalDetails_Name"", ""t0"".""UniqueNo"", ""t0"".""Details_Name""
                    FROM ""Sample"" AS ""s""
                    LEFT JOIN (
                        SELECT ""s0"".""UniqueNo"", ""s0"".""AdditionalDetails_Name""
                        FROM ""Sample"" AS ""s0""
                        WHERE ""s0"".""AdditionalDetails_Name"" IS NOT NULL
                    ) AS ""t"" ON ""s"".""UniqueNo"" = ""t"".""UniqueNo""
                    LEFT JOIN (
                        SELECT ""s1"".""UniqueNo"", ""s1"".""Details_Name""
                        FROM ""Sample"" AS ""s1""
                        WHERE ""s1"".""Details_Name"" IS NOT NULL
                    ) AS ""t0"" ON ""s"".""UniqueNo"" = ""t0"".""UniqueNo""
                    WHERE ""s"".""UniqueNo"" = 1
                ",
                @"
                    @p2='1'
                    @p0='ModifiedData' (Nullable = false) (Size = 4000)
                    @p1='00000000-0000-0000-0003-000000000001'
                    @p3='00000001-0000-0000-0000-000000000001'
                    
                    SET NOCOUNT ON;
                    UPDATE ""Sample"" SET ""Name"" = @p0, ""RowVersion"" = @p1
                    WHERE ""UniqueNo"" = @p2 AND ""RowVersion"" = @p3;
                    SELECT @@ROWCOUNT;
                ",
                @"
                    @p2='1'
                    @p0='ChangedData' (Nullable = false) (Size = 4000)
                    @p1='00000000-0000-0000-0002-000000000001'
                    @p3='00000001-0000-0000-0000-000000000001'
                    
                    SET NOCOUNT ON;
                    UPDATE ""Sample"" SET ""Name"" = @p0, ""RowVersion"" = @p1
                    WHERE ""UniqueNo"" = @p2 AND ""RowVersion"" = @p3;
                    SELECT @@ROWCOUNT;
                "
            );
        }

        [ActianTodo]
        public override void DatabaseGeneratedAttribute_autogenerates_values_when_set_to_identity()
        {
            base.DatabaseGeneratedAttribute_autogenerates_values_when_set_to_identity();
            AssertSql(@"
                @p0=NULL (Size = 10)
                @p1='Third' (Nullable = false) (Size = 4000)
                @p2='00000000-0000-0000-0000-000000000003'
                @p3='Third Additional Name' (Size = 4000)
                @p4='Third Name' (Size = 4000)
                
                SET NOCOUNT ON;
                INSERT INTO ""Sample"" (""MaxLengthProperty"", ""Name"", ""RowVersion"", ""AdditionalDetails_Name"", ""Details_Name"")
                VALUES (@p0, @p1, @p2, @p3, @p4);
                SELECT ""UniqueNo""
                FROM ""Sample""
                WHERE @@ROWCOUNT = 1 AND ""UniqueNo"" = scope_identity();
            ");
        }

        [ActianTodo]
        public override void MaxLengthAttribute_throws_while_inserting_value_longer_than_max_length()
        {
            base.MaxLengthAttribute_throws_while_inserting_value_longer_than_max_length();
            AssertSql(
                @"
                    @p0='Short' (Size = 10)
                    @p1='ValidString' (Nullable = false) (Size = 4000)
                    @p2='00000000-0000-0000-0000-000000000001'
                    @p3='Third Additional Name' (Size = 4000)
                    @p4='Third Name' (Size = 4000)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""Sample"" (""MaxLengthProperty"", ""Name"", ""RowVersion"", ""AdditionalDetails_Name"", ""Details_Name"")
                    VALUES (@p0, @p1, @p2, @p3, @p4);
                    SELECT ""UniqueNo""
                    FROM ""Sample""
                    WHERE @@ROWCOUNT = 1 AND ""UniqueNo"" = scope_identity();
                ",
                @"
                    @p0='VeryVeryVeryVeryVeryVeryLongString' (Size = -1)
                    @p1='ValidString' (Nullable = false) (Size = 4000)
                    @p2='00000000-0000-0000-0000-000000000002'
                    @p3='Third Additional Name' (Size = 4000)
                    @p4='Third Name' (Size = 4000)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""Sample"" (""MaxLengthProperty"", ""Name"", ""RowVersion"", ""AdditionalDetails_Name"", ""Details_Name"")
                    VALUES (@p0, @p1, @p2, @p3, @p4);
                    SELECT ""UniqueNo""
                    FROM ""Sample""
                    WHERE @@ROWCOUNT = 1 AND ""UniqueNo"" = scope_identity();
                "
            );
        }

        [ActianTodo]
        public override void NotMappedAttribute_ignores_entityType()
        {
            base.NotMappedAttribute_ignores_entityType();
        }

        [ActianTodo]
        public override void NotMappedAttribute_ignores_navigation()
        {
            base.NotMappedAttribute_ignores_navigation();
        }

        [ActianTodo]
        public override void NotMappedAttribute_ignores_property()
        {
            base.NotMappedAttribute_ignores_property();
        }

        [ActianTodo]
        public override void NotMappedAttribute_ignores_explicit_interface_implementation_property()
        {
            base.NotMappedAttribute_ignores_explicit_interface_implementation_property();
        }

        [ActianTodo]
        public override void NotMappedAttribute_removes_ambiguity_in_relationship_building()
        {
            base.NotMappedAttribute_removes_ambiguity_in_relationship_building();
        }

        [ActianTodo]
        public override void NotMappedAttribute_removes_ambiguity_in_relationship_building_with_base()
        {
            base.NotMappedAttribute_removes_ambiguity_in_relationship_building_with_base();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_removes_ambiguity()
        {
            base.InversePropertyAttribute_removes_ambiguity();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_removes_ambiguity_with_base_type()
        {
            base.InversePropertyAttribute_removes_ambiguity_with_base_type();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_removes_ambiguity_with_base_type_ignored()
        {
            base.InversePropertyAttribute_removes_ambiguity_with_base_type_ignored();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_from_ignored_base_causes_ambiguity()
        {
            base.InversePropertyAttribute_from_ignored_base_causes_ambiguity();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_from_ignored_base_can_be_ignored_to_remove_ambiguity()
        {
            base.InversePropertyAttribute_from_ignored_base_can_be_ignored_to_remove_ambiguity();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_removes_ambiguity_from_the_ambiguous_end()
        {
            base.InversePropertyAttribute_removes_ambiguity_from_the_ambiguous_end();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_removes_ambiguity_when_combined_with_other_attributes()
        {
            base.InversePropertyAttribute_removes_ambiguity_when_combined_with_other_attributes();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_removes_ambiguity_with_base_type_bidirectional()
        {
            base.InversePropertyAttribute_removes_ambiguity_with_base_type_bidirectional();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_is_noop_in_unambiguous_models()
        {
            base.InversePropertyAttribute_is_noop_in_unambiguous_models();
        }

        [ActianTodo]
        public override void InversePropertyAttribute_pointing_to_same_nav_on_base_causes_ambiguity()
        {
            base.InversePropertyAttribute_pointing_to_same_nav_on_base_causes_ambiguity();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_creates_two_relationships_if_applied_on_property_on_both_side()
        {
            base.ForeignKeyAttribute_creates_two_relationships_if_applied_on_property_on_both_side();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_creates_two_relationships_if_applied_on_navigations_on_both_sides_and_values_do_not_match()
        {
            base.ForeignKeyAttribute_creates_two_relationships_if_applied_on_navigations_on_both_sides_and_values_do_not_match();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_creates_two_relationships_if_applied_on_navigation_and_property_on_different_sides_and_values_do_not_match()
        {
            base.ForeignKeyAttribute_creates_two_relationships_if_applied_on_navigation_and_property_on_different_sides_and_values_do_not_match();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_throws_if_applied_on_property_on_both_side_but_navigations_are_connected_by_inverse_property()
        {
            base.ForeignKeyAttribute_throws_if_applied_on_property_on_both_side_but_navigations_are_connected_by_inverse_property();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_throws_if_applied_on_both_navigations_connected_by_inverse_property_but_values_do_not_match()
        {
            base.ForeignKeyAttribute_throws_if_applied_on_both_navigations_connected_by_inverse_property_but_values_do_not_match();
        }

        [ActianTodo]
        public override void ForeignKeyAttribute_throws_if_applied_on_two_relationships_targetting_the_same_property()
        {
            base.ForeignKeyAttribute_throws_if_applied_on_two_relationships_targetting_the_same_property();
        }

        [ActianTodo]
        public override void RequiredAttribute_for_navigation_throws_while_inserting_null_value()
        {
            base.RequiredAttribute_for_navigation_throws_while_inserting_null_value();
            AssertSql(
                @"
                    @p0=NULL (DbType = Int32)
                    @p1='1'
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""BookDetails"" (""AdditionalBookDetailsId"", ""AnotherBookId"")
                    VALUES (@p0, @p1);
                    SELECT ""Id""
                    FROM ""BookDetails""
                    WHERE @@ROWCOUNT = 1 AND ""Id"" = scope_identity();
                ",
                @"
                    @p0=NULL (DbType = Int32)
                    @p1=NULL (Nullable = false) (DbType = Int32)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""BookDetails"" (""AdditionalBookDetailsId"", ""AnotherBookId"")
                    VALUES (@p0, @p1);
                    SELECT ""Id""
                    FROM ""BookDetails""
                    WHERE @@ROWCOUNT = 1 AND ""Id"" = scope_identity();
                "
            );
        }

        [ActianTodo]
        public override void RequiredAttribute_does_nothing_when_specified_on_nav_to_dependent_per_convention()
        {
            base.RequiredAttribute_does_nothing_when_specified_on_nav_to_dependent_per_convention();
        }

        [ActianTodo]
        public override void RequiredAttribute_for_property_throws_while_inserting_null_value()
        {
            base.RequiredAttribute_for_property_throws_while_inserting_null_value();
            AssertSql(
                @"
                    @p0=NULL (Size = 10)
                    @p1='ValidString' (Nullable = false) (Size = 4000)
                    @p2='00000000-0000-0000-0000-000000000001'
                    @p3='Two' (Size = 4000)
                    @p4='One' (Size = 4000)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""Sample"" (""MaxLengthProperty"", ""Name"", ""RowVersion"", ""AdditionalDetails_Name"", ""Details_Name"")
                    VALUES (@p0, @p1, @p2, @p3, @p4);
                    SELECT ""UniqueNo""
                    FROM ""Sample""
                    WHERE @@ROWCOUNT = 1 AND ""UniqueNo"" = scope_identity();
                ",
                @"
                    @p0=NULL (Size = 10)
                    @p1=NULL (Nullable = false) (Size = 4000)
                    @p2='00000000-0000-0000-0000-000000000002'
                    @p3='Two' (Size = 4000)
                    @p4='One' (Size = 4000)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""Sample"" (""MaxLengthProperty"", ""Name"", ""RowVersion"", ""AdditionalDetails_Name"", ""Details_Name"")
                    VALUES (@p0, @p1, @p2, @p3, @p4);
                    SELECT ""UniqueNo""
                    FROM ""Sample""
                    WHERE @@ROWCOUNT = 1 AND ""UniqueNo"" = scope_identity();
                "
            );
        }

        [ActianTodo]
        public override void StringLengthAttribute_throws_while_inserting_value_longer_than_max_length()
        {
            base.StringLengthAttribute_throws_while_inserting_value_longer_than_max_length();
            AssertSql(
                @"
                    @p0='ValidString' (Size = 16)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""Two"" (""Data"")
                    VALUES (@p0);
                    SELECT ""Id"", ""Timestamp""
                    FROM ""Two""
                    WHERE @@ROWCOUNT = 1 AND ""Id"" = scope_identity();
                ",
                @"
                    @p0='ValidButLongString' (Size = -1)
                    
                    SET NOCOUNT ON;
                    INSERT INTO ""Two"" (""Data"")
                    VALUES (@p0);
                    SELECT ""Id"", ""Timestamp""
                    FROM ""Two""
                    WHERE @@ROWCOUNT = 1 AND ""Id"" = scope_identity();
                "
            );
        }

        [ActianTodo]
        public override void TimestampAttribute_throws_if_value_in_database_changed()
        {
            base.TimestampAttribute_throws_if_value_in_database_changed();
        }

        [ActianTodo]
        public override void OwnedEntityTypeAttribute_configures_one_reference_as_owned()
        {
            base.OwnedEntityTypeAttribute_configures_one_reference_as_owned();
        }

        [ActianTodo]
        public override void OwnedEntityTypeAttribute_configures_all_references_as_owned()
        {
            base.OwnedEntityTypeAttribute_configures_all_references_as_owned();
        }

        private static readonly string _eol = Environment.NewLine;

        public class DataAnnotationActianFixture : DataAnnotationFixtureBase, IActianSqlFixture
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
        }
    }
}
