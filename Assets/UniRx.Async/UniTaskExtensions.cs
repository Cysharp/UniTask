#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using UniRx.Async.Internal;

namespace UniRx.Async
{
    public static partial class UniTaskExtensions
    {
        /// <summary>
        /// Convert UniTask -> UniTask[AsyncUnit].
        /// </summary>
        public static UniTask<AsyncUnit> AsAsyncUnitUniTask(this UniTask task)
        {
            // use implicit conversion
            return task;
        }

        /// <summary>
        /// Convert Task[T] -> UniTask[T].
        /// </summary>
        public static UniTask<T> AsUniTask<T>(this Task<T> task, bool useCurrentSynchronizationContext = true)
        {
            var promise = new UniTaskCompletionSource<T>();

            task.ContinueWith((x, state) =>
            {
                var p = (UniTaskCompletionSource<T>)state;

                switch (x.Status)
                {
                    case TaskStatus.Canceled:
                        p.TrySetCanceled();
                        break;
                    case TaskStatus.Faulted:
                        p.TrySetException(x.Exception);
                        break;
                    case TaskStatus.RanToCompletion:
                        p.TrySetResult(x.Result);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }, promise, useCurrentSynchronizationContext ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);

            return new UniTask<T>(promise);
        }

        /// <summary>
        /// Convert Task -> UniTask.
        /// </summary>
        public static UniTask AsUniTask(this Task task, bool useCurrentSynchronizationContext = true)
        {
            var promise = new UniTaskCompletionSource<AsyncUnit>();

            task.ContinueWith((x, state) =>
            {
                var p = (UniTaskCompletionSource<AsyncUnit>)state;

                switch (x.Status)
                {
                    case TaskStatus.Canceled:
                        p.TrySetCanceled();
                        break;
                    case TaskStatus.Faulted:
                        p.TrySetException(x.Exception);
                        break;
                    case TaskStatus.RanToCompletion:
                        p.TrySetResult(default(AsyncUnit));
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }, promise, useCurrentSynchronizationContext ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);

            return new UniTask(promise);
        }

        public static IEnumerator ToCoroutine<T>(this UniTask<T> task, Action<T> resultHandler = null, Action<Exception> exceptionHandler = null)
        {
            return new ToCoroutineEnumerator<T>(task, resultHandler, exceptionHandler);
        }

        public static IEnumerator ToCoroutine(this UniTask task, Action<Exception> exceptionHandler = null)
        {
            return new ToCoroutineEnumerator(task, exceptionHandler);
        }

        public static UniTask Timeout(this UniTask task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
        {
            return Timeout(task.AsAsyncUnitUniTask(), timeout, ignoreTimeScale, timeoutCheckTiming, taskCancellationTokenSource);
        }

        public static async UniTask<T> Timeout<T>(this UniTask<T> task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
        {
            // left, right both suppress operation canceled exception.

            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = (UniTask)UniTask.Delay(timeout, ignoreTimeScale, timeoutCheckTiming, delayCancellationTokenSource.Token).SuppressCancellationThrow();

            var (hasValue, value) = await UniTask.WhenAny(task.SuppressCancellationThrow(), timeoutTask);

            if (!hasValue)
            {
                if (taskCancellationTokenSource != null)
                {
                    taskCancellationTokenSource.Cancel();
                    taskCancellationTokenSource.Dispose();
                }

                throw new TimeoutException("Exceed Timeout:" + timeout);
            }
            else
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
            }

            if (value.IsCanceled)
            {
                Error.ThrowOperationCanceledException();
            }

            return value.Result;
        }

        /// <summary>
        /// Timeout with suppress OperationCanceledException. Returns (bool, IsCacneled).
        /// </summary>
        public static async UniTask<bool> TimeoutWithoutException(this UniTask task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
        {
            var v = await TimeoutWithoutException(task.AsAsyncUnitUniTask(), timeout, ignoreTimeScale, timeoutCheckTiming, taskCancellationTokenSource);
            return v.IsTimeout;
        }


        /// <summary>
        /// Timeout with suppress OperationCanceledException. Returns (bool IsTimeout, T Result).
        /// </summary>
        public static async UniTask<(bool IsTimeout, T Result)> TimeoutWithoutException<T>(this UniTask<T> task, TimeSpan timeout, bool ignoreTimeScale = true, PlayerLoopTiming timeoutCheckTiming = PlayerLoopTiming.Update, CancellationTokenSource taskCancellationTokenSource = null)
        {
            // left, right both suppress operation canceled exception.

            var delayCancellationTokenSource = new CancellationTokenSource();
            var timeoutTask = (UniTask)UniTask.Delay(timeout, ignoreTimeScale, timeoutCheckTiming, delayCancellationTokenSource.Token).SuppressCancellationThrow();

            var (hasValue, value) = await UniTask.WhenAny(task.SuppressCancellationThrow(), timeoutTask);

            if (!hasValue)
            {
                if (taskCancellationTokenSource != null)
                {
                    taskCancellationTokenSource.Cancel();
                    taskCancellationTokenSource.Dispose();
                }

                return (true, default(T));
            }
            else
            {
                delayCancellationTokenSource.Cancel();
                delayCancellationTokenSource.Dispose();
            }

            if (value.IsCanceled)
            {
                Error.ThrowOperationCanceledException();
            }

            return (false, value.Result);
        }

        public static void Forget(this UniTask task)
        {
            ForgetCore(task).Forget();
        }

        public static void Forget(this UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
        {
            if (exceptionHandler == null)
            {
                ForgetCore(task).Forget();
            }
            else
            {
                ForgetCoreWithCatch(task, exceptionHandler, handleExceptionOnMainThread).Forget();
            }
        }

        // UniTask to UniTaskVoid
        static async UniTaskVoid ForgetCore(UniTask task)
        {
            await task;
        }

        static async UniTaskVoid ForgetCoreWithCatch(UniTask task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                try
                {
                    if (handleExceptionOnMainThread)
                    {
                        await UniTask.SwitchToMainThread();
                    }
                    exceptionHandler(ex);
                }
                catch (Exception ex2)
                {
                    UniTaskScheduler.PublishUnobservedTaskException(ex2);
                }
            }
        }

        public static void Forget<T>(this UniTask<T> task)
        {
            ForgetCore(task).Forget();
        }

        public static void Forget<T>(this UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread = true)
        {
            if (exceptionHandler == null)
            {
                ForgetCore(task).Forget();
            }
            else
            {
                ForgetCoreWithCatch(task, exceptionHandler, handleExceptionOnMainThread).Forget();
            }
        }

        // UniTask to UniTaskVoid
        static async UniTaskVoid ForgetCore<T>(UniTask<T> task)
        {
            await task;
        }

        static async UniTaskVoid ForgetCoreWithCatch<T>(UniTask<T> task, Action<Exception> exceptionHandler, bool handleExceptionOnMainThread)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                try
                {
                    if (handleExceptionOnMainThread)
                    {
                        await UniTask.SwitchToMainThread();
                    }
                    exceptionHandler(ex);
                }
                catch (Exception ex2)
                {
                    UniTaskScheduler.PublishUnobservedTaskException(ex2);
                }
            }
        }

        public static async UniTask ContinueWith<T>(this UniTask<T> task, Action<T> continuationFunction)
        {
            continuationFunction(await task);
        }

        public static async UniTask ContinueWith<T>(this UniTask<T> task, Func<T, UniTask> continuationFunction)
        {
            await continuationFunction(await task);
        }

        public static async UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, TR> continuationFunction)
        {
            return continuationFunction(await task);
        }

        public static async UniTask<TR> ContinueWith<T, TR>(this UniTask<T> task, Func<T, UniTask<TR>> continuationFunction)
        {
            return await continuationFunction(await task);
        }

        public static async UniTask ContinueWith(this UniTask task, Action continuationFunction)
        {
            await task;
            continuationFunction();
        }

        public static async UniTask ContinueWith(this UniTask task, Func<UniTask> continuationFunction)
        {
            await task;
            await continuationFunction();
        }

        public static async UniTask<T> ContinueWith<T>(this UniTask task, Func<T> continuationFunction)
        {
            await task;
            return continuationFunction();
        }

        public static async UniTask<T> ContinueWith<T>(this UniTask task, Func<UniTask<T>> continuationFunction)
        {
            await task;
            return await continuationFunction();
        }

        public static async UniTask ConfigureAwait(this Task task, PlayerLoopTiming timing)
        {
            await task.ConfigureAwait(false);
            await UniTask.Yield(timing);
        }

        public static async UniTask<T> ConfigureAwait<T>(this Task<T> task, PlayerLoopTiming timing)
        {
            var v = await task.ConfigureAwait(false);
            await UniTask.Yield(timing);
            return v;
        }

        public static async UniTask ConfigureAwait(this UniTask task, PlayerLoopTiming timing)
        {
            await task;
            await UniTask.Yield(timing);
        }

        public static async UniTask<T> ConfigureAwait<T>(this UniTask<T> task, PlayerLoopTiming timing)
        {
            var v = await task;
            await UniTask.Yield(timing);
            return v;
        }

        public static async UniTask<T> Unwrap<T>(this UniTask<UniTask<T>> task)
        {
            return await await task;
        }

        public static async UniTask Unwrap<T>(this UniTask<UniTask> task)
        {
            await await task;
        }

        class ToCoroutineEnumerator : IEnumerator
        {
            bool completed;
            UniTask task;
            Action<Exception> exceptionHandler = null;
            bool isStarted = false;
            ExceptionDispatchInfo exception;

            public ToCoroutineEnumerator(UniTask task, Action<Exception> exceptionHandler)
            {
                completed = false;
                this.exceptionHandler = exceptionHandler;
                this.task = task;
            }

            async UniTaskVoid RunTask(UniTask task)
            {
                try
                {
                    await task;
                }
                catch (Exception ex)
                {
                    if (exceptionHandler != null)
                    {
                        exceptionHandler(ex);
                    }
                    else
                    {
                        this.exception = ExceptionDispatchInfo.Capture(ex);
                    }
                }
                finally
                {
                    completed = true;
                }
            }

            public object Current => null;

            public bool MoveNext()
            {
                if (!isStarted)
                {
                    isStarted = true;
                    RunTask(task).Forget();
                }

                if (exception != null)
                {
                    // throw exception on iterator (main)thread.
                    // unfortunately unity test-runner can not handle throw exception on hand-write IEnumerator.MoveNext.
                    UnityEngine.Debug.LogException(exception.SourceException);
                }

                return !completed;
            }

            public void Reset()
            {
            }
        }

        class ToCoroutineEnumerator<T> : IEnumerator
        {
            bool completed;
            Action<T> resultHandler = null;
            Action<Exception> exceptionHandler = null;
            bool isStarted = false;
            UniTask<T> task;
            object current = null;
            ExceptionDispatchInfo exception;

            public ToCoroutineEnumerator(UniTask<T> task, Action<T> resultHandler, Action<Exception> exceptionHandler)
            {
                completed = false;
                this.task = task;
                this.resultHandler = resultHandler;
                this.exceptionHandler = exceptionHandler;
            }

            async UniTaskVoid RunTask(UniTask<T> task)
            {
                try
                {
                    var value = await task;
                    current = value;
                    if (resultHandler != null)
                    {
                        resultHandler(value);
                    }
                }
                catch (Exception ex)
                {
                    if (exceptionHandler != null)
                    {
                        exceptionHandler(ex);
                    }
                    else
                    {
                        this.exception = ExceptionDispatchInfo.Capture(ex);
                    }
                }
                finally
                {
                    completed = true;
                }
            }

            public object Current => current;

            public bool MoveNext()
            {
                if (!isStarted)
                {
                    isStarted = true;
                    RunTask(task).Forget();
                }

                if (exception != null)
                {
                    // throw exception on iterator (main)thread.
                    // unfortunately unity test-runner can not handle throw exception on hand-write IEnumerator.MoveNext.
                    UnityEngine.Debug.LogException(exception.SourceException);
                }

                return !completed;
            }

            public void Reset()
            {
            }
        }
    }
}

#endif