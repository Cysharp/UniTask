#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using Cysharp.Threading.Tasks.Internal;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
    public abstract class AsyncTriggerBase<T> : MonoBehaviour, IUniTaskAsyncEnumerable<T>
    {
        protected TriggerEvent<T> triggerEvent;

        bool calledAwake;
        bool calledDestroy;
        ICancelPromise triggerSlot;

        void Awake()
        {
            calledAwake = true;
        }

        protected TriggerEvent<T> GetTriggerEvent()
        {
            if (triggerEvent == null)
            {
                triggerEvent = new TriggerEvent<T>();
                if (triggerSlot == null)
                {
                    triggerSlot = triggerEvent;
                }
                else
                {
                    throw new InvalidOperationException("triggerSlot is already filled.");
                }
            }

            if (!calledAwake)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AwakeMonitor(this));
            }

            return triggerEvent;
        }

        void OnDestroy()
        {
            if (calledDestroy) return;
            calledDestroy = true;

            triggerSlot?.TrySetCanceled();
            triggerSlot = null;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(GetTriggerEvent(), cancellationToken);
        }

        sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IResolveCancelPromise<T>
        {
            static Action<object> cancellationCallback = CancellationCallback;

            readonly TriggerEvent<T> triggerEvent;
            CancellationToken cancellationToken;
            CancellationTokenRegistration registration;
            bool called;
            bool isDisposed;

            public Enumerator(TriggerEvent<T> triggerEvent, CancellationToken cancellationToken)
            {
                this.triggerEvent = triggerEvent;
                this.cancellationToken = cancellationToken;
            }

            public bool TrySetCanceled(CancellationToken cancellationToken = default)
            {
                return completionSource.TrySetCanceled(cancellationToken);
            }

            public bool TrySetResult(T value)
            {
                Current = value;
                return completionSource.TrySetResult(true);
            }

            static void CancellationCallback(object state)
            {
                var self = (Enumerator)state;
                self.DisposeAsync().Forget(); // sync

                self.completionSource.TrySetCanceled(self.cancellationToken);
            }

            public T Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (!called)
                {
                    called = true;

                    TaskTracker.TrackActiveTask(this, 3);
                    triggerEvent.Add(this);
                    if (cancellationToken.CanBeCanceled)
                    {
                        registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                    }
                }
                
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    registration.Dispose();
                    triggerEvent.Remove(this);
                }

                return default;
            }
        }

        class AwakeMonitor : IPlayerLoopItem
        {
            readonly AsyncTriggerBase<T> trigger;

            public AwakeMonitor(AsyncTriggerBase<T> trigger)
            {
                this.trigger = trigger;
            }

            public bool MoveNext()
            {
                if (trigger.calledAwake) return false;
                if (trigger == null)
                {
                    trigger.OnDestroy();
                    return false;
                }
                return true;
            }
        }
    }

    public interface IAsyncOneShotTrigger
    {
        UniTask OneShotAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOneShotTrigger
    {
        UniTask IAsyncOneShotTrigger.OneShotAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)this, core.Version);
        }
    }

    public sealed partial class AsyncTriggerHandler<T> : IUniTaskSource<T>, IResolveCancelPromise<T>, IDisposable
    {
        static Action<object> cancellationCallback = CancellationCallback;

        readonly TriggerEvent<T> trigger;

        CancellationToken cancellationToken;
        CancellationTokenRegistration registration;
        bool isDisposed;
        bool callOnce;

        UniTaskCompletionSourceCore<T> core;

        internal CancellationToken CancellationToken => cancellationToken;

        public AsyncTriggerHandler(TriggerEvent<T> trigger, bool callOnce)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            this.trigger = trigger;
            this.cancellationToken = default;
            this.registration = default;
            this.callOnce = callOnce;

            trigger.Add(this);

            TaskTracker.TrackActiveTask(this, 3);
        }

        public AsyncTriggerHandler(TriggerEvent<T> trigger, CancellationToken cancellationToken, bool callOnce)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            this.trigger = trigger;
            this.cancellationToken = cancellationToken;
            this.callOnce = callOnce;

            trigger.Add(this);

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
            }

            TaskTracker.TrackActiveTask(this, 3);
        }

        static void CancellationCallback(object state)
        {
            var self = (AsyncTriggerHandler<T>)state;
            self.Dispose();

            self.core.TrySetCanceled(self.cancellationToken);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration.Dispose();
                trigger.Remove(this);
            }
        }

        T IUniTaskSource<T>.GetResult(short token)
        {
            try
            {
                return core.GetResult(token);
            }
            finally
            {
                if (callOnce)
                {
                    Dispose();
                }
            }
        }

        bool IResolvePromise<T>.TrySetResult(T result)
        {
            return core.TrySetResult(result);
        }

        bool ICancelPromise.TrySetCanceled(CancellationToken cancellationToken)
        {
            return core.TrySetCanceled(cancellationToken);
        }

        void IUniTaskSource.GetResult(short token)
        {
            ((IUniTaskSource<T>)this).GetResult(token);
        }

        UniTaskStatus IUniTaskSource.GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        UniTaskStatus IUniTaskSource.UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }
    }

    public sealed class TriggerEvent<T> : IResolveCancelPromise<T>
    {
        // optimize: many cases, handler is single.
        IResolveCancelPromise<T> singleHandler;

        IResolveCancelPromise<T>[] handlers;

        // when running(in TrySetResult), does not add immediately.
        bool isRunning;
        IResolveCancelPromise<T> waitHandler;
        MinimumQueue<IResolveCancelPromise<T>> waitQueue;

        public bool TrySetResult(T value)
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    singleHandler.TrySetResult(value);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            if (handlers != null)
            {
                for (int i = 0; i < handlers.Length; i++)
                {
                    if (handlers[i] != null)
                    {
                        try
                        {
                            handlers[i].TrySetResult(value);
                        }
                        catch (Exception ex)
                        {
                            handlers[i] = null;
                            Debug.LogException(ex);
                        }
                    }
                }
            }

            isRunning = false;

            if (waitHandler != null)
            {
                var h = waitHandler;
                waitHandler = null;
                Add(h);
            }

            if (waitQueue != null)
            {
                while (waitQueue.Count != 0)
                {
                    Add(waitQueue.Dequeue());
                }
            }

            return true;
        }

        public bool TrySetCanceled(CancellationToken cancellationToken)
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    ((ICancelPromise)singleHandler).TrySetCanceled(cancellationToken);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            if (handlers != null)
            {
                for (int i = 0; i < handlers.Length; i++)
                {
                    if (handlers[i] != null)
                    {
                        try
                        {
                            ((ICancelPromise)handlers[i]).TrySetCanceled(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                            handlers[i] = null;
                        }
                    }
                }
            }

            isRunning = false;

            if (waitHandler != null)
            {
                var h = waitHandler;
                waitHandler = null;
                Add(h);
            }

            if (waitQueue != null)
            {
                while (waitQueue.Count != 0)
                {
                    Add(waitQueue.Dequeue());
                }
            }

            return true;
        }

        public void Add(IResolveCancelPromise<T> handler)
        {
            if (isRunning)
            {
                if (waitHandler == null)
                {
                    waitHandler = handler;
                    return;
                }

                if (waitQueue == null)
                {
                    waitQueue = new MinimumQueue<IResolveCancelPromise<T>>(4);
                }
                waitQueue.Enqueue(handler);
                return;
            }

            if (singleHandler == null)
            {
                singleHandler = handler;
            }
            else
            {
                if (handlers == null)
                {
                    handlers = new IResolveCancelPromise<T>[4];
                }

                // check empty
                for (int i = 0; i < handlers.Length; i++)
                {
                    if (handlers[i] == null)
                    {
                        handlers[i] = handler;
                        return;
                    }
                }

                // full, ensure capacity
                var last = handlers.Length;
                {
                    EnsureCapacity(ref handlers);
                    handlers[last] = handler;
                }
            }
        }

        static void EnsureCapacity(ref IResolveCancelPromise<T>[] array)
        {
            var newSize = array.Length * 2;
            var newArray = new IResolveCancelPromise<T>[newSize];
            Array.Copy(array, 0, newArray, 0, array.Length);
            array = newArray;
        }

        public void Remove(IResolveCancelPromise<T> handler)
        {
            if (singleHandler == handler)
            {
                singleHandler = null;
            }
            else
            {
                if (handlers != null)
                {
                    for (int i = 0; i < handlers.Length; i++)
                    {
                        if (handlers[i] == handler)
                        {
                            // fill null.
                            handlers[i] = null;
                            return;
                        }
                    }
                }
            }
        }
    }
}