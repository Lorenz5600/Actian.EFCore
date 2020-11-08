using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

// TODO: ActianHistoryRepository
namespace Actian.EFCore.Migrations.Internal
{
    public class ActianHistoryRepository : HistoryRepository
    {
        public ActianHistoryRepository([NotNull] HistoryRepositoryDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override string ExistsSql => $@"
            select distinc 1
              from $ingres.iitables
             where table_owner = '{TableSchema}'
               and table_name = '{TableName}'
        ";

        protected override bool InterpretExistsResult([NotNull] object value)
        {
            return value is int intValue && intValue == 1;
        }

        public override string GetCreateIfNotExistsScript()
        {
            return GetCreateScript().Replace("CREATE TABLE", "CREATE TABLE IF NOT EXISTS");
        }

        public override string GetBeginIfExistsScript(string migrationId)
        {
            throw new NotImplementedException();
        }

        public override string GetBeginIfNotExistsScript(string migrationId)
        {
            throw new NotImplementedException();
        }

        public override string GetEndIfScript()
        {
            throw new NotImplementedException();
        }
    }
}
