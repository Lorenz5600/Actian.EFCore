using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class ProxyGraphLazyLoadingUpdatesActianTest : ProxyGraphUpdatesTestBase<ProxyGraphLazyLoadingUpdatesActianTest.ProxyGraphLazyLoadingUpdatesActianFixture>
    {
        public ProxyGraphLazyLoadingUpdatesActianTest(ProxyGraphLazyLoadingUpdatesActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Optional_one_to_one_relationships_are_one_to_one()
        {
            base.Optional_one_to_one_relationships_are_one_to_one();
        }

        [ActianTodo]
        public override void Required_one_to_one_relationships_are_one_to_one()
        {
            base.Required_one_to_one_relationships_are_one_to_one();
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_AK_relationships_are_one_to_one()
        {
            base.Optional_one_to_one_with_AK_relationships_are_one_to_one();
        }

        [ActianTodo]
        public override void Required_one_to_one_with_AK_relationships_are_one_to_one()
        {
            base.Required_one_to_one_with_AK_relationships_are_one_to_one();
        }

        [ActianTodo]
        public override void No_fixup_to_Deleted_entities()
        {
            base.No_fixup_to_Deleted_entities();
        }

        [ActianTodo]
        public override void Save_optional_many_to_one_dependents(ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_optional_many_to_one_dependents(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_required_many_to_one_dependents(ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_required_many_to_one_dependents(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_removed_optional_many_to_one_dependents(ChangeMechanism changeMechanism)
        {
            base.Save_removed_optional_many_to_one_dependents(changeMechanism);
        }

        [ActianTodo]
        public override void Save_removed_required_many_to_one_dependents(ChangeMechanism changeMechanism)
        {
            base.Save_removed_required_many_to_one_dependents(changeMechanism);
        }

        [ActianTodo]
        public override void Save_changed_optional_one_to_one(ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_changed_optional_one_to_one(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_required_one_to_one_changed_by_reference(ChangeMechanism changeMechanism)
        {
            base.Save_required_one_to_one_changed_by_reference(changeMechanism);
        }

        [ActianTodo]
        public override void Save_required_non_PK_one_to_one_changed_by_reference(ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_required_non_PK_one_to_one_changed_by_reference(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Sever_optional_one_to_one(ChangeMechanism changeMechanism)
        {
            base.Sever_optional_one_to_one(changeMechanism);
        }

        [ActianTodo]
        public override void Sever_required_one_to_one(ChangeMechanism changeMechanism)
        {
            base.Sever_required_one_to_one(changeMechanism);
        }

        [ActianTodo]
        public override void Sever_required_non_PK_one_to_one(ChangeMechanism changeMechanism)
        {
            base.Sever_required_non_PK_one_to_one(changeMechanism);
        }

        [ActianTodo]
        public override void Reparent_optional_one_to_one(ChangeMechanism changeMechanism, bool useExistingRoot)
        {
            base.Reparent_optional_one_to_one(changeMechanism, useExistingRoot);
        }

        [ActianTodo]
        public override void Reparent_required_one_to_one(ChangeMechanism changeMechanism, bool useExistingRoot)
        {
            base.Reparent_required_one_to_one(changeMechanism, useExistingRoot);
        }

        [ActianTodo]
        public override void Reparent_required_non_PK_one_to_one(ChangeMechanism changeMechanism, bool useExistingRoot)
        {
            base.Reparent_required_non_PK_one_to_one(changeMechanism, useExistingRoot);
        }

        [ActianTodo]
        public override void Reparent_to_different_one_to_many(ChangeMechanism changeMechanism, bool useExistingParent)
        {
            base.Reparent_to_different_one_to_many(changeMechanism, useExistingParent);
        }

        [ActianTodo]
        public override void Reparent_one_to_many_overlapping(ChangeMechanism changeMechanism, bool useExistingParent)
        {
            base.Reparent_one_to_many_overlapping(changeMechanism, useExistingParent);
        }

        [ActianTodo]
        public override void Save_optional_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_optional_many_to_one_dependents_with_alternate_key(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_required_many_to_one_dependents_with_alternate_key(
            ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_required_many_to_one_dependents_with_alternate_key(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_removed_optional_many_to_one_dependents_with_alternate_key(ChangeMechanism changeMechanism)
        {
            base.Save_removed_optional_many_to_one_dependents_with_alternate_key(changeMechanism);
        }

        [ActianTodo]
        public override void Save_removed_required_many_to_one_dependents_with_alternate_key(ChangeMechanism changeMechanism)
        {
            base.Save_removed_required_many_to_one_dependents_with_alternate_key(changeMechanism);
        }

        [ActianTodo]
        public override void Save_changed_optional_one_to_one_with_alternate_key(ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_changed_optional_one_to_one_with_alternate_key(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_changed_optional_one_to_one_with_alternate_key_in_store()
        {
            base.Save_changed_optional_one_to_one_with_alternate_key_in_store();
        }

        [ActianTodo]
        public override void Save_required_one_to_one_changed_by_reference_with_alternate_key(
            ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_required_one_to_one_changed_by_reference_with_alternate_key(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Save_required_non_PK_one_to_one_changed_by_reference_with_alternate_key(
            ChangeMechanism changeMechanism, bool useExistingEntities)
        {
            base.Save_required_non_PK_one_to_one_changed_by_reference_with_alternate_key(changeMechanism, useExistingEntities);
        }

        [ActianTodo]
        public override void Sever_optional_one_to_one_with_alternate_key(ChangeMechanism changeMechanism)
        {
            base.Sever_optional_one_to_one_with_alternate_key(changeMechanism);
        }

        [ActianTodo]
        public override void Sever_required_one_to_one_with_alternate_key(ChangeMechanism changeMechanism)
        {
            base.Sever_required_one_to_one_with_alternate_key(changeMechanism);
        }

        [ActianTodo]
        public override void Sever_required_non_PK_one_to_one_with_alternate_key(ChangeMechanism changeMechanism)
        {
            base.Sever_required_non_PK_one_to_one_with_alternate_key(changeMechanism);
        }

        [ActianTodo]
        public override void Reparent_optional_one_to_one_with_alternate_key(ChangeMechanism changeMechanism, bool useExistingRoot)
        {
            base.Reparent_optional_one_to_one_with_alternate_key(changeMechanism, useExistingRoot);
        }

        [ActianTodo]
        public override void Reparent_required_one_to_one_with_alternate_key(ChangeMechanism changeMechanism, bool useExistingRoot)
        {
            base.Reparent_required_one_to_one_with_alternate_key(changeMechanism, useExistingRoot);
        }

        [ActianTodo]
        public override void Reparent_required_non_PK_one_to_one_with_alternate_key(ChangeMechanism changeMechanism, bool useExistingRoot)
        {
            base.Reparent_required_non_PK_one_to_one_with_alternate_key(changeMechanism, useExistingRoot);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_deleted(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_are_orphaned(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_are_orphaned(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_one_to_one_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_deleted(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_with_alternate_key_are_orphaned(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_with_alternate_key_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_alternate_key_are_orphaned(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_alternate_key_are_orphaned(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_deleted_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_deleted_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_are_orphaned_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_are_orphaned_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_one_to_one_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_with_alternate_key_are_orphaned_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_with_alternate_key_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_alternate_key_are_orphaned_in_store(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_alternate_key_are_orphaned_in_store(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_deleted_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_are_orphaned_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_are_orphaned_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_one_to_one_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_deleted_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_deleted_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_many_to_one_dependents_with_alternate_key_are_orphaned_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_many_to_one_dependents_with_alternate_key_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Optional_one_to_one_with_alternate_key_are_orphaned_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Optional_one_to_one_with_alternate_key_are_orphaned_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_deleted_starting_detached(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_are_cascade_detached_when_Added(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_are_cascade_detached_when_Added(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_are_cascade_detached_when_Added(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_many_to_one_dependents_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_many_to_one_dependents_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_one_to_one_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_one_to_one_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Required_non_PK_one_to_one_with_alternate_key_are_cascade_detached_when_Added(
            CascadeTiming cascadeDeleteTiming,
            CascadeTiming deleteOrphansTiming)
        {
            base.Required_non_PK_one_to_one_with_alternate_key_are_cascade_detached_when_Added(cascadeDeleteTiming, deleteOrphansTiming);
        }

        [ActianTodo]
        public override void Sometimes_not_calling_DetectChanges_when_required_does_not_throw_for_null_ref()
        {
            base.Sometimes_not_calling_DetectChanges_when_required_does_not_throw_for_null_ref();
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class ProxyGraphLazyLoadingUpdatesActianFixture : ProxyGraphUpdatesFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override string StoreName { get; } = "ProxyGraphLazyLoadingUpdatesTest";

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => base.AddOptions(builder.UseLazyLoadingProxies());

            protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
                => base.AddServices(serviceCollection.AddEntityFrameworkProxies());

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                modelBuilder.UseIdentityColumns();

                base.OnModelCreating(modelBuilder, context);
            }
        }
    }
}
