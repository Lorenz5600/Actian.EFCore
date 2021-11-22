using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class WarningsActianTest : WarningsTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public WarningsActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Does_not_throw_for_top_level_single()
        {
            base.Does_not_throw_for_top_level_single();
        }

        [ActianTodo]
        public override void Paging_operation_without_orderby_issues_warning()
        {
            base.Paging_operation_without_orderby_issues_warning();
        }

        [ActianTodo]
        public override Task Paging_operation_without_orderby_issues_warning_async()
        {
            return base.Paging_operation_without_orderby_issues_warning_async();
        }

        [ActianTodo]
        public override void FirstOrDefault_without_orderby_and_filter_issues_warning_subquery()
        {
            base.FirstOrDefault_without_orderby_and_filter_issues_warning_subquery();
        }

        public override void FirstOrDefault_without_orderby_but_with_filter_doesnt_issue_warning()
        {
            base.FirstOrDefault_without_orderby_but_with_filter_doesnt_issue_warning();
        }

        public override void Single_SingleOrDefault_without_orderby_doesnt_issue_warning()
        {
            base.Single_SingleOrDefault_without_orderby_doesnt_issue_warning();
        }

        [ActianTodo]
        public override void LastOrDefault_with_order_by_does_not_issue_client_eval_warning()
        {
            base.LastOrDefault_with_order_by_does_not_issue_client_eval_warning();
        }

        public override void Last_with_order_by_does_not_issue_client_eval_warning_if_at_top_level()
        {
            base.Last_with_order_by_does_not_issue_client_eval_warning_if_at_top_level();
        }

        public override void Max_does_not_issue_client_eval_warning_when_at_top_level()
        {
            base.Max_does_not_issue_client_eval_warning_when_at_top_level();
        }

        public override void Comparing_collection_navigation_to_null_issues_possible_unintended_consequences_warning()
        {
            base.Comparing_collection_navigation_to_null_issues_possible_unintended_consequences_warning();
        }

        public override void Comparing_two_collections_together_issues_possible_unintended_reference_comparison_warning()
        {
            base.Comparing_two_collections_together_issues_possible_unintended_reference_comparison_warning();
        }
    }
}
