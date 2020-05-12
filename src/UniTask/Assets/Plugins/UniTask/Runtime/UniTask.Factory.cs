#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        static readonly UniTask CanceledUniTask = new Func<UniTask>(() =>
        {
            var promise = new UniTaskCompletionSource();
            promise.TrySetCanceled(CancellationToken.None);
            promise.MarkHandled();
            return promise.Task;
        })();

        static class CanceledUniTaskCache<T>
        {
            public static readonly UniTask<T> Task;

            static CanceledUniTaskCache()
            {
                var promise = new UniTaskCompletionSource<T>();
                promise.TrySetCanceled(CancellationToken.None);
                promise.MarkHandled();
                Task = promise.Task;
            }
        }

        public static readonly UniTask CompletedTask = new UniTask();

        public static UniTask FromException(Exception ex)
        {
            var promise = new UniTaskCompletionSource();
            promise.TrySetException(ex);
            promise.MarkHandled();
            return promise.Task;
        }

        public static UniTask<T> FromException<T>(Exception ex)
        {
            var promise = new UniTaskCompletionSource<T>();
            promise.TrySetException(ex);
            promise.MarkHandled();
            return promise.Task;
        }

        public static UniTask<T> FromResult<T>(T value)
        {
            return new UniTask<T>(value);
        }

        public static UniTask FromCanceled(CancellationToken cancellationToken = default)
        {
            if (cancellationToken == CancellationToken.None)
            {
                return CanceledUniTask;
            }
            else
            {
                var promise = new UniTaskCompletionSource();
                promise.TrySetCanceled(cancellationToken);
                promise.MarkHandled();
                return promise.Task;
            }
        }

        public static UniTask<T> FromCanceled<T>(CancellationToken cancellationToken = default)
        {
            if (cancellationToken == CancellationToken.None)
            {
                return CanceledUniTaskCache<T>.Task;
            }
            else
            {
                var promise = new UniTaskCompletionSource<T>();
                promise.TrySetCanceled(cancellationToken);
                promise.MarkHandled();
                return promise.Task;
            }
        }

        public static UniTask Create(Func<UniTask> factory)
        {
            return factory();
        }

        public static UniTask<T> Create<T>(Func<UniTask<T>> factory)
        {
            return factory();
        }

        public static AsyncLazy Lazy(Func<UniTask> factory)
        {
            return new AsyncLazy(factory);
        }

        public static AsyncLazy<T> Lazy<T>(Func<UniTask<T>> factory)
        {
            return new AsyncLazy<T>(factory);
        }

        /// <summary>
        /// helper of create add UniTaskVoid to delegate.
        /// For example: FooEvent += () => UniTask.Void(async () => { /* */ })
        /// </summary>
        public static void Void(Func<UniTask> asyncAction)
        {
            asyncAction().Forget();
        }

        public static Action VoidAction(Func<UniTask> asyncAction)
        {
            return () => Void(asyncAction);
        }

#if UNITY_2018_3_OR_NEWER

        public static UnityEngine.Events.UnityAction VoidUnityAction(Func<UniTask> asyncAction)
        {
            return () => Void(asyncAction);
        }

#endif

        /// <summary>
        /// helper of create add UniTaskVoid to delegate.
        /// For example: FooEvent += (sender, e) => UniTask.Void(async arg => { /* */ }, (sender, e))
        /// </summary>
        public static void Void<T>(Func<T, UniTask> asyncAction, T state)
        {
            asyncAction(state).Forget();
        }
    }

    internal static class CompletedTasks
    {
        public static readonly UniTask<AsyncUnit> AsyncUnit = UniTask.FromResult(Cysharp.Threading.Tasks.AsyncUnit.Default);
        public static readonly UniTask<bool> True = UniTask.FromResult(true);
        public static readonly UniTask<bool> False = UniTask.FromResult(false);
        public static readonly UniTask<int> Zero = UniTask.FromResult(0);
        public static readonly UniTask<int> MinusOne = UniTask.FromResult(-1);
        public static readonly UniTask<int> One = UniTask.FromResult(1);
    }
}
