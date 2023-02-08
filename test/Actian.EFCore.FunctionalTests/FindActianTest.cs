using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public abstract class FindActianTest : FindTestBase<FindActianTest.FindActianFixture>
    {
        protected FindActianTest(FindActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();


        public override void Find_int_key_tracked()
        {
            base.Find_int_key_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_int_key_from_store()
        {
            base.Find_int_key_from_store();
            AssertSql(@"
                @__p_0='77'
                
                SELECT FIRST 1 ""i"".""Id"", ""i"".""Foo""
                FROM ""IntKey"" AS ""i""
                WHERE ""i"".""Id"" = @__p_0
            ");
        }


        public override void Returns_null_for_int_key_not_in_store()
        {
            base.Returns_null_for_int_key_not_in_store();
            AssertSql(@"
                @__p_0='99'
                
                SELECT FIRST 1 ""i"".""Id"", ""i"".""Foo""
                FROM ""IntKey"" AS ""i""
                WHERE ""i"".""Id"" = @__p_0
            ");
        }


        public override void Find_nullable_int_key_tracked()
        {
            base.Find_int_key_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_nullable_int_key_from_store()
        {
            base.Find_int_key_from_store();
            AssertSql(@"
                @__p_0='77'
                
                SELECT FIRST 1 ""i"".""Id"", ""i"".""Foo""
                FROM ""IntKey"" AS ""i""
                WHERE ""i"".""Id"" = @__p_0
            ");
        }


        public override void Returns_null_for_nullable_int_key_not_in_store()
        {
            base.Returns_null_for_int_key_not_in_store();
            AssertSql(@"
                @__p_0='99'
                
                SELECT FIRST 1 ""i"".""Id"", ""i"".""Foo""
                FROM ""IntKey"" AS ""i""
                WHERE ""i"".""Id"" = @__p_0
            ");
        }


        public override void Find_string_key_tracked()
        {
            base.Find_string_key_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_string_key_from_store()
        {
            base.Find_string_key_from_store();
            AssertSql(@"
                @__p_0='Cat'
                
                SELECT FIRST 1 ""s"".""Id"", ""s"".""Foo""
                FROM ""StringKey"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override void Returns_null_for_string_key_not_in_store()
        {
            base.Returns_null_for_string_key_not_in_store();
            AssertSql(@"
                @__p_0='Fox'
                
                SELECT FIRST 1 ""s"".""Id"", ""s"".""Foo""
                FROM ""StringKey"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override void Find_composite_key_tracked()
        {
            base.Find_composite_key_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_composite_key_from_store()
        {
            base.Find_composite_key_from_store();
            AssertSql(@"
                @__p_0='77'
                @__p_1='Dog'
                
                SELECT FIRST 1 ""c"".""Id1"", ""c"".""Id2"", ""c"".""Foo""
                FROM ""CompositeKey"" AS ""c""
                WHERE (""c"".""Id1"" = @__p_0) AND (""c"".""Id2"" = @__p_1)
            ");
        }


        public override void Returns_null_for_composite_key_not_in_store()
        {
            base.Returns_null_for_composite_key_not_in_store();
            AssertSql(@"
                @__p_0='77'
                @__p_1='Fox'
                
                SELECT FIRST 1 ""c"".""Id1"", ""c"".""Id2"", ""c"".""Foo""
                FROM ""CompositeKey"" AS ""c""
                WHERE (""c"".""Id1"" = @__p_0) AND (""c"".""Id2"" = @__p_1)
            ");
        }


        public override void Find_base_type_tracked()
        {
            base.Find_base_type_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_base_type_from_store()
        {
            base.Find_base_type_from_store();
            AssertSql(@"
                @__p_0='77'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE ""b"".""Discriminator"" IN (N'BaseType', N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Returns_null_for_base_type_not_in_store()
        {
            base.Returns_null_for_base_type_not_in_store();
            AssertSql(@"
                @__p_0='99'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE ""b"".""Discriminator"" IN (N'BaseType', N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Find_derived_type_tracked()
        {
            base.Find_derived_type_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_derived_type_from_store()
        {
            base.Find_derived_type_from_store();
            AssertSql(@"
                @__p_0='78'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE (""b"".""Discriminator"" = N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Returns_null_for_derived_type_not_in_store()
        {
            base.Returns_null_for_derived_type_not_in_store();
            AssertSql(@"
                @__p_0='99'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE (""b"".""Discriminator"" = N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Find_base_type_using_derived_set_tracked()
        {
            base.Find_base_type_using_derived_set_tracked();
            AssertSql(@"
                @__p_0='88'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE (""b"".""Discriminator"" = N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Find_base_type_using_derived_set_from_store()
        {
            base.Find_base_type_using_derived_set_from_store();
            AssertSql(@"
                @__p_0='77'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE (""b"".""Discriminator"" = N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Find_derived_type_using_base_set_tracked()
        {
            base.Find_derived_type_using_base_set_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_derived_using_base_set_type_from_store()
        {
            base.Find_derived_using_base_set_type_from_store();
            AssertSql(@"
                @__p_0='78'
                
                SELECT FIRST 1 ""b"".""Id"", ""b"".""Discriminator"", ""b"".""Foo"", ""b"".""Boo""
                FROM ""BaseType"" AS ""b""
                WHERE ""b"".""Discriminator"" IN (N'BaseType', N'DerivedType') AND (""b"".""Id"" = @__p_0)
            ");
        }


        public override void Find_shadow_key_tracked()
        {
            base.Find_shadow_key_tracked();
            Assert.Equal("", Sql);
        }


        public override void Find_shadow_key_from_store()
        {
            base.Find_shadow_key_from_store();
            AssertSql(@"
                @__p_0='77'
                
                SELECT FIRST 1 ""s"".""Id"", ""s"".""Foo""
                FROM ""ShadowKey"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override void Returns_null_for_shadow_key_not_in_store()
        {
            base.Returns_null_for_shadow_key_not_in_store();
            AssertSql(@"
                @__p_0='99'
                
                SELECT FIRST 1 ""s"".""Id"", ""s"".""Foo""
                FROM ""ShadowKey"" AS ""s""
                WHERE ""s"".""Id"" = @__p_0
            ");
        }


        public override void Returns_null_for_null_key_values_array()
        {
            base.Returns_null_for_null_key_values_array();
        }


        public override void Returns_null_for_null_key()
        {
            base.Returns_null_for_null_key();
        }


        public override void Returns_null_for_null_nullable_key()
        {
            base.Returns_null_for_null_nullable_key();
        }


        public override void Returns_null_for_null_in_composite_key()
        {
            base.Returns_null_for_null_in_composite_key();
        }


        public override void Throws_for_multiple_values_passed_for_simple_key()
        {
            base.Throws_for_multiple_values_passed_for_simple_key();
        }


        public override void Throws_for_wrong_number_of_values_for_composite_key()
        {
            base.Throws_for_wrong_number_of_values_for_composite_key();
        }


        public override void Throws_for_bad_type_for_simple_key()
        {
            base.Throws_for_bad_type_for_simple_key();
        }


        public override void Throws_for_bad_type_for_composite_key()
        {
            base.Throws_for_bad_type_for_composite_key();
        }


        public override void Throws_for_bad_entity_type()
        {
            base.Throws_for_bad_entity_type();
        }


        public override Task Find_int_key_tracked_async()
        {
            return base.Find_int_key_tracked_async();
        }


        public override Task Find_int_key_from_store_async()
        {
            return base.Find_int_key_from_store_async();
        }


        public override Task Returns_null_for_int_key_not_in_store_async()
        {
            return base.Returns_null_for_int_key_not_in_store_async();
        }


        public override Task Find_nullable_int_key_tracked_async()
        {
            return base.Find_nullable_int_key_tracked_async();
        }


        public override Task Find_nullable_int_key_from_store_async()
        {
            return base.Find_nullable_int_key_from_store_async();
        }


        public override Task Returns_null_for_nullable_int_key_not_in_store_async()
        {
            return base.Returns_null_for_nullable_int_key_not_in_store_async();
        }


        public override Task Find_string_key_tracked_async()
        {
            return base.Find_string_key_tracked_async();
        }


        public override Task Find_string_key_from_store_async()
        {
            return base.Find_string_key_from_store_async();
        }


        public override Task Returns_null_for_string_key_not_in_store_async()
        {
            return base.Returns_null_for_string_key_not_in_store_async();
        }


        public override Task Find_composite_key_tracked_async()
        {
            return base.Find_composite_key_tracked_async();
        }


        public override Task Find_composite_key_from_store_async()
        {
            return base.Find_composite_key_from_store_async();
        }


        public override Task Returns_null_for_composite_key_not_in_store_async()
        {
            return base.Returns_null_for_composite_key_not_in_store_async();
        }


        public override Task Find_base_type_tracked_async()
        {
            return base.Find_base_type_tracked_async();
        }


        public override Task Find_base_type_from_store_async()
        {
            return base.Find_base_type_from_store_async();
        }


        public override Task Returns_null_for_base_type_not_in_store_async()
        {
            return base.Returns_null_for_base_type_not_in_store_async();
        }


        public override Task Find_derived_type_tracked_async()
        {
            return base.Find_derived_type_tracked_async();
        }


        public override Task Find_derived_type_from_store_async()
        {
            return base.Find_derived_type_from_store_async();
        }


        public override Task Returns_null_for_derived_type_not_in_store_async()
        {
            return base.Returns_null_for_derived_type_not_in_store_async();
        }


        public override Task Find_base_type_using_derived_set_tracked_async()
        {
            return base.Find_base_type_using_derived_set_tracked_async();
        }


        public override Task Find_base_type_using_derived_set_from_store_async()
        {
            return base.Find_base_type_using_derived_set_from_store_async();
        }


        public override Task Find_derived_type_using_base_set_tracked_async()
        {
            return base.Find_derived_type_using_base_set_tracked_async();
        }


        public override Task Find_derived_using_base_set_type_from_store_async()
        {
            return base.Find_derived_using_base_set_type_from_store_async();
        }


        public override Task Find_shadow_key_tracked_async()
        {
            return base.Find_shadow_key_tracked_async();
        }


        public override Task Find_shadow_key_from_store_async()
        {
            return base.Find_shadow_key_from_store_async();
        }


        public override Task Returns_null_for_shadow_key_not_in_store_async()
        {
            return base.Returns_null_for_shadow_key_not_in_store_async();
        }


        public override Task Returns_null_for_null_key_values_array_async()
        {
            return base.Returns_null_for_null_key_values_array_async();
        }


        public override Task Returns_null_for_null_key_async()
        {
            return base.Returns_null_for_null_key_async();
        }


        public override Task Returns_null_for_null_in_composite_key_async()
        {
            return base.Returns_null_for_null_in_composite_key_async();
        }


        public override Task Throws_for_multiple_values_passed_for_simple_key_async()
        {
            return base.Throws_for_multiple_values_passed_for_simple_key_async();
        }


        public override Task Throws_for_wrong_number_of_values_for_composite_key_async()
        {
            return base.Throws_for_wrong_number_of_values_for_composite_key_async();
        }


        public override Task Throws_for_bad_type_for_simple_key_async()
        {
            return base.Throws_for_bad_type_for_simple_key_async();
        }


        public override Task Throws_for_bad_type_for_composite_key_async()
        {
            return base.Throws_for_bad_type_for_composite_key_async();
        }


        public override Task Throws_for_bad_entity_type_async()
        {
            return base.Throws_for_bad_entity_type_async();
        }

        public class FindActianTestSet : FindActianTest
        {
            public FindActianTestSet(FindActianFixture fixture, ITestOutputHelper testOutputHelper)
                : base(fixture, testOutputHelper)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => context.Set<TEntity>().Find(keyValues);

            protected override ValueTask<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => context.Set<TEntity>().FindAsync(keyValues);
        }

        public class FindActianTestContext : FindActianTest
        {
            public FindActianTestContext(FindActianFixture fixture, ITestOutputHelper testOutputHelper)
                : base(fixture, testOutputHelper)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => context.Find<TEntity>(keyValues);

            protected override ValueTask<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => context.FindAsync<TEntity>(keyValues);
        }

        public class FindActianTestNonGeneric : FindActianTest
        {
            public FindActianTestNonGeneric(FindActianFixture fixture, ITestOutputHelper testOutputHelper)
                : base(fixture, testOutputHelper)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => (TEntity)context.Find(typeof(TEntity), keyValues);

            protected override async ValueTask<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => (TEntity)await context.FindAsync(typeof(TEntity), keyValues);
        }

        private string Sql => Fixture.TestSqlLoggerFactory.Sql;

        public class FindActianFixture : FindFixtureBase, IActianSqlFixture
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
        }
    }
}
