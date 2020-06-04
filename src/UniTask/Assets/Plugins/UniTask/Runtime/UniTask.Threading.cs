#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
#if UNITY_2018_3_OR_NEWER

        /// <summary>
        /// If running on mainthread, do nothing. Otherwise, same as UniTask.Yield(PlayerLoopTiming.Update).
        /// </summary>
        public static SwitchToMainThreadAwaitable SwitchToMainThread()
        {
            return new SwitchToMainThreadAwaitable(PlayerLoopTiming.Update);
        }

        /// <summary>
        /// If running on mainthread, do nothing. Otherwise, same as UniTask.Yield(timing).
        /// </summary>
        public static SwitchToMainThreadAwaitable SwitchToMainThread(PlayerLoopTiming timing)
        {
            return new SwitchToMainThreadAwaitable(timing);
        }

        /// <summary>
        /// Return to mainthread(same as await SwitchToMainThread) after using scope is closed.
        /// </summary>
        public static ReturnToMainThread ReturnToMainThread()
        {
            return new ReturnToMainThread(PlayerLoopTiming.Update);
        }

        /// <summary>
        /// Return to mainthread(same as await SwitchToMainThread) after using scope is closed.
        /// </summary>
        public static ReturnToMainThread ReturnToMainThread(PlayerLoopTiming timing)
        {
            return new ReturnToMainThread(timing);
        }

#endif

        public static SwitchToThreadPoolAwaitable SwitchToThreadPool()
        {
            return new SwitchToThreadPoolAwaitable();
        }

        /// <summary>
        /// Note: use SwitchToThreadPool is recommended.
        /// </summary>
        public static SwitchToTaskPoolAwaitable SwitchToTaskPool()
        {
            return new SwitchToTaskPoolAwaitable();
        }

        public static SwitchToSynchronizationContextAwaitable SwitchToSynchronizationContext(SynchronizationContext synchronizationContext)
        {
            Error.ThrowArgumentNullException(synchronizationContext, nameof(synchronizationContext));
            return new SwitchToSynchronizationContextAwaitable(synchronizationContext);
        }

        public static ReturnToSynchronizationContext ReturnToSynchronizationContext(SynchronizationContext synchronizationContext)
        {
            return new ReturnToSynchronizationContext(synchronizationContext, false);
        }

        public static ReturnToSynchronizationContext ReturnToCurrentSynchronizationContext(bool dontPostWhenSameContext = true)
        {
            return new ReturnToSynchronizationContext(SynchronizationContext.Current, dontPostWhenSameContext);
        }
    }

#if UNITY_2018_3_OR_NEWER

    public struct SwitchToMainThreadAwaitable
    {
        readonly PlayerLoopTiming playerLoopTiming;

        public SwitchToMainThreadAwaitable(PlayerLoopTiming playerLoopTiming)
        {
            this.playerLoopTiming = playerLoopTiming;
        }

        public Awaiter GetAwaiter() => new Awaiter(playerLoopTiming);

        public struct Awaiter : ICriticalNotifyCompletion
        {
            readonly PlayerLoopTiming playerLoopTiming;

            public Awaiter(PlayerLoopTiming playerLoopTiming)
            {
                this.playerLoopTiming = playerLoopTiming;
            }

            public bool IsCompleted
            {
                get
                {
                    var currentThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    if (PlayerLoopHelper.MainThreadId == currentThreadId)
                    {
                        return true; // run immediate.
                    }
                    else
                    {
                        return false; // register continuation.
                    }
                }
            }

            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(playerLoopTiming, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(playerLoopTiming, continuation);
            }
        }
    }

    public struct ReturnToMainThread
    {
        readonly PlayerLoopTiming playerLoopTiming;

        public ReturnToMainThread(PlayerLoopTiming playerLoopTiming)
        {
            this.playerLoopTiming = playerLoopTiming;
        }

        public Awaiter DisposeAsync()
        {
            return new Awaiter(playerLoopTiming); // run immediate.
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            readonly PlayerLoopTiming timing;

            public Awaiter(PlayerLoopTiming timing)
            {
                this.timing = timing;
            }

            public Awaiter GetAwaiter() => this;

            public bool IsCompleted => PlayerLoopHelper.MainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId;

            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }
        }
    }

