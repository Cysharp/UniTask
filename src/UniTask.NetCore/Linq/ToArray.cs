using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<TSource[]> ToArrayAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            return Cysharp.Threading.Tasks.Linq.ToArray<TSource>.InvokeAsync(source, cancellationToken);
        }
    }

    internal static class ToArray<TSource>
    {
        internal static async UniTask<TSource[]> InvokeAsync(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            TSource[] result = default;
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    result = Array.Empty<TSource>();
                }
                else
                {
                    result = new TSource[i];
                    Array.Copy(array, result, i);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }

            return result;
        }
    }
}