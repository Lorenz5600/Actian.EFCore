using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore.Query
{
    public class QueryTaggingActianTest : QueryTaggingTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public QueryTaggingActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Single_query_tag()
        {
            base.Single_query_tag();
            AssertSql(@"
                -- Yanni
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        public override void Single_query_multiple_tags()
        {
            base.Single_query_multiple_tags();
            AssertSql(@"
                -- Yanni
                
                -- Enya
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        public override void Duplicate_tags()
        {
            base.Duplicate_tags();
            AssertSql(@"
                -- Yanni
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Tags_on_subquery()
        {
            base.Tags_on_subquery();
            AssertSql(@"
                -- Yanni
                
                -- Laurel
                
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                CROSS JOIN (
                    SELECT FIRST 5 ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    ORDER BY ""o"".""OrderID""
                ) AS ""t""
                WHERE ""c"".""CustomerID"" = N'ALFKI'
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Tag_on_include_query()
        {
            base.Tag_on_include_query();
            AssertSql(@"
                -- Yanni
                
                SELECT ""t"".""CustomerID"", ""t"".""Address"", ""t"".""City"", ""t"".""CompanyName"", ""t"".""ContactName"", ""t"".""ContactTitle"", ""t"".""Country"", ""t"".""Fax"", ""t"".""Phone"", ""t"".""PostalCode"", ""t"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM (
                    SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    ORDER BY ""c"".""CustomerID""
                ) AS ""t""
                LEFT JOIN ""Orders"" AS ""o"" ON ""t"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""t"".""CustomerID"", ""o"".""OrderID""
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Tag_on_scalar_query()
        {
            base.Tag_on_scalar_query();
            AssertSql(@"
                -- Yanni
                
                SELECT FIRST 1 ""o"".""OrderDate""
                FROM ""Orders"" AS ""o""
                ORDER BY ""o"".""OrderID""
            ");
        }

        public override void Single_query_multiline_tag()
        {
            base.Single_query_multiline_tag();
            AssertSql(@"
                -- Yanni
                -- AND
                -- Laurel
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        public override void Single_query_multiple_multiline_tag()
        {
            base.Single_query_multiple_multiline_tag();
            AssertSql(@"
                -- Yanni
                -- AND
                -- Laurel
                
                -- Yet
                -- Another
                -- Multiline
                -- Tag
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        public override void Single_query_multiline_tag_with_empty_lines()
        {
            base.Single_query_multiline_tag_with_empty_lines();
            AssertSql(@"
                -- Yanni
                -- 
                -- AND
                -- 
                -- Laurel
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                ORDER BY ""c"".""CustomerID""
            ");
        }
    }
}
