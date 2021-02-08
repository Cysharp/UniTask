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
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled(cancellationToken);
            if (asyncOperation.isDone) return UniTask.CompletedTask;
            return new UniTask(AsyncOperationWithCancellationSource.Create(asyncOperation, cancellationToken, out var token), token);
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

        sealed class AsyncOperationWithCancellationSource : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<AsyncOperationWithCancellationSource>
        {
            static TaskPool<AsyncOperationWithCancellationSource> pool;
            AsyncOperationWithCancellationSource nextNode;
            public ref AsyncOperationWithCancellationSource NextNode => ref nextNode;

            static AsyncOperationWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperation> continuationAction;
            AsyncOperation asyncOperation;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            AsyncOperationWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource Create(AsyncOperation asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationWithCancellationSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                asyncOperation.completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperation _)
            {
                asyncOperation.completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                        return;
                    }

                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            public void GetResult(short token)
            {
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
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                cancellationToken = default;
                return pool.TryPush(this);
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
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object>(cancellationToken);
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
            return new UniTask<UnityEngine.Object>(ResourceRequestWithCancellationSource.Create(asyncOperation, cancellationToken, out var token), token);
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

        sealed class ResourceRequestWithCancellationSource : IUniTaskSource<UnityEngine.Object>, IPlayerLoopItem, ITaskPoolNode<ResourceRequestWithCancellationSource>
        {
            static TaskPool<ResourceRequestWithCancellationSource> pool;
            ResourceRequestWithCancellationSource nextNode;
            public ref ResourceRequestWithCancellationSource NextNode => ref nextNode;

            static ResourceRequestWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(ResourceRequestWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperation> continuationAction;
            ResourceRequest asyncOperation;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<UnityEngine.Object> core;

            ResourceRequestWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource<UnityEngine.Object> Create(ResourceRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new ResourceRequestWithCancellationSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                asyncOperation.completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperation _)
            {
                asyncOperation.completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                        return;
                    }

                    core.TrySetResult(asyncOperation.asset);
                }
            }

            public UnityEngine.Object GetResult(short token)
            {
                return core.GetResult(token);
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
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                cancellationToken = default;
                return pool.TryPush(this);
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
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object>(cancellationToken);
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.asset);
            return new UniTask<UnityEngine.Object>(AssetBundleRequestWithCancellationSource.Create(asyncOperation, cancellationToken, out var token), token);
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

        sealed class AssetBundleRequestWithCancellationSource : IUniTaskSource<UnityEngine.Object>, IPlayerLoopItem, ITaskPoolNode<AssetBundleRequestWithCancellationSource>
        {
            static TaskPool<AssetBundleRequestWithCancellationSource> pool;
            AssetBundleRequestWithCancellationSource nextNode;
            public ref AssetBundleRequestWithCancellationSource NextNode => ref nextNode;

            static AssetBundleRequestWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleRequestWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperation> continuationAction;
            AssetBundleRequest asyncOperation;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<UnityEngine.Object> core;

            AssetBundleRequestWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource<UnityEngine.Object> Create(AssetBundleRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestWithCancellationSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                asyncOperation.completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperation _)
            {
                asyncOperation.completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                        return;
                    }

                    core.TrySetResult(asyncOperation.asset);
                }
            }

            public UnityEngine.Object GetResult(short token)
            {
                return core.GetResult(token);
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
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                cancellationToken = default;
                return pool.TryPush(this);
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
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<AssetBundle>(cancellationToken);
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.assetBundle);
            return new UniTask<AssetBundle>(AssetBundleCreateRequestWithCancellationSource.Create(asyncOperation, cancellationToken, out var token), token);
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

        sealed class AssetBundleCreateRequestWithCancellationSource : IUniTaskSource<AssetBundle>, IPlayerLoopItem, ITaskPoolNode<AssetBundleCreateRequestWithCancellationSource>
        {
            static TaskPool<AssetBundleCreateRequestWithCancellationSource> pool;
            AssetBundleCreateRequestWithCancellationSource nextNode;
            public ref AssetBundleCreateRequestWithCancellationSource NextNode => ref nextNode;

            static AssetBundleCreateRequestWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleCreateRequestWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperation> continuationAction;
            AssetBundleCreateRequest asyncOperation;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<AssetBundle> core;

            AssetBundleCreateRequestWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource<AssetBundle> Create(AssetBundleCreateRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<AssetBundle>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleCreateRequestWithCancellationSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                asyncOperation.completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperation _)
            {
                asyncOperation.completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                        return;
                    }

                    core.TrySetResult(asyncOperation.assetBundle);
                }
            }

            public AssetBundle GetResult(short token)
            {
                return core.GetResult(token);
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
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                cancellationToken = default;
                return pool.TryPush(this);
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
            return new UniTask<UnityWebRequest>(UnityWebRequestAsyncOperationWithCancellationSource.Create(asyncOperation, cancellationToken, out var token), token);
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

        sealed class UnityWebRequestAsyncOperationWithCancellationSource : IUniTaskSource<UnityWebRequest>, IPlayerLoopItem, ITaskPoolNode<UnityWebRequestAsyncOperationWithCancellationSource>
        {
            static TaskPool<UnityWebRequestAsyncOperationWithCancellationSource> pool;
            UnityWebRequestAsyncOperationWithCancellationSource nextNode;
            public ref UnityWebRequestAsyncOperationWithCancellationSource NextNode => ref nextNode;

            static UnityWebRequestAsyncOperationWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(UnityWebRequestAsyncOperationWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperation> continuationAction;
            UnityWebRequestAsyncOperation asyncOperation;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<UnityWebRequest> core;

            UnityWebRequestAsyncOperationWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource<UnityWebRequest> Create(UnityWebRequestAsyncOperation asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityWebRequest>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new UnityWebRequestAsyncOperationWithCancellationSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                asyncOperation.completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperation _)
            {
                asyncOperation.completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        core.TrySetCanceled(cancellationToken);
                        return;
                    }

                    var result = asyncOperation.webRequest;
                    if (result.IsError())
                    {
                        core.TrySetException(new UnityWebRequestException(result));
                    }
                    else
                    {
                        core.TrySetResult(result);
                    }
                }
            }

            public UnityWebRequest GetResult(short token)
            {
                return core.GetResult(token);
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
                if (completed)
                {
                    TryReturn();
                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    asyncOperation.webRequest.Abort();
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                asyncOperation = default;
                cancellationToken = default;
                return pool.TryPush(this);
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