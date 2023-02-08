using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Actian.EFCore.Extensions.Internal;
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
        private readonly IMigrationsAnnotationProvider _migrationsAnnotations;
        private IReadOnlyList<MigrationOperation> _operations;
        private string _currentUserName = null;
        private string StatementTerminator => Dependencies.SqlGenerationHelper.StatementTerminator;

        public ActianMigrationsSqlGenerator(
            [NotNull] MigrationsSqlGeneratorDependencies dependencies,
            [NotNull] IMigrationsAnnotationProvider migrationsAnnotations)
            : base(dependencies)
        {
            _migrationsAnnotations = migrationsAnnotations;
        }

        public override IReadOnlyList<MigrationCommand> Generate(
            IReadOnlyList<MigrationOperation> operations,
            IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            _operations = operations;
            try
            {
                var builder = new MigrationCommandListBuilder(Dependencies);
                ResetCurrentUser(builder);
                foreach (var operation in operations)
                {
                    Generate(operation, model, builder);
                }
                ResetCurrentUser(builder);
                return builder.GetCommandList();
            }
            finally
            {
                _operations = null;
            }
        }

        #region Databases

        protected override void Generate(
            [NotNull] AlterDatabaseOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(model, nameof(model));
            Check.NotNull(builder, nameof(builder));

            throw new NotSupportedException("Actian does not support altering an existing database.");
        }

        #endregion Databases

        #region Schemas

        protected override void Generate(
            [NotNull] EnsureSchemaOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

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

        #endregion Schemas

        #region Tables

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

        #endregion Tables

        #region Columns

        protected override void Generate(
            [NotNull] AddColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            ClearDefaultValueIfIdentity(operation);

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");

            ColumnDefinition(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);

                ModifyToReconstruct(builder, operation.Schema, operation.Table);
            }
        }

        protected override void Generate(
            [NotNull] RenameColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" RENAME COLUMN ")
                .Append(DelimitIdentifier(operation.Name))
                .Append(" TO ")
                .Append(DelimitIdentifier(operation.NewName))
                .AppendLine(StatementTerminator);

            EndStatement(builder);

            ModifyToReconstruct(builder, operation.Schema, operation.Table);
        }

        protected override void Generate(
            [NotNull] AlterColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            var indexesToRebuild = null as IEnumerable<IIndex>;
            var property = FindProperty(model, operation.Schema, operation.Table, operation.Name);

            if (operation.ComputedColumnSql != null)
            {
                        throw new NotSupportedException("Actian databases do not support computed columns");
            }

            var narrowed = false;
            if (IsOldColumnSupported(model))
            {
                if (IsIdentity(operation) != IsIdentity(operation.OldColumn))
                {
                    throw new InvalidOperationException(ActianStrings.AlterIdentityColumn);
                }

                var type = operation.ColumnType ?? GetColumnType(
                    operation.Schema,
                    operation.Table,
                    operation.Name,
                    operation,
                    model
                );

                var oldType = operation.OldColumn.ColumnType ?? GetColumnType(
                    operation.Schema,
                    operation.Table,
                    operation.Name,
                    operation.OldColumn,
                    model
                );

                narrowed = type != oldType || !operation.IsNullable && operation.OldColumn.IsNullable;
            }

            if (narrowed)
            {
                indexesToRebuild = GetIndexesToRebuild(property, operation).ToList();
                DropIndexes(indexesToRebuild, builder);
            }

            builder
                .Append("ALTER TABLE ")
                .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ALTER COLUMN ");

                var definitionOperation = new AlterColumnOperation
            {
                Schema = operation.Schema,
                Table = operation.Table,
                Name = operation.Name,
                ClrType = operation.ClrType,
                ColumnType = operation.ColumnType,
                IsUnicode = operation.IsUnicode,
                IsFixedLength = operation.IsFixedLength,
                MaxLength = operation.MaxLength,
                IsRowVersion = operation.IsRowVersion,
                IsNullable = operation.IsNullable,
                ComputedColumnSql = operation.ComputedColumnSql,
                OldColumn = operation.OldColumn,
                DefaultValue = operation.DefaultValue,
                DefaultValueSql = operation.DefaultValueSql
            };

            definitionOperation.AddAnnotations(operation.GetAnnotations());

            ClearDefaultValueIfIdentity(definitionOperation);

            ColumnDefinition(
                operation.Schema,
                operation.Table,
                operation.Name,
                definitionOperation,
                model,
                builder
            );

            builder.AppendLine(StatementTerminator);

            if (operation.OldColumn.Comment != operation.Comment)
            {
                CommentOnColumn(
                    builder,
                    operation.Schema,
                    operation.Table,
                    operation.Name,
                    operation.Comment
                );
            }

            if (narrowed)
            {
                CreateIndexes(indexesToRebuild, builder);
            }

            EndStatement(builder);

            ModifyToReconstruct(builder, operation.Schema, operation.Table);
        }

        protected override void Generate(
            [NotNull] DropColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP COLUMN ")
                .Append(DelimitIdentifier(operation.Name))
                .Append(" RESTRICT");

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);

                ModifyToReconstruct(builder, operation.Schema, operation.Table);
            }
        }

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
                .Append(DelimitIdentifier(name))
                .Append(" ")
                .Append(columnType);

            builder.Append(operation.IsNullable ? " WITH NULL" : " NOT NULL");

            if (IdentityDefinition(operation, builder))
                return;

            if (!operation.IsNullable && operation.DefaultValue is null && operation.DefaultValueSql is null)
            {
                var property = FindProperty(model, schema, table, name);
                if (property != null && !property.IsKey() && !property.IsForeignKey())
                {
                    operation.DefaultValueSql = GetDefaultValueSql(columnType);
                }
            }

            DefaultValue(operation.DefaultValue, operation.DefaultValueSql, columnType, builder);
        }

        protected virtual bool IdentityDefinition(ColumnOperation operation, MigrationCommandListBuilder builder)
        {
            var valueGenerationStrategy = operation[ActianAnnotationNames.ValueGenerationStrategy] as ActianValueGenerationStrategy?;
            if (!valueGenerationStrategy.IsIdentity())
                return false;

            builder.Append(valueGenerationStrategy switch
            {
                ActianValueGenerationStrategy.IdentityAlwaysColumn => " GENERATED ALWAYS AS IDENTITY",
                ActianValueGenerationStrategy.IdentityByDefaultColumn => " GENERATED BY DEFAULT AS IDENTITY",
                _ => ""
            });

            if (operation[ActianAnnotationNames.IdentityOptions] is string identitySequenceOptions)
            {
                var options = IdentitySequenceOptionsData.Deserialize(identitySequenceOptions);

                var prefix = " (";

                var incrementBy = options.IncrementBy;

                var defaultMinValue = incrementBy > 0 ? 1 : operation.ClrType.MinLongValue();
                var defaultMaxValue = incrementBy > 0 ? operation.ClrType.MaxLongValue() : -1;

                var minValue = options.MinValue ?? defaultMinValue;
                var maxValue = options.MaxValue ?? defaultMaxValue;

                var defaultStartValue = incrementBy > 0 ? minValue : maxValue;
                if (options.StartValue.HasValue && options.StartValue != defaultStartValue)
                    AppendWithPrefix("START WITH ", options.StartValue);

                if (incrementBy != 1)
                    AppendWithPrefix("INCREMENT BY ", incrementBy);

                if (minValue != defaultMinValue)
                    AppendWithPrefix("MINVALUE ", minValue);

                if (maxValue != defaultMaxValue)
                    AppendWithPrefix("MAXVALUE ", maxValue);

                if (options.IsCyclic)
                    AppendWithPrefix("CYCLE");

                if (options.NumbersToCache != 1)
                    AppendWithPrefix("CACHE ", options.NumbersToCache);

                if (prefix != " (")
                    builder.Append(')');

                void AppendWithPrefix(params object[] values)
                {
                    builder.Append(prefix);
                    prefix = " ";
                    foreach (var value in values)
                    {
                        builder.Append(value);
                    }
                }
            }

            return true;
        }


        private string GetDefaultValueSql(string columnType) => columnType.ToLowerInvariant() switch
        {
            "money" => "0",
            _ when columnType.Contains("int") => "0",
            _ when columnType.Contains("float") => "0",
            _ when columnType.Contains("decimal") => "0",
            _ when columnType.Contains("char") => "''",
            _ => null
        };

        protected override void ComputedColumnDefinition(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string name,
            [NotNull] ColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
                throw new NotSupportedException("Actian databases do not support computed columns");
        }

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

            var keyOrIndex = GetIsKeyOrIndex(schema, table, name, model);

            var property = FindProperty(model, schema, table, name);
            if (property != null)
            {
                var isUnicode = property.IsUnicode();
                var maxLength = property.GetMaxLength();
                var isFixedLength = property.IsFixedLength();
                var isRowVersion = property.IsConcurrencyToken && property.ValueGenerated == ValueGenerated.OnAddOrUpdate;

                if (!keyOrIndex
                    && operation.IsUnicode == isUnicode
                    && operation.MaxLength == maxLength
                    && (operation.IsFixedLength ?? false) == isFixedLength
                    && operation.IsRowVersion == isRowVersion)
                {
                    return Dependencies.TypeMappingSource.FindMapping(property).StoreType;
                }
            }

            return Dependencies.TypeMappingSource.FindMapping(
                operation.ClrType,
                null,
                keyOrIndex,
                operation.IsUnicode,
                operation.MaxLength,
                operation.IsRowVersion,
                operation.IsFixedLength
            ).StoreType;
        }

        private bool GetIsKeyOrIndex(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string name,
            [CanBeNull] IModel model
            )
        {
            Check.NotEmpty(table, nameof(table));
            Check.NotEmpty(name, nameof(name));

            if (model is null)
                return false;

            foreach (var entity in FindEntityTypes(model, schema, table))
            {
                var entityProperty = entity.GetDeclaredProperties()
                    .FirstOrDefault(p => p.GetColumnName() == name);

                if (entityProperty is null)
                    continue;

                if (entityProperty.IsKey() || entityProperty.IsForeignKey())
                    return true;

                foreach (var index in entity.GetDeclaredIndexes())
                {
                    if (index.Properties.Any(p => p.Name == entityProperty.Name))
                        return true;
                }
            }

            return false;
        }

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
                    .Append(" WITH DEFAULT ")
                    .Append(defaultValueSql);
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
                    .Append(" WITH DEFAULT ")
                    .Append(typeMapping.GenerateSqlLiteral(defaultValue));
            }
        }

        private void ModifyToReconstruct(
            [NotNull] MigrationCommandListBuilder builder,
            [CanBeNull] string schema,
            [NotNull] string tableName
            )
        {
            Check.NotNull(builder, nameof(builder));
            Check.NotEmpty(tableName, nameof(tableName));

            SetCurrentUser(schema, builder);

            builder
                .Append("MODIFY ")
                .Append(DelimitIdentifier(tableName, schema))
                .Append(" TO RECONSTRUCT")
                .AppendLine(StatementTerminator);

            EndStatement(builder);
        }

        private static bool IsIdentity(ColumnOperation operation)
            => operation[ActianAnnotationNames.ValueGenerationStrategy] is ActianValueGenerationStrategy strategy && strategy.IsIdentity();

        private TColumnOperation ClearDefaultValueIfIdentity<TColumnOperation>(TColumnOperation operation)
            where TColumnOperation : ColumnOperation
        {
            if (operation[ActianAnnotationNames.ValueGenerationStrategy] is ActianValueGenerationStrategy strategy)
            {
                switch (strategy)
                {
                    case ActianValueGenerationStrategy.IdentityAlwaysColumn:
                    case ActianValueGenerationStrategy.IdentityByDefaultColumn:
                                                        operation.DefaultValue = null;
                        break;
                }
            }
            return operation;
        }

        #endregion Columns

        #region Primary Keys

        protected override void Generate(
            [NotNull] AddPrimaryKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");
            PrimaryKeyConstraint(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

        protected override void Generate(
            [NotNull] DropPrimaryKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                        .Append(" DROP CONSTRAINT ")
                .Append(DelimitIdentifier(operation.Name));

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

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
                    .Append(DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("PRIMARY KEY ");

            IndexTraits(operation, model, builder);

            builder.Append("(")
                .Append(ColumnList(operation.Columns))
                .Append(")");
        }

        #endregion Primary Keys

        #region Foreign Keys

        protected override void Generate(
            [NotNull] AddForeignKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");

            ForeignKeyConstraint(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

        protected override void Generate(
            [NotNull] DropForeignKeyOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP CONSTRAINT ")
                .Append(DelimitIdentifier(operation.Name))
                .Append(" RESTRICT");

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

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
                    .Append(DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("FOREIGN KEY (")
                .Append(ColumnList(operation.Columns))
                .Append(") REFERENCES ")
                .Append(DelimitIdentifier(operation.PrincipalTable, operation.PrincipalSchema));

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

        #endregion Foreign Keys

        #region Unique Constraints

        protected override void Generate(
            [NotNull] AddUniqueConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");
            UniqueConstraint(operation, model, builder);
            builder.AppendLine(StatementTerminator);
            EndStatement(builder);
        }

        protected override void Generate(
            [NotNull] DropUniqueConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP CONSTRAINT ")
                .Append(DelimitIdentifier(operation.Name))
                .AppendLine(StatementTerminator);

            EndStatement(builder);
        }

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
                    .Append(DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("UNIQUE ");

            IndexTraits(operation, model, builder);

            builder.Append("(")
                .Append(ColumnList(operation.Columns))
                .Append(")");
        }

        #endregion Unique Constraints

        #region Check Constraints

        protected override void Generate(
            [NotNull] CreateCheckConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);
            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" ADD ");
            CheckConstraint(operation, model, builder);
            builder.AppendLine(StatementTerminator);
            EndStatement(builder);
        }

        protected override void Generate(
            [NotNull] DropCheckConstraintOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                .Append("ALTER TABLE ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" DROP CONSTRAINT ")
                .Append(DelimitIdentifier(operation.Name))
                .AppendLine(StatementTerminator);

            EndStatement(builder);
        }

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
                    .Append(DelimitIdentifier(operation.Name))
                    .Append(" ");
            }

            builder
                .Append("CHECK ");

            builder.Append("(")
                .Append(operation.Sql)
                .Append(")");
        }

        #endregion Check Constraints

        #region Indexes

        protected override void Generate(
            [NotNull] CreateIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            var nullableColumns = operation.Columns
                .Where(column =>
                {
                    var property = FindProperty(model, operation.Schema, operation.Table, column);
                    return property?.IsColumnNullable() != false;
                })
                .ToList();

            builder.Append("CREATE ");

            if (operation.IsUnique && !nullableColumns.Any())
            {
                builder.Append("UNIQUE ");
            }

            IndexTraits(operation, model, builder);

            builder
                .Append("INDEX ")
                .Append(DelimitIdentifier(operation.Name))
                .Append(" ON ")
                .Append(DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" (")
                .Append(ColumnList(operation.Columns))
                .Append(")");

            IndexOptions(operation, model, builder);

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

        protected override void Generate(
            [NotNull] DropIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            SetCurrentUser(operation.Schema, builder);

            builder
                        .Append("DROP INDEX ")
                .Append(DelimitIdentifier(operation.Name, operation.Schema));

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }

        protected override void Generate(
            [NotNull] RenameIndexOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
                throw new NotImplementedException();
        }

        protected override void IndexTraits(
            [NotNull] MigrationOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
        }

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

        private IEnumerable<IIndex> GetIndexesToRebuild(
            [CanBeNull] IProperty property,
            [NotNull] MigrationOperation currentOperation)
        {
            Check.NotNull(currentOperation, nameof(currentOperation));

            if (property == null)
            {
                yield break;
            }

            var createIndexOperations = _operations
                .SkipWhile(o => o != currentOperation)
                .Skip(1)
                .OfType<CreateIndexOperation>()
                .ToList();

            var declaredIndexes = property.DeclaringEntityType.GetDerivedTypes().SelectMany(et => et.GetDeclaredIndexes());
            var indexes = property.DeclaringEntityType.GetIndexes().Concat(declaredIndexes);

            foreach (var index in indexes)
            {
                var indexName = index.GetName();
                if (createIndexOperations.Any(o => o.Name == indexName))
                {
                    continue;
                }

                if (index.Properties.Any(p => p == property))
                {
                    yield return index;
                }
                else if (index.GetIncludeProperties() is IReadOnlyList<string> includeProperties)
                {
                    if (includeProperties.Contains(property.Name))
                    {
                        yield return index;
                    }
                }
            }
        }

        private void DropIndexes(
            [NotNull] IEnumerable<IIndex> indexes,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(indexes, nameof(indexes));
            Check.NotNull(builder, nameof(builder));

            foreach (var index in indexes)
            {
                var operation = new DropIndexOperation
                {
                    Schema = index.DeclaringEntityType.GetSchema(),
                    Table = index.DeclaringEntityType.GetTableName(),
                    Name = index.GetName()
                };
                operation.AddAnnotations(_migrationsAnnotations.ForRemove(index));

                Generate(operation, index.DeclaringEntityType.Model, builder, terminate: false);
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
            }
        }

        private void CreateIndexes(
            [NotNull] IEnumerable<IIndex> indexes,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(indexes, nameof(indexes));
            Check.NotNull(builder, nameof(builder));

            foreach (var index in indexes)
            {
                var operation = new CreateIndexOperation
                {
                    IsUnique = index.IsUnique,
                    Name = index.GetName(),
                    Schema = index.DeclaringEntityType.GetSchema(),
                    Table = index.DeclaringEntityType.GetTableName(),
                    Columns = index.Properties.Select(p => p.GetColumnName()).ToArray(),
                    Filter = index.GetFilter()
                };
                operation.AddAnnotations(_migrationsAnnotations.For(index));

                Generate(operation, index.DeclaringEntityType.Model, builder, terminate: false);
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
            }
        }

        #endregion Indexes

        #region Sequences

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

        #endregion Sequences

        #region Data

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

        #endregion Data

        #region Comments

        private void CommentOnTable(
            [NotNull] MigrationCommandListBuilder builder,
            [CanBeNull] string schema,
            [NotNull] string table,
            [CanBeNull] string comment
        )
        {
            builder
                .Append("COMMENT ON TABLE ")
                .Append(DelimitIdentifier(table, schema))
                .Append(" IS ")
                .Append(SqlLiteral(comment ?? ""))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
        }

        private void CommentOnView(
            [NotNull] MigrationCommandListBuilder builder,
            [CanBeNull] string schema,
            [NotNull] string table,
            [CanBeNull] string comment
        )
        {
            builder
                .Append("COMMENT ON VIEW ")
                .Append(DelimitIdentifier(table, schema))
                .Append(" IS ")
                .Append(SqlLiteral(comment ?? ""))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
        }

        private void CommentOnColumn(
            [NotNull] MigrationCommandListBuilder builder,
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string column,
            [CanBeNull] string comment
        )
        {
            builder
                .Append("COMMENT ON COLUMN ")
                .Append(DelimitIdentifier(table, schema))
                .Append(".")
                .Append(DelimitIdentifier(column))
                .Append(" IS ")
                .Append(SqlLiteral(comment ?? ""))
                .AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
        }

        #endregion Comments

        #region Helpers

        protected override void EndStatement(
            [NotNull] MigrationCommandListBuilder builder,
            bool suppressTransaction = false
            )
        {
            base.EndStatement(builder, true);
        }

        private string SqlLiteral<T>(T value)
            => Dependencies.TypeMappingSource.GetMapping(typeof(T)).GenerateSqlLiteral(value);

        private string DelimitIdentifier([NotNull] string identifier)
            => Dependencies.SqlGenerationHelper.DelimitIdentifier(identifier);

        private string DelimitIdentifier([NotNull] string name, [CanBeNull] string schema)
            => Dependencies.SqlGenerationHelper.DelimitIdentifier(name, schema);

        private void SetCurrentUser(
            [CanBeNull] string userName,
            [NotNull] MigrationCommandListBuilder builder
            )
        {
            Check.NotNull(builder, nameof(builder));

            if (userName == _currentUserName)
                return;

            builder.Append("set session authorization ");
            if (userName is null)
            {
                builder.Append("initial_user");
            }
            else
            {
                builder.Append(DelimitIdentifier(userName));
            }
            builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);

            EndStatement(builder, true);

            _currentUserName = userName;
        }

        private void ResetCurrentUser(
            [NotNull] MigrationCommandListBuilder builder
            )
        {
            SetCurrentUser(null, builder);
        }

        #endregion Helpers
    }
}
