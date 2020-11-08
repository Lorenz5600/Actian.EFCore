using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Actian.EFCore.Internal;
using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Migrations.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Migrations
{
    public class ActianMigrationsSqlGenerator : MigrationsSqlGenerator
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IMigrationsAnnotationProvider _migrationsAnnotations;
#pragma warning restore IDE0052 // Remove unread private members

        /// <summary>
        /// Creates a new <see cref="ActianMigrationsSqlGenerator" /> instance.
        /// </summary>
        /// <param name="dependencies"> Parameter object containing dependencies for this service. </param>
        /// <param name="migrationsAnnotations"> Provider-specific Migrations annotations to use. </param>
        public ActianMigrationsSqlGenerator(
            [NotNull] MigrationsSqlGeneratorDependencies dependencies,
            [NotNull] IMigrationsAnnotationProvider migrationsAnnotations)
            : base(dependencies)
        {
            _migrationsAnnotations = migrationsAnnotations;
        }

        public string CurrentUserName { get; set; } = null;

        /// <inheritdoc />
        public override IReadOnlyList<MigrationCommand> Generate(
            IReadOnlyList<MigrationOperation> operations,
            IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            var builder = new MigrationCommandListBuilder(Dependencies);
            ResetUser(builder);
            foreach (var operation in operations)
            {
                Generate(operation, model, builder);
            }
            ResetUser(builder);
            return builder.GetCommandList();
        }




        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AddColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");

            ColumnDefinition(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AddForeignKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");

            ForeignKeyConstraint(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AddPrimaryKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");
            PrimaryKeyConstraint(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AddUniqueConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");
            UniqueConstraint(operation, model, builder);
            builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] CreateCheckConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");
            CheckConstraint(operation, model, builder);
            builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AlterColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AlterDatabaseOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] RenameIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AlterSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER SEQUENCE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema));

            SequenceOptions(operation, model, builder);

            builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] AlterTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);
            builder
                .Append("COMMENT ON TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema))
                .Append(" IS ")
                .Append(SqlLiteral(operation.Comment))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] RenameTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.NewSchema != operation.Schema)
            {
                throw new InvalidOperationException(ActianStrings.AlterMemoryOptimizedTable); // TODO: Use the correct string
            }

            SetUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema))
                .Append(" RENAME TO ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.NewName))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] CreateIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder.Append("CREATE ");

            if (operation.IsUnique)
            {
                builder.Append("UNIQUE ");
            }

            IndexTraits(operation, model, builder);

            builder
                .Append("INDEX ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .Append(" ON ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" (")
                .Append(ColumnList(operation.Columns))
                .Append(")");

            IndexOptions(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] EnsureSchemaOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] CreateSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("CREATE SEQUENCE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema));

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

            builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
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
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema))
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

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP COLUMN ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name));

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropForeignKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP CONSTRAINT ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .Append(" RESTRICT");

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                //.Append("DROP INDEX IF EXISTS ")
                .Append("DROP INDEX ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema));

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropPrimaryKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                //.Append(" DROP CONSTRAINT IF EXISTS ")
                .Append(" DROP CONSTRAINT ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name));

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropSchemaOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Name, builder);

            builder
                .Append("DROP SCHEMA ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .Append(" RESTRICT")
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                //.Append("DROP SEQUENCE IF EXISTS ")
                .Append("DROP SEQUENCE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                //.Append("DROP TABLE IF EXISTS ")
                .Append("DROP ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema));

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropUniqueConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP CONSTRAINT ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DropCheckConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP CONSTRAINT ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] RenameColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] RenameSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] RestartSequenceOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetUser(operation.Schema, builder);

            builder
                .Append("ALTER SEQUENCE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema))
                .Append(" RESTART WITH ")
                .Append(SqlLiteral(operation.StartValue))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] InsertDataOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            var sqlBuilder = new StringBuilder();
            foreach (var modificationCommand in operation.GenerateModificationCommands(model))
            {
                SqlGenerator.AppendInsertOperation(
                    sqlBuilder,
                    modificationCommand,
                    0);
            }

            builder.Append(sqlBuilder.ToString());

            if (terminate)
            {
                EndStatement(builder);
            }
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] DeleteDataOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            var sqlBuilder = new StringBuilder();
            foreach (var modificationCommand in operation.GenerateModificationCommands(model))
            {
                SqlGenerator.AppendDeleteOperation(
                    sqlBuilder,
                    modificationCommand,
                    0);
            }

            builder.Append(sqlBuilder.ToString());
            EndStatement(builder);
        }

        /// <inheritdoc />
        protected override void Generate(
            [NotNull] UpdateDataOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            var sqlBuilder = new StringBuilder();
            foreach (var modificationCommand in operation.GenerateModificationCommands(model))
            {
                SqlGenerator.AppendUpdateOperation(
                    sqlBuilder,
                    modificationCommand,
                    0);
            }

            builder.Append(sqlBuilder.ToString());
            EndStatement(builder);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void CreateTableColumns(
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            for (var i = 0; i < operation.Columns.Count; i++)
            {
                var column = operation.Columns[i];
                ColumnDefinition(column, model, builder);

                if (i != operation.Columns.Count - 1)
                {
                    builder.AppendLine(",");
                }
            }
        }

        /// <inheritdoc />
        protected override void ColumnDefinition(
            [NotNull] AddColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
            => ColumnDefinition(
                operation.Schema,
                operation.Table,
                operation.Name,
                operation,
                model,
                builder);

        /// <inheritdoc />
        protected override void ColumnDefinition(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string name,
            [NotNull] ColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.ComputedColumnSql != null)
            {
                ComputedColumnDefinition(schema, table, name, operation, model, builder);

                return;
            }

            var columnType = operation.ColumnType ?? GetColumnType(schema, table, name, operation, model);
            builder
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(name))
                .Append(" ")
                .Append(columnType);

            builder.Append(operation.IsNullable ? " NULL" : " NOT NULL");

            DefaultValue(operation.DefaultValue, operation.DefaultValueSql, columnType, builder);
        }

        /// <inheritdoc />
        protected override void ComputedColumnDefinition(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string name,
            [NotNull] ColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override string GetColumnType(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string name,
            [NotNull] ColumnOperation operation,
            [CanBeNull] IModel model)
        {
            Check.NotEmpty(table, nameof(table));
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(operation, nameof(operation));

            var keyOrIndex = false;

            var property = FindProperty(model, schema, table, name);
            if (property != null)
            {
                if (operation.IsUnicode == property.IsUnicode()
                    && operation.MaxLength == property.GetMaxLength()
                    && (operation.IsFixedLength ?? false) == property.IsFixedLength()
                    && operation.IsRowVersion == (property.IsConcurrencyToken && property.ValueGenerated == ValueGenerated.OnAddOrUpdate))
                {
                    return Dependencies.TypeMappingSource.FindMapping(property).StoreType;
                }

                keyOrIndex = property.IsKey() || property.IsForeignKey();
            }

            return Dependencies.TypeMappingSource.FindMapping(
                    operation.ClrType,
                    null,
                    keyOrIndex,
                    operation.IsUnicode,
                    operation.MaxLength,
                    operation.IsRowVersion,
                    operation.IsFixedLength)
                .StoreType;
        }

        /// <inheritdoc />
        protected override void DefaultValue(
            [CanBeNull] object defaultValue,
            [CanBeNull] string defaultValueSql,
            [CanBeNull] string columnType,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            if (defaultValueSql != null)
            {
                builder
                    .Append(" DEFAULT (")
                    .Append(defaultValueSql)
                    .Append(")");
            }
            else if (defaultValue != null)
            {
                var typeMapping = columnType != null
                    ? Dependencies.TypeMappingSource.FindMapping(defaultValue.GetType(), columnType)
                    : null;
                if (typeMapping == null)
                {
                    typeMapping = Dependencies.TypeMappingSource.GetMappingForValue(defaultValue);
                }

                builder
                    .Append(" DEFAULT ")
                    .Append(typeMapping.GenerateSqlLiteral(defaultValue));
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void CreateTableForeignKeys(
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            foreach (var foreignKey in operation.ForeignKeys)
            {
                builder.AppendLine(",");
                ForeignKeyConstraint(foreignKey, model, builder);
            }
        }

        /// <inheritdoc />
        protected override void ForeignKeyConstraint(
            [NotNull] AddForeignKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.Name != null)
            {
                builder
                    .Append("CONSTRAINT ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("FOREIGN KEY (")
                .Append(ColumnList(operation.Columns))
                .Append(") REFERENCES ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.PrincipalTable, operation.PrincipalSchema));

            if (operation.PrincipalColumns != null)
            {
                builder
                    .Append(" (")
                    .Append(ColumnList(operation.PrincipalColumns))
                    .Append(")");
            }

            if (operation.OnUpdate != ReferentialAction.NoAction)
            {
                builder.Append(" ON UPDATE ");
                ForeignKeyAction(operation.OnUpdate, builder);
            }

            if (operation.OnDelete != ReferentialAction.NoAction)
            {
                builder.Append(" ON DELETE ");
                ForeignKeyAction(operation.OnDelete, builder);
            }
        }

        /// <inheritdoc />
        protected override void CreateTablePrimaryKeyConstraint(
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.PrimaryKey != null)
            {
                builder.AppendLine(",");
                PrimaryKeyConstraint(operation.PrimaryKey, model, builder);
            }
        }

        /// <inheritdoc />
        protected override void PrimaryKeyConstraint(
            [NotNull] AddPrimaryKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.Name != null)
            {
                builder
                    .Append("CONSTRAINT ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("PRIMARY KEY ");

            IndexTraits(operation, model, builder);

            builder.Append("(")
                .Append(ColumnList(operation.Columns))
                .Append(")");
        }

        /// <inheritdoc />
        protected override void CreateTableUniqueConstraints(
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            foreach (var uniqueConstraint in operation.UniqueConstraints)
            {
                builder.AppendLine(",");
                UniqueConstraint(uniqueConstraint, model, builder);
            }
        }

        /// <inheritdoc />
        protected override void UniqueConstraint(
            [NotNull] AddUniqueConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.Name != null)
            {
                builder
                    .Append("CONSTRAINT ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("UNIQUE ");

            IndexTraits(operation, model, builder);

            builder.Append("(")
                .Append(ColumnList(operation.Columns))
                .Append(")");
        }

        /// <inheritdoc />
        protected override void CreateTableCheckConstraints(
            [NotNull] CreateTableOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            foreach (var checkConstraint in operation.CheckConstraints)
            {
                builder.AppendLine(",");
                CheckConstraint(checkConstraint, model, builder);
            }
        }

        /// <inheritdoc />
        protected override void CheckConstraint(
            [NotNull] CreateCheckConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.Name != null)
            {
                builder
                    .Append("CONSTRAINT ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("CHECK ");

            builder.Append("(")
                .Append(operation.Sql)
                .Append(")");
        }

        /// <inheritdoc />
        protected override void IndexTraits(
            [NotNull] MigrationOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
        }

        /// <inheritdoc />
        protected override void IndexOptions(
            [NotNull] CreateIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            if (!string.IsNullOrEmpty(operation.Filter))
            {
                throw new InvalidOperationException(ActianStrings.AlterMemoryOptimizedTable); // TODO: Use the correct string
            }

            builder.WithClause()
                .Persistence(operation[ActianAnnotationNames.Persistence] as bool?)
                .Build();
        }

        /// <inheritdoc />
        protected override void ForeignKeyAction(
            ReferentialAction referentialAction,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            switch (referentialAction)
            {
                case ReferentialAction.Restrict:
                    builder.Append("RESTRICT");
                    break;
                case ReferentialAction.Cascade:
                    builder.Append("CASCADE");
                    break;
                case ReferentialAction.SetNull:
                    builder.Append("SET NULL");
                    break;
                case ReferentialAction.SetDefault:
                    builder.Append("SET DEFAULT");
                    break;
                default:
                    Debug.Assert(
                        referentialAction == ReferentialAction.NoAction,
                        "Unexpected value: " + referentialAction);
                    break;
            }
        }

























        /// <inheritdoc />
        protected override void EndStatement(
            [NotNull] MigrationCommandListBuilder builder,
            bool suppressTransaction = false)
        {
            base.EndStatement(builder, true);
        }

        private void SetUser(
            [CanBeNull] string userName,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            if (userName == CurrentUserName)
                return;

            if (userName is null)
            {
                builder
                    .Append("set session authorization initial_user");
            }
            else
            {
                builder
                    .Append("set session authorization ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(userName));
            }

            EndStatement(builder, true);

            CurrentUserName = userName;
        }

        private void ResetUser(
            [NotNull] MigrationCommandListBuilder builder)
        {
            SetUser(null, builder);
        }

        private string SqlLiteral<T>(T value)
        {
            return Dependencies.TypeMappingSource.GetMapping(typeof(T)).GenerateSqlLiteral(value);
        }
    }
}
