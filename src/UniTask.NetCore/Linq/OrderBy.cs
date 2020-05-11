using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Internal;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        #region OrderBy_OrderByDescending

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ThenBy_ThenByDescending

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}