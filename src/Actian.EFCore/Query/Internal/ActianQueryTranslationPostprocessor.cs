using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

// TODO: ActianQueryTranslationPostprocessor
namespace Actian.EFCore.Query.Internal
{
    public class ActianQueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {
        public ActianQueryTranslationPostprocessor(
            QueryTranslationPostprocessorDependencies dependencies,
            RelationalQueryTranslationPostprocessorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
        }

        public override Expression Process(Expression query)
        {
            query = base.Process(query);
            //query = new SearchConditionConvertingExpressionVisitor(SqlExpressionFactory).Visit(query);

            return query;
        }
    }
}
