using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Actian.EFCore.Migrations
{
    partial class ActianMigrationsSqlGenerator
    {
        protected override void Generate(
            [NotNull] EnsureSchemaOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            // Schemas are implicitly created in Actian databases
        }

        protected override void Generate(
            [NotNull] DropSchemaOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Name, builder);

            builder
                .Append("DROP SCHEMA ")
                .Append(DelimitIdentifier(operation.Name))
                .Append(" RESTRICT")
                .AppendLine(StatementTerminator);

            EndStatement(builder);
        }
    }
}
