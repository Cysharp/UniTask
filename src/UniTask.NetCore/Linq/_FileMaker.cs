using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ___Dummy
{

    public interface IAsyncGrouping<TKey, TValue>
    {
    }


    public interface IOrderedAsyncEnumerable<T>
    {

    }
    public static partial class _FileMaker
    {
        // Buffer,Distinct, DistinctUntilChanged, Do, MaxBy, MinBy, Never,Return, Throw

        public static UniTask<TSource> AggregateAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TSource, TSource> accumulator, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> AggregateAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TSource, UniTask<TSource>> accumulator, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TAccumulate> AggregateAwaitAsync<TSource, TAccumulate>(this IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, UniTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> AggregateAwaitAsync<TSource, TAccumulate, TResult>(this IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, UniTask<TAccumulate>> accumulator, Func<TAccumulate, UniTask<TResult>> resultSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> AggregateAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TSource, CancellationToken, UniTask<TSource>> accumulator, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TAccumulate> AggregateAwaitWithCancellationAsync<TSource, TAccumulate>(this IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, UniTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> AggregateAwaitWithCancellationAsync<TSource, TAccumulate, TResult>(this IUniTaskAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, UniTask<TAccumulate>> accumulator, Func<TAccumulate, CancellationToken, UniTask<TResult>> resultSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AllAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AllAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AllAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AnyAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AnyAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AnyAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> AnyAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Append<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource element)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> AverageAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> AverageAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAsync(this IUniTaskAsyncEnumerable<Nullable<Int32>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAsync(this IUniTaskAsyncEnumerable<Nullable<Int64>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> AverageAsync(this IUniTaskAsyncEnumerable<Nullable<Single>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAsync(this IUniTaskAsyncEnumerable<Nullable<Double>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> AverageAsync(this IUniTaskAsyncEnumerable<Nullable<Decimal>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> AverageAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> AverageAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> AverageAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> Cast<TResult>(this IUniTaskAsyncEnumerable<Object> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Concat<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> ContainsAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> ContainsAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource value, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> CountAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> CountAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> CountAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> CountAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource defaultValue)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Distinct<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Distinct<TSource>(this IUniTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> ElementAtAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 index, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> ElementAtOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 index, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Except<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Except<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstOrDefaultAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> FirstOrDefaultAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public static IUniTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IUniTaskAsyncEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IUniTaskAsyncEnumerable<TSource>, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IUniTaskAsyncEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IUniTaskAsyncEnumerable<TElement>, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwait<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TKey, IUniTaskAsyncEnumerable<TSource>, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TElement, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, Func<TKey, IUniTaskAsyncEnumerable<TElement>, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwait<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TKey, IUniTaskAsyncEnumerable<TSource>, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwait<TSource, TKey, TElement, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, Func<TKey, IUniTaskAsyncEnumerable<TElement>, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitWithCancellation<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TKey, IUniTaskAsyncEnumerable<TSource>, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, Func<TKey, IUniTaskAsyncEnumerable<TElement>, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitWithCancellation<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TKey, IUniTaskAsyncEnumerable<TSource>, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupByAwaitWithCancellation<TSource, TKey, TElement, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, Func<TKey, IUniTaskAsyncEnumerable<TElement>, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IUniTaskAsyncEnumerable<TInner>, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IUniTaskAsyncEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupJoinAwait<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, IUniTaskAsyncEnumerable<TInner>, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupJoinAwait<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, IUniTaskAsyncEnumerable<TInner>, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, IUniTaskAsyncEnumerable<TInner>, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> GroupJoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, IUniTaskAsyncEnumerable<TInner>, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Intersect<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Intersect<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastOrDefaultAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> LastOrDefaultAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> LongCountAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> LongCountAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> LongCountAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> LongCountAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MaxAsync(this IUniTaskAsyncEnumerable<Nullable<Int64>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MaxAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MaxAsync(this IUniTaskAsyncEnumerable<Nullable<Single>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MaxAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MaxAsync(this IUniTaskAsyncEnumerable<Nullable<Double>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MaxAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MaxAsync(this IUniTaskAsyncEnumerable<Nullable<Decimal>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> MaxAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MaxAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MaxAsync(this IUniTaskAsyncEnumerable<Nullable<Int32>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MaxAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MaxAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> MaxAwaitAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MaxAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> MaxAwaitWithCancellationAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MaxAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MinAsync(this IUniTaskAsyncEnumerable<Nullable<Double>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MinAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MinAsync(this IUniTaskAsyncEnumerable<Nullable<Decimal>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MinAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MinAsync(this IUniTaskAsyncEnumerable<Nullable<Int32>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MinAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MinAsync(this IUniTaskAsyncEnumerable<Nullable<Int64>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MinAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MinAsync(this IUniTaskAsyncEnumerable<Nullable<Single>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MinAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> MinAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> MinAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> MinAwaitAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MinAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TResult> MinAwaitWithCancellationAsync<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> MinAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> OfType<TResult>(this IUniTaskAsyncEnumerable<Object> source)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Prepend<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource element)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Reverse<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }



        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, IUniTaskAsyncEnumerable<TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, IUniTaskAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> SequenceEqualAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Boolean> SequenceEqualAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleOrDefaultAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleOrDefaultAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<TSource> SingleOrDefaultAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Skip<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipLast<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> SumAsync(this IUniTaskAsyncEnumerable<Nullable<Int64>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> SumAsync(this IUniTaskAsyncEnumerable<Nullable<Single>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> SumAsync(this IUniTaskAsyncEnumerable<Nullable<Double>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> SumAsync(this IUniTaskAsyncEnumerable<Nullable<Decimal>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> SumAsync(this IUniTaskAsyncEnumerable<Int32> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> SumAsync(this IUniTaskAsyncEnumerable<Int64> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int64> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> SumAsync(this IUniTaskAsyncEnumerable<Single> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Single> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> SumAsync(this IUniTaskAsyncEnumerable<Double> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Double> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> SumAsync(this IUniTaskAsyncEnumerable<Decimal> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Decimal> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> SumAsync(this IUniTaskAsyncEnumerable<Nullable<Int32>> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> SumAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Nullable<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> SumAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int32>> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int32>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Int64>> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Int64>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Single>> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Single>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Double>> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Double>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Nullable<Decimal>> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Nullable<Decimal>>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int32> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int32>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Int64> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Int64>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Single> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Single>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Double> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Double>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Decimal> SumAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Decimal>> selector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Take<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeLast<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> ToAsyncEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> ToAsyncEnumerable<TSource>(this IObservable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> ToAsyncEnumerable<TSource>(this Task<TSource> task)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TSource>> ToDictionaryAwaitAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TSource>> ToDictionaryAwaitAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TElement>> ToDictionaryAwaitAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TElement>> ToDictionaryAwaitAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TSource>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TSource>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TElement>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<Dictionary<TKey, TElement>> ToDictionaryAwaitWithCancellationAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<TSource> ToEnumerable<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static UniTask<HashSet<TSource>> ToHashSetAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<HashSet<TSource>> ToHashSetAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<List<TSource>> ToListAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public static IObservable<TSource> ToObservable<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Union<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Union<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Where<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Where<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, Boolean> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> WhereAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> WhereAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> WhereAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> WhereAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<Boolean>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<ValueTuple<TFirst, TSecond>> Zip<TFirst, TSecond>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> ZipAwait<TFirst, TSecond, TResult>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, UniTask<TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> ZipAwaitWithCancellation<TFirst, TSecond, TResult>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, UniTask<TResult>> selector)
        {
            throw new NotImplementedException();
        }


    }


}































































































































































































































































































































































































































































































































































































































































































































































































