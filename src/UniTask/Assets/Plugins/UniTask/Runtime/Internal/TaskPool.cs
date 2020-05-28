using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
    // internaly used but public, allow to user create custom operator with pooling.

    public static class TaskPool
    {
        internal static int MaxPoolSize;

        static TaskPool()
        {
            try
            {
                var value = Environment.GetEnvironmentVariable("UNITASK_MAX_POOLSIZE");
                if (value != null)
                {
                    if (int.TryParse(value, out var size))
                    {
                        MaxPoolSize = size;
                        return;
                    }
                }
            }
            catch { }

            MaxPoolSize = int.MaxValue;
        }

        public static void SetMaxPoolSize(int maxPoolSize)
        {
            MaxPoolSize = maxPoolSize;
        }
    }


    public interface ITaskPoolNode<T>
    {
        T NextNode { get; set; }
    }

    // mutable struct, don't mark readonly.
    public struct TaskPool<T>
        where T : class, ITaskPoolNode<T>
    {
        int gate;
        int size;
        T root;

        public int Size => size;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPop(out T result)
        {
            if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
            {
                var v = root;
                if (!(v is null))
                {
                    root = v.NextNode;
                    v.NextNode = null;
                    size--;
                    result = v;
                    Volatile.Write(ref gate, 0);
                    return true;
                }

                Volatile.Write(ref gate, 0);
            }
            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPush(T item)
        {
            if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
            {
                if (size < TaskPool.MaxPoolSize)
                {
                    item.NextNode = root;
                    root = item;
                    size++;
                    Volatile.Write(ref gate, 0);
                    return true;
                }
                else
                {
                    Volatile.Write(ref gate, 0);
                }
            }
            return false;
        }
    }

    public static class TaskPoolMonitor
    {
        static ConcurrentDictionary<Type, Func<int>> sizes = new ConcurrentDictionary<Type, Func<int>>();

        public static IEnumerable<(Type, int)> GetCacheSizeInfo()
        {
            foreach (var item in sizes)
            {
                yield return (item.Key, item.Value());
            }
        }

        public static void RegisterSizeGettter(Type type, Func<int> getSize)
        {
            sizes[type] = getSize;
        }
    }
}