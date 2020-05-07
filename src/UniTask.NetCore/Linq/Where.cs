using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> Where<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return new Cysharp.Threading.Tasks.Linq.Where<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> WhereAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate)
        {
            return new Cysharp.Threading.Tasks.Linq.WhereAwait<TSource>(source, predicate);
        }
    }

    internal sealed class Where<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, bool> predicate;

        public Where(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(source, predicate, cancellationToken);
        }

        class Enumerator : AsyncEnumeratorBase<TSource, TSource>
        {
            readonly Func<TSource, bool> predicate;

            public Enumerator(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (predicate(SourceCurrent))
                    {
                        Current = SourceCurrent;
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

    internal sealed class WhereAwait<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<bool>> predicate;

        public WhereAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(source, predicate, cancellationToken);
        }

        class Enumerator : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            readonly Func<TSource, UniTask<bool>> predicate;

            public Enumerator(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(bool awaitResult)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

}