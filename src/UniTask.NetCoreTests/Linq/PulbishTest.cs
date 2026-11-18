using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests.Linq
{
    public class PublishTest
    {
        [Fact]
        public async Task Normal()
        {
            var rp = new AsyncReactiveProperty<int>(1);

            var multicast = rp.Publish();

            var a = multicast.ToArrayAsync();
            var b = multicast.Take(2).ToArrayAsync();

            var disp = multicast.Connect();

            rp.Value = 2;

            (await b).Should().Equal(1, 2);

            var c = multicast.ToArrayAsync();

            rp.Value = 3;
            rp.Value = 4;
            rp.Value = 5;

            rp.Dispose();

            (await a).Should().Equal(1, 2, 3, 4, 5);
            (await c).Should().Equal(3, 4, 5);

            disp.Dispose();
        }

        [Fact]
        public async Task EnumeratorWithCanceledTokenPreservesCancellation()
        {
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            var token = cancellation.Token;
            var enumerator = UniTaskAsyncEnumerable.Return(1).Publish().GetAsyncEnumerator(token);

            var error = await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
            error.CancellationToken.Should().Be(token);
        }

        [Fact]
        public async Task EnumeratorWithCanceledTokenCanBeDisposedBeforeMoving()
        {
            var token = new CancellationToken(canceled: true);
            var enumerator = UniTaskAsyncEnumerable.Return(1).Publish().GetAsyncEnumerator(token);

            await enumerator.DisposeAsync();
        }

        [Fact]
        public async Task Cancel()
        {
            var rp = new AsyncReactiveProperty<int>(1);

            var multicast = rp.Publish();

            var a = multicast.ToArrayAsync();
            var b = multicast.Take(2).ToArrayAsync();

            var disp = multicast.Connect();

            rp.Value = 2;

            (await b).Should().Equal(1, 2);

            var c = multicast.ToArrayAsync();

            rp.Value = 3;

            disp.Dispose();

            rp.Value = 4;
            rp.Value = 5;

            rp.Dispose();

            await Assert.ThrowsAsync<OperationCanceledException>(async () => await a);
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await c);
        }


    }
}
