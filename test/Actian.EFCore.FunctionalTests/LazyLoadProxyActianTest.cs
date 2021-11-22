using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class LazyLoadProxyActianTest : LazyLoadProxyTestBase<LazyLoadProxyActianTest.LoadActianFixture>
    {
        protected LazyLoadProxyActianTest(LoadActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Attached_references_to_principal_are_marked_as_loaded(EntityState state, bool lazy)
        {
            base.Attached_references_to_principal_are_marked_as_loaded(state, lazy);
        }

        [ActianTodo]
        public override void Attached_references_to_dependents_are_marked_as_loaded(EntityState state, bool lazy)
        {
            base.Attached_references_to_dependents_are_marked_as_loaded(state, lazy);
        }

        [ActianTodo]
        public override void Attached_collections_are_not_marked_as_loaded(EntityState state, bool lazy)
        {
            base.Attached_collections_are_not_marked_as_loaded(state, lazy);
        }

        [ActianTodo]
        public override void Lazy_load_collection(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_collection(state, useAttach, useDetach);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_many_to_one_reference_to_principal(state, useAttach, useDetach);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_one_to_one_reference_to_principal(state, useAttach, useDetach);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_dependent(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_one_to_one_reference_to_dependent(state, useAttach, useDetach);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_changed_non_found_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_changed_non_found_FK(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_changed_found_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_changed_found_FK(state);
        }

        [ActianTodo]
        public override void Lazy_load_collection_not_found(EntityState state)
        {
            base.Lazy_load_collection_not_found(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_not_found(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_not_found(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_dependent_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_not_found(state);
        }

        [ActianTodo]
        public override void Lazy_load_collection_already_loaded(EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_collection_already_loaded(state, cascadeDeleteTiming);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_already_loaded(
            EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_many_to_one_reference_to_principal_already_loaded(state, cascadeDeleteTiming);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_already_loaded(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_dependent_already_loaded(
            EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_already_loaded(state, cascadeDeleteTiming);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_alternate_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_alternate_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_dependent_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_alternate_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_collection_shadow_fk(EntityState state)
        {
            base.Lazy_load_collection_shadow_fk(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_shadow_fk(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_shadow_fk(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_dependent_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_shadow_fk(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_collection_composite_key(EntityState state)
        {
            base.Lazy_load_collection_composite_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_composite_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_composite_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_dependent_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_composite_key(state);
        }

        [ActianTodo]
        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(state);
            Assert.Equal("", Sql);
        }

        [ActianTodo]
        public override void Lazy_load_collection_for_detached_is_no_op()
        {
            base.Lazy_load_collection_for_detached_is_no_op();
        }

        [ActianTodo]
        public override void Lazy_load_reference_to_principal_for_detached_is_no_op()
        {
            base.Lazy_load_reference_to_principal_for_detached_is_no_op();
        }

        [ActianTodo]
        public override void Lazy_load_reference_to_dependent_for_detached_is_no_op()
        {
            base.Lazy_load_reference_to_dependent_for_detached_is_no_op();
        }

        [ActianTodo]
        public override void Lazy_load_collection_for_no_tracking_throws()
        {
            base.Lazy_load_collection_for_no_tracking_throws();
        }

        [ActianTodo]
        public override void Lazy_load_reference_to_principal_for_no_tracking_throws()
        {
            base.Lazy_load_reference_to_principal_for_no_tracking_throws();
        }

        [ActianTodo]
        public override void Lazy_load_reference_to_dependent_for_no_tracking_throws()
        {
            base.Lazy_load_reference_to_dependent_for_no_tracking_throws();
        }

        [ActianTodo]
        public override void Lazy_load_collection_for_no_tracking_does_not_throw_if_populated()
        {
            base.Lazy_load_collection_for_no_tracking_does_not_throw_if_populated();
        }

        [ActianTodo]
        public override void Lazy_load_reference_to_principal_for_no_tracking_does_not_throw_if_populated()
        {
            base.Lazy_load_reference_to_principal_for_no_tracking_does_not_throw_if_populated();
        }

        [ActianTodo]
        public override void Lazy_load_reference_to_dependent_for_no_does_not_throw_if_populated()
        {
            base.Lazy_load_reference_to_dependent_for_no_does_not_throw_if_populated();
        }

        [ActianTodo]
        public override async Task Load_collection(EntityState state, bool async)
        {
            await base.Load_collection(state, async);
            if (!async)
            {
                // AssertSql(@"
                //     @__p_0='707' (Nullable = true)
                //
                //     SELECT [e].[Id], [e].[ParentId]
                //     FROM [Child] AS [e]
                //     WHERE [e].[ParentId] = @__p_0
                // ");
            }
        }

        [ActianTodo]
        public override void Lazy_loading_finds_correct_entity_type_with_already_loaded_owned_types()
        {
            base.Lazy_loading_finds_correct_entity_type_with_already_loaded_owned_types();
        }

        [ActianTodo]
        public override void Lazy_loading_finds_correct_entity_type_with_multiple_queries()
        {
            base.Lazy_loading_finds_correct_entity_type_with_multiple_queries();
        }

        [ActianTodo]
        public override void Lazy_loading_finds_correct_entity_type_with_opaque_predicate_and_multiple_queries()
        {
            base.Lazy_loading_finds_correct_entity_type_with_opaque_predicate_and_multiple_queries();
        }

        [ActianTodo]
        public override void Lazy_loading_finds_correct_entity_type_with_multiple_queries_using_Count()
        {
            base.Lazy_loading_finds_correct_entity_type_with_multiple_queries_using_Count();
        }

        [ActianTodo]
        public override void Lazy_loading_shares_service__property_on_derived_types()
        {
            base.Lazy_loading_shares_service__property_on_derived_types();
        }

        [ActianTodo]
        public override void Lazy_loading_finds_correct_entity_type_with_alternate_model()
        {
            base.Lazy_loading_finds_correct_entity_type_with_alternate_model();
        }

        [ActianTodo]
        public override void Top_level_projection_track_entities_before_passing_to_client_method()
        {
            base.Top_level_projection_track_entities_before_passing_to_client_method();
            AssertSql(@"
                @__p_0='707' (Nullable = true)
            
                SELECT [e].[Id], [e].[ParentId]
                FROM [Child] AS [e]
                WHERE [e].[ParentId] = @__p_0
            ");
        }

        protected override void ClearLog() => Fixture.TestSqlLoggerFactory.Clear();

        protected override void RecordLog() => Sql = Fixture.TestSqlLoggerFactory.Sql;

        private string Sql { get; set; }

        private void AssertSql(string expected) => Sql.Should().NotDifferFrom(expected);

        public class LoadActianFixture : LoadFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
        }
    }
}
