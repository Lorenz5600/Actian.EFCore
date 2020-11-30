using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class IndexFacets : ActianDatabaseModelFactoryTestBase
    {
        public IndexFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Create_composite_index() => Test(test => test
            .Arrange(@"
                CREATE TABLE CompositeIndexTable (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL
                );

                CREATE INDEX IX_COMPOSITE ON CompositeIndexTable ( Id2, Id1 );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "compositeindextable",
                    Indexes = Items(
                        new
                        {
                            Name = "ix_composite",
                            IsUnique = false,
                            Columns = Items(
                                new { Name = "id2" },
                                new { Name = "id1" }
                            )
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                )
            )
        );

        [ConditionalFact]
        public void Set_unique_true_for_unique_index() => Test(test => test
            .Arrange(@"
                CREATE TABLE UniqueIndexTable (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL
                );

                CREATE UNIQUE INDEX IX_UNIQUE ON UniqueIndexTable ( Id2 );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "uniqueindextable",
                    Indexes = Items(
                        new
                        {
                            Name = "ix_unique",
                            IsUnique = true,
                            Columns = Items(
                                new { Name = "id2" }
                            )
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                )
            )
        );
    }
}
