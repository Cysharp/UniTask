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
    public class SortCheck
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return (Age, FirstName, LastName).ToString();
        }
    }

    public class Sort
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
        static UniTask<T> RandomRun<T>(T value, CancellationToken ct)
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
        public async Task OrderBy()
        {
            var array = new[] { 1, 99, 32, 4, 536, 7, 8 };
            {
                var xs = await array.ToUniTaskAsyncEnumerable().OrderBy(x => x).ToArrayAsync();
                var ys = array.OrderBy(x => x).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await array.ToUniTaskAsyncEnumerable().OrderByDescending(x => x).ToArrayAsync();
                var ys = array.OrderByDescending(x => x).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await array.ToUniTaskAsyncEnumerable().OrderByAwait(RandomRun).ToArrayAsync();
                var ys = array.OrderBy(x => x).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwait(RandomRun).ToArrayAsync();
                var ys = array.OrderByDescending(x => x).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await array.ToUniTaskAsyncEnumerable().OrderByAwaitWithCancellation(RandomRun).ToArrayAsync();
                var ys = array.OrderBy(x => x).ToArray();
                xs.Should().Equal(ys);
            }
            {
                var xs = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwaitWithCancellation(RandomRun).ToArrayAsync();
                var ys = array.OrderByDescending(x => x).ToArray();
                xs.Should().Equal(ys);
            }
        }

        [Fact]
        public async Task ThenBy()
        {
            var array = new[]
            {
                new SortCheck { Age = 99, FirstName = "ABC", LastName = "DEF" },
                new SortCheck { Age = 49, FirstName = "ABC", LastName = "DEF" },
                new SortCheck { Age = 49, FirstName = "ABC", LastName = "ZKH" },
                new SortCheck { Age = 12, FirstName = "ABC", LastName = "DEF" },
                new SortCheck { Age = 49, FirstName = "ABC", LastName = "MEF" },
                new SortCheck { Age = 12, FirstName = "QQQ", LastName = "DEF" },
                new SortCheck { Age = 19, FirstName = "ZKN", LastName = "DEF" },
                new SortCheck { Age = 39, FirstName = "APO", LastName = "REF" },
                new SortCheck { Age = 59, FirstName = "ABC", LastName = "DEF" },
                new SortCheck { Age = 99, FirstName = "DBC", LastName = "DEF" },
                new SortCheck { Age = 99, FirstName = "DBC", LastName = "MEF" },
            };
            {
                var a = array.OrderBy(x => x.Age).ThenBy(x => x.FirstName).ThenBy(x => x.LastName).ToArray();
                var b = array.OrderBy(x => x.Age).ThenBy(x => x.FirstName).ThenByDescending(x => x.LastName).ToArray();
                var c = array.OrderBy(x => x.Age).ThenByDescending(x => x.FirstName).ThenBy(x => x.LastName).ToArray();
                var d = array.OrderBy(x => x.Age).ThenByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToArray();
                var e = array.OrderByDescending(x => x.Age).ThenBy(x => x.FirstName).ThenBy(x => x.LastName).ToArray();
                var f = array.OrderByDescending(x => x.Age).ThenBy(x => x.FirstName).ThenByDescending(x => x.LastName).ToArray();
                var g = array.OrderByDescending(x => x.Age).ThenByDescending(x => x.FirstName).ThenBy(x => x.LastName).ToArray();
                var h = array.OrderByDescending(x => x.Age).ThenByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToArray();

                {
                    var a2 = await array.ToUniTaskAsyncEnumerable().OrderBy(x => x.Age).ThenBy(x => x.FirstName).ThenBy(x => x.LastName).ToArrayAsync();
                    var b2 = await array.ToUniTaskAsyncEnumerable().OrderBy(x => x.Age).ThenBy(x => x.FirstName).ThenByDescending(x => x.LastName).ToArrayAsync();
                    var c2 = await array.ToUniTaskAsyncEnumerable().OrderBy(x => x.Age).ThenByDescending(x => x.FirstName).ThenBy(x => x.LastName).ToArrayAsync();
                    var d2 = await array.ToUniTaskAsyncEnumerable().OrderBy(x => x.Age).ThenByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToArrayAsync();
                    var e2 = await array.ToUniTaskAsyncEnumerable().OrderByDescending(x => x.Age).ThenBy(x => x.FirstName).ThenBy(x => x.LastName).ToArrayAsync();
                    var f2 = await array.ToUniTaskAsyncEnumerable().OrderByDescending(x => x.Age).ThenBy(x => x.FirstName).ThenByDescending(x => x.LastName).ToArrayAsync();
                    var g2 = await array.ToUniTaskAsyncEnumerable().OrderByDescending(x => x.Age).ThenByDescending(x => x.FirstName).ThenBy(x => x.LastName).ToArrayAsync();
                    var h2 = await array.ToUniTaskAsyncEnumerable().OrderByDescending(x => x.Age).ThenByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToArrayAsync();

                    a.Should().Equal(a2);
                    b.Should().Equal(b2);
                    c.Should().Equal(c2);
                    d.Should().Equal(d2);
                    e.Should().Equal(e2);
                    f.Should().Equal(f2);
                    g.Should().Equal(g2);
                    h.Should().Equal(h2);
                }
                {
                    var a2 = await array.ToUniTaskAsyncEnumerable().OrderByAwait(x => RandomRun(x.Age)).ThenByAwait(x => RandomRun(x.FirstName)).ThenByAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var b2 = await array.ToUniTaskAsyncEnumerable().OrderByAwait(x => RandomRun(x.Age)).ThenByAwait(x => RandomRun(x.FirstName)).ThenByDescendingAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var c2 = await array.ToUniTaskAsyncEnumerable().OrderByAwait(x => RandomRun(x.Age)).ThenByDescendingAwait(x => RandomRun(x.FirstName)).ThenByAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var d2 = await array.ToUniTaskAsyncEnumerable().OrderByAwait(x => RandomRun(x.Age)).ThenByDescendingAwait(x => RandomRun(x.FirstName)).ThenByDescendingAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var e2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwait(x => RandomRun(x.Age)).ThenByAwait(x => RandomRun(x.FirstName)).ThenByAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var f2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwait(x => RandomRun(x.Age)).ThenByAwait(x => RandomRun(x.FirstName)).ThenByDescendingAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var g2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwait(x => RandomRun(x.Age)).ThenByDescendingAwait(x => RandomRun(x.FirstName)).ThenByAwait(x => RandomRun(x.LastName)).ToArrayAsync();
                    var h2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwait(x => RandomRun(x.Age)).ThenByDescendingAwait(x => RandomRun(x.FirstName)).ThenByDescendingAwait(x => RandomRun(x.LastName)).ToArrayAsync();

                    a.Should().Equal(a2);
                    b.Should().Equal(b2);
                    c.Should().Equal(c2);
                    d.Should().Equal(d2);
                    e.Should().Equal(e2);
                    f.Should().Equal(f2);
                    g.Should().Equal(g2);
                    h.Should().Equal(h2);
                }
                {
                    var a2 = await array.ToUniTaskAsyncEnumerable().OrderByAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var b2 = await array.ToUniTaskAsyncEnumerable().OrderByAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var c2 = await array.ToUniTaskAsyncEnumerable().OrderByAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var d2 = await array.ToUniTaskAsyncEnumerable().OrderByAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var e2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var f2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var g2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();
                    var h2 = await array.ToUniTaskAsyncEnumerable().OrderByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.Age)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.FirstName)).ThenByDescendingAwaitWithCancellation((x, ct) => RandomRun(x.LastName)).ToArrayAsync();

                    a.Should().Equal(a2);
                    b.Should().Equal(b2);
                    c.Should().Equal(c2);
                    d.Should().Equal(d2);
                    e.Should().Equal(e2);
                    f.Should().Equal(f2);
                    g.Should().Equal(g2);
                    h.Should().Equal(h2);
                }
            }
        }


        [Fact]
        public async Task Throws()
        {
            foreach (var item in UniTaskTestException.Throws())
            {
                {
                    var a = item.OrderBy(x => x).ToArrayAsync();
                    var b = item.OrderByDescending(x => x).ToArrayAsync();
                    var c = item.OrderBy(x => x).ThenBy(x => x).ToArrayAsync();
                    var d = item.OrderBy(x => x).ThenByDescending(x => x).ToArrayAsync();

                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await a);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await b);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await c);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await d);
                }
                {
                    var a = item.OrderByAwait(RandomRun).ToArrayAsync();
                    var b = item.OrderByDescendingAwait(RandomRun).ToArrayAsync();
                    var c = item.OrderByAwait(RandomRun).ThenByAwait(RandomRun).ToArrayAsync();
                    var d = item.OrderByAwait(RandomRun).ThenByDescendingAwait(RandomRun).ToArrayAsync();

                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await a);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await b);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await c);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await d);
                }
                {
                    var a = item.OrderByAwaitWithCancellation(RandomRun).ToArrayAsync();
                    var b = item.OrderByDescendingAwaitWithCancellation(RandomRun).ToArrayAsync();
                    var c = item.OrderByAwaitWithCancellation(RandomRun).ThenByAwaitWithCancellation(RandomRun).ToArrayAsync();
                    var d = item.OrderByAwaitWithCancellation(RandomRun).ThenByDescendingAwaitWithCancellation(RandomRun).ToArrayAsync();

                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await a);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await b);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await c);
                    await Assert.ThrowsAsync<UniTaskTestException>(async () => await d);
                }
            }

        }
    }
}
