using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<TSource> ElementAtAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, int index, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ElementAt.ElementAtAsync(source, index, cancellationToken, false);
        }

        public static UniTask<TSource> ElementAtOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, int index, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return ElementAt.ElementAtAsync(source, index, cancellationToken, true);
        }
    }

    internal static class ElementAt
    {
        public static async UniTask<TSource> ElementAtAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, int index, CancellationToken cancellationToken, bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                int i = 0;
                while (await e.MoveNextAsync())
                {
                    if (i++ == index)
                    {
                        return e.Current;
                    }
                }

                if (defaultIfEmpty)
                {
                    return default;
                }
                else
                {
                    throw Error.ArgumentOutOfRange(nameof(index));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
    }
}