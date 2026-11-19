using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Xunit;

namespace NetCoreTests.Linq
{
    public class MergeExhaustionTest
    {
        [Fact]
        public async Task MergeCompletesAfterAnArrayInputIsExhausted()
        {
            var source = new[] { 7 }.ToUniTaskAsyncEnumerable();
            var result = await UniTaskAsyncEnumerable.Empty<int>().Merge(source).ToArrayAsync();

            Assert.Equal(new[] { 7 }, result);
        }

        [Fact]
        public async Task MergeCompletesWhenTheLastInputFinishesAsynchronously()
        {
            var source = new PendingEmptySource();
            await using var enumerator = UniTaskAsyncEnumerable.Empty<int>().Merge(source).GetAsyncEnumerator();
            var moveNext = enumerator.MoveNextAsync();
            Assert.Equal(UniTaskStatus.Pending, moveNext.Status);

            Assert.True(source.Completion.TrySetResult(false));
            Assert.Equal(UniTaskStatus.Succeeded, moveNext.Status);
            Assert.False(await moveNext);
        }

        sealed class PendingEmptySource : IUniTaskAsyncEnumerable<int>, IUniTaskAsyncEnumerator<int>
        {
            public readonly UniTaskCompletionSource<bool> Completion = new UniTaskCompletionSource<bool>();

            public int Current => throw new InvalidOperationException("The input has no current item.");

            public IUniTaskAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return this;
            }

            public UniTask<bool> MoveNextAsync()
            {
                return Completion.Task;
            }

            public UniTask DisposeAsync()
            {
                return default;
            }
        }
    }
}
