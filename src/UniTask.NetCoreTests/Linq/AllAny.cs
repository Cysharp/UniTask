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
    public class AllAny
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(0, 10)]
        [InlineData(0, 11)]
        public async Task AllTest(int start, int count)
        {
            var range = Enumerable.Range(start, count);
            var x = await range.ToUniTaskAsyncEnumerable().AllAsync(x => x % 2 == 0);
            var y = range.All(x => x % 2 == 0);

            x.Should().Be(y);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(0, 10)]
        [InlineData(0, 11)]
        public async Task AnyTest(int start, int count)
        {
            var range = Enumerable.Range(start, count);
            {
                var x = await range.ToUniTaskAsyncEnumerable().AnyAsync();
                var y = range.Any();

                x.Should().Be(y);
            }
            {
                var x = await range.ToUniTaskAsyncEnumerable().AnyAsync(x => x % 2 == 0);
                var y = range.Any(x => x % 2 == 0);

                x.Should().Be(y);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(0, 10)]
        [InlineData(0, 11)]
        public async Task ContainsTest(int start, int count)
        {
            var range = Enumerable.Range(start, count);
            foreach (var c in Enumerable.Range(0, 15))
            {
                var x = await range.ToUniTaskAsyncEnumerable().ContainsAsync(c);
                var y = range.Contains(c);
                x.Should().Be(y);
            }
        }

        [Fact]
        public async Task SequenceEqual()
        {
            // empty and empty
            (await new int[0].ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[0].ToUniTaskAsyncEnumerable())).Should().BeTrue();
            (new int[0].SequenceEqual(new int[0])).Should().BeTrue();

            // empty and exists
            (await new int[0].ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1 }.ToUniTaskAsyncEnumerable())).Should().BeFalse();
            (new int[0].SequenceEqual(new int[] { 1 })).Should().BeFalse();

            // exists and empty
            (await new int[] { 1 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[0].ToUniTaskAsyncEnumerable())).Should().BeFalse();
            (new int[] { 1 }.SequenceEqual(new int[] { })).Should().BeFalse();

            // samelength same value
            (await new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable())).Should().BeTrue();
            (new int[] { 1, 2, 3 }.SequenceEqual(new int[] { 1, 2, 3 })).Should().BeTrue();

            // samelength different value(first)
            (await new int[] { 5, 2, 3 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable())).Should().BeFalse();

            // samelength different value(middle)
            (await new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1, 5, 3 }.ToUniTaskAsyncEnumerable())).Should().BeFalse();

            // samelength different value(last)
            (await new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1, 2, 5 }.ToUniTaskAsyncEnumerable())).Should().BeFalse();

            // left is long
            (await new int[] { 1, 2, 3, 4 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable())).Should().BeFalse();
            (new int[] { 1, 2, 3, 4 }.SequenceEqual(new int[] { 1, 2, 3 })).Should().BeFalse();

            // right is long
            (await new int[] { 1, 2, 3 }.ToUniTaskAsyncEnumerable().SequenceEqualAsync(new int[] { 1, 2, 3, 4 }.ToUniTaskAsyncEnumerable())).Should().BeFalse();
            (new int[] { 1, 2, 3 }.SequenceEqual(new int[] { 1, 2, 3, 4 })).Should().BeFalse();
        }
    }
}
