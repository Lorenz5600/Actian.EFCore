using System;
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
