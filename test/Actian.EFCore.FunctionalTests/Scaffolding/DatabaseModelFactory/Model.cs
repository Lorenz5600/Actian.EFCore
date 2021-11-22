using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public partial class ActianDatabaseModelFactoryTest
    {
        public class Model : ActianDatabaseModelFactoryTestBase
        {
            public Model(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
                : base(fixture, output)
            {
            }

            public void Get_default_schema() => Test(test => test
            .Assert(dbModel =>
            {
                var defaultSchema = Fixture.TestStore
                    .ExecuteScalar<string>("select dba_name from $ingres.iidbconstants")
                    .Trim();
                Assert.Equal(defaultSchema, dbModel.DefaultSchema);
            })
        );

            public void Create_tables() => Test(test => test
            .Arrange(@"
                    SET SESSION AUTHORIZATION ""db2"";
                    CREATE TABLE ""Everest"" ( ""id"" int );
                    CREATE TABLE ""Denali"" ( ""id"" int );
                ")
            .Assert(dbModel => dbModel.Tables
                .OrderBy(t => t.Name)
                .Should().BeEquivalentTo(Items(
                    new { Schema = "db2", Name = "Denali" },
                    new { Schema = "db2", Name = "Everest" }
                ), options => options
                    .UsingDelimitedName(dbModel, "Schema")
                    .UsingDelimitedName(dbModel, "Name")
                )
            )
        );
        }
    }
}
