using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Query.SqlExpressions
{
    public static partial class SqlExpressionFactoryExtensions
    {
        public static TrimExpression Trim(this ISqlExpressionFactory factory, SqlExpression instance, SqlExpression trimChar, TrimWhere where = TrimWhere.Both)
            => (TrimExpression)factory.ApplyDefaultTypeMapping(new TrimExpression(instance, trimChar, where, null));

        public static TrimExpression Trim(this ISqlExpressionFactory factory, SqlExpression instance, TrimWhere where = TrimWhere.Both)
            => factory.Trim(instance, null, where);
    }

    public enum TrimWhere
    {
        Both,
        Leading,
        Trailing
    }

    public class TrimExpression : SqlExpression
    {

        public TrimExpression(SqlExpression instance, SqlExpression trimChar, TrimWhere? where, RelationalTypeMapping typeMapping)
            : base(instance.Type, typeMapping)
        {
            Instance = instance;
            TrimChar = trimChar;
            Where = where;
        }

        public SqlExpression Instance { get; }
        public SqlExpression TrimChar { get; }
        public TrimWhere? Where { get; }

        public override void Print(ExpressionPrinter expressionPrinter)
        {
            expressionPrinter.Append("TRIM(");

            expressionPrinter.Append(Where switch
            {
                TrimWhere.Leading => "LEADING ",
                TrimWhere.Trailing => "TRAILING ",
                _ => ""
            });

            if (TrimChar != null)
            {
                expressionPrinter.Visit(TrimChar);
                expressionPrinter.Append(" ");
            }
            expressionPrinter.Append("FROM ");
            expressionPrinter.Visit(Instance);
            expressionPrinter.Append(")");
        }
    }
}
