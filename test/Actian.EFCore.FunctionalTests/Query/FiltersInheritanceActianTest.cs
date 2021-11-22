using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class FiltersInheritanceActianTest : FiltersInheritanceTestBase<FiltersInheritanceActianFixture>
    {
        public FiltersInheritanceActianTest(FiltersInheritanceActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Can_use_of_type_animal()
        {
            base.Can_use_of_type_animal();
            AssertSql(@"
                SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE ""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)
                ORDER BY ""a"".""Species""
            ");
        }

        [ActianTodo]
        public override void Can_use_is_kiwi()
        {
            base.Can_use_is_kiwi();
            AssertSql(@"
                SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE (""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND (""a"".""Discriminator"" = N'Kiwi')
            ");
        }

        [ActianTodo]
        public override void Can_use_is_kiwi_with_other_predicate()
        {
            base.Can_use_is_kiwi_with_other_predicate();
            AssertSql(@"
                SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE (""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND ((""a"".""Discriminator"" = N'Kiwi') AND (""a"".""CountryId"" = 1))
            ");
        }

        [ActianTodo]
        public override void Can_use_is_kiwi_in_projection()
        {
            base.Can_use_is_kiwi_in_projection();
            AssertSql(@"
                SELECT CASE
                    WHEN ""a"".""Discriminator"" = N'Kiwi' THEN TRUE
                    ELSE FALSE
                END
                FROM ""Animal"" AS ""a""
                WHERE ""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)
            ");
        }

        [ActianTodo]
        public override void Can_use_of_type_bird()
        {
            base.Can_use_of_type_bird();
            AssertSql(@"
                SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE (""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND ""a"".""Discriminator"" IN (N'Eagle', N'Kiwi')
                ORDER BY ""a"".""Species""
            ");
        }

        [ActianTodo]
        public override void Can_use_of_type_bird_predicate()
        {
            base.Can_use_of_type_bird_predicate();
            AssertSql(@"
                SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE ((""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND (""a"".""CountryId"" = 1)) AND ""a"".""Discriminator"" IN (N'Eagle', N'Kiwi')
                ORDER BY ""a"".""Species""
            ");
        }

        [ActianTodo]
        public override void Can_use_of_type_bird_with_projection()
        {
            base.Can_use_of_type_bird_with_projection();
            AssertSql(@"
                SELECT ""a"".""EagleId""
                FROM ""Animal"" AS ""a""
                WHERE (""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND ""a"".""Discriminator"" IN (N'Eagle', N'Kiwi')
            ");
        }

        [ActianTodo]
        public override void Can_use_of_type_bird_first()
        {
            base.Can_use_of_type_bird_first();
            AssertSql(@"
                SELECT FIRST 1 ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""Group"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE (""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND ""a"".""Discriminator"" IN (N'Eagle', N'Kiwi')
                ORDER BY ""a"".""Species""
            ");
        }

        [ActianTodo]
        public override void Can_use_of_type_kiwi()
        {
            base.Can_use_of_type_kiwi();
            AssertSql(@"
                SELECT ""a"".""Species"", ""a"".""CountryId"", ""a"".""Discriminator"", ""a"".""Name"", ""a"".""EagleId"", ""a"".""IsFlightless"", ""a"".""FoundOn""
                FROM ""Animal"" AS ""a""
                WHERE (""a"".""Discriminator"" IN (N'Eagle', N'Kiwi') AND (""a"".""CountryId"" = 1)) AND (""a"".""Discriminator"" = N'Kiwi')
            ");
        }

        [ActianTodo]
        public override void Can_use_derived_set()
        {
            base.Can_use_derived_set();
        }

        [ActianTodo]
        public override void Can_use_IgnoreQueryFilters_and_GetDatabaseValues()
        {
            base.Can_use_IgnoreQueryFilters_and_GetDatabaseValues();
        }
    }
}
