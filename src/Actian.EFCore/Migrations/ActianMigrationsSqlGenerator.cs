using System.Collections.Generic;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Migrations
{
    public partial class ActianMigrationsSqlGenerator : MigrationsSqlGenerator
    {
        private readonly IMigrationsAnnotationProvider _migrationsAnnotations;
        private IReadOnlyList<MigrationOperation> _operations;
        private string _currentUserName = null;
        private string StatementTerminator => Dependencies.SqlGenerationHelper.StatementTerminator;

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
    }
}
