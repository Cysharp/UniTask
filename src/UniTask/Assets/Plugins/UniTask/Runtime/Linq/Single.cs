using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<TSource> SingleAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return SingleOperator.SingleAsync(source, cancellationToken, false);
        }

        public static UniTask<TSource> SingleAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAsync(source, predicate, cancellationToken, false);
        }

        public static UniTask<TSource> SingleAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitAsync(source, predicate, cancellationToken, false);
        }

        public static UniTask<TSource> SingleAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitWithCancellationAsync(source, predicate, cancellationToken, false);
        }

        public static UniTask<TSource> SingleOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return SingleOperator.SingleAsync(source, cancellationToken, true);
        }

        public static UniTask<TSource> SingleOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAsync(source, predicate, cancellationToken, true);
        }

        public static UniTask<TSource> SingleOrDefaultAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitAsync(source, predicate, cancellationToken, true);
        }

        public static UniTask<TSource> SingleOrDefaultAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return SingleOperator.SingleAwaitWithCancellationAsync(source, predicate, cancellationToken, true);
        }
    }

    internal static class SingleOperator
    {
        public static async UniTask<TSource> SingleAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken, bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                if (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (!await e.MoveNextAsync())
                    {
                        return v;
                    }

                    throw Error.MoreThanOneElement();
                }
                else
                {
                    if (defaultIfEmpty)
                    {
                        return default;
                    }
                    else
                    {
                        throw Error.NoElements();
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
        }

        public static async UniTask<TSource> SingleAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;
                bool found = false;
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (predicate(v))
                    {
                        if (found)
                        {
                            throw Error.MoreThanOneElement();
                        }
                        else
                        {
                            found = true;
                            value = v;
                        }
                    }
                }

                if (found || defaultIfEmpty)
                {
                    return value;
                }

                throw Error.NoElements();
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UniTask<TSource> SingleAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;
                bool found = false;
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (await predicate(v))
                    {
                        if (found)
                        {
                            throw Error.MoreThanOneElement();
                        }
                        else
                        {
                            found = true;
                            value = v;
                        }
                    }
                }

                if (found || defaultIfEmpty)
                {
                    return value;
                }

                throw Error.NoElements();
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UniTask<TSource> SingleAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken, bool defaultIfEmpty)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                TSource value = default;
                bool found = false;
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (await predicate(v, cancellationToken))
                    {
                        if (found)
                        {
                            throw Error.MoreThanOneElement();
                        }
                        else
                        {
                            found = true;
                            value = v;
                        }
                    }
                }

                if (found || defaultIfEmpty)
                {
                    return value;
                }

                throw Error.NoElements();
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