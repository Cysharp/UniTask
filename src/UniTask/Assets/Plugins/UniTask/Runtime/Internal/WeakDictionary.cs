#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;

namespace Cysharp.Threading.Tasks.Internal
{
    // Hash map with weakly referenced keys. An entry whose key has been collected is dropped the next
    // time its bucket is walked. Every public operation runs under the same lock, including ToList.
    internal class WeakDictionary<TKey, TValue>
        where TKey : class
    {
        readonly object gate = new object();
        readonly float loadFactor;
        readonly IEqualityComparer<TKey> keyEqualityComparer;

        Entry[] buckets;
        int size; // entries in the table, dead ones included until they are swept

        public WeakDictionary(int capacity = 4, float loadFactor = 0.75f, IEqualityComparer<TKey> keyComparer = null)
        {
            this.buckets = new Entry[CalculateCapacity(capacity, loadFactor)];
            this.loadFactor = loadFactor;
            this.keyEqualityComparer = keyComparer ?? EqualityComparer<TKey>.Default;
        }

        public bool TryAdd(TKey key, TValue value)
        {
            var hash = keyEqualityComparer.GetHashCode(key);
            lock (gate)
            {
                if (TryGetEntry(key, hash, out _, out _, out _))
                {
                    return false; // duplicate
                }

                var capacity = CalculateCapacity(size + 1, loadFactor);
                if (buckets.Length < capacity)
                {
                    Rehash(capacity);
                }

                var index = hash & (buckets.Length - 1);
                buckets[index] = new Entry
                {
                    Key = new WeakReference<TKey>(key, false),
                    Value = value,
                    Hash = hash,
                    Next = buckets[index]
                };
                size++;
                return true;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var hash = keyEqualityComparer.GetHashCode(key);
            lock (gate)
            {
                if (TryGetEntry(key, hash, out _, out _, out var entry))
                {
                    value = entry.Value;
                    return true;
                }

                value = default(TValue);
                return false;
            }
        }

        public bool TryRemove(TKey key)
        {
            var hash = keyEqualityComparer.GetHashCode(key);
            lock (gate)
            {
                if (TryGetEntry(key, hash, out var index, out var prev, out var entry))
                {
                    Unlink(index, prev, entry);
                    return true;
                }

                return false;
            }
        }

        // Walks the bucket of the key and sweeps dead entries on the way.
        // prev is the live entry before the match, or null when the match heads the bucket.
        bool TryGetEntry(TKey key, int hash, out int index, out Entry prev, out Entry entry)
        {
            index = hash & (buckets.Length - 1);
            prev = null;
            entry = buckets[index];
            while (entry != null)
            {
                if (entry.Key.TryGetTarget(out var target))
                {
                    if (entry.Hash == hash && keyEqualityComparer.Equals(key, target))
                    {
                        return true;
                    }

                    prev = entry;
                    entry = entry.Next;
                }
                else
                {
                    var next = entry.Next;
                    Unlink(index, prev, entry);
                    entry = next;
                }
            }

            return false;
        }

        void Unlink(int index, Entry prev, Entry entry)
        {
            if (prev == null)
            {
                buckets[index] = entry.Next;
            }
            else
            {
                prev.Next = entry.Next;
            }

            entry.Next = null;
            size--;
        }

        // Moves the live entries into a table of the given capacity; dead entries are dropped.
        void Rehash(int capacity)
        {
            var next = new Entry[capacity];
            var count = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                var entry = buckets[i];
                while (entry != null)
                {
                    var following = entry.Next;
                    if (entry.Key.TryGetTarget(out _))
                    {
                        var index = entry.Hash & (capacity - 1);
                        entry.Next = next[index];
                        next[index] = entry;
                        count++;
                    }

                    entry = following;
                }
            }

            buckets = next;
            size = count;
        }

        public List<KeyValuePair<TKey, TValue>> ToList()
        {
            var list = new List<KeyValuePair<TKey, TValue>>(size);
            ToList(ref list, false);
            return list;
        }

        // Fills the list from its start, reusing existing slots, and returns the number of live entries.
        public int ToList(ref List<KeyValuePair<TKey, TValue>> list, bool clear = true)
        {
            if (clear)
            {
                list.Clear();
            }

            var listIndex = 0;
            lock (gate)
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    Entry prev = null;
                    var entry = buckets[i];
                    while (entry != null)
                    {
                        var next = entry.Next;
                        if (entry.Key.TryGetTarget(out var target))
                        {
                            var item = new KeyValuePair<TKey, TValue>(target, entry.Value);
                            if (listIndex < list.Count)
                            {
                                list[listIndex] = item;
                            }
                            else
                            {
                                list.Add(item);
                            }

                            listIndex++;
                            prev = entry;
                        }
                        else
                        {
                            Unlink(i, prev, entry);
                        }

                        entry = next;
                    }
                }
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
            public Entry Next;
        }
    }
}
