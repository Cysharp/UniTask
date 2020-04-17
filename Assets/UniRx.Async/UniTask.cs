#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0436

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using UniRx.Async.CompilerServices;
using UniRx.Async.Internal;

namespace UniRx.Async
{


    public partial struct UniTask2
    {
        public static UniTask2 DelayFrame(int frameCount, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
        {
            return new UniTask2(DelayPromiseCore2.Create(frameCount, timing, cancellationToken, out var token), token);


            //return new ValueTask<int>(DelayPromiseCore2.Create(frameCount, timing, cancellationToken, out var token), token);
        }

    }


    public class DelayPromiseCore2 : IUniTaskSource, IPlayerLoopItem, IPromisePoolItem
    {
        static readonly PromisePool<DelayPromiseCore2> pool = new PromisePool<DelayPromiseCore2>();

        int delayFrameCount;
        CancellationToken cancellationToken;

        int currentFrameCount;
        UniTaskCompletionSourceCore<object> core;

        DelayPromiseCore2()
        {
        }

        public static IUniTaskSource Create(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
            }

            var result = pool.TryRent() ?? new DelayPromiseCore2();

            result.delayFrameCount = delayFrameCount;
            result.cancellationToken = cancellationToken;

            TaskTracker2.TrackActiveTask(result, 3);

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

        public bool MoveNext()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                TaskTracker2.RemoveTracking(this);
                core.SetCancellation(cancellationToken);
                return false;
            }

            if (currentFrameCount == delayFrameCount)
            {
                TaskTracker2.RemoveTracking(this);
                core.SetResult(null);
                return false;
            }

            currentFrameCount++;
            return true;
        }

