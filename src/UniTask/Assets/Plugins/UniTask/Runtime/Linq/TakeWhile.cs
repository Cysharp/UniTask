using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> TakeWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhile<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, Boolean> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileInt<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileAwait<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileIntAwait<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileAwaitWithCancellation<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> TakeWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new TakeWhileIntAwaitWithCancellation<TSource>(source, predicate);
        }
    }

    internal sealed class TakeWhile<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, bool> predicate;

        public TakeWhile(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeWhile(source, predicate, cancellationToken);
        }

        class _TakeWhile : AsyncEnumeratorBase<TSource, TSource>
        {
            Func<TSource, bool> predicate;

            public _TakeWhile(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)

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
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class TakeWhileInt<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, bool> predicate;

        public TakeWhileInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeWhileInt(source, predicate, cancellationToken);
        }

        class _TakeWhileInt : AsyncEnumeratorBase<TSource, TSource>
        {
            readonly Func<TSource, int, bool> predicate;
            int index;

            public _TakeWhileInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (predicate(SourceCurrent, checked(index++)))
                    {
                        Current = SourceCurrent;
                        result = true;
                        return true;
                    }
                }

                result = false;
                return true;
            }
        }
    }

    internal sealed class TakeWhileAwait<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<bool>> predicate;

        public TakeWhileAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeWhileAwait(source, predicate, cancellationToken);
        }

        class _TakeWhileAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            Func<TSource, UniTask<bool>> predicate;

            public _TakeWhileAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }
                else
                {
                    terminateIteration = true;
                    return false;
                }
            }
        }
    }

    internal sealed class TakeWhileIntAwait<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, UniTask<bool>> predicate;

        public TakeWhileIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeWhileIntAwait(source, predicate, cancellationToken);
        }

        class _TakeWhileIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            readonly Func<TSource, int, UniTask<bool>> predicate;
            int index;

            public _TakeWhileIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<bool>> predicate, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, checked(index++));
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }
                else
                {
                    terminateIteration = true;
                    return false;
                }
            }
        }
    }

    internal sealed class TakeWhileAwaitWithCancellation<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, CancellationToken, UniTask<bool>> predicate;

        public TakeWhileAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeWhileAwaitWithCancellation(source, predicate, cancellationToken);
        }

        class _TakeWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            Func<TSource, CancellationToken, UniTask<bool>> predicate;

            public _TakeWhileAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }
                else
                {
                    terminateIteration = true;
                    return false;
                }
            }
        }
    }

    internal sealed class TakeWhileIntAwaitWithCancellation<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, CancellationToken, UniTask<bool>> predicate;

        public TakeWhileIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeWhileIntAwaitWithCancellation(source, predicate, cancellationToken);
        }

        class _TakeWhileIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            readonly Func<TSource, int, CancellationToken, UniTask<bool>> predicate;
            int index;

            public _TakeWhileIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                return predicate(sourceCurrent, checked(index++), cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (awaitResult)
                {
                    Current = SourceCurrent;
                    terminateIteration = false;
                    return true;
                }
                else
                {
                    terminateIteration = true;
                    return false;
                }
            }
        }
    }
}