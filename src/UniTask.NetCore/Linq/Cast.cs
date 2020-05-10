using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> Cast<TResult>(this IUniTaskAsyncEnumerable<Object> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Cast<TResult>(source);
        }
    }

    internal sealed class Cast<TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<object> source;

        public Cast(IUniTaskAsyncEnumerable<object> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(source, cancellationToken);
        }

        class Enumerator : AsyncEnumeratorBase<object, TResult>
        {
            public Enumerator(IUniTaskAsyncEnumerable<object> source, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    Current = (TResult)SourceCurrent;
                    result = true;
                    return true;
                }

                result = false;
                return true;
            }
        }
    }
}