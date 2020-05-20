using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using Cysharp.Threading.Tasks.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class CancellationTokenTest
    {
        [Fact]
        public async Task WaitUntilCanceled()
        {
            var cts = new CancellationTokenSource();

            cts.CancelAfter(TimeSpan.FromSeconds(1.5));

            var now = DateTime.UtcNow;

            await cts.Token.WaitUntilCanceled();

            var elapsed = DateTime.UtcNow - now;

            elapsed.Should().BeGreaterThan(TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void AlreadyCanceled()
        {
            var cts = new CancellationTokenSource();

            cts.Cancel();

            cts.Token.WaitUntilCanceled().GetAwaiter().IsCompleted.Should().BeTrue();
        }

        [Fact]
        public void None()
        {
            CancellationToken.None.WaitUntilCanceled().GetAwaiter().IsCompleted.Should().BeTrue();
        }
    }


}
