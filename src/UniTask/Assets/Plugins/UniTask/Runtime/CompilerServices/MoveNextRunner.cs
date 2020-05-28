#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using Cysharp.Threading.Tasks.Internal;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
    public interface IMoveNextRunner
    {
        Action MoveNext { get; }
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

    internal sealed class MoveNextRunner<TStateMachine> : IMoveNextRunner, ITaskPoolNode<MoveNextRunner<TStateMachine>>
        where TStateMachine : IAsyncStateMachine
    {
        static TaskPool<MoveNextRunner<TStateMachine>> pool;

        TStateMachine stateMachine;

        public Action MoveNext { get; }

        public MoveNextRunner()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(ref AsyncUniTaskVoidMethodBuilder builder, ref TStateMachine stateMachine)
        {
            if (!pool.TryPop(out var result))
            {
                result = new MoveNextRunner<TStateMachine>();
            }

            builder.runner = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        static MoveNextRunner()
        {
            TaskPoolMonitor.RegisterSizeGettter(typeof(MoveNextRunner<TStateMachine>), () => pool.Size);
        }

        public MoveNextRunner<TStateMachine> NextNode { get; set; }

        public void Return()
        {
            stateMachine = default;
            pool.TryPush(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            stateMachine.MoveNext();
        }
    }

    internal class MoveNextRunnerPromise<TStateMachine> : IMoveNextRunnerPromise, IUniTaskSource, ITaskPoolNode<MoveNextRunnerPromise<TStateMachine>>
        where TStateMachine : IAsyncStateMachine
    {
        static TaskPool<MoveNextRunnerPromise<TStateMachine>> pool;

        TStateMachine stateMachine;

        public Action MoveNext { get; }

        UniTaskCompletionSourceCore<AsyncUnit> core;

        MoveNextRunnerPromise()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(ref AsyncUniTaskMethodBuilder builder, ref TStateMachine stateMachine)
        {
            if (!pool.TryPop(out var result))
            {
                result = new MoveNextRunnerPromise<TStateMachine>();
            }
            TaskTracker.TrackActiveTask(result, 3);

            builder.runnerPromise = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        public MoveNextRunnerPromise<TStateMachine> NextNode { get; set; }

        static MoveNextRunnerPromise()
        {
            TaskPoolMonitor.RegisterSizeGettter(typeof(MoveNextRunnerPromise<TStateMachine>), () => pool.Size);
        }

        bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            return pool.TryPush(this);
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
                core.GetResult(token);
            }
            finally
            {
                TryReturn();
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

        ~MoveNextRunnerPromise()
        {
            if (TryReturn())
            {
                GC.ReRegisterForFinalize(this);
            }
        }
    }

    internal class MoveNextRunnerPromise<TStateMachine, T> : IMoveNextRunnerPromise<T>, IUniTaskSource<T>, ITaskPoolNode<MoveNextRunnerPromise<TStateMachine, T>>
        where TStateMachine : IAsyncStateMachine
    {
        static TaskPool<MoveNextRunnerPromise<TStateMachine, T>> pool;

        TStateMachine stateMachine;

        public Action MoveNext { get; }

        UniTaskCompletionSourceCore<T> core;

        MoveNextRunnerPromise()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(ref AsyncUniTaskMethodBuilder<T> builder, ref TStateMachine stateMachine)
        {
            if (!pool.TryPop(out var result))
            {
                result = new MoveNextRunnerPromise<TStateMachine, T>();
            }
            TaskTracker.TrackActiveTask(result, 3);

            builder.runnerPromise = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        public MoveNextRunnerPromise<TStateMachine, T> NextNode { get; set; }

        static MoveNextRunnerPromise()
        {
            TaskPoolMonitor.RegisterSizeGettter(typeof(MoveNextRunnerPromise<TStateMachine, T>), () => pool.Size);
        }

        bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            return pool.TryPush(this);
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
                return core.GetResult(token);
            }
            finally
            {
                TryReturn();
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

        ~MoveNextRunnerPromise()
        {
            if (TryReturn())
            {
                GC.ReRegisterForFinalize(this);
            }
        }
    }
}

