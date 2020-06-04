using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new DefaultIfEmpty<TSource>(source, default);
        }

        public static IUniTaskAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(this IUniTaskAsyncEnumerable<TSource> source, TSource defaultValue)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new DefaultIfEmpty<TSource>(source, defaultValue);
        }
    }

    internal sealed class DefaultIfEmpty<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly TSource defaultValue;

        public DefaultIfEmpty(IUniTaskAsyncEnumerable<TSource> source, TSource defaultValue)
        {
            this.source = source;
            this.defaultValue = defaultValue;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _DefaultIfEmpty(source, defaultValue, cancellationToken);
        }

        sealed class _DefaultIfEmpty : MoveNextSource, IUniTaskAsyncEnumerator<TSource>
        {
            enum IteratingState : byte
            {
                Empty,
                Iterating,
                Completed
            }

            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly TSource defaultValue;
            CancellationToken cancellationToken;

            IteratingState iteratingState;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;

            public _DefaultIfEmpty(IUniTaskAsyncEnumerable<TSource> source, TSource defaultValue, CancellationToken cancellationToken)
            {
                this.source = source;
                this.defaultValue = defaultValue;
                this.cancellationToken = cancellationToken;

                this.iteratingState = IteratingState.Empty;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }


            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (iteratingState == IteratingState.Completed)
                {
                    return CompletedTasks.False;
                }

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken);
                }

                awaiter = enumerator.MoveNextAsync().GetAwaiter();

                if (awaiter.IsCompleted)
                {
                    MoveNextCore(this);
                }
                else
                {
                    awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void MoveNextCore(object state)
            {
                var self = (_DefaultIfEmpty)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.iteratingState = IteratingState.Iterating;
                        self.Current = self.enumerator.Current;
                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        if (self.iteratingState == IteratingState.Empty)
                        {
                            self.iteratingState = IteratingState.Completed;

                            self.Current = self.defaultValue;
                            self.completionSource.TrySetResult(true);
                        }
                        else
                        {
                            self.completionSource.TrySetResult(false);
                        }
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
