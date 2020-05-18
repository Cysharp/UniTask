using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> TakeLast<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Int32 count)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            // non take.
            if (count <= 0)
            {
                return Empty<TSource>();
            }

            return new TakeLast<TSource>(source, count);
        }
    }

    internal sealed class TakeLast<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly int count;

        public TakeLast(IUniTaskAsyncEnumerable<TSource> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TakeLast(source, count, cancellationToken);
        }

        sealed class _TakeLast : MoveNextSource, IUniTaskAsyncEnumerator<TSource>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly int count;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            Queue<TSource> queue;

            bool iterateCompleted;
            bool continueNext;

            public _TakeLast(IUniTaskAsyncEnumerable<TSource> source, int count, CancellationToken cancellationToken)
            {
                this.source = source;
                this.count = count;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken);
                    queue = new Queue<TSource>();
                }

                completionSource.Reset();
                SourceMoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void SourceMoveNext()
            {
                if (iterateCompleted)
                {
                    if (queue.Count > 0)
                    {
                        Current = queue.Dequeue();
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                        completionSource.TrySetResult(false);
                    }

                    return;
                }

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
                            goto LOOP; // avoid recursive
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
                var self = (_TakeLast)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        if (self.queue.Count < self.count)
                        {
                            self.queue.Enqueue(self.enumerator.Current);

                            if (!self.continueNext)
                            {
                                self.SourceMoveNext();
                            }
                        }
                        else
                        {
                            self.queue.Dequeue();
                            self.queue.Enqueue(self.enumerator.Current);

                            if (!self.continueNext)
                            {
                                self.SourceMoveNext();
                            }
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.iterateCompleted = true;
                        self.SourceMoveNext();
                    }
                }
                else
                {
                    self.continueNext = false;
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