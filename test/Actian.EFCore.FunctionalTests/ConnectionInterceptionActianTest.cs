using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public abstract class ConnectionInterceptionActianTest : ConnectionInterceptionTestBase
    {
        protected ConnectionInterceptionActianTest(InterceptionActianFixtureBase fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override Task Intercept_connection_passively(bool async)
        {
            return base.Intercept_connection_passively(async);
        }

        public override Task Intercept_connection_to_override_opening(bool async)
        {
            return base.Intercept_connection_to_override_opening(async);
        }

        public override Task Intercept_connection_with_multiple_interceptors(bool async)
        {
            return base.Intercept_connection_with_multiple_interceptors(async);
        }

        public override Task Intercept_connection_that_throws_on_open(bool async)
        {
            return base.Intercept_connection_that_throws_on_open(async);
        }

        public abstract class InterceptionActianFixtureBase : InterceptionFixtureBase
        {
            protected override string StoreName => "ConnectionInterception";
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;

            protected override IServiceCollection InjectInterceptors(
                IServiceCollection serviceCollection,
                IEnumerable<IInterceptor> injectedInterceptors)
                => base.InjectInterceptors(serviceCollection.AddEntityFrameworkActian(), injectedInterceptors);
        }

        protected override BadUniverseContext CreateBadUniverse(DbContextOptionsBuilder optionsBuilder)
            => new BadUniverseContext(optionsBuilder.UseActian(new FakeDbConnection()).Options);

        public class FakeDbConnection : DbConnection
        {
            public override string ConnectionString { get; set; }
            public override string Database => "Database";
            public override string DataSource => "DataSource";
            public override string ServerVersion => throw new NotImplementedException();
            public override ConnectionState State => ConnectionState.Closed;
            public override void ChangeDatabase(string databaseName) => throw new NotImplementedException();
            public override void Close() => throw new NotImplementedException();
            public override void Open() => throw new NotImplementedException();
            protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => throw new NotImplementedException();
            protected override DbCommand CreateDbCommand() => throw new NotImplementedException();
        }

        public class WithoutDiagnostics : ConnectionInterceptionActianTest, IClassFixture<WithoutDiagnostics.InterceptionActianFixture>
        {
            public WithoutDiagnostics(InterceptionActianFixture fixture, ITestOutputHelper testOutputHelper)
                : base(fixture, testOutputHelper)
            {
            }

            public class InterceptionActianFixture : InterceptionActianFixtureBase
            {
                protected override bool ShouldSubscribeToDiagnosticListener => false;
            }
        }

        public class WithDiagnostics : ConnectionInterceptionActianTest, IClassFixture<WithDiagnostics.InterceptionActianFixture>
        {
            public WithDiagnostics(InterceptionActianFixture fixture, ITestOutputHelper testOutputHelper)
                : base(fixture, testOutputHelper)
            {
            }

            public class InterceptionActianFixture : InterceptionActianFixtureBase
            {
                protected override bool ShouldSubscribeToDiagnosticListener => true;
            }
        }
    }
}
