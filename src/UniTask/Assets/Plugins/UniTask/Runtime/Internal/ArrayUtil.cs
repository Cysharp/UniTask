#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
    internal static class ArrayUtil
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnsureCapacity<T>(ref T[] array, int index)
        {
            if (array.Length <= index)
            {
                EnsureCore(ref array, index);
            }
        }

        // rare case, no inlining.
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void EnsureCore<T>(ref T[] array, int index)
        {
            var newSize = array.Length * 2;
            var newArray = new T[(index < newSize) ? newSize : (index * 2)];
            Array.Copy(array, 0, newArray, 0, array.Length);

            array = newArray;
        }

        /// <summary>
        /// Optimizing utility to avoid .ToArray() that creates buffer copy(cut to just size).
        /// </summary>
        public static (T[] array, int length) Materialize<T>(IEnumerable<T> source)
        {
            if (source is T[] array)
            {
                return (array, array.Length);
            }

            var defaultCount = 4;
            if (source is ICollection<T> coll)
            {
                defaultCount = coll.Count;
                var buffer = new T[defaultCount];
                coll.CopyTo(buffer, 0);
                return (buffer, defaultCount);
            }
            else if (source is IReadOnlyCollection<T> rcoll)
            {
                defaultCount = rcoll.Count;
            }

            if (defaultCount == 0)
            {
                return (Array.Empty<T>(), 0);
            }

            {
                var index = 0;
                var buffer = new T[defaultCount];
                foreach (var item in source)
                {
                    EnsureCapacity(ref buffer, index);
                    buffer[index++] = item;
                }

                return (buffer, index);
            }
        }
        
        public static void Swap<T>(IList<T> array, int indexA, int indexB)
        {
            var tmp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = tmp;
        }
        
        /// <summary>
        /// Returns the index of the first element satisfying the <paramref name="predicate"/>.
        /// Returns -1 if no such element exists.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="predicate">The predicate use to search the array.</param>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <returns>Index of the first element satisfying the <paramref name="predicate"/> otherwise returns -1.</returns>
        public static int FirstIndexOf<T>(IReadOnlyList<T> array, Func<T, bool> predicate)
        {
            for (var i = 0; i < array.Count; ++i)
            {
                if (!predicate(array[i])) continue;
                return i;
            }

            return -1;
        }

        /// <summary>
        /// Reorders the elements in <paramref name="array"/> in such a way that all elements for which the <paramref name="predicate"/>
        /// returns <c>true </c> precede the elements for which <paramref name="predicate"/> returns <c>false</c>.
        /// Relative order of the elements is not preserved. 
        /// </summary>
        /// <param name="array">The array to partition.</param>
        /// <param name="predicate">The predicate that determine how the elements are partitioned.</param>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <returns>Return the index of first element of the second group.</returns>
        public static int Partition<T>(T[] array, Func<T, bool> predicate)
        {
            var pivot = FirstIndexOf(array, element => !predicate(element));
            if (pivot == -1) return array.Length;
            for (var i = pivot + 1; i < array.Length; ++i)
            {
                if (!predicate(array[i])) continue;
                Swap(array, pivot, i);
                ++pivot;
            }

            return pivot;
        }
    }
}

