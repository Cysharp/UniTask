using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            return DistinctUntilChanged(source, EqualityComparer<TSource>.Default);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource>(this IUniTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new DistinctUntilChanged<TSource>(source, comparer);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return DistinctUntilChanged(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new DistinctUntilChanged<TSource, TKey>(source, keySelector, comparer);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChangedAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector)
        {
            return DistinctUntilChangedAwait(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChangedAwait<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new DistinctUntilChangedAwait<TSource, TKey>(source, keySelector, comparer);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChangedAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector)
        {
            return DistinctUntilChangedAwaitWithCancellation(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static IUniTaskAsyncEnumerable<TSource> DistinctUntilChangedAwaitWithCancellation<TSource, TKey>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(keySelector, nameof(keySelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new DistinctUntilChangedAwaitWithCancellation<TSource, TKey>(source, keySelector, comparer);
        }
    }

    internal sealed class DistinctUntilChanged<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly IEqualityComparer<TSource> comparer;

        public DistinctUntilChanged(IUniTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            this.source = source;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChanged(source, comparer, cancellationToken);
        }

        class _DistinctUntilChanged : AsyncEnumeratorBase<TSource, TSource>
        {
            readonly IEqualityComparer<TSource> comparer;
            TSource prev;
            bool first;

            public _DistinctUntilChanged(IUniTaskAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.comparer = comparer;
                this.first = true;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    var v = SourceCurrent;
                    if (first || !comparer.Equals(prev, v))
                    {
                        first = false;
                        Current = prev = v;
                        result = true;
                        return true;
                    }
                    else
                    {
                        result = default;
                        return false;
                    }
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class DistinctUntilChanged<TSource, TKey> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, TKey> keySelector;
        readonly IEqualityComparer<TKey> comparer;

        public DistinctUntilChanged(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChanged(source, keySelector, comparer, cancellationToken);
        }

        class _DistinctUntilChanged : AsyncEnumeratorBase<TSource, TSource>
        {
            readonly IEqualityComparer<TKey> comparer;
            readonly Func<TSource, TKey> keySelector;
            TKey prev;
            bool first;

            public _DistinctUntilChanged(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.comparer = comparer;
                this.keySelector = keySelector;
                this.first = true;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    var v = SourceCurrent;
                    var key = keySelector(v);
                    if (first || !comparer.Equals(prev, key))
                    {
                        first = false;
                        prev = key;
                        Current = v;
                        result = true;
                        return true;
                    }
                    else
                    {
                        result = default;
                        return false;
                    }
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class DistinctUntilChangedAwait<TSource, TKey> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<TKey>> keySelector;
        readonly IEqualityComparer<TKey> comparer;

        public DistinctUntilChangedAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChangedAwait(source, keySelector, comparer, cancellationToken);
        }

        class _DistinctUntilChangedAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, TKey>
        {
            readonly IEqualityComparer<TKey> comparer;
            readonly Func<TSource, UniTask<TKey>> keySelector;
            TKey prev;
            bool first;

            public _DistinctUntilChangedAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.comparer = comparer;
                this.keySelector = keySelector;
                this.first = true;
            }

            protected override UniTask<TKey> TransformAsync(TSource sourceCurrent)
            {
                return keySelector(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(TKey key, out bool terminateIteration)
            {
                if (first || !comparer.Equals(prev, key))
                {
                    first = false;
                    prev = key;
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }
                else
                {
                    terminateIteration = false;
                    return false;
                }
            }
        }
    }

    internal sealed class DistinctUntilChangedAwaitWithCancellation<TSource, TKey> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, CancellationToken, UniTask<TKey>> keySelector;
        readonly IEqualityComparer<TKey> comparer;

        public DistinctUntilChangedAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _DistinctUntilChangedAwaitWithCancellation(source, keySelector, comparer, cancellationToken);
        }

        class _DistinctUntilChangedAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, TKey>
        {
            readonly IEqualityComparer<TKey> comparer;
            readonly Func<TSource, CancellationToken, UniTask<TKey>> keySelector;
            TKey prev;
            bool first;

            public _DistinctUntilChangedAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TKey>> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.comparer = comparer;
                this.keySelector = keySelector;
                this.first = true;
            }

            protected override UniTask<TKey> TransformAsync(TSource sourceCurrent)
            {
                return keySelector(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(TKey key, out bool terminateIteration)
            {
                if (first || !comparer.Equals(prev, key))
                {
                    first = false;
                    prev = key;
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }
                else
                {
                    terminateIteration = false;
                    return false;
                }
            }
        }
    }
}