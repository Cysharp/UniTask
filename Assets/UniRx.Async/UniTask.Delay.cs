#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UniRx.Async.Internal;
using UnityEngine;

namespace UniRx.Async
{
    // TODO:rename
    public partial struct UniTask2
    {
        public static YieldAwaitable2 Yield(PlayerLoopTiming timing = PlayerLoopTiming.Update)
        {
            // optimized for single continuation
            return new YieldAwaitable2(timing);
        }

        public static UniTask2 Yield(PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            return new UniTask2(YieldPromise.Create(timing, cancellationToken, out var token), token);
        }

        public static UniTask2 DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount);
            }

            return new UniTask2(DelayFramePromise.Create(delayFrameCount, delayTiming, cancellationToken, out var token), token);
        }

        public static UniTask2 Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus millisecondsDelay. millisecondsDelay:" + millisecondsDelay);
            }

            return (ignoreTimeScale)
                ? new UniTask2(DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token)
                : new UniTask2(DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, out token), token);
        }

        public static UniTask2 Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayTimeSpan. delayTimeSpan:" + delayTimeSpan);
            }

            return (ignoreTimeScale)
                ? new UniTask2(DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token)
                : new UniTask2(DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, out token), token);
        }

        class YieldPromise : IUniTaskSource, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<YieldPromise> pool = new PromisePool<YieldPromise>();

            CancellationToken cancellationToken;
            UniTaskCompletionSourceCore<object> core;

            YieldPromise()
            {
            }

            public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new YieldPromise();

                result.cancellationToken = cancellationToken;

                TaskTracker2.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    TaskTracker2.RemoveTracking(this);
                    core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
                }
            }

            public AwaiterStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AwaiterStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.SetCanceled(cancellationToken);
                    return false;
                }

                core.SetResult(null);
                return false;
            }

            public void Reset()
            {
                core.Reset();
                cancellationToken = default;
            }
        }

        class DelayFramePromise : IUniTaskSource, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<DelayFramePromise> pool = new PromisePool<DelayFramePromise>();

            int delayFrameCount;
            CancellationToken cancellationToken;

            int currentFrameCount;
            UniTaskCompletionSourceCore<object> core;

            DelayFramePromise()
            {
            }

            public static IUniTaskSource Create(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new DelayFramePromise();

                result.delayFrameCount = delayFrameCount;
                result.cancellationToken = cancellationToken;

                TaskTracker2.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    TaskTracker2.RemoveTracking(this);
                    core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
                }
            }

            public AwaiterStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AwaiterStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.SetCanceled(cancellationToken);
                    return false;
                }

                if (currentFrameCount == delayFrameCount)
                {
                    core.SetResult(null);
                    return false;
                }

                currentFrameCount++;
                return true;
            }

            public void Reset()
            {
                core.Reset();
                currentFrameCount = default;
                delayFrameCount = default;
                cancellationToken = default;
            }
        }

        class DelayPromise : IUniTaskSource, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<DelayPromise> pool = new PromisePool<DelayPromise>();

            float delayFrameTimeSpan;
            float elapsed;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<object> core;

            DelayPromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new DelayPromise();

                result.elapsed = 0.0f;
                result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;

                TaskTracker2.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    TaskTracker2.RemoveTracking(this);
                    core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
                }
            }

            public AwaiterStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AwaiterStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.SetCanceled(cancellationToken);
                    return false;
                }

                elapsed += Time.deltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    core.SetResult(null);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                delayFrameTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
            }
        }

        class DelayIgnoreTimeScalePromise : IUniTaskSource, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<DelayIgnoreTimeScalePromise> pool = new PromisePool<DelayIgnoreTimeScalePromise>();

            float delayFrameTimeSpan;
            float elapsed;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<object> core;

            DelayIgnoreTimeScalePromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new DelayIgnoreTimeScalePromise();

                result.elapsed = 0.0f;
                result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;

                TaskTracker2.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    TaskTracker2.RemoveTracking(this);
                    core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
                }
            }

            public AwaiterStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public AwaiterStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.SetCanceled(cancellationToken);
                    return false;
                }

                elapsed += Time.unscaledDeltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    core.SetResult(null);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                delayFrameTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
            }
        }
    }

    // TODO:rename
    public struct YieldAwaitable2
    {
        readonly PlayerLoopTiming timing;

        public YieldAwaitable2(PlayerLoopTiming timing)
        {
            this.timing = timing;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(timing);
        }

        public UniTask2 ToUniTask()
        {
            return UniTask2.Yield(timing, CancellationToken.None);
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


    // TODO:remove
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

    // TODO:remove
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