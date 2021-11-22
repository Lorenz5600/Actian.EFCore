using System;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Migrations
{
    partial class ActianMigrationsSqlGenerator
    {
        protected override void Generate(
            [NotNull] CreateSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("CREATE SEQUENCE ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema));

            var typeMapping = Dependencies.TypeMappingSource.GetMapping(operation.ClrType);

            if (operation.ClrType != typeof(long))
            {
                builder
                    .Append(" AS ")
                    .Append(typeMapping.StoreType);

                // set the typeMapping for use with operation.StartValue (i.e. a long) below
                typeMapping = Dependencies.TypeMappingSource.GetMapping(typeof(long));
            }

            builder
                .Append(" START WITH ")
                .Append(typeMapping.GenerateSqlLiteral(operation.StartValue));

            SequenceOptions(operation, model, builder);

            builder.AppendLine(StatementTerminator);

            EndStatement(builder);
        }

        protected override void Generate(
            [NotNull] RenameSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        protected override void Generate(
            [NotNull] AlterSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER SEQUENCE ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema));

            SequenceOptions(operation, model, builder);

            builder.AppendLine(StatementTerminator);

            EndStatement(builder);
        }

        protected override void Generate(
            [NotNull] DropSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                //.Append("DROP SEQUENCE IF EXISTS ")
                .Append("DROP SEQUENCE ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema))
                .AppendLine(StatementTerminator);

            EndStatement(builder);
        }

        protected override void Generate(
            [NotNull] RestartSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER SEQUENCE ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema))
                .Append(" RESTART WITH ")
                .Append(SqlLiteral(operation.StartValue))
                .AppendLine(StatementTerminator);

            EndStatement(builder);
        }

        protected override void SequenceOptions(
            [NotNull] AlterSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
            => SequenceOptions(
                operation.Schema,
                operation.Name,
                operation,
                model,
                builder);


        protected override void SequenceOptions(
            [NotNull] CreateSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
            => SequenceOptions(
                operation.Schema,
                operation.Name,
                operation,
                model,
                builder);


        protected override void SequenceOptions(
            [CanBeNull] string schema,
            [NotNull] string name,
            [NotNull] SequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .Append(" INCREMENT BY ")
                .Append(SqlLiteral(operation.IncrementBy));

            if (operation.MinValue != null)
            {
                builder
                    .Append(" MINVALUE ")
                    .Append(SqlLiteral(operation.MinValue));
            }
            else
            {
                builder.Append(" NO MINVALUE");
            }

            if (operation.MaxValue != null)
            {
                builder
                    .Append(" MAXVALUE ")
                    .Append(SqlLiteral(operation.MaxValue));
            }
            else
            {
                builder.Append(" NO MAXVALUE");
            }

            builder.Append(operation.IsCyclic ? " CYCLE" : " NO CYCLE");
        }
    }
}
