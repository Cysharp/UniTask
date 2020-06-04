using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> SkipWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Boolean> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhile<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhile<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, Boolean> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileInt<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileAwait<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileIntAwait<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileAwaitWithCancellation<TSource>(source, predicate);
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipWhileAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<Boolean>> predicate)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(predicate, nameof(predicate));

            return new SkipWhileIntAwaitWithCancellation<TSource>(source, predicate);
        }
    }

    internal sealed class SkipWhile<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, bool> predicate;

        public SkipWhile(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipWhile(source, predicate, cancellationToken);
        }

        class _SkipWhile : AsyncEnumeratorBase<TSource, TSource>
        {
            Func<TSource, bool> predicate;

            public _SkipWhile(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (predicate == null || !predicate(SourceCurrent))
                    {
                        predicate = null;
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

    internal sealed class SkipWhileInt<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, bool> predicate;

        public SkipWhileInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipWhileInt(source, predicate, cancellationToken);
        }

        class _SkipWhileInt : AsyncEnumeratorBase<TSource, TSource>
        {
            Func<TSource, int, bool> predicate;
            int index;

            public _SkipWhileInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override bool TryMoveNextCore(bool sourceHasCurrent, out bool result)
            {
                if (sourceHasCurrent)
                {
                    if (predicate == null || !predicate(SourceCurrent, checked(index++)))
                    {
                        predicate = null;
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

    internal sealed class SkipWhileAwait<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<bool>> predicate;

        public SkipWhileAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipWhileAwait(source, predicate, cancellationToken);
        }

        class _SkipWhileAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            Func<TSource, UniTask<bool>> predicate;

            public _SkipWhileAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<bool>> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                if (!awaitResult)
                {
                    predicate = null;
                    Current = SourceCurrent;
                    terminateIteration= false;
                    return true;
                }
                else
                {
                    terminateIteration= false;
                    return false;
                }
            }
        }
    }

    internal sealed class SkipWhileIntAwait<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, UniTask<bool>> predicate;

        public SkipWhileIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipWhileIntAwait(source, predicate, cancellationToken);
        }

        class _SkipWhileIntAwait : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            Func<TSource, int, UniTask<bool>> predicate;
            int index;

            public _SkipWhileIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<bool>> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, checked(index++));
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                terminateIteration= false;
                if (!awaitResult)
                {
                    predicate = null;
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

    internal sealed class SkipWhileAwaitWithCancellation<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, CancellationToken, UniTask<bool>> predicate;

        public SkipWhileAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipWhileAwaitWithCancellation(source, predicate, cancellationToken);
        }

        class _SkipWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            Func<TSource, CancellationToken, UniTask<bool>> predicate;

            public _SkipWhileAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                terminateIteration= false;
                if (!awaitResult)
                {
                    predicate = null;
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

    internal sealed class SkipWhileIntAwaitWithCancellation<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, CancellationToken, UniTask<bool>> predicate;

        public SkipWhileIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<bool>> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipWhileIntAwaitWithCancellation(source, predicate, cancellationToken);
        }

        class _SkipWhileIntAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
        {
            Func<TSource, int, CancellationToken, UniTask<bool>> predicate;
            int index;

            public _SkipWhileIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)

                : base(source, cancellationToken)
            {
                this.predicate = predicate;
            }

            protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
            {
                if (predicate == null)
                {
                    return CompletedTasks.False;
                }

                return predicate(sourceCurrent, checked(index++), cancellationToken);
            }

            protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
            {
                terminateIteration= false;
                if (!awaitResult)
                {
                    predicate = null;
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