using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<TSource> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<TResult> MinAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<TResult> MinAwaitAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }
    }

    internal static partial class Min
    {
        public static async UniTask<TSource> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            TSource value = default;
            var comparer = Comparer<TSource>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }

        public static async UniTask<TResult> MinAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
        {
            TResult value = default;
            var comparer = Comparer<TResult>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }

        public static async UniTask<TResult> MinAwaitAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken)
        {
            TResult value = default;
            var comparer = Comparer<TResult>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }

        public static async UniTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
        {
            TResult value = default;
            var comparer = Comparer<TResult>.Default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);

                    goto NEXT_LOOP;
                }

                return value;

                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (comparer.Compare(value, x) > 0)
                    {
                        value = x;
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

            return value;
        }
    }
}