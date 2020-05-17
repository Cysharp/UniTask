using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public interface IAsyncReadOnlyReactiveProperty<T> : IUniTaskAsyncEnumerable<T>
    {
        T Value { get; }
    }

    public interface IAsyncReactiveProperty<T> : IAsyncReadOnlyReactiveProperty<T>
    {
        new T Value { get; set; }
    }

    [Serializable]
    public class AsyncReactiveProperty<T> : IAsyncReactiveProperty<T>, IDisposable
    {
        TriggerEvent<T> triggerEvent;

#if UNITY_2018_3_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        T latestValue;

        public T Value
        {
            get
            {
                return latestValue;
            }
            set
            {
                this.latestValue = value;
                triggerEvent.SetResult(value);
            }
        }

        public AsyncReactiveProperty(T value)
        {
            this.latestValue = value;
            this.triggerEvent = default;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new Enumerator(this, cancellationToken);
        }

        public void Dispose()
        {
            triggerEvent.SetCanceled(CancellationToken.None);
        }

        sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IResolveCancelPromise<T>
        {
            static Action<object> cancellationCallback = CancellationCallback;

            readonly AsyncReactiveProperty<T> parent;
            readonly CancellationToken cancellationToken;
            readonly CancellationTokenRegistration cancellationTokenRegistration;
            T value;
            bool isDisposed;

            public Enumerator(AsyncReactiveProperty<T> parent, CancellationToken cancellationToken)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;

                parent.triggerEvent.Add(this);
                TaskTracker.TrackActiveTask(this, 3);

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                }
            }

            public T Current => value;

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    completionSource.TrySetCanceled(cancellationToken);
                    parent.triggerEvent.Remove(this);
                }
                return default;
            }

            public bool TrySetResult(T value)
            {
                this.value = value;
                completionSource.TrySetResult(true);
                return true;
            }

            public bool TrySetCanceled(CancellationToken cancellationToken = default)
            {
                DisposeAsync().Forget();
                return true;
            }

            static void CancellationCallback(object state)
            {
                var self = (Enumerator)state;
                self.DisposeAsync().Forget();
            }
        }
    }
}
