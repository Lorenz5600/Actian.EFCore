using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Actian.EFCore.Query.Internal
{
    public class ActianConvertTranslator : IMethodCallTranslator
    {
        private static readonly Dictionary<string, string> _typeMapping = new Dictionary<string, string>
        {
            [nameof(Convert.ToByte)] = "tinyint",
            [nameof(Convert.ToDecimal)] = "decimal(18, 2)",
            [nameof(Convert.ToDouble)] = "float",
            [nameof(Convert.ToInt16)] = "smallint",
            [nameof(Convert.ToInt32)] = "int",
            [nameof(Convert.ToInt64)] = "bigint",
            [nameof(Convert.ToString)] = "nvarchar"
        };

        private static readonly List<Type> _supportedTypes = new List<Type>
        {
            typeof(byte),
            typeof(DateTime),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(long),
            typeof(short),
            typeof(string)
        };

        private static readonly IEnumerable<MethodInfo> _supportedMethods = _typeMapping.Keys.SelectMany(t => typeof(Convert)
            .GetTypeInfo()
            .GetDeclaredMethods(t)
            .Where(m => m.GetParameters().Length == 1 && _supportedTypes.Contains(m.GetParameters().First().ParameterType))
        );

        private readonly ISqlExpressionFactory Factory;

        public ActianConvertTranslator(ISqlExpressionFactory factory)
        {
            Factory = factory;
        }

        public virtual SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (!_supportedMethods.Contains(method))
                return null;

            return method.Name switch
            {
                nameof(Convert.ToString) => Factory.Function("nvarchar", new[] { arguments[0] }, method.ReturnType),
                _ => Factory.Convert(arguments[0], method.ReturnType)
            };
        }
    }
}
