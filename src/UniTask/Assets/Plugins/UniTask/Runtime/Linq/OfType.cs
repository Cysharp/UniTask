using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> OfType<TResult>(this IUniTaskAsyncEnumerable<Object> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new OfType<TResult>(source);
        }
    }

    internal sealed class OfType<TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<object> source;

        public OfType(IUniTaskAsyncEnumerable<object> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _OfType(source, cancellationToken);
        }

        class _OfType : AsyncEnumeratorBase<object, TResult>
        {
            public _OfType(IUniTaskAsyncEnumerable<object> source, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (SourceCurrent is TResult castCurent)
                    {
                        Current = castCurent;
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