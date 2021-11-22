// TODO: Implement for Actian

using System;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore
{
    public class ActianFixture : ServiceProviderFixtureBase
    {
        public static IServiceProvider DefaultServiceProvider { get; } = new ServiceCollection()
            .AddEntityFrameworkActian()
            .BuildServiceProvider();

        public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ServiceProvider.GetRequiredService<ILoggerFactory>();
        protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder) => base.AddOptions(builder)
            .ConfigureWarnings(w => w.Log(ActianEventId.ByteIdentityColumnWarning));
    }
}
