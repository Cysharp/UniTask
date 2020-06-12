using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> ToUniTaskAsyncEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToUniTaskAsyncEnumerable<TSource>(source);
        }

        public static IUniTaskAsyncEnumerable<TSource> ToUniTaskAsyncEnumerable<TSource>(this Task<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToUniTaskAsyncEnumerableTask<TSource>(source);
        }

        public static IUniTaskAsyncEnumerable<TSource> ToUniTaskAsyncEnumerable<TSource>(this UniTask<TSource> source)
        {
            return new ToUniTaskAsyncEnumerableUniTask<TSource>(source);
        }

        public static IUniTaskAsyncEnumerable<TSource> ToUniTaskAsyncEnumerable<TSource>(this IObservable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToUniTaskAsyncEnumerableObservable<TSource>(source);
        }
    }

    internal class ToUniTaskAsyncEnumerable<T> : IUniTaskAsyncEnumerable<T>
    {
        readonly IEnumerable<T> source;

        public ToUniTaskAsyncEnumerable(IEnumerable<T> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToUniTaskAsyncEnumerable(source, cancellationToken);
        }

        class _ToUniTaskAsyncEnumerable : IUniTaskAsyncEnumerator<T>
        {
            readonly IEnumerable<T> source;
            CancellationToken cancellationToken;

            IEnumerator<T> enumerator;

            public _ToUniTaskAsyncEnumerable(IEnumerable<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
            }

            public T Current => enumerator.Current;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (enumerator == null)
                {
                    enumerator = source.GetEnumerator();
                }

                if (enumerator.MoveNext())
                {
                    return CompletedTasks.True;
                }

                return CompletedTasks.False;
            }

            public UniTask DisposeAsync()
            {
                enumerator.Dispose();
                return default;
            }
        }
    }

    internal class ToUniTaskAsyncEnumerableTask<T> : IUniTaskAsyncEnumerable<T>
    {
        readonly Task<T> source;

        public ToUniTaskAsyncEnumerableTask(Task<T> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToUniTaskAsyncEnumerableTask(source, cancellationToken);
        }

        class _ToUniTaskAsyncEnumerableTask : IUniTaskAsyncEnumerator<T>
        {
            readonly Task<T> source;
            CancellationToken cancellationToken;

            T current;
            bool called;

            public _ToUniTaskAsyncEnumerableTask(Task<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;

                this.called = false;
            }

            public T Current => current;

            public async UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (called)
                {
                    return false;
                }
                called = true;

                current = await source;
                return true;
            }

            public UniTask DisposeAsync()
            {
                return default;
            }
        }
    }

    internal class ToUniTaskAsyncEnumerableUniTask<T> : IUniTaskAsyncEnumerable<T>
    {
        readonly UniTask<T> source;

        public ToUniTaskAsyncEnumerableUniTask(UniTask<T> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToUniTaskAsyncEnumerableUniTask(source, cancellationToken);
        }

        class _ToUniTaskAsyncEnumerableUniTask : IUniTaskAsyncEnumerator<T>
        {
            readonly UniTask<T> source;
            CancellationToken cancellationToken;

            T current;
            bool called;

            public _ToUniTaskAsyncEnumerableUniTask(UniTask<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;

                this.called = false;
            }

            public T Current => current;

            public async UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (called)
                {
                    return false;
                }
                called = true;

                current = await source;
                return true;
            }

            public UniTask DisposeAsync()
            {
                return default;
            }
        }
    }

    internal class ToUniTaskAsyncEnumerableObservable<T> : IUniTaskAsyncEnumerable<T>
    {
        readonly IObservable<T> source;

        public ToUniTaskAsyncEnumerableObservable(IObservable<T> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _ToUniTaskAsyncEnumerableObservable(source, cancellationToken);
        }

        class _ToUniTaskAsyncEnumerableObservable : MoveNextSource, IUniTaskAsyncEnumerator<T>, IObserver<T>
        {
            static readonly Action<object> OnCanceledDelegate = OnCanceled;

            readonly IObservable<T> source;
            CancellationToken cancellationToken;


            bool useCachedCurrent;
            T current;
            bool subscribeCompleted;
            readonly Queue<T> queuedResult;
            Exception error;
            IDisposable subscription;
            CancellationTokenRegistration cancellationTokenRegistration;

            public _ToUniTaskAsyncEnumerableObservable(IObservable<T> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
                this.queuedResult = new Queue<T>();

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(OnCanceledDelegate, this);
                }
            }

            public T Current
            {
                get
                {
                    if (useCachedCurrent)
                    {
                        return current;
                    }

                    lock (queuedResult)
                    {
                        if (queuedResult.Count != 0)
                        {
                            current = queuedResult.Dequeue();
                            useCachedCurrent = true;
                            return current;
                        }
                        else
                        {
                            return default; // undefined.
                        }
                    }
                }
            }

            public UniTask<bool> MoveNextAsync()
            {
                lock (queuedResult)
                {
                    useCachedCurrent = false;

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return UniTask.FromCanceled<bool>(cancellationToken);
                    }

                    if (subscription == null)
                    {
                        subscription = source.Subscribe(this);
                    }

                    if (error != null)
                    {
                        return UniTask.FromException<bool>(error);
                    }

                    if (queuedResult.Count != 0)
                    {
                        return CompletedTasks.True;
                    }

                    if (subscribeCompleted)
                    {
                        return CompletedTasks.False;
                    }

                    completionSource.Reset();
                    return new UniTask<bool>(this, completionSource.Version);
                }
            }

            public UniTask DisposeAsync()
            {
                subscription.Dispose();
                cancellationTokenRegistration.Dispose();
                completionSource.Reset();
                return default;
            }

            public void OnCompleted()
            {
                lock (queuedResult)
                {
                    subscribeCompleted = true;
                    completionSource.TrySetResult(false);
                }
            }

            public void OnError(Exception error)
            {
                lock (queuedResult)
                {
                    this.error = error;
                    completionSource.TrySetException(error);
                }
            }

            public void OnNext(T value)
            {
                lock (queuedResult)
                {
                    queuedResult.Enqueue(value);
                    completionSource.TrySetResult(true); // include callback execution, too long lock?
                }
            }

            static void OnCanceled(object state)
            {
                var self = (_ToUniTaskAsyncEnumerableObservable)state;
                lock (self.queuedResult)
                {
                    self.completionSource.TrySetCanceled(self.cancellationToken);
                }
            }
        }
    }
}































































































































































































































































































































































































































































































































































































































































































































































































