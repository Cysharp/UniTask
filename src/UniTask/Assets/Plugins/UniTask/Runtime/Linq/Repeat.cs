using Cysharp.Threading.Tasks.Internal;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TElement> Repeat<TElement>(TElement element, int count)
        {
            if (count < 0) throw Error.ArgumentOutOfRange(nameof(count));

            return new Repeat<TElement>(element, count);
        }
    }

    internal class Repeat<TElement> : IUniTaskAsyncEnumerable<TElement>
    {
        readonly TElement element;
        readonly int count;

        public Repeat(TElement element, int count)
        {
            this.element = element;
            this.count = count;
        }

        public IUniTaskAsyncEnumerator<TElement> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Repeat(element, count, cancellationToken);
        }

        class _Repeat : IUniTaskAsyncEnumerator<TElement>
        {
            readonly TElement element;
            readonly int count;
            int remaining;
            CancellationToken cancellationToken;

            public _Repeat(TElement element, int count, CancellationToken cancellationToken)
            {
                this.element = element;
                this.count = count;
                this.cancellationToken = cancellationToken;

                this.remaining = count;
            }

            public TElement Current => element;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

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