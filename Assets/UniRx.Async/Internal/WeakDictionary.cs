#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Threading;

namespace UniRx.Async.Internal
{
    // Add, Remove, Enumerate with sweep. All operations are thread safe(in spinlock).
    internal class WeakDictionary<TKey, TValue>
        where TKey : class
    {
        Entry[] buckets;
        int size;
        SpinLock gate; // mutable struct(not readonly)

        readonly float loadFactor;
        readonly IEqualityComparer<TKey> keyEqualityComparer;

        public WeakDictionary(int capacity = 4, float loadFactor = 0.75f, IEqualityComparer<TKey> keyComparer = null)
        {
            var tableSize = CalculateCapacity(capacity, loadFactor);
            this.buckets = new Entry[tableSize];
            this.loadFactor = loadFactor;
            this.gate = new SpinLock(false);
            this.keyEqualityComparer = keyComparer ?? EqualityComparer<TKey>.Default;
        }

        public bool TryAdd(TKey key, TValue value)
        {
            bool lockTaken = false;
            try
            {
                gate.Enter(ref lockTaken);
                return TryAddInternal(key, value);
            }
            finally
            {
                if (lockTaken) gate.Exit(false);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            bool lockTaken = false;
            try
            {
                gate.Enter(ref lockTaken);
                if (TryGetEntry(key, out _, out var entry))
                {
                    value = entry.Value;
                    return true;
                }

                value = default(TValue);
                return false;
            }
            finally
            {
                if (lockTaken) gate.Exit(false);
            }
        }

        public bool TryRemove(TKey key)
        {
            bool lockTaken = false;
            try
            {
                gate.Enter(ref lockTaken);
                if (TryGetEntry(key, out var hashIndex, out var entry))
                {
                    Remove(hashIndex, entry);
                    return true;
                }

                return false;
            }
            finally
            {
                if (lockTaken) gate.Exit(false);
            }
        }

        bool TryAddInternal(TKey key, TValue value)
        {
            var nextCapacity = CalculateCapacity(size + 1, loadFactor);

            TRY_ADD_AGAIN:
            if (buckets.Length < nextCapacity)
            {
                // rehash
                var nextBucket = new Entry[nextCapacity];
                for (int i = 0; i < buckets.Length; i++)
                {
                    var e = buckets[i];
                    while (e != null)
                    {
                        AddToBuckets(nextBucket, key, e.Value, e.Hash);
                        e = e.Next;
                    }
                }

                buckets = nextBucket;
                goto TRY_ADD_AGAIN;
            }
            else
            {
                // add entry
                var successAdd = AddToBuckets(buckets, key, value, keyEqualityComparer.GetHashCode(key));
                if (successAdd) size++;
                return successAdd;
            }
        }

        bool AddToBuckets(Entry[] targetBuckets, TKey newKey, TValue value, int keyHash)
        {
            var h = keyHash;
            var hashIndex = h & (targetBuckets.Length - 1);

            TRY_ADD_AGAIN:
            if (targetBuckets[hashIndex] == null)
            {
                targetBuckets[hashIndex] = new Entry
                {
                    Key = new WeakReference<TKey>(newKey, false),
                    Value = value,
                    Hash = h
                };

                return true;
            }
            else
            {
                // add to last.
                var entry = targetBuckets[hashIndex];
                while (entry != null)
                {
                    if (entry.Key.TryGetTarget(out var target))
                    {
                        if (keyEqualityComparer.Equals(newKey, target))
                        {
                            return false; // duplicate
                        }
                    }
                    else
                    {
                        Remove(hashIndex, entry);
                        if (targetBuckets[hashIndex] == null) goto TRY_ADD_AGAIN; // add new entry
                    }

                    if (entry.Next != null)
                    {
                        entry = entry.Next;
                    }
                    else
                    {
                        // found last
                        entry.Next = new Entry
                        {
                            Key = new WeakReference<TKey>(newKey, false),
                            Value = value,
                            Hash = h
                        };
                        entry.Next.Prev = entry;
                    }
                }

                return false;
            }
        }

        bool TryGetEntry(TKey key, out int hashIndex, out Entry entry)
        {
            var table = buckets;
            var hash = keyEqualityComparer.GetHashCode(key);
            hashIndex = hash & table.Length - 1;
            entry = table[hashIndex];

            while (entry != null)
            {
                if (entry.Key.TryGetTarget(out var target))
                {
                    if (keyEqualityComparer.Equals(key, target))
                    {
                        return true;
                    }
                }
                else
                {
                    // sweap
                    Remove(hashIndex, entry);
                }

                entry = entry.Next;
            }

            return false;
        }

        void Remove(int hashIndex, Entry entry)
        {
            if (entry.Prev == null && entry.Next == null)
            {
                buckets[hashIndex] = null;
            }
            else
            {
                if (entry.Prev == null)
                {
                    buckets[hashIndex] = entry.Next;
                }
                if (entry.Prev != null)
                {
                    entry.Prev.Next = entry.Next;
                }
                if (entry.Next != null)
                {
                    entry.Next.Prev = entry.Prev;
                }
            }
            size--;
        }

        public List<KeyValuePair<TKey, TValue>> ToList()
        {
            var list = new List<KeyValuePair<TKey, TValue>>(size);
            ToList(ref list, false);
            return list;
        }

        // avoid allocate everytime.
        public int ToList(ref List<KeyValuePair<TKey, TValue>> list, bool clear = true)
        {
            if (clear)
            {
                list.Clear();
            }

            var listIndex = 0;

            bool lockTaken = false;
            try
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    var entry = buckets[i];
                    while (entry != null)
                    {
                        if (entry.Key.TryGetTarget(out var target))
                        {
                            var item = new KeyValuePair<TKey, TValue>(target, entry.Value);
                            if (listIndex < list.Count)
                            {
                                list[listIndex++] = item;
                            }
                            else
                            {
                                list.Add(item);
                                listIndex++;
                            }
                        }
                        else
                        {
                            // sweap
                            Remove(i, entry);
                        }

                        entry = entry.Next;
                    }
                }
            }
            finally
            {
                if (lockTaken) gate.Exit(false);
            }

            return listIndex;
        }

        static int CalculateCapacity(int collectionSize, float loadFactor)
        {
            var size = (int)(((float)collectionSize) / loadFactor);

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

        class Entry
        {
            public WeakReference<TKey> Key;
            public TValue Value;
            public int Hash;
            public Entry Prev;
            public Entry Next;

            // debug only
            public override string ToString()
            {
                if (Key.TryGetTarget(out var target))
                {
                    return target + "(" + Count() + ")";
                }
                else
                {
                    return "(Dead)";
                }
            }

            int Count()
            {
                var count = 1;
                var n = this;
                while (n.Next != null)
                {
                    count++;
                    n = n.Next;
                }
                return count;
            }
        }
    }
}

#endif