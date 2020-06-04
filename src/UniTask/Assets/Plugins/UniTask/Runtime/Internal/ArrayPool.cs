#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
    // Same interface as System.Buffers.ArrayPool<T> but only provides Shared.

    internal sealed class ArrayPool<T>
    {
        // Same size as System.Buffers.DefaultArrayPool<T>
        const int DefaultMaxNumberOfArraysPerBucket = 50;

        static readonly T[] EmptyArray = new T[0];

        public static readonly ArrayPool<T> Shared = new ArrayPool<T>();

        readonly MinimumQueue<T[]>[] buckets;
        readonly SpinLock[] locks;

        ArrayPool()
        {
            // see: GetQueueIndex
            buckets = new MinimumQueue<T[]>[18];
            locks = new SpinLock[18];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new MinimumQueue<T[]>(4);
                locks[i] = new SpinLock(false);
            }
        }

        public T[] Rent(int minimumLength)
        {
            if (minimumLength < 0)
            {
                throw new ArgumentOutOfRangeException("minimumLength");
            }
            else if (minimumLength == 0)
            {
                return EmptyArray;
            }

            var size = CalculateSize(minimumLength);
            var index = GetQueueIndex(size);
            if (index != -1)
            {
                var q = buckets[index];
                var lockTaken = false;
                try
                {
                    locks[index].Enter(ref lockTaken);

                    if (q.Count != 0)
                    {
                        return q.Dequeue();
                    }
                }
                finally
                {
                    if (lockTaken) locks[index].Exit(false);
                }
            }

            return new T[size];
        }

        public void Return(T[] array, bool clearArray = false)
        {
            if (array == null || array.Length == 0)
            {
                return;
            }

            var index = GetQueueIndex(array.Length);
            if (index != -1)
            {
                if (clearArray)
                {
                    Array.Clear(array, 0, array.Length);
                }

                var q = buckets[index];
                var lockTaken = false;

                try
                {
                    locks[index].Enter(ref lockTaken);

                    if (q.Count > DefaultMaxNumberOfArraysPerBucket)
                    {
                        return;
                    }

                    q.Enqueue(array);
                }
                finally
                {
                    if (lockTaken) locks[index].Exit(false);
                }
            }
        }

        static int CalculateSize(int size)
        {
            size--;
            size |= size >> 1;
            size |= size >> 2;
            size |= size >> 4;
            size |= size >> 8;
            size |= size >> 16;
            size += 1;

            if (size < 8)
            {
                size = 8;
            }

            return size;
        }

        static int GetQueueIndex(int size)
        {
            switch (size)
            {
                case 8: return 0;
                case 16: return 1;
                case 32: return 2;
                case 64: return 3;
                case 128: return 4;
                case 256: return 5;
                case 512: return 6;
                case 1024: return 7;
                case 2048: return 8;
                case 4096: return 9;
                case 8192: return 10;
                case 16384: return 11;
                case 32768: return 12;
                case 65536: return 13;
                case 131072: return 14;
                case 262144: return 15;
                case 524288: return 16;
                case 1048576: return 17; // max array length
                default:
                    return -1;
            }
        }
    }
}
