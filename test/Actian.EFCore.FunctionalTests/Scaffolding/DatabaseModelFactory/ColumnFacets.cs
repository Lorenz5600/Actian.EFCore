using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class ColumnFacets : ActianDatabaseModelFactoryTestBase
        {
            public ColumnFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            // Note: in Actian numeric is simply an alias for decimal
            public void Decimal_types_have_precision_scale() => Test(test => test
                .Arrange(@"
                    CREATE TABLE ""NumericColumns"" (
                        ""decimalColumn""                 decimal        NOT NULL,
                        ""decimal105Column""              decimal(10, 5) NOT NULL,
                        ""decimalDefaultColumn""          decimal(18, 2) NOT NULL,
                        ""numericColumn""                 numeric        NOT NULL,
                        ""numeric152Column""              numeric(15, 2) NOT NULL,
                        ""numericDefaultColumn""          numeric(18, 2) NOT NULL,
                        ""numericDefaultPrecisionColumn"" numeric(38, 5) NOT NULL
                    );
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "NumericColumns",
                        Columns = Items(
                            new { Name = "decimalColumn", StoreType = "decimal" },
                            new { Name = "decimal105Column", StoreType = "decimal(10,5)" },
                            new { Name = "decimalDefaultColumn", StoreType = "decimal(18,2)" },
                            new { Name = "numericColumn", StoreType = "decimal" },
                            new { Name = "numeric152Column", StoreType = "decimal(15,2)" },
                            new { Name = "numericDefaultColumn", StoreType = "decimal(18,2)" },
                            new { Name = "numericDefaultPrecisionColumn", StoreType = "decimal(38,5)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Specific_max_length_are_added_to_store_type() => Test(test => test
                .Arrange(@"
                    CREATE TABLE ""LengthColumns"" (
                        ""char10Column""                       char(10)                   NULL,
                        ""varchar66Column""                    varchar(66)                NULL,
                        ""nchar99Column""                      nchar(99)                  NULL,
                        ""nvarchar100Column""                  nvarchar(100)              NULL,
                        ""binary111Column""                    binary(111)                NULL,
                        ""varbinary123Column""                 varbinary(123)             NULL,
                        ""binaryVarying133Column""             binary varying(133)        NULL
                    );
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "LengthColumns",
                        Columns = Items(
                            new { Name = "char10Column", StoreType = "char(10)" },
                            new { Name = "varchar66Column", StoreType = "varchar(66)" },
                            new { Name = "nchar99Column", StoreType = "nchar(99)" },
                            new { Name = "nvarchar100Column", StoreType = "nvarchar(100)" },
                            new { Name = "binary111Column", StoreType = "byte(111)" },
                            new { Name = "varbinary123Column", StoreType = "byte varying(123)" },
                            new { Name = "binaryVarying133Column", StoreType = "byte varying(133)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Default_max_length_are_added_to_binary_varbinary() => Test(test => test
                .Arrange(@"
                    CREATE TABLE ""DefaultRequiredLengthBinaryColumns"" (
                        ""binaryColumn"" binary(8000),
                        ""binaryVaryingColumn"" binary varying(8000),
                        ""varbinaryColumn"" varbinary(8000)
                    );
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultRequiredLengthBinaryColumns",
                        Columns = Items(
                            new { Name = "binaryColumn", StoreType = "byte(8000)" },
                            new { Name = "binaryVaryingColumn", StoreType = "byte varying(8000)" },
                            new { Name = "varbinaryColumn", StoreType = "byte varying(8000)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Default_max_length_are_added_to_char() => Test(test => test
                .Arrange(@"
                        CREATE TABLE ""DefaultRequiredLengthCharColumns"" (
                            ""charColumn"" char(8000)
                        );
                    ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultRequiredLengthCharColumns",
                        Columns = Items(
                            new { Name = "charColumn", StoreType = "char(8000)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Default_max_length_are_added_to_varchar() => Test(test => test
                .Arrange(@"
                        CREATE TABLE ""DefaultRequiredLengthVarcharColumns"" (
                            ""varcharColumn"" varchar(8000)
                        );
                    ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultRequiredLengthVarcharColumns",
                        Columns = Items(
                            new { Name = "varcharColumn", StoreType = "varchar(8000)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Default_max_length_are_added_to_nchar() => Test(test => test
                .Arrange(@"
                        CREATE TABLE ""DefaultRequiredLengthNcharColumns"" (
                            ""ncharColumn"" nchar(8000)
                        );
                    ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultRequiredLengthNcharColumns",
                        Columns = Items(
                            new { Name = "ncharColumn", StoreType = "nchar(8000)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Default_max_length_are_added_to_nvarchar() => Test(test => test
                .Arrange(@"
                        CREATE TABLE ""DefaultRequiredLengthNvarcharColumns"" (
                            ""nvarcharColumn"" nvarchar(4000)
                        );
                    ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultRequiredLengthNvarcharColumns",
                        Columns = Items(
                            new { Name = "nvarcharColumn", StoreType = "nvarchar(4000)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Datetime_types_have_precision_if_non_null_scale() => Test(test => test
               .Arrange(@"
                        CREATE TABLE ""DatetimeColumns"" (
                            ""time1Column""        time(1)                     NULL,
                            ""timetz2Column""      time(2) with time zone      NULL,
                            ""timestamp3Column""   timestamp(3)                NULL,
                            ""timestamptz4Column"" timestamp(4) with time zone NULL,
                            ""interval5Column""    interval day to second(5)   NULL
                        )
                    ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DatetimeColumns",
                        Columns = Items(
                            new { Name = "time1Column", StoreType = "time(1) without time zone" },
                            new { Name = "timetz2Column", StoreType = "time(2) with time zone" },
                            new { Name = "timestamp3Column", StoreType = "timestamp(3) without time zone" },
                            new { Name = "timestamptz4Column", StoreType = "timestamp(4) with time zone" },
                            new { Name = "interval5Column", StoreType = "interval day to second(5)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                 )
            );

            public void Types_with_required_length_uses_length_of_one() => Test(test => test
               .Arrange(@"
                        CREATE TABLE ""OneLengthColumns"" (
                            ""binaryColumn""                   binary                NULL,
                            ""binaryVaryingColumn""            binary varying        NULL,
                            ""charColumn""                     char                  NULL,
                            ""ncharColumn""                    nchar                 NULL,
                            ""nvarcharColumn""                 nvarchar              NULL,
                            ""varbinaryColumn""                varbinary             NULL,
                            ""varcharColumn""                  varchar               NULL
                        );
                    ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "OneLengthColumns",
                        Columns = Items(
                            new { Name = "binaryColumn", StoreType = "byte(1)" },
                            new { Name = "binaryVaryingColumn", StoreType = "byte varying(1)" },
                            new { Name = "charColumn", StoreType = "char(1)" },
                            new { Name = "ncharColumn", StoreType = "nchar(1)" },
                            new { Name = "nvarcharColumn", StoreType = "nvarchar(1)" },
                            new { Name = "varbinaryColumn", StoreType = "byte varying(1)" },
                            new { Name = "varcharColumn", StoreType = "varchar(1)" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                 )
            );

            public void Store_types_without_any_facets() => Test(test => test
                .Arrange(@"
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
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "NoFacetTypes",
                        Columns = Items(
                            new { Name = "booleanColumn", StoreType = "boolean" },
                            new { Name = "floatColumn", StoreType = "float4" },
                            new { Name = "doubleColumn", StoreType = "float" },
                            new { Name = "decimalColumn", StoreType = "decimal" },
                            new { Name = "moneyColumn", StoreType = "money" },
                            //new { Name = "guidColumn", StoreType = "byte(16)" }, // TODO: Fix in ADO.NET provider?
                            new { Name = "byteColumn", StoreType = "tinyint" },
                            new { Name = "shortColumn", StoreType = "smallint" },
                            new { Name = "intColumn", StoreType = "integer" },
                            new { Name = "longColumn", StoreType = "bigint" },
                            new { Name = "timeColumn", StoreType = "time without time zone" },
                            new { Name = "timetzColumn", StoreType = "time with time zone" },
                            new { Name = "timestampColumn", StoreType = "timestamp without time zone" },
                            new { Name = "timestamptzColumn", StoreType = "timestamp with time zone" },
                            new { Name = "intervalDsColumn", StoreType = "interval day to second" },
                            new { Name = "intervalYmColumn", StoreType = "interval year to month" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Default_values_are_stored() => Test(test => test
                .Arrange(@"
                    CREATE TABLE ""DefaultValues"" (
                        ""FixedDefaultValue"" timestamp NOT NULL DEFAULT timestamp '1999-01-08'
                    )
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultValues",
                        Columns = Items(
                            new { Name = "FixedDefaultValue", DefaultValueSql = "timestamp '1999-01-08'" }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
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
                .Arrange($@"
                    CREATE TABLE ""DefaultValues"" (
                        ""IgnoredDefault""  {storeType}  {nullClause}  DEFAULT {defaultValue}
                    );
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "DefaultValues",
                        Columns = Items(
                            new { Name = "IgnoredDefault", DefaultValueSql = null as string }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void ValueGenerated_is_set_for_identity_and_computed_column() => Test(test => test
                .Arrange(@"
                    CREATE TABLE ""ValueGeneratedProperties"" (
                        ""Id""  int  NOT NULL GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1)
                    );
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "ValueGeneratedProperties",
                        Columns = Items(
                            new { Name = "Id", ValueGenerated = ValueGenerated.OnAdd }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );

            public void Column_nullability_is_set() => Test(test => test
                .Arrange(@"
                    CREATE TABLE ""NullableColumns"" (
                        ""NullableInt"" int NULL,
                        ""NonNullString"" nvarchar(100) NOT NULL
                    );
                ")
                .Assert(dbModel => dbModel.Tables
                    .SingleOrDefault()
                    .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                    {
                        Schema = "dbo",
                        Name = "NullableColumns",
                        Columns = Items(
                            new { Name = "NullableInt", IsNullable = true },
                            new { Name = "NonNullString", IsNullable = false }
                        )
                    }, options => options
                        .UsingDelimitedName(dbModel, "Name")
                        .UsingDelimitedName(dbModel, "Columns[].Name")
                    )
                )
            );
        }
    }
}
