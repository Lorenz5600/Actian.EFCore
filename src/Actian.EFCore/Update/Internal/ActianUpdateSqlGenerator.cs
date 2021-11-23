using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Actian.EFCore.Utilities;
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

        protected override ResultSetMapping AppendSelectAffectedCommand(StringBuilder commandStringBuilder, string name, string schema, IReadOnlyList<ColumnModification> readOperations, IReadOnlyList<ColumnModification> conditionOperations, int commandPosition)
        {
            return ResultSetMapping.NoResultSet;
        }

        protected override void AppendIdentityWhereCondition([NotNull] StringBuilder commandStringBuilder, [NotNull] ColumnModification columnModification)
        {
            throw new NotImplementedException();
        }

        protected override void AppendRowsAffectedWhereCondition([NotNull] StringBuilder commandStringBuilder, int expectedRowsAffected)
        {
            throw new NotImplementedException();
        }
    }
}
