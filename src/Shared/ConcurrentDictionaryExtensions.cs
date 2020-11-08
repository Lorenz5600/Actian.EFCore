using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Actian.EFCore.Utilities
{
    [DebuggerStepThrough]
    internal static class ConcurrentDictionaryExtensions
    {
        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(
            [NotNull] this IEnumerable<(TKey key, TValue value)> source
            )
        {
            return new ConcurrentDictionary<TKey, TValue>(
                source.ToDictionary(item => item.key, item => item.value)
            );
        }

        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(
            [NotNull] this IEnumerable<(TKey key, TValue value)> source,
            [NotNull] IEqualityComparer<TKey> comparer
            )
        {
            return new ConcurrentDictionary<TKey, TValue>(
                source.ToDictionary(item => item.key, item => item.value),
                comparer
            );
        }
    }
}
