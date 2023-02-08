﻿using System;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore
{
    public class CustomConvertersActianTest : CustomConvertersTestBase<CustomConvertersActianTest.CustomConvertersActianFixture>, IDisposable
    {
        public CustomConvertersActianTest(CustomConvertersActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public void Dispose()
        {
            Helpers.LogSql();
        }


        public override Task Can_filter_projection_with_captured_enum_variable(bool async)
        {
            return base.Can_filter_projection_with_captured_enum_variable(async);
        }


        public override Task Can_filter_projection_with_inline_enum_variable(bool async)
        {
            return base.Can_filter_projection_with_inline_enum_variable(async);
        }

        [ActianTodo]
        public override void Can_perform_query_with_max_length()
        {
            base.Can_perform_query_with_max_length();
        }

        [ActianSkip(LongRunning)]
        public override void Can_perform_query_with_ansi_strings_test()
        {
            base.Can_perform_query_with_ansi_strings_test();
        }


        public override void Can_query_using_any_data_type()
        {
            base.Can_query_using_any_data_type();
        }


        public override void Can_query_using_any_data_type_shadow()
        {
            base.Can_query_using_any_data_type_shadow();
        }


        public override void Can_query_using_any_nullable_data_type()
        {
            base.Can_query_using_any_nullable_data_type();
        }

        [ActianTodo]
        public override void Can_query_using_any_data_type_nullable_shadow()
        {
            base.Can_query_using_any_data_type_nullable_shadow();
        }


        public override void Can_query_using_any_nullable_data_type_as_literal()
        {
            base.Can_query_using_any_nullable_data_type_as_literal();
        }


        public override void Can_query_with_null_parameters_using_any_nullable_data_type()
        {
            base.Can_query_with_null_parameters_using_any_nullable_data_type();
        }


        public override void Can_insert_and_read_back_all_non_nullable_data_types()
        {
            base.Can_insert_and_read_back_all_non_nullable_data_types();
        }


        public override void Can_insert_and_read_with_max_length_set()
        {
            base.Can_insert_and_read_with_max_length_set();
        }


        public override void Can_insert_and_read_back_with_binary_key()
        {
            base.Can_insert_and_read_back_with_binary_key();
        }


        public override void Can_insert_and_read_back_with_null_binary_foreign_key()
        {
            base.Can_insert_and_read_back_with_null_binary_foreign_key();
        }


        public override void Can_insert_and_read_back_with_string_key()
        {
            base.Can_insert_and_read_back_with_string_key();
        }


        public override void Can_insert_and_read_back_with_null_string_foreign_key()
        {
            base.Can_insert_and_read_back_with_null_string_foreign_key();
        }


        public override void Can_insert_and_read_back_all_nullable_data_types_with_values_set_to_null()
        {
            base.Can_insert_and_read_back_all_nullable_data_types_with_values_set_to_null();
        }


        public override void Can_insert_and_read_back_all_nullable_data_types_with_values_set_to_non_null()
        {
            base.Can_insert_and_read_back_all_nullable_data_types_with_values_set_to_non_null();
        }

        [ActianTodo]
        public override void Can_insert_and_read_back_object_backed_data_types()
        {
            base.Can_insert_and_read_back_object_backed_data_types();
        }

        [ActianTodo]
        public override void Can_insert_and_read_back_nullable_backed_data_types()
        {
            base.Can_insert_and_read_back_nullable_backed_data_types();
        }

        [ActianTodo]
        public override void Can_insert_and_read_back_non_nullable_backed_data_types()
        {
            base.Can_insert_and_read_back_non_nullable_backed_data_types();
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Can_read_back_mapped_enum_from_collection_first_or_default()
        {
            base.Can_read_back_mapped_enum_from_collection_first_or_default();
        }


        public override void Can_read_back_bool_mapped_as_int_through_navigation()
        {
            base.Can_read_back_bool_mapped_as_int_through_navigation();
        }


        public override void Can_query_and_update_with_nullable_converter_on_unique_index()
        {
            base.Can_query_and_update_with_nullable_converter_on_unique_index();
        }


        public override void Can_query_and_update_with_nullable_converter_on_primary_key()
        {
            base.Can_query_and_update_with_nullable_converter_on_primary_key();
        }


        public override void Can_query_and_update_with_conversion_for_custom_type()
        {
            base.Can_query_and_update_with_conversion_for_custom_type();
        }


        public override void Can_query_and_update_with_conversion_for_custom_struct()
        {
            base.Can_query_and_update_with_conversion_for_custom_struct();
        }

        [ActianTodo]
        public override void Can_insert_and_read_back_with_case_insensitive_string_key()
        {
            base.Can_insert_and_read_back_with_case_insensitive_string_key();
        }


        public override void Can_insert_and_read_back_with_string_list()
        {
            base.Can_insert_and_read_back_with_string_list();
        }


        public override void Can_insert_and_query_struct_to_string_converter_for_pk()
        {
            base.Can_insert_and_query_struct_to_string_converter_for_pk();
        }


        public override Task Can_query_custom_type_not_mapped_by_default_equality(bool isAsync)
        {
            return base.Can_query_custom_type_not_mapped_by_default_equality(isAsync);
        }


        public override void Field_on_derived_type_retrieved_via_cast_applies_value_converter()
        {
            base.Field_on_derived_type_retrieved_via_cast_applies_value_converter();
        }


        public override void Value_conversion_is_appropriately_used_for_join_condition()
        {
            base.Value_conversion_is_appropriately_used_for_join_condition();
        }


        public override void Value_conversion_is_appropriately_used_for_left_join_condition()
        {
            base.Value_conversion_is_appropriately_used_for_left_join_condition();
        }


        public override void Where_bool_gets_converted_to_equality_when_value_conversion_is_used()
        {
            base.Where_bool_gets_converted_to_equality_when_value_conversion_is_used();
        }


        public override void Value_conversion_with_property_named_value()
        {
            base.Value_conversion_with_property_named_value();
        }


        public override void Collection_property_as_scalar()
        {
            base.Collection_property_as_scalar();
        }


        public virtual void Columns_have_expected_data_types()
        {
            var actual = ActianBuiltInDataTypesTest.QueryForColumnTypes(
                CreateContext(),
                nameof(ObjectBackedDataTypes), nameof(NullableBackedDataTypes), nameof(NonNullableBackedDataTypes)
            );

            const string expected = @"
                Animal.Id ---> [int] [Precision = 10 Scale = 0]
                AnimalDetails.AnimalId ---> [nullable int] [Precision = 10 Scale = 0]
                AnimalDetails.BoolField ---> [int] [Precision = 10 Scale = 0]
                AnimalDetails.Id ---> [int] [Precision = 10 Scale = 0]
                AnimalIdentification.AnimalId ---> [int] [Precision = 10 Scale = 0]
                AnimalIdentification.Id ---> [int] [Precision = 10 Scale = 0]
                AnimalIdentification.Method ---> [nvarchar] [MaxLength = 6]
                BinaryForeignKeyDataType.BinaryKeyDataTypeId ---> [nullable nvarchar] [MaxLength = 450]
                BinaryForeignKeyDataType.Id ---> [int] [Precision = 10 Scale = 0]
                BinaryKeyDataType.Ex ---> [nullable nvarchar] [MaxLength = -1]
                BinaryKeyDataType.Id ---> [nvarchar] [MaxLength = 450]
                BuiltInDataTypes.Enum16 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.Enum32 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.Enum64 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.Enum8 ---> [nchar] [MaxLength = 17]
                BuiltInDataTypes.EnumS8 ---> [varchar] [MaxLength = -1]
                BuiltInDataTypes.EnumU16 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.EnumU32 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.EnumU64 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.Id ---> [int] [Precision = 10 Scale = 0]
                BuiltInDataTypes.PartitionId ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestBoolean ---> [nvarchar] [MaxLength = 1]
                BuiltInDataTypes.TestByte ---> [int] [Precision = 10 Scale = 0]
                BuiltInDataTypes.TestCharacter ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestDateTime ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestDateTimeOffset ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestDecimal ---> [varbinary] [MaxLength = 16]
                BuiltInDataTypes.TestDouble ---> [decimal] [Precision = 38 Scale = 17]
                BuiltInDataTypes.TestInt16 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestInt32 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestInt64 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestSignedByte ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestSingle ---> [decimal] [Precision = 38 Scale = 17]
                BuiltInDataTypes.TestTimeSpan ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypes.TestUnsignedInt16 ---> [decimal] [Precision = 20 Scale = 0]
                BuiltInDataTypes.TestUnsignedInt32 ---> [decimal] [Precision = 20 Scale = 0]
                BuiltInDataTypes.TestUnsignedInt64 ---> [decimal] [Precision = 20 Scale = 0]
                BuiltInDataTypesShadow.Enum16 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.Enum32 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.Enum64 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.Enum8 ---> [nvarchar] [MaxLength = -1]
                BuiltInDataTypesShadow.EnumS8 ---> [nvarchar] [MaxLength = -1]
                BuiltInDataTypesShadow.EnumU16 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.EnumU32 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.EnumU64 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.Id ---> [int] [Precision = 10 Scale = 0]
                BuiltInDataTypesShadow.PartitionId ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestBoolean ---> [nvarchar] [MaxLength = 1]
                BuiltInDataTypesShadow.TestByte ---> [int] [Precision = 10 Scale = 0]
                BuiltInDataTypesShadow.TestCharacter ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestDateTime ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestDateTimeOffset ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestDecimal ---> [varbinary] [MaxLength = 16]
                BuiltInDataTypesShadow.TestDouble ---> [decimal] [Precision = 38 Scale = 17]
                BuiltInDataTypesShadow.TestInt16 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestInt32 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestInt64 ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestSignedByte ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestSingle ---> [decimal] [Precision = 38 Scale = 17]
                BuiltInDataTypesShadow.TestTimeSpan ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInDataTypesShadow.TestUnsignedInt16 ---> [decimal] [Precision = 20 Scale = 0]
                BuiltInDataTypesShadow.TestUnsignedInt32 ---> [decimal] [Precision = 20 Scale = 0]
                BuiltInDataTypesShadow.TestUnsignedInt64 ---> [decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypes.Enum16 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.Enum32 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.Enum64 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.Enum8 ---> [nullable nvarchar] [MaxLength = -1]
                BuiltInNullableDataTypes.EnumS8 ---> [nullable nvarchar] [MaxLength = -1]
                BuiltInNullableDataTypes.EnumU16 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.EnumU32 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.EnumU64 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.Id ---> [int] [Precision = 10 Scale = 0]
                BuiltInNullableDataTypes.PartitionId ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestByteArray ---> [nullable varbinary] [MaxLength = -1]
                BuiltInNullableDataTypes.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 1]
                BuiltInNullableDataTypes.TestNullableByte ---> [nullable int] [Precision = 10 Scale = 0]
                BuiltInNullableDataTypes.TestNullableCharacter ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableDateTime ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableDateTimeOffset ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
                BuiltInNullableDataTypes.TestNullableDouble ---> [nullable decimal] [Precision = 38 Scale = 17]
                BuiltInNullableDataTypes.TestNullableInt16 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableInt32 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableInt64 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableSignedByte ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableSingle ---> [nullable decimal] [Precision = 38 Scale = 17]
                BuiltInNullableDataTypes.TestNullableTimeSpan ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypes.TestNullableUnsignedInt16 ---> [nullable decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypes.TestNullableUnsignedInt32 ---> [nullable decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypes.TestNullableUnsignedInt64 ---> [nullable decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypes.TestString ---> [nullable nvarchar] [MaxLength = -1]
                BuiltInNullableDataTypesShadow.Enum16 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.Enum32 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.Enum64 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.Enum8 ---> [nullable nvarchar] [MaxLength = -1]
                BuiltInNullableDataTypesShadow.EnumS8 ---> [nullable nvarchar] [MaxLength = -1]
                BuiltInNullableDataTypesShadow.EnumU16 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.EnumU32 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.EnumU64 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.Id ---> [int] [Precision = 10 Scale = 0]
                BuiltInNullableDataTypesShadow.PartitionId ---> [bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestByteArray ---> [nullable varbinary] [MaxLength = -1]
                BuiltInNullableDataTypesShadow.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 1]
                BuiltInNullableDataTypesShadow.TestNullableByte ---> [nullable int] [Precision = 10 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableCharacter ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableDateTime ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableDateTimeOffset ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
                BuiltInNullableDataTypesShadow.TestNullableDouble ---> [nullable decimal] [Precision = 38 Scale = 17]
                BuiltInNullableDataTypesShadow.TestNullableInt16 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableInt32 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableInt64 ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableSignedByte ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableSingle ---> [nullable decimal] [Precision = 38 Scale = 17]
                BuiltInNullableDataTypesShadow.TestNullableTimeSpan ---> [nullable bigint] [Precision = 19 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableUnsignedInt16 ---> [nullable decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableUnsignedInt32 ---> [nullable decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypesShadow.TestNullableUnsignedInt64 ---> [nullable decimal] [Precision = 20 Scale = 0]
                BuiltInNullableDataTypesShadow.TestString ---> [nullable nvarchar] [MaxLength = -1]
                EmailTemplate.Id ---> [uniqueidentifier]
                EmailTemplate.TemplateType ---> [int] [Precision = 10 Scale = 0]
                MaxLengthDataTypes.ByteArray5 ---> [nullable nvarchar] [MaxLength = 8]
                MaxLengthDataTypes.ByteArray9000 ---> [nullable nvarchar] [MaxLength = -1]
                MaxLengthDataTypes.Id ---> [int] [Precision = 10 Scale = 0]
                MaxLengthDataTypes.String3 ---> [nullable varbinary] [MaxLength = 3]
                MaxLengthDataTypes.String9000 ---> [nullable varbinary] [MaxLength = -1]
                StringForeignKeyDataType.Id ---> [int] [Precision = 10 Scale = 0]
                StringForeignKeyDataType.StringKeyDataTypeId ---> [nullable varbinary] [MaxLength = 900]
                StringKeyDataType.Id ---> [varbinary] [MaxLength = 900]
                UnicodeDataTypes.Id ---> [int] [Precision = 10 Scale = 0]
                UnicodeDataTypes.StringAnsi ---> [nullable varchar] [MaxLength = -1]
                UnicodeDataTypes.StringAnsi3 ---> [nullable varchar] [MaxLength = 3]
                UnicodeDataTypes.StringAnsi9000 ---> [nullable varchar] [MaxLength = -1]
                UnicodeDataTypes.StringDefault ---> [nullable nvarchar] [MaxLength = -1]
                UnicodeDataTypes.StringUnicode ---> [nullable nvarchar] [MaxLength = -1]
            ";

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        public class CustomConvertersActianFixture : CustomConvertersFixtureBase
        {
            public override bool StrictEquality => true;

            public override bool SupportsAnsi => true;

            public override bool SupportsUnicodeToAnsiConversion => true;

            public override bool SupportsLargeStringComparisons => true;

            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            public override bool SupportsBinaryKeys => true;

            public override bool SupportsDecimalComparisons => true;

            public override DateTime DefaultDateTime => new DateTime();

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => base
                    .AddOptions(builder)
                    .ConfigureWarnings(
                        c => c.Log(ActianEventId.DecimalTypeDefaultWarning));

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<BuiltInDataTypes>().Property(e => e.TestBoolean).IsFixedLength();
            }
        }
    }
}
