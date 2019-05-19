#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using UniRx.Async.Internal;

namespace UniRx.Async
{
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
}

#endif