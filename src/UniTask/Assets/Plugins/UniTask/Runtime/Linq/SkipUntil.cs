using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> SkipUntil<TSource>(this IUniTaskAsyncEnumerable<TSource> source, UniTask other)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new SkipUntil<TSource>(source, other, null);
        }

        public static IUniTaskAsyncEnumerable<TSource> SkipUntil<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<CancellationToken, UniTask> other)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(source, nameof(other));

            return new SkipUntil<TSource>(source, default, other);
        }
    }

    internal sealed class SkipUntil<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly UniTask other;
        readonly Func<CancellationToken, UniTask> other2;

        public SkipUntil(IUniTaskAsyncEnumerable<TSource> source, UniTask other, Func<CancellationToken, UniTask> other2)
        {
            this.source = source;
            this.other = other;
            this.other2 = other2;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            if (other2 != null)
            {
                return new _SkipUntil(source, this.other2(cancellationToken), cancellationToken);
            }
            else
            {
                return new _SkipUntil(source, this.other, cancellationToken);
            }
        }

        sealed class _SkipUntil : MoveNextSource, IUniTaskAsyncEnumerator<TSource>
        {
            static readonly Action<object> CancelDelegate1 = OnCanceled1;
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            CancellationToken cancellationToken1;

            bool completed;
            CancellationTokenRegistration cancellationTokenRegistration1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            bool continueNext;
            Exception exception;

            public _SkipUntil(IUniTaskAsyncEnumerable<TSource> source, UniTask other, CancellationToken cancellationToken1)
            {
                this.source = source;
                this.cancellationToken1 = cancellationToken1;
                if (cancellationToken1.CanBeCanceled)
                {
                    this.cancellationTokenRegistration1 = cancellationToken1.RegisterWithoutCaptureExecutionContext(CancelDelegate1, this);
                }

                TaskTracker.TrackActiveTask(this, 3);
                RunOther(other).Forget();
            }

            public TSource Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (exception != null)
                {
                    return UniTask.FromException<bool>(exception);
                }

                if (cancellationToken1.IsCancellationRequested)
                {
                    return UniTask.FromCanceled<bool>(cancellationToken1);
                }

                if (enumerator == null)
                {
                    enumerator = source.GetAsyncEnumerator(cancellationToken1);
                }
                completionSource.Reset();

                if (completed)
                {
                    SourceMoveNext();
                }
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
                var self = (_SkipUntil)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.Current = self.enumerator.Current;
                        self.completionSource.TrySetResult(true);
                        if (self.continueNext)
                        {
                            self.SourceMoveNext();
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            async UniTaskVoid RunOther(UniTask other)
            {
                try
                {
                    await other;
                    completed = true;
                    SourceMoveNext();
                }
                catch (Exception ex)
                {
                    exception = ex;
                    completionSource.TrySetException(ex);
                }
            }

            static void OnCanceled1(object state)
            {
                var self = (_SkipUntil)state;
                self.completionSource.TrySetCanceled(self.cancellationToken1);
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                cancellationTokenRegistration1.Dispose();
                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }
                return default;
            }
        }
    }
}