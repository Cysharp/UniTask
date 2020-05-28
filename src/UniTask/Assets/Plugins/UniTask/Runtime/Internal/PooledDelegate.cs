using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class PooledDelegate<T> : ITaskPoolNode<PooledDelegate<T>>
    {
        static TaskPool<PooledDelegate<T>> pool;

        public PooledDelegate<T> NextNode { get; set; }

        static PooledDelegate()
        {
            TaskPool.RegisterSizeGetter(typeof(PooledDelegate<T>), () => pool.Size);
        }

        readonly Action<T> runDelegate;
        Action continuation;

        PooledDelegate()
        {
            runDelegate = Run;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<T> Create(Action continuation)
        {
            if (!pool.TryPop(out var item))
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
                pool.TryPush(this);
                call.Invoke();
            }
        }
    }
}