using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;

namespace Actian.EFCore.Build.Commands
{
    public class CreateTestDatabases : BuildCommand
    {
        public CreateTestDatabases(BuildContext context)
            : base(context, "create-test-databases")
        {
        }

        public override async Task RunInternal()
        {
            using var console = new LogConsole($"Creating test databases", buffer: false);
            foreach (var db in Context.TestDatabases.Where(d => d.Create))
            {
                await CreateTestDatabase(db, console);
            }
        }

        public static async Task CreateTestDatabase(TestDatabase db, LogConsole console)
        {
            console.WriteCaption($"Create database {db.Name}");

            using var cli = new CliSession(console);
            await cli.ExecuteAsync("createdb", $@"-i -u\""{db.DbmsUser}\"" {db.Name}");

            using var session = new IngresSession(Config.GetConnectionString("iidbdb"), console);

            await session.ExecuteAsync($@"
                grant access, db_admin on database {db.Name} to public
            ", ignoreErrors: true);
        }
    }
}
