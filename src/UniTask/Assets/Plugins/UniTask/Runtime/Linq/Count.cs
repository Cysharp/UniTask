using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<Int32> CountAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Count.CountAsync(source, cancellationToken);
        }

        public static UniTask<Int32> CountAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Count.CountAsync(source, predicate, cancellationToken);
        }

        public static UniTask<Int32> CountAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Count.CountAwaitAsync(source, predicate, cancellationToken);
        }

        public static UniTask<Int32> CountAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return Count.CountAwaitWithCancellationAsync(source, predicate, cancellationToken);
        }
    }

    internal static class Count
    {
        internal static async UniTask<int> CountAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            var count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked { count++; }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }

        internal static async UniTask<int> CountAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken)
        {
            var count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (predicate(e.Current))
                    {
                        checked { count++; }
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }

        internal static async UniTask<int> CountAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken)
        {
            var count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (await predicate(e.Current))
                    {
                        checked { count++; }
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }

        internal static async UniTask<int> CountAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken)
        {
            var count = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    if (await predicate(e.Current, cancellationToken))
                    {
                        checked { count++; }
                    }
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return count;
        }
    }
}