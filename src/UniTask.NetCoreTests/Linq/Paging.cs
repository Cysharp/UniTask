using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;


namespace NetCoreTests.Linq
{
    public class Paging
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(9, 0)]
        [InlineData(9, 1)]
        [InlineData(9, 5)]
        [InlineData(9, 9)]
        [InlineData(9, 15)]
        public async Task Skip(int collection, int skipCount)
        {
            var xs = await UniTaskAsyncEnumerable.Range(1, collection).Skip(skipCount).ToArrayAsync();
            var ys = Enumerable.Range(1, collection).Skip(skipCount).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task SkipException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Skip(5).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(9, 0)]
        [InlineData(9, 1)]
        [InlineData(9, 5)]
        [InlineData(9, 9)]
        [InlineData(9, 15)]
        public async Task SkipLast(int collection, int skipCount)
        {
            var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipLast(skipCount).ToArrayAsync();
            var ys = Enumerable.Range(1, collection).SkipLast(skipCount).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task SkipLastException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipLast(5).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(9, 0)]
        [InlineData(9, 1)]
        [InlineData(9, 5)]
        [InlineData(9, 9)]
        [InlineData(9, 15)]
        public async Task TakeLast(int collection, int takeCount)
        {
            var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeLast(takeCount).ToArrayAsync();
            var ys = Enumerable.Range(1, collection).TakeLast(takeCount).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task TakeLastException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeLast(5).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(9, 0)]
        [InlineData(9, 1)]
        [InlineData(9, 5)]
        [InlineData(9, 9)]
        [InlineData(9, 15)]
        public async Task Take(int collection, int takeCount)
        {
            var xs = await UniTaskAsyncEnumerable.Range(1, collection).Take(takeCount).ToArrayAsync();
            var ys = Enumerable.Range(1, collection).Take(takeCount).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task TakeException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Take(5).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(9, 0)]
        [InlineData(9, 1)]
        [InlineData(9, 5)]
        [InlineData(9, 9)]
        [InlineData(9, 15)]
        public async Task SkipWhile(int collection, int skipCount)
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipWhile(x => x < skipCount).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).SkipWhile(x => x < skipCount).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipWhile((x, i) => x < (skipCount - i)).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).SkipWhile((x, i) => x < (skipCount - i)).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipWhileAwait(x => UniTask.Run(() => x < skipCount)).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).SkipWhile(x => x < skipCount).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipWhileAwait((x, i) => UniTask.Run(() => x < (skipCount - i))).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).SkipWhile((x, i) => x < (skipCount - i)).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipWhileAwaitWithCancellation((x, _) => UniTask.Run(() => x < skipCount)).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).SkipWhile(x => x < skipCount).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).SkipWhileAwaitWithCancellation((x, i, _) => UniTask.Run(() => x < (skipCount - i))).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).SkipWhile((x, i) => x < (skipCount - i)).ToArray();

                xs.Should().Equal(ys);
            }
        }

        [Fact]
        public async Task SkipWhileException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipWhile(x => x < 2).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipWhile((x, i) => x < 2).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipWhileAwait((x) => UniTask.Run(() => x < 2)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipWhileAwait((x, i) => UniTask.Run(() => x < 2)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipWhileAwaitWithCancellation((x, _) => UniTask.Run(() => x < 2)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SkipWhileAwaitWithCancellation((x, i, _) => UniTask.Run(() => x < 2)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(9, 0)]
        [InlineData(9, 1)]
        [InlineData(9, 5)]
        [InlineData(9, 9)]
        [InlineData(9, 15)]
        public async Task TakeWhile(int collection, int skipCount)
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeWhile(x => x < skipCount).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).TakeWhile(x => x < skipCount).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeWhile((x, i) => x < (skipCount - i)).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).TakeWhile((x, i) => x < (skipCount - i)).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeWhileAwait(x => UniTask.Run(() => x < skipCount)).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).TakeWhile(x => x < skipCount).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeWhileAwait((x, i) => UniTask.Run(() => x < (skipCount - i))).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).TakeWhile((x, i) => x < (skipCount - i)).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeWhileAwaitWithCancellation((x, _) => UniTask.Run(() => x < skipCount)).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).TakeWhile(x => x < skipCount).ToArray();

                xs.Should().Equal(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, collection).TakeWhileAwaitWithCancellation((x, i, _) => UniTask.Run(() => x < (skipCount - i))).ToArrayAsync();
                var ys = Enumerable.Range(1, collection).TakeWhile((x, i) => x < (skipCount - i)).ToArray();

                xs.Should().Equal(ys);
            }
        }

        [Fact]
        public async Task TakeWhileException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeWhile(x => x < 5).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeWhile((x, i) => x < 5).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeWhileAwait((x) => UniTask.Run(() => x < 5)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeWhileAwait((x, i) => UniTask.Run(() => x < 5)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeWhileAwaitWithCancellation((x, _) => UniTask.Run(() => x < 5)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.TakeWhileAwaitWithCancellation((x, i, _) => UniTask.Run(() => x < 5)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }
    }
}
