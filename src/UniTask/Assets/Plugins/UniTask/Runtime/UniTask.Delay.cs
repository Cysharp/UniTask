#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks
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
            return new UniTask(YieldPromise.Create(timing, cancellationToken, out var token), token);
        }

        public static UniTask DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount);
            }

            return new UniTask(DelayFramePromise.Create(delayFrameCount, delayTiming, cancellationToken, out var token), token);
        }

        public static UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus millisecondsDelay. millisecondsDelay:" + millisecondsDelay);
            }

            return (ignoreTimeScale)
                ? new UniTask(DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token)
                : new UniTask(DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, out token), token);
        }

        public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayTimeSpan. delayTimeSpan:" + delayTimeSpan);
            }

            return (ignoreTimeScale)
                ? new UniTask(DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token)
                : new UniTask(DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, out token), token);
        }

        sealed class YieldPromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<YieldPromise>
        {
            static TaskPool<YieldPromise> pool;
            public YieldPromise NextNode { get; set; }

            static YieldPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(YieldPromise), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new YieldPromise();
                }


                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                core.TrySetResult(null);
                return false;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationToken = default;
                return pool.TryPush(this);
            }

            ~YieldPromise()
            {
                if (TryReturn())
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        sealed class DelayFramePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayFramePromise>
        {
            static TaskPool<DelayFramePromise> pool;
            public DelayFramePromise NextNode { get; set; }

            static DelayFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayFramePromise), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new DelayFramePromise();
                }

                result.delayFrameCount = delayFrameCount;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (currentFrameCount == delayFrameCount)
                {
                    core.TrySetResult(null);
                    return false;
                }

                currentFrameCount++;
                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                currentFrameCount = default;
                delayFrameCount = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }

            ~DelayFramePromise()
            {
                if (TryReturn())
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        sealed class DelayPromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayPromise>
        {
            static TaskPool<DelayPromise> pool;
            public DelayPromise NextNode { get; set; }

            static DelayPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayPromise), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new DelayPromise();
                }

                result.elapsed = 0.0f;
                result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                elapsed += Time.deltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayFrameTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }

            ~DelayPromise()
            {
                if (TryReturn())
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        sealed class DelayIgnoreTimeScalePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayIgnoreTimeScalePromise>
        {
            static TaskPool<DelayIgnoreTimeScalePromise> pool;
            public DelayIgnoreTimeScalePromise NextNode { get; set; }

            static DelayIgnoreTimeScalePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayIgnoreTimeScalePromise), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new DelayIgnoreTimeScalePromise();
                }

                result.elapsed = 0.0f;
                result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                elapsed += Time.unscaledDeltaTime;
                if (elapsed >= delayFrameTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayFrameTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }

            ~DelayIgnoreTimeScalePromise()
            {
                if (TryReturn())
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }
    }

    public readonly struct YieldAwaitable
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

        public readonly struct Awaiter : ICriticalNotifyCompletion
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
