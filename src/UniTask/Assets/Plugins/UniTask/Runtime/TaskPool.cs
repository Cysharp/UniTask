using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    // internally used but public, allow to user create custom operator with pooling.

    public static class TaskPool
    {
        internal static int MaxPoolSize;

        // avoid to use ConcurrentDictionary for safety of WebGL build.
        static Dictionary<Type, Func<int>> sizes = new Dictionary<Type, Func<int>>();

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

        public static IEnumerable<(Type, int)> GetCacheSizeInfo()
        {
            lock (sizes)
            {
                foreach (var item in sizes)
                {
                    yield return (item.Key, item.Value());
                }
            }
        }

        public static void RegisterSizeGetter(Type type, Func<int> getSize)
        {
            lock (sizes)
            {
                sizes[type] = getSize;
            }
        }
    }

    public interface ITaskPoolNode<T>
    {
        ref T NextNode { get; }
    }

    // mutable struct, don't mark readonly.
    [StructLayout(LayoutKind.Auto)]
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
                    ref var nextNode = ref v.NextNode;
                    root = nextNode;
                    nextNode = null;
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
}