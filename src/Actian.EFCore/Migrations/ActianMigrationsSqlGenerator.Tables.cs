using System;
using System.Collections.Generic;
using Actian.EFCore.Internal;
using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Migrations.Internal;
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
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .Append("CREATE TABLE ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema))
                .AppendLine(" (");

            using (builder.Indent())
            {
                CreateTableColumns(operation, model, builder);
                CreateTableConstraints(operation, model, builder);
                builder.AppendLine();
            }

            builder.Append(")");

            builder.WithClause()
                .Locations(operation[ActianAnnotationNames.Locations] as IEnumerable<string>)
                .Journaling(operation[ActianAnnotationNames.Journaling] as bool?)
                .Duplicates(operation[ActianAnnotationNames.Duplicates] as bool?)
                .Build();

            builder.AppendLine(StatementTerminator);

            if (!string.IsNullOrEmpty(operation.Comment))
            {
                CommentOnTable(builder, operation.Schema, operation.Name, operation.Comment);
            }

            if (terminate)
            {
                EndStatement(builder);
            }
        }

        protected override void Generate(
            [NotNull] RenameTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (string.IsNullOrEmpty(operation.NewSchema))
            {
                operation.NewSchema = operation.Schema;
            }

            if (operation.NewSchema != operation.Schema)
                throw new InvalidOperationException(ActianStrings.RenameTableToDifferentSchema(operation.Schema, operation.Name, operation.NewSchema, operation.NewName));

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema))
                .Append(" RENAME TO ")
                .Append(DelimitIdentifier(operation.NewName));

            builder.AppendLine(StatementTerminator);
            EndStatement(builder);

            ModifyToReconstruct(builder, operation.Schema, operation.NewName);
        }

        protected override void Generate(
            [NotNull] AlterTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);
            // TODO: MODIFY Statement
            CommentOnTable(builder, operation.Schema, operation.Name, operation.Comment);
            EndStatement(builder);
        }

        protected override void Generate(
            [NotNull] DropTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                //.Append("DROP TABLE IF EXISTS ")
                .Append("DROP ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema));

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

        protected override void CreateTableConstraints(
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            CreateTablePrimaryKeyConstraint(operation, model, builder);
            CreateTableUniqueConstraints(operation, model, builder);
            CreateTableCheckConstraints(operation, model, builder);
            CreateTableForeignKeys(operation, model, builder);
        }
    }
}
