#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using UniRx.Async.Internal;

namespace UniRx.Async
{
    public interface IResolvePromise
    {
        void SetResult();
    }

    public interface IResolvePromise<T>
    {
        void SetResult(T value);
    }

    public interface IRejectPromise
    {
        void SetException(Exception exception);
    }

    public interface ICancelPromise
    {
        void SetCanceled(CancellationToken cancellationToken = default);
    }

    public interface IPromise<T> : IResolvePromise<T>, IRejectPromise, ICancelPromise
    {
    }

    public interface IPromise : IResolvePromise, IRejectPromise, ICancelPromise
    {
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

        public void SetCanceled(CancellationToken cancellationToken = default)
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

    internal static class UniTaskCompletionSourceCoreShared // separated out of generic to avoid unnecessary duplication
    {
        internal static readonly Action<object> s_sentinel = CompletionSentinel;

        private static void CompletionSentinel(object _) // named method to aid debugging
        {
            throw new InvalidOperationException("The sentinel delegate should never be invoked.");
        }
    }

    public class UniTaskCompletionSource : IUniTaskSource, IPromise
    {
        UniTaskCompletionSourceCore<AsyncUnit> core;
        bool handled = false;

        public UniTaskCompletionSource()
        {
            TaskTracker2.TrackActiveTask(this, 2);
        }

        [Conditional("UNITY_EDITOR")]
        internal void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker2.RemoveTracking(this);
            }
        }

        public UniTask Task
        {
            get
            {
                return new UniTask(this, core.Version);
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

        public void SetCanceled(CancellationToken cancellationToken = default)
        {
            core.SetCanceled(cancellationToken);
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

        ~UniTaskCompletionSource()
        {
            // clear error information.
            core.Reset();
        }
    }

    public class AutoResetUniTaskCompletionSource : IUniTaskSource, IPromisePoolItem, IPromise
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
            source.SetCanceled(cancellationToken);
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

        public UniTask Task
        {
            get
            {
                return new UniTask(this, core.Version);
            }
        }

        public void SetResult()
        {
            core.SetResult(AsyncUnit.Default);
        }

        public void SetCanceled(CancellationToken cancellationToken = default)
        {
            core.SetCanceled(cancellationToken);
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

    public class UniTaskCompletionSource<T> : IUniTaskSource<T>, IPromise<T>
    {
        UniTaskCompletionSourceCore<T> core;
        bool handled = false;

        public UniTaskCompletionSource()
        {
            TaskTracker2.TrackActiveTask(this, 2);
        }

        [Conditional("UNITY_EDITOR")]
        internal void MarkHandled()
        {
            if (!handled)
            {
                handled = true;
                TaskTracker2.RemoveTracking(this);
            }
        }

        public UniTask<T> Task
        {
            get
            {
                return new UniTask<T>(this, core.Version);
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

        public void SetCanceled(CancellationToken cancellationToken = default)
        {
            core.SetCanceled(cancellationToken);
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

        ~UniTaskCompletionSource()
        {
            // clear error information.
            core.Reset();
        }
    }

    public class AutoResetUniTaskCompletionSource<T> : IUniTaskSource<T>, IPromisePoolItem, IPromise<T>
    {
        static readonly PromisePool<AutoResetUniTaskCompletionSource<T>> pool = new PromisePool<AutoResetUniTaskCompletionSource<T>>();

        UniTaskCompletionSourceCore<T> core;

        AutoResetUniTaskCompletionSource()
        {
        }

        public static AutoResetUniTaskCompletionSource<T> Create()
        {
            var result = pool.TryRent() ?? new AutoResetUniTaskCompletionSource<T>();
            TaskTracker2.TrackActiveTask(result, 2);
            return result;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromCanceled(CancellationToken cancellationToken, out short token)
        {
            var source = Create();
            source.SetCanceled(cancellationToken);
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

        public UniTask<T> Task
        {
            get
            {
                return new UniTask<T>(this, core.Version);
            }
        }

        public void SetResult(T result)
        {
            core.SetResult(result);
        }

        public void SetCanceled(CancellationToken cancellationToken = default)
        {
            core.SetCanceled(cancellationToken);
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
}

#endif