#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks.Internal;
#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || UNITASK_WEBREQUEST_SUPPORT)
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
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask WithCancellation(this AsyncOperation asyncOperation, bool handleImmediately, CancellationToken cancellationToken)
        {
            if (handleImmediately)
            {
                Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
                if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled(cancellationToken);
                if (asyncOperation.isDone) return UniTask.CompletedTask;
                return new UniTask(AsyncOperationCallbackHandlerSource.Create(asyncOperation, cancellationToken, out var token), token);
            }
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask ToUniTask(this AsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled(cancellationToken);
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
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        sealed class AsyncOperationCallbackHandlerSource : IUniTaskSource, ITaskPoolNode<AsyncOperationCallbackHandlerSource>
        {
            static TaskPool<AsyncOperationCallbackHandlerSource> pool;
            AsyncOperationCallbackHandlerSource nextNode;
            public ref AsyncOperationCallbackHandlerSource NextNode => ref nextNode;

            static AsyncOperationCallbackHandlerSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationCallbackHandlerSource), () => pool.Size);
            }

            AsyncOperation asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            AsyncOperationCallbackHandlerSource()
            {
            }

            public static IUniTaskSource Create(AsyncOperation asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationCallbackHandlerSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                asyncOperation.completed += result.AsyncOperationCompletedHandler;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var source = (AsyncOperationCallbackHandlerSource)state;
                        source.core.TrySetCanceled(source.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

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
                asyncOperation.completed -= AsyncOperationCompletedHandler;
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                return pool.TryPush(this);
            }

            void AsyncOperationCompletedHandler(AsyncOperation _)
            {
                core.TrySetResult(AsyncUnit.Default);
            }
        }

        sealed class AsyncOperationConfiguredSource : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<AsyncOperationConfiguredSource>
        {
            static TaskPool<AsyncOperationConfiguredSource> pool;
            AsyncOperationConfiguredSource nextNode;
            public ref AsyncOperationConfiguredSource NextNode => ref nextNode;

            static AsyncOperationConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationConfiguredSource), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationConfiguredSource();
                }

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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
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
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<UnityEngine.Object> WithCancellation(this ResourceRequest asyncOperation, bool handleImmediately, CancellationToken cancellationToken)
        {
            if (handleImmediately)
            {
                Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
                if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object>(cancellationToken);
                if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
                return new UniTask<UnityEngine.Object>(ResourceRequestCallbackHandlerSource.Create(asyncOperation, cancellationToken, out var token), token);
            }
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<UnityEngine.Object> ToUniTask(this ResourceRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object>(cancellationToken);
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
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        sealed class ResourceRequestCallbackHandlerSource : IUniTaskSource<UnityEngine.Object>, ITaskPoolNode<ResourceRequestCallbackHandlerSource>
        {
            static TaskPool<ResourceRequestCallbackHandlerSource> pool;
            ResourceRequestCallbackHandlerSource nextNode;
            public ref ResourceRequestCallbackHandlerSource NextNode => ref nextNode;

            static ResourceRequestCallbackHandlerSource()
            {
                TaskPool.RegisterSizeGetter(typeof(ResourceRequestCallbackHandlerSource), () => pool.Size);
            }

            ResourceRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;

            UniTaskCompletionSourceCore<UnityEngine.Object> core;

            ResourceRequestCallbackHandlerSource()
            {
            }

            public static IUniTaskSource<UnityEngine.Object> Create(ResourceRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new ResourceRequestCallbackHandlerSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                asyncOperation.completed += result.AsyncOperationCompletedHandler;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var source = (ResourceRequestCallbackHandlerSource)state;
                        source.core.TrySetCanceled(source.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object GetResult(short token)
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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation.completed -= AsyncOperationCompletedHandler;
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                return pool.TryPush(this);
            }

            void AsyncOperationCompletedHandler(AsyncOperation _)
            {
                core.TrySetResult(asyncOperation.asset);
            }
        }

        sealed class ResourceRequestConfiguredSource : IUniTaskSource<UnityEngine.Object>, IPlayerLoopItem, ITaskPoolNode<ResourceRequestConfiguredSource>
        {
            static TaskPool<ResourceRequestConfiguredSource> pool;
            ResourceRequestConfiguredSource nextNode;
            public ref ResourceRequestConfiguredSource NextNode => ref nextNode;

            static ResourceRequestConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(ResourceRequestConfiguredSource), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new ResourceRequestConfiguredSource();
                }

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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

#if UNITASK_ASSETBUNDLE_SUPPORT
        #region AssetBundleRequest

        public static AssetBundleRequestAwaiter GetAwaiter(this AssetBundleRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleRequestAwaiter(asyncOperation);
        }

        public static UniTask<UnityEngine.Object> WithCancellation(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken)
        {
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<UnityEngine.Object> WithCancellation(this AssetBundleRequest asyncOperation, bool handleImmediately, CancellationToken cancellationToken)
        {
            if (handleImmediately)
            {
                Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
                if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object>(cancellationToken);
                if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
                return new UniTask<UnityEngine.Object>(AssetBundleRequestCallbackHandlerSource.Create(asyncOperation, cancellationToken, out var token), token);
            }
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<UnityEngine.Object> ToUniTask(this AssetBundleRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object>(cancellationToken);
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
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        sealed class AssetBundleRequestCallbackHandlerSource : IUniTaskSource<UnityEngine.Object>, ITaskPoolNode<AssetBundleRequestCallbackHandlerSource>
        {
            static TaskPool<AssetBundleRequestCallbackHandlerSource> pool;
            AssetBundleRequestCallbackHandlerSource nextNode;
            public ref AssetBundleRequestCallbackHandlerSource NextNode => ref nextNode;

            static AssetBundleRequestCallbackHandlerSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleRequestCallbackHandlerSource), () => pool.Size);
            }

            AssetBundleRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;

            UniTaskCompletionSourceCore<UnityEngine.Object> core;

            AssetBundleRequestCallbackHandlerSource()
            {
            }

            public static IUniTaskSource<UnityEngine.Object> Create(AssetBundleRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestCallbackHandlerSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                asyncOperation.completed += result.AsyncOperationCompletedHandler;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var source = (AssetBundleRequestCallbackHandlerSource)state;
                        source.core.TrySetCanceled(source.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object GetResult(short token)
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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation.completed -= AsyncOperationCompletedHandler;
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                return pool.TryPush(this);
            }

            void AsyncOperationCompletedHandler(AsyncOperation _)
            {
                core.TrySetResult(asyncOperation.asset);
            }
        }

        sealed class AssetBundleRequestConfiguredSource : IUniTaskSource<UnityEngine.Object>, IPlayerLoopItem, ITaskPoolNode<AssetBundleRequestConfiguredSource>
        {
            static TaskPool<AssetBundleRequestConfiguredSource> pool;
            AssetBundleRequestConfiguredSource nextNode;
            public ref AssetBundleRequestConfiguredSource NextNode => ref nextNode;

            static AssetBundleRequestConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleRequestConfiguredSource), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestConfiguredSource();
                }

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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion
#endif

#if UNITASK_ASSETBUNDLE_SUPPORT
        #region AssetBundleCreateRequest

        public static AssetBundleCreateRequestAwaiter GetAwaiter(this AssetBundleCreateRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleCreateRequestAwaiter(asyncOperation);
        }

        public static UniTask<AssetBundle> WithCancellation(this AssetBundleCreateRequest asyncOperation, CancellationToken cancellationToken)
        {
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<AssetBundle> WithCancellation(this AssetBundleCreateRequest asyncOperation, bool handleImmediately, CancellationToken cancellationToken)
        {
            if (handleImmediately)
            {
                Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
                if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<AssetBundle>(cancellationToken);
                if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.assetBundle);
                return new UniTask<AssetBundle>(AssetBundleCreateRequestCallbackHandlerSource.Create(asyncOperation, cancellationToken, out var token), token);
            }
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<AssetBundle> ToUniTask(this AssetBundleCreateRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<AssetBundle>(cancellationToken);
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
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        sealed class AssetBundleCreateRequestCallbackHandlerSource : IUniTaskSource<AssetBundle>, ITaskPoolNode<AssetBundleCreateRequestCallbackHandlerSource>
        {
            static TaskPool<AssetBundleCreateRequestCallbackHandlerSource> pool;
            AssetBundleCreateRequestCallbackHandlerSource nextNode;
            public ref AssetBundleCreateRequestCallbackHandlerSource NextNode => ref nextNode;

            static AssetBundleCreateRequestCallbackHandlerSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleCreateRequestCallbackHandlerSource), () => pool.Size);
            }

            AssetBundleCreateRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;

            UniTaskCompletionSourceCore<AssetBundle> core;

            AssetBundleCreateRequestCallbackHandlerSource()
            {
            }

            public static IUniTaskSource<AssetBundle> Create(AssetBundleCreateRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<AssetBundle>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleCreateRequestCallbackHandlerSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                asyncOperation.completed += result.AsyncOperationCompletedHandler;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var source = (AssetBundleCreateRequestCallbackHandlerSource)state;
                        source.core.TrySetCanceled(source.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            public AssetBundle GetResult(short token)
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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation.completed -= AsyncOperationCompletedHandler;
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                return pool.TryPush(this);
            }

            void AsyncOperationCompletedHandler(AsyncOperation _)
            {
                core.TrySetResult(asyncOperation.assetBundle);
            }
        }

        sealed class AssetBundleCreateRequestConfiguredSource : IUniTaskSource<AssetBundle>, IPlayerLoopItem, ITaskPoolNode<AssetBundleCreateRequestConfiguredSource>
        {
            static TaskPool<AssetBundleCreateRequestConfiguredSource> pool;
            AssetBundleCreateRequestConfiguredSource nextNode;
            public ref AssetBundleCreateRequestConfiguredSource NextNode => ref nextNode;

            static AssetBundleCreateRequestConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleCreateRequestConfiguredSource), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleCreateRequestConfiguredSource();
                }

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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion
#endif

#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || UNITASK_WEBREQUEST_SUPPORT)
        #region UnityWebRequestAsyncOperation

        public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new UnityWebRequestAsyncOperationAwaiter(asyncOperation);
        }

        public static UniTask<UnityWebRequest> WithCancellation(this UnityWebRequestAsyncOperation asyncOperation, CancellationToken cancellationToken)
        {
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<UnityWebRequest> WithCancellation(this UnityWebRequestAsyncOperation asyncOperation, bool handleImmediately, CancellationToken cancellationToken)
        {
            if (handleImmediately)
            {
                Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
                if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityWebRequest>(cancellationToken);
                if (asyncOperation.isDone)
                {
                    if (asyncOperation.webRequest.IsError())
                    {
                        return UniTask.FromException<UnityWebRequest>(new UnityWebRequestException(asyncOperation.webRequest));
                    }
                    return UniTask.FromResult(asyncOperation.webRequest);
                }
                return new UniTask<UnityWebRequest>(UnityWebRequestAsyncOperationCallbackHandlerSource.Create(asyncOperation, cancellationToken, out var token), token);
            }
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<UnityWebRequest> ToUniTask(this UnityWebRequestAsyncOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityWebRequest>(cancellationToken);
            if (asyncOperation.isDone)
            {
                if (asyncOperation.webRequest.IsError())
                {
                    return UniTask.FromException<UnityWebRequest>(new UnityWebRequestException(asyncOperation.webRequest));
                }
                return UniTask.FromResult(asyncOperation.webRequest);
            }
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
                    if (result.IsError())
                    {
                        throw new UnityWebRequestException(result);
                    }
                    return result;
                }
                else
                {
                    var result = asyncOperation.webRequest;
                    asyncOperation = null;
                    if (result.IsError())
                    {
                        throw new UnityWebRequestException(result);
                    }
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
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        sealed class UnityWebRequestAsyncOperationCallbackHandlerSource : IUniTaskSource<UnityWebRequest>, ITaskPoolNode<UnityWebRequestAsyncOperationCallbackHandlerSource>
        {
            static TaskPool<UnityWebRequestAsyncOperationCallbackHandlerSource> pool;
            UnityWebRequestAsyncOperationCallbackHandlerSource nextNode;
            public ref UnityWebRequestAsyncOperationCallbackHandlerSource NextNode => ref nextNode;

            static UnityWebRequestAsyncOperationCallbackHandlerSource()
            {
                TaskPool.RegisterSizeGetter(typeof(UnityWebRequestAsyncOperationCallbackHandlerSource), () => pool.Size);
            }

            UnityWebRequestAsyncOperation asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;

            UniTaskCompletionSourceCore<UnityWebRequest> core;

            UnityWebRequestAsyncOperationCallbackHandlerSource()
            {
            }

            public static IUniTaskSource<UnityWebRequest> Create(UnityWebRequestAsyncOperation asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityWebRequest>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new UnityWebRequestAsyncOperationCallbackHandlerSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                asyncOperation.completed += result.AsyncOperationCompletedHandler;

                if (cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var source = (UnityWebRequestAsyncOperationCallbackHandlerSource)state;
                        source.asyncOperation.webRequest.Abort();
                        source.core.TrySetCanceled(source.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            public UnityWebRequest GetResult(short token)
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

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation.completed -= AsyncOperationCompletedHandler;
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                return pool.TryPush(this);
            }

            void AsyncOperationCompletedHandler(AsyncOperation _)
            {
                if (asyncOperation.webRequest.IsError())
                {
                    core.TrySetException(new UnityWebRequestException(asyncOperation.webRequest));
                }
                else
                {
                    core.TrySetResult(asyncOperation.webRequest);
                }
            }
        }

        sealed class UnityWebRequestAsyncOperationConfiguredSource : IUniTaskSource<UnityWebRequest>, IPlayerLoopItem, ITaskPoolNode<UnityWebRequestAsyncOperationConfiguredSource>
        {
            static TaskPool<UnityWebRequestAsyncOperationConfiguredSource> pool;
            UnityWebRequestAsyncOperationConfiguredSource nextNode;
            public ref UnityWebRequestAsyncOperationConfiguredSource NextNode => ref nextNode;

            static UnityWebRequestAsyncOperationConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(UnityWebRequestAsyncOperationConfiguredSource), () => pool.Size);
            }

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

                if (!pool.TryPop(out var result))
                {
                    result = new UnityWebRequestAsyncOperationConfiguredSource();
                }

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
                    if (asyncOperation.webRequest.IsError())
                    {
                        core.TrySetException(new UnityWebRequestException(asyncOperation.webRequest));
                    }
                    else
                    {
                        core.TrySetResult(asyncOperation.webRequest);
                    }
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion
#endif

    }
}