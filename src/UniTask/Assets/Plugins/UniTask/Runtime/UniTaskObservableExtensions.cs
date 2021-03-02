#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskObservableExtensions
    {
        public static UniTask<T> ToUniTask<T>(this IObservable<T> source, bool useFirstValue = false, CancellationToken cancellationToken = default)
        {
            var promise = new UniTaskCompletionSource<T>();
            var disposable = new SingleAssignmentDisposable();

            var observer = useFirstValue
                ? (IObserver<T>)new FirstValueToUniTaskObserver<T>(promise, disposable, cancellationToken)
                : (IObserver<T>)new ToUniTaskObserver<T>(promise, disposable, cancellationToken);

            try
            {
                disposable.Disposable = source.Subscribe(observer);
            }
            catch (Exception ex)
            {
                promise.TrySetException(ex);
            }

            return promise.Task;
        }

        public static IObservable<T> ToObservable<T>(this UniTask<T> task)
        {
            if (task.Status.IsCompleted())
            {
                try
                {
                    return new ReturnObservable<T>(task.GetAwaiter().GetResult());
                }
                catch (Exception ex)
                {
                    return new ThrowObservable<T>(ex);
                }
            }

            var subject = new AsyncSubject<T>();
            Fire(subject, task).Forget();
            return subject;
        }

        /// <summary>
        /// Ideally returns IObservabl[Unit] is best but Cysharp.Threading.Tasks does not have Unit so return AsyncUnit instead.
        /// </summary>
        public static IObservable<AsyncUnit> ToObservable(this UniTask task)
        {
            if (task.Status.IsCompleted())
            {
                try
                {
                    task.GetAwaiter().GetResult();
                    return new ReturnObservable<AsyncUnit>(AsyncUnit.Default);
                }
                catch (Exception ex)
                {
                    return new ThrowObservable<AsyncUnit>(ex);
                }
            }

            var subject = new AsyncSubject<AsyncUnit>();
            Fire(subject, task).Forget();
            return subject;
        }

        static async UniTaskVoid Fire<T>(AsyncSubject<T> subject, UniTask<T> task)
        {
            try
            {
                var value = await task;
                subject.OnNext(value);
                subject.OnCompleted();
            }
            catch (Exception ex)
            {
                subject.OnError(ex);
            }
        }

        static async UniTaskVoid Fire(AsyncSubject<AsyncUnit> subject, UniTask task)
        {
            try
            {
                await task;
                subject.OnNext(AsyncUnit.Default);
                subject.OnCompleted();
            }
            catch (Exception ex)
            {
                subject.OnError(ex);
            }
        }

        class ToUniTaskObserver<T> : IObserver<T>
        {
            static readonly Action<object> callback = OnCanceled;

            readonly UniTaskCompletionSource<T> promise;
            readonly SingleAssignmentDisposable disposable;
            readonly CancellationToken cancellationToken;
            readonly CancellationTokenRegistration registration;

            bool hasValue;
            T latestValue;

            public ToUniTaskObserver(UniTaskCompletionSource<T> promise, SingleAssignmentDisposable disposable, CancellationToken cancellationToken)
            {
                this.promise = promise;
                this.disposable = disposable;
                this.cancellationToken = cancellationToken;

                if (this.cancellationToken.CanBeCanceled)
                {
                    this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(callback, this);
                }
            }

            static void OnCanceled(object state)
            {
                var self = (ToUniTaskObserver<T>)state;
                self.disposable.Dispose();
                self.promise.TrySetCanceled();
            }

            public void OnNext(T value)
            {
                hasValue = true;
                latestValue = value;
            }

            public void OnError(Exception error)
            {
                try
                {
                    promise.TrySetException(error);
                }
                finally
                {
                    registration.Dispose();
                    disposable.Dispose();
                }
            }

            public void OnCompleted()
            {
                try
                {
                    if (hasValue)
                    {
                        promise.TrySetResult(latestValue);
                    }
                    else
                    {
                        promise.TrySetException(new InvalidOperationException("Sequence has no elements"));
                    }
                }
                finally
                {
                    registration.Dispose();
                    disposable.Dispose();
                }
            }
        }

        class FirstValueToUniTaskObserver<T> : IObserver<T>
        {
            static readonly Action<object> callback = OnCanceled;

            readonly UniTaskCompletionSource<T> promise;
            readonly SingleAssignmentDisposable disposable;
            readonly CancellationToken cancellationToken;
            readonly CancellationTokenRegistration registration;

            bool hasValue;

            public FirstValueToUniTaskObserver(UniTaskCompletionSource<T> promise, SingleAssignmentDisposable disposable, CancellationToken cancellationToken)
            {
                this.promise = promise;
                this.disposable = disposable;
                this.cancellationToken = cancellationToken;

                if (this.cancellationToken.CanBeCanceled)
                {
                    this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(callback, this);
                }
            }

            static void OnCanceled(object state)
            {
                var self = (FirstValueToUniTaskObserver<T>)state;
                self.disposable.Dispose();
                self.promise.TrySetCanceled();
            }

            public void OnNext(T value)
            {
                hasValue = true;
                try
                {
                    promise.TrySetResult(value);
                }
                finally
                {
                    registration.Dispose();
                    disposable.Dispose();
                }
            }

            public void OnError(Exception error)
            {
                try
                {
                    promise.TrySetException(error);
                }
                finally
                {
                    registration.Dispose();
                    disposable.Dispose();
                }
            }

            public void OnCompleted()
            {
                try
                {
                    if (!hasValue)
                    {
                        promise.TrySetException(new InvalidOperationException("Sequence has no elements"));
                    }
                }
                finally
                {
                    registration.Dispose();
                    disposable.Dispose();
                }
            }
        }

        class ReturnObservable<T> : IObservable<T>
        {
            readonly T value;

            public ReturnObservable(T value)
            {
                this.value = value;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                observer.OnNext(value);
                observer.OnCompleted();
                return EmptyDisposable.Instance;
            }
        }

        class ThrowObservable<T> : IObservable<T>
        {
            readonly Exception value;

            public ThrowObservable(Exception value)
            {
                this.value = value;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                observer.OnError(value);
                return EmptyDisposable.Instance;
            }
        }
    }
}

