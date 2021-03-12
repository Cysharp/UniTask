using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.TestTools;

namespace Cysharp.Threading.TasksTests
{
    public class PlayerLoopTimerTest
    {
        void Between(TimeSpan l, TimeSpan data, TimeSpan r)
        {
            NUnit.Framework.Assert.AreEqual(l < data, true, "{0} < {1} failed.", l, data);
            NUnit.Framework.Assert.AreEqual(data < r, true, "{0} < {1} failed.", data, r);
        }

        [UnityTest]
        public IEnumerator StandardTicks() => UniTask.ToCoroutine(async () =>
        {
            foreach (var delay in new[] { DelayType.DeltaTime, DelayType.Realtime, DelayType.UnscaledDeltaTime })
            {
                var raisedTimeout = new UniTaskCompletionSource();
                PlayerLoopTimer.StartNew(TimeSpan.FromSeconds(1), false, delay, PlayerLoopTiming.Update, CancellationToken.None, _ =>
                {
                    raisedTimeout.TrySetResult();
                }, null);


                var sw = Stopwatch.StartNew();
                await raisedTimeout.Task;
                sw.Stop();

                Between(TimeSpan.FromSeconds(0.9), sw.Elapsed, TimeSpan.FromSeconds(1.1));
            }
        });


        [UnityTest]
        public IEnumerator Periodic() => UniTask.ToCoroutine(async () =>
        {
            var raisedTime = new List<DateTime>();
            var count = 0;
            var complete = new UniTaskCompletionSource();

            PlayerLoopTimer timer = null;
            timer = PlayerLoopTimer.StartNew(TimeSpan.FromSeconds(1), true, DelayType.DeltaTime, PlayerLoopTiming.Update, CancellationToken.None, _ =>
            {
                raisedTime.Add(DateTime.UtcNow);
                count++;
                if (count == 3)
                {
                    complete.TrySetResult();
                    timer.Dispose();
                }
            }, null);

            var start = DateTime.UtcNow;
            await complete.Task;

            Between(TimeSpan.FromSeconds(0.9), raisedTime[0] - start, TimeSpan.FromSeconds(1.1));
            Between(TimeSpan.FromSeconds(1.9), raisedTime[1] - start, TimeSpan.FromSeconds(2.1));
            Between(TimeSpan.FromSeconds(2.9), raisedTime[2] - start, TimeSpan.FromSeconds(3.1));
        });

        [UnityTest]
        public IEnumerator CancelAfterSlimTest() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();
            var complete = new UniTaskCompletionSource();
            cts.Token.RegisterWithoutCaptureExecutionContext(() =>
            {
                complete.TrySetResult();
            });

            cts.CancelAfterSlim(TimeSpan.FromSeconds(1));

            var sw = Stopwatch.StartNew();
            await complete.Task;

            Between(TimeSpan.FromSeconds(0.9), sw.Elapsed, TimeSpan.FromSeconds(1.1));
        });

        [UnityTest]
        public IEnumerator CancelAfterSlimCancelTest() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();
            var complete = new UniTaskCompletionSource();
            cts.Token.RegisterWithoutCaptureExecutionContext(() =>
            {
                complete.TrySetResult();
            });

            var d = cts.CancelAfterSlim(TimeSpan.FromSeconds(1));

            var sw = Stopwatch.StartNew();

            await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            d.Dispose();

            await UniTask.Delay(TimeSpan.FromSeconds(2));

            complete.Task.Status.Should().Be(UniTaskStatus.Pending);
        });

        [UnityTest]
        public IEnumerator TimeoutController() => UniTask.ToCoroutine(async () =>
        {
            var controller = new TimeoutController();

            var token = controller.Timeout(TimeSpan.FromSeconds(1));

            var complete = new UniTaskCompletionSource();
            token.RegisterWithoutCaptureExecutionContext(() =>
            {
                complete.TrySetResult();
            });

            var sw = Stopwatch.StartNew();
            await complete.Task;
            Between(TimeSpan.FromSeconds(0.9), sw.Elapsed, TimeSpan.FromSeconds(1.1));

            controller.IsTimeout().Should().BeTrue();
        });


        [UnityTest]
        public IEnumerator TimeoutReuse() => UniTask.ToCoroutine(async () =>
        {
            var controller = new TimeoutController(DelayType.DeltaTime);

            var token = controller.Timeout(TimeSpan.FromSeconds(2));

            var complete = new UniTaskCompletionSource();
            token.RegisterWithoutCaptureExecutionContext(() =>
            {
                complete.TrySetResult(); // reuse, used same token?
            });

            await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            controller.Reset();

            controller.IsTimeout().Should().BeFalse();

            var sw = Stopwatch.StartNew();

            controller.Timeout(TimeSpan.FromSeconds(5));

            await complete.Task;

            UnityEngine.Debug.Log(UnityEngine.Time.timeScale);
            Between(TimeSpan.FromSeconds(4.9), sw.Elapsed, TimeSpan.FromSeconds(5.1));

            controller.IsTimeout().Should().BeTrue();
        });

        [UnityTest]
        public IEnumerator LinkedTokenTest() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();

            var controller = new TimeoutController(cts);
            var token = controller.Timeout(TimeSpan.FromSeconds(2));

            await UniTask.DelayFrame(3);

            cts.Cancel();

            token.IsCancellationRequested.Should().BeTrue();

            controller.Dispose();
        });
    }
}
