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
            await GetExistingDatabases();
            await CreateMissing();

            var northwindDb = Context.TestDatabases.FirstOrDefault(db => db.Aliases.Contains("Northwind"));
            if (northwindDb != null && northwindDb.Create && !Context.DatabaseExists(northwindDb.Name))
            {
                await Context.RunCommand<PopulateNorthwind>();
            }
        }

        private async Task GetExistingDatabases()
        {
            using var console = new LogConsole($"Get existing databases", buffer: false);
            await Context.GetExistingDatabases(console);
        }

        private async Task CreateMissing()
        {
            using var console = new LogConsole($"Creating missing test databases", buffer: false);

            await Context.GetExistingDatabases(console);

            foreach (var db in Context.TestDatabases.Where(d => d.Create))
            {
                if (!Context.DatabaseExists(db.Name))
                {
                    await CreateTestDatabases.CreateTestDatabase(db, console);
                }
            }
        }
    }
}
