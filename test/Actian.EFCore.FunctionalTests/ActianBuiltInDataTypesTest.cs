using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public partial class ActianBuiltInDataTypesTest : BuiltInDataTypesTestBase<ActianBuiltInDataTypesTest.ActianBuiltInDataTypesFixture>, IDisposable
    {
        private static readonly string _eol = Environment.NewLine;

        public ActianBuiltInDataTypesTest(ActianBuiltInDataTypesFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            foreach (var statement in Fixture.TestSqlLoggerFactory.SqlStatements)
            {
                testOutputHelper.WriteLine(statement);
            }
            Helpers = new ActianSqlFixtureHelpers(fixture.TestSqlLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public void Dispose()
        {
            Fixture.TestSqlLoggerFactory.Clear();
        }


        [ActianTodo]
        public void Can_insert_all_non_nullable_data_types()
        {
            //using (var context = CreateContext())
            //{
            //    context.Set<BuiltInDataTypes>().Add(
            //        new BuiltInDataTypes
            //        {
            //            Id = 1,
            //            PartitionId = 1,
            //            TestInt16 = -1234,
            //            TestInt32 = -123456789,
            //            TestInt64 = -1234567890123456789L,
            //            TestDouble = -1.23456789,
            //            TestDecimal = -1234567890.01M,
            //            TestDateTime = DateTime.Parse("01/01/2000 12:34:56"),
            //            TestDateTimeOffset = new DateTimeOffset(DateTime.Parse("01/01/2000 12:34:56"), TimeSpan.FromHours(-8.0)),
            //            TestTimeSpan = new TimeSpan(0, 10, 9, 8, 7),
            //            TestSingle = -1.234F,
            //            TestBoolean = true,
            //            TestByte = 255,
            //            TestUnsignedInt16 = 1234,
            //            TestUnsignedInt32 = 1234565789U,
            //            TestUnsignedInt64 = 1234567890123456789UL,
            //            TestCharacter = 'a',
            //            TestSignedByte = -128,
            //            Enum64 = Enum64.SomeValue,
            //            Enum32 = Enum32.SomeValue,
            //            Enum16 = Enum16.SomeValue,
            //            Enum8 = Enum8.SomeValue,
            //            EnumU64 = EnumU64.SomeValue,
            //            EnumU32 = EnumU32.SomeValue,
            //            EnumU16 = EnumU16.SomeValue,
            //            EnumS8 = EnumS8.SomeValue
            //        });

            //    context.SaveChanges();
            //    //Assert.Equal(1, context.SaveChanges());
            //}
        }



        public override async Task Can_filter_projection_with_captured_enum_variable(bool async)
        {
            await base.Can_filter_projection_with_captured_enum_variable(async);
        }

        public override async Task Can_filter_projection_with_inline_enum_variable(bool async)
        {
            await base.Can_filter_projection_with_inline_enum_variable(async);
        }

        [ActianSkip("Long running")]
        public override void Can_perform_query_with_max_length()
        {
            base.Can_perform_query_with_max_length();
        }

        [ActianSkip("Long running")]
        public override void Can_perform_query_with_ansi_strings_test()
        {
            base.Can_perform_query_with_ansi_strings_test();
        }

        [ActianTodo]
        public override void Can_query_using_any_data_type()
        {
            base.Can_query_using_any_data_type();
        }

        [ActianTodo]
        public override void Can_query_using_any_data_type_shadow()
        {
            base.Can_query_using_any_data_type_shadow();
        }

        [ActianTodo]
        public override void Can_query_using_any_nullable_data_type()
        {
            base.Can_query_using_any_nullable_data_type();
        }

        [ActianTodo]
        public override void Can_query_using_any_data_type_nullable_shadow()
        {
            base.Can_query_using_any_data_type_nullable_shadow();
        }

        [ActianTodo]
        public override void Can_query_using_any_nullable_data_type_as_literal()
        {
            base.Can_query_using_any_nullable_data_type_as_literal();
        }

        public override void Can_query_with_null_parameters_using_any_nullable_data_type()
        {
            base.Can_query_with_null_parameters_using_any_nullable_data_type();
        }

        [ActianTodo]
        public override void Can_insert_and_read_back_all_non_nullable_data_types()
        {
            base.Can_insert_and_read_back_all_non_nullable_data_types();
        }

        [ActianSkip("Long running")]
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

        [ActianTodo]
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

        [ActianTodo]
        public override void Can_read_back_mapped_enum_from_collection_first_or_default()
        {
            base.Can_read_back_mapped_enum_from_collection_first_or_default();
        }

        public override void Can_read_back_bool_mapped_as_int_through_navigation()
        {
            base.Can_read_back_bool_mapped_as_int_through_navigation();
        }

        //[ActianSkip("Skipped")]
        //public override void Can_compare_enum_to_constant()
        //{
        //    base.Can_compare_enum_to_constant();
        //}

        //[ActianSkip("Skipped")]
        //public override void Can_compare_enum_to_parameter()
        //{
        //    base.Can_compare_enum_to_parameter();
        //}

        public static string QueryForColumnTypes(DbContext context, params string[] tablesToIgnore)
        {
            const string query
                = @"SELECT
                        TABLE_NAME,
                        COLUMN_NAME,
                        DATA_TYPE,
                        IS_NULLABLE,
                        CHARACTER_MAXIMUM_LENGTH,
                        NUMERIC_PRECISION,
                        NUMERIC_SCALE,
                        DATETIME_PRECISION
                    FROM INFORMATION_SCHEMA.COLUMNS";

            var columns = new List<ColumnInfo>();

            using (context)
            {
                var connection = context.Database.GetDbConnection();

                var command = connection.CreateCommand();
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var columnInfo = new ColumnInfo
                        {
                            TableName = reader.GetString(0),
                            ColumnName = reader.GetString(1),
                            DataType = reader.GetString(2),
                            IsNullable = reader.IsDBNull(3) ? null : (bool?)(reader.GetString(3) == "YES"),
                            MaxLength = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            NumericPrecision = reader.IsDBNull(5) ? null : (int?)reader.GetByte(5),
                            NumericScale = reader.IsDBNull(6) ? null : (int?)reader.GetInt32(6),
                            DateTimePrecision = reader.IsDBNull(7) ? null : (int?)reader.GetInt16(7)
                        };

                        if (!tablesToIgnore.Contains(columnInfo.TableName))
                        {
                            columns.Add(columnInfo);
                        }
                    }
                }
            }

            var builder = new StringBuilder();

            foreach (var column in columns.OrderBy(e => e.TableName).ThenBy(e => e.ColumnName))
            {
                builder.Append(column.TableName);
                builder.Append(".");
                builder.Append(column.ColumnName);
                builder.Append(" ---> [");

                if (column.IsNullable == true)
                {
                    builder.Append("nullable ");
                }

                builder.Append(column.DataType);
                builder.Append("]");

                if (column.MaxLength.HasValue)
                {
                    builder.Append(" [MaxLength = ");
                    builder.Append(column.MaxLength);
                    builder.Append("]");
                }

                if (column.NumericPrecision.HasValue)
                {
                    builder.Append(" [Precision = ");
                    builder.Append(column.NumericPrecision);
                }

                if (column.DateTimePrecision.HasValue)
                {
                    builder.Append(" [Precision = ");
                    builder.Append(column.DateTimePrecision);
                }

                if (column.NumericScale.HasValue)
                {
                    builder.Append(" Scale = ");
                    builder.Append(column.NumericScale);
                }

                if (column.NumericPrecision.HasValue
                    || column.DateTimePrecision.HasValue
                    || column.NumericScale.HasValue)
                {
                    builder.Append("]");
                }

                builder.AppendLine();
            }

            var actual = builder.ToString();
            return actual;
        }
    }
}
