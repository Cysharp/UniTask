#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

#if UNITY_2018_4 || UNITY_2019_4_OR_NEWER
#if UNITASK_ASSETBUNDLE_SUPPORT

using Cysharp.Threading.Tasks.Internal;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        public static AssetBundleRequestAllAssetsAwaiter AwaitForAllAssets(this AssetBundleRequest asyncOperation)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            return new AssetBundleRequestAllAssetsAwaiter(asyncOperation);
        }

        public static UniTask<UnityEngine.Object[]> AwaitForAllAssets(this AssetBundleRequest asyncOperation, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object[]>(cancellationToken);
            if (asyncOperation.isDone) return UniTask.FromResult<UnityEngine.Object[]>(asyncOperation.allAssets);
            return new UniTask<UnityEngine.Object[]>(AssetBundleRequestAllAssetsWithCancellationSource.Create(asyncOperation, cancellationToken, out var token), token);
        }

        public static UniTask<UnityEngine.Object[]> AwaitForAllAssets(this AssetBundleRequest asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(asyncOperation, nameof(asyncOperation));
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<UnityEngine.Object[]>(cancellationToken);
            if (asyncOperation.isDone) return UniTask.FromResult(asyncOperation.allAssets);
            return new UniTask<UnityEngine.Object[]>(AssetBundleRequestAllAssetsConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, out var token), token);
        }

        public struct AssetBundleRequestAllAssetsAwaiter : ICriticalNotifyCompletion
        {
            AssetBundleRequest asyncOperation;
            Action<AsyncOperation> continuationAction;

            public AssetBundleRequestAllAssetsAwaiter(AssetBundleRequest asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public AssetBundleRequestAllAssetsAwaiter GetAwaiter()
            {
                return this;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public UnityEngine.Object[] GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                    var result = asyncOperation.allAssets;
                    asyncOperation = null;
                    return result;
                }
                else
                {
                    var result = asyncOperation.allAssets;
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

        sealed class AssetBundleRequestAllAssetsWithCancellationSource : IUniTaskSource<UnityEngine.Object[]>, IPlayerLoopItem, ITaskPoolNode<AssetBundleRequestAllAssetsWithCancellationSource>
        {
            static TaskPool<AssetBundleRequestAllAssetsWithCancellationSource> pool;
            AssetBundleRequestAllAssetsWithCancellationSource nextNode;
            public ref AssetBundleRequestAllAssetsWithCancellationSource NextNode => ref nextNode;

            static AssetBundleRequestAllAssetsWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleRequestAllAssetsWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperation> continuationAction;
            AssetBundleRequest asyncOperation;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<UnityEngine.Object[]> core;

            AssetBundleRequestAllAssetsWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource<UnityEngine.Object[]> Create(AssetBundleRequest asyncOperation, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object[]>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestAllAssetsWithCancellationSource();
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

                    core.TrySetResult(asyncOperation.allAssets);
                }
            }

            public UnityEngine.Object[] GetResult(short token)
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

        sealed class AssetBundleRequestAllAssetsConfiguredSource : IUniTaskSource<UnityEngine.Object[]>, IPlayerLoopItem, ITaskPoolNode<AssetBundleRequestAllAssetsConfiguredSource>
        {
            static TaskPool<AssetBundleRequestAllAssetsConfiguredSource> pool;
            AssetBundleRequestAllAssetsConfiguredSource nextNode;
            public ref AssetBundleRequestAllAssetsConfiguredSource NextNode => ref nextNode;

            static AssetBundleRequestAllAssetsConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AssetBundleRequestAllAssetsConfiguredSource), () => pool.Size);
            }

            AssetBundleRequest asyncOperation;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<UnityEngine.Object[]> core;

            AssetBundleRequestAllAssetsConfiguredSource()
            {

            }

            public static IUniTaskSource<UnityEngine.Object[]> Create(AssetBundleRequest asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<UnityEngine.Object[]>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AssetBundleRequestAllAssetsConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public UnityEngine.Object[] GetResult(short token)
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
                    core.TrySetResult(asyncOperation.allAssets);
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
    }
}

#endif
#endif