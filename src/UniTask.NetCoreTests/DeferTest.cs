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
    }


}
