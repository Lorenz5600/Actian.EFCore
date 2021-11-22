using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        public ActianSqlGenerationHelper([NotNull] RelationalSqlGenerationHelperDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <inheritdoc />
        public override string StatementTerminator => "";

        /// <inheritdoc />
        public override string BatchTerminator => "";

        /// <inheritdoc />
        public override void GenerateParameterName(StringBuilder builder, string name)
        {
            if (name.StartsWith("@"))
                builder.Append(name);
            else
                builder.Append("@").Append(name);
        }
    }
}
