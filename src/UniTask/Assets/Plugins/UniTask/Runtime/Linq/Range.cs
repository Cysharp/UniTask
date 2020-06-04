using Cysharp.Threading.Tasks.Internal;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<int> Range(int start, int count)
        {
            if (count < 0) throw Error.ArgumentOutOfRange(nameof(count));

            var end = (long)start + count - 1L;
            if (end > int.MaxValue) throw Error.ArgumentOutOfRange(nameof(count));

            if (count == 0) UniTaskAsyncEnumerable.Empty<int>();

            return new Cysharp.Threading.Tasks.Linq.Range(start, count);
        }
    }

    internal class Range : IUniTaskAsyncEnumerable<int>
    {
        readonly int start;
        readonly int end;

        public Range(int start, int count)
        {
            this.start = start;
            this.end = start + count;
        }

        public IUniTaskAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Range(start, end, cancellationToken);
        }

        class _Range : IUniTaskAsyncEnumerator<int>
        {
            readonly int start;
            readonly int end;
            int current;
            CancellationToken cancellationToken;

            public _Range(int start, int end, CancellationToken cancellationToken)
            {
                this.start = start;
                this.end = end;
                this.cancellationToken = cancellationToken;

                this.current = start - 1;
            }

            public int Current => current;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                current++;

                if (current != end)
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