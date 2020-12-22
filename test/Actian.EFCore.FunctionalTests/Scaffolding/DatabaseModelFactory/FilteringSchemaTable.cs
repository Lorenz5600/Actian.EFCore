using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class FilteringSchemaTable : ActianDatabaseModelFactoryTestBase
    {
        public FilteringSchemaTable(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Filter_schemas() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION ""db2"";
                CREATE TABLE ""db2"".""K2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A""));
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B""));
            ")
            .FilterSchemas("\"db2\"")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "db2",
                    Name = "K2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Filter_tables() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""K2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                CREATE TABLE ""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B""), FOREIGN KEY (""B"") REFERENCES ""K2"" (""A"") );
            ")
            .FilterTables("K2")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "K2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_qualified_name() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""K.2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                CREATE TABLE ""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B"") );
            ")
            .FilterTables(@"""K.2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "K.2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name1() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""dbo"".""K2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                SET SESSION AUTHORIZATION ""db2"";
                CREATE TABLE ""db2"".""K2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B"") );
            ")
            .FilterTables(@"""dbo"".""K2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "K2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name2() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""K.2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                SET SESSION AUTHORIZATION ""db.2"";
                CREATE TABLE ""db.2"".""K.2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                CREATE TABLE ""db.2"".""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B"") );
            ")
            .FilterTables(@"""db.2"".""K.2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "db.2",
                    Name = "K.2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name3() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""K.2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"") );
                SET SESSION AUTHORIZATION ""db2"";
                CREATE TABLE ""db2"".""K.2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"") );
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B"") );
            ")
            .FilterTables(@"""dbo"".""K.2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "K.2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name4() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""K2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                SET SESSION AUTHORIZATION ""db.2"";
                CREATE TABLE ""db.2"".""K2"" ( ""Id"" int not null, ""A"" varchar not null, UNIQUE (""A"" ) );
                CREATE TABLE ""db.2"".""Kilimanjaro"" ( ""Id"" int not null, ""B"" varchar not null, UNIQUE (""B"") );
            ")
            .FilterTables(@"""db.2"".K2")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "db.2",
                    Name = "K2",
                    Columns = Items(
                        new { Name = "Id", StoreType = "integer" },
                        new { Name = "A", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "A" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                }, options => options
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Columns[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

        [ConditionalFact]
        public void Complex_filtering_validation() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION ""dbo"";
                CREATE SEQUENCE ""dbo"".""Sequence"";

                SET SESSION AUTHORIZATION ""db2"";
                CREATE SEQUENCE ""db2"".""Sequence"";

                SET SESSION AUTHORIZATION ""db.2"";
                CREATE TABLE ""db.2"".""QuotedTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""db.2"".""Table.With.Dot"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""db.2"".""SimpleTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""db.2"".""JustTableName"" ( ""Id"" int not null PRIMARY KEY );

                SET SESSION AUTHORIZATION ""dbo"";
                CREATE TABLE ""dbo"".""QuotedTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""dbo"".""Table.With.Dot"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""dbo"".""SimpleTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""dbo"".""JustTableName"" ( ""Id"" int not null PRIMARY KEY );

                SET SESSION AUTHORIZATION ""db2"";
                CREATE TABLE ""db2"".""QuotedTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""db2"".""Table.With.Dot"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""db2"".""SimpleTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""db2"".""JustTableName"" ( ""Id"" int not null PRIMARY KEY );

                CREATE TABLE ""db2"".""PrincipalTable"" (
                    ""Id"" int not null PRIMARY KEY,
                    ""UC1"" text not null,
                    ""UC2"" int not null,
                    ""Index1"" boolean not null,
                    ""Index2"" bigint not null,
                    CONSTRAINT ""UX"" UNIQUE (""UC1"", ""UC2"")
                );

                CREATE INDEX ""db2"".""IX_COMPOSITE"" ON ""db2"".""PrincipalTable"" ( ""Index2"", ""Index1"" );

                CREATE TABLE ""db2"".""DependentTable"" (
                    ""Id"" int not null PRIMARY KEY,
                    ""ForeignKeyId1"" text not null,
                    ""ForeignKeyId2"" int not null,
                    FOREIGN KEY (""ForeignKeyId1"", ""ForeignKeyId2"") REFERENCES ""db2"".""PrincipalTable""(""UC1"", ""UC2"") ON DELETE CASCADE
                );
            ")
            .FilterTables(@"""db.2"".""QuotedTableName""", @"""db.2"".""SimpleTableName""", @"""dbo"".""Table.With.Dot""", @"""dbo"".""SimpleTableName""", @"""JustTableName""")
            .FilterSchemas(@"""db2""")
            .Assert(dbModel => {
                dbModel.Should()
                    .BeEquivalentTo(new
                    {
                        Sequences = Items(
                            new { Schema = "db2", Name = "Sequence" }
                        ),
                        Tables = Items(
                            new { Schema = "db.2", Name = "QuotedTableName" },
                            new { Schema = "db.2", Name = "SimpleTableName" },
                            new { Schema = "db.2", Name = "JustTableName" },
                            new { Schema = "dbo", Name = "Table.With.Dot" },
                            new { Schema = "dbo", Name = "SimpleTableName" },
                            new { Schema = "dbo", Name = "JustTableName" },
                            new { Schema = "db2", Name = "QuotedTableName" },
                            new { Schema = "db2", Name = "Table.With.Dot" },
                            new { Schema = "db2", Name = "SimpleTableName" },
                            new { Schema = "db2", Name = "JustTableName" },
                            new { Schema = "db2", Name = "PrincipalTable" },
                            new { Schema = "db2", Name = "DependentTable" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Sequences[].Schema")
                        .UsingDelimitedName(dbModel, "Sequences[].Name")
                        .UsingDelimitedName(dbModel, "Tables[].Schema")
                        .UsingDelimitedName(dbModel, "Tables[].Name")
                    );


                var principalTable = dbModel.Tables
                    .Where(t => t.Schema == dbModel.NormalizeDelimitedName("db2") && t.Name == dbModel.NormalizeDelimitedName("PrincipalTable"))
                    .SingleOrDefault();

                principalTable.Should().NotBeNull();
                principalTable.PrimaryKey.Should().NotBeNull();
                principalTable.UniqueConstraints.Should().ContainSingle();
                principalTable.Indexes.Should().ContainSingle();

                var dependentTable = dbModel.Tables
                    .Where(t => t.Schema == dbModel.NormalizeDelimitedName("db2") && t.Name == dbModel.NormalizeDelimitedName("DependentTable"))
                    .SingleOrDefault();

                dependentTable.Should().NotBeNull();
                dependentTable.ForeignKeys.Should().ContainSingle();
            })
        );
    }
}
