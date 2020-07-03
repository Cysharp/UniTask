using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> Select<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.Select<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> Select<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, TResult> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectInt<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectAwait<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectIntAwait<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectAwaitWithCancellation<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new Cysharp.Threading.Tasks.Linq.SelectIntAwaitWithCancellation<TSource, TResult>(source, selector);
        }
    }

    internal sealed class Select<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, TResult> selector;

        public Select(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Select(source, selector, cancellationToken);
        }

        sealed class _Select : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, TResult> selector;
            readonly CancellationToken cancellationToken;

            int state = -1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            Action moveNextAction;

            public _Select(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                this.moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (state == -2) return default;

                completionSource.Reset();
                MoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                Current = selector(enumerator.Current);
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
                return;
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectInt<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, TResult> selector;

        public SelectInt(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Select(source, selector, cancellationToken);
        }

        sealed class _Select : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, int, TResult> selector;
            readonly CancellationToken cancellationToken;

            int state = -1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            Action moveNextAction;
            int index;

            public _Select(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                this.moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (state == -2) return default;

                completionSource.Reset();
                MoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                Current = selector(enumerator.Current, checked(index++));
                                goto CONTINUE;
                            }
                            else
                            {
                                goto DONE;
                            }
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
                return;
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectAwait<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<TResult>> selector;

        public SelectAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectAwait(source, selector, cancellationToken);
        }

        sealed class _SelectAwait : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, UniTask<TResult>> selector;
            readonly CancellationToken cancellationToken;

            int state = -1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            UniTask<TResult>.Awaiter awaiter2;
            Action moveNextAction;

            public _SelectAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<TResult>> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                this.moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (state == -2) return default;

                completionSource.Reset();
                MoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }
                                else
                                {
                                    state = 2;
                                    awaiter2.UnsafeOnCompleted(moveNextAction);
                                    return;
                                }
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
                return;
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectIntAwait<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, UniTask<TResult>> selector;

        public SelectIntAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectAwait(source, selector, cancellationToken);
        }

        sealed class _SelectAwait : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, int, UniTask<TResult>> selector;
            readonly CancellationToken cancellationToken;

            int state = -1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            UniTask<TResult>.Awaiter awaiter2;
            Action moveNextAction;
            int index;

            public _SelectAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<TResult>> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                this.moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (state == -2) return default;

                completionSource.Reset();
                MoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current, checked(index++)).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }
                                else
                                {
                                    state = 2;
                                    awaiter2.UnsafeOnCompleted(moveNextAction);
                                    return;
                                }
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
                return;
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectAwaitWithCancellation<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, CancellationToken, UniTask<TResult>> selector;

        public SelectAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectAwaitWithCancellation(source, selector, cancellationToken);
        }

        sealed class _SelectAwaitWithCancellation : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, CancellationToken, UniTask<TResult>> selector;
            readonly CancellationToken cancellationToken;

            int state = -1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            UniTask<TResult>.Awaiter awaiter2;
            Action moveNextAction;

            public _SelectAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                this.moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (state == -2) return default;

                completionSource.Reset();
                MoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current, cancellationToken).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }
                                else
                                {
                                    state = 2;
                                    awaiter2.UnsafeOnCompleted(moveNextAction);
                                    return;
                                }
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
                return;
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }

    internal sealed class SelectIntAwaitWithCancellation<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, int, CancellationToken, UniTask<TResult>> selector;

        public SelectIntAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectAwaitWithCancellation(source, selector, cancellationToken);
        }

        sealed class _SelectAwaitWithCancellation : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, int, CancellationToken, UniTask<TResult>> selector;
            readonly CancellationToken cancellationToken;

            int state = -1;
            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;
            UniTask<TResult>.Awaiter awaiter2;
            Action moveNextAction;
            int index;

            public _SelectAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<TResult>> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
                this.moveNextAction = MoveNext;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                if (state == -2) return default;

                completionSource.Reset();
                MoveNext();
                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNext()
            {
                try
                {
                    switch (state)
                    {
                        case -1: // init
                            enumerator = source.GetAsyncEnumerator(cancellationToken);
                            goto case 0;
                        case 0:
                            awaiter = enumerator.MoveNextAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto case 1;
                            }
                            else
                            {
                                state = 1;
                                awaiter.UnsafeOnCompleted(moveNextAction);
                                return;
                            }
                        case 1:
                            if (awaiter.GetResult())
                            {
                                awaiter2 = selector(enumerator.Current, checked(index++), cancellationToken).GetAwaiter();
                                if (awaiter2.IsCompleted)
                                {
                                    goto case 2;
                                }
                                else
                                {
                                    state = 2;
                                    awaiter2.UnsafeOnCompleted(moveNextAction);
                                    return;
                                }
                            }
                            else
                            {
                                goto DONE;
                            }
                        case 2:
                            Current = awaiter2.GetResult();
                            goto CONTINUE;
                        default:
                            goto DONE;
                    }
                }
                catch (Exception ex)
                {
                    state = -2;
                    completionSource.TrySetException(ex);
                    return;
                }

                DONE:
                state = -2;
                completionSource.TrySetResult(false);
                return;

                CONTINUE:
                state = 0;
                completionSource.TrySetResult(true);
                return;
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                return enumerator.DisposeAsync();
            }
        }
    }
}