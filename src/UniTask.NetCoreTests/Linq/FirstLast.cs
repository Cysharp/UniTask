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
    public class FirstLast
    {
        [Fact]
        public async Task FirstTest()
        {
            {
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().FirstAsync());
                Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().First());

                var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().FirstAsync();
                var y = new[] { 99 }.First();
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().FirstAsync(x => x % 98 == 0));
                Assert.Throws<InvalidOperationException>(() => array.First(x => x % 98 == 0));

                var x = await array.ToUniTaskAsyncEnumerable().FirstAsync(x => x % 2 == 0);
                var y = array.First(x => x % 2 == 0);
                x.Should().Be(y);
            }

            {
                var x = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().FirstOrDefaultAsync();
                var y = Enumerable.Empty<int>().FirstOrDefault();
                x.Should().Be(y);
            }
            {
                var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().FirstOrDefaultAsync();
                var y = new[] { 99 }.FirstOrDefault();
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                var x = await array.ToUniTaskAsyncEnumerable().FirstOrDefaultAsync(x => x % 98 == 0);
                var y = array.FirstOrDefault(x => x % 98 == 0);
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                var x = await array.ToUniTaskAsyncEnumerable().FirstAsync(x => x % 2 == 0);
                var y = array.FirstOrDefault(x => x % 2 == 0);
                x.Should().Be(y);
            }
        }

        [Fact]
        public async Task LastTest()
        {
            {
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().LastAsync());
                Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Last());

                var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().LastAsync();
                var y = new[] { 99 }.Last();
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().LastAsync(x => x % 98 == 0));
                Assert.Throws<InvalidOperationException>(() => array.Last(x => x % 98 == 0));

                var x = await array.ToUniTaskAsyncEnumerable().LastAsync(x => x % 2 == 0);
                var y = array.Last(x => x % 2 == 0);
                x.Should().Be(y);
            }

            {
                var x = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().LastOrDefaultAsync();
                var y = Enumerable.Empty<int>().LastOrDefault();
                x.Should().Be(y);
            }
            {
                var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().LastOrDefaultAsync();
                var y = new[] { 99 }.LastOrDefault();
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                var x = await array.ToUniTaskAsyncEnumerable().LastOrDefaultAsync(x => x % 98 == 0);
                var y = array.LastOrDefault(x => x % 98 == 0);
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                var x = await array.ToUniTaskAsyncEnumerable().LastOrDefaultAsync(x => x % 2 == 0);
                var y = array.LastOrDefault(x => x % 2 == 0);
                x.Should().Be(y);
            }
        }

        [Fact]
        public async Task SingleTest()
        {
            {
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().SingleAsync());
                Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Single());

                var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().SingleAsync();
                var y = new[] { 99 }.Single();
                x.Should().Be(y);

                var array = new[] { 99, 11, 135, 10, 144, 800 };
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().SingleAsync());
                Assert.Throws<InvalidOperationException>(() => array.Single());
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                // not found
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().SingleAsync(x => x % 999 == 0));
                Assert.Throws<InvalidOperationException>(() => array.Single(x => x % 999 == 0));
                // found multi
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().SingleAsync(x => x % 2 == 0));
                Assert.Throws<InvalidOperationException>(() => array.Single(x => x % 2 == 0));

                {
                    var x = await array.ToUniTaskAsyncEnumerable().SingleAsync(x => x % 144 == 0);
                    var y = array.Single(x => x % 144 == 0);
                    x.Should().Be(y);
                }
                {
                    var x = await array.ToUniTaskAsyncEnumerable().SingleAsync(x => x % 800 == 0);
                    var y = array.Single(x => x % 800 == 0);
                    x.Should().Be(y);
                }
            }

            {
                {
                    var x = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().SingleOrDefaultAsync();
                    var y = Enumerable.Empty<int>().SingleOrDefault();
                    x.Should().Be(y);
                }
                {
                    var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().SingleOrDefaultAsync();
                    var y = new[] { 99 }.SingleOrDefault();
                    x.Should().Be(y);

                    var array = new[] { 99, 11, 135, 10, 144, 800 };
                    await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().SingleOrDefaultAsync());
                    Assert.Throws<InvalidOperationException>(() => array.SingleOrDefault());
                }
                {
                    var array = new[] { 99, 11, 135, 10, 144, 800 };
                    // not found
                    {
                        var x = await array.ToUniTaskAsyncEnumerable().SingleOrDefaultAsync(x => x % 999 == 0);
                        var y = array.SingleOrDefault(x => x % 999 == 0);
                        x.Should().Be(y);
                    }
                    // found multi
                    await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().SingleOrDefaultAsync(x => x % 2 == 0));
                    Assert.Throws<InvalidOperationException>(() => array.SingleOrDefault(x => x % 2 == 0));

                    // normal
                    {
                        var x = await array.ToUniTaskAsyncEnumerable().SingleOrDefaultAsync(x => x % 144 == 0);
                        var y = array.SingleOrDefault(x => x % 144 == 0);
                        x.Should().Be(y);
                    }
                    {
                        var x = await array.ToUniTaskAsyncEnumerable().SingleOrDefaultAsync(x => x % 800 == 0);
                        var y = array.SingleOrDefault(x => x % 800 == 0);
                        x.Should().Be(y);
                    }
                }
            }
        }


        [Fact]
        public async Task ElementAtTest()
        {
            {
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().ElementAtAsync(0));
                Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Empty<int>().ElementAt(0));

                var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().ElementAtAsync(0);
                var y = new[] { 99 }.ElementAt(0);
                x.Should().Be(y);
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await array.ToUniTaskAsyncEnumerable().ElementAtAsync(10));
                Assert.Throws<ArgumentOutOfRangeException>(() => array.ElementAt(10));

                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtAsync(0);
                    var y = array.ElementAt(0);
                    x.Should().Be(y);
                }
                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtAsync(3);
                    var y = array.ElementAt(3);
                    x.Should().Be(y);
                }
                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtAsync(5);
                    var y = array.ElementAt(5);
                    x.Should().Be(y);
                }
            }


            {
                {
                    var x = await Enumerable.Empty<int>().ToUniTaskAsyncEnumerable().ElementAtOrDefaultAsync(0);
                    var y = Enumerable.Empty<int>().ElementAtOrDefault(0);
                    x.Should().Be(y);
                }
                {
                    var x = await new[] { 99 }.ToUniTaskAsyncEnumerable().ElementAtOrDefaultAsync(0);
                    var y = new[] { 99 }.ElementAtOrDefault(0);
                    x.Should().Be(y);
                }
            }
            {
                var array = new[] { 99, 11, 135, 10, 144, 800 };
                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtOrDefaultAsync(10);
                    var y = array.ElementAtOrDefault(10);
                    x.Should().Be(y);
                }
                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtOrDefaultAsync(0);
                    var y = array.ElementAtOrDefault(0);
                    x.Should().Be(y);
                }
                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtOrDefaultAsync(3);
                    var y = array.ElementAtOrDefault(3);
                    x.Should().Be(y);
                }
                {
                    var x = await array.ToUniTaskAsyncEnumerable().ElementAtOrDefaultAsync(5);
                    var y = array.ElementAtOrDefault(5);
                    x.Should().Be(y);
                }
            }
        }
    }


}
