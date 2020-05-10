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

        




        public static IUniTaskAsyncEnumerable<TSource> Distinct<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Distinct<TSource>(this IUniTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
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

 


        


        public static IUniTaskAsyncEnumerable<TSource> Union<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TSource> Union<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
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

