#pragma warning disable CS1998
#pragma warning disable CS0162

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests.Linq
{
    public class CreateTest
    {
        [Fact]
        public async Task SyncCreation()
        {
            var from = 10;
            var count = 100;

            var xs = await UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                for (int i = 0; i < count; i++)
                {
                    await writer.YieldAsync(from + i);
                }
            }).ToArrayAsync();

            var ys = await Range(from, count).AsUniTaskAsyncEnumerable().ToArrayAsync();

            xs.Should().Equal(ys);
        }

        [Fact]
        public async Task SyncManually()
        {
            var list = new List<int>();
            var xs = UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                list.Add(100);
                await writer.YieldAsync(10);
                
                list.Add(200);
                await writer.YieldAsync(20);

                list.Add(300);
                await writer.YieldAsync(30);
            
                list.Add(400);
            });

            list.Should().BeEmpty();
            var e = xs.GetAsyncEnumerator();

            list.Should().BeEmpty();

            await e.MoveNextAsync();
            list.Should().Equal(100);
            e.Current.Should().Be(10);

            await e.MoveNextAsync();
            list.Should().Equal(100, 200);
            e.Current.Should().Be(20);

            await e.MoveNextAsync();
            list.Should().Equal(100, 200, 300);
            e.Current.Should().Be(30);

            (await e.MoveNextAsync()).Should().BeFalse();
            list.Should().Equal(100, 200, 300, 400);
        }

        [Fact]
        public async Task SyncExceptionFirst()
        {
            var from = 10;
            var count = 100;

            var xs = UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                for (int i = 0; i < count; i++)
                {
                    throw new UniTaskTestException();
                    await writer.YieldAsync(from + i);
                }
            });

            await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs.ToArrayAsync());
        }

        [Fact]
        public async Task SyncException()
        {
            var from = 10;
            var count = 100;

            var xs = UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                for (int i = 0; i < count; i++)
                {
                    await writer.YieldAsync(from + i);

                    if (i == 15)
                    {
                        throw new UniTaskTestException();
                    }
                }
            });

            await Assert.ThrowsAsync<UniTaskTestException>(async () => await xs.ToArrayAsync());
        }

        [Fact]
        public async Task ASyncManually()
        {
            var list = new List<int>();
            var xs = UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                await UniTask.Yield();

                list.Add(100);
                await writer.YieldAsync(10);

                await UniTask.Yield();

                list.Add(200);
                await writer.YieldAsync(20);

                await UniTask.Yield();
                list.Add(300);
                await UniTask.Yield();
                await writer.YieldAsync(30);

                await UniTask.Yield();

                list.Add(400);
            });

            list.Should().BeEmpty();
            var e = xs.GetAsyncEnumerator();

            list.Should().BeEmpty();

            await e.MoveNextAsync();
            list.Should().Equal(100);
            e.Current.Should().Be(10);

            await e.MoveNextAsync();
            list.Should().Equal(100, 200);
            e.Current.Should().Be(20);

            await e.MoveNextAsync();
            list.Should().Equal(100, 200, 300);
            e.Current.Should().Be(30);

            (await e.MoveNextAsync()).Should().BeFalse();
            list.Should().Equal(100, 200, 300, 400);
        }

        [Fact]
        public async Task AwaitForeachBreak()
        {
            var finallyCalled = false;
            var enumerable = UniTaskAsyncEnumerable.Create<int>(async (writer, _) =>
            {
                try
                {
                    await writer.YieldAsync(1);
                }
                finally
                {
                    finallyCalled = true;
                }
            });

            await foreach (var x in enumerable)
            {
                x.Should().Be(1);
                break;
            }
            finallyCalled.Should().BeTrue();
        }

        async IAsyncEnumerable<int> Range(int from, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return from + i;
            }
        }
    }
}
