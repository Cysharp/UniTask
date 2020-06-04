using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IConnectableUniTaskAsyncEnumerable<TSource> Publish<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new Publish<TSource>(source);
        }
    }

    internal sealed class Publish<TSource> : IConnectableUniTaskAsyncEnumerable<TSource>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly CancellationTokenSource cancellationTokenSource;

        TriggerEvent<TSource> trigger;
        IUniTaskAsyncEnumerator<TSource> enumerator;
        IDisposable connectedDisposable;
        bool isCompleted;

        public Publish(IUniTaskAsyncEnumerable<TSource> source)
        {
            this.source = source;
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public IDisposable Connect()
        {
            if (connectedDisposable != null) return connectedDisposable;

            if (enumerator == null)
            {
                enumerator = source.GetAsyncEnumerator(cancellationTokenSource.Token);
            }

            ConsumeEnumerator().Forget();

            connectedDisposable = new ConnectDisposable(cancellationTokenSource);
            return connectedDisposable;
        }

        async UniTaskVoid ConsumeEnumerator()
        {
            try
            {
                try
                {
                    while (await enumerator.MoveNextAsync())
                    {
                        trigger.SetResult(enumerator.Current);
                    }
                    trigger.SetCompleted();
                }
                catch (Exception ex)
                {
                    trigger.SetError(ex);
                }
            }
            finally
            {
                isCompleted = true;
                await enumerator.DisposeAsync();
            }
        }

        public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Publish(this, cancellationToken);
        }

        sealed class ConnectDisposable : IDisposable
        {
            readonly CancellationTokenSource cancellationTokenSource;

            public ConnectDisposable(CancellationTokenSource cancellationTokenSource)
            {
                this.cancellationTokenSource = cancellationTokenSource;
            }

            public void Dispose()
            {
                this.cancellationTokenSource.Cancel();
            }
        }

        sealed class _Publish : MoveNextSource, IUniTaskAsyncEnumerator<TSource>, ITriggerHandler<TSource>
        {
            static readonly Action<object> CancelDelegate = OnCanceled;

            readonly Publish<TSource> parent;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool isDisposed;

            public _Publish(Publish<TSource> parent, CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested) return;

                this.parent = parent;
                this.cancellationToken = cancellationToken;

                if (cancellationToken.CanBeCanceled)
                {
                    this.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(CancelDelegate, this);
                }

                parent.trigger.Add(this);
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TSource Current { get; private set; }
            ITriggerHandler<TSource> ITriggerHandler<TSource>.Prev { get; set; }
            ITriggerHandler<TSource> ITriggerHandler<TSource>.Next { get; set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (parent.isCompleted) return CompletedTasks.False;

                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            static void OnCanceled(object state)
            {
                var self = (_Publish)state;
                self.completionSource.TrySetCanceled(self.cancellationToken);
                self.DisposeAsync().Forget();
            }

            public UniTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    cancellationTokenRegistration.Dispose();
                    parent.trigger.Remove(this);
                }

                return default;
            }

            public void OnNext(TSource value)
            {
                Current = value;
                completionSource.TrySetResult(true);
            }

            public void OnCanceled(CancellationToken cancellationToken)
            {
                completionSource.TrySetCanceled(cancellationToken);
            }

            public void OnCompleted()
            {
                completionSource.TrySetResult(false);
            }

            public void OnError(Exception ex)
            {
                completionSource.TrySetException(ex);
            }
        }
    }
}