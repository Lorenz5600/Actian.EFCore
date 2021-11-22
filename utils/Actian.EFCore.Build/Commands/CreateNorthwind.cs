using System;
using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;

namespace Actian.EFCore.Build.Commands
{
    public class CreateNorthwind : BuildCommand
    {
        public CreateNorthwind(BuildContext context)
            : base(context, "create-northwind")
        {
        }

        public override async Task RunInternal()
        {
            var northwindDb = Context.TestDatabases.FirstOrDefault(db => db.Aliases.Contains("Northwind"));

            if (northwindDb is null)
                throw new Exception("Northwind test database not found");

            await Recreate(northwindDb);
            await Context.RunCommand<PopulateNorthwind>();
        }

        private async Task Recreate(TestDatabase northwindDb)
        {
            using var console = new LogConsole($"Create northwind database ({northwindDb.Name})", buffer: false);

            await Context.GetExistingDatabases(console);
            await DropTestDatabases.DropDatabase(northwindDb, Context, console);
            await CreateTestDatabases.CreateTestDatabase(northwindDb, console);
        }
    }
}
