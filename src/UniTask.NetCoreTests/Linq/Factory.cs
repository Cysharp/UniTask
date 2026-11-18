using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests.Linq
{
    public class Factory
    {
        [Theory]
        [InlineData(0, 10)]
        [InlineData(0, 0)]
        [InlineData(1, 5)]
        [InlineData(1, 0)]
        [InlineData(0, 11)]
        [InlineData(1, 11)]
        public async Task RangeTest(int start, int count)
        {
            var xs = await UniTaskAsyncEnumerable.Range(start, count).ToArrayAsync();
            var ys = Enumerable.Range(start, count).ToArray();

            xs.Should().Equal(ys);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(-2, 3)]
        [InlineData(int.MinValue, 2)]
        [InlineData(int.MaxValue, 1)]
        [InlineData(int.MaxValue - 1, 2)]
        public async Task RangeStaysExhausted(int start, int count)
        {
            await using var enumerator = UniTaskAsyncEnumerable.Range(start, count).GetAsyncEnumerator();
            foreach (var expected in Enumerable.Range(start, count))
            {
                (await enumerator.MoveNextAsync()).Should().BeTrue();
                enumerator.Current.Should().Be(expected);
            }

            (await enumerator.MoveNextAsync()).Should().BeFalse();
            (await enumerator.MoveNextAsync()).Should().BeFalse();
        }

        [Theory]
        [InlineData("foo", 0)]
        [InlineData("bar", 1)]
        [InlineData("baz", 3)]
        [InlineData("foobar", 10)]
        [InlineData("foobarbaz", 11)]
        public async Task RepeatTest(string value, int count)
        {
            var xs = await UniTaskAsyncEnumerable.Repeat(value, count).ToArrayAsync();
            var ys = Enumerable.Repeat(value, count).ToArray();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task EmptyTest()
        {
            var xs = await UniTaskAsyncEnumerable.Empty<int>().ToArrayAsync();
            var ys = Enumerable.Empty<int>().ToArray();

            xs.Should().Equal(ys);
        }

        [Theory]
        [InlineData(100)]
        [InlineData((string)null)]
        [InlineData("foo")]
        public async Task ReturnTest<T>(T value)
        {
            var xs = await UniTaskAsyncEnumerable.Return(value).ToArrayAsync();

            xs.Length.Should().Be(1);
            xs[0].Should().Be(value);
        }

    }


}
