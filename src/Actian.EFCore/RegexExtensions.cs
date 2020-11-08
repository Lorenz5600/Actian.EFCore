using System.Text.RegularExpressions;

namespace Actian.EFCore
{
    public static class RegexExtensions
    {
        public static bool Matches(this string input, Regex re, out Match match)
        {
            match = re.Match(input);
            return match.Success;
        }

        public static bool Matches(this string input, string pattern, out Match match)
        {
            return input.Matches(new Regex(pattern), out match);
        }

        public static bool Matches(this string input, string pattern, RegexOptions options, out Match match)
        {
            return input.Matches(new Regex(pattern, options), out match);
        }
    }
}
