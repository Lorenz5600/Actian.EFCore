using System;
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
            [NotNull] AlterDatabaseOperation operation,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(model, nameof(model));
            Check.NotNull(builder, nameof(builder));

            throw new NotSupportedException("Actian does not support altering an existing database.");
        }
    }
}
