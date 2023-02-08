using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Actian.EFCore.Utilities
{
    internal static class DisposableExtensions
    {
        public static ValueTask DisposeAsyncIfAvailable([CanBeNull] this IDisposable disposable)
        {
            if (disposable != null)
            {
                if (disposable is IAsyncDisposable asyncDisposable)
                {
                    return asyncDisposable.DisposeAsync();
                }

                disposable.Dispose();
            }

            return default;
        }
    }
}
