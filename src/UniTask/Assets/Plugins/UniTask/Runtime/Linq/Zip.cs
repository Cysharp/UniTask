using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {

        public static IUniTaskAsyncEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));

            return Zip(first, second, (x, y) => (x, y));
        }

        public static IUniTaskAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new Zip<TFirst, TSecond, TResult>(first, second, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> ZipAwait<TFirst, TSecond, TResult>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new ZipAwait<TFirst, TSecond, TResult>(first, second, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> ZipAwaitWithCancellation<TFirst, TSecond, TResult>(this IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(first, nameof(first));
            Error.ThrowArgumentNullException(second, nameof(second));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new ZipAwaitWithCancellation<TFirst, TSecond, TResult>(first, second, selector);
        }
    }

    internal sealed class Zip<TFirst, TSecond, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TFirst> first;
        readonly IUniTaskAsyncEnumerable<TSecond> second;
        readonly Func<TFirst, TSecond, TResult> resultSelector;

        public Zip(IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            this.first = first;
            this.second = second;
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Zip(first, second, resultSelector, cancellationToken);
        }

        sealed class _Zip : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> firstMoveNextCoreDelegate = FirstMoveNextCore;
            static readonly Action<object> secondMoveNextCoreDelegate = SecondMoveNextCore;

            readonly IUniTaskAsyncEnumerable<TFirst> first;
            readonly IUniTaskAsyncEnumerable<TSecond> second;
            readonly Func<TFirst, TSecond, TResult> resultSelector;

            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TFirst> firstEnumerator;
            IUniTaskAsyncEnumerator<TSecond> secondEnumerator;

            UniTask<bool>.Awaiter firstAwaiter;
            UniTask<bool>.Awaiter secondAwaiter;

            public _Zip(IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.first = first;
                this.second = second;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                if (firstEnumerator == null)
                {
                    firstEnumerator = first.GetAsyncEnumerator(cancellationToken);
                    secondEnumerator = second.GetAsyncEnumerator(cancellationToken);
                }

                firstAwaiter = firstEnumerator.MoveNextAsync().GetAwaiter();

                if (firstAwaiter.IsCompleted)
                {
                    FirstMoveNextCore(this);
                }
                else
                {
                    firstAwaiter.SourceOnCompleted(firstMoveNextCoreDelegate, this);
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void FirstMoveNextCore(object state)
            {
                var self = (_Zip)state;

                if (self.TryGetResult(self.firstAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.secondAwaiter = self.secondEnumerator.MoveNextAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        if (self.secondAwaiter.IsCompleted)
                        {
                            SecondMoveNextCore(self);
                        }
                        else
                        {
                            self.secondAwaiter.SourceOnCompleted(secondMoveNextCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void SecondMoveNextCore(object state)
            {
                var self = (_Zip)state;

                if (self.TryGetResult(self.secondAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.Current = self.resultSelector(self.firstEnumerator.Current, self.secondEnumerator.Current);
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }

                        if (self.cancellationToken.IsCancellationRequested)
                        {
                            self.completionSource.TrySetCanceled(self.cancellationToken);
                        }
                        else
                        {
                            self.completionSource.TrySetResult(true);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (firstEnumerator != null)
                {
                    await firstEnumerator.DisposeAsync();
                }
                if (secondEnumerator != null)
                {
                    await secondEnumerator.DisposeAsync();
                }
            }
        }
    }

    internal sealed class ZipAwait<TFirst, TSecond, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TFirst> first;
        readonly IUniTaskAsyncEnumerable<TSecond> second;
        readonly Func<TFirst, TSecond, UniTask<TResult>> resultSelector;

        public ZipAwait(IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, UniTask<TResult>> resultSelector)
        {
            this.first = first;
            this.second = second;
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ZipAwait(first, second, resultSelector, cancellationToken);
        }

        sealed class _ZipAwait : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> firstMoveNextCoreDelegate = FirstMoveNextCore;
            static readonly Action<object> secondMoveNextCoreDelegate = SecondMoveNextCore;
            static readonly Action<object> resultAwaitCoreDelegate = ResultAwaitCore;

            readonly IUniTaskAsyncEnumerable<TFirst> first;
            readonly IUniTaskAsyncEnumerable<TSecond> second;
            readonly Func<TFirst, TSecond, UniTask<TResult>> resultSelector;

            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TFirst> firstEnumerator;
            IUniTaskAsyncEnumerator<TSecond> secondEnumerator;

            UniTask<bool>.Awaiter firstAwaiter;
            UniTask<bool>.Awaiter secondAwaiter;
            UniTask<TResult>.Awaiter resultAwaiter;

            public _ZipAwait(IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, UniTask<TResult>> resultSelector, CancellationToken cancellationToken)
            {
                this.first = first;
                this.second = second;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                if (firstEnumerator == null)
                {
                    firstEnumerator = first.GetAsyncEnumerator(cancellationToken);
                    secondEnumerator = second.GetAsyncEnumerator(cancellationToken);
                }

                firstAwaiter = firstEnumerator.MoveNextAsync().GetAwaiter();

                if (firstAwaiter.IsCompleted)
                {
                    FirstMoveNextCore(this);
                }
                else
                {
                    firstAwaiter.SourceOnCompleted(firstMoveNextCoreDelegate, this);
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void FirstMoveNextCore(object state)
            {
                var self = (_ZipAwait)state;

                if (self.TryGetResult(self.firstAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.secondAwaiter = self.secondEnumerator.MoveNextAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        if (self.secondAwaiter.IsCompleted)
                        {
                            SecondMoveNextCore(self);
                        }
                        else
                        {
                            self.secondAwaiter.SourceOnCompleted(secondMoveNextCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void SecondMoveNextCore(object state)
            {
                var self = (_ZipAwait)state;

                if (self.TryGetResult(self.secondAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.resultAwaiter = self.resultSelector(self.firstEnumerator.Current, self.secondEnumerator.Current).GetAwaiter();
                            if (self.resultAwaiter.IsCompleted)
                            {
                                ResultAwaitCore(self);
                            }
                            else
                            {
                                self.resultAwaiter.SourceOnCompleted(resultAwaitCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void ResultAwaitCore(object state)
            {
                var self = (_ZipAwait)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;

                    if (self.cancellationToken.IsCancellationRequested)
                    {
                        self.completionSource.TrySetCanceled(self.cancellationToken);
                    }
                    else
                    {
                        self.completionSource.TrySetResult(true);
                    }
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (firstEnumerator != null)
                {
                    await firstEnumerator.DisposeAsync();
                }
                if (secondEnumerator != null)
                {
                    await secondEnumerator.DisposeAsync();
                }
            }
        }
    }

    internal sealed class ZipAwaitWithCancellation<TFirst, TSecond, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TFirst> first;
        readonly IUniTaskAsyncEnumerable<TSecond> second;
        readonly Func<TFirst, TSecond, CancellationToken, UniTask<TResult>> resultSelector;

        public ZipAwaitWithCancellation(IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, UniTask<TResult>> resultSelector)
        {
            this.first = first;
            this.second = second;
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ZipAwaitWithCancellation(first, second, resultSelector, cancellationToken);
        }

        sealed class _ZipAwaitWithCancellation : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> firstMoveNextCoreDelegate = FirstMoveNextCore;
            static readonly Action<object> secondMoveNextCoreDelegate = SecondMoveNextCore;
            static readonly Action<object> resultAwaitCoreDelegate = ResultAwaitCore;

            readonly IUniTaskAsyncEnumerable<TFirst> first;
            readonly IUniTaskAsyncEnumerable<TSecond> second;
            readonly Func<TFirst, TSecond, CancellationToken, UniTask<TResult>> resultSelector;

            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TFirst> firstEnumerator;
            IUniTaskAsyncEnumerator<TSecond> secondEnumerator;

            UniTask<bool>.Awaiter firstAwaiter;
            UniTask<bool>.Awaiter secondAwaiter;
            UniTask<TResult>.Awaiter resultAwaiter;

            public _ZipAwaitWithCancellation(IUniTaskAsyncEnumerable<TFirst> first, IUniTaskAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, UniTask<TResult>> resultSelector, CancellationToken cancellationToken)
            {
                this.first = first;
                this.second = second;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                if (firstEnumerator == null)
                {
                    firstEnumerator = first.GetAsyncEnumerator(cancellationToken);
                    secondEnumerator = second.GetAsyncEnumerator(cancellationToken);
                }

                firstAwaiter = firstEnumerator.MoveNextAsync().GetAwaiter();

                if (firstAwaiter.IsCompleted)
                {
                    FirstMoveNextCore(this);
                }
                else
                {
                    firstAwaiter.SourceOnCompleted(firstMoveNextCoreDelegate, this);
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void FirstMoveNextCore(object state)
            {
                var self = (_ZipAwaitWithCancellation)state;

                if (self.TryGetResult(self.firstAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.secondAwaiter = self.secondEnumerator.MoveNextAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        if (self.secondAwaiter.IsCompleted)
                        {
                            SecondMoveNextCore(self);
                        }
                        else
                        {
                            self.secondAwaiter.SourceOnCompleted(secondMoveNextCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void SecondMoveNextCore(object state)
            {
                var self = (_ZipAwaitWithCancellation)state;

                if (self.TryGetResult(self.secondAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.resultAwaiter = self.resultSelector(self.firstEnumerator.Current, self.secondEnumerator.Current, self.cancellationToken).GetAwaiter();
                            if (self.resultAwaiter.IsCompleted)
                            {
                                ResultAwaitCore(self);
                            }
                            else
                            {
                                self.resultAwaiter.SourceOnCompleted(resultAwaitCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void ResultAwaitCore(object state)
            {
                var self = (_ZipAwaitWithCancellation)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;

                    if (self.cancellationToken.IsCancellationRequested)
                    {
                        self.completionSource.TrySetCanceled(self.cancellationToken);
                    }
                    else
                    {
                        self.completionSource.TrySetResult(true);
                    }
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (firstEnumerator != null)
                {
                    await firstEnumerator.DisposeAsync();
                }
                if (secondEnumerator != null)
                {
                    await secondEnumerator.DisposeAsync();
                }
            }
        }
    }
}