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
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ToCancellationTokenCompletedTask(bool linkCancellation)
        {
            using var cts = new CancellationTokenSource();

            var token = linkCancellation
                ? UniTask.CompletedTask.ToCancellationToken(cts.Token)
                : UniTask.CompletedTask.ToCancellationToken();

            token.IsCancellationRequested.Should().BeTrue();
            cts.IsCancellationRequested.Should().BeFalse();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ToCancellationTokenCompletedGenericTask(bool linkCancellation)
        {
            using var cts = new CancellationTokenSource();

            var token = linkCancellation
                ? UniTask.FromResult(42).ToCancellationToken(cts.Token)
                : UniTask.FromResult(42).ToCancellationToken();

            token.IsCancellationRequested.Should().BeTrue();
            cts.IsCancellationRequested.Should().BeFalse();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ToCancellationTokenPendingTask(bool linkCancellation)
        {
            using var cts = new CancellationTokenSource();
            var source = new UniTaskCompletionSource();
            var token = linkCancellation
                ? source.Task.ToCancellationToken(cts.Token)
                : source.Task.ToCancellationToken();

            token.IsCancellationRequested.Should().BeFalse();

            source.TrySetResult();

            token.IsCancellationRequested.Should().BeTrue();
            cts.IsCancellationRequested.Should().BeFalse();
        }

        [Fact]
        public void ToCancellationTokenLinkedCancellation()
        {
            using var cts = new CancellationTokenSource();
            var source = new UniTaskCompletionSource();
            var token = source.Task.ToCancellationToken(cts.Token);

            token.IsCancellationRequested.Should().BeFalse();

            cts.Cancel();

            token.IsCancellationRequested.Should().BeTrue();

            source.TrySetResult();

            token.IsCancellationRequested.Should().BeTrue();
        }

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
