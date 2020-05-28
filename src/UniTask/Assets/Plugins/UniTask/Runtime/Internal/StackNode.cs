using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
    // internaly used but public, allow to user create custom operator with pooling.

    public interface IStackNode<T>
    {
        T NextNode { get; set; }
    }

    public static class StackNodeHelper
    {
        static int MaxPoolSize;

        static StackNodeHelper()
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

        // Strictness as a Stack is not required.
        // If there is a conflict, it will go through as is.

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPop<T>(ref int stackLock, ref int size, ref T root, out T result)
            where T : class, IStackNode<T>
        {
            if (Interlocked.CompareExchange(ref stackLock, 1, 0) == 0)
            {
                var v = root;
                if (!(v is null))
                {
                    root = v.NextNode;
                    v.NextNode = null;
                    size--;
                    result = v;
                    Volatile.Write(ref stackLock, 0);
                    return true;
                }

                Volatile.Write(ref stackLock, 0);
            }
            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPush<T>(ref int stackLock, ref int size, ref T root, T item)
            where T : class, IStackNode<T>
        {
            if (Interlocked.CompareExchange(ref stackLock, 1, 0) == 0)
            {
                if (size < MaxPoolSize)
                {
                    item.NextNode = root;
                    root = item;
                    size++;
                    Volatile.Write(ref stackLock, 0);
                    return true;
                }
                else
                {
                    Volatile.Write(ref stackLock, 0);
                }
            }
            return false;
        }
    }

    public static class StackNodeMonitor
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