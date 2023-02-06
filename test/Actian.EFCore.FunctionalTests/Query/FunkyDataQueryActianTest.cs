using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore.Query
{
    public class FunkyDataQueryActianTest : FunkyDataQueryTestBase<FunkyDataQueryActianTest.FunkyDataQueryActianFixture>
    {
        public FunkyDataQueryActianTest(FunkyDataQueryActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override async Task String_contains_on_argument_with_wildcard_constant(bool isAsync)
        {
            await base.String_contains_on_argument_with_wildcard_constant(isAsync);
            AssertSql(
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(N'%B', ""f"".""FirstName"") > 0
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(N'a_', ""f"".""FirstName"") > 0
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(NULL, ""f"".""FirstName"") > 0
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(N'_Ba_', ""f"".""FirstName"") > 0
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(N'%B%a%r', ""f"".""FirstName"") <= 0
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(NULL, ""f"".""FirstName"") <= 0
                "
            );
        }

        [ActianSkip(LongRunning)]
        public override async Task String_contains_on_argument_with_wildcard_parameter(bool isAsync)
        {
            await base.String_contains_on_argument_with_wildcard_parameter(isAsync);
            AssertSql(
                @"
                    @__prm1_0='%B' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm1_0 = N'') OR (CHARINDEX(@__prm1_0, ""f"".""FirstName"") > 0)
                ",
                @"
                    @__prm2_0='a_' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm2_0 = N'') OR (CHARINDEX(@__prm2_0, ""f"".""FirstName"") > 0)
                ",
                @"
                    @__prm3_0=NULL (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(@__prm3_0, ""f"".""FirstName"") > 0
                ",
                @"
                    @__prm4_0='' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm4_0 = N'') OR (CHARINDEX(@__prm4_0, ""f"".""FirstName"") > 0)
                ",
                @"
                    @__prm5_0='_Ba_' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm5_0 = N'') OR (CHARINDEX(@__prm5_0, ""f"".""FirstName"") > 0)
                ",
                @"
                    @__prm6_0='%B%a%r' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm6_0 <> N'') AND (CHARINDEX(@__prm6_0, ""f"".""FirstName"") <= 0)
                ",
                @"
                    @__prm7_0='' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm7_0 <> N'') AND (CHARINDEX(@__prm7_0, ""f"".""FirstName"") <= 0)
                ",
                @"
                    @__prm8_0=NULL (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE CHARINDEX(@__prm8_0, ""f"".""FirstName"") <= 0
                "
            );
        }

        [ActianTodo]
        public override async Task String_contains_on_argument_with_wildcard_column(bool isAsync)
        {
            await base.String_contains_on_argument_with_wildcard_column(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE (""f0"".""LastName"" = N'') OR (CHARINDEX(""f0"".""LastName"", ""f"".""FirstName"") > 0)
            ");
        }

        [ActianTodo]
        public override async Task String_contains_on_argument_with_wildcard_column_negated(bool isAsync)
        {
            await base.String_contains_on_argument_with_wildcard_column_negated(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE ((""f0"".""LastName"" <> N'') OR ""f0"".""LastName"" IS NULL) AND (CHARINDEX(""f0"".""LastName"", ""f"".""FirstName"") <= 0)
            ");
        }

        [ActianTodo]
        public override async Task String_starts_with_on_argument_with_wildcard_constant(bool isAsync)
        {
            await base.String_starts_with_on_argument_with_wildcard_constant(isAsync);
            AssertSql(
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'\%B%' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'a\_%' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'\_Ba\_%' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND NOT (""f"".""FirstName"" LIKE N'\%B\%a\%r%' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                "
            );
        }

        [ActianTodo]
        public override async Task String_starts_with_on_argument_with_wildcard_parameter(bool isAsync)
        {
            await base.String_starts_with_on_argument_with_wildcard_parameter(isAsync);
            AssertSql(
                @"
                    @__prm1_0='%B' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm1_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm1_0)) = @__prm1_0))
                ",
                @"
                    @__prm2_0='a_' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm2_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm2_0)) = @__prm2_0))
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    @__prm4_0='' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm4_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm4_0)) = @__prm4_0))
                ",
                @"
                    @__prm5_0='_Ba_' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm5_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm5_0)) = @__prm5_0))
                ",
                @"
                    @__prm6_0='%B%a%r' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm6_0 <> N'') AND (""f"".""FirstName"" IS NOT NULL AND ((LEFT(""f"".""FirstName"", LEN(@__prm6_0)) <> @__prm6_0) OR LEFT(""f"".""FirstName"", LEN(@__prm6_0)) IS NULL))
                ",
                @"
                    @__prm7_0='' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm7_0 <> N'') AND (""f"".""FirstName"" IS NOT NULL AND ((LEFT(""f"".""FirstName"", LEN(@__prm7_0)) <> @__prm7_0) OR LEFT(""f"".""FirstName"", LEN(@__prm7_0)) IS NULL))
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                "
            );
        }

        [ActianTodo]
        public override async Task String_starts_with_on_argument_with_bracket(bool isAsync)
        {
            await base.String_starts_with_on_argument_with_bracket(isAsync);
            AssertSql(
                @"
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'\""%' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'B\""%' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'B\""\""a^%' ESCAPE N'\')
                ",
                @"
                    @__prm1_0='""' (Size = 4000)
                    
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm1_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm1_0)) = @__prm1_0))
                ",
                @"
                    @__prm2_0='B""' (Size = 4000)
                    
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm2_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm2_0)) = @__prm2_0))
                ",
                @"
                    @__prm3_0='B""""a^' (Size = 4000)
                    
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm3_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(@__prm3_0)) = @__prm3_0))
                ",
                @"
                    SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (""f"".""LastName"" = N'') OR (""f"".""FirstName"" IS NOT NULL AND (""f"".""LastName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(""f"".""LastName"")) = ""f"".""LastName"")))
                "
            );
        }

        [ActianTodo]
        public override async Task String_starts_with_on_argument_with_wildcard_column(bool isAsync)
        {
            await base.String_starts_with_on_argument_with_wildcard_column(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE (""f0"".""LastName"" = N'') OR (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND (LEFT(""f"".""FirstName"", LEN(""f0"".""LastName"")) = ""f0"".""LastName"")))
            ");
        }

        [ActianTodo]
        public override async Task String_starts_with_on_argument_with_wildcard_column_negated(bool isAsync)
        {
            await base.String_starts_with_on_argument_with_wildcard_column_negated(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE ((""f0"".""LastName"" <> N'') OR ""f0"".""LastName"" IS NULL) AND (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND ((LEFT(""f"".""FirstName"", LEN(""f0"".""LastName"")) <> ""f0"".""LastName"") OR LEFT(""f"".""FirstName"", LEN(""f0"".""LastName"")) IS NULL)))
            ");
        }

        [ActianTodo]
        public override async Task String_ends_with_on_argument_with_wildcard_constant(bool isAsync)
        {
            await base.String_ends_with_on_argument_with_wildcard_constant(isAsync);
            AssertSql(
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'%\%B' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'%a\_' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND (""f"".""FirstName"" LIKE N'%\_Ba\_' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE ""f"".""FirstName"" IS NOT NULL AND NOT (""f"".""FirstName"" LIKE N'%\%B\%a\%r' ESCAPE N'\')
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                "
            );
        }

        [ActianTodo]
        public override async Task String_ends_with_on_argument_with_wildcard_parameter(bool isAsync)
        {
            await base.String_ends_with_on_argument_with_wildcard_parameter(isAsync);
            AssertSql(
                @"
                    @__prm1_0='%B' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm1_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (RIGHT(""f"".""FirstName"", LEN(@__prm1_0)) = @__prm1_0))
                ",
                @"
                    @__prm2_0='a_' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm2_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (RIGHT(""f"".""FirstName"", LEN(@__prm2_0)) = @__prm2_0))
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                ",
                @"
                    @__prm4_0='' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm4_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (RIGHT(""f"".""FirstName"", LEN(@__prm4_0)) = @__prm4_0))
                ",
                @"
                    @__prm5_0='_Ba_' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm5_0 = N'') OR (""f"".""FirstName"" IS NOT NULL AND (RIGHT(""f"".""FirstName"", LEN(@__prm5_0)) = @__prm5_0))
                ",
                @"
                    @__prm6_0='%B%a%r' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm6_0 <> N'') AND (""f"".""FirstName"" IS NOT NULL AND ((RIGHT(""f"".""FirstName"", LEN(@__prm6_0)) <> @__prm6_0) OR RIGHT(""f"".""FirstName"", LEN(@__prm6_0)) IS NULL))
                ",
                @"
                    @__prm7_0='' (Size = 4000)
                    
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE (@__prm7_0 <> N'') AND (""f"".""FirstName"" IS NOT NULL AND ((RIGHT(""f"".""FirstName"", LEN(@__prm7_0)) <> @__prm7_0) OR RIGHT(""f"".""FirstName"", LEN(@__prm7_0)) IS NULL))
                ",
                @"
                    SELECT ""f"".""FirstName""
                    FROM ""FunkyCustomers"" AS ""f""
                    WHERE FALSE = TRUE
                "
            );
        }

        [ActianTodo]
        public override async Task String_ends_with_on_argument_with_wildcard_column(bool isAsync)
        {
            await base.String_ends_with_on_argument_with_wildcard_column(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE (""f0"".""LastName"" = N'') OR (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND (RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) = ""f0"".""LastName"")))
            ");
        }

        [ActianTodo]
        public override async Task String_ends_with_on_argument_with_wildcard_column_negated(bool isAsync)
        {
            await base.String_ends_with_on_argument_with_wildcard_column_negated(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE ((""f0"".""LastName"" <> N'') OR ""f0"".""LastName"" IS NULL) AND (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND ((RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) <> ""f0"".""LastName"") OR RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) IS NULL)))
            ");
        }

        [ActianTodo]
        public override async Task String_ends_with_inside_conditional(bool isAsync)
        {
            await base.String_ends_with_inside_conditional(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE CASE
                    WHEN (""f0"".""LastName"" = N'') OR (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND (RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) = ""f0"".""LastName""))) THEN TRUE
                    ELSE FALSE
                END = TRUE
            ");
        }

        [ActianTodo]
        public override async Task String_ends_with_inside_conditional_negated(bool isAsync)
        {
            await base.String_ends_with_inside_conditional_negated(isAsync);
            AssertSql(@"
                SELECT ""f"".""FirstName"" AS ""fn"", ""f0"".""LastName"" AS ""ln""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE CASE
                    WHEN ((""f0"".""LastName"" <> N'') OR ""f0"".""LastName"" IS NULL) AND (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND ((RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) <> ""f0"".""LastName"") OR RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) IS NULL))) THEN TRUE
                    ELSE FALSE
                END = TRUE
            ");
        }

        [ActianTodo]
        public override async Task String_ends_with_equals_nullable_column(bool isAsync)
        {
            await base.String_ends_with_equals_nullable_column(isAsync);
            AssertSql(@"
                SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool"", ""f0"".""Id"", ""f0"".""FirstName"", ""f0"".""LastName"", ""f0"".""NullableBool""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE CASE
                    WHEN ((""f0"".""LastName"" = N'') AND ""f0"".""LastName"" IS NOT NULL) OR (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND ((RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) = ""f0"".""LastName"") AND RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) IS NOT NULL))) THEN TRUE
                    ELSE FALSE
                END = ""f"".""NullableBool""
            ");
        }

        [ActianTodo]
        public override async Task String_ends_with_not_equals_nullable_column(bool isAsync)
        {
            await base.String_ends_with_not_equals_nullable_column(isAsync);
            AssertSql(@"
                SELECT ""f"".""Id"", ""f"".""FirstName"", ""f"".""LastName"", ""f"".""NullableBool"", ""f0"".""Id"", ""f0"".""FirstName"", ""f0"".""LastName"", ""f0"".""NullableBool""
                FROM ""FunkyCustomers"" AS ""f""
                CROSS JOIN ""FunkyCustomers"" AS ""f0""
                WHERE (CASE
                    WHEN ((""f0"".""LastName"" = N'') AND ""f0"".""LastName"" IS NOT NULL) OR (""f"".""FirstName"" IS NOT NULL AND (""f0"".""LastName"" IS NOT NULL AND ((RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) = ""f0"".""LastName"") AND RIGHT(""f"".""FirstName"", LEN(""f0"".""LastName"")) IS NOT NULL))) THEN TRUE
                    ELSE FALSE
                END <> ""f"".""NullableBool"") OR ""f"".""NullableBool"" IS NULL
            ");
        }

        protected override void ClearLog()
            => Fixture.TestSqlLoggerFactory.Clear();

        public class FunkyDataQueryActianFixture : FunkyDataQueryFixtureBase, IActianSqlFixture
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
        }
    }
}
