using System;
using System.Linq;
using System.Threading.Tasks;

namespace Actian.EFCore.Build.Commands
{
    public class CreateMissingTestDatabases : BuildCommand
    {
        public CreateMissingTestDatabases(BuildContext context)
            : base(context, "create-missing-test-databases")
        {
        }

        public override async Task RunInternal()
        {
            using var console = new LogConsole($"Creating missing test databases", buffer: false);

            await Context.EnsureTestDatabases(console);

            var northwindDb = Context.TestDatabases.FirstOrDefault(db => db.Aliases.Contains("Northwind"));

            if (northwindDb is null || !northwindDb.Create)
                return;

            await Context.RunCommand<PopulateNorthwind>();
        }
    }
}
