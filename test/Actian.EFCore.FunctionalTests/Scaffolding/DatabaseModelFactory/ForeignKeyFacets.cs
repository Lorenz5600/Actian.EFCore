using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class ForeignKeyFacets : ActianDatabaseModelFactoryTestBase
    {
        public ForeignKeyFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Create_composite_foreign_key() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""PrincipalTable"" (
                    ""Id1"" int NOT NULL,
                    ""Id2"" int NOT NULL,
                    PRIMARY KEY (""Id1"", ""Id2"")
                );

                CREATE TABLE ""DependentTable"" (
                    ""Id""            int NOT NULL PRIMARY KEY,
                    ""ForeignKeyId1"" int NOT NULL,
                    ""ForeignKeyId2"" int NOT NULL,
                    FOREIGN KEY (""ForeignKeyId1"", ""ForeignKeyId2"") REFERENCES ""PrincipalTable"" (""Id1"", ""Id2"") ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == dbModel.NormalizeDelimitedName("dbo") && t.Name == dbModel.NormalizeDelimitedName("DependentTable"))
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "ForeignKeyId1" },
                                new { Name = "ForeignKeyId2" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "dbo",
                                Name = "PrincipalTable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "Id1" },
                                new { Name = "Id2" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingString("ForeignKeys[].Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Create_multiple_foreign_key_in_same_table() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""PrincipalTable"" (
                    ""Id"" int NOT NULL PRIMARY KEY
                );

                CREATE TABLE ""AnotherPrincipalTable"" (
                    ""Id"" int NOT NULL PRIMARY KEY
                );

                CREATE TABLE ""DependentTable"" (
                    ""Id""            int NOT NULL PRIMARY KEY,
                    ""ForeignKeyId1"" int NOT NULL,
                    ""ForeignKeyId2"" int NOT NULL,
                    FOREIGN KEY (""ForeignKeyId1"") REFERENCES ""PrincipalTable"" (""Id"") ON DELETE CASCADE,
                    FOREIGN KEY (""ForeignKeyId2"") REFERENCES ""AnotherPrincipalTable"" (""Id"") ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == dbModel.NormalizeDelimitedName("dbo") && t.Name == dbModel.NormalizeDelimitedName("DependentTable"))
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "ForeignKeyId1" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "dbo",
                                Name = "PrincipalTable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "Id" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        },
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "ForeignKeyId2" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "dbo",
                                Name = "AnotherPrincipalTable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "Id" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingString("ForeignKeys[].Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Create_foreign_key_referencing_unique_constraint() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""PrincipalTable"" (
                    ""Id1"" int NOT NULL,
                    ""Id2"" int NOT NULL UNIQUE
                );

                CREATE TABLE ""DependentTable"" (
                    ""Id""           int NOT NULL PRIMARY KEY,
                    ""ForeignKeyId"" int NOT NULL,
                    FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable"" (""Id2"") ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == dbModel.NormalizeDelimitedName("dbo") && t.Name == dbModel.NormalizeDelimitedName("DependentTable"))
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "ForeignKeyId" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "dbo",
                                Name = "PrincipalTable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "Id2" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingString("ForeignKeys[].Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Set_name_for_foreign_key() => Test(test => test
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
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == dbModel.NormalizeDelimitedName("dbo") && t.Name == dbModel.NormalizeDelimitedName("DependentTable"))
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "myfk",
                            Columns = Items(
                                new { Name = "ForeignKeyId" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "dbo",
                                Name = "PrincipalTable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "Id" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingString("ForeignKeys[].Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Set_referential_action_for_foreign_key() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""PrincipalTable"" (
                    ""Id"" int NOT NULL PRIMARY KEY
                );

                CREATE TABLE ""DependentTable"" (
                    ""Id""           int NOT  NULL PRIMARY KEY,
                    ""ForeignKeyId"" int WITH NULL,
                    FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable"" (""Id"") ON DELETE SET NULL
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == dbModel.NormalizeDelimitedName("dbo") && t.Name == dbModel.NormalizeDelimitedName("DependentTable"))
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "ForeignKeyId" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "dbo",
                                Name = "PrincipalTable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "Id" }
                            ),
                            OnDelete = ReferentialAction.SetNull
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingString("ForeignKeys[].Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                    .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                )
            )
        );
    }
}
