#pragma warning disable CS0618

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
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
    public class DelayTest
    {
        [UnityTest]
        public IEnumerator DelayFrame() => UniTask.ToCoroutine(async () =>
        {
            for (int i = 1; i < 5; i++)
            {
                await UniTask.Yield(PlayerLoopTiming.PreUpdate);
                var frameCount = Time.frameCount;
                await UniTask.DelayFrame(i);
                Time.frameCount.Should().Be(frameCount + i);
            }

            for (int i = 1; i < 5; i++)
            {
                await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
                var frameCount = Time.frameCount;
                await UniTask.DelayFrame(i);
                Time.frameCount.Should().Be(frameCount + i);
            }
        });

        [UnityTest]
        public IEnumerator DelayFrameZero() => UniTask.ToCoroutine(async () =>
        {
            {
                await UniTask.Yield(PlayerLoopTiming.PreUpdate);
                var frameCount = Time.frameCount;
                await UniTask.DelayFrame(0);
                Time.frameCount.Should().Be(frameCount); // same frame
            }
            {
                await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
                var frameCount = Time.frameCount;
                await UniTask.DelayFrame(0);
                Time.frameCount.Should().Be(frameCount + 1); // next frame
            }
        });



        [UnityTest]
        public IEnumerator TimerFramePre() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.PreUpdate);

            var initialFrame = Time.frameCount;
            var xs = await UniTaskAsyncEnumerable.TimerFrame(2, 3).Take(5).Select(_ => Time.frameCount).ToArrayAsync();

            xs[0].Should().Be(initialFrame + 2);
            xs[1].Should().Be(initialFrame + 2 + (3 * 1));
            xs[2].Should().Be(initialFrame + 2 + (3 * 2));
            xs[3].Should().Be(initialFrame + 2 + (3 * 3));
            xs[4].Should().Be(initialFrame + 2 + (3 * 4));
        });


        [UnityTest]
        public IEnumerator TimerFramePost() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

            var initialFrame = Time.frameCount;
            var xs = await UniTaskAsyncEnumerable.TimerFrame(2, 3).Take(5).Select(_ => Time.frameCount).ToArrayAsync();

            xs[0].Should().Be(initialFrame + 2);
            xs[1].Should().Be(initialFrame + 2 + (3 * 1));
            xs[2].Should().Be(initialFrame + 2 + (3 * 2));
            xs[3].Should().Be(initialFrame + 2 + (3 * 3));
            xs[4].Should().Be(initialFrame + 2 + (3 * 4));
        });


        [UnityTest]
        public IEnumerator TimerFrameTest() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.PreUpdate);

            var initialFrame = Time.frameCount;
            var xs = await UniTaskAsyncEnumerable.TimerFrame(0, 0).Take(5).Select(_ => Time.frameCount).ToArrayAsync();

            xs[0].Should().Be(initialFrame);
            xs[1].Should().Be(initialFrame + 1);
            xs[2].Should().Be(initialFrame + 2);
            xs[3].Should().Be(initialFrame + 3);
            xs[4].Should().Be(initialFrame + 4);
        });


        [UnityTest]
        public IEnumerator TimerFrameSinglePre() => UniTask.ToCoroutine(async () =>
        {
            {
                await UniTask.Yield(PlayerLoopTiming.PreUpdate);
                var initialFrame = Time.frameCount;
                var xs = await UniTaskAsyncEnumerable.TimerFrame(0).Select(_ => Time.frameCount).ToArrayAsync();
                xs[0].Should().Be(initialFrame);

            }
            {
                await UniTask.Yield(PlayerLoopTiming.PreUpdate);
                var initialFrame = Time.frameCount;

                var xs = await UniTaskAsyncEnumerable.TimerFrame(1).Select(_ =>
                {
                    var t = Time.frameCount;

                    return t;
                }).ToArrayAsync();

                xs[0].Should().Be(initialFrame + 1);
            }
            {
                await UniTask.Yield(PlayerLoopTiming.PreUpdate);
                var initialFrame = Time.frameCount;
                var xs = await UniTaskAsyncEnumerable.TimerFrame(2).Select(_ => Time.frameCount).ToArrayAsync();
                xs[0].Should().Be(initialFrame + 2);
            }
        });


        [UnityTest]
        public IEnumerator TimerFrameSinglePost() => UniTask.ToCoroutine(async () =>
        {
            {
                //await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
                //var initialFrame = Time.frameCount;
                //var xs = await UniTaskAsyncEnumerable.TimerFrame(0).Select(_ => Time.frameCount).ToArrayAsync();
                //xs[0].Should().Be(initialFrame);
            }
            {
                //await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
                var initialFrame = Time.frameCount;
                var xs = await UniTaskAsyncEnumerable.TimerFrame(1).Select(_ => Time.frameCount).ToArrayAsync();
                xs[0].Should().Be(initialFrame + 1);
            }
            {
                //await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
                var initialFrame = Time.frameCount;
                var xs = await UniTaskAsyncEnumerable.TimerFrame(2).Select(_ => Time.frameCount).ToArrayAsync();
                xs[0].Should().Be(initialFrame + 2);
            }
        });



        [UnityTest]
        public IEnumerator Timer() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.PreUpdate);

            {
                var initialSeconds = Time.realtimeSinceStartup;
                var xs = await UniTaskAsyncEnumerable.Timer(TimeSpan.FromSeconds(2)).Select(_ => Time.realtimeSinceStartup).ToArrayAsync();

                Mathf.Approximately(initialSeconds, xs[0]).Should().BeFalse();
                Debug.Log("Init:" + initialSeconds);
                Debug.Log("After:" + xs[0]);
            }
        });

#if !UNITY_WEBGL

        [UnityTest]
        public IEnumerator DelayInThreadPool() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Run(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2));
            });
        });

#endif

        [UnityTest]
        public IEnumerator DelayRealtime() => UniTask.ToCoroutine(async () =>
        {
            var now = DateTimeOffset.UtcNow;

            await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.Realtime);

            var elapsed = DateTimeOffset.UtcNow - now;

            var okay1 = TimeSpan.FromSeconds(1.80) <= elapsed;
            var okay2 = elapsed <= TimeSpan.FromSeconds(2.20);

            okay1.Should().Be(true);
            okay2.Should().Be(true);
        });


        [UnityTest]
        public IEnumerator LoopTest() => UniTask.ToCoroutine(async () =>
        {
            for (int i = 0; i < 20; ++i)
            {
                UniTask.DelayFrame(100).Forget();
                await UniTask.DelayFrame(1);
            }
        });
    }
}