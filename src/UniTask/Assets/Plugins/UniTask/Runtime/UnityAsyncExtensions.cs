#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks.Internal;
#if ENABLE_UNITYWEBREQUEST
using UnityEngine.Networking;
#endif

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        #region AsyncOperation

        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AsyncOperationAwaiter(asyncOperation);
        }

        public static UniTask WithCancellation(this AsyncOperation asyncOperation, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.CompletedTask;
            return new UniTask(AsyncOperationConfiguredSource.Create(asyncOperation, PlayerLoopTiming.Update, null, cancellationToken, out var token), token);
        }

        public static UniTask ToUniTask(this AsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.CompletedTask;
            return new UniTask(AsyncOperationConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, out var token), token);
        }

        public struct AsyncOperationAwaiter : ICriticalNotifyCompletion
        {
            AsyncOperation asyncOperation;
            Action<AsyncOperation> continuationAction;

            public AsyncOperationAwaiter(AsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public void GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    asyncOperation = null;
                }
                else
                {
                    asyncOperation = null;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>(); // allocate delegate.
                asyncOperation.completed += continuationAction;
            }
        }

        class AsyncOperationConfiguredSource : IUniTaskSource, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<AsyncOperationConfiguredSource> pool = new PromisePool<AsyncOperationConfiguredSource>();

            AsyncOperation asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            AsyncOperationConfiguredSource()
            {

            }

            public static IUniTaskSource Create(AsyncOperation asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new AsyncOperationConfiguredSource();

                result.asyncOperation = asyncOperation;
                result.progress = progress;
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
                    TaskTracker.RemoveTracking(this);

                    core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
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

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
            }

            ~AsyncOperationConfiguredSource()
            {
                if (pool.TryReturn(this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        #endregion

        #region ResourceRequest

        public static ResourceRequestAwaiter GetAwaiter(this ResourceRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new ResourceRequestAwaiter(asyncOperation);
        }

        public static UniTask<UnityEngine.Object> WithCancellation(this ResourceRequest asyncOperation, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
            return new UniTask<UnityEngine.Object>(ResourceRequestConfiguredSource.Create(asyncOperation, PlayerLoopTiming.Update, null, cancellationToken, out var token), token);
        }

        public static UniTask<UnityEngine.Object> ToUniTask(this ResourceRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
            return new UniTask<UnityEngine.Object>(ResourceRequestConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, out var token), token);
        }

        public struct ResourceRequestAwaiter : ICriticalNotifyCompletion
        {
            ResourceRequest asyncOperation;
            Action<AsyncOperation> continuationAction;

            public ResourceRequestAwaiter(ResourceRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityEngine.Object GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>(); // allocate delegate.
                asyncOperation.completed += continuationAction;
            }
        }

        class ResourceRequestConfiguredSource : IUniTaskSource<UnityEngine.Object>, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<ResourceRequestConfiguredSource> pool = new PromisePool<ResourceRequestConfiguredSource>();

            ResourceRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<UnityEngine.Object> core;

            ResourceRequestConfiguredSource()
            {

            }

            public static IUniTaskSource<UnityEngine.Object> Create(ResourceRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new ResourceRequestConfiguredSource();

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object GetResult(short token)
            {
                try
                {
                    TaskTracker.RemoveTracking(this);

                    return core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.asset);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
            }

            ~ResourceRequestConfiguredSource()
            {
                if (pool.TryReturn(this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        #endregion

        #region AssetBundleRequest

        public static AssetBundleRequestAwaiter GetAwaiter(this AssetBundleRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleRequestAwaiter(asyncOperation);
        }

        public static UniTask<UnityEngine.Object> WithCancellation(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
            return new UniTask<UnityEngine.Object>(AssetBundleRequestConfiguredSource.Create(asyncOperation, PlayerLoopTiming.Update, null, cancellationToken, out var token), token);
        }

        public static UniTask<UnityEngine.Object> ToUniTask(this AssetBundleRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
            return new UniTask<UnityEngine.Object>(AssetBundleRequestConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, out var token), token);
        }

        public struct AssetBundleRequestAwaiter : ICriticalNotifyCompletion
        {
            AssetBundleRequest asyncOperation;
            Action<AsyncOperation> continuationAction;

            public AssetBundleRequestAwaiter(AssetBundleRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityEngine.Object GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.asset;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>(); // allocate delegate.
                asyncOperation.completed += continuationAction;
            }
        }

        class AssetBundleRequestConfiguredSource : IUniTaskSource<UnityEngine.Object>, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<AssetBundleRequestConfiguredSource> pool = new PromisePool<AssetBundleRequestConfiguredSource>();

            AssetBundleRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<UnityEngine.Object> core;

            AssetBundleRequestConfiguredSource()
            {

            }

            public static IUniTaskSource<UnityEngine.Object> Create(AssetBundleRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new AssetBundleRequestConfiguredSource();

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object GetResult(short token)
            {
                try
                {
                    TaskTracker.RemoveTracking(this);

                    return core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.asset);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
            }

            ~AssetBundleRequestConfiguredSource()
            {
                if (pool.TryReturn(this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        #endregion

        #region AssetBundleCreateRequest

        public static AssetBundleCreateRequestAwaiter GetAwaiter(this AssetBundleCreateRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleCreateRequestAwaiter(asyncOperation);
        }

        public static UniTask<AssetBundle> WithCancellation(this AssetBundleCreateRequest asyncOperation, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.assetBundle);
            return new UniTask<AssetBundle>(AssetBundleCreateRequestConfiguredSource.Create(asyncOperation, PlayerLoopTiming.Update, null, cancellationToken, out var token), token);
        }

        public static UniTask<AssetBundle> ToUniTask(this AssetBundleCreateRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.assetBundle);
            return new UniTask<AssetBundle>(AssetBundleCreateRequestConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, out var token), token);
        }

        public struct AssetBundleCreateRequestAwaiter : ICriticalNotifyCompletion
        {
            AssetBundleCreateRequest asyncOperation;
            Action<AsyncOperation> continuationAction;

            public AssetBundleCreateRequestAwaiter(AssetBundleCreateRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public AssetBundle GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.assetBundle;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.assetBundle;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>(); // allocate delegate.
                asyncOperation.completed += continuationAction;
            }
        }

        class AssetBundleCreateRequestConfiguredSource : IUniTaskSource<AssetBundle>, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<AssetBundleCreateRequestConfiguredSource> pool = new PromisePool<AssetBundleCreateRequestConfiguredSource>();

            AssetBundleCreateRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<AssetBundle> core;

            AssetBundleCreateRequestConfiguredSource()
            {

            }

            public static IUniTaskSource<AssetBundle> Create(AssetBundleCreateRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<AssetBundle>.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new AssetBundleCreateRequestConfiguredSource();

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public AssetBundle GetResult(short token)
            {
                try
                {
                    TaskTracker.RemoveTracking(this);

                    return core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
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
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.assetBundle);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
            }

            ~AssetBundleCreateRequestConfiguredSource()
            {
                if (pool.TryReturn(this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        #endregion

#if ENABLE_UNITYWEBREQUEST
        #region UnityWebRequestAsyncOperation

        public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new UnityWebRequestAsyncOperationAwaiter(asyncOperation);
        }

        public static UniTask<UnityWebRequest> WithCancellation(this UnityWebRequestAsyncOperation asyncOperation, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.webRequest);
            return new UniTask<UnityWebRequest>(UnityWebRequestAsyncOperationConfiguredSource.Create(asyncOperation, PlayerLoopTiming.Update, null, cancellationToken, out var token), token);
        }

        public static UniTask<UnityWebRequest> ToUniTask(this UnityWebRequestAsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.webRequest);
            return new UniTask<UnityWebRequest>(UnityWebRequestAsyncOperationConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, out var token), token);
        }

        public struct UnityWebRequestAsyncOperationAwaiter : ICriticalNotifyCompletion
        {
            UnityWebRequestAsyncOperation asyncOperation;
            Action<AsyncOperation> continuationAction;

            public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityWebRequest GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.webRequest;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.webRequest;
                    asyncOperation = null;
                    return result;
                }
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = continuation.AsFuncOfT<AsyncOperation>(); // allocate delegate.
                asyncOperation.completed += continuationAction;
            }
        }

        class UnityWebRequestAsyncOperationConfiguredSource : IUniTaskSource<UnityWebRequest>, IPlayerLoopItem, IPromisePoolItem
        {
            static readonly PromisePool<UnityWebRequestAsyncOperationConfiguredSource> pool = new PromisePool<UnityWebRequestAsyncOperationConfiguredSource>();

            UnityWebRequestAsyncOperation asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<UnityWebRequest> core;

            UnityWebRequestAsyncOperationConfiguredSource()
            {

            }

            public static IUniTaskSource<UnityWebRequest> Create(UnityWebRequestAsyncOperation asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityWebRequest>.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new UnityWebRequestAsyncOperationConfiguredSource();

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityWebRequest GetResult(short token)
            {
                try
                {
                    TaskTracker.RemoveTracking(this);

                    return core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
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
                    asyncOperation.webRequest.Abort();
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (progress != null)
                {
                    progress.Report(asyncOperation.progress);
                }

                if (asyncOperation.isDone)
                {
                    core.TrySetResult(asyncOperation.webRequest);
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
            }

            ~UnityWebRequestAsyncOperationConfiguredSource()
            {
                if (pool.TryReturn(this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        #endregion
#endif

    }
}