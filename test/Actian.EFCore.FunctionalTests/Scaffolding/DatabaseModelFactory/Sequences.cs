using System.Linq;
using Actian.EFCore.TestUtilities.TestAttributes;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    [ServerSupportsSequences]
    public class Sequences : ActianDatabaseModelFactoryTestBase
    {
        public Sequences(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Create_sequences_with_facets() => Test(test => test
            .Arrange(@$"
                CREATE SEQUENCE DefaultFacetsSequence;

                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE efcore_test2.CustomFacetsSequence
                    AS integer
                    START WITH 1
                    INCREMENT BY 2
                    MINVALUE 1
                    MAXVALUE 90
                    CYCLE;
            ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new {
                    Schema = "efcore_test1",
                    Name = "defaultfacetssequence",
                    StoreType = "bigint",
                    IsCyclic = false,
                    IncrementBy = 1,
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                },
                new {
                    Schema = "efcore_test2",
                    Name = "customfacetssequence",
                    StoreType = "integer",
                    IsCyclic = true,
                    IncrementBy = 2,
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = 90L as long?
                }
            )))
        );

        [ConditionalFact]
        public void Sequence_min_max_start_values_are_null_if_default() => Test(test => test
            .Arrange(@$"
                CREATE SEQUENCE IntSequence AS int;
                CREATE SEQUENCE BigIntSequence AS bigint;
            ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new {
                    Schema = "efcore_test1",
                    Name = "intsequence",
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                },
                new {
                    Schema = "efcore_test1",
                    Name = "bigintsequence",
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                }
            )))
        );

        [ConditionalFact]
        public void Sequence_min_max_start_values_are_not_null_if_decimal() => Test(test => test
            .Arrange(@$"
                CREATE SEQUENCE DecimalSequence AS decimal;
                CREATE SEQUENCE NumericSequence AS numeric;
            ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new {
                    Schema = "efcore_test1",
                    Name = "decimalsequence",
                    StartValue = 1L,
                    MinValue = 1L,
                    MaxValue = 99999L
                },
                new {
                    Schema = "efcore_test1",
                    Name = "numericsequence",
                    StartValue = 1L,
                    MinValue = 1L,
                    MaxValue = 99999L
                }
            )))
        );

        [ConditionalFact]
        public void Sequence_using_type_with_facets() => Test(test => test
            .Arrange(@$"
                CREATE SEQUENCE TypeFacetSequence AS decimal(10, 0);
            ")
            .Assert(dbModel => dbModel.Sequences.Should().BeEquivalentTo(Items(
                new {
                    Schema = "efcore_test1",
                    Name = "typefacetsequence",
                    StoreType = "decimal(10)",
                    IsCyclic = false,
                    IncrementBy = 1
                }
            )))
        );

        [ConditionalFact]
        public void Create_sequences_with_default_facets() => Test(test => test
            .Arrange(@$"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE ""DefaultFacetsSequence"";
            ")
            .Assert(dbModel => dbModel.Sequences
                .SingleOrDefault(ds => ds.Name == "DefaultFacetsSequence".ToLowerInvariant())
                .Should().BeEquivalentTo(new
                {
                    Schema = "efcore_test2",
                    Name = "defaultfacetssequence",
                    StoreType = "bigint",
                    IsCyclic = false,
                    IncrementBy = 1,
                    StartValue = null as long?,
                    MinValue = null as long?,
                    MaxValue = null as long?
                })
            )
        );

        [ConditionalFact]
        public void Create_sequences_with_custom_facets() => Test(test => test
            .Arrange(@$"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE efcore_test2.""CustomFacetsSequence"" AS integer
                    start with 10
                    increment by 2
                    maxvalue 80
                    minvalue 3
                    cycle;
            ")
            .Assert(dbModel => dbModel.Sequences
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
                })
            )
        );

        [ConditionalFact]
        public void Filter_sequences_based_on_schema() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test1;
                CREATE SEQUENCE ""Sequence"";
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE SEQUENCE efcore_test2.""Sequence""
            ")
            .FilterSchemas("efcore_test2")
            .Assert(dbModel => dbModel.Sequences
                .SingleOrDefault()
                .Should().BeOfType<DatabaseSequence>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test2",
                    Name = "sequence",
                    StoreType = "bigint"
                })
            )
        );
    }
}
