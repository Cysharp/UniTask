using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class WhenEachTest
    {
        [Fact]
        public async Task Each()
        {
            var a = Delay(1, 3000);
            var b = Delay(2, 1000);
            var c = Delay(3, 2000);

            var l = new List<int>();
            await foreach (var item in UniTask.WhenEach(a, b, c))
            {
                l.Add(item.Result);
            }

            l.Should().Equal(2, 3, 1);
        }

        [Fact]
        public async Task Error()
        {
            var a = Delay2(1, 3000);
            var b = Delay2(2, 1000);
            var c = Delay2(3, 2000);

            var l = new List<WhenEachResult<int>>();
            await foreach (var item in UniTask.WhenEach(a, b, c))
            {
                l.Add(item);
            }

            l[0].IsCompletedSuccessfully.Should().BeTrue();
            l[0].IsFaulted.Should().BeFalse();
            l[0].Result.Should().Be(2);

            l[1].IsCompletedSuccessfully.Should().BeFalse();
            l[1].IsFaulted.Should().BeTrue();
            l[1].Exception.Message.Should().Be("ERROR");

            l[2].IsCompletedSuccessfully.Should().BeTrue();
            l[2].IsFaulted.Should().BeFalse();
            l[2].Result.Should().Be(1);
        }

        [Fact]
        public async Task DisposeBeforeFirstMoveDoesNotEnumerateInput()
        {
            var inputEnumerated = false;

            IEnumerable<UniTask<int>> Tasks()
            {
                inputEnumerated = true;
                yield return UniTask.FromResult(1);
            }

            var enumerator = UniTask.WhenEach(Tasks()).GetAsyncEnumerator();
            await enumerator.DisposeAsync();
            await enumerator.DisposeAsync();

            var moveNext = enumerator.MoveNextAsync();
            Assert.Equal(UniTaskStatus.Succeeded, moveNext.Status);
            Assert.False(await moveNext);
            Assert.False(inputEnumerated);
        }

        async UniTask<int> Delay(int id, int sleep)
        {
            await Task.Delay(sleep);
            return id;
        }

        async UniTask<int> Delay2(int id, int sleep)
        {
            await Task.Delay(sleep);
            if (id == 3) throw new Exception("ERROR");
            return id;
        }
    }
}
