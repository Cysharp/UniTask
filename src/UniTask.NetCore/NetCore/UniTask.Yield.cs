using System;
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
                        ThreadPool.UnsafeQueueUserWorkItem(WaitCallbackDelegate, continuation);
                    }
                }

                static void Continuation(object state)
                {
                    ((Action)state).Invoke();
                }
            }
        }
    }
}