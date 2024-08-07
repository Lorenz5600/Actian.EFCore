using Actian.EFCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace Actian.EFCore.Query.Internal
{
    public class ActianQueryCompilationContextFactory : IQueryCompilationContextFactory
    {
        private readonly IActianConnection _actianConnection;

        public ActianQueryCompilationContextFactory(
            QueryCompilationContextDependencies dependencies,
            RelationalQueryCompilationContextDependencies relationalDependencies,
            IActianConnection actianConnection)
        {
            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
            _actianConnection = actianConnection;
        }

        /// <summary>
        ///     Dependencies for this service.
        /// </summary>
        protected virtual QueryCompilationContextDependencies Dependencies { get; }

        /// <summary>
        ///     Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalQueryCompilationContextDependencies RelationalDependencies { get; }

        public virtual QueryCompilationContext Create(bool async)
            => new ActianQueryCompilationContext(
                Dependencies, RelationalDependencies, async, _actianConnection.IsMultipleActiveResultSetsEnabled);
    }

}
