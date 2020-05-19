using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public interface IUniTaskAsyncEnumerable<out T>
    {
        IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    }

    public interface IUniTaskAsyncEnumerator<out T> : IUniTaskAsyncDisposable
    {
        T Current { get; }
        UniTask<bool> MoveNextAsync();
    }

    public interface IUniTaskAsyncDisposable
    {
        UniTask DisposeAsync();
    }

    public interface IUniTaskOrderedAsyncEnumerable<TElement> : IUniTaskAsyncEnumerable<TElement>
    {
        IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
        IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending);
        IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending);
    }

    public interface IConnectableUniTaskAsyncEnumerable<out T> : IUniTaskAsyncEnumerable<T>
    {
        IDisposable Connect();
    }

    // don't use AsyncGrouping.
    //public interface IUniTaskAsyncGrouping<out TKey, out TElement> : IUniTaskAsyncEnumerable<TElement>
    //{
    //    TKey Key { get; }
    //}

    public static class UniTaskAsyncEnumerableExtensions
    {
        public static UniTaskCancelableAsyncEnumerable<T> WithCancellation<T>(this IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
        {
            return new UniTaskCancelableAsyncEnumerable<T>(source, cancellationToken);
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public readonly struct UniTaskCancelableAsyncEnumerable<T>
    {
        private readonly IUniTaskAsyncEnumerable<T> enumerable;
        private readonly CancellationToken cancellationToken;

        internal UniTaskCancelableAsyncEnumerable(IUniTaskAsyncEnumerable<T> enumerable, CancellationToken cancellationToken)
        {
            this.enumerable = enumerable;
            this.cancellationToken = cancellationToken;
        }

        public Enumerator GetAsyncEnumerator()
        {
            return new Enumerator(enumerable.GetAsyncEnumerator(cancellationToken));
        }

        [StructLayout(LayoutKind.Auto)]
        public readonly struct Enumerator
        {
            private readonly IUniTaskAsyncEnumerator<T> enumerator;

            internal Enumerator(IUniTaskAsyncEnumerator<T> enumerator)
            {
                this.enumerator = enumerator;
            }

            public T Current => enumerator.Current;

            public UniTask<bool> MoveNextAsync()
            {
                return enumerator.MoveNextAsync();
            }


            public UniTask DisposeAsync()
            {
                return enumerator.DisposeAsync();
            }
        }
    }
}