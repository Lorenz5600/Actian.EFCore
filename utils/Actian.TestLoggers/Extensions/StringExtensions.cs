using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Actian.TestLoggers
{
    public static class StringExtensions
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars()
            .Concat(" ()[]',")
            .Distinct()
            .ToArray();

        public static string ToFileName(this string str)
        {
            foreach (var c in InvalidFileNameChars)
            {
                str = str.Replace(c, '-');
            }
            return Regex.Replace(str, @"-+", "-");
        }

        public static IEnumerable<string> ToLines(this string str)
            => str.ToLines(StringSplitOptions.None, true);

        public static IEnumerable<string> ToLines(this string str, StringSplitOptions options = StringSplitOptions.None, bool trim = true)
        {
            if (str is null)
                return Enumerable.Empty<string>();

            var lines = str.Split(new[] { "\r\n", "\n" }, options);
            return trim ? lines.Trim(line => string.IsNullOrEmpty(line)) : lines;
        }
    }
}
