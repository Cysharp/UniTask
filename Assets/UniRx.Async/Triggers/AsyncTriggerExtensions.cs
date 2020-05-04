#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
using UniRx.Async.Triggers;

namespace UniRx.Async
{
    public static class UniTaskCancellationExtensions
    {
        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this GameObject gameObject)
        {
            return gameObject.GetAsyncDestroyTrigger().CancellationToken;
        }

        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this Component component)
        {
            return component.GetAsyncDestroyTrigger().CancellationToken;
        }
    }
}

namespace UniRx.Async.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        // Util.

        static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        // Special for single operation.

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        public static UniTask OnDestroyAsync(this GameObject gameObject)
        {
            return gameObject.GetAsyncDestroyTrigger().OnDestroyAsync();
        }

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        public static UniTask OnDestroyAsync(this Component component)
        {
            return component.GetAsyncDestroyTrigger().OnDestroyAsync();
        }

        public static UniTask StartAsync(this GameObject gameObject)
        {
            return gameObject.GetAsyncStartTrigger().StartAsync();
        }

        public static UniTask StartAsync(this Component component)
        {
            return component.GetAsyncStartTrigger().StartAsync();
        }

        public static UniTask AwakeAsync(this GameObject gameObject)
        {
            return gameObject.GetAsyncAwakeTrigger().AwakeAsync();
        }

        public static UniTask AwakeAsync(this Component component)
        {
            return component.GetAsyncAwakeTrigger().AwakeAsync();
        }
    }
}

#endif