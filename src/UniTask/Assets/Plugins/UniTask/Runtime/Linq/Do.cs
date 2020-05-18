using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Internal;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> Do<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return source.Do(onNext, null, null);
        }

        public static IUniTaskAsyncEnumerable<TSource> Do<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return source.Do(onNext, onError, null);
        }

        public static IUniTaskAsyncEnumerable<TSource> Do<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext, Action onCompleted)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return source.Do(onNext, null, onCompleted);
        }

        public static IUniTaskAsyncEnumerable<TSource> Do<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            return new Do<TSource>(source, onNext, onError, onCompleted);
        }

        public static IUniTaskAsyncEnumerable<TSource> Do<TSource>(this IUniTaskAsyncEnumerable<TSource> source, IObserver<TSource> observer)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(observer, nameof(observer));

            return source.Do(observer.OnNext, observer.OnError, observer.OnCompleted); // alloc delegate.
        }

        // not yet impl.

        //public static IUniTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask> onNext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask> onNext, Func<Exception, UniTask> onError)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask> onNext, Func<UniTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwait<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask> onNext, Func<Exception, UniTask> onError, Func<UniTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask> onNext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask> onNext, Func<Exception, CancellationToken, UniTask> onError)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask> onNext, Func<CancellationToken, UniTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}

        //public static IUniTaskAsyncEnumerable<TSource> DoAwaitWithCancellation<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask> onNext, Func<Exception, CancellationToken, UniTask> onError, Func<CancellationToken, UniTask> onCompleted)
        //{
        //    throw new NotImplementedException();
        //}
    }

    internal sealed class Do<TSource> : IUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Action<TSource> onNext;
        readonly Action<Exception> onError;
        readonly Action onCompleted;

        public Do(IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
        {
            this.source = source;
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Do(source, onNext, onError, onCompleted, cancellationToken);
        }

        sealed class _Do : MoveNextSource, IUniTaskAsyncEnumerator<TSource>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Action<TSource> onNext;
            readonly Action<Exception> onError;
            readonly Action onCompleted;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TSource> enumerator;
            UniTask<bool>.Awaiter awaiter;

            public _Do(IUniTaskAsyncEnumerable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted, CancellationToken cancellationToken)
            {
                this.source = source;
                this.onNext = onNext;
                this.onError = onError;
                this.onCompleted = onCompleted;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }


            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                bool isCompleted = false;
                try
                {
                    if (enumerator == null)
                    {
                        enumerator = source.GetAsyncEnumerator(cancellationToken);
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    isCompleted = awaiter.IsCompleted;
                }
                catch (Exception ex)
                {
                    CallTrySetExceptionAfterNotification(ex);
                    return new UniTask<bool>(this, completionSource.Version);
                }

                if (isCompleted)
                {
                    MoveNextCore(this);
                }
                else
                {
                    awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            void CallTrySetExceptionAfterNotification(Exception ex)
            {
                if (onError != null)
                {
                    try
                    {
                        onError(ex);
                    }
                    catch (Exception ex2)
                    {
                        completionSource.TrySetException(ex2);
                        return;
                    }
                }

                completionSource.TrySetException(ex);
            }

            bool TryGetResultWithNotification<T>(UniTask<T>.Awaiter awaiter, out T result)
            {
                try
                {
                    result = awaiter.GetResult();
                    return true;
                }
                catch (Exception ex)
                {
                    CallTrySetExceptionAfterNotification(ex);
                    result = default;
                    return false;
                }
            }


            static void MoveNextCore(object state)
            {
                var self = (_Do)state;

                if (self.TryGetResultWithNotification(self.awaiter, out var result))
                {
                    if (result)
                    {
                        var v = self.enumerator.Current;

                        if (self.onNext != null)
                        {
                            try
                            {
                                self.onNext(v);
                            }
                            catch (Exception ex)
                            {
                                self.CallTrySetExceptionAfterNotification(ex);
                            }
                        }

                        self.Current = v;
                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        if (self.onCompleted != null)
                        {
                            try
                            {
                                self.onCompleted();
                            }
                            catch (Exception ex)
                            {
                                self.CallTrySetExceptionAfterNotification(ex);
                                return;
                            }
                        }

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
