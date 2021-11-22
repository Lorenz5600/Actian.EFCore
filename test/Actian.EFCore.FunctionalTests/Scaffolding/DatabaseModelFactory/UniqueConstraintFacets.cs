using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class UniqueConstraintFacets : ActianDatabaseModelFactoryTestBase
        {
            public UniqueConstraintFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            public void Create_composite_unique_constraint() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""CompositeUniqueConstraintTable"" (
                        ""Id1"" int NOT NULL,
                        ""Id2"" int NOT NULL,
                        UNIQUE (""Id2"", ""Id1"")
                    );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "CompositeUniqueConstraintTable",
                    UniqueConstraints = Items(
                        new
                        {
                            Name = "$compo_",
                            Columns = Items(
                                new { Name = "Id2" },
                                new { Name = "Id1" }
                            )
                        }
                    )
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingString("UniqueConstraints[].Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );

            public void Set_unique_constraint_name_from_index() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""UniqueConstraintName"" (
                        ""Id1"" int NOT NULL,
                        ""Id2"" int NOT NULL,
                        CONSTRAINT ""MyUC"" UNIQUE ( ""Id2"" )
                    );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "UniqueConstraintName",
                    UniqueConstraints = Items(
                        new
                        {
                            Name = "MyUC",
                            Columns = Items(
                                new { Name = "Id2" }
                            )
                        }
                    )
                }, options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Name")
                    .UsingDelimitedName(dbModel, "UniqueConstraints[].Columns[].Name")
                )
            )
        );
        }
    }
}
