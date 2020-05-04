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
        bool TrySetCanceled(CancellationToken cancellationToken = default);
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
        object error; // ExceptionDispatchInfo or OperationCanceledException
        short version;
        bool hasUnhandledError;
        int completedCount; // 0: completed == false
        Action<object> continuation;
        object continuationState;

        public void Reset()
        {
            ReportUnhandledError();

            unchecked
            {
                version += 1; // incr version.
            }
            completedCount = 0;
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
        public bool TrySetResult(TResult result)
        {
            if (Interlocked.Increment(ref completedCount) == 1)
            {
                // setup result
                this.result = result;

                if (continuation != null || Interlocked.CompareExchange(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
                {
                    continuation(continuationState);
                    return true;
                }
            }

            return false;
        }

        /// <summary>Completes with an error.</summary>
        /// <param name="error">The exception.</param>
        public bool TrySetException(Exception error)
        {
            if (Interlocked.Increment(ref completedCount) == 1)
            {
                // setup result
                this.hasUnhandledError = true;
                this.error = ExceptionDispatchInfo.Capture(error);

                if (continuation != null || Interlocked.CompareExchange(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
                {
                    continuation(continuationState);
                    return true;
                }
            }

            return false;
        }

        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            if (Interlocked.Increment(ref completedCount) == 1)
            {
                // setup result
                this.hasUnhandledError = true;
                this.error = new OperationCanceledException(cancellationToken);

                if (continuation != null || Interlocked.CompareExchange(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
                {
                    continuation(continuationState);
                    return true;
                }
            }

            return false;
        }

        /// <summary>Gets the operation version.</summary>
        public short Version => version;

        /// <summary>Gets the status of the operation.</summary>
        /// <param name="token">Opaque value that was provided to the <see cref="UniTask"/>'s constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTaskStatus GetStatus(short token)
        {
            ValidateToken(token);
            return (continuation == null || (completedCount == 0)) ? UniTaskStatus.Pending
                 : (error == null) ? UniTaskStatus.Succeeded
                 : (error is OperationCanceledException) ? UniTaskStatus.Canceled
                 : UniTaskStatus.Faulted;
        }

        /// <summary>Gets the status of the operation without token validation.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTaskStatus UnsafeGetStatus()
        {
            return (continuation == null || (completedCount == 0)) ? UniTaskStatus.Pending
                 : (error == null) ? UniTaskStatus.Succeeded
                 : (error is OperationCanceledException) ? UniTaskStatus.Canceled
                 : UniTaskStatus.Faulted;
        }

        /// <summary>Gets the result of the operation.</summary>
        /// <param name="token">Opaque value that was provided to the <see cref="UniTask"/>'s constructor.</param>
        // [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult GetResult(short token)
        {
            ValidateToken(token);
            if (!(completedCount == 0))
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

            /*
                PatternA: GetStatus=Pending => OnCompleted => TrySet*** => GetResult
                PatternB: TrySet*** => GetStatus=!Pending => GetResult
                PatternC: GetStatus=Pending => TrySet/OnCompleted(race condition) => GetResult
                C.1: win OnCompleted -> TrySet invoke saved continuation
                C.2: win TrySet -> should invoke continuation here.
            */

            // not set continuation yet.
            object oldContinuation = this.continuation;
            if (oldContinuation == null)
            {
                continuationState = state;
                oldContinuation = Interlocked.CompareExchange(ref this.continuation, continuation, null);
            }

            if (oldContinuation != null)
            {
                // already running continuation in TrySet.
                // It will cause call OnCompleted multiple time, invalid.
                if (!ReferenceEquals(oldContinuation, UniTaskCompletionSourceCoreShared.s_sentinel))
                {
                    throw new InvalidOperationException();
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
            TaskTracker.TrackActiveTask(this, 2);
            core.Reset();
        }

        public bool TrySetResult()
        {
            return core.TrySetResult(AsyncUnit.Default);
        }

        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        public bool TrySetException(Exception exception)
        {
            return core.TrySetException(exception);
        }

        public void GetResult(short token)
        {
            MarkHandled();
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
            TaskTracker.TrackActiveTask(value, 2);
            return value;
        }

        public static AutoResetUniTaskCompletionSource CreateFromCanceled(CancellationToken cancellationToken, out short token)
        {
            var source = Create();
            source.TrySetCanceled(cancellationToken);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource CreateFromException(Exception exception, out short token)
        {
            var source = Create();
            source.TrySetException(exception);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource CreateCompleted(out short token)
        {
            var source = Create();
            source.TrySetResult();
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

        public bool TrySetResult()
        {
            return core.TrySetResult(AsyncUnit.Default);
        }

        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        public bool TrySetException(Exception exception)
        {
            return core.TrySetException(exception);
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
            TaskTracker.TrackActiveTask(this, 2);
        }

        public bool TrySetResult(T result)
        {
            return core.TrySetResult(result);
        }

        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        public bool TrySetException(Exception exception)
        {
            return core.TrySetException(exception);
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
            TaskTracker.TrackActiveTask(result, 2);
            return result;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromCanceled(CancellationToken cancellationToken, out short token)
        {
            var source = Create();
            source.TrySetCanceled(cancellationToken);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromException(Exception exception, out short token)
        {
            var source = Create();
            source.TrySetException(exception);
            token = source.core.Version;
            return source;
        }

        public static AutoResetUniTaskCompletionSource<T> CreateFromResult(T result, out short token)
        {
            var source = Create();
            source.TrySetResult(result);
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

        public bool TrySetResult(T result)
        {
            return core.TrySetResult(result);
        }

        public bool TrySetCanceled(CancellationToken cancellationToken = default)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        public bool TrySetException(Exception exception)
        {
            return core.TrySetException(exception);
        }

        public T GetResult(short token)
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

        void IPromisePoolItem.Reset()
        {
            core.Reset();
        }

        ~AutoResetUniTaskCompletionSource()
        {
            if (pool.TryReturn(this))
            {
                GC.ReRegisterForFinalize(this);
            }
        }
    }
}

#endif