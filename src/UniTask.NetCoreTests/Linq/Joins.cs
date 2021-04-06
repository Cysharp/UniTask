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

            xs.Should().Equal(ys);
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

            xs.Should().Equal(ys);
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

            xs.Should().Equal(ys);
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


        [Fact]
        public async Task GroupBy()
        {
            var arr = new[] { 1, 4, 10, 10, 4, 5, 10, 9 };
            {
                var xs = await arr.ToUniTaskAsyncEnumerable().GroupBy(x => x).ToArrayAsync();
                var ys = arr.GroupBy(x => x).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.Should().BeEquivalentTo(ys);
            }

            {
                var xs = await arr.ToUniTaskAsyncEnumerable().GroupBy(x => x, (key, xs) => (key, xs.ToArray())).ToArrayAsync();
                var ys = arr.GroupBy(x => x, (key, xs) => (key, xs.ToArray())).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.OrderBy(x => x.key).SelectMany(x => x.Item2).Should().Equal(ys.OrderBy(x => x.key).SelectMany(x => x.Item2));
            }

            {
                var xs = await arr.ToUniTaskAsyncEnumerable().GroupByAwait(x => RandomRun(x)).ToArrayAsync();
                var ys = arr.GroupBy(x => x).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.Should().BeEquivalentTo(ys);
            }

            {
                var xs = await arr.ToUniTaskAsyncEnumerable().GroupByAwait(x => RandomRun(x), (key, xs) => RandomRun((key, xs.ToArray()))).ToArrayAsync();
                var ys = arr.GroupBy(x => x, (key, xs) => (key, xs.ToArray())).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.OrderBy(x => x.key).SelectMany(x => x.Item2).Should().Equal(ys.OrderBy(x => x.key).SelectMany(x => x.Item2));
            }

            {
                var xs = await arr.ToUniTaskAsyncEnumerable().GroupByAwaitWithCancellation((x, _) => RandomRun(x)).ToArrayAsync();
                var ys = arr.GroupBy(x => x).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.Should().BeEquivalentTo(ys);
            }

            {
                var xs = await arr.ToUniTaskAsyncEnumerable().GroupByAwaitWithCancellation((x, _) => RandomRun(x), (key, xs, _) => RandomRun((key, xs.ToArray()))).ToArrayAsync();
                var ys = arr.GroupBy(x => x, (key, xs) => (key, xs.ToArray())).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.OrderBy(x => x.key).SelectMany(x => x.Item2).Should().Equal(ys.OrderBy(x => x.key).SelectMany(x => x.Item2));
            }
        }




        [Fact]
        public async Task GroupByThrow()
        {
            var arr = new[] { 1, 4, 10, 10, 4, 5, 10, 9 };
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.GroupBy(x => x).ToArrayAsync();
                var ys = item.GroupByAwait(x => RandomRun(x)).ToArrayAsync();
                var zs = item.GroupByAwaitWithCancellation((x, _) => RandomRun(x)).ToArrayAsync();

                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await zs);
            }
        }



        [Fact]
        public async Task GroupJoin()
        {
            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 };
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 };
            
            {
                var xs = await outer.ToUniTaskAsyncEnumerable().GroupJoin(inner.ToUniTaskAsyncEnumerable(), x => x, x => x, (x, y) => (x, string.Join(", ", y))).ToArrayAsync();
                var ys = outer.GroupJoin(inner, x => x, x => x, (x, y) => (x, string.Join(", ", y))).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.Should().Equal(ys);
            }
            {
                var xs = await outer.ToUniTaskAsyncEnumerable().GroupJoinAwait(inner.ToUniTaskAsyncEnumerable(), x => RandomRun(x), x => RandomRun(x), (x, y) => RandomRun((x, string.Join(", ", y)))).ToArrayAsync();
                var ys = outer.GroupJoin(inner, x => x, x => x, (x, y) => (x, string.Join(", ", y))).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.Should().Equal(ys);
            }
            {
                var xs = await outer.ToUniTaskAsyncEnumerable().GroupJoinAwaitWithCancellation(inner.ToUniTaskAsyncEnumerable(), (x, _) => RandomRun(x), (x, _) => RandomRun(x), (x, y, _) => RandomRun((x, string.Join(", ", y)))).ToArrayAsync();
                var ys = outer.GroupJoin(inner, x => x, x => x, (x, y) => (x, string.Join(", ", y))).ToArray();

                xs.Length.Should().Be(ys.Length);
                xs.Should().Equal(ys);
            }
        }


        [Fact]
        public async Task GroupJoinThrow()
        {

            var outer = new[] { 1, 2, 4, 5, 8, 10, 14, 4, 8, 1, 2, 10 }.ToUniTaskAsyncEnumerable();
            var inner = new[] { 1, 2, 1, 2, 1, 14, 2 }.ToUniTaskAsyncEnumerable();

            foreach (var item in UniTaskTestException.Throws())
            {
                {
                    var xs = item.GroupJoin(outer, x => x, x => x, (x, y) => x).ToArrayAsync();
                    var ys = inner.GroupJoin(item, x => x, x => x, (x, y) => x).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
                }
                {
                    var xs = item.GroupJoinAwait(outer, x => RandomRun(x), x => RandomRun(x), (x, y) => RandomRun(x)).ToArrayAsync();
                    var ys = inner.GroupJoinAwait(item, x => RandomRun(x), x => RandomRun(x), (x, y) => RandomRun(x)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
                }
                {
                    var xs = item.GroupJoinAwaitWithCancellation(outer, (x, _) => RandomRun(x), (x, _) => RandomRun(x), (x, y, _) => RandomRun(x)).ToArrayAsync();
                    var ys = inner.GroupJoinAwaitWithCancellation(item, (x, _) => RandomRun(x), (x, _) => RandomRun(x), (x, y, _) => RandomRun(x)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await ys);
                }
            }
        }


    }
}
