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
    public class AsyncReactivePropertyTest
    {
        [Fact]
        public async Task Iteration()
        {
            var rp = new AsyncReactiveProperty<int>(99);

            var f = await rp.FirstAsync();
            f.Should().Be(99);

            var array = rp.Take(5).ToArrayAsync();

            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 131;

            var ar = await array;

            ar.Should().Equal(new[] { 99, 100, 100, 100, 131 });
        }

        [Fact]
        public async Task WithoutCurrent()
        {
            var rp = new AsyncReactiveProperty<int>(99);

            var array = rp.WithoutCurrent().Take(5).ToArrayAsync();

            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 100;
            rp.Value = 131;
            rp.Value = 191;

            var ar = await array;

            ar.Should().Equal(new[] { 100, 100, 100, 131, 191 });
        }

        //[Fact]
        //public async Task StateIteration()
        //{
        //    var rp = new ReadOnlyAsyncReactiveProperty<int>(99);
        //    var setter = rp.GetSetter();

        //    var f = await rp.FirstAsync();
        //    f.Should().Be(99);

        //    var array = rp.Take(5).ToArrayAsync();

        //    setter(100);
        //    setter(100);
        //    setter(100);
        //    setter(131);

        //    var ar = await array;

        //    ar.Should().Equal(new[] { 99, 100, 100, 100, 131 });
        //}

        //[Fact]
        //public async Task StateWithoutCurrent()
        //{
        //    var rp = new ReadOnlyAsyncReactiveProperty<int>(99);
        //    var setter = rp.GetSetter();

        //    var array = rp.WithoutCurrent().Take(5).ToArrayAsync();
        //    setter(100);
        //    setter(100);
        //    setter(100);
        //    setter(131);
        //    setter(191);

        //    var ar = await array;

        //    ar.Should().Equal(new[] { 100, 100, 100, 131, 191 });
        //}



        [Fact]
        public void StateFromEnumeration()
        {
            var rp = new AsyncReactiveProperty<int>(10);

            var state = rp.ToReadOnlyAsyncReactiveProperty(CancellationToken.None);

            rp.Value = 10;
            state.Value.Should().Be(10);

            rp.Value = 20;
            state.Value.Should().Be(20);

            state.Dispose();

            rp.Value = 30;
            state.Value.Should().Be(20);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task EnumerationObservesAlreadyCanceledToken(bool readOnly, bool withoutCurrent)
        {
            using var property = new AsyncReactiveProperty<int>(99);
            using var readOnlyProperty = readOnly ? property.ToReadOnlyAsyncReactiveProperty(CancellationToken.None) : null;
            IReadOnlyAsyncReactiveProperty<int> source = readOnly ? readOnlyProperty : property;
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();

            var sequence = withoutCurrent ? source.WithoutCurrent() : source;
            await using var enumerator = sequence.GetAsyncEnumerator(cancellation.Token);
            await AssertMoveCanceled(enumerator, cancellation.Token);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task EnumerationObservesCancellationBetweenMoves(bool readOnly)
        {
            using var property = new AsyncReactiveProperty<int>(99);
            using var readOnlyProperty = readOnly ? property.ToReadOnlyAsyncReactiveProperty(CancellationToken.None) : null;
            IReadOnlyAsyncReactiveProperty<int> source = readOnly ? readOnlyProperty : property;
            using var cancellation = new CancellationTokenSource();
            await using var enumerator = source.GetAsyncEnumerator(cancellation.Token);

            (await enumerator.MoveNextAsync()).Should().BeTrue();
            enumerator.Current.Should().Be(99);

            cancellation.Cancel();
            await AssertMoveCanceled(enumerator, cancellation.Token);
        }

        [Fact]
        public async Task FirstAsyncWithAlreadyCanceledTokenDoesNotWaitForMatchingValue()
        {
            using var property = new AsyncReactiveProperty<int>(99);
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();

            var result = property.FirstAsync(value => value == 100, cancellation.Token);

            result.Status.Should().Be(UniTaskStatus.Canceled);
            var exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await result);
            exception.CancellationToken.Should().Be(cancellation.Token);
        }

        static async Task AssertMoveCanceled(IUniTaskAsyncEnumerator<int> enumerator, CancellationToken cancellationToken)
        {
            var exception = await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                var move = enumerator.MoveNextAsync();
                move.Status.Should().Be(UniTaskStatus.Canceled);
                await move;
            });
            exception.CancellationToken.Should().Be(cancellationToken);
        }

        [Fact]
        public async Task WaitAsyncTest()
        {
            var rp = new AsyncReactiveProperty<int>(128);

            var f = await rp.FirstAsync();
            f.Should().Be(128);

            {
                var t = rp.WaitAsync();
                rp.Value = 99;
                rp.Value = 100;
                var v = await t;

                v.Should().Be(99);
            }

            {
                var t = rp.WaitAsync();
                rp.Value = 99;
                rp.Value = 100;
                var v = await t;

                v.Should().Be(99);
            }
        }


        [Fact]
        public async Task WaitAsyncCancellationTest()
        {
            var cts = new CancellationTokenSource();

            var rp = new AsyncReactiveProperty<int>(128);

            var t = rp.WaitAsync(cts.Token);

            cts.Cancel();

            rp.Value = 99;
            rp.Value = 100;

            await Assert.ThrowsAsync<OperationCanceledException>(async () => { await t; });
        }


        [Fact]
        public async Task ReadOnlyWaitAsyncTest()
        {
            var rp = new AsyncReactiveProperty<int>(128);
            var rrp = rp.ToReadOnlyAsyncReactiveProperty(CancellationToken.None);

            var t = rrp.WaitAsync();
            rp.Value = 99;
            rp.Value = 100;
            var v = await t;

            v.Should().Be(99);
        }


        [Fact]
        public async Task ReadOnlyWaitAsyncCancellationTest()
        {
            var cts = new CancellationTokenSource();

            var rp = new AsyncReactiveProperty<int>(128);
            var rrp = rp.ToReadOnlyAsyncReactiveProperty(CancellationToken.None);

            var t = rrp.WaitAsync(cts.Token);

            cts.Cancel();

            rp.Value = 99;
            rp.Value = 100;

            await Assert.ThrowsAsync<OperationCanceledException>(async () => { await t; });
        }

    }


}
