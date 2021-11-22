using System;
using System.Collections.Generic;

namespace Actian.EFCore.TestGenerators
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var index = 0;
            foreach (var item in source)
            {
                if (predicate(item))
                    return index;
                index += 1;
            }
            return -1;
        }
    }
}
