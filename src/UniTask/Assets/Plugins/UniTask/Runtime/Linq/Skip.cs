using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> Skip<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Skip<TSource>(source, count);
        }
    }

    internal sealed class Skip<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly int count;

        public Skip(IUniTaskAsyncEnumerable<TSource> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Skip(source, count, cancellationToken);
        }

        sealed class _Skip : AsyncEnumeratorBase<TSource, TSource>
        {
            readonly int count;

            int index;

            public _Skip(IUniTaskAsyncEnumerable<TSource> source, int count, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.count = count;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (count <= checked(index++))
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
                else
                {
                    result = false;
                    return true;
                }
            }
        }
    }
}