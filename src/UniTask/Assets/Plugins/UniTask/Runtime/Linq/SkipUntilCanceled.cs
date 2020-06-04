using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> SkipUntilCanceled<TSource>(this IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new SkipUntilCanceled<TSource>(source, cancellationToken);
        }
    }

    internal sealed class SkipUntilCanceled<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly CancellationToken cancellationToken;

        public SkipUntilCanceled(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SkipUntilCanceled(source, this.cancellationToken, cancellationToken);
        }

        sealed class _SkipUntilCanceled : MoveNextSource, IUniTaskAsyncEnumerator<TSource>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            CancellationToken cancellationToken1;
            CancellationToken cancellationToken2;

            bool isCanceled;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            bool continueNext;

            public _SkipUntilCanceled(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
            {
                this.source = source;
                this.cancellationToken1 = cancellationToken1;
                this.cancellationToken2 = cancellationToken2;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (cancellationToken1.IsCancellationRequested) isCanceled = true;
                if (cancellationToken2.IsCancellationRequested) isCanceled = true;

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken2); // use only AsyncEnumerator provided token.
                }
                completionSource.Reset();
                SourceMoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void SourceMoveNext()
            {
                try
                {
                    LOOP:
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP;
                        }
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }

            static void MoveNextCore(object state)
            {
                var self = (_SkipUntilCanceled)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        AGAIN:

                        if (self.isCanceled)
                        {
                            self.continueNext = false;
                            self.Current = self.enumerator.Current;
                            self.completionSource.TrySetResult(true);
                        }
                        else
                        {
                            if (self.cancellationToken1.IsCancellationRequested) self.isCanceled = true;
                            if (self.cancellationToken2.IsCancellationRequested) self.isCanceled = true;

                            if (self.isCanceled) goto AGAIN;

                            if (!self.continueNext)
                            {
                                self.SourceMoveNext();
                            }
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }
                return default;
            }
        }
    }
}