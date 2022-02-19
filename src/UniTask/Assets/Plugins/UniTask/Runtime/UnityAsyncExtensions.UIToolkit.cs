#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#if UNITY_2021_2_OR_NEWER
using System;
using System.Threading;
using UnityEngine.UIElements;

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        public static IAsyncUIToolkitEventHandler<TEventType> GetAsyncEventHandler<TEventType>(this CallbackEventHandler eventHandler, CancellationToken cancellationToken)
            where TEventType : EventBase<TEventType>, new()
        {
            return new AsyncUIToolkitDefaultEventHandler<TEventType>(eventHandler, cancellationToken, false);
        }

        public static UniTask<TEventType> OnInvokeAsync<TEventType>(this CallbackEventHandler eventHandler, CancellationToken cancellationToken)
            where TEventType : EventBase<TEventType>, new()
        {
            return new AsyncUIToolkitDefaultEventHandler<TEventType>(eventHandler, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<TEventType> OnInvokeAsAsyncEnumerable<TEventType>(this CallbackEventHandler eventHandler, CancellationToken cancellationToken)
            where TEventType : EventBase<TEventType>, new()
        {
            return new UIToolkitEventHandlerAsyncEnumerable<TEventType>(eventHandler, cancellationToken);
        }

        public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUIToolkitDefaultEventHandler<ClickEvent>(button, cancellationToken, false);
        }

        public static UniTask OnClickAsync(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUIToolkitDefaultEventHandler<ClickEvent>(button, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<ClickEvent> OnClickAsAsyncEnumerable(this Button button, CancellationToken cancellationToken)
        {
            return new UIToolkitEventHandlerAsyncEnumerable<ClickEvent>(button, cancellationToken);
        }

        public static IAsyncValueChangedEventHandler<T> GetAsyncValueChangedEventHandler<T>(this INotifyValueChanged<T> eventHandler, CancellationToken cancellationToken)
        {
            return new AsyncUIToolkitChangeEventHandler<T>(eventHandler, cancellationToken, false);
        }

        public static UniTask<T> OnValueChangedAsync<T>(this INotifyValueChanged<T> eventHandler, CancellationToken cancellationToken)
        {
            return new AsyncUIToolkitChangeEventHandler<T>(eventHandler, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<T> OnValueChangedAsAsyncEnumerable<T>(this INotifyValueChanged<T> eventHandler, CancellationToken cancellationToken)
        {
            return new UIToolkitChangeEventHandlerAsyncEnumerable<T>(eventHandler, cancellationToken);
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, TextField text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError, SetTextFromStringEnumerator).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, TextElement text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError, SetTextFromStringEnumerator).Forget();
        }

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, TextField text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError, SetTextFromEnumerator).Forget();
        }

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, TextElement text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError, SetTextFromEnumerator).Forget();
        }

        static void SetTextFromStringEnumerator(INotifyValueChanged<string> control, IUniTaskAsyncEnumerator<string> enumerator)
        {
            control.value = enumerator.Current;
        }

        static void SetTextFromEnumerator<T>(INotifyValueChanged<string> control, IUniTaskAsyncEnumerator<T> enumerator)
        {
            control.value = enumerator.Current.ToString();
        }

        static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, 
            INotifyValueChanged<string> text, 
            CancellationToken cancellationToken, 
            bool rebindOnError,
            Action<INotifyValueChanged<string>, IUniTaskAsyncEnumerator<T>> setter)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    setter(text, e);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
    }

    public interface IAsyncUIToolkitEventHandler<TEventType> : IDisposable
    {
        UniTask<TEventType> OnInvokeAsync();
    }
        
    public class AsyncUIToolkitChangeEventHandler<TReturnType> : AsyncUIToolkitEventHandler<INotifyValueChanged<TReturnType>, ChangeEvent<TReturnType>, TReturnType>
        , IAsyncValueChangedEventHandler<TReturnType> 
    {
        public AsyncUIToolkitChangeEventHandler(INotifyValueChanged<TReturnType> eventHandler, CancellationToken cancellationToken, bool callOnce)
         : base(eventHandler, cancellationToken, callOnce)
         {
             eventHandler.RegisterValueChangedCallback(action);
         }

         UniTask<TReturnType> IAsyncValueChangedEventHandler<TReturnType>.OnValueChangedAsync()
        {
            return OnInvokeAsync();
        }

         protected override TReturnType GetValueFor(ChangeEvent<TReturnType> changeEvent)
        {
            return changeEvent.newValue;
        }

         protected override void UnregisterCallback()
         {
             eventHandler.UnregisterValueChangedCallback(action);
         }
    }

    public class UIToolkitChangeEventHandlerAsyncEnumerable<T> : IUniTaskAsyncEnumerable<T>
    {
        readonly INotifyValueChanged<T> eventHandler;
        readonly CancellationToken cancellationToken1;

        public UIToolkitChangeEventHandlerAsyncEnumerable(INotifyValueChanged<T> eventHandler, CancellationToken cancellationToken)
        {
            this.eventHandler = eventHandler;
            this.cancellationToken1 = cancellationToken;
        }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            if (this.cancellationToken1 == cancellationToken)
            {
                return new UIToolkitChangeEventHandlerAsyncEnumerator(eventHandler, this.cancellationToken1, CancellationToken.None);
            }
            else
            {
                return new UIToolkitChangeEventHandlerAsyncEnumerator(eventHandler, this.cancellationToken1, cancellationToken);
            }
        }

        class UIToolkitChangeEventHandlerAsyncEnumerator : UIToolkitEventHandlerAsyncEnumerator<INotifyValueChanged<T>, ChangeEvent<T>, T>
        {
            public UIToolkitChangeEventHandlerAsyncEnumerator(INotifyValueChanged<T> eventHandler, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
                : base(eventHandler, cancellationToken1, cancellationToken2)
            {
            }

            protected override T GetValueFrom(ChangeEvent<T> changeEvent)
            {
                return changeEvent.newValue;
            }

            protected override void RegisterCallback(EventCallback<ChangeEvent<T>> callback)
            {
                eventHandler.RegisterValueChangedCallback(callback);
            }

            protected override void UnregisterCallback(EventCallback<ChangeEvent<T>> callback)
            {
                eventHandler.UnregisterValueChangedCallback(callback);
            }
        }
    }
    
    public class AsyncUIToolkitDefaultEventHandler<TEventType> : AsyncUIToolkitEventHandler<CallbackEventHandler, TEventType, TEventType>, IAsyncClickEventHandler where TEventType : EventBase<TEventType>, new()
    {
        public AsyncUIToolkitDefaultEventHandler(CallbackEventHandler eventHandler, CancellationToken cancellationToken, bool callOnce)
            : base(eventHandler, cancellationToken, callOnce)
        {
            eventHandler.RegisterCallback<TEventType>(action);
        }

        protected override TEventType GetValueFor(TEventType result)
        {
            return result;
        }

        protected override void UnregisterCallback()
        {
            eventHandler.UnregisterCallback<TEventType>(action);
        }

        UniTask IAsyncClickEventHandler.OnClickAsync()
        {
            return OnInvokeAsync();
        }
    }

    public class UIToolkitEventHandlerAsyncEnumerable<TEventType> : IUniTaskAsyncEnumerable<TEventType> where TEventType : EventBase<TEventType>, new()
    {
        readonly CallbackEventHandler eventHandler;
        readonly CancellationToken cancellationToken1;

        public UIToolkitEventHandlerAsyncEnumerable(CallbackEventHandler eventHandler, CancellationToken cancellationToken)
        {
            this.eventHandler = eventHandler;
            this.cancellationToken1 = cancellationToken;
        }

        public IUniTaskAsyncEnumerator<TEventType> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            if (this.cancellationToken1 == cancellationToken)
            {
                return new UIToolkitEventHandlerAsyncEnumerator(eventHandler, this.cancellationToken1, CancellationToken.None);
            }
            else
            {
                return new UIToolkitEventHandlerAsyncEnumerator(eventHandler, this.cancellationToken1, cancellationToken);
            }
        }

        class UIToolkitEventHandlerAsyncEnumerator : UIToolkitEventHandlerAsyncEnumerator<CallbackEventHandler, TEventType, TEventType>
        {
            public UIToolkitEventHandlerAsyncEnumerator(CallbackEventHandler eventHandler, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
                : base(eventHandler, cancellationToken1, cancellationToken2)
            {
            }

            protected override TEventType GetValueFrom(TEventType eventValue)
            {
                return eventValue;
            }

            protected override void RegisterCallback(EventCallback<TEventType> callback)
            {
                eventHandler.RegisterCallback(callback);
            }

            protected override void UnregisterCallback(EventCallback<TEventType> callback)
            {
                eventHandler.UnregisterCallback(callback);
            }
        }
    }


    public abstract class AsyncUIToolkitEventHandler<TEventHandler, TEventType, TReturnType> : IUniTaskSource<TReturnType>, IAsyncUIToolkitEventHandler<TReturnType> where TEventType : EventBase<TEventType>, new()
    {
        static Action<object> cancellationCallback = CancellationCallback;
        protected readonly EventCallback<TEventType> action;
        protected readonly TEventHandler eventHandler;
        CancellationToken cancellationToken;
        CancellationTokenRegistration registration;
        bool isDisposed;
        bool callOnce;

        UniTaskCompletionSourceCore<TReturnType> core;

        public AsyncUIToolkitEventHandler(TEventHandler eventHandler, CancellationToken cancellationToken, bool callOnce)
        {
            this.cancellationToken = cancellationToken;
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            this.action = Invoke;
            this.eventHandler = eventHandler;
            this.callOnce = callOnce;

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
            }

            TaskTracker.TrackActiveTask(this, 3);
        }

        public UniTask<TReturnType> OnInvokeAsync()
        {
            core.Reset();
            if (isDisposed)
            {
                core.TrySetCanceled(this.cancellationToken);
            }
            return new UniTask<TReturnType>(this, core.Version);
        }

        protected abstract TReturnType GetValueFor(TEventType eventValue);

        void Invoke(TEventType eventValue)
        {
            core.TrySetResult(GetValueFor(eventValue));
        }

        static void CancellationCallback(object state)
        {
            var self = (AsyncUIToolkitEventHandler<TEventHandler, TEventType, TReturnType>)state;
            self.Dispose();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration.Dispose();
                if (eventHandler != null)
                {
                    UnregisterCallback();
                }

                core.TrySetCanceled();
            }
        }

        protected abstract void UnregisterCallback();

        TReturnType IUniTaskSource<TReturnType>.GetResult(short token)
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

        void IUniTaskSource.GetResult(short token)
        {
            ((IUniTaskSource<TReturnType>)this).GetResult(token);
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

    internal abstract class UIToolkitEventHandlerAsyncEnumerator<TEventHandler, TEventType, TReturnType> : MoveNextSource, IUniTaskAsyncEnumerator<TReturnType>
    {
        static readonly Action<object> cancel1 = OnCanceled1;
        static readonly Action<object> cancel2 = OnCanceled2;

        protected readonly TEventHandler eventHandler;
        CancellationToken cancellationToken1;
        CancellationToken cancellationToken2;

        EventCallback<TEventType> unityAction;
        CancellationTokenRegistration registration1;
        CancellationTokenRegistration registration2;
        bool isDisposed;

        public UIToolkitEventHandlerAsyncEnumerator(TEventHandler eventHandler, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
        {
            this.eventHandler = eventHandler;
            this.cancellationToken1 = cancellationToken1;
            this.cancellationToken2 = cancellationToken2;
        }

        public TReturnType Current { get; private set; }

        protected abstract TReturnType GetValueFrom(TEventType eventValue);
        protected abstract void RegisterCallback(EventCallback<TEventType> callback);
        protected abstract void UnregisterCallback(EventCallback<TEventType> calback);

        public UniTask<bool> MoveNextAsync()
        {
            cancellationToken1.ThrowIfCancellationRequested();
            cancellationToken2.ThrowIfCancellationRequested();
            completionSource.Reset();

            if (unityAction == null)
            {
                unityAction = Invoke;

                TaskTracker.TrackActiveTask(this, 3);
                RegisterCallback(unityAction);
                if (cancellationToken1.CanBeCanceled)
                {
                    registration1 = cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel1, this);
                }
                if (cancellationToken2.CanBeCanceled)
                {
                    registration2 = cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel2, this);
                }
            }

            return new UniTask<bool>(this, completionSource.Version);
        }

        void Invoke(TEventType changeEvent)
        {
            Current = GetValueFrom(changeEvent);
            completionSource.TrySetResult(true);
        }

        static void OnCanceled1(object state)
        {
            var self = (UIToolkitEventHandlerAsyncEnumerator<TEventHandler, TEventType, TReturnType> )state;
            self.DisposeAsync().Forget();
        }

        static void OnCanceled2(object state)
        {
            var self = (UIToolkitEventHandlerAsyncEnumerator<TEventHandler, TEventType, TReturnType> )state;
            self.DisposeAsync().Forget();
        }

        public UniTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration1.Dispose();
                registration2.Dispose();
                UnregisterCallback(unityAction);
            }

            return default;
        }
    }
}

#endif