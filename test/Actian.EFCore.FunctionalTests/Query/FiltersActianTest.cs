using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class FiltersActianTest : FiltersTestBase<NorthwindQueryActianFixture<NorthwindFiltersCustomizer>>
    {
        public FiltersActianTest(NorthwindQueryActianFixture<NorthwindFiltersCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Count_query()
        {
            base.Count_query();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                
                SELECT COUNT(*)
                FROM ""Customers"" AS ""c""
                WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
            ");
        }

        [ActianTodo]
        public override void Materialized_query()
        {
            base.Materialized_query();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
            ");
        }

        [ActianTodo]
        public override void Find()
        {
            base.Find();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                @__p_0='ALFKI' (Size = 5)
                
                SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                WHERE ((@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))) AND (""c"".""CustomerID"" = @__p_0)
            ");
        }

        public override void Client_eval()
        {
            base.Client_eval();
        }

        [ActianTodo]
        public override Task Materialized_query_async()
        {
            return base.Materialized_query_async();
        }

        [ActianTodo]
        public override void Materialized_query_parameter()
        {
            base.Materialized_query_parameter();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='F' (Size = 4000)
                
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
            ");
        }

        [ActianTodo]
        public override void Materialized_query_parameter_new_context()
        {
            base.Materialized_query_parameter_new_context();
            AssertSql(
                @"
                    @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                ",
                @"
                    @__ef_filter__TenantPrefix_0='T' (Size = 4000)
                    
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                "
            );
        }

        [ActianTodo]
        public override void Projection_query()
        {
            base.Projection_query();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                
                SELECT ""c"".""CustomerID""
                FROM ""Customers"" AS ""c""
                WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
            ");
        }

        [ActianTodo]
        public override void Projection_query_parameter()
        {
            base.Projection_query_parameter();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='F' (Size = 4000)
                
                SELECT ""c"".""CustomerID""
                FROM ""Customers"" AS ""c""
                WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
            ");
        }

        [ActianTodo]
        public override void Include_query()
        {
            base.Include_query();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""t0"".""OrderID"", ""t0"".""CustomerID"", ""t0"".""EmployeeID"", ""t0"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    LEFT JOIN (
                        SELECT ""c0"".""CustomerID"", ""c0"".""Address"", ""c0"".""City"", ""c0"".""CompanyName"", ""c0"".""ContactName"", ""c0"".""ContactTitle"", ""c0"".""Country"", ""c0"".""Fax"", ""c0"".""Phone"", ""c0"".""PostalCode"", ""c0"".""Region""
                        FROM ""Customers"" AS ""c0""
                        WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c0"".""CompanyName"" IS NOT NULL AND (LEFT(""c0"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                    ) AS ""t"" ON ""o"".""CustomerID"" = ""t"".""CustomerID""
                    WHERE ""t"".""CustomerID"" IS NOT NULL AND ""t"".""CompanyName"" IS NOT NULL
                ) AS ""t0"" ON ""c"".""CustomerID"" = ""t0"".""CustomerID""
                WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                ORDER BY ""c"".""CustomerID"", ""t0"".""OrderID""
            ");
        }

        public override void Include_query_opt_out()
        {
            base.Include_query_opt_out();
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID"", ""o"".""OrderID""
            ");
        }

        [ActianTodo]
        public override void Included_many_to_one_query()
        {
            base.Included_many_to_one_query();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""t"".""CustomerID"", ""t"".""Address"", ""t"".""City"", ""t"".""CompanyName"", ""t"".""ContactName"", ""t"".""ContactTitle"", ""t"".""Country"", ""t"".""Fax"", ""t"".""Phone"", ""t"".""PostalCode"", ""t"".""Region""
                FROM ""Orders"" AS ""o""
                LEFT JOIN (
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                ) AS ""t"" ON ""o"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" IS NOT NULL AND ""t"".""CompanyName"" IS NOT NULL
            ");
        }

        [ActianTodo]
        public override void Project_reference_that_itself_has_query_filter_with_another_reference()
        {
            base.Project_reference_that_itself_has_query_filter_with_another_reference();
            AssertSql(@"
                @__ef_filter__TenantPrefix_1='B' (Size = 4000)
                @__ef_filter___quantity_0='50'
                
                SELECT ""t0"".""OrderID"", ""t0"".""CustomerID"", ""t0"".""EmployeeID"", ""t0"".""OrderDate""
                FROM ""Order Details"" AS ""o""
                INNER JOIN (
                    SELECT ""o0"".""OrderID"", ""o0"".""CustomerID"", ""o0"".""EmployeeID"", ""o0"".""OrderDate"", ""t"".""CustomerID"" AS ""CustomerID0"", ""t"".""Address"", ""t"".""City"", ""t"".""CompanyName"", ""t"".""ContactName"", ""t"".""ContactTitle"", ""t"".""Country"", ""t"".""Fax"", ""t"".""Phone"", ""t"".""PostalCode"", ""t"".""Region""
                    FROM ""Orders"" AS ""o0""
                    LEFT JOIN (
                        SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                        FROM ""Customers"" AS ""c""
                        WHERE (@__ef_filter__TenantPrefix_1 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_1)) = @__ef_filter__TenantPrefix_1))
                    ) AS ""t"" ON ""o0"".""CustomerID"" = ""t"".""CustomerID""
                    WHERE ""t"".""CustomerID"" IS NOT NULL AND ""t"".""CompanyName"" IS NOT NULL
                ) AS ""t0"" ON ""o"".""OrderID"" = ""t0"".""OrderID""
                WHERE ""o"".""Quantity"" > @__ef_filter___quantity_0
            ");
        }

        public override void Included_one_to_many_query_with_client_eval()
        {
            base.Included_one_to_many_query_with_client_eval();
        }

        [ActianTodo]
        public override void Navs_query()
        {
            base.Navs_query();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                @__ef_filter___quantity_1='50'
                
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    LEFT JOIN (
                        SELECT ""c0"".""CustomerID"", ""c0"".""Address"", ""c0"".""City"", ""c0"".""CompanyName"", ""c0"".""ContactName"", ""c0"".""ContactTitle"", ""c0"".""Country"", ""c0"".""Fax"", ""c0"".""Phone"", ""c0"".""PostalCode"", ""c0"".""Region""
                        FROM ""Customers"" AS ""c0""
                        WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c0"".""CompanyName"" IS NOT NULL AND (LEFT(""c0"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                    ) AS ""t"" ON ""o"".""CustomerID"" = ""t"".""CustomerID""
                    WHERE ""t"".""CustomerID"" IS NOT NULL AND ""t"".""CompanyName"" IS NOT NULL
                ) AS ""t0"" ON ""c"".""CustomerID"" = ""t0"".""CustomerID""
                INNER JOIN (
                    SELECT ""o0"".""OrderID"", ""o0"".""ProductID"", ""o0"".""Discount"", ""o0"".""Quantity"", ""o0"".""UnitPrice""
                    FROM ""Order Details"" AS ""o0""
                    WHERE ""o0"".""Quantity"" > @__ef_filter___quantity_1
                ) AS ""t1"" ON ""t0"".""OrderID"" = ""t1"".""OrderID""
                WHERE ((@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))) AND (""t1"".""Discount"" < CAST(10 AS real))
            ");
        }

        [ActianTodo]
        public override void Compiled_query()
        {
            try
            {
                base.Compiled_query();
            }
            finally
            {
                AssertSql(
                    @"
                        @__ef_filter__TenantPrefix_0='B'
                        @__customerID='BERGS'
                    
                        SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                        FROM ""Customers"" AS ""c""
                        WHERE ((@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LENGTH(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))) AND (""c"".""CustomerID"" = @__customerID)
                    ",
                    @"
                        @__ef_filter__TenantPrefix_0='B'
                        @__customerID='BLAUS'
                    
                        SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                        FROM ""Customers"" AS ""c""
                        WHERE ((@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LENGTH(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))) AND (""c"".""CustomerID"" = @__customerID)
                    "
                );
            }
        }

        [ActianTodo]
        public override void Entity_Equality()
        {
            base.Entity_Equality();
            AssertSql(@"
                @__ef_filter__TenantPrefix_0='B' (Size = 4000)
                
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Orders"" AS ""o""
                LEFT JOIN (
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE (@__ef_filter__TenantPrefix_0 = N'') OR (""c"".""CompanyName"" IS NOT NULL AND (LEFT(""c"".""CompanyName"", LEN(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0))
                ) AS ""t"" ON ""o"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" IS NOT NULL AND ""t"".""CompanyName"" IS NOT NULL
            ");
        }
    }
}
