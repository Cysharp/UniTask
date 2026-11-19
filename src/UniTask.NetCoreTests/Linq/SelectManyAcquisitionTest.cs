using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Xunit;

namespace NetCoreTests.Linq
{
    public class SelectManyAcquisitionTest
    {
        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task MoveNextFaultsWhenInnerEnumeratorAcquisitionThrows(
            bool withCancellation, bool selectorPending)
        {
            var error = new InvalidOperationException("Unable to acquire the inner enumerator.");
            var inner = new ThrowOnAcquisition(error);
            var selector = new UniTaskCompletionSource<IUniTaskAsyncEnumerable<int>>();
            if (!selectorPending)
            {
                Assert.True(selector.TrySetResult(inner));
            }

            var outer = UniTaskAsyncEnumerable.Return(1);
            var sequence = withCancellation
                ? outer.SelectManyAwaitWithCancellation((_, token) => selector.Task)
                : outer.SelectManyAwait(_ => selector.Task);

            await using var enumerator = sequence.GetAsyncEnumerator();
            var moveNext = enumerator.MoveNextAsync();
            if (selectorPending)
            {
                Assert.Equal(UniTaskStatus.Pending, moveNext.Status);
                Assert.True(selector.TrySetResult(inner));
            }

            Assert.Equal(UniTaskStatus.Faulted, moveNext.Status);
            Assert.Same(error, await Assert.ThrowsAsync<InvalidOperationException>(async () => await moveNext));
        }

        sealed class ThrowOnAcquisition : IUniTaskAsyncEnumerable<int>
        {
            readonly Exception error;

            public ThrowOnAcquisition(Exception error)
            {
                this.error = error;
            }

            public IUniTaskAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                throw error;
            }
        }
    }
}
