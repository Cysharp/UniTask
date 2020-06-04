using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<Int32> SumAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Int32> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> SumAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Int64> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> SumAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Single> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> SumAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Double> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> SumAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Decimal> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> SumAsync(this IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Int32?> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int32?> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> SumAsync(this IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Int64?> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Int64?> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> SumAsync(this IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Single?> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Single?> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> SumAsync(this IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Double?> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Double?> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> SumAsync(this IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Sum.SumAsync(source, cancellationToken);
        }

        public static UniTask<Decimal?> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<Decimal?> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Sum.SumAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

    }

    internal static class Sum
    {
        public static async UniTask<Int32> SumAsync(IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken)
        {
            Int32 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken)
        {
            Int32 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            Int32 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            Int32 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64> SumAsync(IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken)
        {
            Int64 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken)
        {
            Int64 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            Int64 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            Int64 sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single> SumAsync(IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken)
        {
            Single sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken)
        {
            Single sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            Single sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            Single sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double> SumAsync(IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken)
        {
            Double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken)
        {
            Double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            Double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            Double sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal> SumAsync(IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken)
        {
            Decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken)
        {
            Decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            Decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            Decimal sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32?> SumAsync(IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken)
        {
            Int32? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32?> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken)
        {
            Int32? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32?> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            Int32? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int32?> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            Int32? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64?> SumAsync(IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken)
        {
            Int64? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64?> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken)
        {
            Int64? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64?> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            Int64? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Int64?> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            Int64? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single?> SumAsync(IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken)
        {
            Single? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single?> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken)
        {
            Single? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single?> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            Single? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Single?> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            Single? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double?> SumAsync(IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken)
        {
            Double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double?> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken)
        {
            Double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double?> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            Double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Double?> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            Double? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal?> SumAsync(IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken)
        {
            Decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += e.Current.GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal?> SumAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken)
        {
            Decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += selector(e.Current).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal?> SumAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            Decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

        public static async UniTask<Decimal?> SumAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            Decimal? sum = default;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    sum += (await selector(e.Current, cancellationToken)).GetValueOrDefault();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return sum;
        }

    }
}
