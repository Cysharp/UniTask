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
            var xs = await array.ToUniTaskAsyncEnumerable().Distinct().ToArrayAsync();
            var ys = array.Distinct().ToArray();

            xs.Should().BeEquivalentTo(ys);
        }

        [Fact]
        public async Task DistinctThrow()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Distinct().ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
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
                    xs.Should().BeEquivalentTo(ys);
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
                    xs.Should().BeEquivalentTo(ys);
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
                    xs.Should().BeEquivalentTo(ys);
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
