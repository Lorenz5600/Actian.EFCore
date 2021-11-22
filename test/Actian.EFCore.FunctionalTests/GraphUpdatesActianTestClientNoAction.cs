using System.Linq;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class GraphUpdatesActianTestClientNoAction : GraphUpdatesActianTestBase<GraphUpdatesActianTestClientNoAction.GraphUpdatesWithClientNoActionActianFixture>
    {
        public GraphUpdatesActianTestClientNoAction(GraphUpdatesWithClientNoActionActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture, testOutputHelper)
        {
        }

        [ActianTodo]
        public override void Changes_to_Added_relationships_are_picked_up(ChangeMechanism changeMechanism)
        {
            base.Changes_to_Added_relationships_are_picked_up(changeMechanism);
        }

        [ActianTodo]
        public override void New_FK_is_not_cleared_on_old_dependent_delete(
            bool loadNewParent,
            CascadeTiming? deleteOrphansTiming)
        {
            base.New_FK_is_not_cleared_on_old_dependent_delete(loadNewParent, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_One_to_one_relationships_are_one_to_one(
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_One_to_one_relationships_are_one_to_one(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_One_to_one_relationships_are_one_to_one(
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_One_to_one_relationships_are_one_to_one(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_One_to_one_with_AK_relationships_are_one_to_one(
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_One_to_one_with_AK_relationships_are_one_to_one(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_One_to_one_with_AK_relationships_are_one_to_one(
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_One_to_one_with_AK_relationships_are_one_to_one(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void No_fixup_to_Deleted_entities(
            CascadeTiming? deleteOrphansTiming)
        {
            base.No_fixup_to_Deleted_entities(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_optional_many_to_one_dependents(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_optional_many_to_one_dependents(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_required_many_to_one_dependents(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_many_to_one_dependents(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_removed_optional_many_to_one_dependents(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_removed_optional_many_to_one_dependents(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Resetting_a_deleted_reference_fixes_up_again()
        {
            base.Resetting_a_deleted_reference_fixes_up_again();
        }

        [ActianTodo]
        public override void Detaching_principal_entity_will_remove_references_to_it()
        {
            base.Detaching_principal_entity_will_remove_references_to_it();
        }

        [ActianTodo]
        public override void Detaching_dependent_entity_will_not_remove_references_to_it()
        {
            base.Detaching_dependent_entity_will_not_remove_references_to_it();
        }

        [ActianTodo]
        public override void Save_removed_required_many_to_one_dependents(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_removed_required_many_to_one_dependents(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_changed_optional_one_to_one(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_changed_optional_one_to_one(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_required_one_to_one_changed_by_reference(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_one_to_one_changed_by_reference(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_required_non_PK_one_to_one_changed_by_reference(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_non_PK_one_to_one_changed_by_reference(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_optional_one_to_one(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_optional_one_to_one(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_one_to_one(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_one_to_one(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_non_PK_one_to_one(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_non_PK_one_to_one(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_optional_one_to_one(
            ChangeMechanism changeMechanism,
            bool useExistingRoot,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_optional_one_to_one(changeMechanism, useExistingRoot, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_required_one_to_one(
            ChangeMechanism changeMechanism,
            bool useExistingRoot,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_required_one_to_one(changeMechanism, useExistingRoot, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_required_non_PK_one_to_one(
            ChangeMechanism changeMechanism,
            bool useExistingRoot,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_required_non_PK_one_to_one(changeMechanism, useExistingRoot, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_to_different_one_to_many(
            ChangeMechanism changeMechanism,
            bool useExistingParent,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_to_different_one_to_many(changeMechanism, useExistingParent, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_one_to_many_overlapping(
            ChangeMechanism changeMechanism,
            bool useExistingParent,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_one_to_many_overlapping(changeMechanism, useExistingParent, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Mark_modified_one_to_many_overlapping(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Mark_modified_one_to_many_overlapping(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_optional_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_optional_many_to_one_dependents_with_alternate_key(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_required_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_many_to_one_dependents_with_alternate_key(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_removed_optional_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_removed_optional_many_to_one_dependents_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_removed_required_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_removed_required_many_to_one_dependents_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_changed_optional_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_changed_optional_one_to_one_with_alternate_key(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_changed_optional_one_to_one_with_alternate_key_in_store()
        {
            base.Save_changed_optional_one_to_one_with_alternate_key_in_store();
        }

        [ActianTodo]
        public override void Save_required_one_to_one_changed_by_reference_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_one_to_one_changed_by_reference_with_alternate_key(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_required_non_PK_one_to_one_changed_by_reference_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_non_PK_one_to_one_changed_by_reference_with_alternate_key(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_optional_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_optional_one_to_one_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_one_to_one_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_non_PK_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_non_PK_one_to_one_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_optional_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingRoot,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_optional_one_to_one_with_alternate_key(changeMechanism, useExistingRoot, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_required_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingRoot,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_required_one_to_one_with_alternate_key(changeMechanism, useExistingRoot, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Reparent_required_non_PK_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingRoot,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Reparent_required_non_PK_one_to_one_with_alternate_key(changeMechanism, useExistingRoot, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependent_leaves_can_be_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependent_leaves_can_be_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_are_orphaned(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependent_leaves_can_be_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependent_leaves_can_be_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_are_orphaned(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_leaf_can_be_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_leaf_can_be_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_leaf_can_be_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_leaf_can_be_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_leaf_can_be_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_leaf_can_be_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_with_alternate_key_are_orphaned(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_with_alternate_key_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_alternate_key_are_orphaned(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_alternate_key_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_deleted_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_deleted_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_are_orphaned_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_are_orphaned_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_with_alternate_key_are_orphaned_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_with_alternate_key_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_alternate_key_are_orphaned_in_store(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_alternate_key_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_are_orphaned_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_are_orphaned_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_with_alternate_key_are_orphaned_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_with_alternate_key_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_alternate_key_are_orphaned_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_alternate_key_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Re_childing_parent_to_new_child_with_delete(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Re_childing_parent_to_new_child_with_delete(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sometimes_not_calling_DetectChanges_when_required_does_not_throw_for_null_ref()
        {
            base.Sometimes_not_calling_DetectChanges_when_required_does_not_throw_for_null_ref();
        }

        [ActianTodo]
        public override void Can_add_valid_first_dependent_when_multiple_possible_principal_sides()
        {
            base.Can_add_valid_first_dependent_when_multiple_possible_principal_sides();
        }

        [ActianTodo]
        public override void Can_add_valid_second_dependent_when_multiple_possible_principal_sides()
        {
            base.Can_add_valid_second_dependent_when_multiple_possible_principal_sides();
        }

        [ActianTodo]
        public override void Can_add_multiple_dependents_when_multiple_possible_principal_sides()
        {
            base.Can_add_multiple_dependents_when_multiple_possible_principal_sides();
        }

        public class GraphUpdatesWithClientNoActionActianFixture : GraphUpdatesActianFixtureBase
        {
            protected override string StoreName { get; } = "GraphClientNoActionUpdatesTest";
            public override bool ForceClientNoAction => true;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                foreach (var foreignKey in modelBuilder.Model
                    .GetEntityTypes()
                    .SelectMany(e => e.GetDeclaredForeignKeys()))
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.ClientNoAction;
                }
            }
        }
    }
}
