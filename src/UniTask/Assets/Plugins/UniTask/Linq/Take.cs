using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> Take<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Take<TSource>(source, count);
        }
    }

    internal sealed class Take<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly int count;

        public Take(IUniTaskAsyncEnumerable<TSource> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(source, count, cancellationToken);
        }

        sealed class Enumerator : AsyncEnumeratorBase<TSource, TSource>
        {
            readonly int count;

            int index;

            public Enumerator(IUniTaskAsyncEnumerable<TSource> source, int count, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.count = count;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (checked(index++) < count)
                    {
                        Current = SourceCurrent;
                        result = true;
                        return true;
                    }
                    else
                    {
                        result = false;
                        return true;
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