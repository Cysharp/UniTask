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
    public class Projection
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 10)]
        public async Task Reverse(int start, int count)
        {
            var xs = await Enumerable.Range(start, count).ToUniTaskAsyncEnumerable().Reverse().ToArrayAsync();
            var ys = Enumerable.Range(start, count).Reverse().ToArray();

            xs.Should().BeEquivalentTo(ys);
        }

        [Fact]
        public async Task ReverseException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Reverse().ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(9)]
        public async Task Select(int count)
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, count).Select(x => x * x).ToArrayAsync();
                var ys = Enumerable.Range(1, count).Select(x => x * x).ToArray();
                xs.Should().BeEquivalentTo(ys);

                var zs = await UniTaskAsyncEnumerable.Range(1, count).SelectAwait((x) => UniTask.Run(() => x * x)).ToArrayAsync();
                zs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, count).Select((x, i) => x * x * i).ToArrayAsync();
                var ys = Enumerable.Range(1, count).Select((x, i) => x * x * i).ToArray();
                xs.Should().BeEquivalentTo(ys);

                var zs = await UniTaskAsyncEnumerable.Range(1, count).SelectAwait((x, i) => UniTask.Run(() => x * x * i)).ToArrayAsync();
                zs.Should().BeEquivalentTo(ys);
            }
        }


        [Fact]
        public async Task SelectException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Select(x => UniTaskAsyncEnumerable.Range(0, 1)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }

            // await

            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SelectAwait(x => UniTask.Run(() => UniTaskAsyncEnumerable.Range(0, 1))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }

            // cancel

            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SelectAwaitWithCancellation((x, _) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(0, 1))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }


        [Theory]
        [InlineData(0, 9)] // empty + exists
        [InlineData(9, 0)] // exists + empty
        [InlineData(9, 9)] // exists + exists
        public async Task SelectMany(int leftCount, int rightCount)
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectMany(x => UniTaskAsyncEnumerable.Range(99, rightCount * x)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany(x => Enumerable.Range(99, rightCount * x)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectMany((i, x) => UniTaskAsyncEnumerable.Range(99 * i, rightCount * x)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany((i, x) => Enumerable.Range(99 * i, rightCount * x)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectMany(x => UniTaskAsyncEnumerable.Range(99, rightCount * x), (x, y) => x * y).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany(x => Enumerable.Range(99, rightCount * x), (x, y) => x * y).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectMany((i, x) => UniTaskAsyncEnumerable.Range(99 * i, rightCount * x), (x, y) => x * y).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany((i, x) => Enumerable.Range(99 * i, rightCount * x), (x, y) => x * y).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }

            // await

            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwait(x => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99, rightCount * x))).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany(x => Enumerable.Range(99, rightCount * x)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwait((i, x) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99 * i, rightCount * x))).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany((i, x) => Enumerable.Range(99 * i, rightCount * x)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwait(x => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99, rightCount * x)), (x, y) => UniTask.Run(() => x * y)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany(x => Enumerable.Range(99, rightCount * x), (x, y) => x * y).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwait((i, x) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99 * i, rightCount * x)), (x, y) => UniTask.Run(() => x * y)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany((i, x) => Enumerable.Range(99 * i, rightCount * x), (x, y) => x * y).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }

            // with cancel

            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwaitWithCancellation((x, _) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99, rightCount * x))).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany(x => Enumerable.Range(99, rightCount * x)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwaitWithCancellation((i, x, _) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99 * i, rightCount * x))).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany((i, x) => Enumerable.Range(99 * i, rightCount * x)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwaitWithCancellation((x, _) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99, rightCount * x)), (x, y, _) => UniTask.Run(() => x * y)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany(x => Enumerable.Range(99, rightCount * x), (x, y) => x * y).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).SelectManyAwaitWithCancellation((i, x, _) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(99 * i, rightCount * x)), (x, y, _) => UniTask.Run(() => x * y)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).SelectMany((i, x) => Enumerable.Range(99 * i, rightCount * x), (x, y) => x * y).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
        }

        [Fact]
        public async Task SelectManyException()
        {
            // error + exists
            // exists + error
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SelectMany(x => UniTaskAsyncEnumerable.Range(0, 1)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Range(0, 1).SelectMany(x => item).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }

            // await

            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SelectManyAwait(x => UniTask.Run(() => UniTaskAsyncEnumerable.Range(0, 1))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Range(0, 1).SelectManyAwait(x => UniTask.Run(() => item)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }

            // with c

            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.SelectManyAwaitWithCancellation((x, _) => UniTask.Run(() => UniTaskAsyncEnumerable.Range(0, 1))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Range(0, 1).SelectManyAwaitWithCancellation((x, _) => UniTask.Run(() => item)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }



        [Theory]
        [InlineData(0, 9)] // empty + exists
        [InlineData(9, 0)] // exists + empty
        [InlineData(9, 9)] // same
        [InlineData(9, 4)] // leftlong
        [InlineData(4, 9)] // rightlong
        public async Task Zip(int leftCount, int rightCount)
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).Zip(UniTaskAsyncEnumerable.Range(99, rightCount)).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).Zip(Enumerable.Range(99, rightCount)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).ZipAwait(UniTaskAsyncEnumerable.Range(99, rightCount), (x, y) => UniTask.Run(() => (x, y))).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).Zip(Enumerable.Range(99, rightCount)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, leftCount).ZipAwaitWithCancellation(UniTaskAsyncEnumerable.Range(99, rightCount), (x, y, _) => UniTask.Run(() => (x, y))).ToArrayAsync();
                var ys = Enumerable.Range(1, leftCount).Zip(Enumerable.Range(99, rightCount)).ToArray();
                xs.Should().BeEquivalentTo(ys);
            }
        }
        [Fact]
        public async Task ZipException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Zip(UniTaskAsyncEnumerable.Range(1, 10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Range(1, 10).Zip(item).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }

            // a

            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.ZipAwait(UniTaskAsyncEnumerable.Range(1, 10), (x, y) => UniTask.Run(() => (x, y))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Range(1, 10).ZipAwait(item, (x, y) => UniTask.Run(() => (x, y))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }

            // c

            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.ZipAwaitWithCancellation(UniTaskAsyncEnumerable.Range(1, 10), (x, y, c) => UniTask.Run(() => (x, y))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Range(1, 10).ZipAwaitWithCancellation(item, (x, y, c) => UniTask.Run(() => (x, y))).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }
    }
}