namespace Cysharp.Threading.Tasks.Internal
{
    // Bridges for Rx.

    internal class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Instance = new EmptyDisposable();

        EmptyDisposable()
        {

        }

        public void Dispose()
        {
        }
    }

    internal sealed class SingleAssignmentDisposable : IDisposable
    {
        readonly object gate = new object();
        IDisposable current;
        bool disposed;

        public bool IsDisposed { get { lock (gate) { return disposed; } } }

        public IDisposable Disposable
        {
            get
            {
                return current;
            }
            set
            {
                var old = default(IDisposable);
                bool alreadyDisposed;
                lock (gate)
                {
                    alreadyDisposed = disposed;
                    old = current;
                    if (!alreadyDisposed)
                    {
                        if (value == null) return;
                        current = value;
                    }
                }

                if (alreadyDisposed && value != null)
                {
                    value.Dispose();
                    return;
                }

                if (old != null) throw new InvalidOperationException("Disposable is already set");
            }
        }


        public void Dispose()
        {
            IDisposable old = null;

            lock (gate)
            {
                if (!disposed)
                {
                    disposed = true;
                    old = current;
                    current = null;
                }
            }

            if (old != null) old.Dispose();
        }
    }

    internal sealed class AsyncSubject<T> : IObservable<T>, IObserver<T>
    {
        object observerLock = new object();

        T lastValue;
        bool hasValue;
        bool isStopped;
        bool isDisposed;
        Exception lastError;
        IObserver<T> outObserver = EmptyObserver<T>.Instance;

        public T Value
        {
            get
            {
                ThrowIfDisposed();
                if (!isStopped) throw new InvalidOperationException("AsyncSubject is not completed yet");
                if (lastError != null) ExceptionDispatchInfo.Capture(lastError).Throw();
                return lastValue;
            }
        }

        public bool HasObservers
        {
            get
            {
                return !(outObserver is EmptyObserver<T>) && !isStopped && !isDisposed;
            }
        }

        public bool IsCompleted { get { return isStopped; } }

        public void OnCompleted()
        {
            IObserver<T> old;
            T v;
            bool hv;
            lock (observerLock)
            {
                ThrowIfDisposed();
                if (isStopped) return;

                old = outObserver;
                outObserver = EmptyObserver<T>.Instance;
                isStopped = true;
                v = lastValue;
                hv = hasValue;
            }

            if (hv)
            {
                old.OnNext(v);
                old.OnCompleted();
            }
            else
            {
                old.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            if (error == null) throw new ArgumentNullException("error");

            IObserver<T> old;
            lock (observerLock)
            {
                ThrowIfDisposed();
                if (isStopped) return;

                old = outObserver;
                outObserver = EmptyObserver<T>.Instance;
                isStopped = true;
                lastError = error;
            }

            old.OnError(error);
        }

        public void OnNext(T value)
        {
            lock (observerLock)
            {
                ThrowIfDisposed();
                if (isStopped) return;

                this.hasValue = true;
                this.lastValue = value;
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            var ex = default(Exception);
            var v = default(T);
            var hv = false;

            lock (observerLock)
            {
                ThrowIfDisposed();
                if (!isStopped)
                {
                    var listObserver = outObserver as ListObserver<T>;
                    if (listObserver != null)
                    {
                        outObserver = listObserver.Add(observer);
                    }
                    else
                    {
                        var current = outObserver;
                        if (current is EmptyObserver<T>)
                        {
                            outObserver = observer;
                        }
                        else
                        {
                            outObserver = new ListObserver<T>(new ImmutableList<IObserver<T>>(new[] { current, observer }));
                        }
                    }

                    return new Subscription(this, observer);
                }

                ex = lastError;
                v = lastValue;
                hv = hasValue;
            }

            if (ex != null)
            {
                observer.OnError(ex);
            }
            else if (hv)
            {
                observer.OnNext(v);
                observer.OnCompleted();
            }
            else
            {
                observer.OnCompleted();
            }

            return EmptyDisposable.Instance;
        }

        public void Dispose()
        {
            lock (observerLock)
            {
                isDisposed = true;
                outObserver = DisposedObserver<T>.Instance;
                lastError = null;
                lastValue = default(T);
            }
        }

        void ThrowIfDisposed()
        {
            if (isDisposed) throw new ObjectDisposedException("");
        }
        
        class Subscription : IDisposable
        {
            readonly object gate = new object();
            AsyncSubject<T> parent;
            IObserver<T> unsubscribeTarget;

            public Subscription(AsyncSubject<T> parent, IObserver<T> unsubscribeTarget)
            {
                this.parent = parent;
                this.unsubscribeTarget = unsubscribeTarget;
            }

            public void Dispose()
            {
                lock (gate)
                {
                    if (parent != null)
                    {
                        lock (parent.observerLock)
                        {
                            var listObserver = parent.outObserver as ListObserver<T>;
                            if (listObserver != null)
                            {
                                parent.outObserver = listObserver.Remove(unsubscribeTarget);
                            }
                            else
                            {
                                parent.outObserver = EmptyObserver<T>.Instance;
                            }

                            unsubscribeTarget = null;
                            parent = null;
                        }
                    }
                }
            }
        }
    }

    internal class ListObserver<T> : IObserver<T>
    {
        private readonly ImmutableList<IObserver<T>> _observers;

        public ListObserver(ImmutableList<IObserver<T>> observers)
        {
            _observers = observers;
        }

        public void OnCompleted()
        {
            var targetObservers = _observers.Data;
            for (int i = 0; i < targetObservers.Length; i++)
            {
                targetObservers[i].OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            var targetObservers = _observers.Data;
            for (int i = 0; i < targetObservers.Length; i++)
            {
                targetObservers[i].OnError(error);
            }
        }

        public void OnNext(T value)
        {
            var targetObservers = _observers.Data;
            for (int i = 0; i < targetObservers.Length; i++)
            {
                targetObservers[i].OnNext(value);
            }
        }

        internal IObserver<T> Add(IObserver<T> observer)
        {
            return new ListObserver<T>(_observers.Add(observer));
        }

        internal IObserver<T> Remove(IObserver<T> observer)
        {
            var i = Array.IndexOf(_observers.Data, observer);
            if (i < 0)
                return this;

            if (_observers.Data.Length == 2)
            {
                return _observers.Data[1 - i];
            }
            else
            {
                return new ListObserver<T>(_observers.Remove(observer));
            }
        }
    }

    internal class EmptyObserver<T> : IObserver<T>
    {
        public static readonly EmptyObserver<T> Instance = new EmptyObserver<T>();

        EmptyObserver()
        {

        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
        }
    }

    internal class ThrowObserver<T> : IObserver<T>
    {
        public static readonly ThrowObserver<T> Instance = new ThrowObserver<T>();

        ThrowObserver()
        {

        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            ExceptionDispatchInfo.Capture(error).Throw();
        }

        public void OnNext(T value)
        {
        }
    }

    internal class DisposedObserver<T> : IObserver<T>
    {
        public static readonly DisposedObserver<T> Instance = new DisposedObserver<T>();

        DisposedObserver()
        {

        }

        public void OnCompleted()
        {
            throw new ObjectDisposedException("");
        }

        public void OnError(Exception error)
        {
            throw new ObjectDisposedException("");
        }

        public void OnNext(T value)
        {
            throw new ObjectDisposedException("");
        }
    }

    internal class ImmutableList<T>
    {
        public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

        T[] data;

        public T[] Data
        {
            get { return data; }
        }

        ImmutableList()
        {
            data = new T[0];
        }

        public ImmutableList(T[] data)
        {
            this.data = data;
        }

        public ImmutableList<T> Add(T value)
        {
            var newData = new T[data.Length + 1];
            Array.Copy(data, newData, data.Length);
            newData[data.Length] = value;
            return new ImmutableList<T>(newData);
        }

        public ImmutableList<T> Remove(T value)
        {
            var i = IndexOf(value);
            if (i < 0) return this;

            var length = data.Length;
            if (length == 1) return Empty;

            var newData = new T[length - 1];

            Array.Copy(data, 0, newData, 0, i);
            Array.Copy(data, i + 1, newData, i, length - i - 1);

            return new ImmutableList<T>(newData);
        }

        public int IndexOf(T value)
        {
            for (var i = 0; i < data.Length; ++i)
            {
                // ImmutableList only use for IObserver(no worry for boxed)
                if (object.Equals(data[i], value)) return i;
            }
            return -1;
        }
    }
}

