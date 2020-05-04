#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6)) && ENABLE_MANAGED_JOBS
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using Unity.Jobs;

namespace UniRx.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static UniTask.Awaiter GetAwaiter(this JobHandle jobHandle)
        {
            var handler = JobHandlePromise.Create(jobHandle, CancellationToken.None, out var token);
            if (handler.GetStatus(token).IsCompleted() && handler is JobHandlePromise loopItem)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreUpdate, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreLateUpdate, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PostLateUpdate, loopItem);
            }

            return new UniTask(handler, token).GetAwaiter();
        }

        public static UniTask ToUniTask(this JobHandle jobHandle, CancellationToken cancellation = default(CancellationToken))
        {
            var handler = JobHandlePromise.Create(jobHandle, cancellation, out var token);
            if (handler.GetStatus(token).IsCompleted() && handler is JobHandlePromise loopItem)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreUpdate, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreLateUpdate, loopItem);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PostLateUpdate, loopItem);
            }

            return new UniTask(handler, token);
        }

        public static UniTask ConfigureAwait(this JobHandle jobHandle, PlayerLoopTiming waitTiming, CancellationToken cancellation = default(CancellationToken))
        {
            var handler = JobHandlePromise.Create(jobHandle, cancellation, out var token);
            if (handler.GetStatus(token).IsCompleted() && handler is JobHandlePromise loopItem)
            {
                PlayerLoopHelper.AddAction(waitTiming, loopItem);
            }

            return new UniTask(handler, token);
        }

        sealed class JobHandlePromise : IUniTaskSource, IPlayerLoopItem
        {
            JobHandle jobHandle;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            public static IUniTaskSource Create(JobHandle jobHandle, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                // not use pool.
                var result = new JobHandlePromise();

                result.jobHandle = jobHandle;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                core.GetResult(token);
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

                if (jobHandle.IsCompleted)
                {
                    jobHandle.Complete();
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                return true;
            }
        }
    }
}

#endif