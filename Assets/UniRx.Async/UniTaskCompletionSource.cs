#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using UniRx.Async.Internal;

namespace UniRx.Async
{
    // TODO: Remove all?

    internal class ExceptionHolder
    {
        ExceptionDispatchInfo exception;
        bool calledGet = false;

        public ExceptionHolder(ExceptionDispatchInfo exception)
        {
            this.exception = exception;
        }

        public ExceptionDispatchInfo GetException()
        {
            if (!calledGet)
            {
                calledGet = true;
                GC.SuppressFinalize(this);
            }
            return exception;
        }

        ~ExceptionHolder()
        {
            UniTaskScheduler.PublishUnobservedTaskException(exception.SourceException);
        }
    }

    public interface IResolvePromise
    {
        bool TrySetResult();
    }

    public interface IResolvePromise<T>
    {
        bool TrySetResult(T value);
    }

    public interface IRejectPromise
    {
        bool TrySetException(Exception exception);
    }

    public interface ICancelPromise
    {
        bool TrySetCanceled();
    }

    public interface IPromise<T> : IResolvePromise<T>, IRejectPromise, ICancelPromise
    {
    }

    public interface IPromise : IResolvePromise, IRejectPromise, ICancelPromise
    {
    }

    public class UniTaskCompletionSource : IAwaiter, IPromise
    {
        // State(= AwaiterStatus)
        const int Pending = 0;
        const int Succeeded = 1;
        const int Faulted = 2;
        const int Canceled = 3;

        int state = 0;
        bool handled = false;
        ExceptionHolder exception;
        object continuation; // action or list

        AwaiterStatus IAwaiter.Status => (AwaiterStatus)state;

        bool IAwaiter.IsCompleted => state != Pending;

        public UniTask Task => new UniTask(this);

        public UniTaskCompletionSource()
        {
            TaskTracker.TrackActiveTask(this, 2);
        }

