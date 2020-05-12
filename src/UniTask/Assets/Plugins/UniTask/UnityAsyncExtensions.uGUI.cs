#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        public static AsyncUnityEventHandler GetAsyncEventHandler(this UnityEvent unityEvent, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(unityEvent, cancellationToken, false);
        }

        public static UniTask OnInvokeAsync(this UnityEvent unityEvent, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(unityEvent, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button)
        {
            return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncClickEventHandler GetAsyncClickEventHandler(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(button.onClick, cancellationToken, false);
        }

        public static UniTask OnClickAsync(this Button button)
        {
            return new AsyncUnityEventHandler(button.onClick, button.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask OnClickAsync(this Button button, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler(button.onClick, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(this Toggle toggle)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<bool> GetAsyncValueChangedEventHandler(this Toggle toggle, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, false);
        }

        public static UniTask<bool> OnValueChangedAsync(this Toggle toggle)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, toggle.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<bool> OnValueChangedAsync(this Toggle toggle, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<bool>(toggle.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, false);
        }

        public static UniTask<float> OnValueChangedAsync(this Scrollbar scrollbar)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, scrollbar.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<float> OnValueChangedAsync(this Scrollbar scrollbar, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(scrollbar.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<Vector2> GetAsyncValueChangedEventHandler(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, false);
        }

        public static UniTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, scrollRect.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<Vector2> OnValueChangedAsync(this ScrollRect scrollRect, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<Vector2>(scrollRect.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Slider slider)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<float> GetAsyncValueChangedEventHandler(this Slider slider, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, false);
        }

        public static UniTask<float> OnValueChangedAsync(this Slider slider)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, slider.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<float> OnValueChangedAsync(this Slider slider, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<float>(slider.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, false);
        }

        public static UniTask<string> OnEndEditAsync(this InputField inputField)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<string> OnEndEditAsync(this InputField inputField, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, true).OnInvokeAsync();
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, false);
        }

        public static UniTask<int> OnValueChanged(this Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<int> OnValueChanged(this Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }
    }

    public interface IAsyncClickEventHandler : IDisposable, IUniTaskAsyncEnumerable<AsyncUnit>
    {
        UniTask OnClickAsync();
        IAsyncClickEventHandler DisableAutoClose();
    }

    public interface IAsyncValueChangedEventHandler<T> : IDisposable, IUniTaskAsyncEnumerable<T>
    {
        UniTask<T> OnValueChangedAsync();
        IAsyncValueChangedEventHandler<T> DisableAutoClose();
    }

    public interface IAsyncEndEditEventHandler<T> : IDisposable, IUniTaskAsyncEnumerable<T>
    {
        UniTask<T> OnEndEditAsync();
        IAsyncEndEditEventHandler<T> DisableAutoClose();
    }

    public class AsyncUnityEventHandler : IUniTaskSource, IDisposable, IAsyncClickEventHandler, IUniTaskAsyncEnumerable<AsyncUnit>
    {
        static Action<object> cancellationCallback = CancellationCallback;

        readonly UnityAction action;
        readonly UnityEvent unityEvent;

        CancellationToken cancellationToken;
        CancellationTokenRegistration registration;
        bool isDisposed;
        bool callOnce;

        UniTaskCompletionSourceCore<AsyncUnit> core;

        public AsyncUnityEventHandler(UnityEvent unityEvent, CancellationToken cancellationToken, bool callOnce)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            this.action = Invoke;
            this.unityEvent = unityEvent;
            this.cancellationToken = cancellationToken;
            this.callOnce = callOnce;

            unityEvent.AddListener(action);

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
            }

            TaskTracker.TrackActiveTask(this, 3);
        }

        public UniTask OnInvokeAsync()
        {
            core.Reset();
            return new UniTask(this, core.Version);
        }

        void Invoke()
        {
            asyncEnumerator?.SetResult();
            core.TrySetResult(AsyncUnit.Default);
        }

        static void CancellationCallback(object state)
        {
            var self = (AsyncUnityEventHandler)state;
            self.Dispose();

            // call child cancel
            if (self.asyncEnumerator != null)
            {
                self.asyncEnumerator.CancelFromParent(self.cancellationToken);
                self.asyncEnumerator = null;
            }

            self.core.TrySetCanceled(self.cancellationToken);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration.Dispose();
                if (unityEvent != null)
                {
                    unityEvent.RemoveListener(action);
                }
            }
        }

        UniTask IAsyncClickEventHandler.OnClickAsync()
        {
            return OnInvokeAsync();
        }

        void IUniTaskSource.GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                if (callOnce)
                {
                    Dispose();
                }
            }
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

        // AsyncEnumerator

        bool disableAutoClose;
        Enumerator asyncEnumerator;

        public AsyncUnityEventHandler DisableAutoClose()
        {
            disableAutoClose = true;
            return this;
        }

        IAsyncClickEventHandler IAsyncClickEventHandler.DisableAutoClose()
        {
            disableAutoClose = true;
            return this;
        }

        IUniTaskAsyncEnumerator<AsyncUnit> IUniTaskAsyncEnumerable<AsyncUnit>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            if (this.asyncEnumerator != null)
            {
                throw new InvalidOperationException("Already acquired GetAsyncEnumerator, does not allow get twice before previous enumerator completed.");
            }

            this.asyncEnumerator = new Enumerator(this, cancellationToken);
            return asyncEnumerator;
        }

        class Enumerator : Cysharp.Threading.Tasks.Linq.MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>
        {
            static Action<object> cancellationCallback = CancellationCallback;

            AsyncUnityEventHandler parent;
            CancellationToken cancellationToken;
            CancellationTokenRegistration registration;
            bool isDisposed;

            public Enumerator(AsyncUnityEventHandler parent, CancellationToken cancellationToken)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;

                if (cancellationToken.CanBeCanceled && parent.cancellationToken != cancellationToken)
                {
                    registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                }

                TaskTracker.TrackActiveTask(this, 3);
            }

            static void CancellationCallback(object state)
            {
                var self = (Enumerator)state;
                self.DisposeCore(self.cancellationToken);
            }

            public void CancelFromParent(CancellationToken cancellationToken)
            {
                // call from parent, avoid parent close.
                parent.disableAutoClose = true;
                DisposeCore(cancellationToken);
            }

            public void SetResult()
            {
                completionSource.TrySetResult(true);
            }

            public AsyncUnit Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                DisposeCore(CancellationToken.None);
                return default;
            }

            void DisposeCore(CancellationToken cancellationToken)
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    registration.Dispose();
                    TaskTracker.RemoveTracking(this);

                    if (!parent.disableAutoClose)
                    {
                        parent.Dispose(); // dispose parent.
                    }

                    if (parent.asyncEnumerator == this)
                    {
                        parent.asyncEnumerator = null;
                    }

                    try
                    {
                        completionSource.TrySetCanceled(cancellationToken);
                    }
                    catch (OperationCanceledException) { }
                }
            }
        }
    }

    public class AsyncUnityEventHandler<T> : IUniTaskSource<T>, IDisposable, IAsyncValueChangedEventHandler<T>, IAsyncEndEditEventHandler<T>, IUniTaskAsyncEnumerable<T>
    {
        static Action<object> cancellationCallback = CancellationCallback;

        readonly UnityAction<T> action;
        readonly UnityEvent<T> unityEvent;

        CancellationToken cancellationToken;
        CancellationTokenRegistration registration;
        bool isDisposed;
        bool callOnce;

        UniTaskCompletionSourceCore<T> core;

        public AsyncUnityEventHandler(UnityEvent<T> unityEvent, CancellationToken cancellationToken, bool callOnce)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                isDisposed = true;
                return;
            }

            this.action = Invoke;
            this.unityEvent = unityEvent;
            this.cancellationToken = cancellationToken;
            this.callOnce = callOnce;

            unityEvent.AddListener(action);

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
            }

            TaskTracker.TrackActiveTask(this, 3);
        }

        public UniTask<T> OnInvokeAsync()
        {
            core.Reset();
            return new UniTask<T>(this, core.Version);
        }

        void Invoke(T result)
        {
            asyncEnumerator?.SetResult(result);
            core.TrySetResult(result);
        }

        static void CancellationCallback(object state)
        {
            var self = (AsyncUnityEventHandler<T>)state;
            self.Dispose();

            // call child cancel
            if (self.asyncEnumerator != null)
            {
                self.asyncEnumerator.CancelFromParent(self.cancellationToken);
                self.asyncEnumerator = null;
            }

            self.core.TrySetCanceled(self.cancellationToken);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration.Dispose();
                if (unityEvent != null)
                {
                    unityEvent.RemoveListener(action);
                }

                asyncEnumerator?.DisposeAsync().Forget();
                try
                {
                    core.TrySetCanceled();
                }
                catch (OperationCanceledException) { }
            }
        }

        UniTask<T> IAsyncValueChangedEventHandler<T>.OnValueChangedAsync()
        {
            return OnInvokeAsync();
        }

        UniTask<T> IAsyncEndEditEventHandler<T>.OnEndEditAsync()
        {
            return OnInvokeAsync();
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

        // AsyncEnumerator

        bool disableAutoClose;
        Enumerator asyncEnumerator;

        public AsyncUnityEventHandler<T> DisableAutoClose()
        {
            disableAutoClose = true;
            return this;
        }

        IAsyncValueChangedEventHandler<T> IAsyncValueChangedEventHandler<T>.DisableAutoClose()
        {
            disableAutoClose = true;
            return this;
        }

        IAsyncEndEditEventHandler<T> IAsyncEndEditEventHandler<T>.DisableAutoClose()
        {
            disableAutoClose = true;
            return this;
        }

        IUniTaskAsyncEnumerator<T> IUniTaskAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            if (this.asyncEnumerator != null)
            {
                throw new InvalidOperationException("Already acquired GetAsyncEnumerator, does not allow get twice before previous enumerator completed.");
            }

            this.asyncEnumerator = new Enumerator(this, cancellationToken);
            return asyncEnumerator;
        }

        class Enumerator : Cysharp.Threading.Tasks.Linq.MoveNextSource, IUniTaskAsyncEnumerator<T>
        {
            static Action<object> cancellationCallback = CancellationCallback;

            AsyncUnityEventHandler<T> parent;
            CancellationToken cancellationToken;
            CancellationTokenRegistration registration;
            bool isDisposed;

            public Enumerator(AsyncUnityEventHandler<T> parent, CancellationToken cancellationToken)
            {
                this.parent = parent;
                this.cancellationToken = cancellationToken;

                if (cancellationToken.CanBeCanceled && parent.cancellationToken != cancellationToken)
                {
                    registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
                }

                TaskTracker.TrackActiveTask(this, 3);
            }

            static void CancellationCallback(object state)
            {
                var self = (Enumerator)state;
                self.DisposeCore(self.cancellationToken);
            }

            public void CancelFromParent(CancellationToken cancellationToken)
            {
                // call from parent, avoid parent close.
                parent.disableAutoClose = true;
                DisposeCore(cancellationToken);
            }

            public void SetResult(T result)
            {
                Current = result;
                completionSource.TrySetResult(true);
            }

            public T Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                DisposeCore(CancellationToken.None);
                return default;
            }

            void DisposeCore(CancellationToken cancellationToken)
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    registration.Dispose();
                    TaskTracker.RemoveTracking(this);

                    if (!parent.disableAutoClose)
                    {
                        parent.Dispose(); // dispose parent.
                    }

                    if (parent.asyncEnumerator == this)
                    {
                        parent.asyncEnumerator = null;
                    }

                    try
                    {
                        completionSource.TrySetCanceled(cancellationToken);
                    }
                    catch (OperationCanceledException) { }
                }
            }
        }
    }
}