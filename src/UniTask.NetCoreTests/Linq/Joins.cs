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
    public class Joins
    {
        static int rd;

        static UniTask<T> RandomRun<T>(T value)
        {
            if (Interlocked.Increment(ref rd) % 2 == 0)
            {
                return UniTask.Run(() => value);
            }
            else
            {
                return UniTask.FromResult(value);
            }
        }

        [Fact]
        public async Task Join()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };

            var xs = await outer.ToUniTaskAsyncEnumerable().Join(inner.ToUniTaskAsyncEnumerable(), x => x, x => x, (x, y) => (x, y)).ToArrayAsync();
            var ys = outer.Join(inner, x => x, x => x, (x, y) => (x, y)).ToArray();

            xs.Should().BeEquivalentTo(ys);
        }


        [Fact]
        public async Task JoinThrow()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = outer.ToUniTaskAsyncEnumerable().Join(item, x => x, x => x, (x, y) => x + y).ToArrayAsync();
                var ys = item.Join(inner.ToUniTaskAsyncEnumerable(), x => x, x => x, (x, y) => x + y).ToArrayAsync();

                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
            }
        }

        [Fact]
        public async Task JoinAwait()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };

            var xs = await outer.ToUniTaskAsyncEnumerable().JoinAwait(inner.ToUniTaskAsyncEnumerable(), x => RandomRun(x), x => RandomRun(x), (x, y) => RandomRun((x, y))).ToArrayAsync();
            var ys = outer.Join(inner, x => x, x => x, (x, y) => (x, y)).ToArray();

            xs.Should().BeEquivalentTo(ys);
        }


        [Fact]
        public async Task JoinAwaitThrow()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = outer.ToUniTaskAsyncEnumerable().JoinAwait(item, x => RandomRun(x), x => RandomRun(x), (x, y) => RandomRun(x + y)).ToArrayAsync();
                var ys = item.Join(inner.ToUniTaskAsyncEnumerable(), x => x, x => x, (x, y) => x + y).ToArrayAsync();

                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
            }
        }

        [Fact]
        public async Task JoinAwaitCt()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };

            var xs = await outer.ToUniTaskAsyncEnumerable().JoinAwaitWithCancellation(inner.ToUniTaskAsyncEnumerable(), (x, _) => RandomRun(x), (x, _) => RandomRun(x), (x, y, _) => RandomRun((x, y))).ToArrayAsync();
            var ys = outer.Join(inner, x => x, x => x, (x, y) => (x, y)).ToArray();

            xs.Should().BeEquivalentTo(ys);
        }


        [Fact]
        public async Task JoinAwaitCtThrow()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = outer.ToUniTaskAsyncEnumerable().JoinAwaitWithCancellation(item, (x, _) => RandomRun(x), (x, _) => RandomRun(x), (x, y, _) => RandomRun(x + y)).ToArrayAsync();
                var ys = item.JoinAwaitWithCancellation(inner.ToUniTaskAsyncEnumerable(), (x, _) => RandomRun(x), (x, _) => RandomRun(x), (x, y, _) => RandomRun(x + y)).ToArrayAsync();

                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
            }
        }
    }
}
