#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;

namespace UniRx.Async
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
    }
}

#endif