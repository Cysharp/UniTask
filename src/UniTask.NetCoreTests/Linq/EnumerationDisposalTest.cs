using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Xunit;

namespace NetCoreTests.Linq
{
    public class EnumerationDisposalTest
    {
        [Fact]
        public async Task DisposeBeforeFirstMoveDoesNotAcquireSource()
        {
            var source = new FailingSource();

            foreach (var sequence in ApplyOperators(source))
            {
                await sequence.GetAsyncEnumerator().DisposeAsync();
            }
        }

        [Fact]
        public async Task TerminalOperatorPreservesSourceAcquisitionError()
        {
            var source = new FailingSource();

            foreach (var sequence in ApplyOperators(source))
            {
                var error = await Assert.ThrowsAsync<UniTaskTestException>(async () => await sequence.ToArrayAsync());
                Assert.Same(source.Error, error);
            }
        }

        static IEnumerable<IUniTaskAsyncEnumerable<int>> ApplyOperators(IUniTaskAsyncEnumerable<int> source)
        {
            yield return source.Select(value => value);
            yield return source.Select((value, _) => value);
            yield return source.SelectAwait(value => UniTask.FromResult(value));
            yield return source.SelectAwait((value, _) => UniTask.FromResult(value));
            yield return source.SelectAwaitWithCancellation((value, _) => UniTask.FromResult(value));
            yield return source.SelectAwaitWithCancellation((value, _, _) => UniTask.FromResult(value));
            yield return source.Where(_ => true);
            yield return source.Where((_, _) => true);
            yield return source.WhereAwait(_ => UniTask.FromResult(true));
            yield return source.WhereAwait((_, _) => UniTask.FromResult(true));
            yield return source.WhereAwaitWithCancellation((_, _) => UniTask.FromResult(true));
            yield return source.WhereAwaitWithCancellation((_, _, _) => UniTask.FromResult(true));
            yield return source.DistinctUntilChanged();
            yield return source.DistinctUntilChanged(value => value);
            yield return source.DistinctUntilChangedAwait(value => UniTask.FromResult(value));
            yield return source.DistinctUntilChangedAwaitWithCancellation((value, _) => UniTask.FromResult(value));
        }

        sealed class FailingSource : IUniTaskAsyncEnumerable<int>
        {
            public UniTaskTestException Error { get; } = new UniTaskTestException();

            public IUniTaskAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                throw Error;
            }
        }
    }
}
