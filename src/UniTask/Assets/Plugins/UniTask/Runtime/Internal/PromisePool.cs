using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
    // public, allow to user create custom operator with pool.

    public interface IPromisePoolItem
    {
        void Reset();
    }

    public class PromisePool<T>
        where T : class, IPromisePoolItem
    {
        int count = 0;
        readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        readonly int maxSize;

        public PromisePool(int maxSize = 256)
        {
            this.maxSize = maxSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T TryRent()
        {
            if (queue.TryDequeue(out var value))
            {
                Interlocked.Decrement(ref count);
                return value;
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReturn(T value)
        {
            value.Reset(); // reset when return.

            if (count < maxSize)
            {
                queue.Enqueue(value);
                Interlocked.Increment(ref count);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
