using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<Boolean> SequenceEqualAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, CancellationToken cancellationToken = default)
        {
            return SequenceEqualAsync(first, second, EqualityComparer<TSource>.Default, cancellationToken);
        }

        public static UniTask<Boolean> SequenceEqualAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return SequenceEqual.SequenceEqualAsync(first, second, comparer, cancellationToken);
        }
    }

    internal static class SequenceEqual
    {
        internal static async UniTask<bool> SequenceEqualAsync<TSource>(IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken)
        {
            var e1 = first.GetAsyncEnumerator(cancellationToken);
            try
            {
                var e2 = second.GetAsyncEnumerator(cancellationToken);
                try
                {
                    while (true)
                    {
                        if (await e1.MoveNextAsync())
                        {
                            if (await e2.MoveNextAsync())
                            {
                                if (comparer.Equals(e1.Current, e2.Current))
                                {
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                // e2 is finished, but e1 has value
                                return false;
                            }
                        }
                        else
                        {
                            // e1 is finished, e2?
                            if (await e2.MoveNextAsync())
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                finally
                {
                    if (e2 != null)
                    {
                        await e2.DisposeAsync();
                    }
                }
            }
            finally
            {
                if (e1 != null)
                {
                    await e1.DisposeAsync();
                }
            }
        }
    }
}