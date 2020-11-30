using System.Linq;
using Actian.EFCore.TestUtilities.TestAttributes;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class ColumnFacets : ActianDatabaseModelFactoryTestBase
    {
        public ColumnFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        // Note: in Actian numeric is simply an alias for decimal
        [ConditionalFact]
        public void Decimal_types_have_precision_scale() => Test(test => test
            .Arrange(@"
                CREATE TABLE NumericColumns (
                    decimalColumn                 decimal        NOT NULL,
                    decimal105Column              decimal(10, 5) NOT NULL,
                    decimalDefaultColumn          decimal(18, 2) NOT NULL,
                    numericColumn                 numeric        NOT NULL,
                    numeric152Column              numeric(15, 2) NOT NULL,
                    numericDefaultColumn          numeric(18, 2) NOT NULL,
                    numericDefaultPrecisionColumn numeric(38, 5) NOT NULL
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "numericcolumns",
                    Columns = Items(
                        new { Name = "decimalcolumn", StoreType = "decimal" },
                        new { Name = "decimal105column", StoreType = "decimal(10,5)" },
                        new { Name = "decimaldefaultcolumn", StoreType = "decimal(18,2)" },
                        new { Name = "numericcolumn", StoreType = "decimal" },
                        new { Name = "numeric152column", StoreType = "decimal(15,2)" },
                        new { Name = "numericdefaultcolumn", StoreType = "decimal(18,2)" },
                        new { Name = "numericdefaultprecisioncolumn", StoreType = "decimal(38,5)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Specific_max_length_are_added_to_store_type() => Test(test => test
            .Arrange(@"
                CREATE TABLE LengthColumns (
                    char10Column                       char(10)                   NULL,
                    varchar66Column                    varchar(66)                NULL,
                    nchar99Column                      nchar(99)                  NULL,
                    nvarchar100Column                  nvarchar(100)              NULL,
                    binary111Column                    binary(111)                NULL,
                    varbinary123Column                 varbinary(123)             NULL,
                    binaryVarying133Column             binary varying(133)        NULL
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "lengthcolumns",
                    Columns = Items(
                        new { Name = "char10column", StoreType = "char(10)" },
                        new { Name = "varchar66column", StoreType = "varchar(66)" },
                        new { Name = "nchar99column", StoreType = "nchar(99)" },
                        new { Name = "nvarchar100column", StoreType = "nvarchar(100)" },
                        new { Name = "binary111column", StoreType = "byte(111)" },
                        new { Name = "varbinary123column", StoreType = "byte varying(123)" },
                        new { Name = "binaryvarying133column", StoreType = "byte varying(133)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Default_max_length_are_added_to_binary_varbinary() => Test(test => test
            .Arrange(@"
                CREATE TABLE DefaultRequiredLengthBinaryColumns (
                    binaryColumn binary(8000),
                    binaryVaryingColumn binary varying(8000),
                    varbinaryColumn varbinary(8000)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultrequiredlengthbinarycolumns",
                    Columns = Items(
                        new { Name = "binarycolumn", StoreType = "byte(8000)" },
                        new { Name = "binaryvaryingcolumn", StoreType = "byte varying(8000)" },
                        new { Name = "varbinarycolumn", StoreType = "byte varying(8000)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Default_max_length_are_added_to_char() => Test(test => test
            .Arrange(@"
                CREATE TABLE DefaultRequiredLengthCharColumns (
                    charColumn char(8000)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultrequiredlengthcharcolumns",
                    Columns = Items(
                        new { Name = "charcolumn", StoreType = "char(8000)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Default_max_length_are_added_to_varchar() => Test(test => test
            .Arrange(@"
                CREATE TABLE DefaultRequiredLengthVarcharColumns (
                    varcharColumn varchar(8000)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultrequiredlengthvarcharcolumns",
                    Columns = Items(
                        new { Name = "varcharcolumn", StoreType = "varchar(8000)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Default_max_length_are_added_to_nchar() => Test(test => test
            .Arrange(@"
                CREATE TABLE DefaultRequiredLengthNcharColumns (
                    ncharColumn nchar(8000)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultrequiredlengthncharcolumns",
                    Columns = Items(
                        new { Name = "ncharcolumn", StoreType = "nchar(8000)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Default_max_length_are_added_to_nvarchar() => Test(test => test
            .Arrange(@"
                CREATE TABLE DefaultRequiredLengthNvarcharColumns (
                    nvarcharColumn nvarchar(4000)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultrequiredlengthnvarcharcolumns",
                    Columns = Items(
                        new { Name = "nvarcharcolumn", StoreType = "nvarchar(4000)" }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Datetime_types_have_precision_if_non_null_scale() => Test(test => test
           .Arrange(@"
                CREATE TABLE DatetimeColumns (
                    time1Column        time(1)                     NULL,
                    timetz2Column      time(2) with time zone      NULL,
                    timestamp3Column   timestamp(3)                NULL,
                    timestamptz4Column timestamp(4) with time zone NULL,
                    interval5Column    interval day to second(5)   NULL
                )
            ")
            .Assert(dbModel => dbModel.Tables
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
                })
             )
        );

        [ConditionalFact]
        public void Types_with_required_length_uses_length_of_one() => Test(test => test
           .Arrange(@"
                CREATE TABLE OneLengthColumns (
                    binaryColumn                   binary                NULL,
                    binaryVaryingColumn            binary varying        NULL,
                    charColumn                     char                  NULL,
                    ncharColumn                    nchar                 NULL,
                    nvarcharColumn                 nvarchar              NULL,
                    varbinaryColumn                varbinary             NULL,
                    varcharColumn                  varchar               NULL
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "onelengthcolumns",
                    Columns = Items(
                        new { Name = "binarycolumn", StoreType = "byte(1)" },
                        new { Name = "binaryvaryingcolumn", StoreType = "byte varying(1)" },
                        new { Name = "charcolumn", StoreType = "char(1)" },
                        new { Name = "ncharcolumn", StoreType = "nchar(1)" },
                        new { Name = "nvarcharcolumn", StoreType = "nvarchar(1)" },
                        new { Name = "varbinarycolumn", StoreType = "byte varying(1)" },
                        new { Name = "varcharcolumn", StoreType = "varchar(1)" }
                    )
                })
             )
        );

        [ConditionalFact]
        public void Store_types_without_any_facets() => Test(test => test
            .Arrange(@"
                CREATE TABLE NoFacetTypes (
                    booleanColumn boolean,
                    floatColumn float4,
                    doubleColumn float,
                    decimalColumn decimal,
                    moneyColumn money,
                    -- guidColumn uuid,
                    byteColumn integer1,
                    shortColumn integer2,
                    intColumn integer4,
                    longColumn integer8,
                    timeColumn time,
                    timetzColumn time with time zone,
                    timestampColumn timestamp,
                    timestamptzColumn timestamp with time zone,
                    intervalDsColumn interval day to second,
                    intervalYmColumn interval year to month
                )
            ")
            .Assert(dbModel => dbModel.Tables
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
                })
            )
        );

        [ConditionalFact]
        public void Default_values_are_stored() => Test(test => test
            .Arrange(@"
                CREATE TABLE DefaultValues (
                    FixedDefaultValue timestamp NOT NULL DEFAULT timestamp '1999-01-08'
                )
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultvalues",
                    Columns = Items(
                        new { Name = "fixeddefaultvalue", DefaultValueSql = "timestamp '1999-01-08'" }
                    )
                })
            )
        );

        [ConditionalTheory]
        [InlineData("int", "WITH NULL", "NULL")]
        [InlineData("int", "NOT NULL", "0")]
        [InlineData("bigint", "NOT NULL", "0")]
        [InlineData("boolean", "NOT NULL", "false")]
        [InlineData("decimal", "NOT NULL", "0")]
        [InlineData("float", "NOT NULL", "0")]
        [InlineData("money", "NOT NULL", "0")]
        [InlineData("numeric", "NOT NULL", "0")]
        [InlineData("real", "NOT NULL", "0")]
        [InlineData("smallint", "NOT NULL", "0")]
        [InlineData("tinyint", "NOT NULL", "0")]
        [InlineData("decimal", "NOT NULL", "0.0")]
        [InlineData("float", "NOT NULL", "0.0")]
        [InlineData("money", "NOT NULL", "0.0")]
        [InlineData("numeric", "NOT NULL", "0.0")]
        [InlineData("real", "NOT NULL", "0.0")]
        [InlineData("float", "NOT NULL", "0.0E0")]
        [InlineData("ansidate", "NOT NULL", "date '0001-01-01'")]
        [InlineData("ingresdate", "NOT NULL", "'0001-01-01 00:00:00.000'")]
        [InlineData("timestamp", "NOT NULL", "timestamp '0001-01-01 00:00:00.000'")]
        [InlineData("time", "NOT NULL", "time '00:00:00'")]
        //[InlineData("uuid", "NOT NULL", "'00000000-0000-0000-0000-000000000000'")]
        public void Default_value_matching_clr_default_is_not_stored(string storeType, string nullClause, string defaultValue) => Test(test => test
            .Arrange(@$"
                CREATE TABLE DefaultValues (
                    IgnoredDefault  {storeType}  {nullClause}  DEFAULT {defaultValue}
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "defaultvalues",
                    Columns = Items(
                        new { Name = "ignoreddefault", DefaultValueSql = null as string }
                    )
                })
            )
        );

        [MinimumServerVersion(11, 0)]
        [ConditionalFact]
        public void ValueGenerated_is_set_for_identity_and_computed_column() => Test(test => test
            .Arrange(@"
                CREATE TABLE ValueGeneratedProperties (
                    Id  int  NOT NULL GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "valuegeneratedproperties",
                    Columns = Items(
                        new { Name = "id", ValueGenerated = ValueGenerated.OnAdd }
                    )
                })
            )
        );

        [ConditionalFact]
        public void Column_nullability_is_set() => Test(test => test
            .Arrange(@"
                CREATE TABLE NullableColumns (
                    NullableInt int NULL,
                    NonNullString nvarchar(100) NOT NULL
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "nullablecolumns",
                    Columns = Items(
                        new { Name = "nullableint", IsNullable = true },
                        new { Name = "nonnullstring", IsNullable = false }
                    )
                })
            )
        );
    }
}
