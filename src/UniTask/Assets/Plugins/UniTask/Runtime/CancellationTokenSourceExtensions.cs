#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks.Triggers;
using System;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{

    public static class CancellationTokenSourceExtensions
    {
        public static void CancelAfterSlim(this CancellationTokenSource cts, int millisecondsDelay, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            var delay = UniTask.Delay(millisecondsDelay, delayType, delayTiming, cts.Token);
            CancelAfterCore(cts, delay).Forget();
        }

        public static void CancelAfterSlim(this CancellationTokenSource cts, TimeSpan delayTimeSpan, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            var delay = UniTask.Delay(delayTimeSpan, delayType, delayTiming, cts.Token);
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

        // TODO:delete this.
        public static IDisposable CancelAfterSlim2(this CancellationTokenSource cts, TimeSpan delayTimeSpan)
        {
            var timer = PlayerLoopTimer.Create(delayTimeSpan, DelayType.DeltaTime, PlayerLoopTiming.Update, cts.Token, state =>
            {
                var x = (CancellationTokenSource)state;
                x.Cancel();
            }, cts);
            timer.Restart();

            return timer;
        }

        abstract class PlayerLoopTimer : IDisposable, IPlayerLoopItem
        {
            readonly CancellationToken cancellationToken;
            readonly Action<object> timerCallback;
            readonly object state;
            readonly PlayerLoopTiming playerLoopTiming;

            bool isPlaying;
            bool isDisposed;

            protected PlayerLoopTimer(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
            {
                this.playerLoopTiming = playerLoopTiming;
                this.cancellationToken = cancellationToken;
                this.timerCallback = timerCallback;
                this.state = state;
            }

            public static PlayerLoopTimer Create(TimeSpan delayTimeSpan, DelayType delayType, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
            {
                switch (delayType)
                {
                    case DelayType.UnscaledDeltaTime:
                        return new IgnoreTimeScalePlayerLoopTimer(delayTimeSpan, playerLoopTiming, cancellationToken, timerCallback, state);
                    case DelayType.Realtime:
                        return new RealtimePlayerLoopTimer(delayTimeSpan, playerLoopTiming, cancellationToken, timerCallback, state);
                    case DelayType.DeltaTime:
                    default:
                        return new DeltaTimePlayerLoopTimer(delayTimeSpan, playerLoopTiming, cancellationToken, timerCallback, state);
                }
            }

            /// <summary>
            /// Restart(Reset and Start) timer.
            /// </summary>
            public void Restart()
            {
                if (isDisposed) throw new ObjectDisposedException(null);
                if (isPlaying) return;

                ResetCore(); // init state
                isPlaying = true;
                PlayerLoopHelper.AddAction(playerLoopTiming, this);
            }

            /// <summary>
            /// Stop timer.
            /// </summary>
            public void Stop()
            {
                isPlaying = false;
            }

            protected abstract void ResetCore();

            public void Dispose()
            {
                isDisposed = true;
            }

            bool IPlayerLoopItem.MoveNext()
            {
                if (isDisposed) return false;
                if (!isPlaying) return false;
                if (cancellationToken.IsCancellationRequested) return false;

                if (!MoveNextCore())
                {
                    timerCallback(state);
                    return false;
                }

                return true;
            }

            protected abstract bool MoveNextCore();
        }

        sealed class DeltaTimePlayerLoopTimer : PlayerLoopTimer
        {
            int initialFrame;
            float elapsed;
            readonly float delayTimeSpan;

            public DeltaTimePlayerLoopTimer(TimeSpan delayTimeSpan, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
                : base(playerLoopTiming, cancellationToken, timerCallback, state)
            {
                this.elapsed = 0.0f;
                this.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
                this.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
            }

            protected override bool MoveNextCore()
            {
                if (elapsed == 0.0f)
                {
                    if (initialFrame == Time.frameCount)
                    {
                        return true;
                    }
                }

                elapsed += Time.deltaTime;
                if (elapsed >= delayTimeSpan)
                {
                    return false;
                }

                return true;
            }

            protected override void ResetCore()
            {
                elapsed = 0.0f;
            }
        }

        sealed class IgnoreTimeScalePlayerLoopTimer : PlayerLoopTimer
        {
            int initialFrame;
            float elapsed;
            readonly float delayTimeSpan;

            public IgnoreTimeScalePlayerLoopTimer(TimeSpan delayTimeSpan, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
                : base(playerLoopTiming, cancellationToken, timerCallback, state)
            {
                this.elapsed = 0.0f;
                this.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
                this.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
            }

            protected override bool MoveNextCore()
            {
                if (elapsed == 0.0f)
                {
                    if (initialFrame == Time.frameCount)
                    {
                        return true;
                    }
                }

                elapsed += Time.unscaledDeltaTime;
                if (elapsed >= delayTimeSpan)
                {
                    return false;
                }

                return true;
            }

            protected override void ResetCore()
            {
                elapsed = 0.0f;
            }
        }

        sealed class RealtimePlayerLoopTimer : PlayerLoopTimer
        {
            ValueStopwatch stopwatch;
            readonly long delayTimeSpanTicks;

            public RealtimePlayerLoopTimer(TimeSpan delayTimeSpan, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
                : base(playerLoopTiming, cancellationToken, timerCallback, state)
            {
                this.stopwatch = ValueStopwatch.StartNew();
                this.delayTimeSpanTicks = delayTimeSpan.Ticks;
            }

            protected override bool MoveNextCore()
            {
                if (stopwatch.ElapsedTicks >= delayTimeSpanTicks)
                {
                    return false;
                }

                return true;
            }

            protected override void ResetCore()
            {
                this.stopwatch = ValueStopwatch.StartNew();
            }
        }
    }
}

