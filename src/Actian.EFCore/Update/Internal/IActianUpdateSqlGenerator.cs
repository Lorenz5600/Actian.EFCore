using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Update;

namespace Actian.EFCore.Update.Internal
{
    public interface IActianUpdateSqlGenerator : IUpdateSqlGenerator
    {
        ResultSetMapping AppendBulkInsertOperation(
            StringBuilder commandStringBuilder,
            IReadOnlyList<IReadOnlyModificationCommand> modificationCommands,
            int commandPosition,
            out bool requiresTransaction);

        ResultSetMapping AppendBulkInsertOperation(
            StringBuilder commandStringBuilder,
            IReadOnlyList<IReadOnlyModificationCommand> modificationCommands,
            int commandPosition)
            => AppendBulkInsertOperation(commandStringBuilder, modificationCommands, commandPosition, out _);
    }
}
