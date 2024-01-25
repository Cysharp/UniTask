#pragma warning disable CS1998

using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using Xunit;

namespace NetCoreTests.Linq
{
    public class MergeTest
    {
        [Fact]
        public async Task TwoSource()
        {
            var semaphore = new SemaphoreSlim(1, 1);

            var a = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await UniTask.SwitchToThreadPool();

                await semaphore.WaitAsync();
                await writer.YieldAsync("A1");
                semaphore.Release();

                await semaphore.WaitAsync();
                await writer.YieldAsync("A2");
                semaphore.Release();
            });

            var b = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await UniTask.SwitchToThreadPool();

                await semaphore.WaitAsync();
                await writer.YieldAsync("B1");
                await writer.YieldAsync("B2");
                semaphore.Release();

                await semaphore.WaitAsync();
                await writer.YieldAsync("B3");
                semaphore.Release();
            });

            var result = await a.Merge(b).ToArrayAsync();
            result.Should().Equal("A1", "B1", "B2", "A2", "B3");
        }

        [Fact]
        public async Task ThreeSource()
        {
            var semaphore = new SemaphoreSlim(0, 1);

            var a = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await UniTask.SwitchToThreadPool();

                await semaphore.WaitAsync();
                await writer.YieldAsync("A1");
                semaphore.Release();

                await semaphore.WaitAsync();
                await writer.YieldAsync("A2");
                semaphore.Release();
            });

            var b = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await UniTask.SwitchToThreadPool();

                await semaphore.WaitAsync();
                await writer.YieldAsync("B1");
                await writer.YieldAsync("B2");
                semaphore.Release();

                await semaphore.WaitAsync();
                await writer.YieldAsync("B3");
                semaphore.Release();
            });

            var c = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await UniTask.SwitchToThreadPool();

                await writer.YieldAsync("C1");
                semaphore.Release();
            });

            var result = await a.Merge(b, c).ToArrayAsync();
            result.Should().Equal("C1", "A1", "B1", "B2", "A2", "B3");
        }

        [Fact]
        public async Task Throw()
        {
            var a = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await writer.YieldAsync("A1");

            });

            var b = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                throw new UniTaskTestException();
            });

            var enumerator = a.Merge(b).GetAsyncEnumerator();
            (await enumerator.MoveNextAsync()).Should().Be(true);
            enumerator.Current.Should().Be("A1");

            await Assert.ThrowsAsync<UniTaskTestException>(async () => await enumerator.MoveNextAsync());
        }

        [Fact]
        public async Task Cancel()
        {
            var cts = new CancellationTokenSource();

            var a = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await writer.YieldAsync("A1");
            });

            var b = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await writer.YieldAsync("B1");
            });

            var enumerator = a.Merge(b).GetAsyncEnumerator(cts.Token);
            (await enumerator.MoveNextAsync()).Should().Be(true);
            enumerator.Current.Should().Be("A1");

            cts.Cancel();
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
        }
    }
}