using System;
using System.Collections.Generic;
using System.Linq;
using Actian.EFCore.Internal;
using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Migrations.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Actian.EFCore.Migrations
{
    partial class ActianMigrationsSqlGenerator
    {
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
                //.Append("DROP INDEX IF EXISTS ")
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
            // TODO: Implement
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

        /// <summary>
        ///     Gets the list of indexes that need to be rebuilt when the given property is changing.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="currentOperation"> The operation which may require a rebuild. </param>
        /// <returns> The list of indexes affected. </returns>
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

        /// <summary>
        ///     Generates SQL to drop the given indexes.
        /// </summary>
        /// <param name="indexes"> The indexes to drop. </param>
        /// <param name="builder"> The command builder to use to build the commands. </param>
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

        /// <summary>
        ///     Generates SQL to create the given indexes.
        /// </summary>
        /// <param name="indexes"> The indexes to create. </param>
        /// <param name="builder"> The command builder to use to build the commands. </param>
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
    }
}