        [Conditional("UNITY_EDITOR")]
        internal void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker.RemoveTracking(this);
            }
        }

        void IAwaiter.GetResult()
        {
            MarkHandled();

            if (state == Succeeded)
            {
                return;
            }
            else if (state == Faulted)
            {
                exception.GetException().Throw();
            }
            else if (state == Canceled)
            {
                if (exception != null)
                {
                    exception.GetException().Throw(); // guranteed operation canceled exception.
                }

                throw new OperationCanceledException();
            }
            else // Pending
            {
                throw new NotSupportedException("UniTask does not allow call GetResult directly when task not completed. Please use 'await'.");
            }
        }

        void ICriticalNotifyCompletion.UnsafeOnCompleted(Action action)
        {
            if (Interlocked.CompareExchange(ref continuation, (object)action, null) == null)
            {
                if (state != Pending)
                {
                    TryInvokeContinuation();
                }
            }
            else
            {
                var c = continuation;
                if (c is Action)
                {
                    var list = new List<Action>();
                    list.Add((Action)c);
                    list.Add(action);
                    if (Interlocked.CompareExchange(ref continuation, list, c) == c)
                    {
                        goto TRYINVOKE;
                    }
                }

                var l = (List<Action>)continuation;
                lock (l)
                {
                    l.Add(action);
                }

                TRYINVOKE:
                if (state != Pending)
                {
                    TryInvokeContinuation();
                }
            }
        }

        void TryInvokeContinuation()
        {
            var c = Interlocked.Exchange(ref continuation, null);
            if (c != null)
            {
                if (c is Action)
                {
                    ((Action)c).Invoke();
                }
                else
                {
                    var l = (List<Action>)c;
                    var cnt = l.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        l[i].Invoke();
                    }
                }
            }
        }

        public bool TrySetResult()
        {
            if (Interlocked.CompareExchange(ref state, Succeeded, Pending) == Pending)
            {
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        public bool TrySetException(Exception exception)
        {
            if (Interlocked.CompareExchange(ref state, Faulted, Pending) == Pending)
            {
                this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        public bool TrySetCanceled()
        {
            if (Interlocked.CompareExchange(ref state, Canceled, Pending) == Pending)
            {
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        public bool TrySetCanceled(OperationCanceledException exception)
        {
            if (Interlocked.CompareExchange(ref state, Canceled, Pending) == Pending)
            {
                this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            ((ICriticalNotifyCompletion)this).UnsafeOnCompleted(continuation);
        }
    }

    public class UniTaskCompletionSource<T> : IAwaiter<T>, IPromise<T>
    {
        // State(= AwaiterStatus)
        const int Pending = 0;
        const int Succeeded = 1;
        const int Faulted = 2;
        const int Canceled = 3;

        int state = 0;
        T value;
        bool handled = false;
        ExceptionHolder exception;
        object continuation; // action or list

        bool IAwaiter.IsCompleted => state != Pending;

        public UniTask<T> Task => new UniTask<T>(this);
        public UniTask UnitTask => new UniTask(this);

        AwaiterStatus IAwaiter.Status => (AwaiterStatus)state;

        public UniTaskCompletionSource()
        {
            TaskTracker.TrackActiveTask(this, 2);
        }

        [Conditional("UNITY_EDITOR")]
        internal void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker.RemoveTracking(this);
            }
        }

        T IAwaiter<T>.GetResult()
        {
            MarkHandled();

            if (state == Succeeded)
            {
                return value;
            }
            else if (state == Faulted)
            {
                exception.GetException().Throw();
            }
            else if (state == Canceled)
            {
                if (exception != null)
                {
                    exception.GetException().Throw(); // guranteed operation canceled exception.
                }

                throw new OperationCanceledException();
            }
            else // Pending
            {
                throw new NotSupportedException("UniTask does not allow call GetResult directly when task not completed. Please use 'await'.");
            }

            return default(T);
        }

        void ICriticalNotifyCompletion.UnsafeOnCompleted(Action action)
        {
            if (Interlocked.CompareExchange(ref continuation, (object)action, null) == null)
            {
                if (state != Pending)
                {
                    TryInvokeContinuation();
                }
            }
            else
            {
                var c = continuation;
                if (c is Action)
                {
                    var list = new List<Action>();
                    list.Add((Action)c);
                    list.Add(action);
                    if (Interlocked.CompareExchange(ref continuation, list, c) == c)
                    {
                        goto TRYINVOKE;
                    }
                }

                var l = (List<Action>)continuation;
                lock (l)
                {
                    l.Add(action);
                }

                TRYINVOKE:
                if (state != Pending)
                {
                    TryInvokeContinuation();
                }
            }
        }

        void TryInvokeContinuation()
        {
            var c = Interlocked.Exchange(ref continuation, null);
            if (c != null)
            {
                if (c is Action)
                {
                    ((Action)c).Invoke();
                }
                else
                {
                    var l = (List<Action>)c;
                    var cnt = l.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        l[i].Invoke();
                    }
                }
            }
        }

        public bool TrySetResult(T value)
        {
            if (Interlocked.CompareExchange(ref state, Succeeded, Pending) == Pending)
            {
                this.value = value;
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        public bool TrySetException(Exception exception)
        {
            if (Interlocked.CompareExchange(ref state, Faulted, Pending) == Pending)
            {
                this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        public bool TrySetCanceled()
        {
            if (Interlocked.CompareExchange(ref state, Canceled, Pending) == Pending)
            {
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        public bool TrySetCanceled(OperationCanceledException exception)
        {
            if (Interlocked.CompareExchange(ref state, Canceled, Pending) == Pending)
            {
                this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
                TryInvokeContinuation();
                return true;
            }
            return false;
        }

        void IAwaiter.GetResult()
        {
            ((IAwaiter<T>)this).GetResult();
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            ((ICriticalNotifyCompletion)this).UnsafeOnCompleted(continuation);
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public struct UniTaskCompletionSourceCore<TResult>
    {
        // Struct Size: TResult + (8 + 2 + 1 + 1 + 8 + 8)

        TResult result;
        object error; // Exception or OperationCanceledException
        short version;
        bool completed;
        bool hasUnhandledError;

        Action<object> continuation;
        object continuationState;

        public void Reset()
        {
            ReportUnhandledError();

            unchecked
            {
                version += 1; // incr version.
            }
            completed = false;
            result = default;
            error = null;
            hasUnhandledError = false;
            continuation = null;
            continuationState = null;
        }

        void ReportUnhandledError()
        {
            if (hasUnhandledError)
            {
                try
                {
                    if (error is OperationCanceledException oc)
                    {
                        UniTaskScheduler.PublishUnobservedTaskException(oc);
                    }
                    else if (error is ExceptionDispatchInfo ei)
                    {
                        UniTaskScheduler.PublishUnobservedTaskException(ei.SourceException);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>Completes with a successful result.</summary>
        /// <param name="result">The result.</param>
        public void SetResult(TResult result)
        {
            this.result = result;
            SignalCompletion();
        }

        /// <summary>Completes with an error.</summary>
        /// <param name="error">The exception.</param>
        public void SetException(Exception error)
        {
            this.hasUnhandledError = true;
            this.error = ExceptionDispatchInfo.Capture(error);
            SignalCompletion();
        }

        public void SetCancellation(CancellationToken cancellationToken)
        {
            this.error = new OperationCanceledException(cancellationToken);
            SignalCompletion();
        }

        /// <summary>Gets the operation version.</summary>
        public short Version => version;

        /// <summary>Gets the status of the operation.</summary>
        /// <param name="token">Opaque value that was provided to the <see cref="UniTask"/>'s constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AwaiterStatus GetStatus(short token)
        {
            ValidateToken(token);
            return (continuation == null || !completed) ? AwaiterStatus.Pending
                 : (error == null) ? AwaiterStatus.Succeeded
                 : (error is OperationCanceledException) ? AwaiterStatus.Canceled
                 : AwaiterStatus.Faulted;
        }

        /// <summary>Gets the status of the operation without token validation.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AwaiterStatus UnsafeGetStatus()
        {
            return (continuation == null || !completed) ? AwaiterStatus.Pending
                 : (error == null) ? AwaiterStatus.Succeeded
                 : (error is OperationCanceledException) ? AwaiterStatus.Canceled
                 : AwaiterStatus.Faulted;
        }

        /// <summary>Gets the result of the operation.</summary>
        /// <param name="token">Opaque value that was provided to the <see cref="UniTask"/>'s constructor.</param>
        // [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult GetResult(short token)
        {
            ValidateToken(token);
            if (!completed)
            {
                throw new InvalidOperationException("not yet completed.");
            }

            if (error != null)
            {
                hasUnhandledError = false;
                if (error is OperationCanceledException oce)
                {
                    throw oce;
                }
                else if (error is ExceptionDispatchInfo edi)
                {
                    edi.Throw();
                }

                throw new InvalidOperationException("Critical: invalid exception type was held.");
            }

            return result;
        }

        /// <summary>Schedules the continuation action for this operation.</summary>
        /// <param name="continuation">The continuation to invoke when the operation has completed.</param>
        /// <param name="state">The state object to pass to <paramref name="continuation"/> when it's invoked.</param>
        /// <param name="token">Opaque value that was provided to the <see cref="UniTask"/>'s constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action<object> continuation, object state, short token /*, ValueTaskSourceOnCompletedFlags flags */)
        {
            if (continuation == null)
            {
                throw new ArgumentNullException(nameof(continuation));
            }
            ValidateToken(token);

            /* no use ValueTaskSourceOnCOmpletedFlags, always no capture ExecutionContext and SynchronizationContext. */

            object oldContinuation = this.continuation;
            if (oldContinuation == null)
            {
                continuationState = state;
                oldContinuation = Interlocked.CompareExchange(ref this.continuation, continuation, null);
            }

            if (oldContinuation != null)
            {
                // Operation already completed, so we need to queue the supplied callback.
                if (!ReferenceEquals(oldContinuation, UniTaskCompletionSourceCoreShared.s_sentinel))
                {
                    throw new InvalidOperationException("already completed.");
                }

                continuation(state);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateToken(short token)
        {
            if (token != version)
            {
                throw new InvalidOperationException("token version is not matched.");
            }
        }

        /// <summary>Signals that the operation has completed. Invoked after the result or error has been set.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SignalCompletion()
        {
            if (completed)
            {
                throw new InvalidOperationException();
            }

            completed = true;

            if (continuation != null || Interlocked.CompareExchange(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
            {
                continuation(continuationState);
            }
        }
    }

    public class UniTaskCompletionSource2 : IUniTaskSource
    {
        UniTaskCompletionSourceCore<AsyncUnit> core;
        bool handled = false;

        public UniTaskCompletionSource2()
        {
            TaskTracker2.TrackActiveTask(this, 2);
        }

        [Conditional("UNITY_EDITOR")]
        void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker2.RemoveTracking(this);
            }
        }

        public UniTask2 Task
        {
            get
            {
                return new UniTask2(this, core.Version);
            }
        }

        public void Reset()
        {
            // Reset, re-active tracker
            handled = false;
            TaskTracker2.TrackActiveTask(this, 2);
            core.Reset();
        }

        public void SetResult()
        {
            core.SetResult(AsyncUnit.Default);
        }

        public void SetCancellation(CancellationToken cancellationToken)
        {
            core.SetCancellation(cancellationToken);
        }

        public void SetException(Exception exception)
        {
            core.SetException(exception);
        }

        public void GetResult(short token)
        {
            MarkHandled();
            core.GetResult(token);
        }

        public AwaiterStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public AwaiterStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        ~UniTaskCompletionSource2()
        {
            // clear error information.
            core.Reset();
        }
    }

    public class AutoResetUniTaskCompletionSource : IUniTaskSource, IPromisePoolItem
    {
        static readonly PromisePool<AutoResetUniTaskCompletionSource> pool = new PromisePool<AutoResetUniTaskCompletionSource>();

        UniTaskCompletionSourceCore<AsyncUnit> core;

        AutoResetUniTaskCompletionSource()
        {
        }

        public static AutoResetUniTaskCompletionSource Create()
        {
            var value = pool.TryRent() ?? new AutoResetUniTaskCompletionSource();
            TaskTracker2.TrackActiveTask(value, 2);
            return value;
        }

        public static AutoResetUniTaskCompletionSource CreateFromCanceled(CancellationToken cancellationToken, out short token)
        {
            var source = Create();
            source.SetCancellation(cancellationToken);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource CreateFromException(Exception exception, out short token)
        {
            var source = Create();
            source.SetException(exception);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource CreateCompleted(out short token)
        {
            var source = Create();
            source.SetResult();
            token = source.core.Version;
            return source;
        }

        public UniTask2 Task
        {
            get
            {
                return new UniTask2(this, core.Version);
            }
        }

        public void SetResult()
        {
            core.SetResult(AsyncUnit.Default);
        }

        public void SetCancellation(CancellationToken cancellationToken)
        {
            core.SetCancellation(cancellationToken);
        }

        public void SetException(Exception exception)
        {
            core.SetException(exception);
        }

        public void GetResult(short token)
        {
            try
            {
                TaskTracker2.RemoveTracking(this);
                core.GetResult(token);
            }
            finally
            {
                pool.TryReturn(this);
            }

        }

        public AwaiterStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public AwaiterStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        void IPromisePoolItem.Reset()
        {
            core.Reset();
        }

        ~AutoResetUniTaskCompletionSource()
        {
            if (pool.TryReturn(this))
            {
                GC.ReRegisterForFinalize(this);
                return;
            }
        }
    }

    public class UniTaskCompletionSource2<T> : IUniTaskSource<T>
    {
        UniTaskCompletionSourceCore<T> core;
        bool handled = false;

        public UniTaskCompletionSource2()
        {
            TaskTracker2.TrackActiveTask(this, 2);
        }

        [Conditional("UNITY_EDITOR")]
        void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker2.RemoveTracking(this);
            }
        }

        public UniTask2<T> Task
        {
            get
            {
                return new UniTask2<T>(this, core.Version);
            }
        }

        public void Reset()
        {
            handled = false;
            core.Reset();
            TaskTracker2.TrackActiveTask(this, 2);
        }

        public void SetResult(T result)
        {
            core.SetResult(result);
        }

        public void SetCancellation(CancellationToken cancellationToken)
        {
            core.SetCancellation(cancellationToken);
        }

        public void SetException(Exception exception)
        {
            core.SetException(exception);
        }

        public T GetResult(short token)
        {
            MarkHandled();
            return core.GetResult(token);
        }

        void IUniTaskSource.GetResult(short token)
        {
            GetResult(token);
        }

        public AwaiterStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public AwaiterStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        ~UniTaskCompletionSource2()
        {
            // clear error information.
            core.Reset();
        }
    }

    public class AutoResetUniTaskCompletionSource<T> : IUniTaskSource<T>, IPromisePoolItem
    {
        static readonly PromisePool<AutoResetUniTaskCompletionSource<T>> pool = new PromisePool<AutoResetUniTaskCompletionSource<T>>();

        UniTaskCompletionSourceCore<T> core;

        AutoResetUniTaskCompletionSource()
        {
        }

        public static AutoResetUniTaskCompletionSource<T> Create()
        {
            var result =  pool.TryRent() ?? new AutoResetUniTaskCompletionSource<T>();
            TaskTracker2.TrackActiveTask(result, 2);
            return result;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromCanceled(CancellationToken cancellationToken, out short token)
        {
            var source = Create();
            source.SetCancellation(cancellationToken);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromException(Exception exception, out short token)
        {
            var source = Create();
            source.SetException(exception);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromResult(T result, out short token)
        {
            var source = Create();
            source.SetResult(result);
            token = source.core.Version;
            return source;
        }

        public UniTask2<T> Task
        {
            get
            {
                return new UniTask2<T>(this, core.Version);
            }
        }

        public void SetResult(T result)
        {
            core.SetResult(result);
        }

        public void SetCancellation(CancellationToken cancellationToken)
        {
            core.SetCancellation(cancellationToken);
        }

        public void SetException(Exception exception)
        {
            core.SetException(exception);
        }

        public T GetResult(short token)
        {
            try
            {
                TaskTracker2.RemoveTracking(this);
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

        public AwaiterStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public AwaiterStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        void IPromisePoolItem.Reset()
        {
            core.Reset();
        }

        ~AutoResetUniTaskCompletionSource()
        {
            if (pool.TryReturn(this))
            {
                GC.ReRegisterForFinalize(this);
                return;
            }
        }
    }

    internal static class UniTaskCompletionSourceCoreShared // separated out of generic to avoid unnecessary duplication
    {
        internal static readonly Action<object> s_sentinel = CompletionSentinel;

        private static void CompletionSentinel(object _) // named method to aid debugging
        {
            throw new InvalidOperationException("The sentinel delegate should never be invoked.");
        }
    }
}

#endif