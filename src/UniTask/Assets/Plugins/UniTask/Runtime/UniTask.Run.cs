#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask Run(Action action, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    action();
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                action();
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask Run(Action<object> action, object state, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    action(state);
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                action(state);
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask Run(Func<UniTask> action, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    await action();
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                await action();
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask Run(Func<object, UniTask> action, object state, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    await action(state);
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                await action(state);
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> Run<T>(Func<T> func, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();
            if (configureAwait)
            {
                try
                {
                    return func();
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                return func();
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> Run<T>(Func<UniTask<T>> func, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();
            if (configureAwait)
            {
                try
                {
                    return await func();
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                return await func();
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    return func(state);
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                return func(state);
            }
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async UniTask<T> Run<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true)
        {
            await UniTask.SwitchToThreadPool();

            if (configureAwait)
            {
                try
                {
                    return await func(state);
                }
                finally
                {
                    await UniTask.Yield();
                }
            }
            else
            {
                return await func(state);
            }
        }
    }
}

