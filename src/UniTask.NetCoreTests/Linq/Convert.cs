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
        public async Task ToObservable()
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, 10).ToObservable().ToArray();
                xs.Should().Equal(Enumerable.Range(1, 10));
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(1, 0).ToObservable().ToArray();
                xs.Should().Equal(Enumerable.Range(1, 0));
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

                xs.Should().Equal(ys);
            }

            {
                var xs = await Observable.Range(1, 100, ThreadPoolScheduler.Instance).ToUniTaskAsyncEnumerable().ToArrayAsync();
                var ys = await Observable.Range(1, 100, ThreadPoolScheduler.Instance).ToArray();

                xs.Should().Equal(ys);
            }

            {
                var xs = await Observable.Empty<int>(ThreadPoolScheduler.Instance).ToUniTaskAsyncEnumerable().ToArrayAsync();
                var ys = await Observable.Empty<int>(ThreadPoolScheduler.Instance).ToArray();

                xs.Should().Equal(ys);
            }
        }

        [Fact]
        public async Task ToDictionary()
        {
            {
                var xs = await Enumerable.Range(1, 100).ToUniTaskAsyncEnumerable().ToDictionaryAsync(x => x);
                var ys = Enumerable.Range(1, 100).ToDictionary(x => x);

                xs.OrderBy(x => x.Key).Should().Equal(ys.OrderBy(x => x.Key));
            }
            {
                var xs = await Enumerable.Range(1, 0).ToUniTaskAsyncEnumerable().ToDictionaryAsync(x => x);
                var ys = Enumerable.Range(1, 0).ToDictionary(x => x);

                xs.OrderBy(x => x.Key).Should().Equal(ys.OrderBy(x => x.Key));
            }
            {
                var xs = await Enumerable.Range(1, 100).ToUniTaskAsyncEnumerable().ToDictionaryAsync(x => x, x => x * 2);
                var ys = Enumerable.Range(1, 100).ToDictionary(x => x, x => x * 2);

                xs.OrderBy(x => x.Key).Should().Equal(ys.OrderBy(x => x.Key));
            }
            {
                var xs = await Enumerable.Range(1, 0).ToUniTaskAsyncEnumerable().ToDictionaryAsync(x => x, x => x * 2);
                var ys = Enumerable.Range(1, 0).ToDictionary(x => x, x => x * 2);

                xs.OrderBy(x => x.Key).Should().Equal(ys.OrderBy(x => x.Key));
            }
        }

        [Fact]
        public async Task ToLookup()
        {
            var arr = new[] { 1, 4, 10, 10, 4, 5, 10, 9 };
            {
                var xs = await arr.ToUniTaskAsyncEnumerable().ToLookupAsync(x => x);
                var ys = arr.ToLookup(x => x);

                xs.Count.Should().Be(ys.Count);
                xs.Should().BeEquivalentTo(ys);
                foreach (var key in xs.Select(x => x.Key))
                {
                    xs[key].Should().Equal(ys[key]);
                }
            }
            {
                var xs = await Enumerable.Range(1, 0).ToUniTaskAsyncEnumerable().ToLookupAsync(x => x);
                var ys = Enumerable.Range(1, 0).ToLookup(x => x);

                xs.Should().BeEquivalentTo(ys);
            }
            {
                var xs = await arr.ToUniTaskAsyncEnumerable().ToLookupAsync(x => x, x => x * 2);
                var ys = arr.ToLookup(x => x, x => x * 2);

                xs.Count.Should().Be(ys.Count);
                xs.Should().BeEquivalentTo(ys);
                foreach (var key in xs.Select(x => x.Key))
                {
                    xs[key].Should().Equal(ys[key]);
                }
            }
            {
                var xs = await Enumerable.Range(1, 0).ToUniTaskAsyncEnumerable().ToLookupAsync(x => x, x => x * 2);
                var ys = Enumerable.Range(1, 0).ToLookup(x => x, x => x * 2);

                xs.Should().BeEquivalentTo(ys);
            }
        }

        [Fact]
        public async Task ToList()
        {
            {
                var xs = await Enumerable.Range(1, 100).ToUniTaskAsyncEnumerable().ToListAsync();
                var ys = Enumerable.Range(1, 100).ToList();

                xs.Should().Equal(ys);
            }
            {
                var xs = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().ToListAsync();
                var ys = Enumerable.Empty<int>().ToList();

                xs.Should().Equal(ys);
            }
        }

        [Fact]
        public async Task ToHashSet()
        {
            {
                var xs = await new[] { 1, 20, 4, 5, 20, 4, 6 }.ToUniTaskAsyncEnumerable().ToHashSetAsync();
                var ys = new[] { 1, 20, 4, 5, 20, 4, 6 }.ToHashSet();

                xs.OrderBy(x => x).Should().Equal(ys.OrderBy(x => x));
            }
            {
                var xs = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().ToHashSetAsync();
                var ys = Enumerable.Empty<int>().ToHashSet();

                xs.Should().Equal(ys);
            }
        }
    }


}
