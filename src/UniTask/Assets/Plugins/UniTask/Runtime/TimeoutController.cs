#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using System;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    // CancellationTokenSource itself can not reuse but CancelAfter(Timeout.InfiniteTimeSpan) allows reuse if did not reach timeout.
    // Similar discussion:
    // https://github.com/dotnet/runtime/issues/4694
    // https://github.com/dotnet/runtime/issues/48492
    // This TimeoutController emulate similar implementation, using CancelAfterSlim; to achieve zero allocation timeout.

    public sealed class TimeoutController : IDisposable
    {
        CancellationTokenSource timeoutSource;
        CancellationTokenSource linkedSource;
        StoppableDelayRealtimePromise timeoutDelay;

        readonly CancellationTokenSource originalLinkCancellationTokenSource;

        public TimeoutController()
        {
            this.timeoutSource = new CancellationTokenSource();
            this.originalLinkCancellationTokenSource = null;
            this.linkedSource = null;
            this.timeoutDelay = null;
        }

        public TimeoutController(CancellationTokenSource linkCancellationTokenSource)
        {
            this.timeoutSource = new CancellationTokenSource();
            this.originalLinkCancellationTokenSource = linkCancellationTokenSource;
            this.linkedSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutSource.Token, linkCancellationTokenSource.Token);
            this.timeoutDelay = null;
        }

        public CancellationToken Timeout(TimeSpan timeout)
        {
            if (originalLinkCancellationTokenSource != null && originalLinkCancellationTokenSource.IsCancellationRequested)
            {
                return originalLinkCancellationTokenSource.Token;
            }

            if (timeoutSource.IsCancellationRequested)
            {
                timeoutSource.Dispose();
                timeoutSource = new CancellationTokenSource();
                if (linkedSource != null)
                {
                    this.linkedSource.Cancel();
                    this.linkedSource.Dispose();
                    this.linkedSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutSource.Token, originalLinkCancellationTokenSource.Token);
                }
            }

            if (timeoutDelay == null)
            {
                RunDelayAsync(timeout).Forget(); // timeoutDelay = ... in RunDelayAsync(immediately, before await)
            }
            else
            {
                timeoutDelay.RestartStopwatch(); // already running RunDelayAsync
            }

            return (linkedSource != null) ? linkedSource.Token : timeoutSource.Token;
        }

        public bool IsTimeout()
        {
            return timeoutSource.IsCancellationRequested;
        }

        public void Reset()
        {
            if (timeoutDelay != null)
            {
                timeoutDelay.Stop(); // stop delay, will finish RunDelayAsync
                timeoutDelay = null;
            }
        }

        async UniTaskVoid RunDelayAsync(TimeSpan timeout)
        {
            timeoutDelay = StoppableDelayRealtimePromise.Create(timeout, PlayerLoopTiming.Update, (linkedSource == null) ? CancellationToken.None : linkedSource.Token, out var version);
            try
            {
                var reason = await new UniTask<DelayResult>(timeoutDelay, version);
                if (reason == DelayResult.DelayCompleted)
                {
                    // UnityEngine.Debug.Log("DEBUG:Timeout Complete, try to call timeoutSource.Cancel");
                    timeoutSource.Cancel();
                }
                else if (reason == DelayResult.LinkedTokenCanceled)
                {
                    // UnityEngine.Debug.Log("DEBUG:LinkedSource IsCancellationRequested");
                }
                else if (reason == DelayResult.ExternalStopped)
                {
                    // Reset(Promise.Stop) called, do nothing.
                    // UnityEngine.Debug.Log("DEBUG:Reset called");
                }
            }
            finally
            {
                timeoutDelay = null;
            }
        }

        public void Dispose()
        {
            if (timeoutDelay != null)
            {
                timeoutDelay.Stop();
            }
            timeoutSource.Dispose();
            if (linkedSource != null)
            {
                linkedSource.Dispose();
            }
        }

        enum DelayResult
        {
            LinkedTokenCanceled,
            ExternalStopped,
            DelayCompleted, // as Timeout.
        }

        // Stop + SuppressCancellationThrow.
        sealed class StoppableDelayRealtimePromise : IUniTaskSource<DelayResult>, IPlayerLoopItem, ITaskPoolNode<StoppableDelayRealtimePromise>
        {
            static OperationCanceledException ExterenalStopException = new OperationCanceledException();

            static TaskPool<StoppableDelayRealtimePromise> pool;
            StoppableDelayRealtimePromise nextNode;
            public ref StoppableDelayRealtimePromise NextNode => ref nextNode;

            static StoppableDelayRealtimePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(StoppableDelayRealtimePromise), () => pool.Size);
            }

            long delayTimeSpanTicks;
            ValueStopwatch stopwatch;
            CancellationToken cancellationToken;
            bool externalStop;

            UniTaskCompletionSourceCore<DelayResult> core;

            StoppableDelayRealtimePromise()
            {
            }

            public static StoppableDelayRealtimePromise Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (!pool.TryPop(out var result))
                {
                    result = new StoppableDelayRealtimePromise();
                }

                result.stopwatch = ValueStopwatch.StartNew();
                result.delayTimeSpanTicks = delayTimeSpan.Ticks;
                result.cancellationToken = cancellationToken;
                result.externalStop = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void Stop()
            {
                externalStop = true;
            }

            public void RestartStopwatch()
            {
                stopwatch = ValueStopwatch.StartNew();
            }

            public DelayResult GetResult(short token)
            {
                try
                {
                    return core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
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
                    core.TrySetResult(DelayResult.LinkedTokenCanceled);
                    return false;
                }

                if (externalStop)
                {
                    core.TrySetResult(DelayResult.ExternalStopped);
                    return false;
                }

                if (stopwatch.IsInvalid)
                {
                    core.TrySetResult(DelayResult.DelayCompleted);
                    return false;
                }

                if (stopwatch.ElapsedTicks >= delayTimeSpanTicks)
                {
                    core.TrySetResult(DelayResult.DelayCompleted);
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
                externalStop = false;
                return pool.TryPush(this);
            }
        }
    }
}