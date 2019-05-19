#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
using UniRx.Async.Triggers;
using System;

namespace UniRx.Async
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
            trigger.AddCancellationTriggerOnDestory(cts);
        }
    }
}

#endif