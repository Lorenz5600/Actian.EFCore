using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Actian.EFCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

#nullable enable

namespace Actian.EFCore.Query.Internal
{
    public class ActianStringMethodTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo StartsWith = typeof(string).GetRuntimeMethod(nameof(string.StartsWith), new[] { typeof(string) })!;
        private static readonly MethodInfo Contains = typeof(string).GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) })!;
        private static readonly MethodInfo EndsWith = typeof(string).GetRuntimeMethod(nameof(string.EndsWith), new[] { typeof(string) })!;

        private static readonly MethodInfo IndexOfChar = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(char) })!;
        private static readonly MethodInfo IndexOfString = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(string) })!;

        private static readonly MethodInfo IsNullOrWhiteSpace = typeof(string).GetRuntimeMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) })!;
        private static readonly MethodInfo PadLeft = typeof(string).GetRuntimeMethod(nameof(string.PadLeft), new[] { typeof(int) })!;
        private static readonly MethodInfo PadLeftWithChar = typeof(string).GetRuntimeMethod(nameof(string.PadLeft), new[] { typeof(int), typeof(char) })!;
        private static readonly MethodInfo PadRight = typeof(string).GetRuntimeMethod(nameof(string.PadRight), new[] { typeof(int) })!;
        private static readonly MethodInfo PadRightWithChar = typeof(string).GetRuntimeMethod(nameof(string.PadRight), new[] { typeof(int), typeof(char) })!;
        private static readonly MethodInfo Replace = typeof(string).GetRuntimeMethod(nameof(string.Replace), new[] { typeof(string), typeof(string) })!;
        private static readonly MethodInfo Substring = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int) })!;
        private static readonly MethodInfo SubstringWithLength = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int) })!;
        private static readonly MethodInfo ToLower = typeof(string).GetRuntimeMethod(nameof(string.ToLower), new Type[0])!;
        private static readonly MethodInfo ToUpper = typeof(string).GetRuntimeMethod(nameof(string.ToUpper), new Type[0])!;
        private static readonly MethodInfo TrimBothWithNoParam = typeof(string).GetRuntimeMethod(nameof(string.Trim), Type.EmptyTypes)!;
        private static readonly MethodInfo TrimBothWithChars = typeof(string).GetRuntimeMethod(nameof(string.Trim), new[] { typeof(char[]) })!;
        private static readonly MethodInfo TrimBothWithSingleChar = typeof(string).GetRuntimeMethod(nameof(string.Trim), new[] { typeof(char) })!;
        private static readonly MethodInfo TrimEndWithNoParam = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new Type[0])!;
        private static readonly MethodInfo TrimEndWithChars = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new[] { typeof(char[]) })!;
        private static readonly MethodInfo TrimEndWithSingleChar = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new[] { typeof(char) })!;
        private static readonly MethodInfo TrimStartWithNoParam = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new Type[0])!;
        private static readonly MethodInfo TrimStartWithChars = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new[] { typeof(char[]) })!;
        private static readonly MethodInfo TrimStartWithSingleChar = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new[] { typeof(char) })!;

        private static readonly MethodInfo IndexOfMethodInfo
    = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(string) })!;

        private static readonly MethodInfo IndexOfMethodInfoWithStartingPosition
            = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(string), typeof(int) })!;

        private static readonly MethodInfo ReplaceMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Replace), new[] { typeof(string), typeof(string) })!;

        private static readonly MethodInfo ToLowerMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.ToLower), Type.EmptyTypes)!;

        private static readonly MethodInfo ToUpperMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.ToUpper), Type.EmptyTypes)!;

        private static readonly MethodInfo SubstringMethodInfoWithOneArg
            = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int) })!;

        private static readonly MethodInfo SubstringMethodInfoWithTwoArgs
            = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int), typeof(int) })!;

        private static readonly MethodInfo IsNullOrEmptyMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.IsNullOrEmpty), new[] { typeof(string) })!;

        private static readonly MethodInfo IsNullOrWhiteSpaceMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) })!;

        // Method defined in netcoreapp2.0 only
        private static readonly MethodInfo TrimStartMethodInfoWithoutArgs
            = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), Type.EmptyTypes)!;

        private static readonly MethodInfo TrimEndMethodInfoWithoutArgs
            = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), Type.EmptyTypes)!;

        private static readonly MethodInfo TrimMethodInfoWithoutArgs
            = typeof(string).GetRuntimeMethod(nameof(string.Trim), Type.EmptyTypes)!;

        // Method defined in netstandard2.0
        private static readonly MethodInfo TrimStartMethodInfoWithCharArrayArg
            = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new[] { typeof(char[]) })!;

        private static readonly MethodInfo TrimEndMethodInfoWithCharArrayArg
            = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new[] { typeof(char[]) })!;

        private static readonly MethodInfo TrimMethodInfoWithCharArrayArg
            = typeof(string).GetRuntimeMethod(nameof(string.Trim), new[] { typeof(char[]) })!;

        private static readonly MethodInfo FirstOrDefaultMethodInfoWithoutArgs
            = typeof(Enumerable).GetRuntimeMethods().Single(
                m => m.Name == nameof(Enumerable.FirstOrDefault)
                    && m.GetParameters().Length == 1).MakeGenericMethod(typeof(char));

        private static readonly MethodInfo LastOrDefaultMethodInfoWithoutArgs
            = typeof(Enumerable).GetRuntimeMethods().Single(
                m => m.Name == nameof(Enumerable.LastOrDefault)
                    && m.GetParameters().Length == 1).MakeGenericMethod(typeof(char));

        private readonly ISqlExpressionFactory _sqlExpressionFactory;   

        private const char LikeEscapeChar = '\\';

        public ActianStringMethodTranslator(ISqlExpressionFactory factory)
        {
            _sqlExpressionFactory = factory;
        }

        public virtual SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (method == StartsWith)
                return TranslateStartsWith(instance, arguments[0]);

            if (method == Contains)
                return TranslateContains(instance, arguments[0]);

            if (method == EndsWith)
                return TranslateEndsWith(instance, arguments[0]);

            if (method == IndexOfChar || method == IndexOfString)
                return TranslateIndexOf(instance, arguments[0], method.ReturnType);

            if (method == IsNullOrWhiteSpace)
                return TranslateIsNullOrWhiteSpace(instance, arguments[0]);

            if (method == PadLeft)
                return TranslatePad("LPAD", instance, arguments[0]);

            if (method == PadLeftWithChar)
                return TranslatePad("LPAD", instance, arguments[0], arguments[1]);

            if (method == PadRight)
                return TranslatePad("RPAD", instance, arguments[0]);

            if (method == PadRightWithChar)
                return TranslatePad("RPAD", instance, arguments[0], arguments[1]);

            if (method == Replace)
                return TranslateReplace(instance, arguments[0], arguments[1], method.ReturnType);

            if (method == Substring)
                return TranslateSubstring(instance, arguments[0], method.ReturnType);

            if (method == SubstringWithLength)
                return TranslateSubstring(instance, arguments[0], arguments[1], method.ReturnType);

            if (method == ToLower)
                return TranslateToLower(instance, method.ReturnType);

            if (method == ToUpper)
                return TranslateToUpper(instance, method.ReturnType);

            if (method == TrimBothWithNoParam)
                return TranslateTrim(instance, TrimWhere.Both);

            if (method == TrimBothWithChars)
                return null!;

            if (method == TrimBothWithSingleChar)
                return TranslateTrim(instance, arguments[0], TrimWhere.Both);

            if (method == TrimEndWithNoParam)
                return TranslateTrim(instance, TrimWhere.Trailing);

            if (method == TrimEndWithChars)
                return null!;

            if (method == TrimEndWithSingleChar)
                return TranslateTrim(instance, arguments[0], TrimWhere.Trailing);

            if (method == TrimStartWithNoParam)
                return TranslateTrim(instance, TrimWhere.Leading);

            if (method == TrimStartWithChars)
                return null!;

            if (method == TrimStartWithSingleChar)
                return TranslateTrim(instance, arguments[0], TrimWhere.Leading);

            return null!;
        }

        private SqlExpression TranslateStartsWith(SqlExpression instance, SqlExpression pattern)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            pattern = _sqlExpressionFactory.ApplyTypeMapping(pattern, stringTypeMapping);

            if (pattern is SqlConstantExpression constantExpression)
            {
                if (!(constantExpression.Value is string constantPattern))
                    return _sqlExpressionFactory.Like(instance, _sqlExpressionFactory.Constant(null, stringTypeMapping));

                if (constantPattern == string.Empty)
                    return _sqlExpressionFactory.Constant(true);

                return constantPattern.Any(IsLikeWildChar)
                    ? _sqlExpressionFactory.Like(instance, EscapeLikePattern(constantPattern) + '%', LikeEscapeChar)
                    : _sqlExpressionFactory.Like(instance, EscapeLikePattern(constantPattern) + '%');
            }

            var length = _sqlExpressionFactory.Function("LENGTH",
                new[] { pattern },
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                typeof(int));
            var left = _sqlExpressionFactory.Function("LEFT",
                new[] { instance, length },
                nullable: true,
                argumentsPropagateNullability: new[] { true, true },
                typeof(string),
                stringTypeMapping);
            return _sqlExpressionFactory.Equal(left, pattern);
        }

        private SqlExpression TranslateContains(SqlExpression instance, SqlExpression pattern)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            pattern = _sqlExpressionFactory.ApplyTypeMapping(pattern, stringTypeMapping);

            if (pattern is SqlConstantExpression constantExpression)
            {
                if (!(constantExpression.Value is string constantPattern))
                    return _sqlExpressionFactory.Like(instance, _sqlExpressionFactory.Constant(null, stringTypeMapping));

                if (constantPattern == string.Empty)
                    return _sqlExpressionFactory.Constant(true);

                return constantPattern.Any(IsLikeWildChar)
                    ? _sqlExpressionFactory.Like(instance, '%' + EscapeLikePattern(constantPattern) + '%', LikeEscapeChar)
                    : _sqlExpressionFactory.Like(instance, '%' + EscapeLikePattern(constantPattern) + '%');
            }

            return _sqlExpressionFactory.GreaterThan(
                _sqlExpressionFactory.Function(
                    "POSITION",
                    new[] { pattern, instance },
                    nullable:true,
                    argumentsPropagateNullability: new[] { true, true },
                    typeof(int)),
                _sqlExpressionFactory.Constant(0)
            );
        }

        private SqlExpression TranslateEndsWith(SqlExpression instance, SqlExpression pattern)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            pattern = _sqlExpressionFactory.ApplyTypeMapping(pattern, stringTypeMapping);

            if (pattern is SqlConstantExpression constantExpression)
            {
                if (!(constantExpression.Value is string constantPattern))
                    return _sqlExpressionFactory.Like(instance, _sqlExpressionFactory.Constant(null, stringTypeMapping));

                if (constantPattern == string.Empty)
                    return _sqlExpressionFactory.Constant(true);

                return constantPattern.Any(IsLikeWildChar)
                    ? _sqlExpressionFactory.Like(instance, '%' + EscapeLikePattern(constantPattern), LikeEscapeChar)
                    : _sqlExpressionFactory.Like(instance, '%' + EscapeLikePattern(constantPattern));
            }

            var length = _sqlExpressionFactory.Function(
                "LENGTH",
                new[] { pattern },
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                typeof(int));
            var right = _sqlExpressionFactory.Function(
                "RIGHT",
                new[] { instance, length },
                nullable: true,
                argumentsPropagateNullability: new[] { true, true },
                typeof(string),
                stringTypeMapping);
            return _sqlExpressionFactory.Equal(right, pattern);
        }

        private SqlExpression TranslateIndexOf(SqlExpression instance, SqlExpression argument, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, argument);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            argument = _sqlExpressionFactory.ApplyTypeMapping(argument, stringTypeMapping);
            var empty = _sqlExpressionFactory.Constant(string.Empty, stringTypeMapping);

            var charIndexExpression = _sqlExpressionFactory.Subtract(
                _sqlExpressionFactory.Function("POSITION",
                new[] { argument, instance },
                nullable: true,
                argumentsPropagateNullability: new[] { true, true },
                returnType),
                _sqlExpressionFactory.Constant(1)
            );

            var isEmpty = _sqlExpressionFactory.Equal(argument, empty);

            return _sqlExpressionFactory.Case(
                new[] { new CaseWhenClause(isEmpty, _sqlExpressionFactory.Constant(0)) },
                charIndexExpression
            );
        }

        private SqlExpression TranslateIsNullOrWhiteSpace(SqlExpression instance, SqlExpression argument)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, argument);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            argument = _sqlExpressionFactory.ApplyTypeMapping(argument, stringTypeMapping);
            var empty = _sqlExpressionFactory.Constant(string.Empty, stringTypeMapping);

            var squeezed = _sqlExpressionFactory.Function("SQUEEZE",
                new[] { argument },
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                argument.Type,
                argument.TypeMapping);

            return _sqlExpressionFactory.OrElse(
                _sqlExpressionFactory.IsNull(argument),
                _sqlExpressionFactory.Equal(squeezed, empty)
            );
        }

        private SqlExpression TranslatePad(string function, SqlExpression instance, SqlExpression count, SqlExpression padding = null!)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);

            var arguments = padding is null
                ? new[] { instance, count }
                : new[] { instance, count, _sqlExpressionFactory.ApplyTypeMapping(padding, stringTypeMapping) };

            return _sqlExpressionFactory.Function(function,
                arguments,
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                instance.Type,
                instance.TypeMapping);
        }

        private SqlExpression TranslateReplace(SqlExpression instance, SqlExpression oldValue, SqlExpression newValue, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, oldValue, newValue);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            oldValue = _sqlExpressionFactory.ApplyTypeMapping(oldValue, stringTypeMapping);
            newValue = _sqlExpressionFactory.ApplyTypeMapping(newValue, stringTypeMapping);

            return _sqlExpressionFactory.Function(
                "REPLACE",
                new[] { instance, oldValue, newValue },
                nullable: true,
                argumentsPropagateNullability: new[] { true, true, true },
                returnType,
                stringTypeMapping
            );
        }

        private SqlExpression TranslateSubstring(SqlExpression instance, SqlExpression startIndex, Type returnType)
        {
            return TranslateSubstring(instance, startIndex, null!, returnType);
        }

        private SqlExpression TranslateSubstring(SqlExpression instance, SqlExpression startIndex, SqlExpression length, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            startIndex = _sqlExpressionFactory.Add(startIndex, _sqlExpressionFactory.Constant(1));

            var arguments = length is null
                ? new[] { instance, startIndex }
                : new[] { instance, startIndex, _sqlExpressionFactory.ApplyTypeMapping(length, stringTypeMapping) };

            return _sqlExpressionFactory.Function(
                "SUBSTR",
                arguments,
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                returnType,
                instance.TypeMapping
            );
        }

        private SqlExpression TranslateToLower(SqlExpression instance, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            return _sqlExpressionFactory.Function("LOWERCASE",
                new[] { instance },
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                returnType,
                instance.TypeMapping);
        }

        private SqlExpression TranslateToUpper(SqlExpression instance, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            return _sqlExpressionFactory.Function("UPPERCASE",
                new[] { instance },
                nullable: true,
                argumentsPropagateNullability: new[] { true },
                returnType,
                instance.TypeMapping);
        }

        private SqlExpression TranslateTrim(SqlExpression instance, TrimWhere where)
        {
            return _sqlExpressionFactory.Trim(instance, where);
        }

        private SqlExpression TranslateTrim(SqlExpression instance, SqlExpression trimChar, TrimWhere where)
        {
            return _sqlExpressionFactory.Trim(instance, trimChar, where);
        }


        private bool IsLikeWildChar(char c) => c == '%' || c == '_' || c == '[';

        private string EscapeLikePattern(string pattern)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < pattern.Length; i++)
            {
                var c = pattern[i];
                if (IsLikeWildChar(c) || c == LikeEscapeChar)
                {
                    builder.Append(LikeEscapeChar);
                }

                builder.Append(c);
            }

            return builder.ToString();
        }

        public virtual SqlExpression? Translate(
            SqlExpression? instance,
            MethodInfo method,
            IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (instance != null)
            {
                if (IndexOfMethodInfo.Equals(method))
                {
                    return TranslateIndexOf(instance, method, arguments[0], null);
                }

                if (IndexOfMethodInfoWithStartingPosition.Equals(method))
                {
                    return TranslateIndexOf(instance, method, arguments[0], arguments[1]);
                }

                if (ReplaceMethodInfo.Equals(method))
                {
                    var firstArgument = arguments[0];
                    var secondArgument = arguments[1];
                    var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, firstArgument, secondArgument);

                    instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
                    firstArgument = _sqlExpressionFactory.ApplyTypeMapping(firstArgument, stringTypeMapping);
                    secondArgument = _sqlExpressionFactory.ApplyTypeMapping(secondArgument, stringTypeMapping);

                    return _sqlExpressionFactory.Function(
                        "REPLACE",
                        new[] { instance, firstArgument, secondArgument },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true, true, true },
                        method.ReturnType,
                        stringTypeMapping);
                }

                if (ToLowerMethodInfo.Equals(method)
                    || ToUpperMethodInfo.Equals(method))
                {
                    return _sqlExpressionFactory.Function(
                        ToLowerMethodInfo.Equals(method) ? "LOWER" : "UPPER",
                        new[] { instance },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true },
                        method.ReturnType,
                        instance.TypeMapping);
                }

                if (SubstringMethodInfoWithOneArg.Equals(method))
                {
                    return _sqlExpressionFactory.Function(
                        "SUBSTRING",
                        new[]
                        {
                        instance,
                        _sqlExpressionFactory.Add(
                            arguments[0],
                            _sqlExpressionFactory.Constant(1)),
                        _sqlExpressionFactory.Function(
                            "LEN",
                            new[] { instance },
                            nullable: true,
                            argumentsPropagateNullability: new[] { true },
                            typeof(int))
                        },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true, true, true },
                        method.ReturnType,
                        instance.TypeMapping);
                }

                if (SubstringMethodInfoWithTwoArgs.Equals(method))
                {
                    return _sqlExpressionFactory.Function(
                        "SUBSTRING",
                        new[]
                        {
                        instance,
                        _sqlExpressionFactory.Add(
                            arguments[0],
                            _sqlExpressionFactory.Constant(1)),
                        arguments[1]
                        },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true, true, true },
                        method.ReturnType,
                        instance.TypeMapping);
                }

                if (TrimStartMethodInfoWithoutArgs.Equals(method)
                    || (TrimStartMethodInfoWithCharArrayArg.Equals(method)
                        // SqlServer LTRIM does not take arguments
                        && ((arguments[0] as SqlConstantExpression)?.Value as Array)?.Length == 0))
                {
                    return _sqlExpressionFactory.Function(
                        "LTRIM",
                        new[] { instance },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true },
                        instance.Type,
                        instance.TypeMapping);
                }

                if (TrimEndMethodInfoWithoutArgs.Equals(method)
                    || (TrimEndMethodInfoWithCharArrayArg.Equals(method)
                        // SqlServer RTRIM does not take arguments
                        && ((arguments[0] as SqlConstantExpression)?.Value as Array)?.Length == 0))
                {
                    return _sqlExpressionFactory.Function(
                        "RTRIM",
                        new[] { instance },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true },
                        instance.Type,
                        instance.TypeMapping);
                }

                if (TrimMethodInfoWithoutArgs.Equals(method)
                    || (TrimMethodInfoWithCharArrayArg.Equals(method)
                        // SqlServer LTRIM/RTRIM does not take arguments
                        && ((arguments[0] as SqlConstantExpression)?.Value as Array)?.Length == 0))
                {
                    return _sqlExpressionFactory.Function(
                        "LTRIM",
                        new[]
                        {
                        _sqlExpressionFactory.Function(
                            "RTRIM",
                            new[] { instance },
                            nullable: true,
                            argumentsPropagateNullability: new[] { true },
                            instance.Type,
                            instance.TypeMapping)
                        },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true },
                        instance.Type,
                        instance.TypeMapping);
                }
            }

            if (IsNullOrEmptyMethodInfo.Equals(method))
            {
                var argument = arguments[0];

                return _sqlExpressionFactory.OrElse(
                    _sqlExpressionFactory.IsNull(argument),
                    _sqlExpressionFactory.Like(
                        argument,
                        _sqlExpressionFactory.Constant(string.Empty)));
            }

            if (IsNullOrWhiteSpaceMethodInfo.Equals(method))
            {
                var argument = arguments[0];

                return _sqlExpressionFactory.OrElse(
                    _sqlExpressionFactory.IsNull(argument),
                    _sqlExpressionFactory.Equal(
                        argument,
                        _sqlExpressionFactory.Constant(string.Empty, argument.TypeMapping)));
            }

            if (FirstOrDefaultMethodInfoWithoutArgs.Equals(method))
            {
                var argument = arguments[0];
                return _sqlExpressionFactory.Function(
                    "SUBSTRING",
                    new[] { argument, _sqlExpressionFactory.Constant(1), _sqlExpressionFactory.Constant(1) },
                    nullable: true,
                    argumentsPropagateNullability: new[] { true, true, true },
                    method.ReturnType);
            }

            if (LastOrDefaultMethodInfoWithoutArgs.Equals(method))
            {
                var argument = arguments[0];
                return _sqlExpressionFactory.Function(
                    "SUBSTRING",
                    new[]
                    {
                    argument,
                    _sqlExpressionFactory.Function(
                        "LEN",
                        new[] { argument },
                        nullable: true,
                        argumentsPropagateNullability: new[] { true },
                        typeof(int)),
                    _sqlExpressionFactory.Constant(1)
                    },
                    nullable: true,
                    argumentsPropagateNullability: new[] { true, true, true },
                    method.ReturnType);
            }

            return null;
        }

        private SqlExpression TranslateIndexOf(
    SqlExpression instance,
    MethodInfo method,
    SqlExpression searchExpression,
    SqlExpression? startIndex)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, searchExpression)!;
            searchExpression = _sqlExpressionFactory.ApplyTypeMapping(searchExpression, stringTypeMapping);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);

            var charIndexArguments = new List<SqlExpression> { searchExpression, instance };

            if (startIndex is not null)
            {
                charIndexArguments.Add(
                    startIndex is SqlConstantExpression { Value: int constantStartIndex }
                        ? _sqlExpressionFactory.Constant(constantStartIndex + 1, typeof(int))
                        : _sqlExpressionFactory.Add(startIndex, _sqlExpressionFactory.Constant(1)));
            }

            var argumentsPropagateNullability = Enumerable.Repeat(true, charIndexArguments.Count);

            SqlExpression charIndexExpression;
            var storeType = stringTypeMapping.StoreType;
            if (string.Equals(storeType, "nvarchar(max)", StringComparison.OrdinalIgnoreCase)
                || string.Equals(storeType, "varchar(max)", StringComparison.OrdinalIgnoreCase))
            {
                charIndexExpression = _sqlExpressionFactory.Function(
                    "CHARINDEX",
                    charIndexArguments,
                    nullable: true,
                    argumentsPropagateNullability,
                    typeof(long));

                charIndexExpression = _sqlExpressionFactory.Convert(charIndexExpression, typeof(int));
            }
            else
            {
                charIndexExpression = _sqlExpressionFactory.Function(
                    "CHARINDEX",
                    charIndexArguments,
                    nullable: true,
                    argumentsPropagateNullability,
                    method.ReturnType);
            }

            charIndexExpression = _sqlExpressionFactory.Subtract(charIndexExpression, _sqlExpressionFactory.Constant(1));

            // If the pattern is an empty string, we need to special case to always return 0 (since CHARINDEX return 0, which we'd subtract to
            // -1). Handle separately for constant and non-constant patterns.
            if (searchExpression is SqlConstantExpression { Value: string constantSearchPattern })
            {
                return constantSearchPattern == string.Empty
                    ? _sqlExpressionFactory.Constant(0, typeof(int))
                    : charIndexExpression;
            }

            return _sqlExpressionFactory.Case(
                new[]
                {
                new CaseWhenClause(
                    _sqlExpressionFactory.Equal(
                        searchExpression,
                        _sqlExpressionFactory.Constant(string.Empty, stringTypeMapping)),
                    _sqlExpressionFactory.Constant(0))
                },
                charIndexExpression);
        }
    }
}
