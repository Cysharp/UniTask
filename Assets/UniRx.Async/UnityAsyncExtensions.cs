#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using UniRx.Async.Internal;
using UnityEngine;
using UnityEngine.Networking;

namespace UniRx.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AsyncOperationAwaiter(asyncOperation);
        }

        public static UniTask ToUniTask(this AsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new UniTask(new AsyncOperationAwaiter(asyncOperation));
        }

        public static UniTask ConfigureAwait(this AsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellation = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));

            var awaiter = new AsyncOperationConfiguredAwaiter(asyncOperation, progress, cancellation);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(timing, awaiter);
            }
            return new UniTask(awaiter);
        }

        public static ResourceRequestAwaiter GetAwaiter(this ResourceRequest resourceRequest)
        {
            Error.ThrowArgumentNullException(resourceRequest, nameof(resourceRequest));
            return new ResourceRequestAwaiter(resourceRequest);
        }

        public static UniTask<UnityEngine.Object> ToUniTask(this ResourceRequest resourceRequest)
        {
            Error.ThrowArgumentNullException(resourceRequest, nameof(resourceRequest));
            return new UniTask<UnityEngine.Object>(new ResourceRequestAwaiter(resourceRequest));
        }

        public static UniTask<UnityEngine.Object> ConfigureAwait(this ResourceRequest resourceRequest, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellation = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(resourceRequest, nameof(resourceRequest));

            var awaiter = new ResourceRequestConfiguredAwaiter(resourceRequest, progress, cancellation);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(timing, awaiter);
            }
            return new UniTask<UnityEngine.Object>(awaiter);
        }

#if ENABLE_WWW

#if UNITY_2018_3_OR_NEWER
#pragma warning disable CS0618
#endif

        public static IAwaiter GetAwaiter(this WWW www)
        {
            Error.ThrowArgumentNullException(www, nameof(www));

            var awaiter = new WWWConfiguredAwaiter(www, null, CancellationToken.None);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, awaiter);
            }
            return awaiter;
        }

        public static UniTask ToUniTask(this WWW www)
        {
            Error.ThrowArgumentNullException(www, nameof(www));

            var awaiter = new WWWConfiguredAwaiter(www, null, CancellationToken.None);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, awaiter);
            }
            return new UniTask(awaiter);
        }

        public static UniTask ConfigureAwait(this WWW www, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellation = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(www, nameof(www));

            var awaiter = new WWWConfiguredAwaiter(www, progress, cancellation);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(timing, awaiter);
            }
            return new UniTask(awaiter);
        }

#if UNITY_2018_3_OR_NEWER
#pragma warning restore CS0618
#endif

#endif

#if ENABLE_UNITYWEBREQUEST

        public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new UnityWebRequestAsyncOperationAwaiter(asyncOperation);
        }

        public static UniTask<UnityWebRequest> ToUniTask(this UnityWebRequestAsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new UniTask<UnityWebRequest>(new UnityWebRequestAsyncOperationAwaiter(asyncOperation));
        }

        public static UniTask<UnityWebRequest> ConfigureAwait(this UnityWebRequestAsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellation = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));

            var awaiter = new UnityWebRequestAsyncOperationConfiguredAwaiter(asyncOperation, progress, cancellation);
            if (!awaiter.IsCompleted)
            {
                PlayerLoopHelper.AddAction(timing, awaiter);
            }
            return new UniTask<UnityWebRequest>(awaiter);
        }

