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
    public class QueueTest
    {
        [Fact]
        public async Task Q()
        {
            var rp = new AsyncReactiveProperty<int>(100);

            var l = new List<int>();
            await rp.Take(10).Queue().ForEachAsync(x =>
            {
                rp.Value += 10;
                l.Add(x);
            });

            l.Should().Equal(100, 110, 120, 130, 140, 150, 160, 170, 180, 190);
        }
    }
}
