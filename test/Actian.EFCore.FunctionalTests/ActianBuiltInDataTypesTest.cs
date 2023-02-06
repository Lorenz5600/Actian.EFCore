using System;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore
{
    //[ActianIncludeTodos]
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

        public override Task Can_filter_projection_with_captured_enum_variable(bool async)
        {
            return base.Can_filter_projection_with_captured_enum_variable(async);
        }

        public override Task Can_filter_projection_with_inline_enum_variable(bool async)
        {
            return base.Can_filter_projection_with_inline_enum_variable(async);
        }

        [ActianSkip(LongRunningAndCrashesDataAccessServer)]
        public override void Can_perform_query_with_max_length()
        {
            base.Can_perform_query_with_max_length();
        }

        [ActianSkip(LongRunning)]
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

        [ActianSkip(LongRunningAndCrashesDataAccessServer)]
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
    }
}
