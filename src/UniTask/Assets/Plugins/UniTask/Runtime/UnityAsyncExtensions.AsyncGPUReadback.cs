#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using UnityEngine.Rendering;

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        #region AsyncGPUReadbackRequest

        public static UniTask<AsyncGPUReadbackRequest>.Awaiter GetAwaiter(this AsyncGPUReadbackRequest asyncOperation)
        {
            return ToUniTask(asyncOperation).GetAwaiter();
        }

        public static UniTask<AsyncGPUReadbackRequest> WithCancellation(this AsyncGPUReadbackRequest asyncOperation, CancellationToken cancellationToken)
        {
            return ToUniTask(asyncOperation, cancellationToken: cancellationToken);
        }

        public static UniTask<AsyncGPUReadbackRequest> ToUniTask(this AsyncGPUReadbackRequest asyncOperation, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (asyncOperation.done) return UniTask.FromResult(asyncOperation);
            return new UniTask<AsyncGPUReadbackRequest>(AsyncGPUReadbackRequestAwaiterConfiguredSource.Create(asyncOperation, timing, cancellationToken, out var token), token);
        }

        sealed class AsyncGPUReadbackRequestAwaiterConfiguredSource : IUniTaskSource<AsyncGPUReadbackRequest>, IPlayerLoopItem, ITaskPoolNode<AsyncGPUReadbackRequestAwaiterConfiguredSource>
        {
            static TaskPool<AsyncGPUReadbackRequestAwaiterConfiguredSource> pool;
            AsyncGPUReadbackRequestAwaiterConfiguredSource nextNode;
            public ref AsyncGPUReadbackRequestAwaiterConfiguredSource NextNode => ref nextNode;

            static AsyncGPUReadbackRequestAwaiterConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncGPUReadbackRequestAwaiterConfiguredSource), () => pool.Size);
            }

            AsyncGPUReadbackRequest asyncOperation;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<AsyncGPUReadbackRequest> core;

            AsyncGPUReadbackRequestAwaiterConfiguredSource()
            {

            }

            public static IUniTaskSource<AsyncGPUReadbackRequest> Create(AsyncGPUReadbackRequest asyncOperation, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<AsyncGPUReadbackRequest>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncGPUReadbackRequestAwaiterConfiguredSource();
                }

                result.asyncOperation = asyncOperation;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public AsyncGPUReadbackRequest GetResult(short token)
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

                if (asyncOperation.hasError)
                {
                    core.TrySetException(new Exception("AsyncGPUReadbackRequest.hasError = true"));
                    return false;
                }

                if (asyncOperation.done)
                {
                    core.TrySetResult(asyncOperation);
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

        #endregion
    }
}