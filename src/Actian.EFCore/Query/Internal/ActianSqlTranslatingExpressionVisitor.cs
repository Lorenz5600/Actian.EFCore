using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

// TODO: ActianSqlTranslatingExpressionVisitor
namespace Actian.EFCore.Query.Internal
{
    public class ActianSqlTranslatingExpressionVisitor : RelationalSqlTranslatingExpressionVisitor
    {
        private static readonly HashSet<string> _dateTimeDataTypes = new HashSet<string>
        {
            "time",
            "date",
            "datetime",
            "datetime2",
            "datetimeoffset"
        };

        private static bool HasDateTimeDataType(SqlExpression expression) => _dateTimeDataTypes.Contains(GetProviderType(expression));
        private static bool HasBooleanType(Expression expression) => expression.Type == typeof(bool);

        private static readonly HashSet<ExpressionType> _arithmeticOperatorTypes = new HashSet<ExpressionType>
        {
            ExpressionType.Add,
            ExpressionType.Subtract,
            ExpressionType.Multiply,
            ExpressionType.Divide,
            ExpressionType.Modulo
        };

        private readonly ISqlExpressionFactory Factory;

        public ActianSqlTranslatingExpressionVisitor(
            RelationalSqlTranslatingExpressionVisitorDependencies dependencies,
            IModel model,
            QueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor)
            : base(dependencies, model, queryableMethodTranslatingExpressionVisitor)
        {
            Factory = dependencies.SqlExpressionFactory;
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            if (binaryExpression.NodeType == ExpressionType.ExclusiveOr)
            {
                return TranslationFailed(binaryExpression.Left, Visit(binaryExpression.Left), out var sqlLeft)
                    || TranslationFailed(binaryExpression.Right, Visit(binaryExpression.Right), out var sqlRight)
                    ? null
                    : Factory.Function(ActianFakeFunctions.ExclusiveOr, new[] { sqlLeft, sqlRight }, binaryExpression.Type);
            }

            var visitedExpression = (SqlExpression)base.VisitBinary(binaryExpression);

            if (visitedExpression == null)
                return null;

            if (!(visitedExpression is SqlBinaryExpression sqlBinary))
                return visitedExpression;

            if (_arithmeticOperatorTypes.Contains(sqlBinary.OperatorType) && (HasDateTimeDataType(sqlBinary.Left) || HasDateTimeDataType(sqlBinary.Right)))
                return null;

            return visitedExpression;
        }

        public override SqlExpression TranslateLongCount(Expression expression = null)
        {
            if (expression != null)
            {
                // TODO: Translate Count with predicate for GroupBy
                return null;
            }

            return Factory.ApplyDefaultTypeMapping(Factory.Function("COUNT_BIG", new[] { Factory.Fragment("*") }, typeof(long)));
        }

        private static string GetProviderType(SqlExpression expression)
        {
            return expression.TypeMapping?.StoreType;
        }

        [DebuggerStepThrough]
        private bool TranslationFailed(Expression original, Expression translation, out SqlExpression castTranslation)
        {
            if (original != null
                && !(translation is SqlExpression))
            {
                castTranslation = null;
                return true;
            }

            castTranslation = translation as SqlExpression;
            return false;
        }
    }
}
