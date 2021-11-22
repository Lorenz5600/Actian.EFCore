using System;
using Actian.EFCore.Infrastructure;
using Actian.EFCore.Infrastructure.Internal;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class LoggingActianTest : LoggingRelationalTestBase<ActianDbContextOptionsBuilder, ActianOptionsExtension>
    {
        public LoggingActianTest(ITestOutputHelper testOutputHelper)
        {
            TestEnvironment.Log(this, testOutputHelper);
        }

        protected override DbContextOptionsBuilder CreateOptionsBuilder(
            IServiceCollection services,
            Action<RelationalDbContextOptionsBuilder<ActianDbContextOptionsBuilder, ActianOptionsExtension>> relationalAction)
            => new DbContextOptionsBuilder()
                .UseInternalServiceProvider(services.AddEntityFrameworkActian().BuildServiceProvider())
                .UseActian(TestEnvironment.GetConnectionString("LoggingTest"), relationalAction);

        protected override string ProviderName => "Actian.EFCore";
    }
}