#endif

        public struct AsyncOperationAwaiter : IAwaiter
        {
            AsyncOperation asyncOperation;
            Action<AsyncOperation> continuationAction;
            AwaiterStatus status;

            public AsyncOperationAwaiter(AsyncOperation asyncOperation)
            {
                this.status = asyncOperation.isDone ? AwaiterStatus.Succeeded : AwaiterStatus.Pending;
                this.asyncOperation = (this.status.IsCompleted()) ? null : asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => status.IsCompleted();
            public AwaiterStatus Status => status;

            public void GetResult()
            {
                if (status == AwaiterStatus.Succeeded) return;

                if (status == AwaiterStatus.Pending)
                {
                    // first timing of call
                    if (asyncOperation.isDone)
                    {
                        status = AwaiterStatus.Succeeded;
                    }
                    else
                    {
                        Error.ThrowNotYetCompleted();
                    }
                }

                asyncOperation = null; // remove reference.

                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>();
                asyncOperation.completed += continuationAction;
            }
        }

        class AsyncOperationConfiguredAwaiter : IAwaiter, IPlayerLoopItem
        {
            AsyncOperation asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            AwaiterStatus status;
            Action continuation;

            public AsyncOperationConfiguredAwaiter(AsyncOperation asyncOperation, IProgress<float> progress, CancellationToken cancellationToken)
            {
                this.status = cancellationToken.IsCancellationRequested ? AwaiterStatus.Canceled
                            : asyncOperation.isDone ? AwaiterStatus.Succeeded
                            : AwaiterStatus.Pending;

                if (this.status.IsCompleted()) return;

                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
                this.continuation = null;

                TaskTracker.TrackActiveTask(this, 2);
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
                    InvokeContinuation(AwaiterStatus.Canceled);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
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
                this.progress = null;
                this.asyncOperation = null;

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

        public struct ResourceRequestAwaiter : IAwaiter<UnityEngine.Object>
        {
            ResourceRequest asyncOperation;
            Action<AsyncOperation> continuationAction;
            AwaiterStatus status;
            UnityEngine.Object result;

            public ResourceRequestAwaiter(ResourceRequest asyncOperation)
            {
                this.status = asyncOperation.isDone ? AwaiterStatus.Succeeded : AwaiterStatus.Pending;
                this.asyncOperation = (this.status.IsCompleted()) ? null : asyncOperation;
                this.result = (this.status.IsCompletedSuccessfully()) ? asyncOperation.asset : null;
                this.continuationAction = null;
            }

            public bool IsCompleted => status.IsCompleted();
            public AwaiterStatus Status => status;

            public UnityEngine.Object GetResult()
            {
                if (status == AwaiterStatus.Succeeded) return this.result;

                if (status == AwaiterStatus.Pending)
                {
                    // first timing of call
                    if (asyncOperation.isDone)
                    {
                        status = AwaiterStatus.Succeeded;
                    }
                    else
                    {
                        Error.ThrowNotYetCompleted();
                    }
                }

                this.result = asyncOperation.asset;
                asyncOperation = null; // remove reference.

                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                }

                return this.result;
            }

            void IAwaiter.GetResult() => GetResult();

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>();
                asyncOperation.completed += continuationAction;
            }
        }

        class ResourceRequestConfiguredAwaiter : IAwaiter<UnityEngine.Object>, IPlayerLoopItem
        {
            ResourceRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            AwaiterStatus status;
            Action continuation;
            UnityEngine.Object result;

            public ResourceRequestConfiguredAwaiter(ResourceRequest asyncOperation, IProgress<float> progress, CancellationToken cancellationToken)
            {
                this.status = cancellationToken.IsCancellationRequested ? AwaiterStatus.Canceled
                            : asyncOperation.isDone ? AwaiterStatus.Succeeded
                            : AwaiterStatus.Pending;

                if (this.status.IsCompletedSuccessfully()) this.result = asyncOperation.asset;
                if (this.status.IsCompleted()) return;

                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
                this.continuation = null;
                this.result = null;

                TaskTracker.TrackActiveTask(this, 2);
            }

            public bool IsCompleted => status.IsCompleted();
            public AwaiterStatus Status => status;
            void IAwaiter.GetResult() => GetResult();

            public UnityEngine.Object GetResult()
            {
                if (status == AwaiterStatus.Succeeded) return this.result;

                if (status == AwaiterStatus.Canceled)
                {
                    Error.ThrowOperationCanceledException();
                }

                return Error.ThrowNotYetCompleted<UnityEngine.Object>();
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    InvokeContinuation(AwaiterStatus.Canceled);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    this.result = asyncOperation.asset;
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
                this.progress = null;
                this.asyncOperation = null;

                if (cont != null) cont.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(this.continuation);
                this.continuation = continuation;
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(this.continuation);
                this.continuation = continuation;
            }
        }

#if ENABLE_WWW

#if UNITY_2018_3_OR_NEWER
#pragma warning disable CS0618
#endif

        class WWWConfiguredAwaiter : IAwaiter, IPlayerLoopItem
        {
            WWW asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            AwaiterStatus status;
            Action continuation;

            public WWWConfiguredAwaiter(WWW asyncOperation, IProgress<float> progress, CancellationToken cancellationToken)
            {
                this.status = cancellationToken.IsCancellationRequested ? AwaiterStatus.Canceled
                            : asyncOperation.isDone ? AwaiterStatus.Succeeded
                            : AwaiterStatus.Pending;

                if (this.status.IsCompleted()) return;

                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
                this.continuation = null;

                TaskTracker.TrackActiveTask(this, 2);
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
                    InvokeContinuation(AwaiterStatus.Canceled);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
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
                this.progress = null;
                this.asyncOperation = null;

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

#if UNITY_2018_3_OR_NEWER
#pragma warning restore CS0618
#endif

#endif

#if ENABLE_UNITYWEBREQUEST

        public struct UnityWebRequestAsyncOperationAwaiter : IAwaiter<UnityWebRequest>
        {
            UnityWebRequestAsyncOperation asyncOperation;
            Action<AsyncOperation> continuationAction;
            AwaiterStatus status;
            UnityWebRequest result;

            public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.status = asyncOperation.isDone ? AwaiterStatus.Succeeded : AwaiterStatus.Pending;
                this.asyncOperation = (this.status.IsCompleted()) ? null : asyncOperation;
                this.result = (this.status.IsCompletedSuccessfully()) ? asyncOperation.webRequest : null;
                this.continuationAction = null;
            }

            public bool IsCompleted => status.IsCompleted();
            public AwaiterStatus Status => status;

            public UnityWebRequest GetResult()
            {
                if (status == AwaiterStatus.Succeeded) return this.result;

                if (status == AwaiterStatus.Pending)
                {
                    // first timing of call
                    if (asyncOperation.isDone)
                    {
                        status = AwaiterStatus.Succeeded;
                    }
                    else
                    {
                        Error.ThrowNotYetCompleted();
                    }
                }

                this.result = asyncOperation.webRequest;
                asyncOperation = null; // remove reference.

                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                }

                return this.result;
            }

            void IAwaiter.GetResult() => GetResult();

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>();
                asyncOperation.completed += continuationAction;
            }
        }

        class UnityWebRequestAsyncOperationConfiguredAwaiter : IAwaiter<UnityWebRequest>, IPlayerLoopItem
        {
            UnityWebRequestAsyncOperation asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            AwaiterStatus status;
            Action continuation;
            UnityWebRequest result;

            public UnityWebRequestAsyncOperationConfiguredAwaiter(UnityWebRequestAsyncOperation asyncOperation, IProgress<float> progress, CancellationToken cancellationToken)
            {
                this.status = cancellationToken.IsCancellationRequested ? AwaiterStatus.Canceled
                            : asyncOperation.isDone ? AwaiterStatus.Succeeded
                            : AwaiterStatus.Pending;

                if (this.status.IsCompletedSuccessfully()) this.result = asyncOperation.webRequest;
                if (this.status.IsCompleted()) return;

                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
                this.continuation = null;
                this.result = null;

                TaskTracker.TrackActiveTask(this, 2);
            }

            public bool IsCompleted => status.IsCompleted();
            public AwaiterStatus Status => status;
            void IAwaiter.GetResult() => GetResult();

            public UnityWebRequest GetResult()
            {
                if (status == AwaiterStatus.Succeeded) return this.result;

                if (status == AwaiterStatus.Canceled)
                {
                    Error.ThrowOperationCanceledException();
                }

                return Error.ThrowNotYetCompleted<UnityWebRequest>();
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    InvokeContinuation(AwaiterStatus.Canceled);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    this.result = asyncOperation.webRequest;
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
                this.progress = null;
                this.asyncOperation = null;

                if (cont != null) cont.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(this.continuation);
                this.continuation = continuation;
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(this.continuation);
                this.continuation = continuation;
            }
        }

#endif
    }
}
#endif