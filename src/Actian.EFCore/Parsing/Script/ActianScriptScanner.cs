using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Actian.EFCore.Parsing.Script
{
    public static partial class ActianScriptScanner
    {
        public static IEnumerable<ISqlStatement> ScanScriptFromFile(string path)
        {
            return ScanScript(File.ReadAllText(path, Encoding.UTF8), Path.GetDirectoryName(path));
        }

        public static IEnumerable<ISqlStatement> ScanScript(string input, string directory)
        {
            return ScanStatements(new ActianScriptScannerContext(input, directory))
                .Where(statement => !statement.IsEmpty)
                .ToList();
        }

        public static string StripComments(string sql)
        {
            static (bool isComment, string part) scanLinePart(ActianScriptScannerContext context)
            {
                if (context.Eof)
                    return (false, "");

                return context.Char switch
                {
                    'N' when context.Rest.StartsWith("N'") => (false, ScanSqlString(context)),
                    'U' when context.Rest.StartsWith("U&'") => (false, ScanSqlString(context)),
                    '\'' => (false, ScanSqlString(context)),
                    '-' when context.Rest.StartsWith("--") => (true, context.GetRest()),
                    _ => (false, context.Get(1))
                };
            }

            var lines = sql.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Select(line =>
            {
                var text = new StringBuilder();
                var context = new ActianScriptScannerContext(line, "");
                while (!context.Eof)
                {
                    var (isComment, part) = scanLinePart(context);
                    if (isComment)
                        break;
                    text.Append(part);
                }
                return text.ToString();
            });
            return string.Join("\n", lines);
        }

        private static IEnumerable<ISqlStatement> ScanStatements(ActianScriptScannerContext context)
        {
            while (!context.Eof)
            {
                var statement = ScanStatement(context);
                context.Skip(';');
                context.SkipWhiteSpace();
                if (statement is SqlCommand command && command.Command == "include")
                {
                    foreach (var includedStatement in ScanScriptFromFile(Path.GetFullPath(Path.Combine(context.Directory, command.Param))))
                    {
                        yield return includedStatement;
                    }
                }
                else
                {
                    yield return statement;
                }
            }
        }

        private static ISqlStatement ScanStatement(ActianScriptScannerContext context)
        {
            context.SkipWhiteSpace();
            switch (context.Char)
            {
                case '\\':
                    return ScanCommand(context);
                default:
                    return ScanSqlStatement(context);
            }
        }

        private static SqlCommand ScanCommand(ActianScriptScannerContext context)
        {
            context.NextChar();
            var command = context.GetWhile(char.IsLetter);
            context.SkipWhiteSpace();

            switch (command)
            {
                case "g":
                case "go":
                    return SqlCommand.Create("go");
                case "co":
                case "continue":
                    return SqlCommand.Create("continue");
                case "noco":
                case "nocontinue":
                    return SqlCommand.Create("nocontinue");
                case "i":
                case "include":
                    return SqlCommand.Create("include", context.GetUntil(char.IsWhiteSpace));
                default:
                    return SqlCommand.Create(command);
            }
        }

        private static readonly Regex ProcedureRe = new Regex(@"^create(?:\s+or\s+replace)?\s+procedure\s", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        private static SqlStatement ScanSqlStatement(ActianScriptScannerContext context)
        {
            var inCreateProcedure = false;
            var started = false;
            var text = new StringBuilder();
            while (!context.Eof && context.Char != '\\')
            {
                if (context.Char == ';' && !inCreateProcedure)
                    break;

                if (!started && ProcedureRe.IsMatch(context.Rest))
                {
                    inCreateProcedure = true;
                }

                var (type, part) = ScanSqlStatementPart(context);
                text.Append(part);

                if (inCreateProcedure && part.Equals("end", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (type != "whitespace" && type != "comment")
                    started = true;
            }
            return SqlStatement.Create(text.ToString());
        }

        private static (string type, string part) ScanSqlStatementPart(ActianScriptScannerContext context)
        {
            return context.Char switch
            {
                'N' when context.Rest.StartsWith("N'") => ("string", ScanSqlString(context)),
                'U' when context.Rest.StartsWith("U&'") => ("string", ScanSqlString(context)),
                '\'' => ("string", ScanSqlString(context)),
                '-' when context.Rest.StartsWith("--") => ("comment", context.GetUntil('\r', '\n')),
                _ when char.IsWhiteSpace(context.Char) => ("whitespace", context.GetWhile(char.IsWhiteSpace)),
                _ when IsLetterOrUnderscore(context.Char) => ("word", context.GetWhile(IsLetterOrDigitOrUnderscore)),
                _ => ("other", context.Get(1))
            };
        }

        private static string ScanSqlString(ActianScriptScannerContext context)
        {
            var text = new StringBuilder();
            var start = context.GetUntil('\'');
            if (start == "N")
                start = "U&";
            context.NextChar();
            while (!context.Eof)
            {
                if (context.Rest.StartsWith("''"))
                {
                    text.Append(context.Get(2));
                }
                else if (context.Char == '\'')
                {
                    context.NextChar();
                    break;
                }
                else
                {
                    var (c, unicode) = ToActianChar(context.Char);
                    if (unicode)
                        start = "U&";
                    text.Append(c);
                    context.NextChar();
                }
            }
            return $"{start}'{text}'";
        }

        private static bool IsLetterOrUnderscore(char c)
        {
            return char.IsLetter(c) || c == '_';
        }

        private static bool IsLetterOrDigitOrUnderscore(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        private static (string c, bool unicode) ToActianChar(char c) => c < 128 && !char.IsControl(c)
            ? ($"{c}", false)
            : ($"\\{(int)c:X4}", true);
    }
}
