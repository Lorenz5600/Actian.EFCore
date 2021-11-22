using System.Linq;
using Actian.EFCore.Diagnostics.Internal;
using Actian.EFCore.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class Warnings : ActianDatabaseModelFactoryTestBase
        {
            public Warnings(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            public void Warn_missing_schema() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""Blank"" (
                        ""Id"" int
                    );
                ")
            .FilterSchemas(@"""MySchema""")
            .Assert(dbModel =>
            {
                Assert.Empty(dbModel.Tables);

                var (_, Id, Message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log.Where(t => t.Level == LogLevel.Warning));

                Assert.Equal(ActianResources.LogMissingSchema(new TestLogger<ActianLoggingDefinitions>()).EventId, Id);
                Assert.Equal(
                    ActianResources.LogMissingSchema(new TestLogger<ActianLoggingDefinitions>()).GenerateMessage(dbModel.NormalizeDelimitedName("MySchema")),
                    Message);
            })
        );

            public void Warn_missing_table() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""Blank"" (
                        ""Id"" int
                    );
                ")
            .FilterTables("MyTable")
            .Assert(dbModel =>
            {
                Assert.Empty(dbModel.Tables);

                var (_, Id, Message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log.Where(t => t.Level == LogLevel.Warning));

                Assert.Equal(ActianResources.LogMissingTable(new TestLogger<ActianLoggingDefinitions>()).EventId, Id);
                Assert.Equal(
                    ActianResources.LogMissingTable(new TestLogger<ActianLoggingDefinitions>()).GenerateMessage("MyTable"),
                    Message);
            })
        );

            public void Warn_missing_principal_table_for_foreign_key() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""PrincipalTable"" (
                        ""Id"" int NOT NULL PRIMARY KEY
                    );

                    CREATE TABLE ""DependentTable"" (
                        ""Id""           int NOT NULL PRIMARY KEY,
                        ""ForeignKeyId"" int NOT NULL,
                        CONSTRAINT ""MYFK"" FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable"" (""Id"") ON DELETE CASCADE
                    );
                ")
            .FilterTables(@"""DependentTable""")
            .Assert(dbModel =>
            {
                var (_, Id, Message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log.Where(t => t.Level == LogLevel.Warning));

                Assert.Equal(
                    ActianResources.LogPrincipalTableNotInSelectionSet(new TestLogger<ActianLoggingDefinitions>()).EventId, Id);
                Assert.Equal(
                    ActianResources.LogPrincipalTableNotInSelectionSet(new TestLogger<ActianLoggingDefinitions>())
                        .GenerateMessage(dbModel.NormalizeDelimitedName("MYFK"), dbModel.NormalizeDelimitedName("dbo.DependentTable"), dbModel.NormalizeDelimitedName("dbo.PrincipalTable")), Message);
            })
        );

            public void Skip_reflexive_foreign_key() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""PrincipalTable"" (
                        ""Id"" int NOT NULL PRIMARY KEY,
                        CONSTRAINT ""MYFK"" FOREIGN KEY (""Id"") REFERENCES ""PrincipalTable"" (""Id"")
                    );
                ")
            .Assert(dbModel =>
            {
                var (level, _, message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log, t => t.Id == ActianEventId.ReflexiveConstraintIgnored);

                Assert.Equal(LogLevel.Debug, level);
                Assert.Equal(
                    ActianResources.LogReflexiveConstraintIgnored(new TestLogger<ActianLoggingDefinitions>())
                        .GenerateMessage(dbModel.NormalizeDelimitedName("MYFK"), dbModel.NormalizeDelimitedName("dbo.PrincipalTable")), message);

                var table = Assert.Single(dbModel.Tables);
                Assert.Empty(table.ForeignKeys);
            })
        );
        }
    }
}
