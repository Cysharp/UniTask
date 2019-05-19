#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6)) && ENABLE_MANAGED_JOBS
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using UniRx.Async.Internal;
using Unity.Jobs;

namespace UniRx.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static IAwaiter GetAwaiter(this JobHandle jobHandle)
        {
            var awaiter = new JobHandleAwaiter(jobHandle, CancellationToken.None);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreUpdate, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreLateUpdate, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PostLateUpdate, awaiter);
            }
            return awaiter;
        }

        public static UniTask ToUniTask(this JobHandle jobHandle, CancellationToken cancellation = default(CancellationToken))
        {
            var awaiter = new JobHandleAwaiter(jobHandle, cancellation);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreUpdate, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PreLateUpdate, awaiter);
                PlayerLoopHelper.AddAction(PlayerLoopTiming.PostLateUpdate, awaiter);
            }
            return new UniTask(awaiter);
        }

        public static UniTask ConfigureAwait(this JobHandle jobHandle, PlayerLoopTiming waitTiming, CancellationToken cancellation = default(CancellationToken))
        {
            var awaiter = new JobHandleAwaiter(jobHandle, cancellation);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(waitTiming, awaiter);
            }
            return new UniTask(awaiter);
        }

        class JobHandleAwaiter : IAwaiter, IPlayerLoopItem
        {
            JobHandle jobHandle;
            CancellationToken cancellationToken;
            AwaiterStatus status;
            Action continuation;

            public JobHandleAwaiter(JobHandle jobHandle, CancellationToken cancellationToken, int skipFrame = 2)
            {
                this.status = cancellationToken.IsCancellationRequested ? AwaiterStatus.Canceled
                            : jobHandle.IsCompleted ? AwaiterStatus.Succeeded
                            : AwaiterStatus.Pending;

                if (this.status.IsCompleted()) return;

                this.jobHandle = jobHandle;
                this.cancellationToken = cancellationToken;
                this.status = AwaiterStatus.Pending;
                this.continuation = null;

                TaskTracker.TrackActiveTask(this, skipFrame);
            }

            public bool IsCompleted => status.IsCompleted();

            public AwaiterStatus Status => status;

            public void GetResult()
            {
                if (status == AwaiterStatus.Succeeded)
                {
                    return;
                }
                else if (status == AwaiterStatus.Canceled)
                {
                    Error.ThrowOperationCanceledException();
                }

                Error.ThrowNotYetCompleted();
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    // Call jobHandle.Complete after finished.
                    PlayerLoopHelper.AddAction(PlayerLoopTiming.EarlyUpdate, new JobHandleAwaiter(jobHandle, CancellationToken.None, 1));
                    InvokeContinuation(AwaiterStatus.Canceled);
                    return false;
                }

                if (jobHandle.IsCompleted)
                {
                    jobHandle.Complete();
                    InvokeContinuation(AwaiterStatus.Succeeded);
                    return false;
                }

                return true;
            }

            void InvokeContinuation(AwaiterStatus status)
            {
                this.status = status;
                var cont = this.continuation;

                // cleanup
                TaskTracker.RemoveTracking(this);
                this.continuation = null;
                this.cancellationToken = CancellationToken.None;
                this.jobHandle = default(JobHandle);

                if (cont != null) cont.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(this.continuation);
                this.continuation = continuation;
            }
        }
    }
}

#endif