using System;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class OptimisticConcurrencyActianTest : OptimisticConcurrencyTestBase<F1ActianFixture>, IDisposable
    {
        public OptimisticConcurrencyActianTest(F1ActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void Dispose() => Helpers.LogSql();

        [ActianTodo]
        public override void External_model_builder_uses_validation()
        {
            base.External_model_builder_uses_validation();
        }

        [ActianTodo]
        public override void Nullable_client_side_concurrency_token_can_be_used()
        {
            base.Nullable_client_side_concurrency_token_can_be_used();
        }

        [ActianTodo]
        public override Task Simple_concurrency_exception_can_be_resolved_with_client_values()
        {
            return base.Simple_concurrency_exception_can_be_resolved_with_client_values();
        }

        [ActianTodo]
        public override Task Simple_concurrency_exception_can_be_resolved_with_store_values()
        {
            return base.Simple_concurrency_exception_can_be_resolved_with_store_values();
        }

        [ActianTodo]
        public override Task Simple_concurrency_exception_can_be_resolved_with_new_values()
        {
            return base.Simple_concurrency_exception_can_be_resolved_with_new_values();
        }

        [ActianTodo]
        public override Task Simple_concurrency_exception_can_be_resolved_with_store_values_using_equivalent_of_accept_changes()
        {
            return base.Simple_concurrency_exception_can_be_resolved_with_store_values_using_equivalent_of_accept_changes();
        }

        [ActianTodo]
        public override Task Simple_concurrency_exception_can_be_resolved_with_store_values_using_Reload()
        {
            return base.Simple_concurrency_exception_can_be_resolved_with_store_values_using_Reload();
        }

        [ActianTodo]
        public override Task Two_concurrency_issues_in_one_to_one_related_entities_can_be_handled_by_dealing_with_dependent_first()
        {
            return base.Two_concurrency_issues_in_one_to_one_related_entities_can_be_handled_by_dealing_with_dependent_first();
        }

        [ActianTodo]
        public override Task Two_concurrency_issues_in_one_to_many_related_entities_can_be_handled_by_dealing_with_dependent_first()
        {
            return base.Two_concurrency_issues_in_one_to_many_related_entities_can_be_handled_by_dealing_with_dependent_first();
        }

        [ActianTodo]
        public override Task Concurrency_issue_where_the_FK_is_the_concurrency_token_can_be_handled()
        {
            return base.Concurrency_issue_where_the_FK_is_the_concurrency_token_can_be_handled();
        }

        [ActianTodo]
        public override Task Change_in_independent_association_results_in_independent_association_exception()
        {
            return base.Change_in_independent_association_results_in_independent_association_exception();
        }

        [ActianTodo]
        public override Task Change_in_independent_association_after_change_in_different_concurrency_token_results_in_independent_association_exception()
        {
            return base.Change_in_independent_association_after_change_in_different_concurrency_token_results_in_independent_association_exception();
        }

        [ActianTodo]
        public override Task Attempting_to_delete_same_relationship_twice_for_many_to_many_results_in_independent_association_exception()
        {
            return base.Attempting_to_delete_same_relationship_twice_for_many_to_many_results_in_independent_association_exception();
        }

        [ActianTodo]
        public override Task Attempting_to_add_same_relationship_twice_for_many_to_many_results_in_independent_association_exception()
        {
            return base.Attempting_to_add_same_relationship_twice_for_many_to_many_results_in_independent_association_exception();
        }

        [ActianTodo]
        public override Task Concurrency_issue_where_a_complex_type_nested_member_is_the_concurrency_token_can_be_handled()
        {
            return base.Concurrency_issue_where_a_complex_type_nested_member_is_the_concurrency_token_can_be_handled();
        }

        [ActianTodo]
        public override Task Adding_the_same_entity_twice_results_in_DbUpdateException()
        {
            return base.Adding_the_same_entity_twice_results_in_DbUpdateException();
        }

        [ActianTodo]
        public override Task Deleting_the_same_entity_twice_results_in_DbUpdateConcurrencyException()
        {
            return base.Deleting_the_same_entity_twice_results_in_DbUpdateConcurrencyException();
        }

        [ActianTodo]
        public override Task Updating_then_deleting_the_same_entity_results_in_DbUpdateConcurrencyException()
        {
            return base.Updating_then_deleting_the_same_entity_results_in_DbUpdateConcurrencyException();
        }

        [ActianTodo]
        public override Task Updating_then_deleting_the_same_entity_results_in_DbUpdateConcurrencyException_which_can_be_resolved_with_store_values()
        {
            return base.Updating_then_deleting_the_same_entity_results_in_DbUpdateConcurrencyException_which_can_be_resolved_with_store_values();
        }

        [ActianTodo]
        public override Task Deleting_then_updating_the_same_entity_results_in_DbUpdateConcurrencyException()
        {
            return base.Deleting_then_updating_the_same_entity_results_in_DbUpdateConcurrencyException();
        }

        [ActianTodo]
        public override Task Deleting_then_updating_the_same_entity_results_in_DbUpdateConcurrencyException_which_can_be_resolved_with_store_values()
        {
            return base.Deleting_then_updating_the_same_entity_results_in_DbUpdateConcurrencyException_which_can_be_resolved_with_store_values();
        }

        [ActianTodo]
        public override Task Calling_Reload_on_an_Added_entity_that_is_not_in_database_is_no_op(bool async)
        {
            return base.Calling_Reload_on_an_Added_entity_that_is_not_in_database_is_no_op(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_an_Unchanged_entity_that_is_not_in_database_detaches_it(bool async)
        {
            return base.Calling_Reload_on_an_Unchanged_entity_that_is_not_in_database_detaches_it(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_a_Modified_entity_that_is_not_in_database_detaches_it(bool async)
        {
            return base.Calling_Reload_on_a_Modified_entity_that_is_not_in_database_detaches_it(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_a_Deleted_entity_that_is_not_in_database_detaches_it(bool async)
        {
            return base.Calling_Reload_on_a_Deleted_entity_that_is_not_in_database_detaches_it(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_a_Detached_entity_that_is_not_in_database_detaches_it(bool async)
        {
            return base.Calling_Reload_on_a_Detached_entity_that_is_not_in_database_detaches_it(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_an_Unchanged_entity_makes_the_entity_unchanged(bool async)
        {
            return base.Calling_Reload_on_an_Unchanged_entity_makes_the_entity_unchanged(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_a_Modified_entity_makes_the_entity_unchanged(bool async)
        {
            return base.Calling_Reload_on_a_Modified_entity_makes_the_entity_unchanged(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_a_Deleted_entity_makes_the_entity_unchanged(bool async)
        {
            return base.Calling_Reload_on_a_Deleted_entity_makes_the_entity_unchanged(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_an_Added_entity_that_was_saved_elsewhere_makes_the_entity_unchanged(bool async)
        {
            return base.Calling_Reload_on_an_Added_entity_that_was_saved_elsewhere_makes_the_entity_unchanged(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_a_Detached_entity_makes_the_entity_unchanged(bool async)
        {
            return base.Calling_Reload_on_a_Detached_entity_makes_the_entity_unchanged(async);
        }

        [ActianTodo]
        public override Task Calling_GetDatabaseValues_on_owned_entity_works(bool async)
        {
            return base.Calling_GetDatabaseValues_on_owned_entity_works(async);
        }

        [ActianTodo]
        public override Task Calling_Reload_on_owned_entity_works(bool async)
        {
            return base.Calling_Reload_on_owned_entity_works(async);
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());
    }
}
