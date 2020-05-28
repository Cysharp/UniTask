
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.CompilerServices
{
    internal interface IMoveNextRunner
    {
        Action CallMoveNext { get; }
        void Return();
    }

    internal interface IMoveNextRunnerPromise : IUniTaskSource
    {
        Action MoveNext { get; }
        UniTask Task { get; }
        void SetResult();
        void SetException(Exception exception);
    }

    internal interface IMoveNextRunnerPromise<T> : IUniTaskSource<T>
    {
        Action MoveNext { get; }
        UniTask<T> Task { get; }
        void SetResult(T result);
        void SetException(Exception exception);
    }

    internal sealed class MoveNextRunner<TStateMachine> : IMoveNextRunner, IPromisePoolItem
        where TStateMachine : IAsyncStateMachine
    {
        static PromisePool<MoveNextRunner<TStateMachine>> pool = new PromisePool<MoveNextRunner<TStateMachine>>();

        TStateMachine stateMachine;
        readonly Action callMoveNext;

        public Action CallMoveNext => callMoveNext;

        MoveNextRunner()
        {
            callMoveNext = Run;
        }

        public static void SetStateMachine(ref AsyncUniTaskVoidMethodBuilder builder, ref TStateMachine stateMachine)
        {
            var result = pool.TryRent();
            if (result == null)
            {
                result = new MoveNextRunner<TStateMachine>();
            }

            builder.runner = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            stateMachine.MoveNext();
        }

        public void Return()
        {
            pool.TryReturn(this);
        }

        void IPromisePoolItem.Reset()
        {
            stateMachine = default;
        }
    }

    internal class MoveNextRunnerPromise<TStateMachine> : IMoveNextRunnerPromise, IUniTaskSource, IPromisePoolItem
        where TStateMachine : IAsyncStateMachine
    {
        static readonly PromisePool<MoveNextRunnerPromise<TStateMachine>> pool = new PromisePool<MoveNextRunnerPromise<TStateMachine>>();

        TStateMachine stateMachine;

        public Action MoveNext { get; }

        UniTaskCompletionSourceCore<AsyncUnit> core;

        MoveNextRunnerPromise()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(ref AsyncUniTaskMethodBuilder builder, ref TStateMachine stateMachine)
        {
            var result = pool.TryRent();
            if (result == null)
            {
                result = new MoveNextRunnerPromise<TStateMachine>();
            }

            TaskTracker.TrackActiveTask(result, 2);

            builder.runnerPromise = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            stateMachine.MoveNext();
        }

        public UniTask Task
        {
            [DebuggerHidden]
            get
            {
                return new UniTask(this, core.Version);
            }
        }

        [DebuggerHidden]
        public void SetResult()
        {
            core.TrySetResult(AsyncUnit.Default);
        }

        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            core.TrySetException(exception);
        }

        [DebuggerHidden]
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

        [DebuggerHidden]
        public UniTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        [DebuggerHidden]
        public UniTaskStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        [DebuggerHidden]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        [DebuggerHidden]
        void IPromisePoolItem.Reset()
        {
            stateMachine = default;
            core.Reset();
        }

        ~MoveNextRunnerPromise()
        {
            if (pool.TryReturn(this))
            {
                GC.ReRegisterForFinalize(this);
            }
        }
    }

    internal class MoveNextRunnerPromise<TStateMachine, T> : IMoveNextRunnerPromise<T>, IUniTaskSource<T>, IPromisePoolItem
        where TStateMachine : IAsyncStateMachine
    {
        static readonly PromisePool<MoveNextRunnerPromise<TStateMachine, T>> pool = new PromisePool<MoveNextRunnerPromise<TStateMachine, T>>();

        TStateMachine stateMachine;

        public Action MoveNext { get; }

        UniTaskCompletionSourceCore<T> core;

        MoveNextRunnerPromise()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(ref AsyncUniTaskMethodBuilder<T> builder, ref TStateMachine stateMachine)
        {
            var result = pool.TryRent();
            if (result == null)
            {
                result = new MoveNextRunnerPromise<TStateMachine, T>();
            }

            TaskTracker.TrackActiveTask(result, 2);

            builder.runnerPromise = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            stateMachine.MoveNext();
        }

        public UniTask<T> Task
        {
            [DebuggerHidden]
            get
            {
                return new UniTask<T>(this, core.Version);
            }
        }

        [DebuggerHidden]
        public void SetResult(T result)
        {
            core.TrySetResult(result);
        }

        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            core.TrySetException(exception);
        }

        [DebuggerHidden]
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

        [DebuggerHidden]
        void IUniTaskSource.GetResult(short token)
        {
            GetResult(token);
        }

        [DebuggerHidden]
        public UniTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        [DebuggerHidden]
        public UniTaskStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        [DebuggerHidden]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        [DebuggerHidden]
        void IPromisePoolItem.Reset()
        {
            stateMachine = default;
            core.Reset();
        }

        ~MoveNextRunnerPromise()
        {
            if (pool.TryReturn(this))
            {
                GC.ReRegisterForFinalize(this);
            }
        }
    }
}

