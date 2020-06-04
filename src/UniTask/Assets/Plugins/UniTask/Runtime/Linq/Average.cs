using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<double> AverageAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<double> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<double> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<float> AverageAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<float> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<float> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<float> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<double> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<double> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<decimal> AverageAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<decimal> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<decimal> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<decimal> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAsync(this IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<double?> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAsync(this IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<double?> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<float?> AverageAsync(this IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<float?> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<float?> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<float?> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAsync(this IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<double?> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

        public static UniTask<decimal?> AverageAsync(this IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return Average.AverageAsync(source, cancellationToken);
        }

        public static UniTask<decimal?> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAsync(source, selector, cancellationToken);
        }

        public static UniTask<decimal?> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitAsync(source, selector, cancellationToken);
        }

        public static UniTask<decimal?> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(selector));

            return Average.AverageAwaitWithCancellationAsync(source, selector, cancellationToken);
        }

    }

    internal static class Average
    {
        public static async UniTask<double> AverageAsync(IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAsync(IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<double> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64 sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
                        count++;
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

            return (double)sum / count;
        }

        public static async UniTask<float> AverageAsync(IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Single sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
                        count++;
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

            return (float)(sum / count);
        }

        public static async UniTask<float> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Single sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
                        count++;
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

            return (float)(sum / count);
        }

        public static async UniTask<float> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Single sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
                        count++;
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

            return (float)(sum / count);
        }

        public static async UniTask<float> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Single sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
                        count++;
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

            return (float)(sum / count);
        }

        public static async UniTask<double> AverageAsync(IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
                        count++;
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

            return sum / count;
        }

        public static async UniTask<double> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
                        count++;
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

            return sum / count;
        }

        public static async UniTask<double> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
                        count++;
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

            return sum / count;
        }

        public static async UniTask<double> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Double sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
                        count++;
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

            return sum / count;
        }

        public static async UniTask<decimal> AverageAsync(IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += e.Current;
                        count++;
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

            return sum / count;
        }

        public static async UniTask<decimal> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += selector(e.Current);
                        count++;
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

            return sum / count;
        }

        public static async UniTask<decimal> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current);
                        count++;
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

            return sum / count;
        }

        public static async UniTask<decimal> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        sum += await selector(e.Current, cancellationToken);
                        count++;
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

            return sum / count;
        }

        public static async UniTask<double?> AverageAsync(IUniTaskAsyncEnumerable<Int32?> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32?> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int32? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAsync(IUniTaskAsyncEnumerable<Int64?> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64?> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Int64? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (double)sum / count;
        }

        public static async UniTask<float?> AverageAsync(IUniTaskAsyncEnumerable<Single?> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Single? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (float)(sum / count);
        }

        public static async UniTask<float?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single?> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Single? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (float)(sum / count);
        }

        public static async UniTask<float?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Single? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (float)(sum / count);
        }

        public static async UniTask<float?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Single? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return (float)(sum / count);
        }

        public static async UniTask<double?> AverageAsync(IUniTaskAsyncEnumerable<Double?> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<double?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double?> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<double?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<double?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Double? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<decimal?> AverageAsync(IUniTaskAsyncEnumerable<Decimal?> source, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = e.Current;
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<decimal?> AverageAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal?> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<decimal?> AverageAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

        public static async UniTask<decimal?> AverageAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal?>> selector, CancellationToken cancellationToken)
        {
            long count = 0;
            Decimal? sum = 0;

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    var v = await selector(e.Current, cancellationToken);
                    if (v.HasValue)
                    {
                        checked    
                        {
                            sum += v.Value;
                            count++;
                        }
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

            return sum / count;
        }

    }
}
