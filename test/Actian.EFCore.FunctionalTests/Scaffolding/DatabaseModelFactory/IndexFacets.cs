using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class IndexFacets : ActianDatabaseModelFactoryTestBase
        {
            public IndexFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            public void Create_composite_index() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""CompositeIndexTable"" (
                        ""id1"" int NOT NULL,
                        ""id2"" int NOT NULL
                    );

                    CREATE INDEX ""IX_COMPOSITE"" ON ""CompositeIndexTable"" ( ""id2"", ""id1"" );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "CompositeIndexTable",
                    Indexes = Items(
                        new
                        {
                            Name = "IX_COMPOSITE",
                            IsUnique = false,
                            Columns = Items(
                                new { Name = "id2" },
                                new { Name = "id1" }
                            )
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Columns[].Name")
                )
            )
        );

            public void Set_unique_true_for_unique_index() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""UniqueIndexTable"" (
                        ""id1"" int NOT NULL,
                        ""id2"" int NOT NULL
                    );

                    CREATE UNIQUE INDEX ""IX_UNIQUE"" ON ""UniqueIndexTable"" ( ""id2"" );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "UniqueIndexTable",
                    Indexes = Items(
                        new
                        {
                            Name = "IX_UNIQUE",
                            IsUnique = true,
                            Columns = Items(
                                new { Name = "id2" }
                            )
                        }
                    )
                }, options => options
                    .WithStrictOrdering()
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Name")
                    .UsingDelimitedName(dbModel, "Indexes[].Columns[].Name")
                )
            )
        );
        }
    }
}