#endif

    public struct SwitchToThreadPoolAwaitable
    {
        public Awaiter GetAwaiter() => new Awaiter();

        public struct Awaiter : ICriticalNotifyCompletion
        {
            static readonly WaitCallback switchToCallback = Callback;

            public bool IsCompleted => false;
            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                ThreadPool.QueueUserWorkItem(switchToCallback, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
#if NETCOREAPP3_1
                ThreadPool.UnsafeQueueUserWorkItem(ThreadPoolWorkItem.Create(continuation), false);
#else
                ThreadPool.UnsafeQueueUserWorkItem(switchToCallback, continuation);
#endif
            }

            static void Callback(object state)
            {
                var continuation = (Action)state;
                continuation();
            }
        }

#if NETCOREAPP3_1

        sealed class ThreadPoolWorkItem : IThreadPoolWorkItem, ITaskPoolNode<ThreadPoolWorkItem>
        {
            static TaskPool<ThreadPoolWorkItem> pool;
            public ThreadPoolWorkItem NextNode { get; set; }

            static ThreadPoolWorkItem()
            {
                TaskPool.RegisterSizeGetter(typeof(ThreadPoolWorkItem), () => pool.Size);
            }

            Action continuation;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ThreadPoolWorkItem Create(Action continuation)
            {
                if (!pool.TryPop(out var item))
                {
                    item = new ThreadPoolWorkItem();
                }

                item.continuation = continuation;
                return item;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Execute()
            {
                var call = continuation;
                continuation = null;
                if (call != null)
                {
                    pool.TryPush(this);
                    call.Invoke();
                }
            }
        }

#endif
    }

    public struct SwitchToTaskPoolAwaitable
    {
        public Awaiter GetAwaiter() => new Awaiter();

        public struct Awaiter : ICriticalNotifyCompletion
        {
            static readonly Action<object> switchToCallback = Callback;

            public bool IsCompleted => false;
            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                Task.Factory.StartNew(switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Task.Factory.StartNew(switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
            }

            static void Callback(object state)
            {
                var continuation = (Action)state;
                continuation();
            }
        }
    }

    public struct SwitchToSynchronizationContextAwaitable
    {
        readonly SynchronizationContext synchronizationContext;

        public SwitchToSynchronizationContextAwaitable(SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
        }

        public Awaiter GetAwaiter() => new Awaiter(synchronizationContext);

        public struct Awaiter : ICriticalNotifyCompletion
        {
            static readonly SendOrPostCallback switchToCallback = Callback;
            readonly SynchronizationContext synchronizationContext;

            public Awaiter(SynchronizationContext synchronizationContext)
            {
                this.synchronizationContext = synchronizationContext;
            }

            public bool IsCompleted => false;
            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                synchronizationContext.Post(switchToCallback, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                synchronizationContext.Post(switchToCallback, continuation);
            }

            static void Callback(object state)
            {
                var continuation = (Action)state;
                continuation();
            }
        }
    }

    public struct ReturnToSynchronizationContext
    {
        readonly SynchronizationContext syncContext;
        readonly bool dontPostWhenSameContext;

        public ReturnToSynchronizationContext(SynchronizationContext syncContext, bool dontPostWhenSameContext)
        {
            this.syncContext = syncContext;
            this.dontPostWhenSameContext = dontPostWhenSameContext;
        }

        public Awaiter DisposeAsync()
        {
            return new Awaiter(syncContext, dontPostWhenSameContext);
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            static readonly SendOrPostCallback switchToCallback = Callback;

            readonly SynchronizationContext synchronizationContext;
            readonly bool dontPostWhenSameContext;

            public Awaiter(SynchronizationContext synchronizationContext, bool dontPostWhenSameContext)
            {
                this.synchronizationContext = synchronizationContext;
                this.dontPostWhenSameContext = dontPostWhenSameContext;
            }

            public Awaiter GetAwaiter() => this;

            public bool IsCompleted
            {
                get
                {
                    if (!dontPostWhenSameContext) return false;

                    var current = SynchronizationContext.Current;
                    if (current == synchronizationContext)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                synchronizationContext.Post(switchToCallback, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                synchronizationContext.Post(switchToCallback, continuation);
            }

            static void Callback(object state)
            {
                var continuation = (Action)state;
                continuation();
            }
        }
    }
}
