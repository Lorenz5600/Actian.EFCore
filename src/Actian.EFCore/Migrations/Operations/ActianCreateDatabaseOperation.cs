using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Actian.EFCore.Migrations.Operations
{
    /// <summary>
    /// An Actian-specific <see cref="MigrationOperation" /> to create a database.
    /// </summary>
    public class ActianCreateDatabaseOperation : MigrationOperation
    {
        /// <summary>
        /// The name of the database.
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
    }
}
