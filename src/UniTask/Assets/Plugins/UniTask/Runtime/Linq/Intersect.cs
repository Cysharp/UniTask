using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> Intersect<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));

            return new Intersect<TSource>(first, second, EqualityComparer<TSource>.Default);
        }

        public static IUniTaskAsyncEnumerable<TSource> Intersect<TSource>(this IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new Intersect<TSource>(first, second, comparer);
        }
    }

    internal sealed class Intersect<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> first;
        readonly IUniTaskAsyncEnumerable<TSource> second;
        readonly IEqualityComparer<TSource> comparer;

        public Intersect(IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            this.first = first;
            this.second = second;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Intersect(first, second, comparer, cancellationToken);
        }

        class _Intersect : AsyncEnumeratorBase<TSource, TSource>
        {
            static Action<object> HashSetAsyncCoreDelegate = HashSetAsyncCore;

            readonly IEqualityComparer<TSource> comparer;
            readonly IUniTaskAsyncEnumerable<TSource> second;

            HashSet<TSource> set;
            UniTask<HashSet<TSource>>.Awaiter awaiter;

            public _Intersect(IUniTaskAsyncEnumerable<TSource> first, IUniTaskAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken)

                : base(first, cancellationToken)
            {
                this.second = second;
                this.comparer = comparer;
            }

            protected override bool OnFirstIteration()
            {
                if (set != null) return false;

                awaiter = second.ToHashSetAsync(cancellationToken).GetAwaiter();
                if (awaiter.IsCompleted)
                {
                    set = awaiter.GetResult();
                    SourceMoveNext();
                }
                else
                {
                    awaiter.SourceOnCompleted(HashSetAsyncCoreDelegate, this);
                }

                return true;
            }

            static void HashSetAsyncCore(object state)
            {
                var self = (_Intersect)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    self.set = result;
                    self.SourceMoveNext();
                }
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    var v = SourceCurrent;

                    if (set.Remove(v))
                    {
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
}