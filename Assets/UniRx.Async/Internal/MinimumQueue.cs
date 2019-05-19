#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#endif

using System;
using System.Runtime.CompilerServices;

namespace UniRx.Async.Internal
{
    // optimized version of Standard Queue<T>.
    internal class MinimumQueue<T>
    {
        const int MinimumGrow = 4;
        const int GrowFactor = 200;

        T[] array;
        int head;
        int tail;
        int size;

        public MinimumQueue(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException("capacity");
            array = new T[capacity];
            head = tail = size = 0;
        }

        public int Count
        {
#if NET_4_6 || NET_STANDARD_2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get { return size; }
        }

        public T Peek()
        {
            if (size == 0) ThrowForEmptyQueue();
            return array[head];
        }

#if NET_4_6 || NET_STANDARD_2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Enqueue(T item)
        {
            if (size == array.Length)
            {
                Grow();
            }

            array[tail] = item;
            MoveNext(ref tail);
            size++;
        }

#if NET_4_6 || NET_STANDARD_2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public T Dequeue()
        {
            if (size == 0) ThrowForEmptyQueue();

            int head = this.head;
            T[] array = this.array;
            T removed = array[head];
            array[head] = default(T);
            MoveNext(ref this.head);
            size--;
            return removed;
        }

        void Grow()
        {
            int newcapacity = (int)((long)array.Length * (long)GrowFactor / 100);
            if (newcapacity < array.Length + MinimumGrow)
            {
                newcapacity = array.Length + MinimumGrow;
            }
            SetCapacity(newcapacity);
        }

        void SetCapacity(int capacity)
        {
            T[] newarray = new T[capacity];
            if (size > 0)
            {
                if (head < tail)
                {
                    Array.Copy(array, head, newarray, 0, size);
                }
                else
                {
                    Array.Copy(array, head, newarray, 0, array.Length - head);
                    Array.Copy(array, 0, newarray, array.Length - head, tail);
                }
            }

            array = newarray;
            head = 0;
            tail = (size == capacity) ? 0 : size;
        }

#if NET_4_6 || NET_STANDARD_2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        void MoveNext(ref int index)
        {
            int tmp = index + 1;
            if (tmp == array.Length)
            {
                tmp = 0;
            }
            index = tmp;
        }

        void ThrowForEmptyQueue()
        {
            throw new InvalidOperationException("EmptyQueue");
        }
    }
}