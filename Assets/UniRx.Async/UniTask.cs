#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0436

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UniRx.Async.CompilerServices;
using UniRx.Async.Internal;

namespace UniRx.Async
{
    /// <summary>
    /// Lightweight unity specified task-like object.
    /// </summary>
    [AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder))]
    public partial struct UniTask : IEquatable<UniTask>
    {
        static readonly UniTask<AsyncUnit> DefaultAsyncUnitTask = new UniTask<AsyncUnit>(AsyncUnit.Default);

        readonly IAwaiter awaiter;

        [DebuggerHidden]
        public UniTask(IAwaiter awaiter)
        {
            this.awaiter = awaiter;
        }

        [DebuggerHidden]
        public UniTask(Func<UniTask> factory)
        {
            this.awaiter = new LazyPromise(factory);
        }

        [DebuggerHidden]
        public AwaiterStatus Status
        {
            get
            {
                return awaiter == null ? AwaiterStatus.Succeeded : awaiter.Status;
            }
        }

        [DebuggerHidden]
        public bool IsCompleted
        {
            get
            {
                return awaiter == null ? true : awaiter.IsCompleted;
            }
        }

        [DebuggerHidden]
        public void GetResult()
        {
            if (awaiter != null)
            {
                awaiter.GetResult();
            }
        }

        [DebuggerHidden]
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        /// <summary>
        /// returns (bool IsCanceled) instead of throws OperationCanceledException.
        /// </summary>
        public UniTask<bool> SuppressCancellationThrow()
        {
            var status = Status;
            if (status == AwaiterStatus.Succeeded) return CompletedTasks.False;
            if (status == AwaiterStatus.Canceled) return CompletedTasks.True;
            return new UniTask<bool>(new IsCanceledAwaiter(awaiter));
        }

        public bool Equals(UniTask other)
        {
            if (this.awaiter == null && other.awaiter == null)
            {
                return true;
            }
            else if (this.awaiter != null && other.awaiter != null)
            {
                return this.awaiter == other.awaiter;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            if (this.awaiter == null)
            {
                return 0;
            }
            else
            {
                return this.awaiter.GetHashCode();
            }
        }

        public override string ToString()
        {
            return (this.awaiter == null) ? "()"
                 : (this.awaiter.Status == AwaiterStatus.Succeeded) ? "()"
                 : "(" + this.awaiter.Status + ")";
        }

        public static implicit operator UniTask<AsyncUnit>(UniTask task)
        {
            if (task.awaiter != null)
            {
                if (task.awaiter.IsCompleted)
                {
                    return DefaultAsyncUnitTask;
                }
                else
                {
                    // UniTask<T> -> UniTask is free but UniTask -> UniTask<T> requires wrapping cost.
                    return new UniTask<AsyncUnit>(new AsyncUnitAwaiter(task.awaiter));
                }
            }
            else
            {
                return DefaultAsyncUnitTask;
            }
        }

        class AsyncUnitAwaiter : IAwaiter<AsyncUnit>
        {
            readonly IAwaiter awaiter;

            public AsyncUnitAwaiter(IAwaiter awaiter)
            {
                this.awaiter = awaiter;
            }

            public bool IsCompleted => awaiter.IsCompleted;

            public AwaiterStatus Status => awaiter.Status;

            public AsyncUnit GetResult()
            {
                awaiter.GetResult();
                return AsyncUnit.Default;
            }

            public void OnCompleted(Action continuation)
            {
                awaiter.OnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                awaiter.UnsafeOnCompleted(continuation);
            }

            void IAwaiter.GetResult()
            {
                awaiter.GetResult();
            }
        }

        class IsCanceledAwaiter : IAwaiter<bool>
        {
            readonly IAwaiter awaiter;

            public IsCanceledAwaiter(IAwaiter awaiter)
            {
                this.awaiter = awaiter;
            }

            public bool IsCompleted => awaiter.IsCompleted;

            public AwaiterStatus Status => awaiter.Status;

            public bool GetResult()
            {
                if (awaiter.Status == AwaiterStatus.Canceled)
                {
                    return true;
                }
                awaiter.GetResult();
                return false;
            }

            public void OnCompleted(Action continuation)
            {
                awaiter.OnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                awaiter.UnsafeOnCompleted(continuation);
            }

            void IAwaiter.GetResult()
            {
                awaiter.GetResult();
            }
        }

        public struct Awaiter : IAwaiter
        {
            readonly UniTask task;

            [DebuggerHidden]
            public Awaiter(UniTask task)
            {
                this.task = task;
            }

            [DebuggerHidden]
            public bool IsCompleted => task.IsCompleted;

            [DebuggerHidden]
            public AwaiterStatus Status => task.Status;

            [DebuggerHidden]
            public void GetResult() => task.GetResult();

            [DebuggerHidden]
            public void OnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.OnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }

            [DebuggerHidden]
            public void UnsafeOnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.UnsafeOnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }
        }
    }

    /// <summary>
    /// Lightweight unity specified task-like object.
    /// </summary>
    [AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder<>))]
    public struct UniTask<T> : IEquatable<UniTask<T>>
    {
        readonly T result;
        readonly IAwaiter<T> awaiter;

        [DebuggerHidden]
        public UniTask(T result)
        {
            this.result = result;
            this.awaiter = null;
        }

        [DebuggerHidden]
        public UniTask(IAwaiter<T> awaiter)
        {
            this.result = default(T);
            this.awaiter = awaiter;
        }

        [DebuggerHidden]
        public UniTask(Func<UniTask<T>> factory)
        {
            this.result = default(T);
            this.awaiter = new LazyPromise<T>(factory);
        }

        [DebuggerHidden]
        public AwaiterStatus Status
        {
            get
            {
                return awaiter == null ? AwaiterStatus.Succeeded : awaiter.Status;
            }
        }

        [DebuggerHidden]
        public bool IsCompleted
        {
            get
            {
                return awaiter == null ? true : awaiter.IsCompleted;
            }
        }

        [DebuggerHidden]
        public T Result
        {
            get
            {
                if (awaiter == null)
                {
                    return result;
                }
                else
                {
                    return awaiter.GetResult();
                }
            }
        }

        [DebuggerHidden]
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        /// <summary>
        /// returns (bool IsCanceled, T Result) instead of throws OperationCanceledException.
        /// </summary>
        public UniTask<(bool IsCanceled, T Result)> SuppressCancellationThrow()
        {
            var status = Status;
            if (status == AwaiterStatus.Succeeded)
            {
                return new UniTask<(bool, T)>((false, Result));
            }
            else if (status == AwaiterStatus.Canceled)
            {
                return new UniTask<(bool, T)>((true, default(T)));
            }
            return new UniTask<(bool, T)>(new IsCanceledAwaiter(awaiter));
        }

        public bool Equals(UniTask<T> other)
        {
            if (this.awaiter == null && other.awaiter == null)
            {
                return EqualityComparer<T>.Default.Equals(this.result, other.result);
            }
            else if (this.awaiter != null && other.awaiter != null)
            {
                return this.awaiter == other.awaiter;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            if (this.awaiter == null)
            {
                if (result == null) return 0;
                return result.GetHashCode();
            }
            else
            {
                return this.awaiter.GetHashCode();
            }
        }

        public override string ToString()
        {
            return (this.awaiter == null) ? result.ToString()
                 : (this.awaiter.Status == AwaiterStatus.Succeeded) ? this.awaiter.GetResult().ToString()
                 : "(" + this.awaiter.Status + ")";
        }

        public static implicit operator UniTask(UniTask<T> task)
        {
            if (task.awaiter != null)
            {
                return new UniTask(task.awaiter);
            }
            else
            {
                return new UniTask();
            }
        }

        class IsCanceledAwaiter : IAwaiter<(bool, T)>
        {
            readonly IAwaiter<T> awaiter;

            public IsCanceledAwaiter(IAwaiter<T> awaiter)
            {
                this.awaiter = awaiter;
            }

            public bool IsCompleted => awaiter.IsCompleted;

            public AwaiterStatus Status => awaiter.Status;

            public (bool, T) GetResult()
            {
                if (awaiter.Status == AwaiterStatus.Canceled)
                {
                    return (true, default(T));
                }
                return (false, awaiter.GetResult());
            }

            public void OnCompleted(Action continuation)
            {
                awaiter.OnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                awaiter.UnsafeOnCompleted(continuation);
            }

            void IAwaiter.GetResult()
            {
                awaiter.GetResult();
            }
        }

        public struct Awaiter : IAwaiter<T>
        {
            readonly UniTask<T> task;

            [DebuggerHidden]
            public Awaiter(UniTask<T> task)
            {
                this.task = task;
            }

            [DebuggerHidden]
            public bool IsCompleted => task.IsCompleted;

            [DebuggerHidden]
            public AwaiterStatus Status => task.Status;

            [DebuggerHidden]
            void IAwaiter.GetResult() => GetResult();

            [DebuggerHidden]
            public T GetResult() => task.Result;

            [DebuggerHidden]
            public void OnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.OnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }

            [DebuggerHidden]
            public void UnsafeOnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.UnsafeOnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }
        }
    }
}

#endif