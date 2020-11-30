using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class UniqueConstraintFacets : ActianDatabaseModelFactoryTestBase
    {
        public UniqueConstraintFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Create_composite_unique_constraint() => Test(test => test
            .Arrange(@"
                CREATE TABLE CompositeUniqueConstraintTable (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL,
                    UNIQUE (Id2, Id1)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "compositeuniqueconstrainttable",
                    UniqueConstraints = Items(
                        new
                        {
                            Name = "$compo_",
                            Columns = Items(
                                new { Name = "id2" },
                                new { Name = "id1" }
                            )
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => info.SelectedMemberPath == "UniqueConstraints[0].Name")
                )
            )
        );

        [ConditionalFact]
        public void Set_unique_constraint_name_from_index() => Test(test => test
            .Arrange(@"
                CREATE TABLE UniqueConstraintName (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL,
                    CONSTRAINT MyUC UNIQUE ( Id2 )
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "uniqueconstraintname",
                    UniqueConstraints = Items(
                        new
                        {
                            Name = "myuc",
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
