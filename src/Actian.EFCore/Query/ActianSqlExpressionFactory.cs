using Actian.EFCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Query
{
    public class ActianSqlExpressionFactory : SqlExpressionFactory
    {
        private readonly IRelationalTypeMappingSource _typeMappingSource;
        private readonly RelationalTypeMapping _boolTypeMapping;

        public ActianSqlExpressionFactory(SqlExpressionFactoryDependencies dependencies)
            : base(dependencies)
        {
            _typeMappingSource = dependencies.TypeMappingSource;
            _boolTypeMapping = _typeMappingSource.FindMapping(typeof(bool));
        }

        public override SqlExpression ApplyTypeMapping(SqlExpression sqlExpression, RelationalTypeMapping typeMapping)
        {
            if (sqlExpression == null || sqlExpression.TypeMapping != null)
                return sqlExpression;

            return sqlExpression switch
            {
                TrimExpression e => ApplyTypeMappingOnTrim(e),
                _ => base.ApplyTypeMapping(sqlExpression, typeMapping)
            };
        }

        private SqlExpression ApplyTypeMappingOnTrim(TrimExpression trimExpression)
        {
            var inferredTypeMapping = (trimExpression.TrimChar == null
                    ? ExpressionExtensions.InferTypeMapping(
                        trimExpression.Instance)
                    : ExpressionExtensions.InferTypeMapping(
                        trimExpression.Instance, trimExpression.TrimChar))
                ?? _typeMappingSource.FindMapping(trimExpression.Instance.Type);

            return new TrimExpression(
                ApplyTypeMapping(trimExpression.Instance, inferredTypeMapping),
                ApplyTypeMapping(trimExpression.TrimChar, inferredTypeMapping),
                trimExpression.Where,
                _boolTypeMapping);
        }
    }
}
