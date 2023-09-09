using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<T> Merge<T>(this IUniTaskAsyncEnumerable<T> first, IUniTaskAsyncEnumerable<T> second)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));

            return new Merge<T>(new [] { first, second });
        }

        public static IUniTaskAsyncEnumerable<T> Merge<T>(this IUniTaskAsyncEnumerable<T> first, IUniTaskAsyncEnumerable<T> second, IUniTaskAsyncEnumerable<T> third)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));
            Error.ThrowArgumentNullException(third, nameof(third));

            return new Merge<T>(new[] { first, second, third });
        }

        public static IUniTaskAsyncEnumerable<T> Merge<T>(this IEnumerable<IUniTaskAsyncEnumerable<T>> sources)
        {
            return new Merge<T>(sources.ToArray());
        }

        public static IUniTaskAsyncEnumerable<T> Merge<T>(params IUniTaskAsyncEnumerable<T>[] sources)
        {
            return new Merge<T>(sources);
        }
    }

    internal sealed class Merge<T> : IUniTaskAsyncEnumerable<T>
    {
        readonly IUniTaskAsyncEnumerable<T>[] sources;

        public Merge(IUniTaskAsyncEnumerable<T>[] sources)
        {
            if (sources.Length <= 0)
            {
                Error.ThrowArgumentException("No source async enumerable to merge");
            }
            this.sources = sources;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => new _Merge(sources, cancellationToken);

        enum MergeSourceState
        {
            Pending,
            Running,
            Completed,
        }

        sealed class _Merge : MoveNextSource, IUniTaskAsyncEnumerator<T>
        {
            static readonly Action<object> GetResultAtAction = GetResultAt;

            readonly int length;
            readonly IUniTaskAsyncEnumerator<T>[] enumerators;
            readonly MergeSourceState[] states;
            readonly Queue<(T, Exception)> queuedResult = new Queue<(T, Exception)>();
            readonly CancellationToken cancellationToken;

            public T Current { get; private set; }

            public _Merge(IUniTaskAsyncEnumerable<T>[] sources, CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
                length = sources.Length;
                states = ArrayPool<MergeSourceState>.Shared.Rent(length);
                enumerators = ArrayPool<IUniTaskAsyncEnumerator<T>>.Shared.Rent(length);
                for (var i = 0; i < length; i++)
                {
                    enumerators[i] = sources[i].GetAsyncEnumerator(cancellationToken);
                    states[i] = MergeSourceState.Pending;
                }
            }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                lock (queuedResult)
                {
                    if (queuedResult.Count > 0)
                    {
                        var result = queuedResult.Dequeue();
                        var queuedValue = result.Item1;
                        var queuedException = result.Item2;

                        if (queuedException != null)
                        {
                            completionSource.TrySetException(queuedException);
                        }
                        else
                        {
                            Current = queuedValue;
                            completionSource.TrySetResult(!IsCompletedAll());
                        }
                        return new UniTask<bool>(this, completionSource.Version);
                    }
                }

                for (var i = 0; i < length; i++)
                {
                    lock (states)
                    {
                        if (states[i] != MergeSourceState.Pending)
                        {
                            continue;
                        }
                        states[i] = MergeSourceState.Running;
                    }

                    var awaiter = enumerators[i].MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        GetResultAt(i, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(GetResultAtAction, StateTuple.Create(this, i, awaiter));
                    }
                }
                return new UniTask<bool>(this, completionSource.Version);
            }

            public async UniTask DisposeAsync()
            {
                for (var i = 0; i < length; i++)
                {
                    await enumerators[i].DisposeAsync();
                }

                ArrayPool<MergeSourceState>.Shared.Return(states, true);
                ArrayPool<IUniTaskAsyncEnumerator<T>>.Shared.Return(enumerators, true);
            }

            static void GetResultAt(object state)
            {
                var tuple = (StateTuple<_Merge, int, UniTask<bool>.Awaiter>)state;
                tuple.Item1.GetResultAt(tuple.Item2, tuple.Item3);
            }

            void GetResultAt(int index, UniTask<bool>.Awaiter awaiter)
            {
                bool hasNext;
                try
                {
                    hasNext = awaiter.GetResult();
                    lock (states)
                    {
                        states[index] = hasNext ? MergeSourceState.Pending : MergeSourceState.Completed;
                    }
                }
                catch (Exception ex)
                {
                    if (!completionSource.TrySetException(ex))
                    {
                        lock (queuedResult)
                        {
                            queuedResult.Enqueue((default, ex));
                        }
                    }
                    return;
                }

                var completedAll = IsCompletedAll();
                if (hasNext || completedAll)
                {
                    lock (queuedResult)
                    {
                        if (completionSource.GetStatus(completionSource.Version).IsCompleted())
                        {
                            queuedResult.Enqueue((enumerators[index].Current, null));
                        }
                        else
                        {
                            Current = enumerators[index].Current;
                            completionSource.TrySetResult(!completedAll);
                        }
                    }
                }
            }

            bool IsCompletedAll()
            {
                lock (states)
                {
                    for (var i = 0; i < length; i++)
                    {
                        if (states[i] != MergeSourceState.Completed)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
    }
}