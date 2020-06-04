using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UniTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return ToLookup.ToLookupAsync(source, keySelector, EqualityComparer<TKey>.Default, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToLookup.ToLookupAsync(source, keySelector, comparer, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));

            return ToLookup.ToLookupAsync(source, keySelector, elementSelector, EqualityComparer<TKey>.Default, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToLookup.ToLookupAsync(source, keySelector, elementSelector, comparer, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return ToLookup.ToLookupAwaitAsync(source, keySelector, EqualityComparer<TKey>.Default, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToLookup.ToLookupAwaitAsync(source, keySelector, comparer, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));

            return ToLookup.ToLookupAwaitAsync(source, keySelector, elementSelector, EqualityComparer<TKey>.Default, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToLookup.ToLookupAwaitAsync(source, keySelector, elementSelector, comparer, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));

            return ToLookup.ToLookupAwaitWithCancellationAsync(source, keySelector, EqualityComparer<TKey>.Default, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToLookup.ToLookupAwaitWithCancellationAsync(source, keySelector, comparer, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));

            return ToLookup.ToLookupAwaitWithCancellationAsync(source, keySelector, elementSelector, EqualityComparer<TKey>.Default, cancellationToken);
        }

        public static UniTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(elementSelector, nameof(elementSelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return ToLookup.ToLookupAwaitWithCancellationAsync(source, keySelector, elementSelector, comparer, cancellationToken);
        }
    }

    internal static class ToLookup
    {
        internal static async UniTask<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TSource>.CreateEmpty();
                }
                else
                {
                    return Lookup<TKey, TSource>.Create(new ArraySegment<TSource>(array, 0, i), keySelector, comparer);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async UniTask<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IUniTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TElement>.CreateEmpty();
                }
                else
                {
                    return Lookup<TKey, TElement>.Create(new ArraySegment<TSource>(array, 0, i), keySelector, elementSelector, comparer);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }


        // with await

        internal static async UniTask<ILookup<TKey, TSource>> ToLookupAwaitAsync<TSource, TKey>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IUniTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TSource>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TSource>.CreateAsync(new ArraySegment<TSource>(array, 0, i), keySelector, comparer);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async UniTask<ILookup<TKey, TElement>> ToLookupAwaitAsync<TSource, TKey, TElement>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IUniTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TElement>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TElement>.CreateAsync(new ArraySegment<TSource>(array, 0, i), keySelector, elementSelector, comparer);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // with cancellation

        internal static async UniTask<ILookup<TKey, TSource>> ToLookupAwaitWithCancellationAsync<TSource, TKey>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IUniTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TSource>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TSource>.CreateAsync(new ArraySegment<TSource>(array, 0, i), keySelector, comparer, cancellationToken);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal static async UniTask<ILookup<TKey, TElement>> ToLookupAwaitWithCancellationAsync<TSource, TKey, TElement>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
        {
            var pool = ArrayPool<TSource>.Shared;
            var array = pool.Rent(16);

            IUniTaskAsyncEnumerator<TSource> e = default;
            try
            {
                e = source.GetAsyncEnumerator(cancellationToken);
                var i = 0;
                while (await e.MoveNextAsync())
                {
                    ArrayPoolUtil.EnsureCapacity(ref array, i, pool);
                    array[i++] = e.Current;
                }

                if (i == 0)
                {
                    return Lookup<TKey, TElement>.CreateEmpty();
                }
                else
                {
                    return await Lookup<TKey, TElement>.CreateAsync(new ArraySegment<TSource>(array, 0, i), keySelector, elementSelector, comparer, cancellationToken);
                }
            }
            finally
            {
                pool.Return(array, clearArray: !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<TSource>());

                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // Lookup

        class Lookup<TKey, TElement> : ILookup<TKey, TElement>
        {
            static readonly Lookup<TKey, TElement> empty = new Lookup<TKey, TElement>(new Dictionary<TKey, Grouping<TKey, TElement>>());

            // original lookup keeps order but this impl does not(dictionary not guarantee)
            readonly Dictionary<TKey, Grouping<TKey, TElement>> dict;

            Lookup(Dictionary<TKey, Grouping<TKey, TElement>> dict)
            {
                this.dict = dict;
            }

            public static Lookup<TKey, TElement> CreateEmpty()
            {
                return empty;
            }

            public static Lookup<TKey, TElement> Create(ArraySegment<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (int i = source.Offset; i < c; i++)
                {
                    var key = keySelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(arr[i]);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static Lookup<TKey, TElement> Create<TSource>(ArraySegment<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (int i = source.Offset; i < c; i++)
                {
                    var key = keySelector(arr[i]);
                    var elem = elementSelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(elem);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async UniTask<Lookup<TKey, TElement>> CreateAsync(ArraySegment<TElement> source, Func<TElement, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (int i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(arr[i]);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async UniTask<Lookup<TKey, TElement>> CreateAsync<TSource>(ArraySegment<TSource> source, Func<TSource, UniTask<TKey>> keySelector, Func<TSource, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (int i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i]);
                    var elem = await elementSelector(arr[i]);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(elem);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async UniTask<Lookup<TKey, TElement>> CreateAsync(ArraySegment<TElement> source, Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (int i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i], cancellationToken);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(arr[i]);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public static async UniTask<Lookup<TKey, TElement>> CreateAsync<TSource>(ArraySegment<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, Func<TSource, CancellationToken, UniTask<TElement>> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
            {
                var dict = new Dictionary<TKey, Grouping<TKey, TElement>>(comparer);

                var arr = source.Array;
                var c = source.Count;
                for (int i = source.Offset; i < c; i++)
                {
                    var key = await keySelector(arr[i], cancellationToken);
                    var elem = await elementSelector(arr[i], cancellationToken);

                    if (!dict.TryGetValue(key, out var list))
                    {
                        list = new Grouping<TKey, TElement>(key);
                        dict[key] = list;
                    }

                    list.Add(elem);
                }

                return new Lookup<TKey, TElement>(dict);
            }

            public IEnumerable<TElement> this[TKey key] => dict.TryGetValue(key, out var g) ? g : Enumerable.Empty<TElement>();

            public int Count => dict.Count;

            public bool Contains(TKey key)
            {
                return dict.ContainsKey(key);
            }

            public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
            {
                return dict.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return dict.Values.GetEnumerator();
            }
        }

        class Grouping<TKey, TElement> : IGrouping<TKey, TElement> // , IUniTaskAsyncGrouping<TKey, TElement>
        {
            readonly List<TElement> elements;

            public TKey Key { get; private set; }

            public Grouping(TKey key)
            {
                this.Key = key;
                this.elements = new List<TElement>();
            }

            public void Add(TElement value)
            {
                elements.Add(value);
            }
            public IEnumerator<TElement> GetEnumerator()
            {
                return elements.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return elements.GetEnumerator();
            }

            public IUniTaskAsyncEnumerator<TElement> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return this.ToUniTaskAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
            }

            public override string ToString()
            {
                return "Key: " + Key + ", Count: " + elements.Count;
            }
        }
    }
}