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
    public class Sets
    {
        public static IEnumerable<object[]> array1 = new object[][]
        {
            new object[] { new int[] { } }, // empty
            new object[] { new int[] { 1, 2, 3 } }, // no dup
            new object[] { new int[] { 1, 2, 3, 3, 4, 5, 2 } }, // dup
        };

        public static IEnumerable<object[]> array2 = new object[][]
        {
            new object[] { new int[] { } }, // empty
            new object[] { new int[] { 1, 2 } },
            new object[] { new int[] { 1, 2, 4, 5, 9 } }, // dup
        };

        [Theory]
        [MemberData(nameof(array1))]
        public async Task Distinct(int[] array)
        {
            var ys = array.Distinct().ToArray();
            {
                (await array.ToUniTaskAsyncEnumerable().Distinct().ToArrayAsync()).Should().Equal(ys);
                (await array.ToUniTaskAsyncEnumerable().Distinct(x => x).ToArrayAsync()).Should().Equal(ys);
                (await array.ToUniTaskAsyncEnumerable().DistinctAwait(x => UniTask.Run(() => x)).ToArrayAsync()).Should().Equal(ys);
                (await array.ToUniTaskAsyncEnumerable().DistinctAwaitWithCancellation((x, _) => UniTask.Run(() => x)).ToArrayAsync()).Should().Equal(ys);
            }
        }

        [Fact]
        public async Task DistinctThrow()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                {
                    var xs = item.Distinct().ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.Distinct(x => x).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.DistinctAwait(x => UniTask.Run(() => x)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.DistinctAwaitWithCancellation((x, _) => UniTask.Run(() => x)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
            }
        }

        [Theory]
        [MemberData(nameof(array1))]
        public async Task DistinctUntilChanged(int[] array)
        {
            var ys = await array.ToAsyncEnumerable().DistinctUntilChanged().ToArrayAsync();
            {
                (await array.ToUniTaskAsyncEnumerable().DistinctUntilChanged().ToArrayAsync()).Should().Equal(ys);
                (await array.ToUniTaskAsyncEnumerable().DistinctUntilChanged(x => x).ToArrayAsync()).Should().Equal(ys);
                (await array.ToUniTaskAsyncEnumerable().DistinctUntilChangedAwait(x => UniTask.Run(() => x)).ToArrayAsync()).Should().Equal(ys);
                (await array.ToUniTaskAsyncEnumerable().DistinctUntilChangedAwaitWithCancellation((x, _) => UniTask.Run(() => x)).ToArrayAsync()).Should().Equal(ys);
            }
        }

        [Fact]
        public async Task DistinctUntilChangedThrow()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                {
                    var xs = item.DistinctUntilChanged().ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.DistinctUntilChanged(x => x).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.DistinctUntilChangedAwait(x => UniTask.Run(() => x)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.DistinctUntilChangedAwaitWithCancellation((x, _) => UniTask.Run(() => x)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
            }
        }


        [Fact]
        public async Task Except()
        {
            foreach (var a1 in array1.First().Cast<int[]>())
            {
                foreach (var a2 in array2.First().Cast<int[]>())
                {
                    var xs = await a1.ToUniTaskAsyncEnumerable().Except(a2.ToUniTaskAsyncEnumerable()).ToArrayAsync();
                    var ys = a1.Except(a2).ToArray();
                    xs.Should().Equal(ys);
                }
            }
        }

        [Fact]
        public async Task ExceptThrow()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Except(UniTaskAsyncEnumerable.Return(10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Return(10).Except(item).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Fact]
        public async Task Intersect()
        {
            foreach (var a1 in array1.First().Cast<int[]>())
            {
                foreach (var a2 in array2.First().Cast<int[]>())
                {
                    var xs = await a1.ToUniTaskAsyncEnumerable().Intersect(a2.ToUniTaskAsyncEnumerable()).ToArrayAsync();
                    var ys = a1.Intersect(a2).ToArray();
                    xs.Should().Equal(ys);
                }
            }
        }

        [Fact]
        public async Task IntersectThrow()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Intersect(UniTaskAsyncEnumerable.Return(10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Return(10).Intersect(item).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Fact]
        public async Task Union()
        {
            foreach (var a1 in array1.First().Cast<int[]>())
            {
                foreach (var a2 in array2.First().Cast<int[]>())
                {
                    var xs = await a1.ToUniTaskAsyncEnumerable().Union(a2.ToUniTaskAsyncEnumerable()).ToArrayAsync();
                    var ys = a1.Union(a2).ToArray();
                    xs.Should().Equal(ys);
                }
            }
        }

        [Fact]
        public async Task UnionThrow()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Union(UniTaskAsyncEnumerable.Return(10)).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = UniTaskAsyncEnumerable.Return(10).Union(item).ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }
    }
}
