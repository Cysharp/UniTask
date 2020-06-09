using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Cysharp.Threading.TasksTests
{
    public class Preserve
    {
        public Preserve()
        {
            // TaskPool.SetMaxPoolSize(0);
        }

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
            await UniTask.Yield(PlayerLoopTiming.Update);

            var delay = UniTask.DelayFrame(5, PlayerLoopTiming.PostLateUpdate).Preserve();

            var before = UnityEngine.Time.frameCount; // 0

            await delay;
            var afterOne = UnityEngine.Time.frameCount; // 5

            await delay;
            var afterTwo = UnityEngine.Time.frameCount; // 5

            (afterOne - before).Should().Be(5);
            afterOne.Should().Be(afterTwo);
        });
    }
}
