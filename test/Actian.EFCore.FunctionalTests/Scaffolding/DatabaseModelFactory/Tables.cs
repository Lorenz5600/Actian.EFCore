using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class Tables : ActianDatabaseModelFactoryTestBase
        {
            public Tables(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

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
                    Schema = "dbo",
                    Name = "Blogs",
                    Comment = "Blog table comment.\nOn multiple lines.",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer", Comment = "Blog.Id column comment.", Table = new { Schema = "dbo", Name = "Blogs" } },
                        new { Name = "Name", StoreType = "text", Comment = null as string, Table = new { Schema = "dbo", Name = "Blogs" } }
                    )
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "Columns[].Table.Schema")
                    .UsingDelimitedName(dbModel, "Columns[].Table.Name")
                )
            )
        );

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
                    Schema = "dbo",
                    Name = "BlogsView",
                    PrimaryKey = (DatabasePrimaryKey)null,
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer", Table = new { Schema = "dbo", Name = "BlogsView" } },
                        new { Name = "Name", StoreType = "nvarchar(100)", Table = new { Schema = "dbo", Name = "BlogsView" } }
                    )
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "Columns[].Table.Schema")
                    .UsingDelimitedName(dbModel, "Columns[].Table.Name")
                )
            )
        );

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
                    Schema = "dbo",
                    Name = "PrimaryKeyTable",
                    PrimaryKey = new
                    {
                        Name = "$prima_",
                        Table = new { Schema = "dbo", Name = "PrimaryKeyTable" },
                        Columns = Items(
                            new { Name = "Id" }
                        )
                    }
                }, options => options
                    .UsingString("PrimaryKey.Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "PrimaryKey.Table.Schema")
                    .UsingDelimitedName(dbModel, "PrimaryKey.Table.Name")
                    .UsingDelimitedName(dbModel, "PrimaryKey.Columns[].Name")
                )
            )
        );

            public void Create_unique_constraints() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""UniqueConstraint"" (
                        ""Id"" int not null,
                        ""Name"" int Unique not null,
                        ""IndexProperty"" int not null
                    );
                    CREATE INDEX ""IX_INDEX"" on ""UniqueConstraint"" ( ""IndexProperty"" );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "UniqueConstraint",
                    UniqueConstraints = Items(
                        new
                        {
                            Table = new { Schema = "dbo", Name = "UniqueConstraint" },
                            Columns = Items(
                                new { Name = "Name" }
                            )
                        }
                    )
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Table.Schema")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Table.Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

            public void Create_indexes() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""IndexTable"" (
                        ""Id""            int not null unique,
                        ""Name""          int not null,
                        ""IndexProperty"" int not null
                    );

                    CREATE INDEX ""IX_NAME"" on ""IndexTable"" ( ""Name"" );
                    CREATE INDEX ""IX_INDEX"" on ""IndexTable"" ( ""IndexProperty"" );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "IndexTable",
                        // Unique constraints should *not* be modelled as indices
                        Indexes = Items(
                        new
                        {
                            Table = new { Schema = "dbo", Name = "IndexTable" },
                            Name = "IX_NAME",
                            Columns = Items(
                                new { Name = "Name" }
                            )
                        },
                        new
                        {
                            Table = new { Schema = "dbo", Name = "IndexTable" },
                            Name = "IX_INDEX",
                            Columns = Items(
                                new { Name = "IndexProperty" }
                            )
                        }
                    )
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Table.Schema")
                    .UsingDelimitedName(dbModel, "Indexes[].Table.Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Columns[].Name")
                )
            )
        );

            public void Create_foreign_keys() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""PrincipalTable"" (
                        ""Id""           int not null PRIMARY KEY
                    );

                    CREATE TABLE ""FirstDependent"" (
                        ""Id""           int not null PRIMARY KEY,
                        ""ForeignKeyId"" int,
                        FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable"" (""Id"") ON DELETE CASCADE
                    );

                    CREATE TABLE ""SecondDependent"" (
                        ""Id""           int not null PRIMARY KEY,
                        FOREIGN KEY (""Id"") REFERENCES ""PrincipalTable"" (""Id"") ON DELETE NO ACTION
                    );
                ")
            .Assert(dbModel =>
            {
                dbModel.Tables
                    .SingleOrDefault(t => t.Name == dbModel.NormalizeDelimitedName("FirstDependent"))
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "FirstDependent",
                        ForeignKeys = Items(
                            new
                            {
                                Table = new { Schema = "dbo", Name = "FirstDependent" },
                                PrincipalTable = new { Schema = "dbo", Name = "PrincipalTable" },
                                Columns = Items(new { Name = "ForeignKeyId" }),
                                PrincipalColumns = Items(new { Name = "Id" }),
                                OnDelete = ReferentialAction.Cascade
                            }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Schema")
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].Table.Schema")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].Table.Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                    );
                dbModel.Tables
                    .SingleOrDefault(t => t.Name == dbModel.NormalizeDelimitedName("SecondDependent"))
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "SecondDependent",
                        ForeignKeys = Items(
                            new
                            {
                                Table = new { Schema = "dbo", Name = "SecondDependent" },
                                PrincipalTable = new { Schema = "dbo", Name = "PrincipalTable" },
                                Columns = Items(new { Name = "Id" }),
                                PrincipalColumns = Items(new { Name = "Id" }),
                                OnDelete = ReferentialAction.NoAction
                            }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Schema")
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].Table.Schema")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].Table.Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].Columns[].Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Schema")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalTable.Name")
                        .UsingDelimitedName(dbModel, "ForeignKeys[].PrincipalColumns[].Name")
                    );
            })
        );
        }
    }
}
