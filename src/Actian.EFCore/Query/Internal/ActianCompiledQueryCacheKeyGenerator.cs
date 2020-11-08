using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;

namespace Actian.EFCore.Query.Internal
{
    public class ActianCompiledQueryCacheKeyGenerator : RelationalCompiledQueryCacheKeyGenerator
    {
        public ActianCompiledQueryCacheKeyGenerator(
            [NotNull] CompiledQueryCacheKeyGeneratorDependencies dependencies,
            [NotNull] RelationalCompiledQueryCacheKeyGeneratorDependencies relationalDependencies
            )
            : base(dependencies, relationalDependencies)
        {
        }
    }
}
