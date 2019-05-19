#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UniRx.Async.Internal;
using UnityEngine;

namespace UniRx.Async
{
    public partial struct UniTask
    {
        public static YieldAwaitable Yield(PlayerLoopTiming timing = PlayerLoopTiming.Update)
        {
            // optimized for single continuation
            return new YieldAwaitable(timing);
        }

        public static UniTask Yield(PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            return new UniTask(new YieldPromise(timing, cancellationToken));
        }

        public static UniTask<int> DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount);
            }

            var source = new DelayFramePromise(delayFrameCount, delayTiming, cancellationToken);
            return source.Task;
        }

        public static UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayTimeSpan:" + delayTimeSpan);
            }

            return (ignoreTimeScale)
                ? new DelayIgnoreTimeScalePromise(delayTimeSpan, delayTiming, cancellationToken).Task
                : new DelayPromise(delayTimeSpan, delayTiming, cancellationToken).Task;
        }

        public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayTimeSpan:" + delayTimeSpan);
            }

            return (ignoreTimeScale)
                ? new DelayIgnoreTimeScalePromise(delayTimeSpan, delayTiming, cancellationToken).Task
                : new DelayPromise(delayTimeSpan, delayTiming, cancellationToken).Task;
        }

        class YieldPromise : PlayerLoopReusablePromiseBase
        {
            public YieldPromise(PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
            }

            protected override void OnRunningStart()
            {
            }

            public override bool MoveNext()
            {
                Complete();
                if (cancellationToken.IsCancellationRequested)
                {
                    TrySetCanceled();
                }
                else
                {
                    TrySetResult();
                }

                return false;
            }
        }

        class DelayFramePromise : PlayerLoopReusablePromiseBase<int>
        {
            readonly int delayFrameCount;
            int currentFrameCount;

            public DelayFramePromise(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
                this.delayFrameCount = delayFrameCount;
                this.currentFrameCount = 0;
            }

            protected override void OnRunningStart()
            {
                currentFrameCount = 0;
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                if (currentFrameCount == delayFrameCount)
                {
                    Complete();
                    TrySetResult(currentFrameCount);
                    return false;
                }

                currentFrameCount++;
                return true;
            }
        }

        class DelayPromise : PlayerLoopReusablePromiseBase
        {
            readonly float delayFrameTimeSpan;
            float elapsed;

            public DelayPromise(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
                this.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
            }

            protected override void OnRunningStart()
            {
                this.elapsed = 0.0f;
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                elapsed += Time.deltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    Complete();
                    TrySetResult();
                    return false;
                }

                return true;
            }
        }

        class DelayIgnoreTimeScalePromise : PlayerLoopReusablePromiseBase
        {
            readonly float delayFrameTimeSpan;
            float elapsed;

            public DelayIgnoreTimeScalePromise(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 2)
            {
                this.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
            }

            protected override void OnRunningStart()
            {
                this.elapsed = 0.0f;
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                elapsed += Time.unscaledDeltaTime;

                if (elapsed >= delayFrameTimeSpan)
                {
                    Complete();
                    TrySetResult();
                    return false;
                }

                return true;
            }
        }
    }

    public struct YieldAwaitable
    {
        readonly PlayerLoopTiming timing;

        public YieldAwaitable(PlayerLoopTiming timing)
        {
            this.timing = timing;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(timing);
        }

        public UniTask ToUniTask()
        {
            return UniTask.Yield(timing, CancellationToken.None);
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            readonly PlayerLoopTiming timing;

            public Awaiter(PlayerLoopTiming timing)
            {
                this.timing = timing;
            }

            public bool IsCompleted => false;

            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }
        }
    }
}
#endif