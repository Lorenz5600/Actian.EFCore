using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Actian.EFCore.Diagnostics.Internal;
using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Scaffolding.Internal;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Scaffolding.DatabaseModelFactory
{
    [Collection("ActianDatabaseModelFactoryTests")]
    public abstract class ActianDatabaseModelFactoryTestBase : IClassFixture<ActianDatabaseModelFixture>, IDisposable
    {
        public ActianDatabaseModelFactoryTestBase(ActianDatabaseModelFixture fixture, ITestOutputHelper testOutputHelper)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            Fixture.ListLoggerFactory.Clear();
            Output = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            Fixture.SetOutput(Output);
        }

        public virtual void Dispose()
        {
            Fixture.SetOutput(null);
        }

        protected ActianDatabaseModelFixture Fixture { get; }
        protected ITestOutputHelper Output { get; }

        protected class Tester
        {
            public string ArrangeSql { get; private set; } = "";
            public Action<DatabaseModel> Asserter { get; private set; }
            public IEnumerable<string> Tables { get; private set; }
            public IEnumerable<string> Schemas { get; private set; }

            public Tester Arrange(string arrangeSql)
            {
                ArrangeSql = arrangeSql ?? "";
                return this;
            }

            public Tester FilterTables(params string[] tables)
            {
                Tables = tables?.ToList();
                return this;
            }

            public Tester FilterSchemas(params string[] schemas)
            {
                Schemas = schemas?.ToList();
                return this;
            }

            public Tester Assert(Action<DatabaseModel> asserter)
            {
                Asserter = asserter;
                return this;
            }
        }

        protected void Test(Func<Tester, Tester> arrange)
        {
            var test = arrange(new Tester());

            Fixture.TestStore.CleanObjects();

            Fixture.TestStore.ExecuteStatements(
                @"SET SESSION AUTHORIZATION ""dbo"";",
                test.ArrangeSql
            );

            try
            {
                var databaseModelFactory = new ActianDatabaseModelFactory(
                    new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                        Fixture.ListLoggerFactory,
                        new LoggingOptions(),
                        new DiagnosticListener("Fake"),
                        new ActianLoggingDefinitions()));

                var databaseModel = databaseModelFactory.Create(
                    Fixture.TestStore.ConnectionString,
                    new DatabaseModelFactoryOptions(test.Tables, test.Schemas)
                );
                Assert.NotNull(databaseModel);
                test.Asserter(Log(databaseModel));
            }
            finally
            {
                Fixture.TestStore.ExecuteStatementsIgnoreErrors(
                    @"SET SESSION AUTHORIZATION ""dbo"";"
                );
            }
        }

        protected DatabaseModel Log(DatabaseModel dbModel)
        {
            Output.WriteLine($"Server version: {Fixture.TestStore.GetServerVersion()}");
            Output.WriteLine($"DatabaseName: {dbModel.DatabaseName}");
            Output.WriteLine($"DefaultSchema: {dbModel.DefaultSchema}");
            Output.WriteLine($"Tables:");
            foreach (var table in dbModel.Tables)
            {
                Output.WriteLine($"  {table.Schema}.{table.Name}");
            }
            Output.WriteLine($"Sequences:");
            foreach (var sequence in dbModel.Sequences)
            {
                Output.WriteLine($"  {sequence.Schema}.{sequence.Name}");
            }
            return dbModel;
        }

        protected IEnumerable<T> Items<T>(params T[] items)
        {
            return items;
        }

        protected string NormalizeName(DatabaseModel dbModel, string name)
        {
            return dbModel.GetAnnotation<ActianCasing>(ActianAnnotationNames.DbDelimitedCase).Normalize(name);
        }
    }
}
