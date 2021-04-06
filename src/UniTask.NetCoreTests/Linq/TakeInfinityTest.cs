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
    public class TakeInfinityTest
    {
        [Fact]
        public async Task Take()
        {
            var rp = new AsyncReactiveProperty<int>(1);

            var xs = rp.Take(5).ToArrayAsync();

            rp.Value = 2;
            rp.Value = 3;
            rp.Value = 4;
            rp.Value = 5;

            (await xs).Should().Equal(1, 2, 3, 4, 5);
        }

        [Fact]
        public async Task TakeWhile()
        {
            var rp = new AsyncReactiveProperty<int>(1);

            var xs = rp.TakeWhile(x => x != 5).ToArrayAsync();

            rp.Value = 2;
            rp.Value = 3;
            rp.Value = 4;
            rp.Value = 5;

            (await xs).Should().Equal(1, 2, 3, 4);
        }

        [Fact]
        public async Task TakeUntilCanceled()
        {
            var cts = new CancellationTokenSource();

            var rp = new AsyncReactiveProperty<int>(1);

            var xs = rp.TakeUntilCanceled(cts.Token).ToArrayAsync();

            var c = CancelAsync();

            await c;
            var foo = await xs;

            foo.Should().Equal(new[] { 1, 10, 20 });

            async Task CancelAsync()
            {
                rp.Value = 10;
                await Task.Yield();
                rp.Value = 20;
                await Task.Yield();
                cts.Cancel();
                rp.Value = 30;
                await Task.Yield();
                rp.Value = 40;
            }
        }

        [Fact]
        public async Task SkipUntilCanceled()
        {
            var cts = new CancellationTokenSource();

            var rp = new AsyncReactiveProperty<int>(1);

            var xs = rp.SkipUntilCanceled(cts.Token).ToArrayAsync();

            var c = CancelAsync();

            await c;
            var foo = await xs;

            foo.Should().Equal(new[] { 20, 30, 40 });

            async Task CancelAsync()
            {
                rp.Value = 10;
                await Task.Yield();
                rp.Value = 20;
                await Task.Yield();
                cts.Cancel();
                rp.Value = 30;
                await Task.Yield();
                rp.Value = 40;

                rp.Dispose(); // complete.
            }
        }

        [Fact]
        public async Task TakeUntil()
        {
            var cts = new AsyncReactiveProperty<int>(0);

            var rp = new AsyncReactiveProperty<int>(1);

            var xs = rp.TakeUntil(cts.WaitAsync()).ToArrayAsync();

            var c = CancelAsync();

            await c;
            var foo = await xs;

            foo.Should().Equal(new[] { 1, 10, 20 });

            async Task CancelAsync()
            {
                rp.Value = 10;
                await Task.Yield();
                rp.Value = 20;
                await Task.Yield();
                cts.Value = 9999;
                rp.Value = 30;
                await Task.Yield();
                rp.Value = 40;
            }
        }

        [Fact]
        public async Task SkipUntil()
        {
            var cts = new AsyncReactiveProperty<int>(0);

            var rp = new AsyncReactiveProperty<int>(1);

            var xs = rp.SkipUntil(cts.WaitAsync()).ToArrayAsync();

            var c = CancelAsync();

            await c;
            var foo = await xs;

            foo.Should().Equal(new[] { 20, 30, 40 });

            async Task CancelAsync()
            {
                rp.Value = 10;
                await Task.Yield();
                rp.Value = 20;
                await Task.Yield();
                cts.Value = 9999;
                rp.Value = 30;
                await Task.Yield();
                rp.Value = 40;

                rp.Dispose(); // complete.
            }
        }
    }
}
