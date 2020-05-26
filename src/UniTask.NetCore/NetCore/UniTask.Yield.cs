using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        public static UniTask.YieldAwaitable Yield()
        {
            return default;
        }

        public readonly struct YieldAwaitable
        {
            public Awaiter GetAwaiter()
            {
                return default;
            }

            public readonly struct Awaiter : ICriticalNotifyCompletion
            {
                static readonly SendOrPostCallback SendOrPostCallbackDelegate = Continuation;
                static readonly WaitCallback WaitCallbackDelegate = Continuation;

                public bool IsCompleted => false;

                public void GetResult() { }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    var syncContext = SynchronizationContext.Current;
                    if (syncContext != null)
                    {
                        syncContext.Post(SendOrPostCallbackDelegate, continuation);
                    }
                    else
                    {
#if NETCOREAPP3_1
                        ThreadPool.UnsafeQueueUserWorkItem(ThreadPoolWorkItem.Create(continuation), false);
#else
                        ThreadPool.UnsafeQueueUserWorkItem(WaitCallbackDelegate, continuation);
#endif
                    }
                }

                static void Continuation(object state)
                {
                    ((Action)state).Invoke();
                }
            }

#if NETCOREAPP3_1

            sealed class ThreadPoolWorkItem : IThreadPoolWorkItem
            {
                static readonly ConcurrentQueue<ThreadPoolWorkItem> pool = new ConcurrentQueue<ThreadPoolWorkItem>();

                Action continuation;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ThreadPoolWorkItem Create(Action continuation)
                {
                    if (!pool.TryDequeue(out var item))
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
                    pool.Enqueue(this);

                    call.Invoke();
                }
            }

#endif
        }
    }
}