using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Actian.EFCore.Parsing.Script
{
    public interface ISqlStatement
    {
        bool IsEmpty { get; }
    }

    public class SqlStatement : ISqlStatement
    {
        public static SqlStatement Create(IEnumerable<string> parts)
        {
            return new SqlStatement(string.Join("", parts));
        }

        public static SqlStatement Create(params string[] parts)
        {
            return Create(parts.AsEnumerable());
        }

        public static readonly ISqlStatement Empty = Create();

        private SqlStatement(string sql)
        {
            Sql = sql?.Trim() ?? "";
            SqlWithoutComments = ActianScriptScanner.StripComments(Sql);
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Sql);
        public string Sql { get; }
        public string SqlWithoutComments { get; }
        public bool HasSql => !string.IsNullOrWhiteSpace(SqlWithoutComments);

        public override string ToString()
        {
            return Sql;
        }
    }

    public class SqlCommand : ISqlStatement
    {
        public static SqlCommand Create(string command, string param = null)
        {
            return new SqlCommand(command, param);
        }

        private SqlCommand(string command, string param)
        {
            Command = command;
            Param = param;
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Command);
        public string Command { get; set; }
        public string Param { get; set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Param)
                ? $@"\{Command}"
                : $@"\{Command} {Param}";
        }
    }
}
