using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
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

            (await xs).Should().BeEquivalentTo(1, 2, 3, 4, 5);
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

            (await xs).Should().BeEquivalentTo(1, 2, 3, 4);
        }
    }
}
