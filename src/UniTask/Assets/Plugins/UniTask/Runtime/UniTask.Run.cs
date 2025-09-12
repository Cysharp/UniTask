#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        #region OBSOLETE_RUN

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask Run(Action action, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(action, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask Run(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(action, state, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask Run(Func<UniTask> action, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(action, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask Run(Func<object, UniTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(action, state, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask<T> Run<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(func, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask<T> Run<T>(Func<UniTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(func, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(func, state, configureAwait, cancellationToken);
        }

        [Obsolete("UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.")]
        public static UniTask<T> Run<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            return RunOnThreadPool(func, state, configureAwait, cancellationToken);
        }

        #endregion

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask RunOnThreadPool(Action action, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    action();
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                action();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask RunOnThreadPool(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    action(state);
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                action(state);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask RunOnThreadPool(Func<UniTask> action, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await action();
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                await action();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask RunOnThreadPool(Func<object, UniTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await action(state);
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                await action(state);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> RunOnThreadPool<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return func();
                }
                finally
                {
                    await UniTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                return func();
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> RunOnThreadPool<T>(Func<UniTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return await func();
                }
                finally
                {
                    await UniTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                var result = await func();
                cancellationToken.ThrowIfCancellationRequested();
                return result;
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> RunOnThreadPool<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return func(state);
                }
                finally
                {
                    await UniTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                return func(state);
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> RunOnThreadPool<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return await func(state);
                }
                finally
                {
                    await UniTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                var result = await func(state);
                cancellationToken.ThrowIfCancellationRequested();
                return result;
            }
        }
    }
}

