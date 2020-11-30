using System.Linq;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class Tables : ActianDatabaseModelFactoryTestBase
    {
        public Tables(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Create_columns() => Test(test => test
            .Arrange(@$"
                CREATE TABLE ""Blogs"" (
                    ""Id"" int,
                    ""Name"" text NOT NULL
                );

                COMMENT ON TABLE ""Blogs"" IS 'Blog table comment.
On multiple lines.';

                COMMENT ON COLUMN ""Blogs"".""Id"" IS 'Blog.Id column comment.';
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "blogs",
                    Comment = @"Blog table comment.
On multiple lines.",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer", Comment = "Blog.Id column comment.", Table = new { Schema = "efcore_test1", Name = "blogs" } },
                        new { Name = "name", StoreType = "text", Comment = null as string, Table = new { Schema = "efcore_test1", Name = "blogs" } }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Create_view_columns() => Test(test => test
            .Arrange(@"
                CREATE VIEW ""BlogsView"" AS
                SELECT int(100)          AS ""Id"",
                       nvarchar('', 100) AS ""Name"";
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseView>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "blogsview",
                    PrimaryKey = (DatabasePrimaryKey)null,
                    Columns = Items(
                        new { Name = "id", StoreType = "integer", Table = new { Schema = "efcore_test1", Name = "blogsview" } },
                        new { Name = "name", StoreType = "nvarchar(100)", Table = new { Schema = "efcore_test1", Name = "blogsview" } }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Create_primary_key() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""PrimaryKeyTable"" (
                    ""Id"" int not null PRIMARY KEY
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>()
                .And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "primarykeytable",
                    PrimaryKey = new
                    {
                        Name = "$prima_",
                        Table = new { Schema = "efcore_test1", Name = "primarykeytable" },
                        Columns = Items(
                            new { Name = "id" }
                        )
                    }
                }, options => options
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => info.SelectedMemberPath == "PrimaryKey.Name")
                )
            )
        );

        [ConditionalFact]
        public void Create_unique_constraints() => Test(test => test
            .Arrange(@"
                CREATE TABLE UniqueConstraint (
                    Id int not null,
                    Name int Unique not null,
                    IndexProperty int not null
                );
                CREATE INDEX IX_INDEX on UniqueConstraint ( IndexProperty );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "uniqueconstraint",
                    UniqueConstraints = Items(
                        new
                        {
                            Table = new { Schema = "efcore_test1", Name = "uniqueconstraint" },
                            Columns = Items(
                                new { Name = "name" }
                            )
                        }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Create_indexes() => Test(test => test
            .Arrange(@"
                CREATE TABLE IndexTable (
                    Id            int not null unique,
                    Name          int not null,
                    IndexProperty int not null
                );

                CREATE INDEX IX_NAME on IndexTable ( Name );
                CREATE INDEX IX_INDEX on IndexTable ( IndexProperty );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "indextable",
                    // Unique constraints should *not* be modelled as indices
                    Indexes = Items(
                        new
                        {
                            Table = new { Schema = "efcore_test1", Name = "indextable" },
                            Name = "ix_name",
                            Columns = Items(
                                new { Name = "name" }
                            )
                        },
                        new
                        {
                            Table = new { Schema = "efcore_test1", Name = "indextable" },
                            Name = "ix_index",
                            Columns = Items(
                                new { Name = "indexproperty" }
                            )
                        }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Create_foreign_keys() => Test(test => test
            .Arrange(@"
                CREATE TABLE PrincipalTable (
                    Id           int not null PRIMARY KEY
                );

                CREATE TABLE FirstDependent (
                    Id           int not null PRIMARY KEY,
                    ForeignKeyId int,
                    FOREIGN KEY (ForeignKeyId) REFERENCES PrincipalTable(Id) ON DELETE CASCADE
                );

                CREATE TABLE SecondDependent (
                    Id           int not null PRIMARY KEY,
                    FOREIGN KEY (Id) REFERENCES PrincipalTable(Id) ON DELETE NO ACTION
                );
            ")
            .Assert(dbModel =>
            {
                dbModel.Tables
                    .SingleOrDefault(t => t.Name == "firstdependent")
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "efcore_test1",
                        Name = "firstdependent",
                        ForeignKeys = Items(
                            new
                            {
                                Table = new { Schema = "efcore_test1", Name = "firstdependent" },
                                PrincipalTable = new { Schema = "efcore_test1", Name = "principaltable" },
                                Columns = Items(new { Name = "foreignkeyid" }),
                                PrincipalColumns = Items(new { Name = "id" }),
                                OnDelete = ReferentialAction.Cascade
                            }
                        )
                    });
                dbModel.Tables
                    .SingleOrDefault(t => t.Name == "seconddependent")
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "efcore_test1",
                        Name = "seconddependent",
                        ForeignKeys = Items(
                            new
                            {
                                Table = new { Schema = "efcore_test1", Name = "seconddependent" },
                                PrincipalTable = new { Schema = "efcore_test1", Name = "principaltable" },
                                Columns = Items(new { Name = "id" }),
                                PrincipalColumns = Items(new { Name = "id" }),
                                OnDelete = ReferentialAction.NoAction
                            }
                        )
                    });
            })
        );
    }
}
