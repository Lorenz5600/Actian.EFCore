using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore.Query
{
    partial class SimpleQueryActianTest
    {
        public override async Task Join_customers_orders_projection(bool isAsync)
        {
            await base.Join_customers_orders_projection(isAsync);
            AssertSql(@"
                SELECT ""c"".""ContactName"", ""o"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        public override async Task Join_customers_orders_entities(bool isAsync)
        {
            await base.Join_customers_orders_entities(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        public override Task Join_customers_orders_entities_same_entity_twice(bool isAsync)
        {
            return base.Join_customers_orders_entities_same_entity_twice(isAsync);
        }

        public override async Task Join_select_many(bool isAsync)
        {
            await base.Join_select_many(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""e"".""EmployeeID"", ""e"".""City"", ""e"".""Country"", ""e"".""FirstName"", ""e"".""ReportsTo"", ""e"".""Title""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                CROSS JOIN ""Employees"" AS ""e""
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Client_Join_select_many(bool isAsync)
        {
            await base.Client_Join_select_many(isAsync);
            AssertSql(
                @"
                    @__p_0='2'
                    
                    SELECT ""t0"".""EmployeeID"", ""t0"".""City"", ""t0"".""Country"", ""t0"".""FirstName"", ""t0"".""ReportsTo"", ""t0"".""Title""
                    FROM (
                        SELECT FIRST @__p_0 ""e0"".""EmployeeID"", ""e0"".""City"", ""e0"".""Country"", ""e0"".""FirstName"", ""e0"".""ReportsTo"", ""e0"".""Title""
                        FROM ""Employees"" AS ""e0""
                        ORDER BY ""e0"".""EmployeeID""
                    ) AS ""t0""
                ",
                @"
                    @__p_0='2'
                    
                    SELECT ""t"".""EmployeeID"", ""t"".""City"", ""t"".""Country"", ""t"".""FirstName"", ""t"".""ReportsTo"", ""t"".""Title""
                    FROM (
                        SELECT FIRST @__p_0 ""e"".""EmployeeID"", ""e"".""City"", ""e"".""Country"", ""e"".""FirstName"", ""e"".""ReportsTo"", ""e"".""Title""
                        FROM ""Employees"" AS ""e""
                        ORDER BY ""e"".""EmployeeID""
                    ) AS ""t""
                ",
                @"
                    SELECT ""t1"".""EmployeeID"", ""t1"".""City"", ""t1"".""Country"", ""t1"".""FirstName"", ""t1"".""ReportsTo"", ""t1"".""Title""
                    FROM (
                        SELECT ""e1"".""EmployeeID"", ""e1"".""City"", ""e1"".""Country"", ""e1"".""FirstName"", ""e1"".""ReportsTo"", ""e1"".""Title""
                        FROM ""Employees"" AS ""e1""
                        ORDER BY ""e1"".""EmployeeID""
                        OFFSET 6 ROWS FETCH NEXT 2 ROWS ONLY
                    ) AS ""t1""
                ",
                @"
                    SELECT ""t1"".""EmployeeID"", ""t1"".""City"", ""t1"".""Country"", ""t1"".""FirstName"", ""t1"".""ReportsTo"", ""t1"".""Title""
                    FROM (
                        SELECT ""e1"".""EmployeeID"", ""e1"".""City"", ""e1"".""Country"", ""e1"".""FirstName"", ""e1"".""ReportsTo"", ""e1"".""Title""
                        FROM ""Employees"" AS ""e1""
                        ORDER BY ""e1"".""EmployeeID""
                        OFFSET 6 ROWS FETCH NEXT 2 ROWS ONLY
                    ) AS ""t1""
                "
            );
        }

        [ActianTodo]
        public override async Task Join_customers_orders_select(bool isAsync)
        {
            await base.Join_customers_orders_select(isAsync);
            AssertSql(@"
                SELECT ""c"".""ContactName"", ""o"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        public override async Task Join_customers_orders_with_subquery(bool isAsync)
        {
            await base.Join_customers_orders_with_subquery(isAsync);
            AssertSql(@"
                SELECT ""c"".""ContactName"", ""t"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" = N'ALFKI'
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Join_customers_orders_with_subquery_with_take(bool isAsync)
        {
            await base.Join_customers_orders_with_subquery_with_take(isAsync);
            AssertSql(@"
                @__p_0='5'
                
                SELECT ""c"".""ContactName"", ""t"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT FIRST @__p_0 ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    ORDER BY ""o"".""OrderID""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" = N'ALFKI'
            ");
        }

        public override async Task Join_customers_orders_with_subquery_anonymous_property_method(bool isAsync)
        {
            await base.Join_customers_orders_with_subquery_anonymous_property_method(isAsync);
            AssertSql(@"
                SELECT ""t"".""OrderID"", ""t"".""CustomerID"", ""t"".""EmployeeID"", ""t"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" = N'ALFKI'
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Join_customers_orders_with_subquery_anonymous_property_method_with_take(bool isAsync)
        {
            await base.Join_customers_orders_with_subquery_anonymous_property_method_with_take(isAsync);
            AssertSql(@"
                @__p_0='5'
                
                SELECT ""t"".""OrderID"", ""t"".""CustomerID"", ""t"".""EmployeeID"", ""t"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT FIRST @__p_0 ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    ORDER BY ""o"".""OrderID""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" = N'ALFKI'
            ");
        }

        public override async Task Join_customers_orders_with_subquery_predicate(bool isAsync)
        {
            await base.Join_customers_orders_with_subquery_predicate(isAsync);
            AssertSql(@"
                SELECT ""c"".""ContactName"", ""t"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    WHERE ""o"".""OrderID"" > 0
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" = N'ALFKI'
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Join_customers_orders_with_subquery_predicate_with_take(bool isAsync)
        {
            await base.Join_customers_orders_with_subquery_predicate_with_take(isAsync);
            AssertSql(@"
                @__p_0='5'
                
                SELECT ""c"".""ContactName"", ""t"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT FIRST @__p_0 ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    WHERE ""o"".""OrderID"" > 0
                    ORDER BY ""o"".""OrderID""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                WHERE ""t"".""CustomerID"" = N'ALFKI'
            ");
        }

        public override async Task Join_composite_key(bool isAsync)
        {
            await base.Join_composite_key(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON (""c"".""CustomerID"" = ""o"".""CustomerID"") AND (""c"".""CustomerID"" = ""o"".""CustomerID"")
            ");
        }

        public override async Task Join_complex_condition(bool isAsync)
        {
            await base.Join_complex_condition(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    WHERE ""o"".""OrderID"" < 10250
                ) AS ""t"" ON TRUE = TRUE
                WHERE ""c"".""CustomerID"" = N'ALFKI'
            ");
        }

        [ActianTodo]
        public override async Task Join_client_new_expression(bool isAsync)
        {
            await base.Join_client_new_expression(isAsync);
            AssertSql(
                @"
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                ",
                @"
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                "
            );
        }

        [ActianTodo]
        public override Task Join_local_collection_int_closure_is_cached_correctly(bool isAsync)
        {
            return base.Join_local_collection_int_closure_is_cached_correctly(isAsync);
        }

        [ActianTodo]
        public override Task Join_local_string_closure_is_cached_correctly(bool isAsync)
        {
            return base.Join_local_string_closure_is_cached_correctly(isAsync);
        }

        [ActianTodo]
        public override Task Join_local_bytes_closure_is_cached_correctly(bool isAsync)
        {
            return base.Join_local_bytes_closure_is_cached_correctly(isAsync);
        }

        public override async Task Join_same_collection_multiple(bool isAsync)
        {
            await base.Join_same_collection_multiple(isAsync);
            AssertSql(@"
                SELECT ""c1"".""CustomerID"", ""c1"".""Address"", ""c1"".""City"", ""c1"".""CompanyName"", ""c1"".""ContactName"", ""c1"".""ContactTitle"", ""c1"".""Country"", ""c1"".""Fax"", ""c1"".""Phone"", ""c1"".""PostalCode"", ""c1"".""Region""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Customers"" AS ""c0"" ON ""c"".""CustomerID"" = ""c0"".""CustomerID""
                INNER JOIN ""Customers"" AS ""c1"" ON ""c"".""CustomerID"" = ""c1"".""CustomerID""
            ");
        }

        public override async Task Join_same_collection_force_alias_uniquefication(bool isAsync)
        {
            await base.Join_same_collection_force_alias_uniquefication(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""o0"".""OrderID"", ""o0"".""CustomerID"", ""o0"".""EmployeeID"", ""o0"".""OrderDate""
                FROM ""Orders"" AS ""o""
                INNER JOIN ""Orders"" AS ""o0"" ON ""o"".""CustomerID"" = ""o0"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override Task GroupJoin_customers_orders(bool isAsync)
        {
            return base.GroupJoin_customers_orders(isAsync);
        }

        [ActianTodo]
        public override async Task GroupJoin_customers_orders_count(bool isAsync)
        {
            await base.GroupJoin_customers_orders_count(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task GroupJoin_customers_orders_count_preserves_ordering(bool isAsync)
        {
            await base.GroupJoin_customers_orders_count_preserves_ordering(isAsync);
            AssertSql(@"
                @__p_0='5'
                
                SELECT ""t"".""CustomerID"", ""t"".""Address"", ""t"".""City"", ""t"".""CompanyName"", ""t"".""ContactName"", ""t"".""ContactTitle"", ""t"".""Country"", ""t"".""Fax"", ""t"".""Phone"", ""t"".""PostalCode"", ""t"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM (
                    SELECT FIRST @__p_0 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" NOT IN (N'VAFFE', N'DRACD')
                    ORDER BY ""c"".""City""
                ) AS ""t""
                LEFT JOIN ""Orders"" AS ""o"" ON ""t"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""t"".""City"", ""t"".""CustomerID""
            ");
        }

        public override Task GroupJoin_customers_employees_shadow(bool isAsync)
        {
            return base.GroupJoin_customers_employees_shadow(isAsync);
        }

        public override Task GroupJoin_customers_employees_subquery_shadow(bool isAsync)
        {
            return base.GroupJoin_customers_employees_subquery_shadow(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_customers_employees_subquery_shadow_take(bool isAsync)
        {
            return base.GroupJoin_customers_employees_subquery_shadow_take(isAsync);
        }

        public override async Task GroupJoin_simple(bool isAsync)
        {
            await base.GroupJoin_simple(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        public override async Task GroupJoin_simple2(bool isAsync)
        {
            await base.GroupJoin_simple2(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        public override async Task GroupJoin_simple3(bool isAsync)
        {
            await base.GroupJoin_simple3(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_tracking_groups(bool isAsync)
        {
            await base.GroupJoin_tracking_groups(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_tracking_groups2(bool isAsync)
        {
            await base.GroupJoin_tracking_groups2(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        public override async Task GroupJoin_simple_ordering(bool isAsync)
        {
            await base.GroupJoin_simple_ordering(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""City""
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task GroupJoin_simple_subquery(bool isAsync)
        {
            await base.GroupJoin_simple_subquery(isAsync);
            AssertSql(@"
                @__p_0='4'
                
                SELECT ""t"".""OrderID"", ""t"".""CustomerID"", ""t"".""EmployeeID"", ""t"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT FIRST @__p_0 ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    ORDER BY ""o"".""OrderID""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
            ");
        }

        public override Task GroupJoin_projection(bool isAsync)
        {
            return base.GroupJoin_projection(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_outer_projection(bool isAsync)
        {
            return base.GroupJoin_outer_projection(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_outer_projection2(bool isAsync)
        {
            return base.GroupJoin_outer_projection2(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_outer_projection3(bool isAsync)
        {
            return base.GroupJoin_outer_projection3(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_outer_projection4(bool isAsync)
        {
            return base.GroupJoin_outer_projection4(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_outer_projection_reverse(bool isAsync)
        {
            return base.GroupJoin_outer_projection_reverse(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_outer_projection_reverse2(bool isAsync)
        {
            return base.GroupJoin_outer_projection_reverse2(isAsync);
        }

        [ActianTodo]
        public override Task GroupJoin_subquery_projection_outer_mixed(bool isAsync)
        {
            return base.GroupJoin_subquery_projection_outer_mixed(isAsync);
        }

        public override async Task GroupJoin_DefaultIfEmpty(bool isAsync)
        {
            await base.GroupJoin_DefaultIfEmpty(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        public override async Task GroupJoin_DefaultIfEmpty_multiple(bool isAsync)
        {
            await base.GroupJoin_DefaultIfEmpty_multiple(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""o0"".""OrderID"", ""o0"".""CustomerID"", ""o0"".""EmployeeID"", ""o0"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                LEFT JOIN ""Orders"" AS ""o0"" ON ""c"".""CustomerID"" = ""o0"".""CustomerID""
            ");
        }

        public override async Task GroupJoin_DefaultIfEmpty2(bool isAsync)
        {
            await base.GroupJoin_DefaultIfEmpty2(isAsync);
            AssertSql(@"
                SELECT ""e"".""EmployeeID"", ""e"".""City"", ""e"".""Country"", ""e"".""FirstName"", ""e"".""ReportsTo"", ""e"".""Title"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Employees"" AS ""e""
                LEFT JOIN ""Orders"" AS ""o"" ON ""e"".""EmployeeID"" = ""o"".""EmployeeID""
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task GroupJoin_DefaultIfEmpty3(bool isAsync)
        {
            await base.GroupJoin_DefaultIfEmpty3(isAsync);
            AssertSql(@"
                @__p_0='1'
                
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM (
                    SELECT FIRST @__p_0 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    ORDER BY ""c"".""CustomerID""
                ) AS ""t""
                LEFT JOIN ""Orders"" AS ""o"" ON ""t"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""t"".""CustomerID""
            ");
        }

        public override async Task GroupJoin_Where(bool isAsync)
        {
            await base.GroupJoin_Where(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                WHERE ""o"".""CustomerID"" = N'ALFKI'
            ");
        }

        public override async Task GroupJoin_Where_OrderBy(bool isAsync)
        {
            await base.GroupJoin_Where_OrderBy(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                WHERE (""o"".""CustomerID"" = N'ALFKI') OR (""c"".""CustomerID"" = N'ANATR')
                ORDER BY ""c"".""City""
            ");
        }

        public override async Task GroupJoin_DefaultIfEmpty_Where(bool isAsync)
        {
            await base.GroupJoin_DefaultIfEmpty_Where(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                WHERE ""o"".""OrderID"" IS NOT NULL AND (""o"".""CustomerID"" = N'ALFKI')
            ");
        }

        public override async Task Join_GroupJoin_DefaultIfEmpty_Where(bool isAsync)
        {
            await base.Join_GroupJoin_DefaultIfEmpty_Where(isAsync);
            AssertSql(@"
                SELECT ""o0"".""OrderID"", ""o0"".""CustomerID"", ""o0"".""EmployeeID"", ""o0"".""OrderDate""
                FROM ""Customers"" AS ""c""
                INNER JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                LEFT JOIN ""Orders"" AS ""o0"" ON ""c"".""CustomerID"" = ""o0"".""CustomerID""
                WHERE ""o0"".""OrderID"" IS NOT NULL AND (""o0"".""CustomerID"" = N'ALFKI')
            ");
        }

        public override async Task GroupJoin_DefaultIfEmpty_Project(bool isAsync)
        {
            await base.GroupJoin_DefaultIfEmpty_Project(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_with_different_outer_elements_with_same_key(bool isAsync)
        {
            await base.GroupJoin_with_different_outer_elements_with_same_key(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Orders"" AS ""o""
                LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_with_different_outer_elements_with_same_key_with_predicate(bool isAsync)
        {
            await base.GroupJoin_with_different_outer_elements_with_same_key_with_predicate(isAsync);
            AssertSql(@"
                SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Orders"" AS ""o""
                LEFT JOIN ""Customers"" AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
                WHERE ""o"".""OrderID"" > 11500
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_with_different_outer_elements_with_same_key_projected_from_another_entity(bool isAsync)
        {
            await base.GroupJoin_with_different_outer_elements_with_same_key_projected_from_another_entity(isAsync);
            AssertSql(@"
                SELECT ""od"".""OrderID"", ""od"".""ProductID"", ""od"".""Discount"", ""od"".""Quantity"", ""od"".""UnitPrice"", ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Order Details"" AS ""od""
                INNER JOIN ""Orders"" AS ""od.Order"" ON ""od"".""OrderID"" = ""od.Order"".""OrderID""
                LEFT JOIN ""Customers"" AS ""c"" ON ""od.Order"".""CustomerID"" = ""c"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_SelectMany_subquery_with_filter(bool isAsync)
        {
            await base.GroupJoin_SelectMany_subquery_with_filter(isAsync);
            AssertSql(@"
                SELECT ""c"".""ContactName"", ""t"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    WHERE ""o"".""OrderID"" > 5
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_SelectMany_subquery_with_filter_orderby(bool isAsync)
        {
            await base.GroupJoin_SelectMany_subquery_with_filter_orderby(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_SelectMany_subquery_with_filter_and_DefaultIfEmpty(bool isAsync)
        {
            await base.GroupJoin_SelectMany_subquery_with_filter_and_DefaultIfEmpty(isAsync);
            AssertSql(@"
                SELECT ""c"".""ContactName"", ""t"".""OrderID"", ""t"".""CustomerID"", ""t"".""EmployeeID"", ""t"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                    FROM ""Orders"" AS ""o""
                    WHERE ""o"".""OrderID"" > 5
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_SelectMany_subquery_with_filter_orderby_and_DefaultIfEmpty(bool isAsync)
        {
            await base.GroupJoin_SelectMany_subquery_with_filter_orderby_and_DefaultIfEmpty(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                ORDER BY ""c"".""CustomerID""
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_with_order_by_key_descending1(bool isAsync)
        {
            await base.GroupJoin_with_order_by_key_descending1(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                WHERE ""c"".""CustomerID"" LIKE N'A%'
                ORDER BY ""c"".""CustomerID"" DESC
            ");
        }

        [ActianTodo]
        public override async Task GroupJoin_with_order_by_key_descending2(bool isAsync)
        {
            await base.GroupJoin_with_order_by_key_descending2(isAsync);
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                FROM ""Customers"" AS ""c""
                LEFT JOIN ""Orders"" AS ""o"" ON ""c"".""CustomerID"" = ""o"".""CustomerID""
                WHERE ""c"".""CustomerID"" LIKE N'A%'
                ORDER BY ""c"".""CustomerID"" DESC
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task GroupJoin_Subquery_with_Take_Then_SelectMany_Where(bool isAsync)
        {
            await base.GroupJoin_Subquery_with_Take_Then_SelectMany_Where(isAsync);
            AssertSql(@"
                @__p_0='100'
                
                SELECT ""c"".""CustomerID"", ""t0"".""OrderID""
                FROM ""Customers"" AS ""c""
                INNER JOIN (
                    SELECT ""t"".""OrderID"", ""t"".""CustomerID"", ""t"".""EmployeeID"", ""t"".""OrderDate""
                    FROM (
                        SELECT FIRST @__p_0 ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                        FROM ""Orders"" AS ""o""
                        ORDER BY ""o"".""OrderID""
                    ) AS ""t""
                    WHERE ""t"".""CustomerID"" IS NOT NULL AND (""t"".""CustomerID"" LIKE N'A%')
                ) AS ""t0"" ON ""c"".""CustomerID"" = ""t0"".""CustomerID""
            ");
        }
    }
}
