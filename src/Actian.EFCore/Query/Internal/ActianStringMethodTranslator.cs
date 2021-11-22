// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Actian.EFCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Actian.EFCore.Query.Internal
{
    public class ActianStringMethodTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo StartsWith = typeof(string).GetRuntimeMethod(nameof(string.StartsWith), new[] { typeof(string) });
        private static readonly MethodInfo Contains = typeof(string).GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) });
        private static readonly MethodInfo EndsWith = typeof(string).GetRuntimeMethod(nameof(string.EndsWith), new[] { typeof(string) });

        private static readonly MethodInfo IndexOfChar = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(char) });
        private static readonly MethodInfo IndexOfString = typeof(string).GetRuntimeMethod(nameof(string.IndexOf), new[] { typeof(string) });

        private static readonly MethodInfo IsNullOrWhiteSpace = typeof(string).GetRuntimeMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) });
        private static readonly MethodInfo PadLeft = typeof(string).GetRuntimeMethod(nameof(string.PadLeft), new[] { typeof(int) });
        private static readonly MethodInfo PadLeftWithChar = typeof(string).GetRuntimeMethod(nameof(string.PadLeft), new[] { typeof(int), typeof(char) });
        private static readonly MethodInfo PadRight = typeof(string).GetRuntimeMethod(nameof(string.PadRight), new[] { typeof(int) });
        private static readonly MethodInfo PadRightWithChar = typeof(string).GetRuntimeMethod(nameof(string.PadRight), new[] { typeof(int), typeof(char) });
        private static readonly MethodInfo Replace = typeof(string).GetRuntimeMethod(nameof(string.Replace), new[] { typeof(string), typeof(string) });
        private static readonly MethodInfo Substring = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int) });
        private static readonly MethodInfo SubstringWithLength = typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int) });
        private static readonly MethodInfo ToLower = typeof(string).GetRuntimeMethod(nameof(string.ToLower), new Type[0]);
        private static readonly MethodInfo ToUpper = typeof(string).GetRuntimeMethod(nameof(string.ToUpper), new Type[0]);
        private static readonly MethodInfo TrimBothWithNoParam = typeof(string).GetRuntimeMethod(nameof(string.Trim), Type.EmptyTypes);
        private static readonly MethodInfo TrimBothWithChars = typeof(string).GetRuntimeMethod(nameof(string.Trim), new[] { typeof(char[]) });
        private static readonly MethodInfo TrimBothWithSingleChar = typeof(string).GetRuntimeMethod(nameof(string.Trim), new[] { typeof(char) });
        private static readonly MethodInfo TrimEndWithNoParam = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new Type[0]);
        private static readonly MethodInfo TrimEndWithChars = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new[] { typeof(char[]) });
        private static readonly MethodInfo TrimEndWithSingleChar = typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new[] { typeof(char) });
        private static readonly MethodInfo TrimStartWithNoParam = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new Type[0]);
        private static readonly MethodInfo TrimStartWithChars = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new[] { typeof(char[]) });
        private static readonly MethodInfo TrimStartWithSingleChar = typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new[] { typeof(char) });

        private readonly ISqlExpressionFactory Factory;

        private const char LikeEscapeChar = '\\';

        public ActianStringMethodTranslator(ISqlExpressionFactory factory)
        {
            Factory = factory;
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
                return null;

            if (method == TrimBothWithSingleChar)
                return TranslateTrim(instance, arguments[0], TrimWhere.Both);

            if (method == TrimEndWithNoParam)
                return TranslateTrim(instance, TrimWhere.Trailing);

            if (method == TrimEndWithChars)
                return null;

            if (method == TrimEndWithSingleChar)
                return TranslateTrim(instance, arguments[0], TrimWhere.Trailing);

            if (method == TrimStartWithNoParam)
                return TranslateTrim(instance, TrimWhere.Leading);

            if (method == TrimStartWithChars)
                return null;

            if (method == TrimStartWithSingleChar)
                return TranslateTrim(instance, arguments[0], TrimWhere.Leading);

            return null;
        }

        private SqlExpression TranslateStartsWith(SqlExpression instance, SqlExpression pattern)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            pattern = Factory.ApplyTypeMapping(pattern, stringTypeMapping);

            if (pattern is SqlConstantExpression constantExpression)
            {
                if (!(constantExpression.Value is string constantPattern))
                    return Factory.Like(instance, Factory.Constant(null, stringTypeMapping));

                if (constantPattern == string.Empty)
                    return Factory.Constant(true);

                return constantPattern.Any(IsLikeWildChar)
                    ? Factory.Like(instance, EscapeLikePattern(constantPattern) + '%', LikeEscapeChar)
                    : Factory.Like(instance, EscapeLikePattern(constantPattern) + '%');
            }

            var length = Factory.Function("LENGTH", new[] { pattern }, typeof(int));
            var left = Factory.Function("LEFT", new[] { instance, length }, typeof(string), stringTypeMapping);
            return Factory.Equal(left, pattern);
        }

        private SqlExpression TranslateContains(SqlExpression instance, SqlExpression pattern)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            pattern = Factory.ApplyTypeMapping(pattern, stringTypeMapping);

            if (pattern is SqlConstantExpression constantExpression)
            {
                if (!(constantExpression.Value is string constantPattern))
                    return Factory.Like(instance, Factory.Constant(null, stringTypeMapping));

                if (constantPattern == string.Empty)
                    return Factory.Constant(true);

                return constantPattern.Any(IsLikeWildChar)
                    ? Factory.Like(instance, '%' + EscapeLikePattern(constantPattern) + '%', LikeEscapeChar)
                    : Factory.Like(instance, '%' + EscapeLikePattern(constantPattern) + '%');
            }

            return Factory.GreaterThan(
                Factory.Function("POSITION", new[] { pattern, instance }, typeof(int)),
                Factory.Constant(0)
            );
        }

        private SqlExpression TranslateEndsWith(SqlExpression instance, SqlExpression pattern)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            pattern = Factory.ApplyTypeMapping(pattern, stringTypeMapping);

            if (pattern is SqlConstantExpression constantExpression)
            {
                if (!(constantExpression.Value is string constantPattern))
                    return Factory.Like(instance, Factory.Constant(null, stringTypeMapping));

                if (constantPattern == string.Empty)
                    return Factory.Constant(true);

                return constantPattern.Any(IsLikeWildChar)
                    ? Factory.Like(instance, '%' + EscapeLikePattern(constantPattern), LikeEscapeChar)
                    : Factory.Like(instance, '%' + EscapeLikePattern(constantPattern));
            }

            var length = Factory.Function("LENGTH", new[] { pattern }, typeof(int));
            var right = Factory.Function("RIGHT", new[] { instance, length }, typeof(string), stringTypeMapping);
            return Factory.Equal(right, pattern);
        }

        private SqlExpression TranslateIndexOf(SqlExpression instance, SqlExpression argument, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, argument);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            argument = Factory.ApplyTypeMapping(argument, stringTypeMapping);
            var empty = Factory.Constant(string.Empty, stringTypeMapping);

            var charIndexExpression = Factory.Subtract(
                Factory.Function("POSITION", new[] { argument, instance }, returnType),
                Factory.Constant(1)
            );

            var isEmpty = Factory.Equal(argument, empty);

            return Factory.Case(
                new[] { new CaseWhenClause(isEmpty, Factory.Constant(0)) },
                charIndexExpression
            );
        }

        private SqlExpression TranslateIsNullOrWhiteSpace(SqlExpression instance, SqlExpression argument)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, argument);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            argument = Factory.ApplyTypeMapping(argument, stringTypeMapping);
            var empty = Factory.Constant(string.Empty, stringTypeMapping);

            var squeezed = Factory.Function("SQUEEZE", new[] { argument }, argument.Type, argument.TypeMapping);

            return Factory.OrElse(
                Factory.IsNull(argument),
                Factory.Equal(squeezed, empty)
            );
        }

        private SqlExpression TranslatePad(string function, SqlExpression instance, SqlExpression count, SqlExpression padding = null)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);

            var arguments = padding is null
                ? new[] { instance, count }
                : new[] { instance, count, Factory.ApplyTypeMapping(padding, stringTypeMapping) };

            return Factory.Function(function, arguments, instance.Type, instance.TypeMapping);
        }

        private SqlExpression TranslateReplace(SqlExpression instance, SqlExpression oldValue, SqlExpression newValue, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, oldValue, newValue);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            oldValue = Factory.ApplyTypeMapping(oldValue, stringTypeMapping);
            newValue = Factory.ApplyTypeMapping(newValue, stringTypeMapping);

            return Factory.Function(
                "REPLACE",
                new[] { instance, oldValue, newValue },
                returnType,
                stringTypeMapping
            );
        }

        private SqlExpression TranslateSubstring(SqlExpression instance, SqlExpression startIndex, Type returnType)
        {
            return TranslateSubstring(instance, startIndex, null, returnType);
        }

        private SqlExpression TranslateSubstring(SqlExpression instance, SqlExpression startIndex, SqlExpression length, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            startIndex = Factory.Add(startIndex, Factory.Constant(1));

            var arguments = length is null
                ? new[] { instance, startIndex }
                : new[] { instance, startIndex, Factory.ApplyTypeMapping(length, stringTypeMapping) };

            return Factory.Function(
                "SUBSTR",
                arguments,
                returnType,
                instance.TypeMapping
            );
        }

        private SqlExpression TranslateToLower(SqlExpression instance, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            return Factory.Function("LOWERCASE", new[] { instance }, returnType, instance.TypeMapping);
        }

        private SqlExpression TranslateToUpper(SqlExpression instance, Type returnType)
        {
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance);
            instance = Factory.ApplyTypeMapping(instance, stringTypeMapping);
            return Factory.Function("UPPERCASE", new[] { instance }, returnType, instance.TypeMapping);
        }

        private SqlExpression TranslateTrim(SqlExpression instance, TrimWhere where)
        {
            return Factory.Trim(instance, where);
        }

        private SqlExpression TranslateTrim(SqlExpression instance, SqlExpression trimChar, TrimWhere where)
        {
            return Factory.Trim(instance, trimChar, where);
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
    }
}
