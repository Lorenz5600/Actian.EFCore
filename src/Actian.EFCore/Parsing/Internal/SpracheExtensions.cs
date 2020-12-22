using System.Collections.Generic;
using Sprache;

namespace Actian.EFCore.Parsing
{
    public static class SpracheExtensions
    {
        public static Parser<string> Text(this Parser<char> parser)
        {
            return parser.Select(c => $"{c}");
        }

        public static Parser<string> Text(this Parser<IEnumerable<string>> parser)
        {
            return parser.Select(s => string.Join("", s));
        }
    }
}
