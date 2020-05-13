using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> Select<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.Select<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> Select<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, TResult> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectInt<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectAwait<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectIntAwait<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectAwaitWithCancellation<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectIntAwaitWithCancellation<TSource, TResult>(source, selector);
        }
    }

    internal sealed class Select<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, TResult> selector;

        public Select(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Select(source, selector, cancellationToken);
        }

        sealed class _Select : AsyncEnumeratorBase<TSource, TResult>
        {
            readonly Func<TSource, TResult> selector;

            public _Select(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.selector = selector;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    Current = selector(SourceCurrent);
                    result = true;
                    return true;
                }
                else
                {
                    result = false;
                    return true;
                }
            }
        }
    }

    internal sealed class SelectInt<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, TResult> selector;

        public SelectInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectInt(source, selector, cancellationToken);
        }

        sealed class _SelectInt : AsyncEnumeratorBase<TSource, TResult>
        {
            readonly Func<TSource, int, TResult> selector;
            int index;

            public _SelectInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.selector = selector;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    Current = selector(SourceCurrent, checked(index++));
                    result = true;
                    return true;
                }
                else
                {
                    result = false;
                    return true;
                }
            }
        }
    }

    internal sealed class SelectAwait<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<TResult>> selector;

        public SelectAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectAwait(source, selector, cancellationToken);
        }

        sealed class _SelectAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TResult>
        {
            readonly Func<TSource, UniTask<TResult>> selector;

            public _SelectAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.selector = selector;
            }

            protected override UniTask<TResult> TransformAsync(TSource sourceCurrent)
            {
                return selector(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(TResult awaitResult, out bool terminateIteration)
            {
                Current = awaitResult;
                terminateIteration = false;
                return true;
            }
        }
    }

    internal sealed class SelectIntAwait<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, UniTask<TResult>> selector;

        public SelectIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectIntAwait(source, selector, cancellationToken);
        }

        sealed class _SelectIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TResult>
        {
            readonly Func<TSource, int, UniTask<TResult>> selector;
            int index;

            public _SelectIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<TResult>> selector, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.selector = selector;
            }

            protected override UniTask<TResult> TransformAsync(TSource sourceCurrent)
            {
                return selector(sourceCurrent, checked(index++));
            }

            protected override bool TrySetCurrentCore(TResult awaitResult, out bool terminateIteration)
            {
                Current = awaitResult;
                terminateIteration = false;
                return true;
            }
        }
    }

    internal sealed class SelectAwaitWithCancellation<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, CancellationToken, UniTask<TResult>> selector;

        public SelectAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectAwaitWithCancellation(source, selector, cancellationToken);
        }

        sealed class _SelectAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TResult>
        {
            readonly Func<TSource, CancellationToken, UniTask<TResult>> selector;

            public _SelectAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.selector = selector;
            }

            protected override UniTask<TResult> TransformAsync(TSource sourceCurrent)
            {
                return selector(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(TResult awaitResult, out bool terminateIteration)
            {
                Current = awaitResult;
                terminateIteration = false;
                return true;
            }
        }
    }

    internal sealed class SelectIntAwaitWithCancellation<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, CancellationToken, UniTask<TResult>> selector;

        public SelectIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectIntAwaitWithCancellation(source, selector, cancellationToken);
        }

        sealed class _SelectIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TResult>
        {
            readonly Func<TSource, int, CancellationToken, UniTask<TResult>> selector;
            int index;

            public _SelectIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.selector = selector;
            }

            protected override UniTask<TResult> TransformAsync(TSource sourceCurrent)
            {
                return selector(sourceCurrent, checked(index++), cancellationToken);
            }

            protected override bool TrySetCurrentCore(TResult awaitResult, out bool terminateIteration)
            {
                Current = awaitResult;
                terminateIteration = false;
                return true;
            }
        }
    }

}