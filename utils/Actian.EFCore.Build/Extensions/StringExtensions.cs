using System.Linq;

namespace Actian.EFCore.Build
{
    public static class StringExtensions
    {
        public static string Repeat(this string str, int count)
        {
            if (count <= 0)
                return "";
            return string.Concat(Enumerable.Repeat(str, count));
        }
    }
}
