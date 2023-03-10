using System.Collections.Generic;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;
using static Actian.EFCore.TestUtilities.ActianSkipReasons;

namespace Actian.EFCore
{
    public class LazyLoadProxyActianTest : LazyLoadProxyTestBase<LazyLoadProxyActianTest.LoadActianFixture>
    {
        public LazyLoadProxyActianTest(LoadActianFixture fixture, ITestOutputHelper testOutputHelper)
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


        public override void Lazy_load_collection(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_collection(state, useAttach, useDetach);
        }


        public override void Lazy_load_many_to_one_reference_to_principal(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_many_to_one_reference_to_principal(state, useAttach, useDetach);
        }


        public override void Lazy_load_one_to_one_reference_to_principal(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_one_to_one_reference_to_principal(state, useAttach, useDetach);
        }


        public override void Lazy_load_one_to_one_reference_to_dependent(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_one_to_one_reference_to_dependent(state, useAttach, useDetach);
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal(state);
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_changed_non_found_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_changed_non_found_FK(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_changed_found_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_changed_found_FK(state);
        }


        public override void Lazy_load_collection_not_found(EntityState state)
        {
            base.Lazy_load_collection_not_found(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_not_found(state);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_not_found(state);
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_not_found(state);
        }


        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Lazy_load_collection_already_loaded(EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_collection_already_loaded(state, cascadeDeleteTiming);
        }

        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Lazy_load_many_to_one_reference_to_principal_already_loaded(
            EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_many_to_one_reference_to_principal_already_loaded(state, cascadeDeleteTiming);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_already_loaded(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_already_loaded(
            EntityState state, CascadeTiming cascadeDeleteTiming)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_already_loaded(state, cascadeDeleteTiming);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_alternate_key(state);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_alternate_key(state);
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_alternate_key(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_collection_shadow_fk(EntityState state)
        {
            base.Lazy_load_collection_shadow_fk(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_shadow_fk(state);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_shadow_fk(state);
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_shadow_fk(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_collection_composite_key(EntityState state)
        {
            base.Lazy_load_collection_composite_key(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_composite_key(state);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_composite_key(state);
        }


        public override void Lazy_load_one_to_one_reference_to_dependent_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_composite_key(state);
        }


        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(state);
            Assert.Equal("", Sql);
        }


        public override void Lazy_load_collection_for_detached_is_no_op()
        {
            base.Lazy_load_collection_for_detached_is_no_op();
        }


        public override void Lazy_load_reference_to_principal_for_detached_is_no_op()
        {
            base.Lazy_load_reference_to_principal_for_detached_is_no_op();
        }


        public override void Lazy_load_reference_to_dependent_for_detached_is_no_op()
        {
            base.Lazy_load_reference_to_dependent_for_detached_is_no_op();
        }


        public override void Lazy_load_collection_for_no_tracking_throws()
        {
            base.Lazy_load_collection_for_no_tracking_throws();
        }


        public override void Lazy_load_reference_to_principal_for_no_tracking_throws()
        {
            base.Lazy_load_reference_to_principal_for_no_tracking_throws();
        }


        public override void Lazy_load_reference_to_dependent_for_no_tracking_throws()
        {
            base.Lazy_load_reference_to_dependent_for_no_tracking_throws();
        }


        [ActianSkip(NoOrderByOffsetFirstAndFetchInSubselects)]
        public override void Lazy_load_collection_for_no_tracking_does_not_throw_if_populated()
        {
            base.Lazy_load_collection_for_no_tracking_does_not_throw_if_populated();
        }


        public override void Lazy_load_reference_to_principal_for_no_tracking_does_not_throw_if_populated()
        {
            base.Lazy_load_reference_to_principal_for_no_tracking_does_not_throw_if_populated();
        }


        public override void Lazy_load_reference_to_dependent_for_no_does_not_throw_if_populated()
        {
            base.Lazy_load_reference_to_dependent_for_no_does_not_throw_if_populated();
        }


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


        public override void Lazy_loading_finds_correct_entity_type_with_already_loaded_owned_types()
        {
            base.Lazy_loading_finds_correct_entity_type_with_already_loaded_owned_types();
        }


        public override void Lazy_loading_finds_correct_entity_type_with_multiple_queries()
        {
            base.Lazy_loading_finds_correct_entity_type_with_multiple_queries();
        }


        public override void Lazy_loading_finds_correct_entity_type_with_opaque_predicate_and_multiple_queries()
        {
            base.Lazy_loading_finds_correct_entity_type_with_opaque_predicate_and_multiple_queries();
        }


        public override void Lazy_loading_finds_correct_entity_type_with_multiple_queries_using_Count()
        {
            base.Lazy_loading_finds_correct_entity_type_with_multiple_queries_using_Count();
        }


        public override void Lazy_loading_shares_service__property_on_derived_types()
        {
            base.Lazy_loading_shares_service__property_on_derived_types();
        }


        public override void Lazy_loading_finds_correct_entity_type_with_alternate_model()
        {
            base.Lazy_loading_finds_correct_entity_type_with_alternate_model();
        }


        public override void Top_level_projection_track_entities_before_passing_to_client_method()
        {
            base.Top_level_projection_track_entities_before_passing_to_client_method();
            //AssertSql(@"
            //    @__p_0='707' (Nullable = true)
            
            //    SELECT [e].[Id], [e].[ParentId]
            //    FROM [Child] AS [e]
            //    WHERE [e].[ParentId] = @__p_0
            //");
        }

        protected override void ClearLog() => Fixture.TestSqlLoggerFactory.Clear();

        protected override void RecordLog() => Sql = Fixture.TestSqlLoggerFactory.Sql;

        private string Sql { get; set; }

        private void AssertSql(string expected) => Sql.Should().NotDifferFrom(expected);

        public class LoadActianFixture : LoadFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            //protected override void Seed(DbContext context)
            //{
            //    base.Seed(context);
            //}

            protected override void Seed(DbContext context)
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
                    new Blog
                    {
                        Id = 1,
                        Writer = new Person { FirstName = "firstNameWriter0", LastName = "lastNameWriter0" },
                        Reader = new Person { FirstName = "firstNameReader0", LastName = "lastNameReader0" },
                        Host = new Host { HostName = "127.0.0.1" }
                    });

                context.Add(
                    new Blog
                    {
                        Id = 2,
                        Writer = new Person { FirstName = "firstNameWriter1", LastName = "lastNameWriter1" },
                        Reader = new Person { FirstName = "firstNameReader1", LastName = "lastNameReader1" },
                        Host = new Host { HostName = "127.0.0.2" }
                    });

                context.Add(
                    new Blog
                    {
                        Id = 3,
                        Writer = new Person { FirstName = "firstNameWriter2", LastName = "lastNameWriter2" },
                        Reader = new Person { FirstName = "firstNameReader2", LastName = "lastNameReader2" },
                        Host = new Host { HostName = "127.0.0.3" }
                    });

                var nose1 = new Nose { Id = 1, Size = "Small" };

                var nose2 = new Nose { Id = 2, Size = "Medium" };

                var nose3 = new Nose { Id = 3, Size = "Large" };

                context.Add(
                    new Entity
                    {
                        Id = 1,
                        BaseNoses = new List<Nose> { nose1, nose2, nose3 }
                    });

                context.Add(
                    new Parson { Id = 2, ParsonNoses = new List<Nose> { nose2, nose3 } });

                context.Add(
                    new Company { Id = 3, CompanyNoses = new List<Nose> { nose1, nose3 } });

                context.Add(
                    new Applicant(new FullName(FirstName.Create("Amila"), LastName.Create("Udayanga")))
                    {
                        ApplicantId = 1
                    });

                context.Add(
                    new Pyrson(new FullName(FirstName.Create("Amila"), LastName.Create("Udayanga")))
                    {
                        PyrsonId = 1,
                        Address = new Address { AddressId = 1, Line1 = "Line1", Line2 = "Line2" }
                    });

                context.SaveChanges();
            }
        }
    }
}
