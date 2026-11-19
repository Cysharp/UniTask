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
    public class DeferTest
    {
        [Fact]
        public async Task D()
        {
            var created = false;
            var v = UniTask.Defer(() => { created = true; return UniTask.Run(() => 10); });

            created.Should().BeFalse();

            var t = await v;

            created.Should().BeTrue();

            t.Should().Be(10);
        }

        [Fact]
        public async Task D2()
        {
            var created = false;
            var v = UniTask.Defer(() => { created = true; return UniTask.Run(() => 10).AsUniTask(); });

            created.Should().BeFalse();

            await v;

            created.Should().BeTrue();
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public async Task SynchronousFactoryFailureIsPreserved(bool withResult, bool withState, bool canceled)
        {
            using var source = new CancellationTokenSource();
            source.Cancel();
            Exception failure = canceled
                ? (Exception)new OperationCanceledException(source.Token)
                : new InvalidOperationException();
            var calls = 0;

            Func<Task> consume;
            Func<UniTaskStatus> getStatus;
            if (withResult)
            {
                Func<UniTask<int>> factory = () =>
                {
                    calls++;
                    throw failure;
                };
                var deferred = withState
                    ? UniTask.Defer<Func<UniTask<int>>, int>(factory, invoke => invoke())
                    : UniTask.Defer(factory);
                consume = async () => { await deferred; };
                getStatus = () => deferred.Status;
            }
            else
            {
                Func<UniTask> factory = () =>
                {
                    calls++;
                    throw failure;
                };
                var deferred = withState
                    ? UniTask.Defer(factory, invoke => invoke())
                    : UniTask.Defer(factory);
                consume = async () => await deferred;
                getStatus = () => deferred.Status;
            }

            Assert.Equal(0, calls);
            var observed = await Record.ExceptionAsync(consume);
            if (canceled)
            {
                Assert.Equal(source.Token, Assert.IsType<OperationCanceledException>(observed).CancellationToken);
                Assert.Equal(UniTaskStatus.Canceled, getStatus());
            }
            else
            {
                Assert.Same(failure, observed);
                Assert.Equal(UniTaskStatus.Faulted, getStatus());
            }
            Assert.Equal(1, calls);
        }
    }


}
