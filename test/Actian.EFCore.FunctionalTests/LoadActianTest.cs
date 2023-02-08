using System.Collections.Generic;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore
{
    public class LoadActianTest : LoadTestBase<LoadActianTest.LoadActianFixture>
    {
        public LoadActianTest(LoadActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();


        public override void Attached_references_to_principal_are_marked_as_loaded(EntityState state, bool lazy)
        {
            base.Attached_references_to_principal_are_marked_as_loaded(state, lazy);
        }


        public override void Attached_references_to_dependents_are_marked_as_loaded(EntityState state, bool lazy)
        {
            base.Attached_references_to_dependents_are_marked_as_loaded(state, lazy);
        }


        public override void Attached_collections_are_not_marked_as_loaded(EntityState state, bool lazy)
        {
            base.Attached_collections_are_not_marked_as_loaded(state, lazy);
        }


        public override void Lazy_load_collection(EntityState state)
        {
            base.Lazy_load_collection(state);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal(state);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_principal(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal(state);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_dependent(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent(state);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal(state);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(state);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""s"".""Id""
                FROM ""SinglePkToPk"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK(state);
            AssertSql(@"");
        }


        public override void Lazy_load_collection_not_found(EntityState state)
        {
            base.Lazy_load_collection_not_found(state);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_not_found(state);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_not_found(state);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_not_found(state);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Lazy_load_collection_already_loaded(EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_collection_already_loaded(state, cascadeDeleteTiming);
            AssertSql(@"");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_already_loaded(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_already_loaded(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_already_loaded(
            EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_already_loaded(state, cascadeDeleteTiming);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(state);
            AssertSql(@"");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_alternate_key(state);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""AlternateId"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_alternate_key(state);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""AlternateId"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_alternate_key(state);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""SingleAk"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(state);
            AssertSql(@"");
        }


        public override void Lazy_load_collection_shadow_fk(EntityState state)
        {
            base.Lazy_load_collection_shadow_fk(state);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""ChildShadowFk"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_shadow_fk(state);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_shadow_fk(state);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_shadow_fk(state);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""SingleShadowFk"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(state);
            AssertSql(@"");
        }


        public override void Lazy_load_collection_composite_key(EntityState state)
        {
            base.Lazy_load_collection_composite_key(state);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentAlternateId"", ""c"".""ParentId""
                FROM ""ChildCompositeKey"" AS ""c""
                WHERE (""c"".""ParentAlternateId"" = @__p_0) AND (""c"".""ParentId"" = @__p_1)
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_composite_key(state);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE (""p"".""AlternateId"" = @__p_0) AND (""p"".""Id"" = @__p_1)
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_composite_key(state);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE (""p"".""AlternateId"" = @__p_0) AND (""p"".""Id"" = @__p_1)
            ");
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_composite_key(state);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentAlternateId"", ""s"".""ParentId""
                FROM ""SingleCompositeKey"" AS ""s""
                WHERE (""s"".""ParentAlternateId"" = @__p_0) AND (""s"".""ParentId"" = @__p_1)
            ");
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(state);
            AssertSql(@"");
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(state);
            AssertSql(@"");
        }


        public override void Lazy_load_collection_for_detached_throws(bool noTracking)
        {
            base.Lazy_load_collection_for_detached_throws(noTracking);
        }


        public override void Lazy_load_reference_to_principal_for_detached_throws(bool noTracking)
        {
            base.Lazy_load_reference_to_principal_for_detached_throws(noTracking);
        }


        public override void Lazy_load_reference_to_dependent_for_detached_throws(bool noTracking)
        {
            base.Lazy_load_reference_to_dependent_for_detached_throws(noTracking);
        }


        public override void Lazy_loading_uses_field_access_when_abstract_base_class_navigation()
        {
            base.Lazy_loading_uses_field_access_when_abstract_base_class_navigation();
        }


        public override async Task Load_collection(EntityState state, bool async)
        {
            await base.Load_collection(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override Task Load_collection_with_NoTracking_behavior(EntityState state, bool async)
        {
            return base.Load_collection_with_NoTracking_behavior(state, async);
        }


        public override async Task Load_many_to_one_reference_to_principal(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override Task Load_one_to_one_reference_to_principal_when_NoTracking_behavior(EntityState state, bool async)
        {
            return base.Load_one_to_one_reference_to_principal_when_NoTracking_behavior(state, async);
        }


        public override async Task Load_one_to_one_reference_to_dependent(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_principal(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_principal(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_dependent(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_dependent(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""s"".""Id""
                FROM ""SinglePkToPk"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_collection_using_Query(EntityState state, bool async)
        {
            await base.Load_collection_using_Query(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT FIRST 2 ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_principal_using_Query(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_principal_using_Query(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_dependent_using_Query(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_dependent_using_Query(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""s"".""Id""
                FROM ""SinglePkToPk"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_null_FK(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_null_FK(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_principal_null_FK(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_null_FK(state, async);
            AssertSql(@"");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_null_FK(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_null_FK(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_null_FK(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_null_FK(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_collection_not_found(EntityState state, bool async)
        {
            await base.Load_collection_not_found(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_not_found(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_not_found(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_not_found(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_not_found(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_not_found(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_not_found(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_using_Query_not_found(EntityState state, bool async)
        {
            await base.Load_collection_using_Query_not_found(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_not_found(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_not_found(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_not_found(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_not_found(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_not_found(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_not_found(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT FIRST 2 ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Load_collection_already_loaded(EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_collection_already_loaded(state, async, cascadeDeleteTiming);
            AssertSql(@"");
        }


        public override async Task Load_many_to_one_reference_to_principal_already_loaded(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_already_loaded(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_principal_already_loaded(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_one_to_one_reference_to_principal_already_loaded(state, async, cascadeDeleteTiming);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_dependent_already_loaded(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_one_to_one_reference_to_dependent_already_loaded(state, async, cascadeDeleteTiming);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(state, async);
            AssertSql(@"");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Load_collection_using_Query_already_loaded(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_collection_using_Query_already_loaded(state, async, cascadeDeleteTiming);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_already_loaded(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_already_loaded(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_already_loaded(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_already_loaded(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_already_loaded(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_already_loaded(state, async, cascadeDeleteTiming);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT FIRST 2 ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_principal_using_Query_already_loaded(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_principal_using_Query_already_loaded(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_PK_to_PK_reference_to_dependent_using_Query_already_loaded(EntityState state, bool async)
        {
            await base.Load_one_to_one_PK_to_PK_reference_to_dependent_using_Query_already_loaded(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""s"".""Id""
                FROM ""SinglePkToPk"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_collection_untyped(EntityState state, bool async)
        {
            await base.Load_collection_untyped(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_untyped(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_untyped(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_untyped(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_untyped(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_using_Query_untyped(EntityState state, bool async)
        {
            await base.Load_collection_using_Query_untyped(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_untyped(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_untyped(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_untyped(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_untyped(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_collection_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_using_Query_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_collection_using_Query_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='787'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_not_found_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_not_found_untyped(state, async);
            AssertSql(@"
                @__p_0='767' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Load_collection_already_loaded_untyped(EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_collection_already_loaded_untyped(state, async, cascadeDeleteTiming);
            AssertSql(@"");
        }


        public override async Task Load_many_to_one_reference_to_principal_already_loaded_untyped(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_already_loaded_untyped(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_principal_already_loaded_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_already_loaded_untyped(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_dependent_already_loaded_untyped(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_one_to_one_reference_to_dependent_already_loaded_untyped(state, async, cascadeDeleteTiming);
            AssertSql(@"");
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override async Task Load_collection_using_Query_already_loaded_untyped(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_collection_using_Query_already_loaded_untyped(state, async, cascadeDeleteTiming);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""Child"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_already_loaded_untyped(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_already_loaded_untyped(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_already_loaded_untyped(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_already_loaded_untyped(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_already_loaded_untyped(
            EntityState state, bool async, CascadeTiming cascadeDeleteTiming)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_already_loaded_untyped(state, async, cascadeDeleteTiming);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""Single"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_alternate_key(EntityState state, bool async)
        {
            await base.Load_collection_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""ChildAk"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_alternate_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""AlternateId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_alternate_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""AlternateId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_alternate_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""SingleAk"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_using_Query_alternate_key(EntityState state, bool async)
        {
            await base.Load_collection_using_Query_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""ChildAk"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_alternate_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""AlternateId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_alternate_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""AlternateId"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_alternate_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_alternate_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                
                SELECT FIRST 2 ""s"".""Id"", ""s"".""ParentId""
                FROM ""SingleAk"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_null_FK_alternate_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_null_FK_alternate_key(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_principal_null_FK_alternate_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_null_FK_alternate_key(state, async);
            AssertSql(@"");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_null_FK_alternate_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_null_FK_alternate_key(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_null_FK_alternate_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_null_FK_alternate_key(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_collection_shadow_fk(EntityState state, bool async)
        {
            await base.Load_collection_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""ChildShadowFk"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_shadow_fk(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_shadow_fk(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_shadow_fk(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentId""
                FROM ""SingleShadowFk"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_collection_using_Query_shadow_fk(EntityState state, bool async)
        {
            await base.Load_collection_using_Query_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentId""
                FROM ""ChildShadowFk"" AS ""c""
                WHERE ""c"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_shadow_fk(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_shadow_fk(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE ""p"".""Id"" = @__p_0
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_shadow_fk(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_shadow_fk(state, async);
            AssertSql(@"
                @__p_0='707' (Nullable = true)
                
                SELECT FIRST 2 ""s"".""Id"", ""s"".""ParentId""
                FROM ""SingleShadowFk"" AS ""s""
                WHERE ""s"".""ParentId"" = @__p_0
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_null_FK_shadow_fk(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_null_FK_shadow_fk(state, async);
            AssertSql(@"");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_null_FK_shadow_fk(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_null_FK_shadow_fk(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_null_FK_shadow_fk(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_null_FK_shadow_fk(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_collection_composite_key(EntityState state, bool async)
        {
            await base.Load_collection_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentAlternateId"", ""c"".""ParentId""
                FROM ""ChildCompositeKey"" AS ""c""
                WHERE (""c"".""ParentAlternateId"" = @__p_0) AND (""c"".""ParentId"" = @__p_1)
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_composite_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE (""p"".""AlternateId"" = @__p_0) AND (""p"".""Id"" = @__p_1)
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_composite_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707'
                
                SELECT ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE (""p"".""AlternateId"" = @__p_0) AND (""p"".""Id"" = @__p_1)
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_composite_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707' (Nullable = true)
                
                SELECT ""s"".""Id"", ""s"".""ParentAlternateId"", ""s"".""ParentId""
                FROM ""SingleCompositeKey"" AS ""s""
                WHERE (""s"".""ParentAlternateId"" = @__p_0) AND (""s"".""ParentId"" = @__p_1)
            ");
        }


        public override async Task Load_collection_using_Query_composite_key(EntityState state, bool async)
        {
            await base.Load_collection_using_Query_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707' (Nullable = true)
                
                SELECT ""c"".""Id"", ""c"".""ParentAlternateId"", ""c"".""ParentId""
                FROM ""ChildCompositeKey"" AS ""c""
                WHERE (""c"".""ParentAlternateId"" = @__p_0) AND (""c"".""ParentId"" = @__p_1)
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_composite_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE (""p"".""AlternateId"" = @__p_0) AND (""p"".""Id"" = @__p_1)
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_composite_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707'
                
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE (""p"".""AlternateId"" = @__p_0) AND (""p"".""Id"" = @__p_1)
            ");
        }


        public override async Task Load_one_to_one_reference_to_dependent_using_Query_composite_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_dependent_using_Query_composite_key(state, async);
            AssertSql(@"
                @__p_0='Root'
                @__p_1='707' (Nullable = true)
                
                SELECT FIRST 2 ""s"".""Id"", ""s"".""ParentAlternateId"", ""s"".""ParentId""
                FROM ""SingleCompositeKey"" AS ""s""
                WHERE (""s"".""ParentAlternateId"" = @__p_0) AND (""s"".""ParentId"" = @__p_1)
            ");
        }


        public override async Task Load_many_to_one_reference_to_principal_null_FK_composite_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_null_FK_composite_key(state, async);
            AssertSql(@"");
        }


        public override async Task Load_one_to_one_reference_to_principal_null_FK_composite_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_null_FK_composite_key(state, async);
            AssertSql(@"");
        }


        public override async Task Load_many_to_one_reference_to_principal_using_Query_null_FK_composite_key(EntityState state, bool async)
        {
            await base.Load_many_to_one_reference_to_principal_using_Query_null_FK_composite_key(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override async Task Load_one_to_one_reference_to_principal_using_Query_null_FK_composite_key(EntityState state, bool async)
        {
            await base.Load_one_to_one_reference_to_principal_using_Query_null_FK_composite_key(state, async);
            AssertSql(@"
                SELECT FIRST 2 ""p"".""Id"", ""p"".""AlternateId""
                FROM ""Parent"" AS ""p""
                WHERE FALSE = TRUE
            ");
        }


        public override void Can_change_IsLoaded_flag_for_collection()
        {
            base.Can_change_IsLoaded_flag_for_collection();
        }


        public override void Can_change_IsLoaded_flag_for_reference_only_if_null()
        {
            base.Can_change_IsLoaded_flag_for_reference_only_if_null();
        }


        public override Task Load_collection_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_collection_for_detached_throws(async, noTracking);
        }


        public override Task Load_collection_using_string_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_collection_using_string_for_detached_throws(async, noTracking);
        }


        public override Task Load_collection_with_navigation_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_collection_with_navigation_for_detached_throws(async, noTracking);
        }


        public override Task Load_reference_to_principal_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_reference_to_principal_for_detached_throws(async, noTracking);
        }


        public override Task Load_reference_with_navigation_to_principal_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_reference_with_navigation_to_principal_for_detached_throws(async, noTracking);
        }


        public override Task Load_reference_using_string_to_principal_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_reference_using_string_to_principal_for_detached_throws(async, noTracking);
        }


        public override Task Load_reference_to_dependent_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_reference_to_dependent_for_detached_throws(async, noTracking);
        }


        public override Task Load_reference_to_dependent_with_navigation_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_reference_to_dependent_with_navigation_for_detached_throws(async, noTracking);
        }


        public override Task Load_reference_to_dependent_using_string_for_detached_throws(bool async, bool noTracking)
        {
            return base.Load_reference_to_dependent_using_string_for_detached_throws(async, noTracking);
        }


        public override void Query_collection_for_detached_throws(bool noTracking)
        {
            base.Query_collection_for_detached_throws(noTracking);
        }


        public override void Query_collection_using_string_for_detached_throws(bool noTracking)
        {
            base.Query_collection_using_string_for_detached_throws(noTracking);
        }


        public override void Query_collection_with_navigation_for_detached_throws(bool noTracking)
        {
            base.Query_collection_with_navigation_for_detached_throws(noTracking);
        }


        public override void Query_reference_to_principal_for_detached_throws(bool noTracking)
        {
            base.Query_reference_to_principal_for_detached_throws(noTracking);
        }


        public override void Query_reference_with_navigation_to_principal_for_detached_throws(bool noTracking)
        {
            base.Query_reference_with_navigation_to_principal_for_detached_throws(noTracking);
        }


        public override void Query_reference_using_string_to_principal_for_detached_throws(bool noTracking)
        {
            base.Query_reference_using_string_to_principal_for_detached_throws(noTracking);
        }


        public override void Query_reference_to_dependent_for_detached_throws(bool noTracking)
        {
            base.Query_reference_to_dependent_for_detached_throws(noTracking);
        }


        public override void Query_reference_to_dependent_with_navigation_for_detached_throws(bool noTracking)
        {
            base.Query_reference_to_dependent_with_navigation_for_detached_throws(noTracking);
        }


        public override void Query_reference_to_dependent_using_string_for_detached_throws(bool noTracking)
        {
            base.Query_reference_to_dependent_using_string_for_detached_throws(noTracking);
        }

        private string Sql { get; set; }

        protected override void ClearLog() => Fixture.TestSqlLoggerFactory.Clear();

        protected override void RecordLog() => Sql = Fixture.TestSqlLoggerFactory.Sql;

        private void AssertSql(string expected) => Sql.Should().NotDifferFrom(expected);

        public class LoadActianFixture : LoadFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override void Seed(PoolableDbContext context)
            {
                context.Add(
                    new Parent
                    {
                        Id = 707,
                        AlternateId = "Root",
                        Children = new List<Child> { new Child { Id = 11 }, new Child { Id = 12 } },
                        SinglePkToPk = new SinglePkToPk { Id = 707 },
                        Single = new Single { Id = 21 },
                        ChildrenAk = new List<ChildAk> { new ChildAk { Id = 31 }, new ChildAk { Id = 32 } },
                        SingleAk = new SingleAk { Id = 42 },
                        ChildrenShadowFk = new List<ChildShadowFk> { new ChildShadowFk { Id = 51 }, new ChildShadowFk { Id = 52 } },
                        SingleShadowFk = new SingleShadowFk { Id = 62 },
                        ChildrenCompositeKey = new List<ChildCompositeKey>
                        {
                            new ChildCompositeKey { Id = 51 }, new ChildCompositeKey { Id = 52 }
                        },
                        SingleCompositeKey = new SingleCompositeKey { Id = 62 }
                    });

                context.Add(
                    new SimpleProduct { Id = 142, Deposit = new Deposit { Id = 143 } });

                context.SaveChanges();
            }
        }
    }
}
