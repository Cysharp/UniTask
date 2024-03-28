#if UNITASK_ASYNCINSTANTIATE_SUPPORT
using System;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cysharp.Threading.Tasks
{
    public static class AsyncInstantiateOperationExtensions
    {
        public static UniTask<T[]>.Awaiter GetAwaiter<T>(this AsyncInstantiateOperation<T> operation)
            where T : Object
        {
            return ToUniTask(operation).GetAwaiter();
        }

        public static UniTask<T[]> WithCancellation<T>(this AsyncInstantiateOperation<T> operation, CancellationToken cancellationToken)
            where T : Object
        {
            return ToUniTask(operation, cancellationToken: cancellationToken);
        }

        public static UniTask<T[]> ToUniTask<T>(this AsyncInstantiateOperation<T> operation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
            where T : Object
        {
            if (cancellationToken.IsCancellationRequested)
            {
                operation.Cancel();

                return UniTask.FromCanceled<T[]>(cancellationToken);
            }

            if (operation.isDone)
            {
                return UniTask.FromResult(operation.Result);
            }

            return new UniTask<T[]>(AsyncInstantiateOperationConfiguredSource<T>.Create(operation, timing, progress, cancellationToken, out var token), token);
        }

        private sealed class AsyncInstantiateOperationConfiguredSource<T> : IUniTaskSource<T[]>, IPlayerLoopItem, ITaskPoolNode<AsyncInstantiateOperationConfiguredSource<T>>
            where T : Object
        {
            private static TaskPool<AsyncInstantiateOperationConfiguredSource<T>> pool;
            private AsyncInstantiateOperationConfiguredSource<T> nextNode;

            private AsyncInstantiateOperation<T> operation;
            private IProgress<float> progress;
            private CancellationToken cancellationToken;

            private bool completed;
            private UniTaskCompletionSourceCore<T[]> core;

            static AsyncInstantiateOperationConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncInstantiateOperationConfiguredSource<T>), () => pool.Size);
            }

            public static IUniTaskSource<T[]> Create(AsyncInstantiateOperation<T> operation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (!pool.TryPop(out var source))
                {
                    source = new AsyncInstantiateOperationConfiguredSource<T>();
                }

                source.operation = operation;
                source.progress = progress;
                source.cancellationToken = cancellationToken;
                source.completed = false;

                TaskTracker.TrackActiveTask(source, 3);
                PlayerLoopHelper.AddAction(timing, source);

                operation.completed += source.OnCompleted;
                token = source.core.Version;

                return source;
            }

            private void OnCompleted(AsyncOperation asyncOperation)
            {
                operation.completed -= OnCompleted;

                if (completed)
                {
                    Destroy();
                }
                else
                {
                    completed = true;

                    if (cancellationToken.IsCancellationRequested)
                    {
                        operation.Cancel();
                        core.TrySetCanceled(cancellationToken);
                    }
                    else
                    {
                        core.TrySetResult(operation.Result);
                    }
                }
            }

            private void Destroy()
            {
                TaskTracker.RemoveTracking(this);

                core.Reset();
                operation = default;
                progress = default;
                cancellationToken = default;

                pool.TryPush(this);
            }

            void IUniTaskSource.GetResult(short token)
            {
                core.GetResult(token);
            }

            UniTaskStatus IUniTaskSource.GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            UniTaskStatus IUniTaskSource.UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            T[] IUniTaskSource<T[]>.GetResult(short token)
            {
                return core.GetResult(token);
            }

            void IUniTaskSource<T[]>.OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            bool IPlayerLoopItem.MoveNext()
            {
                if (completed)
                {
                    Destroy();

                    return false;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completed = true;
                    operation.Cancel();
                    core.TrySetCanceled(cancellationToken);

                    return false;
                }

                progress?.Report(operation.progress);

                return true;
            }

            ref AsyncInstantiateOperationConfiguredSource<T> ITaskPoolNode<AsyncInstantiateOperationConfiguredSource<T>>.NextNode => ref nextNode;
        }
    }
}
#endif
