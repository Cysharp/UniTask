using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<AsyncUnit> EveryUpdate(PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            return new EveryUpdate(updateTiming);
        }
    }

    internal class EveryUpdate : IUniTaskAsyncEnumerable<AsyncUnit>
    {
        readonly PlayerLoopTiming updateTiming;

        public EveryUpdate(PlayerLoopTiming updateTiming)
        {
            this.updateTiming = updateTiming;
        }

        public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(updateTiming, cancellationToken);
        }

        class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            readonly PlayerLoopTiming updateTiming;
            CancellationToken cancellationToken;

            bool disposed;

            public Enumerator(PlayerLoopTiming updateTiming, CancellationToken cancellationToken)
            {
                this.updateTiming = updateTiming;

                TaskTracker.TrackActiveTask(this, 2);
                PlayerLoopHelper.AddAction(updateTiming, this);
            }

            public AsyncUnit Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested) return CompletedTasks.False;

                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }
                return default;
            }

            public bool MoveNext()
            {
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetResult(false);
                    return false;
                }

                completionSource.TrySetResult(true);
                return true;
            }
        }
    }
}