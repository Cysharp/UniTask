using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.TestTools;

namespace Cysharp.Threading.TasksTests
{
    public class Preserve
    {
        [UnityTest]
        public IEnumerator AwaitTwice() => UniTask.ToCoroutine(async () =>
        {
            var delay = UniTask.DelayFrame(5);
            await delay;

            try
            {
                await delay;
                Assert.Fail("should throw exception.");
            }
            catch (InvalidOperationException)
            {

            }
        });

        [UnityTest]
        public IEnumerator PreserveAllowTwice() => UniTask.ToCoroutine(async () =>
        {
            var delay = UniTask.DelayFrame(5, PlayerLoopTiming.PostLateUpdate).Preserve();
            var before = UnityEngine.Time.frameCount;
            await delay;
            var afterOne = UnityEngine.Time.frameCount;
            await delay;
            var afterTwo = UnityEngine.Time.frameCount;

            (afterOne - before).Should().Be(5);
            afterOne.Should().Be(afterTwo);
        });
    }
}
