using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;

// TODO: ActianUpdateSqlGenerator
namespace Actian.EFCore.Update.Internal
{
    public class ActianUpdateSqlGenerator : UpdateSqlGenerator, IActianUpdateSqlGenerator
    {
        public ActianUpdateSqlGenerator(
            [NotNull] UpdateSqlGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override ResultSetMapping AppendInsertOperation(StringBuilder commandStringBuilder, ModificationCommand command, int commandPosition)
        {
            var result = base.AppendInsertOperation(commandStringBuilder, command, commandPosition);
            var sql = commandStringBuilder.ToString();
            return result;
        }

        protected override void AppendInsertCommand(StringBuilder commandStringBuilder, string name, string schema, IReadOnlyList<ColumnModification> writeOperations)
        {
            base.AppendInsertCommand(commandStringBuilder, name, schema, writeOperations);
            var sql = commandStringBuilder.ToString();
            var slam = SqlGenerationHelper.StatementTerminator;
        }

        protected override ResultSetMapping AppendSelectAffectedCommand(StringBuilder commandStringBuilder, string name, string schema, IReadOnlyList<ColumnModification> readOperations, IReadOnlyList<ColumnModification> conditionOperations, int commandPosition)
        {
            return base.AppendSelectAffectedCommand(commandStringBuilder, name, schema, readOperations, conditionOperations, commandPosition);
        }

        protected override void AppendIdentityWhereCondition([NotNull] StringBuilder commandStringBuilder, [NotNull] ColumnModification columnModification)
        {
            SqlGenerationHelper.DelimitIdentifier(commandStringBuilder, columnModification.ColumnName);
            commandStringBuilder
                .Append(" = ")
                .Append("LAST_IDENTITY()");
        }

        protected override void AppendRowsAffectedWhereCondition([NotNull] StringBuilder commandStringBuilder, int expectedRowsAffected)
        {
            commandStringBuilder.Append("1 = 1");
            //commandStringBuilder
            //    .Append("ROW_COUNT() = ")
            //    .Append(expectedRowsAffected.ToString(CultureInfo.InvariantCulture));
        }
    }
}
