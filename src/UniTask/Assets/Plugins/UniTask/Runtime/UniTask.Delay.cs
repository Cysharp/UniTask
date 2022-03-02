#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public enum DelayType
    {
        /// <summary>use Time.deltaTime.</summary>
        DeltaTime,
        /// <summary>Ignore timescale, use Time.unscaledDeltaTime.</summary>
        UnscaledDeltaTime,
        /// <summary>use Stopwatch.GetTimestamp().</summary>
        Realtime
    }

    public partial struct UniTask
    {
        public static YieldAwaitable Yield()
        {
            // optimized for single continuation
            return new YieldAwaitable(PlayerLoopTiming.Update);
        }

        public static YieldAwaitable Yield(PlayerLoopTiming timing)
        {
            // optimized for single continuation
            return new YieldAwaitable(timing);
        }

        public static UniTask Yield(CancellationToken cancellationToken)
        {
            return new UniTask(YieldPromise.Create(PlayerLoopTiming.Update, cancellationToken, out var token), token);
        }

        public static UniTask Yield(PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            return new UniTask(YieldPromise.Create(timing, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame()
        {
            return new UniTask(NextFramePromise.Create(PlayerLoopTiming.Update, CancellationToken.None, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame(PlayerLoopTiming timing)
        {
            return new UniTask(NextFramePromise.Create(timing, CancellationToken.None, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame(CancellationToken cancellationToken)
        {
            return new UniTask(NextFramePromise.Create(PlayerLoopTiming.Update, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame(PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            return new UniTask(NextFramePromise.Create(timing, cancellationToken, out var token), token);
        }

        [Obsolete("Use WaitForEndOfFrame(MonoBehaviour) instead or UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate). Equivalent for coroutine's WaitForEndOfFrame requires MonoBehaviour(runner of Coroutine).")]
        public static YieldAwaitable WaitForEndOfFrame()
        {
            return UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
        }

        [Obsolete("Use WaitForEndOfFrame(MonoBehaviour) instead or UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate). Equivalent for coroutine's WaitForEndOfFrame requires MonoBehaviour(runner of Coroutine).")]
        public static UniTask WaitForEndOfFrame(CancellationToken cancellationToken)
        {
            return UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancellationToken);
        }

        public static UniTask WaitForEndOfFrame(MonoBehaviour coroutineRunner, CancellationToken cancellationToken = default)
        {
            var source = WaitForEndOfFramePromise.Create(coroutineRunner, cancellationToken, out var token);
            return new UniTask(source, token);
        }

        /// <summary>
        /// Same as UniTask.Yield(PlayerLoopTiming.FixedUpdate).
        /// </summary>
        public static YieldAwaitable WaitForFixedUpdate()
        {
            return UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }

        /// <summary>
        /// Same as UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken).
        /// </summary>
        public static UniTask WaitForFixedUpdate(CancellationToken cancellationToken)
        {
            return UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
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
            return Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cancellationToken);
        }

        public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayType = ignoreTimeScale ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime;
            return Delay(delayTimeSpan, delayType, delayTiming, cancellationToken);
        }

        public static UniTask Delay(int millisecondsDelay, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            return Delay(delayTimeSpan, delayType, delayTiming, cancellationToken);
        }

        public static UniTask Delay(TimeSpan delayTimeSpan, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayTimeSpan. delayTimeSpan:" + delayTimeSpan);
            }

#if UNITY_EDITOR
            // force use Realtime.
            if (PlayerLoopHelper.IsMainThread && !UnityEditor.EditorApplication.isPlaying)
            {
                delayType = DelayType.Realtime;
            }
#endif

            switch (delayType)
            {
                case DelayType.UnscaledDeltaTime:
                    {
                        return new UniTask(DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token);
                    }
                case DelayType.Realtime:
                    {
                        return new UniTask(DelayRealtimePromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token);
                    }
                case DelayType.DeltaTime:
                default:
                    {
                        return new UniTask(DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, out var token), token);
                    }
            }
        }

        sealed class YieldPromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<YieldPromise>
        {
            static TaskPool<YieldPromise> pool;
            YieldPromise nextNode;
            public ref YieldPromise NextNode => ref nextNode;

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
        }

        sealed class NextFramePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<NextFramePromise>
        {
            static TaskPool<NextFramePromise> pool;
            NextFramePromise nextNode;
            public ref NextFramePromise NextNode => ref nextNode;

            static NextFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(NextFramePromise), () => pool.Size);
            }

            int frameCount;
            CancellationToken cancellationToken;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            NextFramePromise()
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
                    result = new NextFramePromise();
                }

                result.frameCount = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
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

                if (frameCount == Time.frameCount)
                {
                    return true;
                }

                core.TrySetResult(AsyncUnit.Default);
                return false;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        sealed class WaitForEndOfFramePromise : IUniTaskSource, ITaskPoolNode<WaitForEndOfFramePromise>, System.Collections.IEnumerator
        {
            static TaskPool<WaitForEndOfFramePromise> pool;
            WaitForEndOfFramePromise nextNode;
            public ref WaitForEndOfFramePromise NextNode => ref nextNode;

            static WaitForEndOfFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(WaitForEndOfFramePromise), () => pool.Size);
            }

            CancellationToken cancellationToken;
            UniTaskCompletionSourceCore<object> core;

            WaitForEndOfFramePromise()
            {
            }

            public static IUniTaskSource Create(MonoBehaviour coroutineRunner, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new WaitForEndOfFramePromise();
                }

                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                coroutineRunner.StartCoroutine(result);

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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                Reset(); // Reset Enumerator
                cancellationToken = default;
                return pool.TryPush(this);
            }

            // Coroutine Runner implementation

            static readonly WaitForEndOfFrame waitForEndOfFrameYieldInstruction = new WaitForEndOfFrame();
            bool isFirst = true;

            object IEnumerator.Current => waitForEndOfFrameYieldInstruction;

            bool IEnumerator.MoveNext()
            {
                if (isFirst)
                {
                    isFirst = false;
                    return true; // start WaitForEndOfFrame
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                core.TrySetResult(null);
                return false;
            }

            public void Reset()
            {
                isFirst = true;
            }
        }

        sealed class DelayFramePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayFramePromise>
        {
            static TaskPool<DelayFramePromise> pool;
            DelayFramePromise nextNode;
            public ref DelayFramePromise NextNode => ref nextNode;

            static DelayFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayFramePromise), () => pool.Size);
            }

            int initialFrame;
            int delayFrameCount;
            CancellationToken cancellationToken;

            int currentFrameCount;
            UniTaskCompletionSourceCore<AsyncUnit> core;

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
                result.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;

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

                if (currentFrameCount == 0)
                {
                    if (delayFrameCount == 0) // same as Yield
                    {
                        core.TrySetResult(AsyncUnit.Default);
                        return false;
                    }

                    // skip in initial frame.
                    if (initialFrame == Time.frameCount)
                    {
#if UNITY_EDITOR
                        // force use Realtime.
                        if (PlayerLoopHelper.IsMainThread && !UnityEditor.EditorApplication.isPlaying)
                        {
                            //goto ++currentFrameCount
                        }
                        else
                        {
                            return true;
                        }
#else
                        return true;
#endif
                    }
                }

                if (++currentFrameCount >= delayFrameCount)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

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
        }

        sealed class DelayPromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayPromise>
        {
            static TaskPool<DelayPromise> pool;
            DelayPromise nextNode;
            public ref DelayPromise NextNode => ref nextNode;

            static DelayPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayPromise), () => pool.Size);
            }

            int initialFrame;
            float delayTimeSpan;
            float elapsed;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<object> core;

            DelayPromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
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
                result.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;

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
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        sealed class DelayIgnoreTimeScalePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayIgnoreTimeScalePromise>
        {
            static TaskPool<DelayIgnoreTimeScalePromise> pool;
            DelayIgnoreTimeScalePromise nextNode;
            public ref DelayIgnoreTimeScalePromise NextNode => ref nextNode;

            static DelayIgnoreTimeScalePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayIgnoreTimeScalePromise), () => pool.Size);
            }

            float delayFrameTimeSpan;
            float elapsed;
            int initialFrame;
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
                result.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
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

                if (elapsed == 0.0f)
                {
                    if (initialFrame == Time.frameCount)
                    {
                        return true;
                    }
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
        }

        sealed class DelayRealtimePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayRealtimePromise>
        {
            static TaskPool<DelayRealtimePromise> pool;
            DelayRealtimePromise nextNode;
            public ref DelayRealtimePromise NextNode => ref nextNode;

            static DelayRealtimePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayRealtimePromise), () => pool.Size);
            }

            long delayTimeSpanTicks;
            ValueStopwatch stopwatch;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            DelayRealtimePromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayRealtimePromise();
                }

                result.stopwatch = ValueStopwatch.StartNew();
                result.delayTimeSpanTicks = delayTimeSpan.Ticks;
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

                if (stopwatch.IsInvalid)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                if (stopwatch.ElapsedTicks >= delayTimeSpanTicks)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                stopwatch = default;
                cancellationToken = default;
                return pool.TryPush(this);
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
