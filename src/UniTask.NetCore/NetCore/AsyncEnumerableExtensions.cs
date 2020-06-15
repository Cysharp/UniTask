#if !NETSTANDARD2_0

#pragma warning disable 0649

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
    public static class AsyncEnumerableExtensions
    {
        public static IUniTaskAsyncEnumerable<T> AsUniTaskAsyncEnumerable<T>(this IAsyncEnumerable<T> source)
        {
            return new AsyncEnumerableToUniTaskAsyncEnumerable<T>(source);
        }

        public static IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IUniTaskAsyncEnumerable<T> source)
        {
            return new UniTaskAsyncEnumerableToAsyncEnumerable<T>(source);
        }

        sealed class AsyncEnumerableToUniTaskAsyncEnumerable<T> : IUniTaskAsyncEnumerable<T>
        {
            readonly IAsyncEnumerable<T> source;

            public AsyncEnumerableToUniTaskAsyncEnumerable(IAsyncEnumerable<T> source)
            {
                this.source = source;
            }

            public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new Enumerator(source.GetAsyncEnumerator(cancellationToken));
            }

            sealed class Enumerator : IUniTaskAsyncEnumerator<T>
            {
                readonly IAsyncEnumerator<T> enumerator;

                public Enumerator(IAsyncEnumerator<T> enumerator)
                {
                    this.enumerator = enumerator;
                }

                public T Current => enumerator.Current;

                public async UniTask DisposeAsync()
                {
                    await enumerator.DisposeAsync();
                }

                public async UniTask<bool> MoveNextAsync()
                {
                    return await enumerator.MoveNextAsync();
                }
            }
        }

        sealed class UniTaskAsyncEnumerableToAsyncEnumerable<T> : IAsyncEnumerable<T>
        {
            readonly IUniTaskAsyncEnumerable<T> source;

            public UniTaskAsyncEnumerableToAsyncEnumerable(IUniTaskAsyncEnumerable<T> source)
            {
                this.source = source;
            }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new Enumerator(source.GetAsyncEnumerator(cancellationToken));
            }

            sealed class Enumerator : IAsyncEnumerator<T>
            {
                readonly IUniTaskAsyncEnumerator<T> enumerator;

                public Enumerator(IUniTaskAsyncEnumerator<T> enumerator)
                {
                    this.enumerator = enumerator;
                }

                public T Current => enumerator.Current;

                public ValueTask DisposeAsync()
                {
                    return enumerator.DisposeAsync();
                }

                public ValueTask<bool> MoveNextAsync()
                {
                    return enumerator.MoveNextAsync();
                }
            }
        }
    }
}

#endif