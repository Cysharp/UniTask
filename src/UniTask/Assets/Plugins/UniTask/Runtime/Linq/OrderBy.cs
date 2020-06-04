using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        #region OrderBy_OrderByDescending

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, Comparer<TKey>.Default, false, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer, false, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(source, keySelector, Comparer<TKey>.Default, false, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(source, keySelector, comparer, false, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(source, keySelector, Comparer<TKey>.Default, false, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(source, keySelector, comparer, false, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, Comparer<TKey>.Default, true, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer, true, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(source, keySelector, Comparer<TKey>.Default, true, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new OrderedAsyncEnumerableAwait<TSource, TKey>(source, keySelector, comparer, true, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(source, keySelector, Comparer<TKey>.Default, true, null);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new OrderedAsyncEnumerableAwaitWithCancellation<TSource, TKey>(source, keySelector, comparer, true, null);
        }

        #endregion

        #region ThenBy_ThenByDescending

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, false);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, false);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, false);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, true);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, true);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwait<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, Comparer<TKey>.Default, true);
        }

        public static IUniTaskOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellation<TSource, TKey>(this IUniTaskOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        #endregion
    }

    internal abstract class AsyncEnumerableSorter<TElement>
    {
        internal abstract UniTask ComputeKeysAsync(TElement[] elements, int count);

        internal abstract int CompareKeys(int index1, int index2);

        internal async UniTask<int[]> SortAsync(TElement[] elements, int count)
        {
            await ComputeKeysAsync(elements, count);

            int[] map = new int[count];
            for (int i = 0; i < count; i++) map[i] = i;
            QuickSort(map, 0, count - 1);
            return map;
        }

        void QuickSort(int[] map, int left, int right)
        {
            do
            {
                int i = left;
                int j = right;
                int x = map[i + ((j - i) >> 1)];
                do
                {
                    while (i < map.Length && CompareKeys(x, map[i]) > 0) i++;
                    while (j >= 0 && CompareKeys(x, map[j]) < 0) j--;
                    if (i > j) break;
                    if (i < j)
                    {
                        int temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }
                    i++;
                    j--;
                } while (i <= j);
                if (j - left <= right - i)
                {
                    if (left < j) QuickSort(map, left, j);
                    left = i;
                }
                else
                {
                    if (i < right) QuickSort(map, i, right);
                    right = j;
                }
            } while (left < right);
        }
    }

    internal class SyncSelectorAsyncEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
    {
        readonly Func<TElement, TKey> keySelector;
        readonly IComparer<TKey> comparer;
        readonly bool descending;
        readonly AsyncEnumerableSorter<TElement> next;
        TKey[] keys;

        internal SyncSelectorAsyncEnumerableSorter(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TElement> next)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
        }

        internal override async UniTask ComputeKeysAsync(TElement[] elements, int count)
        {
            keys = new TKey[count];
            for (int i = 0; i < count; i++) keys[i] = keySelector(elements[i]);
            if (next != null) await next.ComputeKeysAsync(elements, count);
        }

        internal override int CompareKeys(int index1, int index2)
        {
            int c = comparer.Compare(keys[index1], keys[index2]);
            if (c == 0)
            {
                if (next == null) return index1 - index2;
                return next.CompareKeys(index1, index2);
            }
            return descending ? -c : c;
        }
    }

    internal class AsyncSelectorEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
    {
        readonly Func<TElement, UniTask<TKey>> keySelector;
        readonly IComparer<TKey> comparer;
        readonly bool descending;
        readonly AsyncEnumerableSorter<TElement> next;
        TKey[] keys;

        internal AsyncSelectorEnumerableSorter(Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TElement> next)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
        }

        internal override async UniTask ComputeKeysAsync(TElement[] elements, int count)
        {
            keys = new TKey[count];
            for (int i = 0; i < count; i++) keys[i] = await keySelector(elements[i]);
            if (next != null) await next.ComputeKeysAsync(elements, count);
        }

        internal override int CompareKeys(int index1, int index2)
        {
            int c = comparer.Compare(keys[index1], keys[index2]);
            if (c == 0)
            {
                if (next == null) return index1 - index2;
                return next.CompareKeys(index1, index2);
            }
            return descending ? -c : c;
        }
    }

    internal class AsyncSelectorWithCancellationEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
    {
        readonly Func<TElement, CancellationToken, UniTask<TKey>> keySelector;
        readonly IComparer<TKey> comparer;
        readonly bool descending;
        readonly AsyncEnumerableSorter<TElement> next;
        CancellationToken cancellationToken;
        TKey[] keys;

        internal AsyncSelectorWithCancellationEnumerableSorter(Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TElement> next, CancellationToken cancellationToken)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
            this.cancellationToken = cancellationToken;
        }

        internal override async UniTask ComputeKeysAsync(TElement[] elements, int count)
        {
            keys = new TKey[count];
            for (int i = 0; i < count; i++) keys[i] = await keySelector(elements[i], cancellationToken);
            if (next != null) await next.ComputeKeysAsync(elements, count);
        }

        internal override int CompareKeys(int index1, int index2)
        {
            int c = comparer.Compare(keys[index1], keys[index2]);
            if (c == 0)
            {
                if (next == null) return index1 - index2;
                return next.CompareKeys(index1, index2);
            }
            return descending ? -c : c;
        }
    }

    internal abstract class OrderedAsyncEnumerable<TElement> : IUniTaskOrderedAsyncEnumerable<TElement>
    {
        protected readonly IUniTaskAsyncEnumerable<TElement> source;

        public OrderedAsyncEnumerable(IUniTaskAsyncEnumerable<TElement> source)
        {
            this.source = source;
        }

        public IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedAsyncEnumerable<TElement, TKey>(source, keySelector, comparer, descending, this);
        }

        public IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedAsyncEnumerableAwait<TElement, TKey>(source, keySelector, comparer, descending, this);
        }

        public IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedAsyncEnumerableAwaitWithCancellation<TElement, TKey>(source, keySelector, comparer, descending, this);
        }

        internal abstract AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(AsyncEnumerableSorter<TElement> next, CancellationToken cancellationToken);

        public IUniTaskAsyncEnumerator<TElement> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _OrderedAsyncEnumerator(this, cancellationToken);
        }

        class _OrderedAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<TElement>
        {
            protected readonly OrderedAsyncEnumerable<TElement> parent;
            CancellationToken cancellationToken;
            TElement[] buffer;
            int[] map;
            int index;

            public _OrderedAsyncEnumerator(OrderedAsyncEnumerable<TElement> parent, CancellationToken cancellationToken)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TElement Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (map == null)
                {
                    completionSource.Reset();
                    CreateSortSource().Forget();
                    return new UniTask<bool>(this, completionSource.Version);
                }

                if (index < buffer.Length)
                {
                    Current = buffer[map[index++]];
                    return CompletedTasks.True;
                }
                else
                {
                    return CompletedTasks.False;
                }
            }

            async UniTaskVoid CreateSortSource()
            {
                try
                {
                    buffer = await parent.source.ToArrayAsync();
                    if (buffer.Length == 0)
                    {
                        completionSource.TrySetResult(false);
                        return;
                    }

                    var sorter = parent.GetAsyncEnumerableSorter(null, cancellationToken);
                    map = await sorter.SortAsync(buffer, buffer.Length);
                    sorter = null;

                    // set first value
                    Current = buffer[map[index++]];
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                completionSource.TrySetResult(true);
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return default;
            }
        }
    }

    internal class OrderedAsyncEnumerable<TElement, TKey> : OrderedAsyncEnumerable<TElement>
    {
        readonly Func<TElement, TKey> keySelector;
        readonly IComparer<TKey> comparer;
        readonly bool descending;
        readonly OrderedAsyncEnumerable<TElement> parent;

        public OrderedAsyncEnumerable(IUniTaskAsyncEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedAsyncEnumerable<TElement> parent)
            : base(source)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.parent = parent;
        }

        internal override AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(AsyncEnumerableSorter<TElement> next, CancellationToken cancellationToken)
        {
            AsyncEnumerableSorter<TElement> sorter = new SyncSelectorAsyncEnumerableSorter<TElement, TKey>(keySelector, comparer, descending, next);
            if (parent != null) sorter = parent.GetAsyncEnumerableSorter(sorter, cancellationToken);
            return sorter;
        }
    }

    internal class OrderedAsyncEnumerableAwait<TElement, TKey> : OrderedAsyncEnumerable<TElement>
    {
        readonly Func<TElement, UniTask<TKey>> keySelector;
        readonly IComparer<TKey> comparer;
        readonly bool descending;
        readonly OrderedAsyncEnumerable<TElement> parent;

        public OrderedAsyncEnumerableAwait(IUniTaskAsyncEnumerable<TElement> source, Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, OrderedAsyncEnumerable<TElement> parent)
            : base(source)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.parent = parent;
        }

        internal override AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(AsyncEnumerableSorter<TElement> next, CancellationToken cancellationToken)
        {
            AsyncEnumerableSorter<TElement> sorter = new AsyncSelectorEnumerableSorter<TElement, TKey>(keySelector, comparer, descending, next);
            if (parent != null) sorter = parent.GetAsyncEnumerableSorter(sorter, cancellationToken);
            return sorter;
        }
    }

    internal class OrderedAsyncEnumerableAwaitWithCancellation<TElement, TKey> : OrderedAsyncEnumerable<TElement>
    {
        readonly Func<TElement, CancellationToken, UniTask<TKey>> keySelector;
        readonly IComparer<TKey> comparer;
        readonly bool descending;
        readonly OrderedAsyncEnumerable<TElement> parent;

        public OrderedAsyncEnumerableAwaitWithCancellation(IUniTaskAsyncEnumerable<TElement> source, Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, OrderedAsyncEnumerable<TElement> parent)
            : base(source)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
            this.descending = descending;
            this.parent = parent;
        }

        internal override AsyncEnumerableSorter<TElement> GetAsyncEnumerableSorter(AsyncEnumerableSorter<TElement> next, CancellationToken cancellationToken)
        {
            AsyncEnumerableSorter<TElement> sorter = new AsyncSelectorWithCancellationEnumerableSorter<TElement, TKey>(keySelector, comparer, descending, next, cancellationToken);
            if (parent != null) sorter = parent.GetAsyncEnumerableSorter(sorter, cancellationToken);
            return sorter;
        }
    }
}