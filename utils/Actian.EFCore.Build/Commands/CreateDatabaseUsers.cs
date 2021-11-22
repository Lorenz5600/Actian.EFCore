using System.Threading.Tasks;

namespace Actian.EFCore.Build.Commands
{
    public class CreateDatabaseUsers : BuildCommand
    {
        public CreateDatabaseUsers(BuildContext context)
            : base(context, "create-db-users")
        {
        }

        public override async Task RunInternal()
        {
            var userIds = new[] { "dbo", "db2", "db.2" };

            using var console = new LogConsole($"Creating database users");
            using var session = new IngresSession(Config.GetConnectionString("iidbdb"), console);

            var existingUsers = await session.SelectAsync($@"
                select name from iiuser
            ", reader => reader.GetString(0));

            foreach (var userId in userIds)
            {
                console.WriteCaption($"Create user {userId}");

                if (existingUsers.Contains(userId))
                {
                    await session.ExecuteAsync($@"
                        drop user ""{userId}""
                    ", ignoreErrors: true);
                }

                await session.ExecuteAsync($@"
                    create user ""{userId}"" 
                      with privileges = (createdb, trace, security, operator, maintain_users),
                           nopassword
                ", ignoreErrors: true);

                await session.ExecuteAsync($@"
                    alter user ""{userId}"" 
                     with privileges = (createdb, trace, security, operator, maintain_users),
                          nopassword
                ");
            }
        }
    }
}
