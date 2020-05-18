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
            return new _Take(source, count, cancellationToken);
        }

        sealed class _Take : MoveNextSource, IUniTaskAsyncEnumerator<TSource>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly int count;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            int index;

            public _Take(IUniTaskAsyncEnumerable<TSource> source, int count, CancellationToken cancellationToken)
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
                }

                if (checked(index) >= count)
                {
                    return CompletedTasks.False;
                }

                completionSource.Reset();
                SourceMoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void SourceMoveNext()
            {
                try
                {
                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        MoveNextCore(this);
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
                var self = (_Take)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.index++;
                        self.Current = self.enumerator.Current;
                        self.completionSource.TrySetResult(true);
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