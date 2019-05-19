#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Threading;
using UniRx.Async.Internal;

namespace UniRx.Async
{
    public static class EnumeratorAsyncExtensions
    {
        public static IAwaiter GetAwaiter(this IEnumerator enumerator)
        {
            var awaiter = new EnumeratorAwaiter(enumerator, CancellationToken.None);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, awaiter);
            }
            return awaiter;
        }

        public static UniTask ToUniTask(this IEnumerator enumerator)
        {
            var awaiter = new EnumeratorAwaiter(enumerator, CancellationToken.None);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, awaiter);
            }
            return new UniTask(awaiter);
        }

        public static UniTask ConfigureAwait(this IEnumerator enumerator, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var awaiter = new EnumeratorAwaiter(enumerator, cancellationToken);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(timing, awaiter);
            }
            return new UniTask(awaiter);
        }

        class EnumeratorAwaiter : IAwaiter, IPlayerLoopItem
        {
            IEnumerator innerEnumerator;
            CancellationToken cancellationToken;
            Action continuation;
            AwaiterStatus status;
            ExceptionDispatchInfo exception;

            public EnumeratorAwaiter(IEnumerator innerEnumerator, CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    status = AwaiterStatus.Canceled;
                    return;
                }

                this.innerEnumerator = innerEnumerator;
                this.status = AwaiterStatus.Pending;
                this.cancellationToken = cancellationToken;
                this.continuation = null;

                TaskTracker.TrackActiveTask(this, 2);
            }

            public bool IsCompleted => status.IsCompleted();

            public AwaiterStatus Status => status;

            public void GetResult()
            {
                switch (status)
                {
                    case AwaiterStatus.Succeeded:
                        break;
                    case AwaiterStatus.Pending:
                        Error.ThrowNotYetCompleted();
                        break;
                    case AwaiterStatus.Faulted:
                        exception.Throw();
                        break;
                    case AwaiterStatus.Canceled:
                        Error.ThrowOperationCanceledException();
                        break;
                    default:
                        break;
                }
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    InvokeContinuation(AwaiterStatus.Canceled);
                    return false;
                }

                var success = false;
                try
                {
                    if (innerEnumerator.MoveNext())
                    {
                        return true;
                    }
                    else
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                }

                InvokeContinuation(success ? AwaiterStatus.Succeeded : AwaiterStatus.Faulted);
                return false;
            }

            void InvokeContinuation(AwaiterStatus status)
            {
                this.status = status;
                var cont = this.continuation;

                // cleanup
                TaskTracker.RemoveTracking(this);
                this.continuation = null;
                this.cancellationToken = CancellationToken.None;
                this.innerEnumerator = null;

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