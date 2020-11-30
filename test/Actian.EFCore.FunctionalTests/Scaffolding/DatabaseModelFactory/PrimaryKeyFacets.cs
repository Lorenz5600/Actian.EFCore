using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class PrimaryKeyFacets : ActianDatabaseModelFactoryTestBase
    {
        public PrimaryKeyFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Create_composite_primary_key() => Test(test => test
            .Arrange(@"
                CREATE TABLE CompositePrimaryKeyTable (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL,
                    PRIMARY KEY (Id2, Id1)
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "compositeprimarykeytable",
                    PrimaryKey = new
                    {
                        Name = "$compo_",
                        Columns = Items(
                            new { Name = "id2" },
                            new { Name = "id1" }
                        )
                    }
                }, options => options
                    .WithStrictOrdering()
                    .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation))
                    .When(info => info.SelectedMemberPath == "PrimaryKey.Name")
                )
            )
        );

        [ConditionalFact]
        public void Set_primary_key_name_from_index() => Test(test => test
            .Arrange(@"
                CREATE TABLE PrimaryKeyName (
                    Id1 int NOT NULL,
                    Id2 int NOT NULL,
                    CONSTRAINT MyPK PRIMARY KEY ( Id2 )
                );
            ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "efcore_test1",
                    Name = "primarykeyname",
                    PrimaryKey = new
                    {
                        Name = "mypk",
                        Columns = Items(
                            new { Name = "id2" }
                        )
                    }
                }, options => options
                    .WithStrictOrdering()
                )
            )
        );
    }
}
