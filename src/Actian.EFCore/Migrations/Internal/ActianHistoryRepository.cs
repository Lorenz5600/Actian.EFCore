using System;
using System.Text;
using Actian.EFCore.Scaffolding.Internal;
using Actian.EFCore.Utilities;
using Ingres.Client;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Migrations.Internal
{
    public class ActianHistoryRepository : HistoryRepository
    {
        public ActianHistoryRepository([NotNull] HistoryRepositoryDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override string ExistsSql
        {
            get
            {
                if (!(Dependencies.Connection.DbConnection is IngresConnection ingresConnection))
                    throw new Exception("Connection must be an IngresConnection");

                var (_, dbDelimitedCase) = ingresConnection.GetDbCasing();

                return TableSchema is null
                    ? $@"
                        select distinct 1
                            from $ingres.iitables
                            where table_name = '{dbDelimitedCase.Normalize(TableName)}'
                    "
                    : $@"
                        select distinct 1
                            from $ingres.iitables
                            where table_owner = '{dbDelimitedCase.Normalize(TableSchema)}'
                            and table_name = '{dbDelimitedCase.Normalize(TableName)}'
                    ";
            }
        }

        protected override bool InterpretExistsResult([NotNull] object value)
        {
            return value is int intValue && intValue == 1
                || value is short shortValue && shortValue == 1;
        }

        public override string GetCreateIfNotExistsScript()
            => Exists() ? "" : GetCreateScript();

        public override string GetBeginIfExistsScript(string migrationId)
        {
            Check.NotEmpty(migrationId, nameof(migrationId));

            var stringTypeMapping = Dependencies.TypeMappingSource.GetMapping(typeof(string));

            return new StringBuilder()
                .Append("IF EXISTS (SELECT 1 FROM ")
                .Append(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
                .Append(" WHERE ")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(" = ")
                .Append(stringTypeMapping.GenerateSqlLiteral(migrationId))
                .AppendLine(")")
                .Append("BEGIN")
                .ToString();
        }

        public override string GetBeginIfNotExistsScript(string migrationId)
        {
            Check.NotEmpty(migrationId, nameof(migrationId));

            var stringTypeMapping = Dependencies.TypeMappingSource.GetMapping(typeof(string));

            return new StringBuilder()
                .Append("IF NOT EXISTS (SELECT 1 FROM ")
                .Append(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
                .Append(" WHERE ")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(" = ")
                .Append(stringTypeMapping.GenerateSqlLiteral(migrationId))
                .AppendLine(")")
                .Append("BEGIN")
                .ToString();
        }

        public override string GetEndIfScript() => new StringBuilder()
            .Append("END")
            .AppendLine(SqlGenerationHelper.StatementTerminator)
            .ToString();
    }
}
