using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class FieldMappingActianTest : FieldMappingTestBase<FieldMappingActianTest.FieldMappingActianFixture>
    {
        protected FieldMappingActianTest(FieldMappingActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Field_mapping_with_conversion_does_not_throw()
        {
            base.Field_mapping_with_conversion_does_not_throw();
        }

        [ActianTodo]
        public override void Simple_query_auto_props(bool tracking)
        {
            base.Simple_query_auto_props(tracking);
        }

        [ActianTodo]
        public override void Include_collection_auto_props(bool tracking)
        {
            base.Include_collection_auto_props(tracking);
        }

        [ActianTodo]
        public override void Include_reference_auto_props(bool tracking)
        {
            base.Include_reference_auto_props(tracking);
        }

        [ActianTodo]
        public override void Load_collection_auto_props()
        {
            base.Load_collection_auto_props();
        }

        [ActianTodo]
        public override void Load_reference_auto_props()
        {
            base.Load_reference_auto_props();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_auto_props(bool tracking)
        {
            base.Query_with_conditional_constant_auto_props(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_auto_props(bool tracking)
        {
            base.Query_with_conditional_param_auto_props(tracking);
        }

        [ActianTodo]
        public override void Projection_auto_props(bool tracking)
        {
            base.Projection_auto_props(tracking);
        }

        [ActianTodo]
        public override void Update_auto_props()
        {
            base.Update_auto_props();
        }

        [ActianTodo]
        public override void Simple_query_hiding_props(bool tracking)
        {
            base.Simple_query_hiding_props(tracking);
        }

        [ActianTodo]
        public override void Include_collection_hiding_props(bool tracking)
        {
            base.Include_collection_hiding_props(tracking);
        }

        [ActianTodo]
        public override void Include_reference_hiding_props(bool tracking)
        {
            base.Include_reference_hiding_props(tracking);
        }

        [ActianTodo]
        public override void Load_collection_hiding_props()
        {
            base.Load_collection_hiding_props();
        }

        [ActianTodo]
        public override void Load_reference_hiding_props()
        {
            base.Load_reference_hiding_props();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_hiding_props(bool tracking)
        {
            base.Query_with_conditional_constant_hiding_props(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_hiding_props(bool tracking)
        {
            base.Query_with_conditional_param_hiding_props(tracking);
        }

        [ActianTodo]
        public override void Projection_hiding_props(bool tracking)
        {
            base.Projection_hiding_props(tracking);
        }

        [ActianTodo]
        public override void Update_hiding_props()
        {
            base.Update_hiding_props();
        }

        [ActianTodo]
        public override void Simple_query_full_props(bool tracking)
        {
            base.Simple_query_full_props(tracking);
        }

        [ActianTodo]
        public override void Include_collection_full_props(bool tracking)
        {
            base.Include_collection_full_props(tracking);
        }

        [ActianTodo]
        public override void Include_reference_full_props(bool tracking)
        {
            base.Include_reference_full_props(tracking);
        }

        [ActianTodo]
        public override void Load_collection_full_props()
        {
            base.Load_collection_full_props();
        }

        [ActianTodo]
        public override void Load_reference_full_props()
        {
            base.Load_reference_full_props();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_full_props(bool tracking)
        {
            base.Query_with_conditional_constant_full_props(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_full_props(bool tracking)
        {
            base.Query_with_conditional_param_full_props(tracking);
        }

        [ActianTodo]
        public override void Projection_full_props(bool tracking)
        {
            base.Projection_full_props(tracking);
        }

        [ActianTodo]
        public override void Update_full_props()
        {
            base.Update_full_props();
        }

        [ActianTodo]
        public override void Simple_query_full_props_with_named_fields(bool tracking)
        {
            base.Simple_query_full_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Include_collection_full_props_with_named_fields(bool tracking)
        {
            base.Include_collection_full_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Include_reference_full_props_with_named_fields(bool tracking)
        {
            base.Include_reference_full_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Load_collection_full_props_with_named_fields()
        {
            base.Load_collection_full_props_with_named_fields();
        }

        [ActianTodo]
        public override void Load_reference_full_props_with_named_fields()
        {
            base.Load_reference_full_props_with_named_fields();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_full_props_with_named_fields(bool tracking)
        {
            base.Query_with_conditional_constant_full_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_full_props_with_named_fields(bool tracking)
        {
            base.Query_with_conditional_param_full_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Projection_full_props_with_named_fields(bool tracking)
        {
            base.Projection_full_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Update_full_props_with_named_fields()
        {
            base.Update_full_props_with_named_fields();
        }

        [ActianTodo]
        public override void Simple_query_read_only_props(bool tracking)
        {
            base.Simple_query_read_only_props(tracking);
        }

        [ActianTodo]
        public override void Include_collection_read_only_props(bool tracking)
        {
            base.Include_collection_read_only_props(tracking);
        }

        [ActianTodo]
        public override void Include_reference_read_only_props(bool tracking)
        {
            base.Include_reference_read_only_props(tracking);
        }

        [ActianTodo]
        public override void Load_collection_read_only_props()
        {
            base.Load_collection_read_only_props();
        }

        [ActianTodo]
        public override void Load_reference_read_only_props()
        {
            base.Load_reference_read_only_props();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_read_only_props(bool tracking)
        {
            base.Query_with_conditional_constant_read_only_props(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_read_only_props(bool tracking)
        {
            base.Query_with_conditional_param_read_only_props(tracking);
        }

        [ActianTodo]
        public override void Projection_read_only_props(bool tracking)
        {
            base.Projection_read_only_props(tracking);
        }

        [ActianTodo]
        public override void Update_read_only_props()
        {
            base.Update_read_only_props();
        }

        [ActianTodo]
        public override void Simple_query_read_only_props_with_named_fields(bool tracking)
        {
            base.Simple_query_read_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Include_collection_read_only_props_with_named_fields(bool tracking)
        {
            base.Include_collection_read_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Include_reference_read_only_props_with_named_fields(bool tracking)
        {
            base.Include_reference_read_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Load_collection_read_only_props_with_named_fields()
        {
            base.Load_collection_read_only_props_with_named_fields();
        }

        [ActianTodo]
        public override void Load_reference_read_only_props_with_named_fields()
        {
            base.Load_reference_read_only_props_with_named_fields();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_read_only_props_with_named_fields(bool tracking)
        {
            base.Query_with_conditional_constant_read_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_read_only_props_with_named_fields(bool tracking)
        {
            base.Query_with_conditional_param_read_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Projection_read_only_props_with_named_fields(bool tracking)
        {
            base.Projection_read_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Update_read_only_props_with_named_fields()
        {
            base.Update_read_only_props_with_named_fields();
        }

        [ActianTodo]
        public override void Simple_query_write_only_props(bool tracking)
        {
            base.Simple_query_write_only_props(tracking);
        }

        [ActianTodo]
        public override void Include_collection_write_only_props(bool tracking)
        {
            base.Include_collection_write_only_props(tracking);
        }

        [ActianTodo]
        public override void Include_reference_write_only_props(bool tracking)
        {
            base.Include_reference_write_only_props(tracking);
        }

        [ActianTodo]
        public override void Load_collection_write_only_props()
        {
            base.Load_collection_write_only_props();
        }

        [ActianTodo]
        public override void Load_reference_write_only_props()
        {
            base.Load_reference_write_only_props();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_write_only_props(bool tracking)
        {
            base.Query_with_conditional_constant_write_only_props(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_write_only_props(bool tracking)
        {
            base.Query_with_conditional_param_write_only_props(tracking);
        }

        [ActianTodo]
        public override void Projection_write_only_props(bool tracking)
        {
            base.Projection_write_only_props(tracking);
        }

        [ActianTodo]
        public override void Update_write_only_props()
        {
            base.Update_write_only_props();
        }

        [ActianTodo]
        public override void Simple_query_write_only_props_with_named_fields(bool tracking)
        {
            base.Simple_query_write_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Include_collection_write_only_props_with_named_fields(bool tracking)
        {
            base.Include_collection_write_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Include_reference_write_only_props_with_named_fields(bool tracking)
        {
            base.Include_reference_write_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Load_collection_write_only_props_with_named_fields()
        {
            base.Load_collection_write_only_props_with_named_fields();
        }

        [ActianTodo]
        public override void Load_reference_write_only_props_with_named_fields()
        {
            base.Load_reference_write_only_props_with_named_fields();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_write_only_props_with_named_fields(bool tracking)
        {
            base.Query_with_conditional_constant_write_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_write_only_props_with_named_fields(bool tracking)
        {
            base.Query_with_conditional_param_write_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Projection_write_only_props_with_named_fields(bool tracking)
        {
            base.Projection_write_only_props_with_named_fields(tracking);
        }

        [ActianTodo]
        public override void Update_write_only_props_with_named_fields()
        {
            base.Update_write_only_props_with_named_fields();
        }

        [ActianTodo]
        public override void Simple_query_fields_only(bool tracking)
        {
            base.Simple_query_fields_only(tracking);
        }

        [ActianTodo]
        public override void Include_collection_fields_only(bool tracking)
        {
            base.Include_collection_fields_only(tracking);
        }

        [ActianTodo]
        public override void Include_reference_fields_only(bool tracking)
        {
            base.Include_reference_fields_only(tracking);
        }

        [ActianTodo]
        public override void Load_collection_fields_only()
        {
            base.Load_collection_fields_only();
        }

        [ActianTodo]
        public override void Load_reference_fields_only()
        {
            base.Load_reference_fields_only();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_fields_only(bool tracking)
        {
            base.Query_with_conditional_constant_fields_only(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_fields_only(bool tracking)
        {
            base.Query_with_conditional_param_fields_only(tracking);
        }

        [ActianTodo]
        public override void Projection_fields_only(bool tracking)
        {
            base.Projection_fields_only(tracking);
        }

        [ActianTodo]
        public override void Update_fields_only()
        {
            base.Update_fields_only();
        }

        [ActianTodo]
        public override void Simple_query_fields_only_for_navs_too(bool tracking)
        {
            base.Simple_query_fields_only_for_navs_too(tracking);
        }

        [ActianTodo]
        public override void Include_collection_fields_only_for_navs_too(bool tracking)
        {
            base.Include_collection_fields_only_for_navs_too(tracking);
        }

        [ActianTodo]
        public override void Include_reference_fields_only_only_for_navs_too(bool tracking)
        {
            base.Include_reference_fields_only_only_for_navs_too(tracking);
        }

        [ActianTodo]
        public override void Load_collection_fields_only_only_for_navs_too()
        {
            base.Load_collection_fields_only_only_for_navs_too();
        }

        [ActianTodo]
        public override void Load_reference_fields_only_only_for_navs_too()
        {
            base.Load_reference_fields_only_only_for_navs_too();
        }

        [ActianTodo]
        public override void Query_with_conditional_constant_fields_only_only_for_navs_too(bool tracking)
        {
            base.Query_with_conditional_constant_fields_only_only_for_navs_too(tracking);
        }

        [ActianTodo]
        public override void Query_with_conditional_param_fields_only_only_for_navs_too(bool tracking)
        {
            base.Query_with_conditional_param_fields_only_only_for_navs_too(tracking);
        }

        [ActianTodo]
        public override void Projection_fields_only_only_for_navs_too(bool tracking)
        {
            base.Projection_fields_only_only_for_navs_too(tracking);
        }

        [ActianTodo]
        public override void Update_fields_only_only_for_navs_too()
        {
            base.Update_fields_only_only_for_navs_too();
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class FieldMappingActianFixture : FieldMappingFixtureBase, IActianSqlFixture
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
        }
    }
}
