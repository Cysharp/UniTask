using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        /// <summary>Run action on the threadPool and return to current SynchronizationContext if configureAwait = true.</summary>
        public static async UniTask Run(Action action, bool configureAwait = true)
        {
            if (configureAwait)
            {
                var current = SynchronizationContext.Current;
                await UniTask.SwitchToThreadPool();
                try
                {
                    action();
                }
                finally
                {
                    if (current != null)
                    {
                        await UniTask.SwitchToSynchronizationContext(current);
                    }
                }
            }
            else
            {
                await UniTask.SwitchToThreadPool();
                action();
            }
        }

        /// <summary>Run action on the threadPool and return to current SynchronizationContext if configureAwait = true.</summary>
        public static async UniTask Run(Action<object> action, object state, bool configureAwait = true)
        {
            if (configureAwait)
            {
                var current = SynchronizationContext.Current;
                await UniTask.SwitchToThreadPool();
                try
                {
                    action(state);
                }
                finally
                {
                    if (current != null)
                    {
                        await UniTask.SwitchToSynchronizationContext(current);
                    }
                }
            }
            else
            {
                await UniTask.SwitchToThreadPool();
                action(state);
            }
        }

        /// <summary>Run action on the threadPool and return to current SynchronizationContext if configureAwait = true.</summary>
        public static async UniTask<T> Run<T>(Func<T> func, bool configureAwait = true)
        {
            if (configureAwait)
            {
                var current = SynchronizationContext.Current;
                await UniTask.SwitchToThreadPool();
                try
                {
                    return func();
                }
                finally
                {
                    if (current != null)
                    {
                        await UniTask.SwitchToSynchronizationContext(current);
                    }
                }
            }
            else
            {
                await UniTask.SwitchToThreadPool();
                return func();
            }
        }

        /// <summary>Run action on the threadPool and return to current SynchronizationContext if configureAwait = true.</summary>
        public static async UniTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true)
        {
            if (configureAwait)
            {
                var current = SynchronizationContext.Current;
                await UniTask.SwitchToThreadPool();
                try
                {
                    return func(state);
                }
                finally
                {
                    if (current != null)
                    {
                        await UniTask.SwitchToSynchronizationContext(current);
                    }
                }
            }
            else
            {
                await UniTask.SwitchToThreadPool();
                return func(state);
            }
        }
    }
}