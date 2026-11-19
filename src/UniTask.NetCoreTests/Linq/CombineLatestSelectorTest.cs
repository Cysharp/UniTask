using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Xunit;

namespace NetCoreTests.Linq
{
    public class CombineLatestSelectorTest
    {
        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task MoveNextFaultsWhenResultSelectorThrows(bool threeInputs, bool finalInputPending)
        {
            var error = new InvalidOperationException("The result selector failed.");
            var ready = UniTaskAsyncEnumerable.Return(1);
            var pending = new PendingItem();
            var last = finalInputPending ? (IUniTaskAsyncEnumerable<int>)pending : ready;
            var sequence = threeInputs
                ? ready.CombineLatest<int, int, int, int>(last, ready, (_, _, _) => throw error)
                : ready.CombineLatest<int, int, int>(last, (_, _) => throw error);

            await using var enumerator = sequence.GetAsyncEnumerator();
            var moveNext = enumerator.MoveNextAsync();
            if (finalInputPending)
            {
                Assert.Equal(UniTaskStatus.Pending, moveNext.Status);
                Assert.True(pending.Completion.TrySetResult(true));
            }

            Assert.Equal(UniTaskStatus.Faulted, moveNext.Status);
            Assert.Same(error, await Assert.ThrowsAsync<InvalidOperationException>(async () => await moveNext));
        }

        [Fact]
        public async Task EarlierItemRemainsReadableWhenLaterSelectionThrows()
        {
            var error = new InvalidOperationException("The result selector failed.");
            var inputItems = 0;
            var left = UniTaskAsyncEnumerable.Range(1, 3).Do(_ => inputItems++);
            var right = UniTaskAsyncEnumerable.Return(10);
            await using var enumerator = left.CombineLatest(right, (a, b) =>
                a == 2 ? throw error : a + b).GetAsyncEnumerator();

            Assert.True(await enumerator.MoveNextAsync());
            Assert.Equal(11, enumerator.Current);
            Assert.Equal(2, inputItems);
            Assert.Same(error, await Assert.ThrowsAsync<InvalidOperationException>(async () => await enumerator.MoveNextAsync()));
        }

        sealed class PendingItem : IUniTaskAsyncEnumerable<int>, IUniTaskAsyncEnumerator<int>
        {
            public readonly UniTaskCompletionSource<bool> Completion = new UniTaskCompletionSource<bool>();

            public int Current => 2;

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
