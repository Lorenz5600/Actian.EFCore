using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
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
                CREATE TABLE PrincipalTable (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL,
                    PRIMARY KEY (Id1, Id2)
                );

                CREATE TABLE DependentTable (
                    Id            int NOT NULL PRIMARY KEY,
                    ForeignKeyId1 int NOT NULL,
                    ForeignKeyId2 int NOT NULL,
                    FOREIGN KEY (ForeignKeyId1, ForeignKeyId2) REFERENCES PrincipalTable(Id1, Id2) ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == "efcore_test1" && t.Name == "dependenttable")
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "foreignkeyid1" },
                                new { Name = "foreignkeyid2" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "efcore_test1",
                                Name = "principaltable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "id1" },
                                new { Name = "id2" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => info.SelectedMemberPath == "ForeignKeys[0].Name")
                )
            )
        );

        [ConditionalFact]
        public void Create_multiple_foreign_key_in_same_table() => Test(test => test
            .Arrange(@"
                CREATE TABLE PrincipalTable (
                    Id int NOT NULL PRIMARY KEY
                );

                CREATE TABLE AnotherPrincipalTable (
                    Id int NOT NULL PRIMARY KEY
                );

                CREATE TABLE DependentTable (
                    Id            int NOT NULL PRIMARY KEY,
                    ForeignKeyId1 int NOT NULL,
                    ForeignKeyId2 int NOT NULL,
                    FOREIGN KEY (ForeignKeyId1) REFERENCES PrincipalTable(Id) ON DELETE CASCADE,
                    FOREIGN KEY (ForeignKeyId2) REFERENCES AnotherPrincipalTable(Id) ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == "efcore_test1" && t.Name == "dependenttable")
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "foreignkeyid1" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "efcore_test1",
                                Name = "principaltable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "id" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        },
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "foreignkeyid2" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "efcore_test1",
                                Name = "anotherprincipaltable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "id" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => Regex.IsMatch(info.SelectedMemberPath, @"ForeignKeys\[\d+\].Name"))
                )
            )
        );

        [ConditionalFact]
        public void Create_foreign_key_referencing_unique_constraint() => Test(test => test
            .Arrange(@"
                CREATE TABLE PrincipalTable (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL UNIQUE
                );

                CREATE TABLE DependentTable (
                    Id           int NOT NULL PRIMARY KEY,
                    ForeignKeyId int NOT NULL,
                    FOREIGN KEY (ForeignKeyId) REFERENCES PrincipalTable(Id2) ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == "efcore_test1" && t.Name == "dependenttable")
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "foreignkeyid" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "efcore_test1",
                                Name = "principaltable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "id2" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => Regex.IsMatch(info.SelectedMemberPath, @"ForeignKeys\[\d+\].Name"))
                )
            )
        );

        [ConditionalFact]
        public void Set_name_for_foreign_key() => Test(test => test
            .Arrange(@"
                CREATE TABLE PrincipalTable (
                    Id int NOT NULL PRIMARY KEY
                );

                CREATE TABLE DependentTable (
                    Id           int NOT NULL PRIMARY KEY,
                    ForeignKeyId int NOT NULL,
                    CONSTRAINT MYFK FOREIGN KEY (ForeignKeyId) REFERENCES PrincipalTable(Id) ON DELETE CASCADE
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == "efcore_test1" && t.Name == "dependenttable")
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "myfk",
                            Columns = Items(
                                new { Name = "foreignkeyid" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "efcore_test1",
                                Name = "principaltable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "id" }
                            ),
                            OnDelete = ReferentialAction.Cascade
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                )
            )
        );

        [ConditionalFact]
        public void Set_referential_action_for_foreign_key() => Test(test => test
            .Arrange(@"
                CREATE TABLE PrincipalTable (
                    Id int NOT NULL PRIMARY KEY
                );

                CREATE TABLE DependentTable (
                    Id           int NOT  NULL PRIMARY KEY,
                    ForeignKeyId int WITH NULL,
                    FOREIGN KEY (ForeignKeyId) REFERENCES PrincipalTable(Id) ON DELETE SET NULL
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault(t => t.Schema == "efcore_test1" && t.Name == "dependenttable")
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    ForeignKeys = Items(
                        new
                        {
                            Name = "$depen_",
                            Columns = Items(
                                new { Name = "foreignkeyid" }
                            ),
                            PrincipalTable = new
                            {
                                Schema = "efcore_test1",
                                Name = "principaltable"
                            },
                            PrincipalColumns = Items(
                                new { Name = "id" }
                            ),
                            OnDelete = ReferentialAction.SetNull
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => Regex.IsMatch(info.SelectedMemberPath, @"ForeignKeys\[\d+\].Name"))
                )
            )
        );
    }
}
