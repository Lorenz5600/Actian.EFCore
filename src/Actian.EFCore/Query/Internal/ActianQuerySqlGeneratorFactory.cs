using Microsoft.EntityFrameworkCore.Query;

namespace Actian.EFCore.Query.Internal
{
    public class ActianQuerySqlGeneratorFactory : IQuerySqlGeneratorFactory
    {
        private readonly QuerySqlGeneratorDependencies _dependencies;

        public ActianQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual QuerySqlGenerator Create()
            => new ActianQuerySqlGenerator(_dependencies);
    }
}
