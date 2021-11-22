using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actian.EFCore.Migrations
{
    partial class ActianMigrationsSqlGenerator
    {
        /// <summary>
        ///     <para>
        ///         Generates a comment on command for the description of a table.
        ///     </para>
        /// </summary>
        /// <param name="builder"> The command builder to use to build the commands. </param>
        /// <param name="schema"> The schema of the table. </param>
        /// <param name="table"> The name of the table. </param>
        /// <param name="comment"> The new description to be applied. </param>
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

        /// <summary>
        ///     <para>
        ///         Generates a comment on command for the description of a view.
        ///     </para>
        /// </summary>
        /// <param name="builder"> The command builder to use to build the commands. </param>
        /// <param name="schema"> The schema of the table. </param>
        /// <param name="table"> The name of the table. </param>
        /// <param name="comment"> The new description to be applied. </param>
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

        /// <summary>
        ///     <para>
        ///         Generates a comment on command for the description of a column.
        ///     </para>
        /// </summary>
        /// <param name="builder"> The command builder to use to build the commands. </param>
        /// <param name="schema"> The schema of the table. </param>
        /// <param name="table"> The name of the table. </param>
        /// <param name="column"> The name of the column. </param>
        /// <param name="comment"> The new description to be applied. </param>
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
    }
}
