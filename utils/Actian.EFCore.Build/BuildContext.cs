using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.Build.Commands;
using Actian.EFCore.TestUtilities;

namespace Actian.EFCore.Build
{
    public class BuildContext
    {
        private readonly List<BuildCommand> _commands = new List<BuildCommand>();

        public BuildContext()
        {
            RootCommand.Handler = CommandHandler.Create(HandleRootCommand);
            AddCommand(new CreateDatabaseUsers(this));
            AddCommand(new CreateMissingTestDatabases(this));
            AddCommand(new CreateNorthwind(this));
            AddCommand(new CreateTestDatabases(this));
            AddCommand(new DropTestDatabases(this));
            AddCommand(new PopulateNorthwind(this));
            AddCommand(new SetupTestDatabases(this));
        }

        public RootCommand RootCommand { get; } = new RootCommand();
        public IEnumerable<BuildCommand> Commands => _commands;

        public async Task<int> Run(string[] args)
        {
            using var console = new LogConsole("", buffer: false);
            var exitCode = await RootCommand.InvokeAsync(args);
            console.WriteLine($"Exit code: {exitCode}");
            return exitCode;
        }

        public async Task RunCommand<TCommand>() where TCommand : BuildCommand
        {
            if (!TryGetCommand<TCommand>(out var command))
                throw new Exception("");

            var result = await command.Run();
            if (result != 0)
                throw new Exception("");
        }

        public bool TryGetCommand<TCommand>(out TCommand command)
            where TCommand : BuildCommand
        {
            command = null;
            foreach (var cmd in Commands)
            {
                if (cmd is TCommand result)
                {
                    command = result;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<TestDatabase> TestDatabases { get; } = TestUtilities.TestDatabases
            .Load(Config.TestDatabasesPath).Databases
            .Where(db => db.Create && db.Name != "iidbdb")
            .ToList();

        private List<(string name, string owner)> _existingDatabases = null;
        public List<(string name, string owner)> ExistingDatabases => _existingDatabases ?? throw new Exception("Existing databases have not been retrieved");
        public async Task<List<(string name, string owner)>> GetExistingDatabases(LogConsole console)
        {
            if (_existingDatabases != null)
                return _existingDatabases;

            using var session = new IngresSession(Config.GetConnectionString("iidbdb"), console);

            return _existingDatabases = await session.SelectAsync<(string name, string owner)>($@"
                select name,own
                  from iidatabase
            ", reader => (reader.GetString(0), reader.GetString(1)));
        }

        public bool DatabaseExists(string name)
        {
            return ExistingDatabases
                .Any(edb => edb.name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public (string name, string owner) GetExistingDatabase(string name)
        {
            return ExistingDatabases
                .Where(edb => edb.name.ToLowerInvariant() == name.ToLowerInvariant())
                .DefaultIfEmpty((null, null))
                .First();
        }

        private void HandleRootCommand()
        {
            Console.WriteLine($"InstallationPath:     {Config.InstallationPath}");
            Console.WriteLine($"InstallationCode:     {Config.InstallationCode}");
            Console.WriteLine($"BaseConnectionString: {Config.BaseConnectionString}");
        }

        private void AddCommand(BuildCommand command)
        {
            _commands.Add(command);
            RootCommand.AddCommand(command);
        }
    }
}
