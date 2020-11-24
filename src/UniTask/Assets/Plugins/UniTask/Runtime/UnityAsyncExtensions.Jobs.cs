#if ENABLE_MANAGED_JOBS
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using Unity.Jobs;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        public static async UniTask WaitAsync(this JobHandle jobHandle, PlayerLoopTiming waitTiming, CancellationToken cancellationToken = default)
        {
            await UniTask.Yield(waitTiming);
            jobHandle.Complete();
            cancellationToken.ThrowIfCancellationRequested(); // call cancel after Complete.
        }

        public static UniTask.Awaiter GetAwaiter(this JobHandle jobHandle)
        {
            var handler = JobHandlePromise.Create(jobHandle, out var token);
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, handler);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreUpdate, handler);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, handler);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreLateUpdate, handler);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PostLateUpdate, handler);
            }

            return new UniTask(handler, token).GetAwaiter();
        }

        // can not pass CancellationToken because can't handle JobHandle's Complete and NativeArray.Dispose.

        public static UniTask ToUniTask(this JobHandle jobHandle, PlayerLoopTiming waitTiming)
        {
            var handler = JobHandlePromise.Create(jobHandle, out var token);
            {
                PlayerLoopHelper.AddAction(waitTiming, handler);
            }

            return new UniTask(handler, token);
        }

        sealed class JobHandlePromise : IUniTaskSource, IPlayerLoopItem
        {
            JobHandle jobHandle;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            // Cancellation is not supported.
            public static JobHandlePromise Create(JobHandle jobHandle, out short token)
            {
                // not use pool.
                var result = new JobHandlePromise();

                result.jobHandle = jobHandle;

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
                if (jobHandle.IsCompleted | PlayerLoopHelper.IsEditorApplicationQuitting)
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