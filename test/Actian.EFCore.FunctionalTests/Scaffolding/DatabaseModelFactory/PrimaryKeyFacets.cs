using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class PrimaryKeyFacets : ActianDatabaseModelFactoryTestBase
        {
            public PrimaryKeyFacets(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            public void Create_composite_primary_key() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""CompositePrimaryKeyTable"" (
                        ""Id1"" int NOT NULL,
                        ""Id2"" int NOT NULL,
                        PRIMARY KEY (""Id2"", ""Id1"")
                    );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "CompositePrimaryKeyTable",
                    PrimaryKey = new
                    {
                        Name = "$compo_",
                        Columns = Items(
                            new { Name = "Id2" },
                            new { Name = "Id1" }
                        )
                    }
                }, options => options
                    .WithStrictOrdering()
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingString("PrimaryKey.Name", ctx => ctx.Subject.ToLower().Should().StartWith(ctx.Expectation))
                    .UsingDelimitedName(dbModel, "PrimaryKey.Columns[].Name")
               )
            )
        );

            public void Set_primary_key_name_from_index() => Test(test => test
            .Arrange(@"
                    CREATE TABLE ""PrimaryKeyName"" (
                        ""Id1"" int NOT NULL,
                        ""Id2"" int NOT NULL,
                        CONSTRAINT ""MyPK"" PRIMARY KEY ( ""Id2"" )
                    );
                ")
            .Assert(dbModel => dbModel.Tables
                .SingleOrDefault()
                .Should().BeOfType<DatabaseTable>().And.BeEquivalentTo(new
                {
                    Schema = "dbo",
                    Name = "PrimaryKeyName",
                    PrimaryKey = new
                    {
                        Name = "MyPK",
                        Columns = Items(
                            new { Name = "Id2" }
                        )
                    }
                }, options => options
                    .WithStrictOrdering()
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                    .UsingDelimitedName(dbModel, "PrimaryKey.Name")
                    .UsingDelimitedName(dbModel, "PrimaryKey.Columns[].Name")
                )
            )
        );
        }
    }
}
