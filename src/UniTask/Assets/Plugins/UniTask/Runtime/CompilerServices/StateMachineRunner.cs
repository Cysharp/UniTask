#pragma warning disable CS1591

using Cysharp.Threading.Tasks.Internal;
using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
    // #ENABLE_IL2CPP in this file is to avoid bug of IL2CPP VM.
    // Issue is tracked on https://issuetracker.unity3d.com/issues/il2cpp-incorrect-results-when-calling-a-method-from-outside-class-in-a-struct
    // but currently it is labeled `Won't Fix`.

    internal interface IStateMachineRunner
    {
        Action MoveNext { get; }
        void Return();

#if ENABLE_IL2CPP
        Action ReturnAction { get; }
#endif
    }

    internal interface IStateMachineRunnerPromise : IUniTaskSource
    {
        Action MoveNext { get; }
        UniTask Task { get; }
        void SetResult();
        void SetException(Exception exception);
    }

    internal interface IStateMachineRunnerPromise<T> : IUniTaskSource<T>
    {
        Action MoveNext { get; }
        UniTask<T> Task { get; }
        void SetResult(T result);
        void SetException(Exception exception);
    }

    internal static class StateMachineUtility
    {
        // Get AsyncStateMachine internal state to check IL2CPP bug
        public static int GetState(IAsyncStateMachine stateMachine)
        {
            var info = stateMachine.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .First(x => x.Name.EndsWith("__state"));
            return (int)info.GetValue(stateMachine);
        }
    }

    internal sealed class AsyncUniTaskVoid<TStateMachine> : IStateMachineRunner, ITaskPoolNode<AsyncUniTaskVoid<TStateMachine>>, IUniTaskSource
        where TStateMachine : IAsyncStateMachine
    {
        static TaskPool<AsyncUniTaskVoid<TStateMachine>> pool;

#if ENABLE_IL2CPP
        public Action ReturnAction { get; }
#endif

        TStateMachine stateMachine;

        public Action MoveNext { get; }

        public AsyncUniTaskVoid()
        {
            MoveNext = Run;
#if ENABLE_IL2CPP
            ReturnAction = Return;
#endif
        }

        public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunner runnerFieldRef)
        {
            if (!pool.TryPop(out var result))
            {
                result = new AsyncUniTaskVoid<TStateMachine>();
            }
            TaskTracker.TrackActiveTask(result, 3);

            runnerFieldRef = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        static AsyncUniTaskVoid()
        {
            TaskPool.RegisterSizeGetter(typeof(AsyncUniTaskVoid<TStateMachine>), () => pool.Size);
        }

        public AsyncUniTaskVoid<TStateMachine> NextNode { get; set; }

        public void Return()
        {
            TaskTracker.RemoveTracking(this);
            stateMachine = default;
            pool.TryPush(this);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            stateMachine.MoveNext();
        }

        // dummy interface implementation for TaskTracker.

        UniTaskStatus IUniTaskSource.GetStatus(short token)
        {
            return UniTaskStatus.Pending;
        }

        UniTaskStatus IUniTaskSource.UnsafeGetStatus()
        {
            return UniTaskStatus.Pending;
        }

        void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
        {
        }

        void IUniTaskSource.GetResult(short token)
        {
        }
    }

    internal sealed class AsyncUniTask<TStateMachine> : IStateMachineRunnerPromise, IUniTaskSource, ITaskPoolNode<AsyncUniTask<TStateMachine>>
        where TStateMachine : IAsyncStateMachine
    {
        static TaskPool<AsyncUniTask<TStateMachine>> pool;

#if ENABLE_IL2CPP
        readonly Action returnDelegate;  
#endif
        public Action MoveNext { get; }

        TStateMachine stateMachine;
        UniTaskCompletionSourceCore<AsyncUnit> core;

        AsyncUniTask()
        {
            MoveNext = Run;
#if ENABLE_IL2CPP
            returnDelegate = Return;
#endif
        }

        public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunnerPromise runnerPromiseFieldRef)
        {
            if (!pool.TryPop(out var result))
            {
                result = new AsyncUniTask<TStateMachine>();
            }
            TaskTracker.TrackActiveTask(result, 3);

            runnerPromiseFieldRef = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        public AsyncUniTask<TStateMachine> NextNode { get; set; }

        static AsyncUniTask()
        {
            TaskPool.RegisterSizeGetter(typeof(AsyncUniTask<TStateMachine>), () => pool.Size);
        }

        void Return()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            pool.TryPush(this);
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
#if ENABLE_IL2CPP
                // workaround for IL2CPP bug.
                PlayerLoopHelper.AddContinuation(PlayerLoopTiming.LastPostLateUpdate, returnDelegate);
#else
                TryReturn();
#endif
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
    }

    internal sealed class AsyncUniTask<TStateMachine, T> : IStateMachineRunnerPromise<T>, IUniTaskSource<T>, ITaskPoolNode<AsyncUniTask<TStateMachine, T>>
        where TStateMachine : IAsyncStateMachine
    {
        static TaskPool<AsyncUniTask<TStateMachine, T>> pool;

#if ENABLE_IL2CPP
        readonly Action returnDelegate;  
#endif

        public Action MoveNext { get; }

        TStateMachine stateMachine;
        UniTaskCompletionSourceCore<T> core;

        AsyncUniTask()
        {
            MoveNext = Run;
#if ENABLE_IL2CPP
            returnDelegate = Return;
#endif
        }

        public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunnerPromise<T> runnerPromiseFieldRef)
        {
            if (!pool.TryPop(out var result))
            {
                result = new AsyncUniTask<TStateMachine, T>();
            }
            TaskTracker.TrackActiveTask(result, 3);

            runnerPromiseFieldRef = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        public AsyncUniTask<TStateMachine, T> NextNode { get; set; }

        static AsyncUniTask()
        {
            TaskPool.RegisterSizeGetter(typeof(AsyncUniTask<TStateMachine, T>), () => pool.Size);
        }

        void Return()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            pool.TryPush(this);
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
            // UnityEngine.Debug.Log($"MoveNext State:" + StateMachineUtility.GetState(stateMachine));
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
#if ENABLE_IL2CPP
                // workaround for IL2CPP bug.
                PlayerLoopHelper.AddContinuation(PlayerLoopTiming.LastPostLateUpdate, returnDelegate);
#else
                TryReturn();
#endif
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
    }
}

