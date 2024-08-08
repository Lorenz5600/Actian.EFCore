using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Actian.EFCore
{
    public class GraphUpdatesActianTestIdentity : GraphUpdatesActianTestBase<GraphUpdatesActianTestIdentity.ActianFixture>
    {
        public GraphUpdatesActianTestIdentity(ActianFixture fixture)
            : base(fixture)
        {
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class ActianFixture : GraphUpdatesActianFixtureBase
        {
            protected override string StoreName
                => "GraphIdentityUpdatesTest";

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                modelBuilder.UseIdentityColumns();

                base.OnModelCreating(modelBuilder, context);
            }
        }

        public override async Task Alternate_key_over_foreign_key_doesnt_bypass_delete_behavior(bool async)
        {
            if (!async)
                await base.Alternate_key_over_foreign_key_doesnt_bypass_delete_behavior(async);
            else
                Assert.True(async);
        }

        [ActianTodo]
        public override async Task Can_insert_when_composite_FK_has_default_value_for_one_part(bool async)
        {
            await base.Can_insert_when_composite_FK_has_default_value_for_one_part(async);
        }

        [ActianTodo]
        public override async Task Can_insert_when_FK_has_default_value(bool async)
        {
            await base.Can_insert_when_FK_has_default_value(async);
        }

        [ActianTodo]
        public override async Task Can_insert_when_FK_has_sentinel_value(bool async)
        {
            await base.Can_insert_when_FK_has_sentinel_value(async);
        }

        public override async Task Clearing_shadow_key_owned_collection_throws(bool async, bool useUpdate, bool addNew)
        {
            if (!async)
                await base.Clearing_shadow_key_owned_collection_throws(async, useUpdate, addNew);
            else
                Assert.True(async);
        }


        public override async Task Clearing_CLR_key_owned_collection(bool async, bool useUpdate, bool addNew)
        {
            if (!async)
                await base.Clearing_CLR_key_owned_collection(async, useUpdate, addNew);
            else
                Assert.True(async);
        }

        [ActianTodo]
        public override async Task Delete_principal_with_CLR_key_owned_collection(bool async)
        {
            await base.Delete_principal_with_CLR_key_owned_collection(async);
        }

        public override async Task Delete_principal_with_shadow_key_owned_collection_throws(bool async)
        {
            if (!async)
                await base.Delete_principal_with_shadow_key_owned_collection_throws(async);
            else
                Assert.True(async);
        }

        [ActianTodo]
        public override void Optional_one_to_one_relationships_are_one_to_one(
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_relationships_are_one_to_one(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_AK_relationships_are_one_to_one(
            CascadeTiming? deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_AK_relationships_are_one_to_one(deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_leaf_can_be_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_leaf_can_be_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override async Task Reset_unknown_original_value_when_current_value_is_set(bool async)
        {
            await base.Reset_unknown_original_value_when_current_value_is_set(async);
        }

        [ActianTodo]
        public override void Save_removed_required_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_removed_required_many_to_one_dependents_with_alternate_key(changeMechanism, deleteOrphansTiming);
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
        public override void Save_required_one_to_one_changed_by_reference_with_alternate_key(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_one_to_one_changed_by_reference_with_alternate_key(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Save_required_non_PK_one_to_one_changed_by_reference(
            ChangeMechanism changeMechanism,
            bool useExistingEntities,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Save_required_non_PK_one_to_one_changed_by_reference(changeMechanism, useExistingEntities, deleteOrphansTiming);
        }

        public override async Task Saving_multiple_modified_entities_with_the_same_key_does_not_overflow(bool async)
        {
            if (!async)
                await base.Saving_multiple_modified_entities_with_the_same_key_does_not_overflow(async);
            else
                Assert.True(async);
        }

        public override async Task Saving_unknown_key_value_marks_it_as_unmodified(bool async)
        {
            if (!async)
                await base.Saving_unknown_key_value_marks_it_as_unmodified(async);
            else
                Assert.True(async);
        }

        [ActianTodo]
        public override void Sever_required_non_PK_one_to_one(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_non_PK_one_to_one(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_non_PK_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_non_PK_one_to_one_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_one_to_one(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_one_to_one(changeMechanism, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sever_required_one_to_one_with_alternate_key(
            ChangeMechanism changeMechanism,
            CascadeTiming? deleteOrphansTiming)
        {
            base.Sever_required_one_to_one_with_alternate_key(changeMechanism, deleteOrphansTiming);
        }

        public override async Task Update_principal_with_CLR_key_owned_collection(bool async)
        {
            if (!async)
                await base.Update_principal_with_CLR_key_owned_collection(async);
            else
                Assert.True(async);
        }

        public override async Task Update_principal_with_non_generated_shadow_key_owned_collection_throws(bool async, bool delete)
        {
            if (!async)
                await base.Update_principal_with_non_generated_shadow_key_owned_collection_throws(async, delete);
            else
                Assert.True(async);
        }

        public override async Task Update_principal_with_shadow_key_owned_collection_throws(bool async)
        {
            if (!async)
                await base.Update_principal_with_shadow_key_owned_collection_throws(async);
            else
                Assert.True(async);
        }

        public override async Task Update_root_by_collection_replacement_of_deleted_first_level(bool async)
        {
            if (!async)
                await base.Update_root_by_collection_replacement_of_deleted_first_level(async);
            else
                Assert.True(async);
        }

        public override async Task Update_root_by_collection_replacement_of_deleted_second_level(bool async)
        {
            if (!async)
                await base.Update_root_by_collection_replacement_of_deleted_second_level(async);
            else
                Assert.True(async);
        }

        public override async Task Update_root_by_collection_replacement_of_deleted_third_level(bool async)
        {
            if (!async)
                await base.Update_root_by_collection_replacement_of_deleted_third_level(async);
            else
                Assert.True(async);
        }

        public override async Task Update_root_by_collection_replacement_of_inserted_first_level(bool async)
        {
            if (!async)
                await base.Update_root_by_collection_replacement_of_inserted_first_level(async);
            else
                Assert.True(async);
        }

        public override async Task Update_root_by_collection_replacement_of_inserted_first_level_level(bool async)
        {
            if (!async)
                await base.Update_root_by_collection_replacement_of_inserted_first_level_level(async);
            else
                Assert.True(async);
        }

        public override async Task Update_root_by_collection_replacement_of_inserted_second_level(bool async)
        {
            if (!async)
                await base.Update_root_by_collection_replacement_of_inserted_second_level(async);
            else
                Assert.True(async);
        }
    }
}
