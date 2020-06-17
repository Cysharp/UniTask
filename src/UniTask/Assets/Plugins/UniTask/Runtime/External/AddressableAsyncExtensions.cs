// asmdef Version Defines, enabled when com.unity.addressables is imported.

#if UNITASK_ADDRESSABLE_SUPPORT

using Cysharp.Threading.Tasks.Internal;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Cysharp.Threading.Tasks
{
    public static class AddressableAsyncExtensions
    {
        #region AsyncOperationHandle

        public static AsyncOperationHandleAwaiter GetAwaiter(this AsyncOperationHandle handle)
        {
            return new AsyncOperationHandleAwaiter(handle);
        }

        public static UniTask WithCancellation(this AsyncOperationHandle handle, CancellationToken cancellationToken)
        {
            if (handle.IsDone) return UniTask.CompletedTask;
            return new UniTask(AsyncOperationHandleWithCancellationSource.Create(handle, cancellationToken, out var token), token);
        }

        public static UniTask ToUniTask(this AsyncOperationHandle handle, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (handle.IsDone) return UniTask.CompletedTask;
            return new UniTask(AsyncOperationHandleConfiguredSource.Create(handle, timing, progress, cancellationToken, out var token), token);
        }

        public struct AsyncOperationHandleAwaiter : ICriticalNotifyCompletion
        {
            AsyncOperationHandle handle;
            Action<AsyncOperationHandle> continuationAction;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle handle)
            {
                this.handle = handle;
                this.continuationAction = null;
            }

            public bool IsCompleted => handle.IsDone;

            public void GetResult()
            {
                if (continuationAction != null)
                {
                    handle.Completed -= continuationAction;
                    continuationAction = null;
                }

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    var e = handle.OperationException;
                    handle = default;
                    ExceptionDispatchInfo.Capture(e).Throw();
                }

                var result = handle.Result;
                handle = default;
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperationHandle>.Create(continuation);
                handle.Completed += continuationAction;
            }
        }

        sealed class AsyncOperationHandleWithCancellationSource : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<AsyncOperationHandleWithCancellationSource>
        {
            static TaskPool<AsyncOperationHandleWithCancellationSource> pool;
            public AsyncOperationHandleWithCancellationSource NextNode { get; set; }

            static AsyncOperationHandleWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleWithCancellationSource), () => pool.Size);
            }

            readonly Action<AsyncOperationHandle> continuationAction;
            AsyncOperationHandle handle;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            AsyncOperationHandleWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource Create(AsyncOperationHandle handle, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleWithCancellationSource();
                }

                result.handle = handle;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                handle.Completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperationHandle _)
            {
                handle.Completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        core.TrySetException(handle.OperationException);
                    }
                    else
                    {
                        core.TrySetResult(AsyncUnit.Default);
                    }
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
                handle = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        sealed class AsyncOperationHandleConfiguredSource : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<AsyncOperationHandleConfiguredSource>
        {
            static TaskPool<AsyncOperationHandleConfiguredSource> pool;
            public AsyncOperationHandleConfiguredSource NextNode { get; set; }

            static AsyncOperationHandleConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleConfiguredSource), () => pool.Size);
            }

            AsyncOperationHandle handle;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            AsyncOperationHandleConfiguredSource()
            {

            }

            public static IUniTaskSource Create(AsyncOperationHandle handle, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleConfiguredSource();
                }

                result.handle = handle;
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
                    progress.Report(handle.PercentComplete);
                }

                if (handle.IsDone)
                {
                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        core.TrySetException(handle.OperationException);
                    }
                    else
                    {
                        core.TrySetResult(AsyncUnit.Default);
                    }
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                core.Reset();
                TaskTracker.RemoveTracking(this);
                handle = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion

        #region AsyncOperationHandle_T

        public static AsyncOperationHandleAwaiter<T> GetAwaiter<T>(this AsyncOperationHandle<T> handle)
        {
            return new AsyncOperationHandleAwaiter<T>(handle);
        }

        public static UniTask<T> WithCancellation<T>(this AsyncOperationHandle<T> handle, CancellationToken cancellationToken)
        {
            if (handle.IsDone) return UniTask.FromResult(handle.Result);
            return new UniTask<T>(AsyncOperationHandleWithCancellationSource<T>.Create(handle, cancellationToken, out var token), token);
        }

        public static UniTask<T> ToUniTask<T>(this AsyncOperationHandle<T> handle, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (handle.IsDone) return UniTask.FromResult(handle.Result);
            return new UniTask<T>(AsyncOperationHandleConfiguredSource<T>.Create(handle, timing, progress, cancellationToken, out var token), token);
        }

        public struct AsyncOperationHandleAwaiter<T> : ICriticalNotifyCompletion
        {
            AsyncOperationHandle<T> handle;
            Action<AsyncOperationHandle> continuationAction;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle<T> handle)
            {
                this.handle = handle;
                this.continuationAction = null;
            }

            public bool IsCompleted => handle.IsDone;

            public T GetResult()
            {
                if (continuationAction != null)
                {
                    handle.CompletedTypeless -= continuationAction;
                    continuationAction = null;
                }

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    var e = handle.OperationException;
                    handle = default;
                    ExceptionDispatchInfo.Capture(e).Throw();
                }

                var result = handle.Result;
                handle = default;
                return result;
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperationHandle>.Create(continuation);
                handle.CompletedTypeless += continuationAction;
            }
        }

        sealed class AsyncOperationHandleWithCancellationSource<T> : IUniTaskSource<T>, IPlayerLoopItem, ITaskPoolNode<AsyncOperationHandleWithCancellationSource<T>>
        {
            static TaskPool<AsyncOperationHandleWithCancellationSource<T>> pool;
            public AsyncOperationHandleWithCancellationSource<T> NextNode { get; set; }

            static AsyncOperationHandleWithCancellationSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleWithCancellationSource<T>), () => pool.Size);
            }

            readonly Action<AsyncOperationHandle<T>> continuationAction;
            AsyncOperationHandle<T> handle;
            CancellationToken cancellationToken;
            bool completed;

            UniTaskCompletionSourceCore<T> core;

            AsyncOperationHandleWithCancellationSource()
            {
                continuationAction = Continuation;
            }

            public static IUniTaskSource<T> Create(AsyncOperationHandle<T> handle, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleWithCancellationSource<T>();
                }

                result.handle = handle;
                result.cancellationToken = cancellationToken;
                result.completed = false;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, result);

                handle.Completed += result.continuationAction;

                token = result.core.Version;
                return result;
            }

            void Continuation(AsyncOperationHandle<T> _)
            {
                handle.Completed -= continuationAction;

                if (completed)
                {
                    TryReturn();
                }
                else
                {
                    completed = true;
                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        core.TrySetException(handle.OperationException);
                    }
                    else
                    {
                        core.TrySetResult(handle.Result);
                    }
                }
            }

            public T GetResult(short token)
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
                handle = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        sealed class AsyncOperationHandleConfiguredSource<T> : IUniTaskSource<T>, IPlayerLoopItem, ITaskPoolNode<AsyncOperationHandleConfiguredSource<T>>
        {
            static TaskPool<AsyncOperationHandleConfiguredSource<T>> pool;
            public AsyncOperationHandleConfiguredSource<T> NextNode { get; set; }

            static AsyncOperationHandleConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleConfiguredSource<T>), () => pool.Size);
            }

            AsyncOperationHandle<T> handle;
            IProgress<float> progress;
            CancellationToken cancellationToken;

            UniTaskCompletionSourceCore<T> core;

            AsyncOperationHandleConfiguredSource()
            {

            }

            public static IUniTaskSource<T> Create(AsyncOperationHandle<T> handle, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleConfiguredSource<T>();
                }

                result.handle = handle;
                result.progress = progress;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public T GetResult(short token)
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
                    progress.Report(handle.PercentComplete);
                }

                if (handle.IsDone)
                {
                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        core.TrySetException(handle.OperationException);
                    }
                    else
                    {
                        core.TrySetResult(handle.Result);
                    }
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                handle = default;
                progress = default;
                cancellationToken = default;
                return pool.TryPush(this);
            }
        }

        #endregion
    }
}

#endif