using System;
using System.Collections.Generic;

namespace Actian.EFCore.TestGenerators
{
    public static class StringExtensions
    {
        public static string RemoveLast(this string str, int count)
        {
            count = Math.Min(str.Length, count);
            return str.Remove(str.Length - count, count);
        }

        public static IEnumerable<string> NormalizeSql(this string sql)
        {
            foreach (var line in sql.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                yield return line.Replace("[", "\"\"").Replace("]", "\"\"");
            }
        }
    }
}
