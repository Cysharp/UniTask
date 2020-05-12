using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
    internal static class StateTuple
    {
        public static StateTuple<T1> Create<T1>(T1 item1)
        {
            return StatePool<T1>.Create(item1);
        }

        public static StateTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return StatePool<T1, T2>.Create(item1, item2);
        }

        public static StateTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return StatePool<T1, T2, T3>.Create(item1, item2, item3);
        }
    }

    internal class StateTuple<T1> : IDisposable
    {
        public T1 Item1;

        public void Deconstruct(out T1 item1)
        {
            item1 = this.Item1;
        }

        public void Dispose()
        {
            StatePool<T1>.Return(this);
        }
    }

    internal static class StatePool<T1>
    {
        static readonly ConcurrentQueue<StateTuple<T1>> queue = new ConcurrentQueue<StateTuple<T1>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StateTuple<T1> Create(T1 item1)
        {
            if (queue.TryDequeue(out var value))
            {
                value.Item1 = item1;
                return value;
            }

            return new StateTuple<T1> { Item1 = item1 };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(StateTuple<T1> tuple)
        {
            tuple.Item1 = default;
            queue.Enqueue(tuple);
        }
    }

    internal class StateTuple<T1, T2> : IDisposable
    {
        public T1 Item1;
        public T2 Item2;

        public void Deconstruct(out T1 item1, out T2 item2)
        {
            item1 = this.Item1;
            item2 = this.Item2;
        }

        public void Dispose()
        {
            StatePool<T1, T2>.Return(this);
        }
    }

    internal static class StatePool<T1, T2>
    {
        static readonly ConcurrentQueue<StateTuple<T1, T2>> queue = new ConcurrentQueue<StateTuple<T1, T2>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StateTuple<T1, T2> Create(T1 item1, T2 item2)
        {
            if (queue.TryDequeue(out var value))
            {
                value.Item1 = item1;
                value.Item2 = item2;
                return value;
            }

            return new StateTuple<T1, T2> { Item1 = item1, Item2 = item2 };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(StateTuple<T1, T2> tuple)
        {
            tuple.Item1 = default;
            tuple.Item2 = default;
            queue.Enqueue(tuple);
        }
    }

    internal class StateTuple<T1, T2, T3> : IDisposable
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;

        public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
        {
            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
        }

        public void Dispose()
        {
            StatePool<T1, T2, T3>.Return(this);
        }
    }

    internal static class StatePool<T1, T2, T3>
    {
        static readonly ConcurrentQueue<StateTuple<T1, T2, T3>> queue = new ConcurrentQueue<StateTuple<T1, T2, T3>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StateTuple<T1, T2, T3> Create(T1 item1, T2 item2, T3 item3)
        {
            if (queue.TryDequeue(out var value))
            {
                value.Item1 = item1;
                value.Item2 = item2;
                value.Item3 = item3;
                return value;
            }

            return new StateTuple<T1, T2, T3> { Item1 = item1, Item2 = item2, Item3 = item3 };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(StateTuple<T1, T2, T3> tuple)
        {
            tuple.Item1 = default;
            tuple.Item2 = default;
            tuple.Item3 = default;
            queue.Enqueue(tuple);
        }
    }
}
