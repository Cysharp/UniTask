using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<Boolean> ContainsAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource value, CancellationToken cancellationToken = default)
        {
            return ContainsAsync(source, value, EqualityComparer<TSource>.Default, cancellationToken);
        }

        public static UniTask<Boolean> ContainsAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return Contains.ContainsAsync(source, value, comparer, cancellationToken);
        }
    }

    internal static class Contains
    {
        internal static async UniTask<bool> ContainsAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (comparer.Equals(value, e.Current))
                    {
                        return true;
                    }
                }

                return false;
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