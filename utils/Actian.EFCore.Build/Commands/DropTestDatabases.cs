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
            await Context.DropDatabases(console);
        }
    }
}
