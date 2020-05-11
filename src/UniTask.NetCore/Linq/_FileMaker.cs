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

 


        


    }


}

