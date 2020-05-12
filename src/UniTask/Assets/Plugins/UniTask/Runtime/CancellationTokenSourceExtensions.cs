#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks.Triggers;
using System;

namespace Cysharp.Threading.Tasks
{
    public static class CancellationTokenSourceExtensions
    {
        public static void CancelAfterSlim(this CancellationTokenSource cts, int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            var delay = UniTask.Delay(millisecondsDelay, ignoreTimeScale, delayTiming, cts.Token);
            CancelAfterCore(cts, delay).Forget();
        }

        public static void CancelAfterSlim(this CancellationTokenSource cts, TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            var delay = UniTask.Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cts.Token);
            CancelAfterCore(cts, delay).Forget();
        }

        static async UniTaskVoid CancelAfterCore(CancellationTokenSource cts, UniTask delayTask)
        {
            var alreadyCanceled = await delayTask.SuppressCancellationThrow();
            if (!alreadyCanceled)
            {
                cts.Cancel();
                cts.Dispose();
            }
        }

        public static void RegisterRaiseCancelOnDestroy(this CancellationTokenSource cts, Component component)
        {
            RegisterRaiseCancelOnDestroy(cts, component.gameObject);
        }

        public static void RegisterRaiseCancelOnDestroy(this CancellationTokenSource cts, GameObject gameObject)
        {
            var trigger = gameObject.GetAsyncDestroyTrigger();
            trigger.CancellationToken.RegisterWithoutCaptureExecutionContext(state =>
            {
                var cts2 = (CancellationTokenSource)state;
                cts2.Cancel();
            }, cts);
        }
    }
}

