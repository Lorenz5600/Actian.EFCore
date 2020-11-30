using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    public class Model : ActianDatabaseModelFactoryTestBase
    {
        public Model(ActianDatabaseModelFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [ConditionalFact]
        public void Get_default_schema() => Test(test => test
            .Assert(dbModel =>
            {
                var defaultSchema = Fixture.TestStore
                    .ExecuteScalar<string>("select dba_name from $ingres.iidbconstants")
                    .Trim();
                Assert.Equal(defaultSchema, dbModel.DefaultSchema);
            })
        );

        [ConditionalFact]
        public void Create_tables() => Test(test => test
            .Arrange(@"
                SET SESSION AUTHORIZATION efcore_test2;
                CREATE TABLE ""Everest"" ( id int );
                CREATE TABLE ""Denali"" ( id int );
            ")
            .Assert(dbModel => dbModel.Tables
                .OrderBy(t => t.Name)
                .Should().BeEquivalentTo(Items(
                    new { Schema = "efcore_test2", Name = "denali" },
                    new { Schema = "efcore_test2", Name = "everest" }
                ))
            )
        );
    }
}
