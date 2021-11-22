using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class StoreGeneratedFixupActianTest : StoreGeneratedFixupRelationalTestBase<StoreGeneratedFixupActianTest.StoreGeneratedFixupActianFixture>
    {
        public StoreGeneratedFixupActianTest(StoreGeneratedFixupActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_set_both_navs_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_not_set_both_navs_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_set_both_navs_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_not_set_both_navs_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_not_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_prin_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_many_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_prin_uni_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_many_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_dep_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_many_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_many_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_dep_uni_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_many_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_many_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_many_no_navs_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_many_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_many_no_navs_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_many_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_set_both_navs_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_not_set_both_navs_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_set_both_navs_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_not_set_both_navs_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_not_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_prin_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_one_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_prin_uni_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_one_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_dep_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_one_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_then_principal_one_to_one_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_dep_uni_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_one_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_then_dependent_one_to_one_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_then_principal_one_to_one_no_navs_FK_set_no_navs_set()
        {
            base.Add_dependent_then_principal_one_to_one_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_then_dependent_one_to_one_no_navs_FK_set_no_navs_set()
        {
            base.Add_principal_then_dependent_one_to_one_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_set_both_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_not_set_both_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_set_both_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_not_set_both_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_not_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_prin_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_prin_uni_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_dep_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_dep_uni_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_many_no_navs_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_many_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_many_no_navs_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_many_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_set_both_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_not_set_both_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_set_both_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_not_set_both_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_not_set_both_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_not_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_prin_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_prin_uni_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_prin_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_prin_uni_FK_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_prin_uni_FK_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_prin_uni_FK_not_set_principal_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_prin_uni_FK_not_set_principal_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_dep_uni_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_dep_uni_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_dep_uni_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_dep_uni_FK_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_dep_uni_FK_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_dep_uni_FK_not_set_dependent_nav_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_dep_uni_FK_not_set_dependent_nav_set();
        }

        [ActianTodo]
        public override void Add_dependent_but_not_principal_one_to_one_no_navs_FK_set_no_navs_set()
        {
            base.Add_dependent_but_not_principal_one_to_one_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_principal_but_not_dependent_one_to_one_no_navs_FK_set_no_navs_set()
        {
            base.Add_principal_but_not_dependent_one_to_one_no_navs_FK_set_no_navs_set();
        }

        [ActianTodo]
        public override void Add_overlapping_graph_from_level()
        {
            base.Add_overlapping_graph_from_level();
        }

        [ActianTodo]
        public override void Add_overlapping_graph_from_game()
        {
            base.Add_overlapping_graph_from_game();
        }

        [ActianTodo]
        public override void Add_overlapping_graph_from_item()
        {
            base.Add_overlapping_graph_from_item();
        }

        [ActianTodo]
        public override void Temporary_value_equals_database_generated_value()
        {
            base.Temporary_value_equals_database_generated_value();
        }

        [ActianTodo]
        public override void Remove_overlapping_principal()
        {
            base.Remove_overlapping_principal();
        }

        [ActianTodo]
        public override void Multi_level_add_replace_and_save()
        {
            base.Multi_level_add_replace_and_save();
        }

        [ActianTodo]
        protected override void MarkIdsTemporary(DbContext context, object game, object level, object item)
        {
            var entry = context.Entry(game);
            entry.Property("Id").IsTemporary = true;

            entry = context.Entry(item);
            entry.Property("Id").IsTemporary = true;
        }

        protected override bool EnforcesFKs => true;

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class StoreGeneratedFixupActianFixture : StoreGeneratedFixupRelationalFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<Parent>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<Child>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ParentPN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ChildPN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ParentDN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ChildDN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ParentNN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ChildNN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<CategoryDN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ProductDN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<CategoryPN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ProductPN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<CategoryNN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<ProductNN>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<Category>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<Product>(
                    b =>
                    {
                        b.Property(e => e.Id1).ValueGeneratedOnAdd();
                        b.Property(e => e.Id2).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");
                    });

                modelBuilder.Entity<Item>(b => b.Property(e => e.Id).ValueGeneratedOnAdd());

                modelBuilder.Entity<Game>(b => b.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("newid()"));
            }
        }
    }
}
