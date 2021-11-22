using System;
using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.Parsing;

namespace Actian.EFCore.Build.Commands
{
    public class PopulateNorthwind : BuildCommand
    {
        public PopulateNorthwind(BuildContext context)
            : base(context, "populate-northwind")
        {
        }

        public override async Task RunInternal()
        {
            var db = Context.TestDatabases.FirstOrDefault(db => db.Aliases.Contains("Northwind"));

            if (db is null)
                throw new Exception("Northwind test database not found");

            using var console = new LogConsole($"Populating databases {db.Name}", buffer: false);
            using var session = new IngresSession(Config.GetConnectionString(db.Name, db.DbmsUser), console);

            await ActianSqlExecutor.ExecuteFileAsync(Config.NorthwindSqlPath, (sql, ignoreErrors) => session.ExecuteAsync(sql, ignoreErrors));
        }
    }
}
