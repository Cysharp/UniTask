using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace NetCoreTests.Linq
{
    public class Concat
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 10)]
        public async Task Append(int start, int count)
        {
            var xs = await Enumerable.Range(start, count).ToUniTaskAsyncEnumerable().Append(99).ToArrayAsync();
            var ys = Enumerable.Range(start, count).Append(99).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task AppendThrow()
        {
            var xs = UniTaskTestException.ThrowImmediate().Append(99).ToArrayAsync();
            await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);

            var ys = UniTaskTestException.ThrowAfter().Append(99).ToArrayAsync();
            await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);

            var zs = UniTaskTestException.ThrowInMoveNext().Append(99).ToArrayAsync();
            await Assert.ThrowsAsync<UniTaskTestException>(async () => await zs);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 10)]
        public async Task Prepend(int start, int count)
        {
            var xs = await Enumerable.Range(start, count).ToUniTaskAsyncEnumerable().Prepend(99).ToArrayAsync();
            var ys = Enumerable.Range(start, count).Prepend(99).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task PrependThrow()
        {
            var xs = UniTaskTestException.ThrowImmediate().Prepend(99).ToArrayAsync();
            await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);

            var ys = UniTaskTestException.ThrowAfter().Prepend(99).ToArrayAsync();
            await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);

            var zs = UniTaskTestException.ThrowInMoveNext().Prepend(99).ToArrayAsync();
            await Assert.ThrowsAsync<UniTaskTestException>(async () => await zs);
        }

        public static IEnumerable<object[]> array1 = new object[][]
        {
            new object[] { (0, 0), (0, 0) }, // empty + empty
            new object[] { (0, 1), (0, 0) }, // 1 + empty
            new object[] { (0, 0), (0, 1) }, // empty + 1
            new object[] { (0, 5), (0, 0) }, // 5 + empty
            new object[] { (0, 0), (0, 5) }, // empty + 5
            new object[] { (0, 5), (0, 5) }, // 5 + 5
        };

        [Theory]
        [MemberData(nameof(array1))]
        public async Task ConcatTest((int, int) left, (int, int) right)
        {
            var l = Enumerable.Range(left.Item1, left.Item2);
            var r = Enumerable.Range(right.Item1, right.Item2);

            var xs = await l.ToUniTaskAsyncEnumerable().Concat(r.ToUniTaskAsyncEnumerable()).ToArrayAsync();
            var ys = l.Concat(r).ToArray();
            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task ConcatThrow()
        {
            {
                var xs = UniTaskTestException.ThrowImmediate().Concat(UniTaskAsyncEnumerable.Range(1, 10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);

                var ys = UniTaskTestException.ThrowAfter().Concat(UniTaskAsyncEnumerable.Range(1, 10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);

                var zs = UniTaskTestException.ThrowInMoveNext().Concat(UniTaskAsyncEnumerable.Range(1, 10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await zs);
            }
            {
                var xs = UniTaskAsyncEnumerable.Range(1, 10).Concat(UniTaskTestException.ThrowImmediate()).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);

                var ys = UniTaskAsyncEnumerable.Range(1, 10).Concat(UniTaskTestException.ThrowAfter()).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);

                var zs = UniTaskAsyncEnumerable.Range(1, 10).Concat(UniTaskTestException.ThrowInMoveNext()).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await zs);
            }
        }

        [Fact]
        public async Task ConcatPropagatesSecondAcquisitionFailure()
        {
            var failure = new UniTaskTestException();
            var first = new TrackingEmptySource();
            var second = new FailingAcquisitionSource(failure);
            await using var enumerator = first.Concat(second).GetAsyncEnumerator();

            var next = enumerator.MoveNextAsync();

            next.Status.Should().Be(UniTaskStatus.Faulted);
            var error = await Assert.ThrowsAsync<UniTaskTestException>(async () => await next);
            Assert.Same(failure, error);
            first.DisposeCount.Should().Be(1);
        }

        [Fact]
        public async Task ConcatDoesNotRetryFailedDisposal()
        {
            var failure = new UniTaskTestException();
            var first = new TrackingEmptySource(failure);

            var result = first.Concat(UniTaskAsyncEnumerable.Empty<int>()).ToArrayAsync();

            result.Status.Should().Be(UniTaskStatus.Faulted);
            var error = await Assert.ThrowsAsync<UniTaskTestException>(async () => await result);
            Assert.Same(failure, error);
            first.DisposeCount.Should().Be(1);
        }

        sealed class FailingAcquisitionSource : IUniTaskAsyncEnumerable<int>
        {
            readonly Exception error;

            public FailingAcquisitionSource(Exception error)
            {
                this.error = error;
            }

            public IUniTaskAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                throw error;
            }
        }

        sealed class TrackingEmptySource : IUniTaskAsyncEnumerable<int>, IUniTaskAsyncEnumerator<int>
        {
            readonly Exception disposeError;

            public TrackingEmptySource(Exception disposeError = null)
            {
                this.disposeError = disposeError;
            }

            public int DisposeCount { get; private set; }

            public int Current => default;

            public IUniTaskAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return this;
            }

            public UniTask<bool> MoveNextAsync()
            {
                return UniTask.FromResult(false);
            }

            public UniTask DisposeAsync()
            {
                DisposeCount++;
                return DisposeCount == 1 && disposeError != null ? UniTask.FromException(disposeError) : default;
            }
        }

        [Fact]
        public async Task DefaultIfEmpty()
        {
            {
                var xs = await Enumerable.Range(1, 0).ToUniTaskAsyncEnumerable().DefaultIfEmpty(99).ToArrayAsync();
                var ys = Enumerable.Range(1, 0).DefaultIfEmpty(99).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await Enumerable.Range(1, 1).ToUniTaskAsyncEnumerable().DefaultIfEmpty(99).ToArrayAsync();
                var ys = Enumerable.Range(1, 1).DefaultIfEmpty(99).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await Enumerable.Range(1, 10).ToUniTaskAsyncEnumerable().DefaultIfEmpty(99).ToArrayAsync();
                var ys = Enumerable.Range(1, 10).DefaultIfEmpty(99).ToArray();
                xs.Should().Equal(ys);
            }
            // Throw
            {
                foreach (var item in UniTaskTestException.Throws())
                {
                    var xs = item.DefaultIfEmpty().ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
            }
        }
    }
}
