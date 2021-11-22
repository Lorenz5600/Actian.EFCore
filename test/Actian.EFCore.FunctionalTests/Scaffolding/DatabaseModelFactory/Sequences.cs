using System.Linq;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        [ServerSupportsSequences]
        public class Sequences : ActianDatabaseModelFactoryTestBase
        {
            public Sequences(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            public void Create_sequences_with_facets() => Test(test => test
            .Arrange(@$"
                    CREATE SEQUENCE ""DefaultFacetsSequence"";

                    SET SESSION AUTHORIZATION ""db2"";
                    CREATE SEQUENCE ""db2"".""CustomFacetsSequence""
                        AS integer
                        START WITH 1
                        INCREMENT BY 2
                        MINVALUE 1
                        MAXVALUE 90
                        CYCLE;
                ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new
                {
                    Schema = "dbo",
                    Name = "DefaultFacetsSequence",
                    StoreType = "bigint",
                    IsCyclic = false,
                    IncrementBy = 1,
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                },
                new
                {
                    Schema = "db2",
                    Name = "CustomFacetsSequence",
                    StoreType = "integer",
                    IsCyclic = true,
                    IncrementBy = 2,
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = 90L as long?
                }
            ), options => options
                .UsingDelimitedName(dbModel, "Schema")
                .UsingDelimitedName(dbModel, "Name")
            ))
        );

            public void Sequence_min_max_start_values_are_null_if_default() => Test(test => test
            .Arrange($@"
                    CREATE SEQUENCE ""IntSequence"" AS int;
                    CREATE SEQUENCE ""BigIntSequence"" AS bigint;
                ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new
                {
                    Schema = "dbo",
                    Name = "IntSequence",
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                },
                new
                {
                    Schema = "dbo",
                    Name = "BigIntSequence",
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                }
            ), options => options
                .UsingDelimitedName(dbModel, "Schema")
                .UsingDelimitedName(dbModel, "Name")
            ))
        );

            public void Sequence_min_max_start_values_are_not_null_if_decimal() => Test(test => test
            .Arrange($@"
                    CREATE SEQUENCE ""DecimalSequence"" AS decimal;
                    CREATE SEQUENCE ""NumericSequence"" AS numeric;
                ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new
                {
                    Schema = "dbo",
                    Name = "DecimalSequence",
                    StartValue = 1L,
                    MinValue = 1L,
                    MaxValue = 99999L
                },
                new
                {
                    Schema = "dbo",
                    Name = "NumericSequence",
                    StartValue = 1L,
                    MinValue = 1L,
                    MaxValue = 99999L
                }
            ), options => options
                .UsingDelimitedName(dbModel, "Schema")
                .UsingDelimitedName(dbModel, "Name")
            ))
        );

            public void Sequence_using_type_with_facets() => Test(test => test
            .Arrange($@"
                    CREATE SEQUENCE ""TypeFacetSequence"" AS decimal(10, 0);
                ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new
                {
                    Schema = "dbo",
                    Name = "TypeFacetSequence",
                    StoreType = "decimal(10)",
                    IsCyclic = false,
                    IncrementBy = 1
                }
            ), options => options
                .UsingDelimitedName(dbModel, "Schema")
                .UsingDelimitedName(dbModel, "Name")
            ))
        );

            public void Create_sequences_with_default_facets() => Test(test => test
            .Arrange($@"
                    SET SESSION AUTHORIZATION ""db2"";
                    CREATE SEQUENCE ""DefaultFacetsSequence"";
                ")
            .Assert(dbModel => dbModel.Sequences
                .SingleOrDefault(ds => ds.Name == dbModel.NormalizeDelimitedName("DefaultFacetsSequence"))
                .Should().BeEquivalentTo(new
                {
                    Schema = "db2",
                    Name = "DefaultFacetsSequence",
                    StoreType = "bigint",
                    IsCyclic = false,
                    IncrementBy = 1,
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                )
            )
        );

            public void Create_sequences_with_custom_facets() => Test(test => test
            .Arrange($@"
                    SET SESSION AUTHORIZATION ""db2"";
                    CREATE SEQUENCE ""db2"".""CustomFacetsSequence"" AS integer
                        start with 10
                        increment by 2
                        maxvalue 80
                        minvalue 3
                        cycle;
                ")
            .Assert(dbModel => dbModel.Sequences
                .SingleOrDefault(ds => ds.Name == dbModel.NormalizeDelimitedName("CustomFacetsSequence"))
                .Should().BeOfType<DatabaseSequence>().And.BeEquivalentTo(new
                {
                    Schema = "db2",
                    Name = "CustomFacetsSequence",
                    StoreType = "integer",
                    IsCyclic = true,
                    IncrementBy = 2,
                    StartValue = 10L,
                    MinValue = 3L,
                    MaxValue = 80L
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                )
            )
        );

            public void Filter_sequences_based_on_schema() => Test(test => test
            .Arrange(@"
                    SET SESSION AUTHORIZATION ""dbo"";
                    CREATE SEQUENCE ""Sequence"";
                    SET SESSION AUTHORIZATION ""db2"";
                    CREATE SEQUENCE ""db2"".""Sequence""
                ")
            .FilterSchemas(@"""db2""")
            .Assert(dbModel => dbModel.Sequences
                .SingleOrDefault()
                .Should().BeOfType<DatabaseSequence>().And.BeEquivalentTo(new
                {
                    Schema = "db2",
                    Name = "Sequence",
                    StoreType = "bigint"
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                )
            )
        );
        }
    }
}
