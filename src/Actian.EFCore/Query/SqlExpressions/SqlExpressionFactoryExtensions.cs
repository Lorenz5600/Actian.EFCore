using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Actian.EFCore.Query.SqlExpressions
{
    public static partial class SqlExpressionFactoryExtensions
    {
        public static LikeExpression Like(this ISqlExpressionFactory factory, SqlExpression match, string pattern, char? escapeChar = null)
            => factory.Like(match, factory.Constant(pattern), factory.EscapeChar(escapeChar));

        private static SqlExpression EscapeChar(this ISqlExpressionFactory factory, char? escapeChar)
            => escapeChar is null ? null : factory.Constant(escapeChar.ToString());
    }
}
