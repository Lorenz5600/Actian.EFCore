using System;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class ConcurrencyDetectorActianTest : ConcurrencyDetectorRelationalTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public ConcurrencyDetectorActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override Task SaveChanges_logs_concurrent_access_nonasync()
        {
            return base.SaveChanges_logs_concurrent_access_nonasync();
        }

        public override Task SaveChanges_logs_concurrent_access_async()
        {
            return base.SaveChanges_logs_concurrent_access_async();
        }

        public override Task Find_logs_concurrent_access_nonasync()
        {
            return base.Find_logs_concurrent_access_nonasync();
        }

        public override Task Find_logs_concurrent_access_async()
        {
            return base.Find_logs_concurrent_access_async();
        }

        public override Task Count_logs_concurrent_access_nonasync()
        {
            return base.Count_logs_concurrent_access_nonasync();
        }

        public override Task Count_logs_concurrent_access_async()
        {
            return base.Count_logs_concurrent_access_async();
        }

        public override Task First_logs_concurrent_access_nonasync()
        {
            return base.First_logs_concurrent_access_nonasync();
        }

        public override Task First_logs_concurrent_access_async()
        {
            return base.First_logs_concurrent_access_async();
        }

        public override Task Last_logs_concurrent_access_nonasync()
        {
            return base.Last_logs_concurrent_access_nonasync();
        }

        public override Task Last_logs_concurrent_access_async()
        {
            return base.Last_logs_concurrent_access_async();
        }

        public override Task Single_logs_concurrent_access_nonasync()
        {
            return base.Single_logs_concurrent_access_nonasync();
        }

        public override Task Single_logs_concurrent_access_async()
        {
            return base.Single_logs_concurrent_access_async();
        }

        public override Task Any_logs_concurrent_access_nonasync()
        {
            return base.Any_logs_concurrent_access_nonasync();
        }

        public override Task Any_logs_concurrent_access_async()
        {
            return base.Any_logs_concurrent_access_async();
        }

        public override Task ToList_logs_concurrent_access_nonasync()
        {
            return base.ToList_logs_concurrent_access_nonasync();
        }

        public override Task ToList_logs_concurrent_access_async()
        {
            return base.ToList_logs_concurrent_access_async();
        }

        public override Task FromSql_logs_concurrent_access_nonasync()
        {
            return base.FromSql_logs_concurrent_access_nonasync();
        }

        public override Task FromSql_logs_concurrent_access_async()
        {
            return base.FromSql_logs_concurrent_access_async();
        }

        protected override async Task ConcurrencyDetectorTest(Func<NorthwindContext, Task> test)
        {
            await base.ConcurrencyDetectorTest(test);

            Assert.Empty(Fixture.TestSqlLoggerFactory.SqlStatements);
        }
    }
}
