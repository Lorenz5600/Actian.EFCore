using System;
using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Actian.EFCore
{
    public class ConcurrencyDetectorDisabledActianTest : ConcurrencyDetectorDisabledRelationalTestBase<
        ConcurrencyDetectorDisabledActianTest.ConcurrencyDetectorActianFixture>
    {
        public ConcurrencyDetectorDisabledActianTest(ConcurrencyDetectorActianFixture fixture)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
        }

        protected override async Task ConcurrencyDetectorTest(Func<ConcurrencyDetectorDbContext, Task<object>> test)
        {
            await base.ConcurrencyDetectorTest(test);

            Assert.NotEmpty(Fixture.TestSqlLoggerFactory.SqlStatements);
        }

        [ActianTodo]
        public override async Task Any(bool async)
        {
            await ConcurrencyDetectorTest(
            async c => async
                ? await c.Products.AnyAsync(p => p.Id < 10)
                : c.Products.Any(p => p.Id < 10));
        }

        [ActianTodo]
        public override async Task SaveChanges(bool async)
        {
            if (!async) 
                await base.SaveChanges(async);
            else
                Assert.True(async);
        }

        public class ConcurrencyDetectorActianFixture : ConcurrencyDetectorFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory
                => ActianTestStoreFactory.Instance;

            public TestSqlLoggerFactory TestSqlLoggerFactory
                => (TestSqlLoggerFactory)ListLoggerFactory;

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => builder.EnableThreadSafetyChecks(enableChecks: false);
        }
    }

}
