using System.Linq;
using System.Threading.Tasks;

namespace Actian.EFCore.Build.Commands
{
    public class DropTestDatabases : BuildCommand
    {
        public DropTestDatabases(BuildContext context)
            : base(context, "drop-test-databases")
        {
        }

        public override async Task RunInternal()
        {
            using var console = new LogConsole($"Dropping test databases", buffer: false);

            await Context.GetExistingDatabases(console);

            foreach (var db in Context.TestDatabases.Where(d => d.Drop || d.Create))
            {
                await DropDatabase(db, Context, console);
            }
        }

        public static async Task DropDatabase(TestUtilities.TestDatabase db, BuildContext context, LogConsole console)
        {
            console.WriteCaption($"Drop database {db.Name}");

            var (name, owner) = context.GetExistingDatabase(db.Name);
            if (name != null)
            {
                using var cli = new CliSession(console);
                await cli.ExecuteAsync("destroydb", $@"-u\""{owner}\"" {name}");
            }
            else
            {
                console.WriteLine("Database does not exist");
            }
        }
    }
}
