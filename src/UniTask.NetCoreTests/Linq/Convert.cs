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
    public class Convert
    {
        [Fact]
        public async Task ToAsyncEnumerable()
        {
            {
                var xs = await Enumerable.Range(1, 100).ToUniTaskAsyncEnumerable().ToArrayAsync();

                xs.Length.Should().Be(100);
            }
            {
                var xs = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().ToArrayAsync();

                xs.Length.Should().Be(0);
            }
        }

        [Fact]
        public async Task ToAsyncEnumerableTask()
        {
            var t = Task.FromResult(100);
            var xs = await t.ToUniTaskAsyncEnumerable().ToArrayAsync();

            xs.Length.Should().Be(1);
            xs[0].Should().Be(100);
        }

        [Fact]
        public async Task ToAsyncEnumerableUniTask()
        {
            var t = UniTask.FromResult(100);
            var xs = await t.ToUniTaskAsyncEnumerable().ToArrayAsync();

            xs.Length.Should().Be(1);
            xs[0].Should().Be(100);
        }

        [Fact]
        public async Task ToAsyncEnumerableObservable()
        {
            {
                var xs = await Observable.Range(1, 100).ToUniTaskAsyncEnumerable().ToArrayAsync();
                var ys = await Observable.Range(1, 100).ToArray();

                xs.Should().BeEquivalentTo(ys);
            }

            {
                var xs = await Observable.Range(1, 100, ThreadPoolScheduler.Instance).ToUniTaskAsyncEnumerable().ToArrayAsync();
                var ys = await Observable.Range(1, 100, ThreadPoolScheduler.Instance).ToArray();

                xs.Should().BeEquivalentTo(ys);
            }

            {
                var xs = await Observable.Empty<int>(ThreadPoolScheduler.Instance).ToUniTaskAsyncEnumerable().ToArrayAsync();
                var ys = await Observable.Empty<int>(ThreadPoolScheduler.Instance).ToArray();

                xs.Should().BeEquivalentTo(ys);
            }
        }

    }


}
