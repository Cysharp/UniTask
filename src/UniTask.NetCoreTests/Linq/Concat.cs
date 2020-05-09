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

            xs.Should().BeEquivalentTo(ys);
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

            xs.Should().BeEquivalentTo(ys);
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
            xs.Should().BeEquivalentTo(ys);
        }
    }
}
