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
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task InterleavesSources(bool includeThirdSource)
        {
            var a1Ready = new UniTaskCompletionSource();
            var b1Ready = new UniTaskCompletionSource();
            var a2Ready = new UniTaskCompletionSource();
            var b3Ready = new UniTaskCompletionSource();

            var a = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await a1Ready.Task;
                await writer.YieldAsync("A1");
                b1Ready.TrySetResult();

                await a2Ready.Task;
                await writer.YieldAsync("A2");
                b3Ready.TrySetResult();
            });

            var b = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
            {
                await b1Ready.Task;
                await writer.YieldAsync("B1");
                await writer.YieldAsync("B2");
                a2Ready.TrySetResult();

                await b3Ready.Task;
                await writer.YieldAsync("B3");
            });

            if (includeThirdSource)
            {
                var c = UniTaskAsyncEnumerable.Create<string>(async (writer, _) =>
                {
                    await writer.YieldAsync("C1");
                    a1Ready.TrySetResult();
                });

                var result = await a.Merge(b, c).ToArrayAsync().AsTask().WaitAsync(TimeSpan.FromSeconds(10));
                result.Should().Equal("C1", "A1", "B1", "B2", "A2", "B3");
            }
            else
            {
                a1Ready.TrySetResult();
                var result = await a.Merge(b).ToArrayAsync().AsTask().WaitAsync(TimeSpan.FromSeconds(10));
                result.Should().Equal("A1", "B1", "B2", "A2", "B3");
            }
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