using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable xUnit1024 // Test methods cannot have overloads
namespace Actian.EFCore
{
    public class DatabindingActianTest : DatabindingTestBase<F1ActianFixture>
    {
        protected DatabindingActianTest(F1ActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void DbSet_Local_contains_Unchanged_Modified_and_Added_entities_but_not_Deleted_entities(
            bool toObservableCollection)
        {
            base.DbSet_Local_contains_Unchanged_Modified_and_Added_entities_but_not_Deleted_entities(toObservableCollection);
        }

        [ActianTodo]
        public override void Adding_entity_to_context_is_reflected_in_local_view(
            bool toObservableCollection)
        {
            base.Adding_entity_to_context_is_reflected_in_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Attaching_entity_to_context_is_reflected_in_local_view(
            bool toObservableCollection)
        {
            base.Attaching_entity_to_context_is_reflected_in_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_materialized_into_context_are_reflected_in_local_view(
            bool toObservableCollection)
        {
            base.Entities_materialized_into_context_are_reflected_in_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_detached_from_context_are_removed_from_local_view(
            bool toObservableCollection)
        {
            base.Entities_detached_from_context_are_removed_from_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_deleted_from_context_are_removed_from_local_view(
            bool toObservableCollection)
        {
            base.Entities_deleted_from_context_are_removed_from_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_with_state_changed_to_deleted_are_removed_from_local_view(
            bool toObservableCollection)
        {
            base.Entities_with_state_changed_to_deleted_are_removed_from_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_with_state_changed_to_detached_are_removed_from_local_view(
            bool toObservableCollection)
        {
            base.Entities_with_state_changed_to_detached_are_removed_from_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_with_state_changed_from_deleted_to_added_are_added_to_local_view(
            bool toObservableCollection)
        {
            base.Entities_with_state_changed_from_deleted_to_added_are_added_to_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_with_state_changed_from_deleted_to_unchanged_are_added_to_local_view(
            bool toObservableCollection)
        {
            base.Entities_with_state_changed_from_deleted_to_unchanged_are_added_to_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_added_to_local_view_are_added_to_state_manager(
            bool toObservableCollection)
        {
            base.Entities_added_to_local_view_are_added_to_state_manager(toObservableCollection);
        }

        [ActianTodo]
        public override void Entities_removed_from_the_local_view_are_marked_deleted_in_the_state_manager(
            bool toObservableCollection)
        {
            base.Entities_removed_from_the_local_view_are_marked_deleted_in_the_state_manager(toObservableCollection);
        }

        [ActianTodo]
        public override void Adding_entity_to_local_view_that_is_already_in_the_state_manager_and_not_Deleted_is_noop()
        {
            base.Adding_entity_to_local_view_that_is_already_in_the_state_manager_and_not_Deleted_is_noop();
        }

        [ActianTodo]
        public override void Adding_entity_to_local_view_that_is_Deleted_in_the_state_manager_makes_entity_Added(
            bool toObservableCollection)
        {
            base.Adding_entity_to_local_view_that_is_Deleted_in_the_state_manager_makes_entity_Added(toObservableCollection);
        }

        [ActianTodo]
        public override void Adding_entity_to_state_manager_of_different_type_than_local_keyless_type_has_no_effect_on_local_view(
            bool toObservableCollection)
        {
            base.Adding_entity_to_state_manager_of_different_type_than_local_keyless_type_has_no_effect_on_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void Adding_entity_to_state_manager_of_subtype_still_shows_up_in_local_view(
            bool toObservableCollection)
        {
            base.Adding_entity_to_state_manager_of_subtype_still_shows_up_in_local_view(toObservableCollection);
        }

        [ActianTodo]
        public override void DbSet_Local_is_cached_on_the_set()
        {
            base.DbSet_Local_is_cached_on_the_set();
        }

        [ActianTodo]
        public override void DbSet_Local_calls_DetectChanges()
        {
            base.DbSet_Local_calls_DetectChanges();
        }

        [ActianTodo]
        public override void Load_executes_query_on_DbQuery()
        {
            base.Load_executes_query_on_DbQuery();
        }

        [ActianTodo]
        public override void DbSet_Local_ToBindingList_contains_Unchanged_Modified_and_Added_entities_but_not_Deleted_entities()
        {
            base.DbSet_Local_ToBindingList_contains_Unchanged_Modified_and_Added_entities_but_not_Deleted_entities();
        }

        [ActianTodo]
        public override void Adding_entity_to_context_is_reflected_in_local_binding_list()
        {
            base.Adding_entity_to_context_is_reflected_in_local_binding_list();
        }

        [ActianTodo]
        public override void Entities_materialized_into_context_are_reflected_in_local_binding_list()
        {
            base.Entities_materialized_into_context_are_reflected_in_local_binding_list();
        }

        [ActianTodo]
        public override void Entities_detached_from_context_are_removed_from_local_binding_list()
        {
            base.Entities_detached_from_context_are_removed_from_local_binding_list();
        }

        [ActianTodo]
        public override void Entities_deleted_from_context_are_removed_from_local_binding_list()
        {
            base.Entities_deleted_from_context_are_removed_from_local_binding_list();
        }

        [ActianTodo]
        public override void Entities_added_to_local_binding_list_are_added_to_state_manager()
        {
            base.Entities_added_to_local_binding_list_are_added_to_state_manager();
        }

        [ActianTodo]
        public override void Entities_removed_from_the_local_binding_list_are_marked_deleted_in_the_state_manager()
        {
            base.Entities_removed_from_the_local_binding_list_are_marked_deleted_in_the_state_manager();
        }

        [ActianTodo]
        public override void Adding_entity_to_local_binding_list_that_is_Deleted_in_the_state_manager_makes_entity_Added()
        {
            base.Adding_entity_to_local_binding_list_that_is_Deleted_in_the_state_manager_makes_entity_Added();
        }

        [ActianTodo]
        public override void Adding_entity_to_state_manager_of_different_type_than_local_keyless_type_has_no_effect_on_local_binding_list()
        {
            base.Adding_entity_to_state_manager_of_different_type_than_local_keyless_type_has_no_effect_on_local_binding_list();
        }

        [ActianTodo]
        public override void Adding_entity_to_state_manager_of_subtype_still_shows_up_in_local_binding_list()
        {
            base.Adding_entity_to_state_manager_of_subtype_still_shows_up_in_local_binding_list();
        }

        [ActianTodo]
        public override void Sets_of_subtypes_can_still_be_sorted()
        {
            base.Sets_of_subtypes_can_still_be_sorted();
        }

        [ActianTodo]
        public override void Sets_containing_instances_of_subtypes_can_still_be_sorted()
        {
            base.Sets_containing_instances_of_subtypes_can_still_be_sorted();
        }

        [ActianTodo]
        public override void DbSet_Local_ToBindingList_is_cached_on_the_set()
        {
            base.DbSet_Local_ToBindingList_is_cached_on_the_set();
        }

        [ActianTodo]
        public override void Entity_added_to_context_is_added_to_navigation_property_binding_list()
        {
            base.Entity_added_to_context_is_added_to_navigation_property_binding_list();
        }

        [ActianTodo]
        public override void Entity_added_to_navigation_property_binding_list_is_added_to_context_after_DetectChanges()
        {
            base.Entity_added_to_navigation_property_binding_list_is_added_to_context_after_DetectChanges();
        }

        [ActianTodo]
        public override void Entity_removed_from_navigation_property_binding_list_is_removed_from_nav_property_but_not_marked_Deleted(
            CascadeTiming deleteOrphansTiming)
        {
            base.Entity_removed_from_navigation_property_binding_list_is_removed_from_nav_property_but_not_marked_Deleted(deleteOrphansTiming);
        }

        [ActianTodo]
        [ConditionalTheory]
        [InlineData(false)]
        [InlineData(true)]
        public new void LocalView_is_initialized_with_entities_from_the_context(bool toObservableCollection)
        {
            base.LocalView_is_initialized_with_entities_from_the_context(toObservableCollection);
        }
    }
}
