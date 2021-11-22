using System.Threading.Tasks;

namespace Actian.EFCore.Build.Commands
{
    public class SetupTestDatabases : BuildCommand
    {
        public SetupTestDatabases(BuildContext context)
            : base(context, "setup-test-databases")
        {
        }

        public override async Task RunInternal()
        {
            await Context.RunCommand<DropTestDatabases>();
            await Context.RunCommand<CreateDatabaseUsers>();
            await Context.RunCommand<CreateTestDatabases>();
            await Context.RunCommand<PopulateNorthwind>();
        }
    }
}
