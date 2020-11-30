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
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE efcore_test2.""K2"" ( Id int not null, A varchar not null, UNIQUE (A));
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B));
            ")
            .FilterSchemas("efcore_test2")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test2",
                    Name = "k2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Filter_tables() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B), FOREIGN KEY (B) REFERENCES ""K2"" (A) );
            ")
            .FilterTables("K2")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "k2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_qualified_name() => Test(test => test
            .Arrange(@"
                CREATE TABLE ""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ")
            .FilterTables(@"""k.2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "k.2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name1() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE efcore_test1.""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE efcore_test2.""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ")
            .FilterTables("efcore_test1.k2")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "k2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name2() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                CREATE TABLE ""efcore_test.2"".""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""efcore_test.2"".""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ")
            .FilterTables(@"""efcore_test.2"".""K.2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test.2",
                    Name = "k.2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name3() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE ""efcore_test2"".""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ")
            .FilterTables(@"efcore_test1.""K.2""")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "k.2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Filter_tables_with_schema_qualified_name4() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                CREATE TABLE ""efcore_test.2"".""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""efcore_test.2"".""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ")
            .FilterTables(@"""efcore_test.2"".K2")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test.2",
                    Name = "k2",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer" },
                        new { Name = "a", StoreType = "varchar(1)" }
                    ),
                    UniqueConstraints = Items(
                        new { Columns = Items(new { Name = "a" }) }
                    ),
                    ForeignKeys = Enumerable.Empty<DatabaseForeignKey>()
                })
            )
        );

        [ConditionalFact]
        public void Complex_filtering_validation() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE SEQUENCE efcore_test1.""Sequence"";

                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE ""efcore_test2"".""Sequence"";

                SET SESSION AUTHORIZATION ""efcore_test.2"";
                CREATE TABLE ""efcore_test.2"".""QuotedTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test.2"".""Table.With.Dot"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test.2"".""SimpleTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test.2"".""JustTableName"" ( ""Id"" int not null PRIMARY KEY );

                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""efcore_test1"".""QuotedTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test1"".""Table.With.Dot"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test1"".""SimpleTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test1"".""JustTableName"" ( ""Id"" int not null PRIMARY KEY );

                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE ""efcore_test2"".""QuotedTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test2"".""Table.With.Dot"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test2"".""SimpleTableName"" ( ""Id"" int not null PRIMARY KEY );
                CREATE TABLE ""efcore_test2"".""JustTableName"" ( ""Id"" int not null PRIMARY KEY );

                CREATE TABLE ""efcore_test2"".""PrincipalTable"" (
                    ""Id"" int not null PRIMARY KEY,
                    ""UC1"" text not null,
                    ""UC2"" int not null,
                    ""Index1"" boolean not null,
                    ""Index2"" bigint not null,
                    CONSTRAINT ""UX"" UNIQUE (""UC1"", ""UC2"")
                );

                CREATE INDEX ""efcore_test2"".""IX_COMPOSITE"" ON ""efcore_test2"".""PrincipalTable"" ( ""Index2"", ""Index1"" );

                CREATE TABLE ""efcore_test2"".""DependentTable"" (
                    ""Id"" int not null PRIMARY KEY,
                    ""ForeignKeyId1"" text not null,
                    ""ForeignKeyId2"" int not null,
                    FOREIGN KEY (""ForeignKeyId1"", ""ForeignKeyId2"") REFERENCES ""efcore_test2"".""PrincipalTable""(""UC1"", ""UC2"") ON DELETE CASCADE
                );
            ")
            .FilterTables(@"""efcore_test.2"".""QuotedTableName""", @"""efcore_test.2"".SimpleTableName", @"efcore_test1.""Table.With.Dot""", @"efcore_test1.""SimpleTableName""", @"""JustTableName""")
            .FilterSchemas("efcore_test2")
            .Assert(dbModel =>
            {
                var sequence = Assert.Single(dbModel.Sequences);

                Assert.Equal("efcore_test2", sequence.Schema);

                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test.2" && t.Name == "quotedtablename"));
                Assert.Empty(dbModel.Tables.Where(t => t.Schema == "efcore_test.2" && t.Name == "table.with.dot"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test.2" && t.Name == "simpletablename"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test.2" && t.Name == "justtablename"));

                Assert.Empty(dbModel.Tables.Where(t => t.Schema == "efcore_test1" && t.Name == "quotedtablename"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test1" && t.Name == "table.with.dot"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test1" && t.Name == "simpletablename"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test1" && t.Name == "justtablename"));

                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test2" && t.Name == "quotedtablename"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test2" && t.Name == "table.with.dot"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test2" && t.Name == "simpletablename"));
                Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test2" && t.Name == "justtablename"));

                var principalTable = Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test2" && t.Name == "principaltable"));
                Assert.NotNull(principalTable.PrimaryKey);
                Assert.Single(principalTable.UniqueConstraints);
                Assert.Single(principalTable.Indexes);

                var dependentTable = Assert.Single(dbModel.Tables.Where(t => t.Schema == "efcore_test2" && t.Name == "dependenttable"));
                Assert.Single(dependentTable.ForeignKeys);
            })
        );
    }
}
