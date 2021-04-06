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
    public class ChannelTest
    {
        (System.Threading.Channels.Channel<int>, Cysharp.Threading.Tasks.Channel<int>) CreateChannel()
        {
            var reference = System.Threading.Channels.Channel.CreateUnbounded<int>(new UnboundedChannelOptions
            {
                AllowSynchronousContinuations = true,
                SingleReader = true,
                SingleWriter = false
            });

            var channel = Cysharp.Threading.Tasks.Channel.CreateSingleConsumerUnbounded<int>();

            return (reference, channel);
        }

        [Fact]
        public async Task SingleWriteSingleRead()
        {
            var (reference, channel) = CreateChannel();

            foreach (var item in new[] { 10, 20, 30 })
            {
                var t1 = reference.Reader.WaitToReadAsync();
                var t2 = channel.Reader.WaitToReadAsync();

                t1.IsCompleted.Should().BeFalse();
                t2.Status.IsCompleted().Should().BeFalse();

                reference.Writer.TryWrite(item);
                channel.Writer.TryWrite(item);

                (await t1).Should().BeTrue();
                (await t2).Should().BeTrue();

                reference.Reader.TryRead(out var refitem).Should().BeTrue();
                channel.Reader.TryRead(out var chanitem).Should().BeTrue();
                refitem.Should().Be(item);
                chanitem.Should().Be(item);
            }
        }

        [Fact]
        public async Task MultiWrite()
        {
            var (reference, channel) = CreateChannel();

            foreach (var item in new[] { 10, 20, 30 })
            {
                var t1 = reference.Reader.WaitToReadAsync();
                var t2 = channel.Reader.WaitToReadAsync();

                t1.IsCompleted.Should().BeFalse();
                t2.Status.IsCompleted().Should().BeFalse();

                foreach (var i in Enumerable.Range(1, 3))
                {
                    reference.Writer.TryWrite(item * i);
                    channel.Writer.TryWrite(item * i);
                }

                (await t1).Should().BeTrue();
                (await t2).Should().BeTrue();

                foreach (var i in Enumerable.Range(1, 3))
                {
                    (await reference.Reader.WaitToReadAsync()).Should().BeTrue();
                    (await channel.Reader.WaitToReadAsync()).Should().BeTrue();

                    reference.Reader.TryRead(out var refitem).Should().BeTrue();
                    channel.Reader.TryRead(out var chanitem).Should().BeTrue();
                    refitem.Should().Be(item * i);
                    chanitem.Should().Be(item * i);
                }
            }
        }

        [Fact]
        public async Task CompleteOnEmpty()
        {
            var (reference, channel) = CreateChannel();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Writer.TryWrite(item);
                channel.Writer.TryWrite(item);
                reference.Reader.TryRead(out var refitem);
                channel.Reader.TryRead(out var chanitem);
            }

            // Empty.

            var completion1 = reference.Reader.Completion;
            var wait1 = reference.Reader.WaitToReadAsync();

            var completion2 = channel.Reader.Completion;
            var wait2 = channel.Reader.WaitToReadAsync();

            reference.Writer.TryComplete();
            channel.Writer.TryComplete();

            completion1.Status.Should().Be(TaskStatus.RanToCompletion);
            completion2.Status.Should().Be(UniTaskStatus.Succeeded);

            (await wait1).Should().BeFalse();
            (await wait2).Should().BeFalse();
        }

        [Fact]
        public async Task CompleteErrorOnEmpty()
        {
            var (reference, channel) = CreateChannel();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Writer.TryWrite(item);
                channel.Writer.TryWrite(item);
                reference.Reader.TryRead(out var refitem);
                channel.Reader.TryRead(out var chanitem);
            }

            // Empty.

            var completion1 = reference.Reader.Completion;
            var wait1 = reference.Reader.WaitToReadAsync();

            var completion2 = channel.Reader.Completion;
            var wait2 = channel.Reader.WaitToReadAsync();

            var ex = new Exception();
            reference.Writer.TryComplete(ex);
            channel.Writer.TryComplete(ex);

            completion1.Status.Should().Be(TaskStatus.Faulted);
            completion2.Status.Should().Be(UniTaskStatus.Faulted);

            (await Assert.ThrowsAsync<Exception>(async () => await wait1)).Should().Be(ex);
            (await Assert.ThrowsAsync<Exception>(async () => await wait2)).Should().Be(ex);
        }

        [Fact]
        public async Task CompleteWithRest()
        {
            var (reference, channel) = CreateChannel();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Writer.TryWrite(item);
                channel.Writer.TryWrite(item);
            }

            // Three Item2.

            var completion1 = reference.Reader.Completion;
            var wait1 = reference.Reader.WaitToReadAsync();

            var completion2 = channel.Reader.Completion;
            var wait2 = channel.Reader.WaitToReadAsync();

            reference.Writer.TryComplete();
            channel.Writer.TryComplete();

            // completion1.Status.Should().Be(TaskStatus.WaitingForActivation);
            completion2.Status.Should().Be(UniTaskStatus.Pending);

            (await wait1).Should().BeTrue();
            (await wait2).Should().BeTrue();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Reader.TryRead(out var i1).Should().BeTrue();
                channel.Reader.TryRead(out var i2).Should().BeTrue();
                i1.Should().Be(item);
                i2.Should().Be(item);
            }

            (await reference.Reader.WaitToReadAsync()).Should().BeFalse();
            (await channel.Reader.WaitToReadAsync()).Should().BeFalse();

            completion1.Status.Should().Be(TaskStatus.RanToCompletion);
            completion2.Status.Should().Be(UniTaskStatus.Succeeded);
        }


        [Fact]
        public async Task CompleteErrorWithRest()
        {
            var (reference, channel) = CreateChannel();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Writer.TryWrite(item);
                channel.Writer.TryWrite(item);
            }

            // Three Item2.

            var completion1 = reference.Reader.Completion;
            var wait1 = reference.Reader.WaitToReadAsync();

            var completion2 = channel.Reader.Completion;
            var wait2 = channel.Reader.WaitToReadAsync();

            var ex = new Exception();
            reference.Writer.TryComplete(ex);
            channel.Writer.TryComplete(ex);

            // completion1.Status.Should().Be(TaskStatus.WaitingForActivation);
            completion2.Status.Should().Be(UniTaskStatus.Pending);

            (await wait1).Should().BeTrue();
            (await wait2).Should().BeTrue();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Reader.TryRead(out var i1).Should().BeTrue();
                channel.Reader.TryRead(out var i2).Should().BeTrue();
                i1.Should().Be(item);
                i2.Should().Be(item);
            }

            wait1 = reference.Reader.WaitToReadAsync();
            wait2 = channel.Reader.WaitToReadAsync();

            (await Assert.ThrowsAsync<Exception>(async () => await wait1)).Should().Be(ex);
            (await Assert.ThrowsAsync<Exception>(async () => await wait2)).Should().Be(ex);

            completion1.Status.Should().Be(TaskStatus.Faulted);
            completion2.Status.Should().Be(UniTaskStatus.Faulted);
        }

        [Fact]
        public async Task Cancellation()
        {
            var (reference, channel) = CreateChannel();

            var cts = new CancellationTokenSource();

            var wait1 = reference.Reader.WaitToReadAsync(cts.Token);
            var wait2 = channel.Reader.WaitToReadAsync(cts.Token);

            cts.Cancel();

            (await Assert.ThrowsAsync<OperationCanceledException>(async () => await wait1)).CancellationToken.Should().Be(cts.Token);
            (await Assert.ThrowsAsync<OperationCanceledException>(async () => await wait2)).CancellationToken.Should().Be(cts.Token);
        }

        [Fact]
        public async Task AsyncEnumerator()
        {
            var (reference, channel) = CreateChannel();

            var ta1 = reference.Reader.ReadAllAsync().ToArrayAsync();
            var ta2 = channel.Reader.ReadAllAsync().ToArrayAsync();

            foreach (var item in new[] { 10, 20, 30 })
            {
                reference.Writer.TryWrite(item);
                channel.Writer.TryWrite(item);
            }

            reference.Writer.TryComplete();
            channel.Writer.TryComplete();

            (await ta1).Should().Equal(new[] { 10, 20, 30 });
            (await ta2).Should().Equal(new[] { 10, 20, 30 });
        }

        [Fact]
        public async Task AsyncEnumeratorCancellation()
        {
            // Token1, Token2 and Cancel1
            {
                var cts1 = new CancellationTokenSource();
                var cts2 = new CancellationTokenSource();

                var (reference, channel) = CreateChannel();

                var ta1 = reference.Reader.ReadAllAsync(cts1.Token).ToArrayAsync(cts2.Token);
                var ta2 = channel.Reader.ReadAllAsync(cts1.Token).ToArrayAsync(cts2.Token);

                foreach (var item in new[] { 10, 20, 30 })
                {
                    reference.Writer.TryWrite(item);
                    channel.Writer.TryWrite(item);
                }

                cts1.Cancel();

                await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta1);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta2)).CancellationToken.Should().Be(cts1.Token);
            }
            // Token1, Token2 and Cancel2
            {
                var cts1 = new CancellationTokenSource();
                var cts2 = new CancellationTokenSource();

                var (reference, channel) = CreateChannel();

                var ta1 = reference.Reader.ReadAllAsync(cts1.Token).ToArrayAsync(cts2.Token);
                var ta2 = channel.Reader.ReadAllAsync(cts1.Token).ToArrayAsync(cts2.Token);

                foreach (var item in new[] { 10, 20, 30 })
                {
                    reference.Writer.TryWrite(item);
                    channel.Writer.TryWrite(item);
                }

                cts2.Cancel();

                await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta1);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta2)).CancellationToken.Should().Be(cts2.Token);
            }
            // Token1 and Cancel1
            {
                var cts1 = new CancellationTokenSource();

                var (reference, channel) = CreateChannel();

                var ta1 = reference.Reader.ReadAllAsync(cts1.Token).ToArrayAsync();
                var ta2 = channel.Reader.ReadAllAsync(cts1.Token).ToArrayAsync();

                foreach (var item in new[] { 10, 20, 30 })
                {
                    reference.Writer.TryWrite(item);
                    channel.Writer.TryWrite(item);
                }

                cts1.Cancel();

                await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta1);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta2)).CancellationToken.Should().Be(cts1.Token);
            }
            // Token2 and Cancel2
            {
                var cts2 = new CancellationTokenSource();

                var (reference, channel) = CreateChannel();

                var ta1 = reference.Reader.ReadAllAsync().ToArrayAsync(cts2.Token);
                var ta2 = channel.Reader.ReadAllAsync().ToArrayAsync(cts2.Token);

                foreach (var item in new[] { 10, 20, 30 })
                {
                    reference.Writer.TryWrite(item);
                    channel.Writer.TryWrite(item);
                }

                cts2.Cancel();

                await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta1);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await ta2)).CancellationToken.Should().Be(cts2.Token);
            }
        }
    }
}
