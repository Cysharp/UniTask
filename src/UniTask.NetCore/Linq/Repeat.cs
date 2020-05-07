﻿using Cysharp.Threading.Tasks.Internal;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0) throw Error.ArgumentOutOfRange(nameof(count));

            return new Repeat<TResult>(element, count);
        }
    }

    internal class Repeat<TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly TResult element;
        readonly int count;

        public Repeat(TResult element, int count)
        {
            this.element = element;
            this.count = count;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return new Enumerator(element, count, cancellationToken);
        }

        class Enumerator : IUniTaskAsyncEnumerator<TResult>
        {
            readonly TResult element;
            readonly int count;
            int remaining;
            CancellationToken cancellationToken;

            public Enumerator(TResult element, int count, CancellationToken cancellationToken)
            {
                this.element = element;
                this.count = count;
                this.cancellationToken = cancellationToken;

                this.remaining = count;
            }

            public TResult Current => element;

            public UniTask<bool> MoveNextAsync()
            {
                if (cancellationToken.IsCancellationRequested) return CompletedTasks.False;

                if (remaining-- != 0)
                {
                    return CompletedTasks.True;
                }

                return CompletedTasks.False;
            }

            public UniTask DisposeAsync()
            {
                return default;
            }
        }
    }
}