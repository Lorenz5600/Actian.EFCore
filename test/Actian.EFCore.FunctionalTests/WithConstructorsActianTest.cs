using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class WithConstructorsActianTest : WithConstructorsTestBase<WithConstructorsActianTest.WithConstructorsActianFixture>
    {
        public WithConstructorsActianTest(WithConstructorsActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Query_and_update_using_constructors_with_property_parameters()
        {
            base.Query_and_update_using_constructors_with_property_parameters();
        }

        [ActianTodo]
        public override void Query_with_keyless_type()
        {
            base.Query_with_keyless_type();
        }

        [ActianTodo]
        public override void Query_with_context_injected()
        {
            base.Query_with_context_injected();
        }

        [ActianTodo]
        public override void Query_with_context_injected_into_property()
        {
            base.Query_with_context_injected_into_property();
        }

        [ActianTodo]
        public override void Query_with_context_injected_into_constructor_with_property()
        {
            base.Query_with_context_injected_into_constructor_with_property();
        }

        [ActianTodo]
        public override void Attaching_entity_sets_context()
        {
            base.Attaching_entity_sets_context();
        }

        [ActianTodo]
        public override void Query_with_EntityType_injected()
        {
            base.Query_with_EntityType_injected();
        }

        [ActianTodo]
        public override void Query_with_EntityType_injected_into_property()
        {
            base.Query_with_EntityType_injected_into_property();
        }

        [ActianTodo]
        public override void Query_with_EntityType_injected_into_constructor_with_property()
        {
            base.Query_with_EntityType_injected_into_constructor_with_property();
        }

        [ActianTodo]
        public override void Attaching_entity_sets_EntityType()
        {
            base.Attaching_entity_sets_EntityType();
        }

        [ActianTodo]
        public override void Query_with_StateManager_injected()
        {
            base.Query_with_StateManager_injected();
        }

        [ActianTodo]
        public override void Query_with_StateManager_injected_into_property()
        {
            base.Query_with_StateManager_injected_into_property();
        }

        [ActianTodo]
        public override void Query_with_StateManager_injected_into_constructor_with_property()
        {
            base.Query_with_StateManager_injected_into_constructor_with_property();
        }

        [ActianTodo]
        public override void Attaching_entity_sets_StateManager()
        {
            base.Attaching_entity_sets_StateManager();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_for_reference()
        {
            base.Query_with_loader_injected_for_reference();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_for_collections()
        {
            base.Query_with_loader_injected_for_collections();
        }

        [ActianTodo]
        public override Task Query_with_loader_injected_for_reference_async()
        {
            return base.Query_with_loader_injected_for_reference_async();
        }

        [ActianTodo]
        public override Task Query_with_loader_injected_for_collections_async()
        {
            return base.Query_with_loader_injected_for_collections_async();
        }

        [ActianTodo]
        public override void Query_with_POCO_loader_injected_for_reference()
        {
            base.Query_with_POCO_loader_injected_for_reference();
        }

        [ActianTodo]
        public override void Query_with_POCO_loader_injected_for_collections()
        {
            base.Query_with_POCO_loader_injected_for_collections();
        }

        [ActianTodo]
        public override Task Query_with_loader_delegate_injected_for_reference_async()
        {
            return base.Query_with_loader_delegate_injected_for_reference_async();
        }

        [ActianTodo]
        public override Task Query_with_loader_delegate_injected_for_collections_async()
        {
            return base.Query_with_loader_delegate_injected_for_collections_async();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_into_property_for_reference()
        {
            base.Query_with_loader_injected_into_property_for_reference();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_into_property_for_collections()
        {
            base.Query_with_loader_injected_into_property_for_collections();
        }

        [ActianTodo]
        public override void Attaching_entity_sets_lazy_loader()
        {
            base.Attaching_entity_sets_lazy_loader();
        }

        [ActianTodo]
        public override void Detaching_entity_resets_lazy_loader_so_it_can_be_reattached()
        {
            base.Detaching_entity_resets_lazy_loader_so_it_can_be_reattached();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_into_field_for_reference()
        {
            base.Query_with_loader_injected_into_field_for_reference();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_into_field_for_collections()
        {
            base.Query_with_loader_injected_into_field_for_collections();
        }

        [ActianTodo]
        public override void Attaching_entity_sets_lazy_loader_field()
        {
            base.Attaching_entity_sets_lazy_loader_field();
        }

        [ActianTodo]
        public override void Detaching_entity_resets_lazy_loader_field_so_it_can_be_reattached()
        {
            base.Detaching_entity_resets_lazy_loader_field_so_it_can_be_reattached();
        }

        [ActianTodo]
        public override void Attaching_entity_sets_lazy_loader_delegate()
        {
            base.Attaching_entity_sets_lazy_loader_delegate();
        }

        [ActianTodo]
        public override void Detaching_entity_resets_lazy_loader_delegate_so_it_can_be_reattached()
        {
            base.Detaching_entity_resets_lazy_loader_delegate_so_it_can_be_reattached();
        }

        [ActianTodo]
        public override void Query_with_loader_delegate_injected_into_property_for_reference()
        {
            base.Query_with_loader_delegate_injected_into_property_for_reference();
        }

        [ActianTodo]
        public override void Query_with_loader_delgate_injected_into_property_for_collections()
        {
            base.Query_with_loader_delgate_injected_into_property_for_collections();
        }

        [ActianTodo]
        public override Task Query_with_loader_delegate_injected_into_property_for_reference_async()
        {
            return base.Query_with_loader_delegate_injected_into_property_for_reference_async();
        }

        [ActianTodo]
        public override Task Query_with_loader_delegate_injected_into_property_for_collections_async()
        {
            return base.Query_with_loader_delegate_injected_into_property_for_collections_async();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_into_property_via_constructor_for_reference()
        {
            base.Query_with_loader_injected_into_property_via_constructor_for_reference();
        }

        [ActianTodo]
        public override void Query_with_loader_injected_into_property_via_constructor_for_collections()
        {
            base.Query_with_loader_injected_into_property_via_constructor_for_collections();
        }

        [ActianTodo]
        public override void Query_with_loader_delegate_injected_into_property_via_constructor_for_reference()
        {
            base.Query_with_loader_delegate_injected_into_property_via_constructor_for_reference();
        }

        [ActianTodo]
        public override void Query_with_loader_delegate_injected_into_property_via_constructor_for_collections()
        {
            base.Query_with_loader_delegate_injected_into_property_via_constructor_for_collections();
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class WithConstructorsActianFixture : WithConstructorsFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<BlogQuery>().HasNoKey().ToQuery(
                    () => context.Set<BlogQuery>().FromSqlRaw("SELECT * FROM Blog"));
            }
        }
    }
}
