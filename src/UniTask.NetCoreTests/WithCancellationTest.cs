using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class WithCancellationTest
    {
        [Fact]
        public async Task Standard()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            var v = await UniTask.Run(() => 10).AttachExternalCancellation(cts.Token);

            v.Should().Be(10);
        }

        [Fact]
        public async Task Cancel()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            var t = UniTask.Create(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                return 10;
            }).AttachExternalCancellation(cts.Token);

            cts.Cancel();

            (await Assert.ThrowsAsync<OperationCanceledException>(async () => await t)).CancellationToken.Should().Be(cts.Token);
        }
    }
}
