using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Query.Internal
{
    public class ActianQueryTranslationPostprocessorFactory : IQueryTranslationPostprocessorFactory
    {
        private readonly IRelationalTypeMappingSource _typeMappingSource;

        public ActianQueryTranslationPostprocessorFactory(
            QueryTranslationPostprocessorDependencies dependencies,
            RelationalQueryTranslationPostprocessorDependencies relationalDependencies,
            IRelationalTypeMappingSource typeMappingSource)
        {
            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
            _typeMappingSource = typeMappingSource;
        }

        /// <summary>
        ///     Dependencies for this service.
        /// </summary>
        protected virtual QueryTranslationPostprocessorDependencies Dependencies { get; }

        /// <summary>
        ///     Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalQueryTranslationPostprocessorDependencies RelationalDependencies { get; }

        public virtual QueryTranslationPostprocessor Create(QueryCompilationContext queryCompilationContext)
            => new ActianQueryTranslationPostprocessor(Dependencies, RelationalDependencies, queryCompilationContext, _typeMappingSource);
    }
}
