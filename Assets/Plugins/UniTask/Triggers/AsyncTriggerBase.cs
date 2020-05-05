#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
    public abstract class AsyncTriggerBase : MonoBehaviour
    {
        bool calledAwake;
        bool calledDestroy;
        ICancelPromise triggerSlot;

        void Awake()
        {
            calledAwake = true;
        }

        protected TriggerEvent<T> SetTriggerEvent<T>(ref TriggerEvent<T> field)
        {
            if (field == null)
            {
                field = new TriggerEvent<T>();
                if (triggerSlot == null)
                {
                    triggerSlot = field;
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

            return field;
        }

        void OnDestroy()
        {
            if (calledDestroy) return;
            calledDestroy = true;

            triggerSlot?.TrySetCanceled();
            triggerSlot = null;
        }

        class AwakeMonitor : IPlayerLoopItem
        {
            readonly AsyncTriggerBase trigger;

            public AwakeMonitor(AsyncTriggerBase trigger)
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

    public sealed partial class AsyncTriggerHandler<T> : IUniTaskSource<T>, IResolvePromise<T>, ICancelPromise, IDisposable
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

    public sealed class TriggerEvent<T> : IResolvePromise<T>, ICancelPromise
    {
        // optimize: many cases, handler is single.
        AsyncTriggerHandler<T> singleHandler;

        List<AsyncTriggerHandler<T>> handlers;

        public bool TrySetResult(T value)
        {
            List<Exception> exceptions = null;

            if (singleHandler != null)
            {
                try
                {
                    ((IResolvePromise<T>)singleHandler).TrySetResult(value);
                }
                catch (Exception ex)
                {
                    if (handlers == null)
                    {
                        throw;
                    }
                    else
                    {
                        exceptions = new List<Exception>();
                        exceptions.Add(ex);
                    }
                }
            }

            if (handlers != null)
            {
                // make snapshot
                var rentArray = ArrayPoolUtil.CopyToRentArray(handlers);
                var clearArray = true;
                try
                {
                    var array = rentArray.Array;
                    var len = rentArray.Length;
                    for (int i = 0; i < len; i++)
                    {
                        try
                        {
                            ((IResolvePromise<T>)array[i]).TrySetResult(value);
                        }
                        catch (Exception ex)
                        {
                            if (exceptions == null)
                            {
                                exceptions = new List<Exception>();
                            }
                            exceptions.Add(ex);
                        }
                        finally
                        {
                            array[i] = null;
                        }
                    }
                }
                finally
                {
                    rentArray.DisposeManually(clearArray);
                }
            }

            if (exceptions != null)
            {
                throw new AggregateException(exceptions);
            }

            return true;
        }

        public bool TrySetCanceled(CancellationToken cancellationToken)
        {
            List<Exception> exceptions = null;

            if (singleHandler != null)
            {
                try
                {
                    ((ICancelPromise)singleHandler).TrySetCanceled(cancellationToken);
                }
                catch (Exception ex)
                {
                    if (handlers == null)
                    {
                        throw;
                    }
                    else
                    {
                        exceptions = new List<Exception>();
                        exceptions.Add(ex);
                    }
                }
            }

            if (handlers != null)
            {
                // make snapshot
                var rentArray = ArrayPoolUtil.CopyToRentArray(handlers);
                var clearArray = true;
                try
                {
                    var array = rentArray.Array;
                    var len = rentArray.Length;
                    for (int i = 0; i < len; i++)
                    {
                        try
                        {
                            ((ICancelPromise)array[i]).TrySetCanceled(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            if (exceptions == null)
                            {
                                exceptions = new List<Exception>();
                            }
                            exceptions.Add(ex);
                        }
                        finally
                        {
                            array[i] = null;
                        }
                    }
                }
                finally
                {
                    rentArray.DisposeManually(clearArray);
                }
            }

            if (exceptions != null)
            {
                throw new AggregateException(exceptions);
            }

            return true;
        }

        public void Add(AsyncTriggerHandler<T> handler)
        {
            if (singleHandler == null)
            {
                singleHandler = handler;
            }
            else
            {
                if (handlers != null)
                {
                    handlers = new List<AsyncTriggerHandler<T>>();
                    handlers.Add(handler);
                }
            }
        }

        public void Remove(AsyncTriggerHandler<T> handler)
        {
            if (singleHandler == handler)
            {
                singleHandler = null;
            }
            else
            {
                if (handlers != null)
                {
                    handlers.Remove(handler);
                }
            }
        }
    }
}

#endif