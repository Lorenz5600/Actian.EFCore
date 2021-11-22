using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class IncludeAsyncActianTest : IncludeAsyncTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public IncludeAsyncActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override Task Include_collection()
        {
            return base.Include_collection();
        }

        [ActianSkip("ORDER BY, OFFSET, FIRST and FETCH FIRST clauses cannot be used in subselects.")]
        public override async Task Include_collection_order_by_subquery()
        {
            await base.Include_collection_order_by_subquery();
            AssertSql(@"
                SELECT ""t"".""CustomerID"", ""t"".""Address"", ""t"".""City"", ""t"".""CompanyName"", ""t"".""ContactName"", ""t"".""ContactTitle"", ""t"".""Country"", ""t"".""Fax"", ""t"".""Phone"", ""t"".""PostalCode"", ""t"".""Region"", ""o0"".""OrderID"", ""o0"".""CustomerID"", ""o0"".""EmployeeID"", ""o0"".""OrderDate""
                FROM (
                    SELECT FIRST 1 ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", (
                        SELECT FIRST 1 ""o"".""OrderDate""
                        FROM ""Orders"" AS ""o""
                        WHERE ""c"".""CustomerID"" = ""o"".""CustomerID""
                        ORDER BY ""o"".""EmployeeID"") AS ""c""
                    FROM ""Customers"" AS ""c""
                    WHERE ""c"".""CustomerID"" = N'ALFKI'
                    ORDER BY (
                        SELECT FIRST 1 ""o"".""OrderDate""
                        FROM ""Orders"" AS ""o""
                        WHERE ""c"".""CustomerID"" = ""o"".""CustomerID""
                        ORDER BY ""o"".""EmployeeID"")
                ) AS ""t""
                LEFT JOIN ""Orders"" AS ""o0"" ON ""t"".""CustomerID"" = ""o0"".""CustomerID""
                ORDER BY ""t"".""c"", ""t"".""CustomerID"", ""o0"".""OrderID""
            ");
        }

        [ActianTodo]
        public override Task Include_closes_reader()
        {
            return base.Include_closes_reader();
        }

        public override Task Include_collection_alias_generation()
        {
            return base.Include_collection_alias_generation();
        }

        public override Task Include_collection_and_reference()
        {
            return base.Include_collection_and_reference();
        }

        public override Task Include_collection_as_no_tracking()
        {
            return base.Include_collection_as_no_tracking();
        }

        [ActianTodo]
        public override Task Include_collection_as_no_tracking2()
        {
            return base.Include_collection_as_no_tracking2();
        }

        [ActianTodo]
        public override Task Include_collection_dependent_already_tracked()
        {
            return base.Include_collection_dependent_already_tracked();
        }

        [ActianTodo]
        public override Task Include_collection_dependent_already_tracked_as_no_tracking()
        {
            return base.Include_collection_dependent_already_tracked_as_no_tracking();
        }

        [ActianTodo]
        public override Task Include_collection_on_additional_from_clause()
        {
            return base.Include_collection_on_additional_from_clause();
        }

        [ActianTodo]
        public override Task Include_collection_on_additional_from_clause_no_tracking()
        {
            return base.Include_collection_on_additional_from_clause_no_tracking();
        }

        public override Task Include_collection_on_additional_from_clause_with_filter()
        {
            return base.Include_collection_on_additional_from_clause_with_filter();
        }

        [ActianTodo]
        public override Task Include_collection_on_additional_from_clause2()
        {
            return base.Include_collection_on_additional_from_clause2();
        }

        public override Task Include_collection_on_join_clause_with_filter()
        {
            return base.Include_collection_on_join_clause_with_filter();
        }

        public override Task Include_collection_on_join_clause_with_order_by_and_filter()
        {
            return base.Include_collection_on_join_clause_with_order_by_and_filter();
        }

        [ActianTodo]
        public override Task Include_collection_on_group_join_clause_with_filter()
        {
            return base.Include_collection_on_group_join_clause_with_filter();
        }

        [ActianTodo]
        public override Task Include_collection_on_inner_group_join_clause_with_filter()
        {
            return base.Include_collection_on_inner_group_join_clause_with_filter();
        }

        [ActianTodo]
        public override Task Include_collection_when_groupby()
        {
            return base.Include_collection_when_groupby();
        }

        public override Task Include_collection_order_by_key()
        {
            return base.Include_collection_order_by_key();
        }

        public override Task Include_collection_order_by_non_key()
        {
            return base.Include_collection_order_by_non_key();
        }

        [ActianTodo]
        public override Task Include_collection_principal_already_tracked()
        {
            return base.Include_collection_principal_already_tracked();
        }

        [ActianTodo]
        public override Task Include_collection_principal_already_tracked_as_no_tracking()
        {
            return base.Include_collection_principal_already_tracked_as_no_tracking();
        }

        [ActianTodo]
        public override Task Include_collection_single_or_default_no_result()
        {
            return base.Include_collection_single_or_default_no_result();
        }

        public override Task Include_collection_when_projection()
        {
            return base.Include_collection_when_projection();
        }

        public override Task Include_collection_with_filter()
        {
            return base.Include_collection_with_filter();
        }

        public override Task Include_collection_with_filter_reordered()
        {
            return base.Include_collection_with_filter_reordered();
        }

        [ActianTodo]
        public override Task Include_duplicate_collection()
        {
            return base.Include_duplicate_collection();
        }

        [ActianTodo]
        public override Task Include_duplicate_collection_result_operator()
        {
            return base.Include_duplicate_collection_result_operator();
        }

        [ActianTodo]
        public override Task Include_duplicate_collection_result_operator2()
        {
            return base.Include_duplicate_collection_result_operator2();
        }

        [ActianTodo]
        public override Task Include_duplicate_reference()
        {
            return base.Include_duplicate_reference();
        }

        [ActianTodo]
        public override Task Include_duplicate_reference2()
        {
            return base.Include_duplicate_reference2();
        }

        [ActianTodo]
        public override Task Include_duplicate_reference3()
        {
            return base.Include_duplicate_reference3();
        }

        [ActianTodo]
        public override Task Include_multi_level_reference_and_collection_predicate()
        {
            return base.Include_multi_level_reference_and_collection_predicate();
        }

        public override Task Include_collection_with_client_filter()
        {
            return base.Include_collection_with_client_filter();
        }

        [ActianTodo]
        public override Task Include_multi_level_collection_and_then_include_reference_predicate()
        {
            return base.Include_multi_level_collection_and_then_include_reference_predicate();
        }

        public override Task Include_multiple_references()
        {
            return base.Include_multiple_references();
        }

        public override Task Include_multiple_references_and_collection_multi_level()
        {
            return base.Include_multiple_references_and_collection_multi_level();
        }

        public override Task Include_multiple_references_and_collection_multi_level_reverse()
        {
            return base.Include_multiple_references_and_collection_multi_level_reverse();
        }

        public override Task Include_multiple_references_multi_level()
        {
            return base.Include_multiple_references_multi_level();
        }

        public override Task Include_multiple_references_multi_level_reverse()
        {
            return base.Include_multiple_references_multi_level_reverse();
        }

        public override Task Include_reference()
        {
            return base.Include_reference();
        }

        public override Task Include_reference_alias_generation()
        {
            return base.Include_reference_alias_generation();
        }

        public override Task Include_reference_and_collection()
        {
            return base.Include_reference_and_collection();
        }

        public override Task Include_reference_as_no_tracking()
        {
            return base.Include_reference_as_no_tracking();
        }

        public override Task Include_reference_dependent_already_tracked()
        {
            return base.Include_reference_dependent_already_tracked();
        }

        public override Task Include_reference_single_or_default_when_no_result()
        {
            return base.Include_reference_single_or_default_when_no_result();
        }

        public override Task Include_reference_when_projection()
        {
            return base.Include_reference_when_projection();
        }

        public override Task Include_reference_with_filter()
        {
            return base.Include_reference_with_filter();
        }

        public override Task Include_reference_with_filter_reordered()
        {
            return base.Include_reference_with_filter_reordered();
        }

        public override Task Include_references_and_collection_multi_level()
        {
            return base.Include_references_and_collection_multi_level();
        }

        public override async Task Include_collection_then_include_collection()
        {
            await base.Include_collection_then_include_collection();
            AssertSql(@"
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"", ""t"".""OrderID"", ""t"".""CustomerID"", ""t"".""EmployeeID"", ""t"".""OrderDate"", ""t"".""OrderID0"", ""t"".""ProductID"", ""t"".""Discount"", ""t"".""Quantity"", ""t"".""UnitPrice""
                FROM ""Customers"" AS ""c""
                LEFT JOIN (
                    SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate"", ""o0"".""OrderID"" AS ""OrderID0"", ""o0"".""ProductID"", ""o0"".""Discount"", ""o0"".""Quantity"", ""o0"".""UnitPrice""
                    FROM ""Orders"" AS ""o""
                    LEFT JOIN ""Order Details"" AS ""o0"" ON ""o"".""OrderID"" = ""o0"".""OrderID""
                ) AS ""t"" ON ""c"".""CustomerID"" = ""t"".""CustomerID""
                ORDER BY ""c"".""CustomerID"", ""t"".""OrderID"", ""t"".""OrderID0"", ""t"".""ProductID""
            ");
        }

        public override Task Include_collection_then_include_collection_then_include_reference()
        {
            return base.Include_collection_then_include_collection_then_include_reference();
        }

        [ActianTodo]
        public override Task Include_collection_then_include_collection_predicate()
        {
            return base.Include_collection_then_include_collection_predicate();
        }

        public override Task Include_references_and_collection_multi_level_predicate()
        {
            return base.Include_references_and_collection_multi_level_predicate();
        }

        public override Task Include_references_multi_level()
        {
            return base.Include_references_multi_level();
        }

        [ActianTodo]
        public override Task Include_multi_level_reference_then_include_collection_predicate()
        {
            return base.Include_multi_level_reference_then_include_collection_predicate();
        }

        public override Task Include_multiple_references_then_include_collection_multi_level()
        {
            return base.Include_multiple_references_then_include_collection_multi_level();
        }

        public override Task Include_multiple_references_then_include_collection_multi_level_reverse()
        {
            return base.Include_multiple_references_then_include_collection_multi_level_reverse();
        }

        public override Task Include_multiple_references_then_include_multi_level()
        {
            return base.Include_multiple_references_then_include_multi_level();
        }

        public override Task Include_multiple_references_then_include_multi_level_reverse()
        {
            return base.Include_multiple_references_then_include_multi_level_reverse();
        }

        public override Task Include_references_then_include_collection_multi_level()
        {
            return base.Include_references_then_include_collection_multi_level();
        }

        public override Task Include_references_then_include_collection_multi_level_predicate()
        {
            return base.Include_references_then_include_collection_multi_level_predicate();
        }

        public override Task Include_references_then_include_multi_level()
        {
            return base.Include_references_then_include_multi_level();
        }
    }
}
