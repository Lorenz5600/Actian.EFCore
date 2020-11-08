using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Actian.EFCore.Diagnostics.Internal;
using Actian.EFCore.Scaffolding.Internal;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding
{
    public class ActianDatabaseModelFactoryTest : IClassFixture<ActianDatabaseModelFactoryTest.ActianDatabaseModelFixture>
    {
        protected ActianDatabaseModelFixture Fixture { get; }
        protected ITestOutputHelper Output { get; }

        public ActianDatabaseModelFactoryTest(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            Fixture.ListLoggerFactory.Clear();
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        #region Sequences

        [Fact]
        public void Create_sequences_with_facets() => Test(
            @$"
                CREATE SEQUENCE DefaultFacetsSequence;

                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE efcore_test2.CustomFacetsSequence
                    AS integer
                    START WITH 1
                    INCREMENT BY 2
                    MINVALUE 1
                    MAXVALUE 90
                    CYCLE;
            ",
            dbModel => dbModel.Sequences
                .Should().BeEquivalentTo(new []
                {
                    new {
                        Schema = "efcore_test1",
                        Name = "defaultfacetssequence",
                        StoreType = "bigint",
                        IsCyclic = false,
                        IncrementBy = 1,
                        StartValue = 1L,
                        MinValue = (long?)null,
                        MaxValue = (long?)null
                    },
                    new {
                        Schema = "efcore_test2",
                        Name = "customfacetssequence",
                        StoreType = "integer",
                        IsCyclic = true,
                        IncrementBy = 2,
                        StartValue = 1L,
                        MinValue = (long?)null,
                        MaxValue = (long?)90
                    }
                }),
            @"
                DROP SEQUENCE DefaultFacetsSequence;
                SET SESSION AUTHORIZATION efcore_test2;
                DROP SEQUENCE CustomFacetsSequence;
            "
        );

        [Fact]
        public void Sequence_min_max_start_values_are_null_if_default() => Test(
            @$"
                CREATE SEQUENCE IntSequence AS int;
                CREATE SEQUENCE BigIntSequence AS bigint;
            ",
            dbModel => dbModel.Sequences
                .Should().BeEquivalentTo(new[]
                {
                    new {
                        Schema = "efcore_test1",
                        Name = "intsequence",
                        StoreType = "integer",
                        IsCyclic = false,
                        IncrementBy = 1,
                        StartValue = 1L,
                        MinValue = (long?)null,
                        MaxValue = (long?)null
                    },
                    new {
                        Schema = "efcore_test1",
                        Name = "bigintsequence",
                        StoreType = "bigint",
                        IsCyclic = false,
                        IncrementBy = 1,
                        StartValue = 1L,
                        MinValue = (long?)null,
                        MaxValue = (long?)null
                    }
                }),
            @"
                DROP SEQUENCE IntSequence;
                DROP SEQUENCE BigIntSequence;
            "
        );

        [Fact]
        public void Sequence_min_max_start_values_are_not_null_if_decimal() => Test(
            @$"
                CREATE SEQUENCE DecimalSequence AS decimal;
                CREATE SEQUENCE NumericSequence AS numeric;
            ",
            dbModel => dbModel.Sequences
                .Should().BeEquivalentTo(new[]
                {
                    new {
                        Schema = "efcore_test1",
                        Name = "decimalsequence",
                        StoreType = "decimal",
                        IsCyclic = false,
                        IncrementBy = 1,
                        StartValue = 1L,
                        MinValue = 1L,
                        MaxValue = 99999L
                    },
                    new {
                        Schema = "efcore_test1",
                        Name = "numericsequence",
                        StoreType = "decimal",
                        IsCyclic = false,
                        IncrementBy = 1,
                        StartValue = 1L,
                        MinValue = 1L,
                        MaxValue = 99999L
                    }
                }),
            @"
                DROP SEQUENCE DecimalSequence;
                DROP SEQUENCE NumericSequence;
            "
        );

        [Fact]
        public void Sequence_using_type_with_facets() => Test(
            @$"
                CREATE SEQUENCE TypeFacetSequence AS decimal(10, 0);
            ",
            dbModel => dbModel.Sequences
                .Should().BeEquivalentTo(new[]
                {
                    new {
                        Schema = "efcore_test1",
                        Name = "typefacetsequence",
                        StoreType = "decimal(10)",
                        IsCyclic = false,
                        IncrementBy = 1
                    }
                }),
            @"
                DROP SEQUENCE TypeFacetSequence;
            "
        );






        [Fact]
        public void Create_sequences_with_default_facets() => Test(
            @$"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE ""DefaultFacetsSequence"";
            ",
            dbModel => dbModel.Sequences
                .SingleOrDefault(ds => ds.Name == "DefaultFacetsSequence".ToLowerInvariant())
                .Should().BeEquivalentTo(new
                {
                    Schema = "efcore_test2",
                    Name = "defaultfacetssequence",
                    StoreType = "bigint",
                    IsCyclic = false,
                    IncrementBy = 1,
                    StartValue = 1L,
                    MinValue = null as long?,
                    MaxValue = null as long?
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test2;
                DROP SEQUENCE ""DefaultFacetsSequence"";
            "
        );

        [Fact]
        public void Create_sequences_with_custom_facets() => Test(
            @$"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE efcore_test2.""CustomFacetsSequence"" AS integer
                    start with 10
                    increment by 2
                    maxvalue 80
                    minvalue 3
                    cycle;
            ",
            dbModel => dbModel.Sequences
                .SingleOrDefault(ds => ds.Name == "CustomFacetsSequence".ToLowerInvariant())
                .Should().BeOfType<DatabaseSequence>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test2",
                    Name = "customfacetssequence",
                    StoreType = "integer",
                    IsCyclic = true,
                    IncrementBy = 2,
                    StartValue = 10L,
                    MinValue = 3L,
                    MaxValue = 80L
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test2;
                DROP SEQUENCE efcore_test2.""CustomFacetsSequence"";
            "
        );

        [Fact]
        public void Filter_sequences_based_on_schema() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE SEQUENCE ""Sequence"";
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE efcore_test2.""Sequence""
            ",
            Enumerable.Empty<string>(),
            new[] { "efcore_test2" },
            dbModel => dbModel.Sequences
                .SingleOrDefault()
                .Should().BeOfType<DatabaseSequence>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test2",
                    Name = "sequence",
                    StoreType = "bigint"
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                DROP SEQUENCE ""Sequence"";
                SET SESSION AUTHORIZATION efcore_test2;
                DROP SEQUENCE efcore_test2.""Sequence"";
            "
        );

        #endregion

        #region Model

        [Fact]
        public void Get_default_schema() => Test(
            "SELECT 1",
            dbModel => dbModel.DefaultSchema.Should().Be(TestEnvironment.UserId)
        );

        [Fact]
        public void Create_tables() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE ""Everest"" ( id int );
                CREATE TABLE ""Denali"" ( id int );
            ",
            dbModel => dbModel.Tables
                .OrderBy(t => t.Name)
                .Should().ContainItemsAssignableTo<DatabaseTable>().And.BeEquivalentTo(new[]
                {
                    new { Schema = "efcore_test2", Name = "denali" },
                    new { Schema = "efcore_test2", Name = "everest" }
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test2;
                DROP TABLE ""Everest"";
                DROP TABLE ""Denali"";
            "
        );

        #endregion

        #region FilteringSchemaTable

        [Fact]
        public void Filter_schemas() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE efcore_test2.""K2"" ( Id int not null, A varchar not null, UNIQUE (A));
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B));
            ",
            Enumerable.Empty<string>(),
            new[] { "efcore_test2" },
            dbModel => dbModel.Tables
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
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                DROP TABLE efcore_test1.""Kilimanjaro"";
                SET SESSION AUTHORIZATION efcore_test2;
                DROP TABLE efcore_test2.""K2"";
            "
        );

        [Fact]
        public void Filter_tables() => Test(
            @"
                CREATE TABLE ""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B), FOREIGN KEY (B) REFERENCES ""K2"" (A) );
            ",
            new[] { "K2" },
            Enumerable.Empty<string>(),
            dbModel => dbModel.Tables
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
                }),
            @"
                DROP TABLE efcore_test1.""Kilimanjaro"";
                DROP TABLE efcore_test1.""K2"";
            "
        );

        [Fact]
        public void Filter_tables_with_qualified_name() => Test(
            @"
                CREATE TABLE ""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ",
            new[] { @"""k.2""" },
            Enumerable.Empty<string>(),
            dbModel => dbModel.Tables
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
                }),
            @"
                DROP TABLE efcore_test1.""Kilimanjaro"";
                DROP TABLE efcore_test1.""K.2"";
            "
        );

        [Fact]
        public void Filter_tables_with_schema_qualified_name1() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE efcore_test1.""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE efcore_test2.""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ",
            new[] { "efcore_test1.k2" },
            Enumerable.Empty<string>(),
            dbModel => dbModel.Tables
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
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                DROP TABLE efcore_test1.""Kilimanjaro"";
                DROP TABLE efcore_test1.""K2"";
                SET SESSION AUTHORIZATION efcore_test2;
                DROP TABLE efcore_test2.""K2"";
            "
        );

        [Fact]
        public void Filter_tables_with_schema_qualified_name2() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                CREATE TABLE ""efcore_test.2"".""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""efcore_test.2"".""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ",
            new[] { @"""efcore_test.2"".""K.2""" },
            Enumerable.Empty<string>(),
            dbModel => dbModel.Tables
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
                }),
            @"
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                DROP TABLE ""efcore_test.2"".""Kilimanjaro"";
                SET SESSION AUTHORIZATION efcore_test1;
                DROP TABLE ""K.2"";
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                DROP TABLE ""efcore_test.2"".""K.2"";
            "
        );

        [Fact]
        public void Filter_tables_with_schema_qualified_name3() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE ""efcore_test2"".""K.2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ",
            new[] { @"efcore_test1.""K.2""" },
            Enumerable.Empty<string>(),
            dbModel => dbModel.Tables
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
                }),
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                DROP TABLE ""Kilimanjaro"";
                DROP TABLE ""K.2"";
                SET SESSION AUTHORIZATION efcore_test2;
                DROP TABLE efcore_test2.""K.2"";
            "
        );

        [Fact]
        public void Filter_tables_with_schema_qualified_name4() => Test(
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE TABLE ""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                CREATE TABLE ""efcore_test.2"".""K2"" ( Id int not null, A varchar not null, UNIQUE (A ) );
                CREATE TABLE ""efcore_test.2"".""Kilimanjaro"" ( Id int not null, B varchar not null, UNIQUE (B) );
            ",
            new[] { @"""efcore_test.2"".K2" },
            Enumerable.Empty<string>(),
            dbModel => dbModel.Tables
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
                }),
            @"
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                DROP TABLE ""efcore_test.2"".""Kilimanjaro"";
                SET SESSION AUTHORIZATION efcore_test1;
                DROP TABLE ""K2"";
                SET SESSION AUTHORIZATION ""efcore_test.2"";
                DROP TABLE ""efcore_test.2"".""K2"";
            "
        );

        [Fact]
        public void Complex_filtering_validation() => Test(
            @"
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
            ",
            new[] { @"""efcore_test.2"".""QuotedTableName""", @"""efcore_test.2"".SimpleTableName", @"efcore_test1.""Table.With.Dot""", @"efcore_test1.""SimpleTableName""", @"""JustTableName""" },
            new[] { "efcore_test2" },
            dbModel =>
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
            },
            @"
                SET SESSION AUTHORIZATION efcore_test1;
                DROP SEQUENCE ""efcore_test1"".""Sequence"";

                SET SESSION AUTHORIZATION efcore_test2;
                DROP SEQUENCE ""efcore_test2"".""Sequence"";

                SET SESSION AUTHORIZATION ""efcore_test.2"";
                DROP TABLE ""efcore_test.2"".""QuotedTableName"";
                DROP TABLE ""efcore_test.2"".""Table.With.Dot"";
                DROP TABLE ""efcore_test.2"".""SimpleTableName"";
                DROP TABLE ""efcore_test.2"".""JustTableName"";

                SET SESSION AUTHORIZATION efcore_test1;
                DROP TABLE ""efcore_test1"".""QuotedTableName"";
                DROP TABLE ""efcore_test1"".""Table.With.Dot"";
                DROP TABLE ""efcore_test1"".""SimpleTableName"";
                DROP TABLE ""efcore_test1"".""JustTableName"";

                SET SESSION AUTHORIZATION efcore_test2;
                DROP INDEX ""efcore_test2"".""IX_COMPOSITE"";
                DROP TABLE ""efcore_test2"".""QuotedTableName"";
                DROP TABLE ""efcore_test2"".""Table.With.Dot"";
                DROP TABLE ""efcore_test2"".""SimpleTableName"";
                DROP TABLE ""efcore_test2"".""JustTableName"";
                DROP TABLE ""efcore_test2"".""DependentTable"";
                DROP TABLE ""efcore_test2"".""PrincipalTable"";
            "
        );

        #endregion

        #region Table

        [Fact]
        public void Create_columns() => Test(
            @"
                CREATE TABLE ""Blogs"" (
                    ""Id"" int,
                    ""Name"" text NOT NULL
                );
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "blogs",
                    Columns = Items(
                        new { Name = "id", StoreType = "integer", Table = new { Schema = "efcore_test1", Name = "blogs" } },
                        new { Name = "name", StoreType = "text", Table = new { Schema = "efcore_test1", Name = "blogs" } }
                    )
                }),
            @"
                DROP TABLE ""Blogs""
            "
        );

        [Fact]
        public void Create_view_columns() => Test(
            @"
                CREATE VIEW ""BlogsView"" AS
                SELECT int(100) AS ""Id"",
                       text('') AS ""Name"";
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseView>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "blogsview",
                    PrimaryKey = (DatabasePrimaryKey)null,
                    Columns = Items(
                        new { Name = "id", StoreType = "integer", Table = new { Schema = "efcore_test1", Name = "blogsview" } },
                        new { Name = "name", StoreType = "text", Table = new { Schema = "efcore_test1", Name = "blogsview" } }
                    )
                }),
            @"
                DROP VIEW ""BlogsView"";
            "
        );

        [Fact]
        public void Create_primary_key() => Test(
            @"
                CREATE TABLE ""PrimaryKeyTable"" (
                    ""Id"" int not null PRIMARY KEY
                );
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "primarykeytable",
                    PrimaryKey = new
                    {
                        Table = new { Schema = "efcore_test1", Name = "primarykeytable" },
                        Columns = Items(
                            new { Name = "id" }
                        )
                    }
                }),
            @"
                DROP TABLE ""PrimaryKeyTable""
            "
        );

        [Fact]
        public void Create_unique_constraints() => Test(
            @"
                CREATE TABLE ""UniqueConstraint"" (
                    ""Id"" int not null,
                    ""Name"" int not null Unique,
                    ""IndexProperty"" int not null,
                    ""Unq1"" int not null,
                    ""Unq2"" int not null,
                    UNIQUE (""Unq1"", ""Unq2"")
                );

                CREATE INDEX ""IX_INDEX"" on ""UniqueConstraint"" ( ""IndexProperty"" );
            ",
            dbModel => dbModel.Tables
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
                        },
                        new
                        {
                            Table = new { Schema = "efcore_test1", Name = "uniqueconstraint" },
                            Columns = Items(
                                new { Name = "unq1" },
                                new { Name = "unq2" }
                            )
                        }
                    )
                }),
            @"
                DROP TABLE ""UniqueConstraint""
            "
        );

        [Fact]
        public void Create_indexes() => Test(
            @"
                CREATE TABLE ""IndexTable"" (
                    ""Id"" int not null,
                    ""Name"" int not null,
                    ""IndexProperty"" int not null,
                    ""ConstraintProperty"" int not null,
                    UNIQUE (""ConstraintProperty"")
                );

                CREATE INDEX ""IX_NAME"" on ""IndexTable"" ( ""Name"" );
                CREATE INDEX ""IX_INDEX"" on ""IndexTable"" ( ""IndexProperty"" );
            ",
            dbModel => dbModel.Tables
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
                }),
            @"
                DROP TABLE ""IndexTable""
            "
        );

        [Fact]
        public void Create_foreign_keys() => Test(
            @"
                CREATE TABLE ""PrincipalTable"" (
                    ""Id"" int not null PRIMARY KEY
                );

                CREATE TABLE ""FirstDependent"" (
                    ""Id"" int not null PRIMARY KEY,
                    ""ForeignKeyId"" int,
                    FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE CASCADE
                );

                CREATE TABLE ""SecondDependent"" (
                    ""Id"" int not null PRIMARY KEY,
                    FOREIGN KEY (""Id"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE NO ACTION
                );
            ",
            dbModel =>
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
            },
            @"
                DROP TABLE ""SecondDependent"";
                DROP TABLE ""FirstDependent"";
                DROP TABLE ""PrincipalTable"";
            "
        );

        #endregion

        #region ColumnFacets

        // Note: in Actian numeric is simply an alias for decimal
        [Fact]
        public void Decimal_types_have_precision_scale() => Test(
            @"
                CREATE TABLE ""DecimalColumns"" (
                    ""decimalColumn"" decimal NOT NULL,
                    ""decimal152Column"" decimal(15, 2) NOT NULL,
                    ""decimal18Column"" decimal(18) NOT NULL
                )
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "decimalcolumns",
                    Columns = Items(
                        new { Name = "decimalcolumn", StoreType = "decimal" },
                        new { Name = "decimal152column", StoreType = "decimal(15,2)" },
                        new { Name = "decimal18column", StoreType = "decimal(18)" }
                    )
                }),
            @"
                DROP TABLE ""DecimalColumns""
            "
        );

        [Fact]
        public void Specific_max_length_are_added_to_store_type() => Test(
            @"
                CREATE TABLE ""LengthColumns"" (
                    ""char10Column"" char(10) NULL,
                    ""varchar66Column"" varchar(66) NULL,
                    ""nchar10Column"" nchar(10) NULL,
                    ""nvarchar66Column"" nvarchar(66) NULL,
                    ""byte111Column"" byte(111) NULL,
                    ""varbyte123Column"" varbyte(123) NULL
                )
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "lengthcolumns",
                    Columns = Items(
                        new { Name = "char10column", StoreType = "char(10)" },
                        new { Name = "varchar66column", StoreType = "varchar(66)" },
                        new { Name = "nchar10column", StoreType = "nchar(10)" },
                        new { Name = "nvarchar66column", StoreType = "nvarchar(66)" },
                        new { Name = "byte111column", StoreType = "byte(111)" },
                        new { Name = "varbyte123column", StoreType = "byte varying(123)" }
                    )
                }),
            @"
                DROP TABLE ""LengthColumns""
            "
        );

        [Fact]
        public void Datetime_types_have_precision_if_non_null_scale() => Test(
            @"
                CREATE TABLE ""DatetimeColumns"" (
                    ""time1Column"" time(1) NULL,
                    ""timetz2Column"" time(2) with time zone NULL,
                    ""timestamp3Column"" timestamp(3) NULL,
                    ""timestamptz4Column"" timestamp(4) with time zone NULL,
                    ""interval5Column"" interval day to second(5) NULL
                )
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "datetimecolumns",
                    Columns = Items(
                        new { Name = "time1column", StoreType = "time(1) without time zone" },
                        new { Name = "timetz2column", StoreType = "time(2) with time zone" },
                        new { Name = "timestamp3column", StoreType = "timestamp(3) without time zone" },
                        new { Name = "timestamptz4column", StoreType = "timestamp(4) with time zone" },
                        new { Name = "interval5column", StoreType = "interval day to second(5)" }
                    )
                }),
            @"
                DROP TABLE ""DatetimeColumns""
            "
        );

        [Fact]
        public void Store_types_without_any_facets() => Test(
            @"
                CREATE TABLE ""NoFacetTypes"" (
                    ""booleanColumn"" boolean,
                    ""floatColumn"" float4,
                    ""doubleColumn"" float,
                    ""decimalColumn"" decimal,
                    ""moneyColumn"" money,
                    -- ""guidColumn"" uuid,
                    ""byteColumn"" integer1,
                    ""shortColumn"" integer2,
                    ""intColumn"" integer4,
                    ""longColumn"" integer8,
                    ""timeColumn"" time,
                    ""timetzColumn"" time with time zone,
                    ""timestampColumn"" timestamp,
                    ""timestamptzColumn"" timestamp with time zone,
                    ""intervalDsColumn"" interval day to second,
                    ""intervalYmColumn"" interval year to month
                )
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "nofacettypes",
                    Columns = Items(
                        new { Name = "booleancolumn", StoreType = "boolean" },
                        new { Name = "floatcolumn", StoreType = "float4" },
                        new { Name = "doublecolumn", StoreType = "float" },
                        new { Name = "decimalcolumn", StoreType = "decimal" },
                        new { Name = "moneycolumn", StoreType = "money" },
                        //new { Name = "guidcolumn", StoreType = "byte(16)" }, // TODO: Fix in ADO.NET provider?
                        new { Name = "bytecolumn", StoreType = "tinyint" },
                        new { Name = "shortcolumn", StoreType = "smallint" },
                        new { Name = "intcolumn", StoreType = "integer" },
                        new { Name = "longcolumn", StoreType = "bigint" },
                        new { Name = "timecolumn", StoreType = "time without time zone" },
                        new { Name = "timetzcolumn", StoreType = "time with time zone" },
                        new { Name = "timestampcolumn", StoreType = "timestamp without time zone" },
                        new { Name = "timestamptzcolumn", StoreType = "timestamp with time zone" },
                        new { Name = "intervaldscolumn", StoreType = "interval day to second" },
                        new { Name = "intervalymcolumn", StoreType = "interval year to month" }
                    )
                }),
            @"
                DROP TABLE ""NoFacetTypes""
            "
        );

        [Fact]
        public void Default_values_are_stored() => Test(
            @"
                CREATE TABLE ""DefaultValues"" (
                    ""FixedDefaultValue"" timestamp NOT NULL DEFAULT timestamp '1999-01-08'
                )
            ",
            dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultvalues",
                    Columns = Items(
                        new { Name = "fixeddefaultvalue", DefaultValueSql = "timestamp '1999-01-08'" }
                    )
                }),
            @"
                DROP TABLE ""DefaultValues""
            "
        );

        //        [MinimumPostgresVersionFact(12, 0)]
        //        public void Computed_values_are_stored()
        //            => Test(@"
        //CREATE TABLE ""ComputedValues"" (
        //    ""Id"" int,
        //    ""A"" int NOT NULL,
        //    ""B"" int NOT NULL,
        //    ""SumOfAAndB"" int GENERATED ALWAYS AS (""A"" + ""B"") STORED
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    // Note that on-the-fly computed columns aren't (yet) supported by Actian, only stored/persistedcolumns.
        //                    Assert.Null(columns.Single(c => c.Name == "SumOfAAndB").DefaultValueSql);
        //                    Assert.Equal(@"(""A"" + ""B"")", columns.Single(c => c.Name == "SumOfAAndB").ComputedColumnSql);
        //                },
        //                @"DROP TABLE ""ComputedValues""");

        //        [Fact]
        //        public void Default_value_matching_clr_default_is_not_stored()
        //            => Test(@"
        //CREATE DOMAIN ""decimalDomain"" AS decimal(6);

        //CREATE TABLE ""DefaultValues"" (
        //    ""IgnoredDefault1"" int DEFAULT NULL,
        //    ""IgnoredDefault2"" int NOT NULL DEFAULT NULL,
        //    ""IgnoredDefault9"" int NOT NULL DEFAULT 0,
        //    ""IgnoredDefault14"" smallint NOT NULL DEFAULT 0,
        //    ""IgnoredDefault3"" bigint NOT NULL DEFAULT 0,
        //    ""IgnoredDefault15"" decimal NOT NULL DEFAULT 0,
        //    ""IgnoredDefault16"" decimal NOT NULL DEFAULT 0.0,
        //    ""IgnoredDefault17"" ""decimalDomain"" NOT NULL DEFAULT 0,
        //    ""IgnoredDefault10"" money NOT NULL DEFAULT 0,
        //    ""IgnoredDefault19"" money NOT NULL DEFAULT 0.0,
        //    ""IgnoredDefault21"" float4 NOT NULL DEFAULT 0.0,
        //    ""IgnoredDefault7"" float8 NOT NULL DEFAULT 0,
        //    ""IgnoredDefault18"" float8 NOT NULL DEFAULT 0.0,
        //    ""IgnoredDefault24"" float8 NOT NULL DEFAULT 0E0,
        //    ""IgnoredDefault4"" bool NOT NULL DEFAULT false,
        //    ""IgnoredDefault25"" date NOT NULL DEFAULT '0001-01-01',
        //    ""IgnoredDefault26"" timestamp NOT NULL DEFAULT '1900-01-01T00:00:00.000',
        //    ""IgnoredDefault27"" interval NOT NULL DEFAULT '00:00:00',
        //    ""IgnoredDefault32"" time NOT NULL DEFAULT '00:00:00',
        //    ""IgnoredDefault34"" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    Assert.All(
        //                        columns,
        //                        t => Assert.Null(t.DefaultValueSql));
        //                },
        //                @"
        //DROP TABLE ""DefaultValues"";
        //DROP DOMAIN ""decimalDomain"";");

        //        [Fact]
        //        public void ValueGenerated_is_set_for_default_and_serial_column()
        //            => Test(@"
        //CREATE TABLE ""ValueGeneratedProperties"" (
        //    ""Id"" SERIAL,
        //    ""NoValueGenerationColumn"" text,
        //    ""FixedDefaultValue"" timestamp NOT NULL DEFAULT ('1999-01-08')
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    Assert.Equal(ValueGenerated.OnAdd, columns.Single(c => c.Name == "Id").ValueGenerated);
        //                    Assert.Null(columns.Single(c => c.Name == "NoValueGenerationColumn").ValueGenerated);
        //                    Assert.Null(columns.Single(c => c.Name == "FixedDefaultValue").ValueGenerated);
        //                },
        //                @"DROP TABLE ""ValueGeneratedProperties""");

        //        [MinimumPostgresVersionFact(10, 0)]
        //        public void ValueGenerated_is_set_for_identity_column()
        //            => Test(@"
        //CREATE TABLE ""ValueGeneratedProperties"" (
        //    ""Id1"" INT GENERATED ALWAYS AS IDENTITY,
        //    ""Id2"" INT GENERATED BY DEFAULT AS IDENTITY
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    Assert.Equal(ValueGenerated.OnAdd, columns.Single(c => c.Name == "Id1").ValueGenerated);
        //                    Assert.Equal(ValueGenerated.OnAdd, columns.Single(c => c.Name == "Id2").ValueGenerated);
        //                },
        //                @"DROP TABLE ""ValueGeneratedProperties""");

        //        [MinimumPostgresVersionFact(12, 0)]
        //        public void ValueGenerated_is_set_for_computed_column()
        //            => Test(@"
        //CREATE TABLE ""ValueGeneratedProperties"" (
        //    ""Id"" INT GENERATED ALWAYS AS IDENTITY,
        //    ""A"" int NOT NULL,
        //    ""B"" int NOT NULL,
        //    ""SumOfAAndB"" int GENERATED ALWAYS AS (""A"" + ""B"") STORED
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    Assert.Null(columns.Single(c => c.Name == "SumOfAAndB").ValueGenerated);
        //                },
        //                @"DROP TABLE ""ValueGeneratedProperties""");

        //        [Fact]
        //        public void Column_nullability_is_set()
        //            => Test(@"
        //CREATE TABLE ""NullableColumns"" (
        //    ""Id"" int,
        //    ""NullableInt"" int NULL,
        //    ""NonNullString"" text NOT NULL
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    Assert.True(columns.Single(c => c.Name == "NullableInt").IsNullable);
        //                    Assert.False(columns.Single(c => c.Name == "NonNullString").IsNullable);
        //                },
        //                @"DROP TABLE ""NullableColumns""");

        //        [Fact]
        //        public void System_columns_are_not_created()
        //            => Test(@"
        //CREATE TABLE ""SystemColumnsTable""
        //(
        //     ""Id"" int NOT NULL PRIMARY KEY
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var columns = dbModel.Tables.Single().Columns;

        //                    Assert.Equal(1, columns.Count);
        //                },
        //                @"DROP TABLE ""SystemColumnsTable""");

        #endregion

        //        #region PrimaryKeyFacets

        //        [Fact]
        //        public void Create_composite_primary_key()
        //            => Test(@"
        //CREATE TABLE ""CompositePrimaryKeyTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int,
        //    PRIMARY KEY (""Id2"", ""Id1"")
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var pk = dbModel.Tables.Single().PrimaryKey;

        //                    Assert.Equal("public", pk.Table.Schema);
        //                    Assert.Equal("CompositePrimaryKeyTable", pk.Table.Name);
        //                    Assert.Equal(new List<string> { "Id2", "Id1" }, pk.Columns.Select(ic => ic.Name).ToList());
        //                },
        //                @"DROP TABLE ""CompositePrimaryKeyTable""");

        //        [Fact]
        //        public void Set_primary_key_name_from_index()
        //            => Test(@"
        //CREATE TABLE ""PrimaryKeyName"" (
        //    ""Id1"" int,
        //    ""Id2"" int,
        //    CONSTRAINT ""MyPK"" PRIMARY KEY ( ""Id2"" )
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var pk = dbModel.Tables.Single().PrimaryKey;

        //                    Assert.Equal("public", pk.Table.Schema);
        //                    Assert.Equal("PrimaryKeyName", pk.Table.Name);
        //                    Assert.StartsWith("MyPK", pk.Name);
        //                    Assert.Equal(new List<string> { "Id2" }, pk.Columns.Select(ic => ic.Name).ToList());
        //                },
        //                @"DROP TABLE ""PrimaryKeyName""");

        //        #endregion

        //        #region UniqueConstraintFacets

        //        [Fact]
        //        public void Create_composite_unique_constraint()
        //            => Test(@"
        //CREATE TABLE ""CompositeUniqueConstraintTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int,
        //    CONSTRAINT ""UX"" UNIQUE (""Id2"", ""Id1"")
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var uniqueConstraint = Assert.Single(dbModel.Tables.Single().UniqueConstraints);

        //                    Assert.Equal("public", uniqueConstraint.Table.Schema);
        //                    Assert.Equal("CompositeUniqueConstraintTable", uniqueConstraint.Table.Name);
        //                    Assert.Equal("UX", uniqueConstraint.Name);
        //                    Assert.Equal(new List<string> { "Id2", "Id1" }, uniqueConstraint.Columns.Select(ic => ic.Name).ToList());
        //                },
        //                @"DROP TABLE ""CompositeUniqueConstraintTable""");

        //        [Fact]
        //        public void Set_unique_constraint_name_from_index()
        //            => Test(@"
        //CREATE TABLE ""UniqueConstraintName"" (
        //    ""Id1"" int,
        //    ""Id2"" int,
        //    CONSTRAINT ""MyUC"" UNIQUE ( ""Id2"" )
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();
        //                    var uniqueConstraint = Assert.Single(table.UniqueConstraints);

        //                    Assert.Equal("public", uniqueConstraint.Table.Schema);
        //                    Assert.Equal("UniqueConstraintName", uniqueConstraint.Table.Name);
        //                    Assert.Equal("MyUC", uniqueConstraint.Name);
        //                    Assert.Equal(new List<string> { "Id2" }, uniqueConstraint.Columns.Select(ic => ic.Name).ToList());
        //                    Assert.Empty(table.Indexes);
        //                },
        //                @"DROP TABLE ""UniqueConstraintName""");

        //        #endregion

        //        #region IndexFacets

        //        [Fact]
        //        public void Create_composite_index()
        //            => Test(@"
        //CREATE TABLE ""CompositeIndexTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int
        //);

        //CREATE INDEX ""IX_COMPOSITE"" ON ""CompositeIndexTable"" ( ""Id2"", ""Id1"" );",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var index = Assert.Single(dbModel.Tables.Single().Indexes);

        //                    Assert.Equal("public", index.Table.Schema);
        //                    Assert.Equal("CompositeIndexTable", index.Table.Name);
        //                    Assert.Equal("IX_COMPOSITE", index.Name);
        //                    Assert.Equal(new List<string> { "Id2", "Id1" }, index.Columns.Select(ic => ic.Name).ToList());
        //                },
        //                @"DROP TABLE ""CompositeIndexTable""");

        //        [Fact]
        //        public void Set_unique_true_for_unique_index()
        //            => Test(@"
        //CREATE TABLE ""UniqueIndexTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int
        //);

        //CREATE UNIQUE INDEX ""IX_UNIQUE"" ON ""UniqueIndexTable"" ( ""Id2"" );",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var index = Assert.Single(dbModel.Tables.Single().Indexes);

        //                    Assert.Equal("public", index.Table.Schema);
        //                    Assert.Equal("UniqueIndexTable", index.Table.Name);
        //                    Assert.Equal("IX_UNIQUE", index.Name);
        //                    Assert.True(index.IsUnique);
        //                    Assert.Null(index.Filter);
        //                    Assert.Equal(new List<string> { "Id2" }, index.Columns.Select(ic => ic.Name).ToList());
        //                },
        //                @"DROP TABLE ""UniqueIndexTable""");

        //        [Fact]
        //        public void Set_filter_for_filtered_index()
        //            => Test(@"
        //CREATE TABLE ""FilteredIndexTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int NULL
        //);

        //CREATE UNIQUE INDEX ""IX_UNIQUE"" ON ""FilteredIndexTable"" ( ""Id2"" ) WHERE ""Id2"" > 10;",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var index = Assert.Single(dbModel.Tables.Single().Indexes);

        //                    Assert.Equal("public", index.Table.Schema);
        //                    Assert.Equal("FilteredIndexTable", index.Table.Name);
        //                    Assert.Equal("IX_UNIQUE", index.Name);
        //                    Assert.Equal(@"(""Id2"" > 10)", index.Filter);
        //                    Assert.Equal(new List<string> { "Id2" }, index.Columns.Select(ic => ic.Name).ToList());
        //                },
        //                @"DROP TABLE ""FilteredIndexTable""");

        //        #endregion

        //        #region ForeignKeyFacets

        //        [Fact]
        //        public void Create_composite_foreign_key()
        //            => Test(@"
        //CREATE TABLE ""PrincipalTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int,
        //    PRIMARY KEY (""Id1"", ""Id2"")
        //);

        //CREATE TABLE ""DependentTable"" (
        //    ""Id"" int not null PRIMARY KEY,
        //    ""ForeignKeyId1"" int,
        //    ""ForeignKeyId2"" int,
        //    FOREIGN KEY (""ForeignKeyId1"", ""ForeignKeyId2"") REFERENCES ""PrincipalTable""(""Id1"", ""Id2"") ON DELETE CASCADE
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var fk = Assert.Single(dbModel.Tables.Single(t => t.Name == "DependentTable").ForeignKeys);

        //                    Assert.Equal("public", fk.Table.Schema);
        //                    Assert.Equal("DependentTable", fk.Table.Name);
        //                    Assert.Equal("public", fk.PrincipalTable.Schema);
        //                    Assert.Equal("PrincipalTable", fk.PrincipalTable.Name);
        //                    Assert.Equal(new List<string> { "ForeignKeyId1", "ForeignKeyId2" }, fk.Columns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(new List<string> { "Id1", "Id2" }, fk.PrincipalColumns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(ReferentialAction.Cascade, fk.OnDelete);
        //                },
        //                @"
        //DROP TABLE ""DependentTable"";
        //DROP TABLE ""PrincipalTable"";");

        //        [Fact]
        //        public void Create_multiple_foreign_key_in_same_table()
        //            => Test(@"
        //CREATE TABLE ""PrincipalTable"" (
        //    ""Id"" int not null PRIMARY KEY
        //);

        //CREATE TABLE ""AnotherPrincipalTable"" (
        //    ""Id"" int not null PRIMARY KEY
        //);

        //CREATE TABLE ""DependentTable"" (
        //    ""Id"" int not null PRIMARY KEY,
        //    ""ForeignKeyId1"" int,
        //    ""ForeignKeyId2"" int,
        //    FOREIGN KEY (""ForeignKeyId1"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE CASCADE,
        //    FOREIGN KEY (""ForeignKeyId2"") REFERENCES ""AnotherPrincipalTable""(""Id"") ON DELETE CASCADE
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var foreignKeys = dbModel.Tables.Single(t => t.Name == "DependentTable").ForeignKeys;

        //                    Assert.Equal(2, foreignKeys.Count);

        //                    var principalFk = Assert.Single(foreignKeys.Where(f => f.PrincipalTable.Name == "PrincipalTable"));

        //                    Assert.Equal("public", principalFk.Table.Schema);
        //                    Assert.Equal("DependentTable", principalFk.Table.Name);
        //                    Assert.Equal("public", principalFk.PrincipalTable.Schema);
        //                    Assert.Equal("PrincipalTable", principalFk.PrincipalTable.Name);
        //                    Assert.Equal(new List<string> { "ForeignKeyId1" }, principalFk.Columns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(new List<string> { "Id" }, principalFk.PrincipalColumns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(ReferentialAction.Cascade, principalFk.OnDelete);

        //                    var anotherPrincipalFk = Assert.Single(foreignKeys.Where(f => f.PrincipalTable.Name == "AnotherPrincipalTable"));

        //                    Assert.Equal("public", anotherPrincipalFk.Table.Schema);
        //                    Assert.Equal("DependentTable", anotherPrincipalFk.Table.Name);
        //                    Assert.Equal("public", anotherPrincipalFk.PrincipalTable.Schema);
        //                    Assert.Equal("AnotherPrincipalTable", anotherPrincipalFk.PrincipalTable.Name);
        //                    Assert.Equal(new List<string> { "ForeignKeyId2" }, anotherPrincipalFk.Columns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(new List<string> { "Id" }, anotherPrincipalFk.PrincipalColumns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(ReferentialAction.Cascade, anotherPrincipalFk.OnDelete);
        //                },
        //                @"
        //DROP TABLE ""DependentTable"";
        //DROP TABLE ""AnotherPrincipalTable"";
        //DROP TABLE ""PrincipalTable"";");

        //        [Fact]
        //        public void Create_foreign_key_referencing_unique_constraint()
        //            => Test(@"
        //CREATE TABLE ""PrincipalTable"" (
        //    ""Id1"" int,
        //    ""Id2"" int UNIQUE
        //);

        //CREATE TABLE ""DependentTable"" (
        //    ""Id"" int not null PRIMARY KEY,
        //    ""ForeignKeyId"" int,
        //    FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable""(""Id2"") ON DELETE CASCADE
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var fk = Assert.Single(dbModel.Tables.Single(t => t.Name == "DependentTable").ForeignKeys);

        //                    Assert.Equal("public", fk.Table.Schema);
        //                    Assert.Equal("DependentTable", fk.Table.Name);
        //                    Assert.Equal("public", fk.PrincipalTable.Schema);
        //                    Assert.Equal("PrincipalTable", fk.PrincipalTable.Name);
        //                    Assert.Equal(new List<string> { "ForeignKeyId" }, fk.Columns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(new List<string> { "Id2" }, fk.PrincipalColumns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(ReferentialAction.Cascade, fk.OnDelete);
        //                },
        //                @"
        //DROP TABLE ""DependentTable"";
        //DROP TABLE ""PrincipalTable"";");

        //        [Fact]
        //        public void Set_name_for_foreign_key()
        //            => Test(@"
        //CREATE TABLE ""PrincipalTable"" (
        //    ""Id"" int not null PRIMARY KEY
        //);

        //CREATE TABLE ""DependentTable"" (
        //    ""Id"" int not null PRIMARY KEY,
        //    ""ForeignKeyId"" int,
        //    CONSTRAINT ""MYFK"" FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE CASCADE
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var fk = Assert.Single(dbModel.Tables.Single(t => t.Name == "DependentTable").ForeignKeys);

        //                    Assert.Equal("public", fk.Table.Schema);
        //                    Assert.Equal("DependentTable", fk.Table.Name);
        //                    Assert.Equal("public", fk.PrincipalTable.Schema);
        //                    Assert.Equal("PrincipalTable", fk.PrincipalTable.Name);
        //                    Assert.Equal(new List<string> { "ForeignKeyId" }, fk.Columns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(new List<string> { "Id" }, fk.PrincipalColumns.Select(ic => ic.Name).ToList());
        //                    Assert.Equal(ReferentialAction.Cascade, fk.OnDelete);
        //                    Assert.Equal("MYFK", fk.Name);
        //                },
        //                @"
        //DROP TABLE ""DependentTable"";
        //DROP TABLE ""PrincipalTable"";");

        //        [Fact]
        //        public void Set_referential_action_for_foreign_key()
        //            => Test(@"
        //CREATE TABLE ""PrincipalTable"" (
        //    ""Id"" int not null PRIMARY KEY
        //);

        //CREATE TABLE ""DependentTable"" (
        //    ""Id"" int not null PRIMARY KEY,
        //    ""ForeignKeySetNullId"" int,
        //    ""ForeignKeyCascadeId"" int,
        //    ""ForeignKeyNoActionId"" int,
        //    ""ForeignKeyRestrictId"" int,
        //    ""ForeignKeySetDefaultId"" int,
        //    FOREIGN KEY (""ForeignKeySetNullId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE SET NULL,
        //    FOREIGN KEY (""ForeignKeyCascadeId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE CASCADE,
        //    FOREIGN KEY (""ForeignKeyNoActionId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE NO ACTION,
        //    FOREIGN KEY (""ForeignKeyRestrictId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE RESTRICT,
        //    FOREIGN KEY (""ForeignKeySetDefaultId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE SET DEFAULT
        //);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single(t => t.Name == "DependentTable");

        //                    foreach (var fk in table.ForeignKeys)
        //                    {
        //                        Assert.Equal("public", fk.Table.Schema);
        //                        Assert.Equal("DependentTable", fk.Table.Name);
        //                        Assert.Equal("public", fk.PrincipalTable.Schema);
        //                        Assert.Equal("PrincipalTable", fk.PrincipalTable.Name);
        //                        Assert.Equal(new List<string> { "Id" }, fk.PrincipalColumns.Select(ic => ic.Name).ToList());
        //                    }

        //                    Assert.Equal(ReferentialAction.SetNull, table.ForeignKeys.Single(fk => fk.Columns.Single().Name == "ForeignKeySetNullId").OnDelete);
        //                    Assert.Equal(ReferentialAction.Cascade, table.ForeignKeys.Single(fk => fk.Columns.Single().Name == "ForeignKeyCascadeId").OnDelete);
        //                    Assert.Equal(ReferentialAction.NoAction, table.ForeignKeys.Single(fk => fk.Columns.Single().Name == "ForeignKeyNoActionId").OnDelete);
        //                    Assert.Equal(ReferentialAction.Restrict, table.ForeignKeys.Single(fk => fk.Columns.Single().Name == "ForeignKeyRestrictId").OnDelete);
        //                    Assert.Equal(ReferentialAction.SetDefault, table.ForeignKeys.Single(fk => fk.Columns.Single().Name == "ForeignKeySetDefaultId").OnDelete);
        //                },
        //                @"
        //DROP TABLE ""DependentTable"";
        //DROP TABLE ""PrincipalTable"";");

        //        #endregion

        //        #region Warnings

        //        [Fact]
        //        public void Warn_missing_schema()
        //            => Test(@"
        //CREATE TABLE ""Blank"" (
        //    ""Id"" int
        //)",
        //                Enumerable.Empty<string>(),
        //                new[] { "MySchema" },
        //                dbModel =>
        //                {
        //                    Assert.Empty(dbModel.Tables);

        //                    var (_, Id, Message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log.Where(t => t.Level == LogLevel.Warning));

        //                    Assert.Equal(ActianResources.LogMissingSchema(new TestLogger<ActianLoggingDefinitions>()).EventId, Id);
        //                    Assert.Equal(ActianResources.LogMissingSchema(new TestLogger<ActianLoggingDefinitions>()).GenerateMessage("MySchema"), Message);
        //                },
        //                @"DROP TABLE ""Blank""");

        //        [Fact]
        //        public void Warn_missing_table()
        //            => Test(@"
        //CREATE TABLE ""Blank"" (
        //    ""Id"" int
        //)",
        //                new[] { "MyTable" },
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    Assert.Empty(dbModel.Tables);

        //                    var (_, Id, Message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log.Where(t => t.Level == LogLevel.Warning));

        //                    Assert.Equal(ActianResources.LogMissingTable(new TestLogger<ActianLoggingDefinitions>()).EventId, Id);
        //                    Assert.Equal(ActianResources.LogMissingTable(new TestLogger<ActianLoggingDefinitions>()).GenerateMessage("MyTable"), Message);
        //                },
        //                @"DROP TABLE ""Blank""");

        //        [Fact]
        //        public void Warn_missing_principal_table_for_foreign_key()
        //            => Test(@"
        //CREATE TABLE ""PrincipalTable"" (
        //    ""Id"" int not null PRIMARY KEY
        //);

        //CREATE TABLE ""DependentTable"" (
        //    ""Id"" int not null PRIMARY KEY,
        //    ""ForeignKeyId"" int,
        //    CONSTRAINT ""MYFK"" FOREIGN KEY (""ForeignKeyId"") REFERENCES ""PrincipalTable""(""Id"") ON DELETE CASCADE
        //);",
        //                new[] { "DependentTable" },
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var (_, Id, Message, _, _) = Assert.Single(Fixture.ListLoggerFactory.Log.Where(t => t.Level == LogLevel.Warning));

        //                    Assert.Equal(ActianResources.LogPrincipalTableNotInSelectionSet(new TestLogger<ActianLoggingDefinitions>()).EventId, Id);
        //                    Assert.Equal(
        //                        ActianResources.LogPrincipalTableNotInSelectionSet(new TestLogger<ActianLoggingDefinitions>()).GenerateMessage(
        //                            "MYFK", "public.DependentTable", "public.PrincipalTable"), Message);
        //                },
        //                @"
        //DROP TABLE ""DependentTable"";
        //DROP TABLE ""PrincipalTable"";");

        //        #endregion

        //        #region Actian-specific

        //        [Fact]
        //        public void SequenceSerial() =>
        //            Test(@"
        //CREATE TABLE serial_sequence (id serial PRIMARY KEY);
        //CREATE TABLE ""SerialSequence"" (""Id"" serial PRIMARY KEY);
        //CREATE SCHEMA my_schema;
        //CREATE TABLE my_schema.serial_sequence_in_schema (Id serial PRIMARY KEY);
        //CREATE TABLE my_schema.""SerialSequenceInSchema"" (""Id"" serial PRIMARY KEY);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    // Sequences which belong to a serial column should not get reverse engineered as separate sequences
        //                    Assert.Empty(dbModel.Sequences);

        //                    // Now make sure the field itself is properly reverse-engineered.
        //                    foreach (var column in dbModel.Tables.Select(t => t.Columns.Single()))
        //                    {
        //                        Assert.Equal(ValueGenerated.OnAdd, column.ValueGenerated);
        //                        Assert.Null(column.DefaultValueSql);
        //                        Assert.Equal(ActianValueGenerationStrategy.SerialColumn, (ActianValueGenerationStrategy)column[ActianAnnotationNames.ValueGenerationStrategy]);
        //                    }
        //                },
        //                @"DROP TABLE serial_sequence; DROP TABLE ""SerialSequence""; DROP SCHEMA my_schema CASCADE");

        //        [Fact]
        //        public void SequenceNonSerial() =>
        //            Test(@"
        //CREATE SEQUENCE ""SomeSequence"";
        //CREATE TABLE ""NonSerialSequence"" (""Id"" integer PRIMARY KEY DEFAULT nextval('""SomeSequence""'))",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var column = dbModel.Tables.Single().Columns.Single();
        //                    Assert.Equal(@"nextval('""SomeSequence""'::regclass)", column.DefaultValueSql);
        //                    // Actian has special detection for serial columns (scaffolding them with ValueGenerated.OnAdd
        //                    // and removing the default), but not for non-serial sequence-driven columns, which are scaffolded
        //                    // with a DefaultValue. This is consistent with the Actian scaffolding behavior.
        //                    Assert.Null(column.ValueGenerated);

        //                    Assert.Single(dbModel.Sequences.Where(s => s.Name == "SomeSequence"));
        //                },
        //                @"
        //DROP TABLE ""NonSerialSequence"";
        //DROP SEQUENCE ""SomeSequence""");

        //        [MinimumPostgresVersionFact(10, 0)]
        //        public void Identity()
        //            => Test(@"
        //CREATE TABLE identity (
        //    id int GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
        //    a int GENERATED ALWAYS AS IDENTITY,
        //    b int GENERATED BY DEFAULT AS IDENTITY
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var idIdentityAlways = dbModel.Tables.Single().Columns.Single(c => c.Name == "id");
        //                    Assert.Equal(ValueGenerated.OnAdd, idIdentityAlways.ValueGenerated);
        //                    Assert.Null(idIdentityAlways.DefaultValueSql);
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityAlwaysColumn, (ActianValueGenerationStrategy)idIdentityAlways[ActianAnnotationNames.ValueGenerationStrategy]);

        //                    var identityAlways = dbModel.Tables.Single().Columns.Single(c => c.Name == "a");
        //                    Assert.Equal(ValueGenerated.OnAdd, identityAlways.ValueGenerated);
        //                    Assert.Null(identityAlways.DefaultValueSql);
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityAlwaysColumn, (ActianValueGenerationStrategy)identityAlways[ActianAnnotationNames.ValueGenerationStrategy]);

        //                    var identityByDefault = dbModel.Tables.Single().Columns.Single(c => c.Name == "b");
        //                    Assert.Equal(ValueGenerated.OnAdd, identityByDefault.ValueGenerated);
        //                    Assert.Null(identityByDefault.DefaultValueSql);
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityByDefaultColumn, (ActianValueGenerationStrategy)identityByDefault[ActianAnnotationNames.ValueGenerationStrategy]);
        //                },
        //                "DROP TABLE identity");

        //        [MinimumPostgresVersionFact(10, 0)]
        //        public void Identity_with_sequence_options_all()
        //            => Test(@"
        //CREATE TABLE identity (
        //    with_options int GENERATED BY DEFAULT AS IDENTITY (START WITH 5 INCREMENT BY 2 MINVALUE 3 MAXVALUE 2000 CYCLE CACHE 10),
        //    without_options int GENERATED BY DEFAULT AS IDENTITY,
        //    bigint_without_options bigint GENERATED BY DEFAULT AS IDENTITY,
        //    smallint_without_options smallint GENERATED BY DEFAULT AS IDENTITY
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var withOptions = dbModel.Tables.Single().Columns.Single(c => c.Name == "with_options");
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityByDefaultColumn, (ActianValueGenerationStrategy)withOptions[ActianAnnotationNames.ValueGenerationStrategy]);
        //                    Assert.Equal(new IdentitySequenceOptionsData
        //                    {
        //                        StartValue = 5,
        //                        MinValue = 3,
        //                        MaxValue = 2000,
        //                        IncrementBy = 2,
        //                        IsCyclic = true,
        //                        NumbersToCache = 10
        //                    }.Serialize(), withOptions[ActianAnnotationNames.IdentityOptions]);

        //                    var withoutOptions = dbModel.Tables.Single().Columns.Single(c => c.Name == "without_options");
        //                    Assert.Equal("integer", withOptions.StoreType);
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityByDefaultColumn,
        //                        (ActianValueGenerationStrategy)withoutOptions[ActianAnnotationNames.ValueGenerationStrategy]);
        //                    Assert.Null(withoutOptions[ActianAnnotationNames.IdentityOptions]);

        //                    var bigintWithoutOptions = dbModel.Tables.Single().Columns.Single(c => c.Name == "bigint_without_options");
        //                    Assert.Equal("bigint", bigintWithoutOptions.StoreType);
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityByDefaultColumn,
        //                        (ActianValueGenerationStrategy)bigintWithoutOptions[ActianAnnotationNames.ValueGenerationStrategy]);
        //                    Assert.Null(bigintWithoutOptions[ActianAnnotationNames.IdentityOptions]);

        //                    var smallintWithoutOptions = dbModel.Tables.Single().Columns.Single(c => c.Name == "smallint_without_options");
        //                    Assert.Equal("smallint", smallintWithoutOptions.StoreType);
        //                    Assert.Equal(ActianValueGenerationStrategy.IdentityByDefaultColumn,
        //                        (ActianValueGenerationStrategy)smallintWithoutOptions[ActianAnnotationNames.ValueGenerationStrategy]);
        //                    Assert.Null(smallintWithoutOptions[ActianAnnotationNames.IdentityOptions]);
        //                },
        //                "DROP TABLE identity");

        //        [Fact]
        //        public void Index_method()
        //            => Test(@"
        //CREATE TABLE ""IndexMethod"" (a int, b int);
        //CREATE INDEX ix_a ON ""IndexMethod"" USING hash (a);
        //CREATE INDEX ix_b ON ""IndexMethod"" (b);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();
        //                    Assert.Equal(2, table.Indexes.Count);

        //                    var methodIndex = table.Indexes.Single(i => i.Name == "ix_a");
        //                    Assert.Equal("hash", methodIndex.FindAnnotation(ActianAnnotationNames.IndexMethod).Value);

        //                    // It's cleaner to always output the index method on the database model,
        //                    // even when it's btree (the default);
        //                    // ActianAnnotationCodeGenerator can then omit it as by-convention.
        //                    // However, because of https://github.com/aspnet/EntityFrameworkCore/issues/11846 we omit
        //                    // the annotation from the model entirely.
        //                    var noMethodIndex = table.Indexes.Single(i => i.Name == "ix_b");
        //                    Assert.Null(noMethodIndex.FindAnnotation(ActianAnnotationNames.IndexMethod));
        //                    //Assert.Equal("btree", noMethodIndex.FindAnnotation(ActianAnnotationNames.IndexMethod).Value);
        //                },
        //                @"DROP TABLE ""IndexMethod""");

        //        [Fact]
        //        public void Index_operators()
        //            => Test(@"
        //CREATE TABLE ""IndexOperators"" (a text, b text);
        //CREATE INDEX ix_with ON ""IndexOperators"" (a, b varchar_pattern_ops);
        //CREATE INDEX ix_without ON ""IndexOperators"" (a, b);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();

        //                    var indexWith = table.Indexes.Single(i => i.Name == "ix_with");
        //                    Assert.Equal(new[] { null, "varchar_pattern_ops" }, indexWith.FindAnnotation(ActianAnnotationNames.IndexOperators).Value);

        //                    var indexWithout = table.Indexes.Single(i => i.Name == "ix_without");
        //                    Assert.Null(indexWithout.FindAnnotation(ActianAnnotationNames.IndexOperators));
        //                },
        //                @"DROP TABLE ""IndexOperators""");

        //        [Fact]
        //        public void Index_collation()
        //            => Test(@"
        //CREATE TABLE ""IndexCollation"" (a text, b text);
        //CREATE INDEX ix_with ON ""IndexCollation"" (a, b COLLATE ""POSIX"");
        //CREATE INDEX ix_without ON ""IndexCollation"" (a, b);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();

        //                    var indexWith = table.Indexes.Single(i => i.Name == "ix_with");
        //                    Assert.Equal(new[] { null, "POSIX" }, indexWith.FindAnnotation(ActianAnnotationNames.IndexCollation).Value);

        //                    var indexWithout = table.Indexes.Single(i => i.Name == "ix_without");
        //                    Assert.Null(indexWithout.FindAnnotation(ActianAnnotationNames.IndexCollation));
        //                },
        //                @"DROP TABLE ""IndexCollation""");

        //        [Theory]
        //        [InlineData("gin", null)]
        //        [InlineData("gist", null)]
        //        [InlineData("hash", null)]
        //        [InlineData("brin", null)]
        //        [InlineData("btree", new[] { SortOrder.Ascending, SortOrder.Descending })]
        //        public void Index_sort_order(string method, SortOrder[] expected)
        //            => Test(@"
        //CREATE TABLE ""IndexSortOrder"" (a text, b text, c tsvector);
        //CREATE INDEX ix_gin ON ""IndexSortOrder"" USING gin (c);
        //CREATE INDEX ix_gist ON ""IndexSortOrder"" USING gist (c);
        //CREATE INDEX ix_hash ON ""IndexSortOrder"" USING hash (a);
        //CREATE INDEX ix_brin ON ""IndexSortOrder"" USING brin (a);
        //CREATE INDEX ix_btree ON ""IndexSortOrder"" USING btree (a ASC, b DESC);
        //CREATE INDEX ix_without ON ""IndexSortOrder"" (a, b);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();

        //                    var indexWith = table.Indexes.Single(i => i.Name == $"ix_{method}");
        //                    Assert.Equal(expected, indexWith.FindAnnotation(ActianAnnotationNames.IndexSortOrder)?.Value);

        //                    var indexWithout = table.Indexes.Single(i => i.Name == "ix_without");
        //                    Assert.Null(indexWithout.FindAnnotation(ActianAnnotationNames.IndexSortOrder));
        //                },
        //                @"DROP TABLE ""IndexSortOrder""");

        //        [Fact]
        //        public void Index_null_sort_order()
        //            => Test(@"
        //CREATE TABLE ""IndexNullSortOrder"" (a text, b text);
        //CREATE INDEX ix_with ON ""IndexNullSortOrder"" (a NULLS FIRST, b DESC NULLS LAST);
        //CREATE INDEX ix_without ON ""IndexNullSortOrder"" (a, b);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();

        //                    var indexWith = table.Indexes.Single(i => i.Name == "ix_with");
        //                    Assert.Equal(new[] { NullSortOrder.NullsFirst, NullSortOrder.NullsLast }, indexWith.FindAnnotation(ActianAnnotationNames.IndexNullSortOrder).Value);

        //                    var indexWithout = table.Indexes.Single(i => i.Name == "ix_without");
        //                    Assert.Null(indexWithout.FindAnnotation(ActianAnnotationNames.IndexNullSortOrder));
        //                },
        //                @"DROP TABLE ""IndexNullSortOrder""");

        //        [MinimumPostgresVersionFact(11, 0)]
        //        public void Index_covering()
        //            => Test(@"
        //CREATE TABLE ""IndexCovering"" (a text, b text, c text);
        //CREATE INDEX ix_with ON ""IndexCovering"" (a) INCLUDE (b, c);
        //CREATE INDEX ix_without ON ""IndexCovering"" (a, b, c);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();

        //                    var indexWith = table.Indexes.Single(i => i.Name == "ix_with");
        //                    Assert.Equal("a", indexWith.Columns.Single().Name);
        //                    Assert.Equal(new[] { "b", "c" }, indexWith.FindAnnotation(ActianAnnotationNames.IndexInclude).Value);

        //                    var indexWithout = table.Indexes.Single(i => i.Name == "ix_without");
        //                    Assert.Equal(new[] { "a", "b", "c" }, indexWithout.Columns.Select(i => i.Name).ToArray());
        //                    Assert.Null(indexWithout.FindAnnotation(ActianAnnotationNames.IndexInclude));

        //                },
        //                @"DROP TABLE ""IndexCovering""");

        //        [Fact]
        //        public void Comments()
        //            => Test(@"
        //CREATE TABLE comment (a int);
        //COMMENT ON TABLE comment IS 'table comment';
        //COMMENT ON COLUMN comment.a IS 'column comment'",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var table = dbModel.Tables.Single();
        //                    Assert.Equal("table comment", table.Comment);
        //                    Assert.Equal("column comment", table.Columns.Single().Comment);
        //                },
        //                "DROP TABLE comment");

        //        [MinimumPostgresVersionFact(11, 0)]
        //        public void Sequence_types()
        //            => Test(@"
        //CREATE SEQUENCE ""SmallIntSequence"" AS smallint;
        //CREATE SEQUENCE ""IntSequence"" AS int;
        //CREATE SEQUENCE ""BigIntSequence"" AS bigint;",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var smallSequence = dbModel.Sequences.Single(s => s.Name == "SmallIntSequence");
        //                    Assert.Equal("smallint", smallSequence.StoreType);
        //                    var intSequence = dbModel.Sequences.Single(s => s.Name == "IntSequence");
        //                    Assert.Equal("integer", intSequence.StoreType);
        //                    var bigSequence = dbModel.Sequences.Single(s => s.Name == "BigIntSequence");
        //                    Assert.Equal("bigint", bigSequence.StoreType);
        //                },
        //                @"
        //DROP SEQUENCE ""SmallIntSequence"";
        //DROP SEQUENCE ""IntSequence"";
        //DROP SEQUENCE ""BigIntSequence"";");

        //        [Fact]
        //        public void Dropped_columns()
        //            => Test(@"
        //CREATE TABLE foo (id int primary key);
        //ALTER TABLE foo DROP COLUMN id;
        //ALTER TABLE foo ADD COLUMN id2 int primary key;",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    Assert.Single(dbModel.Tables.Single().Columns);
        //                },
        //                "DROP TABLE foo");

        //        [Fact]
        //        public void Postgres_extensions()
        //            => Test(@"
        //CREATE EXTENSION hstore;
        //CREATE EXTENSION pgcrypto;",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var extensions = dbModel.GetPostgresExtensions();
        //                    Assert.Equal(2, extensions.Count);
        //                    Assert.Single(extensions, e => e.Name == "hstore");
        //                    Assert.Single(extensions, e => e.Name == "pgcrypto");
        //                },
        //                "DROP EXTENSION hstore; DROP EXTENSION pgcrypto");

        //        [Fact]
        //        public void Enums()
        //            => Test(@"
        //CREATE TYPE mood AS ENUM ('happy', 'sad');
        //CREATE TYPE efcore_test2.mood AS ENUM ('excited', 'depressed');
        //CREATE TABLE foo (mood mood UNIQUE);",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var enums = dbModel.GetPostgresEnums();
        //                    Assert.Equal(2, enums.Count);

        //                    var mood = enums.Single(e => e.Schema == null);
        //                    Assert.Equal("mood", mood.Name);
        //                    Assert.Equal(new[] { "happy", "sad" }, mood.Labels);

        //                    var mood2 = enums.Single(e => e.Schema == "efcore_test2");
        //                    Assert.Equal("mood", mood2.Name);
        //                    Assert.Equal(new[] { "excited", "depressed" }, mood2.Labels);

        //                    var table = Assert.Single(dbModel.Tables);
        //                    Assert.NotNull(table);

        //                    // Enum columns are left out of the model for now (a warning is logged).
        //                    Assert.Empty(table.Columns);
        //                    // Constraints and indexes over enum columns also need to be left out
        //                    Assert.Empty(table.UniqueConstraints);
        //                    Assert.Empty(table.Indexes);
        //                },
        //                "DROP TABLE foo; DROP TYPE mood; DROP TYPE efcore_test2.mood;");

        //        [Fact]
        //        public void Bug453()
        //            => Test(@"
        //CREATE TYPE mood AS ENUM ('happy', 'sad');
        //CREATE TABLE foo (mood mood, some_num int UNIQUE);
        //CREATE TABLE bar (foreign_key int REFERENCES foo(some_num))",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    // Enum columns are left out of the model for now (a warning is logged).
        //                    Assert.Equal(1, dbModel.Tables.Single(t => t.Name == "foo").Columns.Count);
        //                },
        //                "DROP TABLE bar; DROP TABLE foo; DROP TYPE mood;");

        //        [Fact]
        //        public void Column_default_type_names_are_scaffolded()
        //        {
        //            var options = new ActianOptions();
        //            options.Initialize(new DbContextOptionsBuilder().Options);

        //            Test(@"
        //CREATE TABLE column_types (
        //    smallint smallint,
        //    integer integer,
        //    bigint bigint,
        //    real real,
        //    ""double precision"" double precision,
        //    money money,
        //    numeric numeric,
        //    boolean boolean,
        //    bytea bytea,
        //    uuid uuid,
        //    text text,
        //    jsonb jsonb,
        //    json json,
        //    ""character varying"" character varying,
        //    ""character(1)"" character,
        //    ""character(2)"" character(2),
        //    ""timestamp without time zone"" timestamp,
        //    ""timestamp with time zone"" timestamptz,
        //    ""time without time zone"" time,
        //    ""time with time zone"" timetz,
        //    interval interval,
        //    macaddr macaddr,
        //    inet inet,
        //    ""bit(1)"" bit,
        //    ""bit varying"" varbit,
        //    point point,
        //    line line
        //)",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    var typeMappingSource = new ActianTypeMappingSource(
        //                        new TypeMappingSourceDependencies(
        //                            new ValueConverterSelector(new ValueConverterSelectorDependencies()),
        //                            Array.Empty<ITypeMappingSourcePlugin>()
        //                        ),
        //                        new RelationalTypeMappingSourceDependencies(Array.Empty<IRelationalTypeMappingSourcePlugin>()),
        //                        new ActianSqlGenerationHelper(new RelationalSqlGenerationHelperDependencies()),
        //                        options
        //                    );

        //                    foreach (var column in dbModel.Tables.Single().Columns)
        //                    {
        //                        Assert.Equal(column.Name, column.StoreType);
        //                        Assert.Equal(
        //                            column.StoreType,
        //                            typeMappingSource.FindMapping(column.StoreType).StoreType
        //                        );
        //                    }
        //                },
        //                "DROP TABLE column_types");
        //        }

        //        [Fact]
        //        public void System_tables_are_ignored()
        //            => Test(
        //                "CREATE EXTENSION postgis",
        //                Enumerable.Empty<string>(),
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    Assert.Empty(dbModel.Tables);
        //                },
        //                "DROP EXTENSION postgis");

        //        [Fact]
        //        public void System_tables_are_not_ignored_if_explicitly_requested()
        //            => Test(
        //                "CREATE EXTENSION postgis",
        //                new[] { "spatial_ref_sys" },
        //                Enumerable.Empty<string>(),
        //                dbModel =>
        //                {
        //                    Assert.Equal("spatial_ref_sys", dbModel.Tables.Single().Name);
        //                },
        //                "DROP EXTENSION postgis");

        //        #endregion

        private void Test(string createSql, Action<DatabaseModel> asserter, string cleanupSql = null)
        {
            Test(createSql, null, null, asserter, cleanupSql);
        }

        private void Test(string createSql, IEnumerable<string> tables, IEnumerable<string> schemas, Action<DatabaseModel> asserter, string cleanupSql = null)
        {
            //Fixture.TestStore.CleanObjects();
            Fixture.TestStore.ExecuteStatementsIgnoreErrors(
                "SET SESSION AUTHORIZATION efcore_test1;",
                cleanupSql
            );

            Fixture.TestStore.ExecuteStatements(
                "SET SESSION AUTHORIZATION efcore_test1;",
                createSql
            );

            try
            {
                var databaseModelFactory = new ActianDatabaseModelFactory(
                    new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                        Fixture.ListLoggerFactory,
                        new LoggingOptions(),
                        new DiagnosticListener("Fake"),
                        new ActianLoggingDefinitions()));

                var databaseModel = databaseModelFactory.Create(
                    Fixture.TestStore.ConnectionString,
                    new DatabaseModelFactoryOptions(tables, schemas)
                );
                Assert.NotNull(databaseModel);
                asserter(Log(databaseModel));
            }
            finally
            {
                Fixture.TestStore.ExecuteStatementsIgnoreErrors(
                    "SET SESSION AUTHORIZATION efcore_test1;",
                    cleanupSql
                );
            }
        }

        private DatabaseModel Log(DatabaseModel dbModel)
        {
            Output.WriteLine($"Server version: {Fixture.TestStore.GetServerVersion()}");
            Output.WriteLine($"DatabaseName: {dbModel.DatabaseName}");
            Output.WriteLine($"DefaultSchema: {dbModel.DefaultSchema}");
            Output.WriteLine($"Tables:");
            foreach (var table in dbModel.Tables)
            {
                Output.WriteLine($"  {table.Schema}.{table.Name}");
            }
            Output.WriteLine($"Sequences:");
            foreach (var sequence in dbModel.Sequences)
            {
                Output.WriteLine($"  {sequence.Schema}.{sequence.Name}");
            }
            return dbModel;
        }

        private IEnumerable<T> Items<T>(params T[] items)
        {
            return items;
        }

        public class ActianDatabaseModelFixture : SharedStoreFixtureBase<PoolableDbContext>
        {
            protected override string StoreName { get; } = "EFCore_DatabaseModelFactory";
            protected override ITestStoreFactory TestStoreFactory => ActianTestStoreFactory.Instance;
            public new ActianTestStore TestStore { get; }
            private readonly ActianTestStore IIDbDbStore;

            public ActianDatabaseModelFixture()
            {
                TestStore = (ActianTestStore)base.TestStore;
                if (TestStore.ConnectionState != ConnectionState.Closed)
                {
                    TestStore.CloseConnection();
                }
                IIDbDbStore = ActianTestStore.GetIIDbDb();

                CreateUser("efcore_test1");
                CreateUser("efcore_test2");
                CreateUser("efcore_test.2");

                IIDbDbStore.ExecuteNonQuery(@$"grant access, db_admin on database {StoreName} to public");
            }

            private void CreateUser(string user)
            {
                if (!UserExists(user))
                {
                    IIDbDbStore.ExecuteNonQuery(@$"create user ""{user}""");
                }
            }

            private bool UserExists(string user)
            {
                return IIDbDbStore.ExecuteScalar<int?>(@$"
                    select 1
                      from iiusers
                     where user_name = '{user.Replace("'", "''")}'
                ") == 1;
            }

            protected override bool ShouldLogCategory(string logCategory)
                => logCategory == DbLoggerCategory.Scaffolding.Name;

            protected override void Clean(DbContext context)
            {
                base.Clean(context);
            }

            public override void Dispose()
            {
                base.Dispose();
                IIDbDbStore.Dispose();
            }
        }
    }
}
