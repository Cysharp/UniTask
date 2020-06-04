using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<Int32> MinAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Int32> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> MinAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Int64> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> MinAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Single> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> MinAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Double> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> MinAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Decimal> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> MinAsync(this IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Int32?> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> MinAsync(this IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Int64?> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> MinAsync(this IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Single?> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> MinAsync(this IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Double?> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> MinAsync(this IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Min.MinAsync(source, cancellationToken);
        }

        public static UniTask<Decimal?> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Min.MinAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

    }

    internal static partial class Min
    {
        public static async UniTask<Int32> MinAsync(IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async UniTask<Int32> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Int32> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Int32> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async UniTask<Int64> MinAsync(IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async UniTask<Int64> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Int64> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Int64> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async UniTask<Single> MinAsync(IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async UniTask<Single> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Single> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Single> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async UniTask<Double> MinAsync(IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async UniTask<Double> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Double> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Double> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async UniTask<Decimal> MinAsync(IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value > x)
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

        public static async UniTask<Decimal> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Decimal> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value > x)
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

        public static async UniTask<Decimal> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value > x)
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

        public static async UniTask<Int32?> MinAsync(IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int32?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int32?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int32?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int64?> MinAsync(IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int64?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int64?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Int64?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Single?> MinAsync(IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Single?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Single?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Single?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Double?> MinAsync(IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Double?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Double?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Double?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Decimal?> MinAsync(IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Decimal?> MinAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Decimal?> MinAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value > x)
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

        public static async UniTask<Decimal?> MinAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value > x)
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

    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<Int32> MaxAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Int32> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> MaxAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Int64> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> MaxAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Single> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> MaxAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Double> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> MaxAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Decimal> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> MaxAsync(this IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Int32?> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> MaxAsync(this IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Int64?> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> MaxAsync(this IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Single?> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> MaxAsync(this IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Double?> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> MaxAsync(this IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Max.MaxAsync(source, cancellationToken);
        }

        public static UniTask<Decimal?> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Max.MaxAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

    }

    internal static partial class Max
    {
        public static async UniTask<Int32> MaxAsync(IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async UniTask<Int32> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Int32> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Int32> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            Int32 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async UniTask<Int64> MaxAsync(IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async UniTask<Int64> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Int64> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Int64> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            Int64 value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async UniTask<Single> MaxAsync(IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async UniTask<Single> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Single> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Single> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            Single value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async UniTask<Double> MaxAsync(IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async UniTask<Double> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Double> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Double> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            Double value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async UniTask<Decimal> MaxAsync(IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if (value < x)
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

        public static async UniTask<Decimal> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Decimal> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if (value < x)
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

        public static async UniTask<Decimal> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            Decimal value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                
                    goto NEXT_LOOP;
                }

                throw Error.NoElements();
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if (value < x)
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

        public static async UniTask<Int32?> MaxAsync(IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int32?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int32?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int32?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            Int32? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int64?> MaxAsync(IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int64?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int64?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Int64?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            Int64? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Single?> MaxAsync(IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Single?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Single?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Single?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            Single? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Double?> MaxAsync(IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Double?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Double?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Double?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            Double? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Decimal?> MaxAsync(IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = e.Current;
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = e.Current;
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Decimal?> MaxAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Decimal?> MaxAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current);
                    if( x == null) continue;
                    if (value < x)
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

        public static async UniTask<Decimal?> MaxAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            Decimal? value = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    value = await selector(e.Current, cancellationToken);
                    if(value == null) continue;
                
                    goto NEXT_LOOP;
                }

                return default;
                
                NEXT_LOOP:

                while (await e.MoveNextAsync())
                {
                    var x = await selector(e.Current, cancellationToken);
                    if( x == null) continue;
                    if (value < x)
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
