using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class PooledDelegate<T>
    {
        static readonly ConcurrentQueue<PooledDelegate<T>> pool = new ConcurrentQueue<PooledDelegate<T>>();

        readonly Action<T> runDelegate;

        Action continuation;


        PooledDelegate()
        {
            runDelegate = Run;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<T> Create(Action continuation)
        {
            if (!pool.TryDequeue(out var item))
            {
                item = new PooledDelegate<T>();
            }

            item.continuation = continuation;
            return item.runDelegate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run(T _)
        {
            var call = continuation;
            continuation = null;
            if (call != null)
            {
                pool.Enqueue(this);
                call.Invoke();
            }
        }
    }
}
