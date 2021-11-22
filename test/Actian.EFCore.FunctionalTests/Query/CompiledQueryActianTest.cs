using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class CompiledQueryActianTest : CompiledQueryTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public CompiledQueryActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void DbSet_query()
        {
            base.DbSet_query();
            AssertSql(
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                ",
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                "
            );
        }

        public override void DbSet_query_first()
        {
            base.DbSet_query_first();
            AssertSql(@"
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override void DbQuery_query()
        {
            base.DbQuery_query();
            AssertSql(
                @"SELECT ""c"".""CustomerID"" + N'' as ""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c""",
                @"SELECT ""c"".""CustomerID"" + N'' as ""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c"""
            );
        }

        [ActianTodo]
        public override void DbQuery_query_first()
        {
            base.DbQuery_query_first();
            AssertSql(@"
                SELECT FIRST 1 ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle""
                FROM (
                    SELECT ""c"".""CustomerID"" + N'' as ""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c""
                ) AS ""c""
                ORDER BY ""c"".""CompanyName""
            ");
        }

        public override void Query_ending_with_include()
        {
            base.Query_ending_with_include();
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID"", ""o"".""OrderID""
            ");
        }

        public override void Untyped_context()
        {
            base.Untyped_context();
            AssertSql(
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                ",
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                "
            );
        }

        public override void Query_with_single_parameter()
        {
            base.Query_with_single_parameter();
            AssertSql(
                @"
                    @__customerID='ALFKI'
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                ",
                @"
                    @__customerID='ANATR'
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                "
            );
        }

        public override void Query_with_single_parameter_with_include()
        {
            base.Query_with_single_parameter_with_include();
        }

        public override void First_query_with_single_parameter()
        {
            base.First_query_with_single_parameter();
            AssertSql(
                @"
                    @__customerID='ALFKI'
                    
                    SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                ",
                @"
                    @__customerID='ANATR'
                    
                    SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                "
            );
        }

        public override void Query_with_two_parameters()
        {
            base.Query_with_two_parameters();
            AssertSql(
                @"
                    @__customerID='ALFKI'
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                ",
                @"
                    @__customerID='ANATR'
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                "
            );
        }

        public override void Query_with_three_parameters()
        {
            base.Query_with_three_parameters();
            AssertSql(
                @"
                    @__customerID='ALFKI'
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                ",
                @"
                    @__customerID='ANATR'
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = @__customerID
                "
            );
        }

        public override void Query_with_array_parameter()
        {
            base.Query_with_array_parameter();
        }

        public override void Query_with_contains()
        {
            base.Query_with_contains();
            AssertSql(
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" IN (N'ALFKI')
                ",
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" IN (N'ANATR')
                "
            );
        }

        [ActianTodo]
        public override void Multiple_queries()
        {
            base.Multiple_queries();
        }

        public override void Query_with_closure()
        {
            base.Query_with_closure();
            AssertSql(
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = N'ALFKI'
                ",
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = N'ALFKI'
                "
            );
        }

        public override void Query_with_closure_null()
        {
            base.Query_with_closure_null();
        }

        public override Task DbSet_query_async()
        {
            return base.DbSet_query_async();
        }

        public override Task DbSet_query_first_async()
        {
            return base.DbSet_query_first_async();
        }

        [ActianTodo]
        public override async Task DbQuery_query_async()
        {
            await base.DbQuery_query_async();
            AssertSql(
                @"SELECT ""c"".""CustomerID"" + N'' as ""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c""",
                @"SELECT ""c"".""CustomerID"" + N'' as ""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c"""
            );
        }

        [ActianTodo]
        public override async Task DbQuery_query_first_async()
        {
            await base.DbQuery_query_first_async();
            AssertSql(@"
                SELECT FIRST 1 ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle""
                FROM (
                    SELECT ""c"".""CustomerID"" + N'' as ""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c""
                ) AS ""c""
                ORDER BY ""c"".""CompanyName""
            ");
        }

        public override Task Untyped_context_async()
        {
            return base.Untyped_context_async();
        }

        public override Task Query_with_single_parameter_async()
        {
            return base.Query_with_single_parameter_async();
        }

        public override Task First_query_with_single_parameter_async()
        {
            return base.First_query_with_single_parameter_async();
        }

        public override Task First_query_with_cancellation_async()
        {
            return base.First_query_with_cancellation_async();
        }

        public override Task Query_with_two_parameters_async()
        {
            return base.Query_with_two_parameters_async();
        }

        public override Task Query_with_three_parameters_async()
        {
            return base.Query_with_three_parameters_async();
        }

        public override Task Query_with_array_parameter_async()
        {
            return base.Query_with_array_parameter_async();
        }

        public override Task Query_with_closure_async()
        {
            return base.Query_with_closure_async();
        }

        public override Task Query_with_closure_async_null()
        {
            return base.Query_with_closure_async_null();
        }

        public override void Compiled_query_when_does_not_end_in_query_operator()
        {
            base.Compiled_query_when_does_not_end_in_query_operator();
            AssertSql(@"
                @__customerID='ALFKI'
                
                SELECT COUNT(*)
                FROM ""Customers"" AS ""c""
                WHERE ""c"".""CustomerID"" = @__customerID
            ");
        }
    }
}