        public void Reset()
        {
            core.Reset();
            currentFrameCount = default;
            delayFrameCount = default;
            cancellationToken = default;
        }
    }








    /// <summary>
    /// Lightweight unity specified task-like object.
    /// </summary>
    [AsyncMethodBuilder(typeof(AsyncUniTask2MethodBuilder))] // TODO:AsyncUniTask2
    public partial struct UniTask2
    {
        // static readonly UniTask<AsyncUnit> DefaultAsyncUnitTask = new UniTask<AsyncUnit>(AsyncUnit.Default);

        readonly IUniTaskSource awaiter;
        readonly short token;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask2(IUniTaskSource awaiter, short token)
        {
            this.awaiter = awaiter;
            this.token = token;
        }

        public AwaiterStatus Status
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return awaiter.GetStatus(token);
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetResult()
        {
            awaiter.GetResult(token);
        }

        // TODO:can be suppress?

        ///// <summary>
        ///// returns (bool IsCanceled) instead of throws OperationCanceledException.
        ///// </summary>
        //public UniTask<bool> SuppressCancellationThrow()
        //{
        //    var status = Status;
        //    if (status == AwaiterStatus.Succeeded) return CompletedTasks.False;
        //    if (status == AwaiterStatus.Canceled) return CompletedTasks.True;
        //    //return new UniTask<bool>(new IsCanceledAwaiter(awaiter));
        //}

        public override string ToString()
        {
            var status = this.awaiter.UnsafeGetStatus();
            return (status == AwaiterStatus.Succeeded) ? "()" : "(" + status + ")";
        }

        //public static implicit operator UniTask<AsyncUnit>(UniTask2 task)
        //{
        //    // TODO:
        //    throw new NotImplementedException();

        //    //if (task.awaiter != null)
        //    //{
        //    //    if (task.awaiter.IsCompleted)
        //    //    {
        //    //        return DefaultAsyncUnitTask;
        //    //    }
        //    //    else
        //    //    {
        //    //        // UniTask<T> -> UniTask is free but UniTask -> UniTask<T> requires wrapping cost.
        //    //        return new UniTask<AsyncUnit>(new AsyncUnitAwaiter(task.awaiter));
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return DefaultAsyncUnitTask;
        //    //}
        //}

        //class AsyncUnitAwaiter : IAwaiter<AsyncUnit>
        //{
        //    readonly IAwaiter2 awaiter;

        //    public AsyncUnitAwaiter(IAwaiter2 awaiter)
        //    {
        //        this.awaiter = awaiter;
        //    }

        //    public bool IsCompleted => awaiter.IsCompleted;

        //    public AwaiterStatus Status => awaiter.Status;

        //    public AsyncUnit GetResult()
        //    {
        //        awaiter.GetResult();
        //        return AsyncUnit.Default;
        //    }

        //    public void OnCompleted(Action continuation)
        //    {
        //        awaiter.OnCompleted(continuation);
        //    }

        //    public void UnsafeOnCompleted(Action continuation)
        //    {
        //        awaiter.UnsafeOnCompleted(continuation);
        //    }

        //    void IAwaiter.GetResult()
        //    {
        //        awaiter.GetResult();
        //    }
        //}

        class IsCanceledAwaiter : IUniTaskSource
        {
            readonly IUniTaskSource awaiter;

            public IsCanceledAwaiter(IUniTaskSource awaiter)
            {
                this.awaiter = awaiter;
            }

            //public bool IsCompleted => awaiter.IsCompleted;

            //public AwaiterStatus Status => awaiter.Status;

            //public bool GetResult()
            //{
            //    if (awaiter.Status == AwaiterStatus.Canceled)
            //    {
            //        return true;
            //    }
            //    awaiter.GetResult();
            //    return false;
            //}

            //public void OnCompleted(Action continuation)
            //{
            //    awaiter.OnCompleted(continuation);
            //}

            //public void UnsafeOnCompleted(Action continuation)
            //{
            //    awaiter.UnsafeOnCompleted(continuation);
            //}

            //void IAwaiter.GetResult()
            //{
            //    awaiter.GetResult();
            //}

            public void GetResult(short token)
            {
                // TODO: bool
                if (awaiter.GetStatus(token) == AwaiterStatus.Canceled)
                {
                    //return true;
                }

                awaiter.GetResult(token);
                // return false
                throw new NotImplementedException();
            }

            public AwaiterStatus GetStatus(short token)
            {
                return awaiter.GetStatus(token);
            }

            public AwaiterStatus UnsafeGetStatus()
            {
                return awaiter.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                awaiter.OnCompleted(continuation, state, token);
            }
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            readonly UniTask2 task;

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Awaiter(in UniTask2 task)
            {
                this.task = task;
            }

            public bool IsCompleted
            {
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return task.Status.IsCompleted();
                }
            }

            public AwaiterStatus Status
            {
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return task.Status;
                }
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void GetResult()
            {
                task.GetResult();
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void OnCompleted(Action continuation)
            {
                task.awaiter.OnCompleted(AwaiterActions.InvokeActionDelegate, continuation, task.token);
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void UnsafeOnCompleted(Action continuation)
            {
                task.awaiter.OnCompleted(AwaiterActions.InvokeActionDelegate, continuation, task.token);
            }
        }
    }

    internal static class AwaiterActions
    {
        internal static readonly Action<object> InvokeActionDelegate = InvokeAction;

        static void InvokeAction(object state)
        {
            ((Action)state).Invoke();
        }
    }




    /// <summary>
    /// Lightweight unity specified task-like object.
    /// </summary>
    [AsyncMethodBuilder(typeof(AsyncUniTask2MethodBuilder))] // TODO:AsyncUniTask2~T
    public struct UniTask2<T>
    {
        // static readonly UniTask<AsyncUnit> DefaultAsyncUnitTask = new UniTask<AsyncUnit>(AsyncUnit.Default);

        readonly IUniTaskSource<T> awaiter;
        readonly short token;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask2(IUniTaskSource<T> awaiter, short token)
        {
            this.awaiter = awaiter;
            this.token = token;
        }

        public AwaiterStatus Status
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return awaiter.GetStatus(token);
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T GetResult()
        {
            return awaiter.GetResult(token);
        }

        // TODO:can be suppress?

        ///// <summary>
        ///// returns (bool IsCanceled) instead of throws OperationCanceledException.
        ///// </summary>
        //public UniTask<bool> SuppressCancellationThrow()
        //{
        //    var status = Status;
        //    if (status == AwaiterStatus.Succeeded) return CompletedTasks.False;
        //    if (status == AwaiterStatus.Canceled) return CompletedTasks.True;
        //    //return new UniTask<bool>(new IsCanceledAwaiter(awaiter));
        //}

        public override string ToString()
        {
            var status = this.awaiter.UnsafeGetStatus();
            return (status == AwaiterStatus.Succeeded) ? "()" : "(" + status + ")";
        }

        //public static implicit operator UniTask<AsyncUnit>(UniTask2 task)
        //{
        //    // TODO:
        //    throw new NotImplementedException();

        //    //if (task.awaiter != null)
        //    //{
        //    //    if (task.awaiter.IsCompleted)
        //    //    {
        //    //        return DefaultAsyncUnitTask;
        //    //    }
        //    //    else
        //    //    {
        //    //        // UniTask<T> -> UniTask is free but UniTask -> UniTask<T> requires wrapping cost.
        //    //        return new UniTask<AsyncUnit>(new AsyncUnitAwaiter(task.awaiter));
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return DefaultAsyncUnitTask;
        //    //}
        //}

        //class AsyncUnitAwaiter : IAwaiter<AsyncUnit>
        //{
        //    readonly IAwaiter2 awaiter;

        //    public AsyncUnitAwaiter(IAwaiter2 awaiter)
        //    {
        //        this.awaiter = awaiter;
        //    }

        //    public bool IsCompleted => awaiter.IsCompleted;

        //    public AwaiterStatus Status => awaiter.Status;

        //    public AsyncUnit GetResult()
        //    {
        //        awaiter.GetResult();
        //        return AsyncUnit.Default;
        //    }

        //    public void OnCompleted(Action continuation)
        //    {
        //        awaiter.OnCompleted(continuation);
        //    }

        //    public void UnsafeOnCompleted(Action continuation)
        //    {
        //        awaiter.UnsafeOnCompleted(continuation);
        //    }

        //    void IAwaiter.GetResult()
        //    {
        //        awaiter.GetResult();
        //    }
        //}

        class IsCanceledAwaiter : IUniTaskSource
        {
            readonly IUniTaskSource awaiter;

            public IsCanceledAwaiter(IUniTaskSource awaiter)
            {
                this.awaiter = awaiter;
            }

            //public bool IsCompleted => awaiter.IsCompleted;

            //public AwaiterStatus Status => awaiter.Status;

            //public bool GetResult()
            //{
            //    if (awaiter.Status == AwaiterStatus.Canceled)
            //    {
            //        return true;
            //    }
            //    awaiter.GetResult();
            //    return false;
            //}

            //public void OnCompleted(Action continuation)
            //{
            //    awaiter.OnCompleted(continuation);
            //}

            //public void UnsafeOnCompleted(Action continuation)
            //{
            //    awaiter.UnsafeOnCompleted(continuation);
            //}

            //void IAwaiter.GetResult()
            //{
            //    awaiter.GetResult();
            //}

            public void GetResult(short token)
            {
                // TODO: bool
                if (awaiter.GetStatus(token) == AwaiterStatus.Canceled)
                {
                    //return true;
                }

                awaiter.GetResult(token);
                // return false
                throw new NotImplementedException();
            }

            public AwaiterStatus GetStatus(short token)
            {
                return awaiter.GetStatus(token);
            }

            public AwaiterStatus UnsafeGetStatus()
            {
                return awaiter.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                awaiter.OnCompleted(continuation, state, token);
            }
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            readonly UniTask2<T> task;

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Awaiter(in UniTask2<T> task)
            {
                this.task = task;
            }

            public bool IsCompleted
            {
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return task.Status.IsCompleted();
                }
            }

            public AwaiterStatus Status
            {
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return task.Status;
                }
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public T GetResult()
            {
                return task.GetResult();
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void OnCompleted(Action continuation)
            {
                task.awaiter.OnCompleted(AwaiterActions.InvokeActionDelegate, continuation, task.token);
            }

            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void UnsafeOnCompleted(Action continuation)
            {
                task.awaiter.OnCompleted(AwaiterActions.InvokeActionDelegate, continuation, task.token);
            }
        }
    }


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