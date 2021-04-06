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
    public class Filtering
    {
        [Fact]
        public async Task Where()
        {
            var range = Enumerable.Range(1, 10);
            var src = range.ToUniTaskAsyncEnumerable();

            {
                var a = await src.Where(x => x % 2 == 0).ToArrayAsync();
                var expected = range.Where(x => x % 2 == 0).ToArray();
                a.Should().Equal(expected);
            }
            {
                var a = await src.Where((x, i) => (x + i) % 2 == 0).ToArrayAsync();
                var expected = range.Where((x, i) => (x + i) % 2 == 0).ToArray();
                a.Should().Equal(expected);
            }
            {
                var a = await src.WhereAwait(x => UniTask.Run(() => x % 2 == 0)).ToArrayAsync();
                var b = await src.WhereAwait(x => UniTask.FromResult(x % 2 == 0)).ToArrayAsync();
                var expected = range.Where(x => x % 2 == 0).ToArray();
                a.Should().Equal(expected);
                b.Should().Equal(expected);
            }
            {
                var a = await src.WhereAwait((x, i) => UniTask.Run(() => (x + i) % 2 == 0)).ToArrayAsync();
                var b = await src.WhereAwait((x, i) => UniTask.FromResult((x + i) % 2 == 0)).ToArrayAsync();
                var expected = range.Where((x, i) => (x + i) % 2 == 0).ToArray();
                a.Should().Equal(expected);
                b.Should().Equal(expected);
            }
        }


        [Fact]
        public async Task WhereException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                {
                    var xs = item.Where(x => x % 2 == 0).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.Where((x, i) => x % 2 == 0).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.WhereAwait(x => UniTask.FromResult(x % 2 == 0)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
                {
                    var xs = item.WhereAwait((x, i) => UniTask.FromResult(x % 2 == 0)).ToArrayAsync();
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
                }
            }
        }

        [Fact]
        public async Task OfType()
        {
            var data = new object[] { 0, null, 10, 30, null, "foo", 99 };

            var a = await data.ToUniTaskAsyncEnumerable().OfType<int>().ToArrayAsync();
            var b = data.OfType<int>().ToArray();

            a.Should().Equal(b);
        }


        [Fact]
        public async Task OfTypeException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Select(x => (object)x).OfType<int>().ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }

        [Fact]
        public async Task Cast()
        {
            var data = new object[] { 0, 10, 30, 99 };

            var a = await data.ToUniTaskAsyncEnumerable().Cast<int>().ToArrayAsync();
            var b = data.Cast<int>().ToArray();

            a.Should().Equal(b);
        }


        [Fact]
        public async Task CastException()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                var xs = item.Select(x => (object)x).Cast<int>().ToArrayAsync();
                await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs);
            }
        }
    }
}
