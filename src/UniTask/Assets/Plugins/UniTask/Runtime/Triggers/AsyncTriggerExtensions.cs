#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks.Triggers;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskCancellationExtensions
    {
#if UNITY_2022_2_OR_NEWER

        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.destroyCancellationToken;
        }

#endif

        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this GameObject gameObject)
        {
            return gameObject.GetAsyncDestroyTrigger().CancellationToken;
        }

        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this Component component)
        {
#if UNITY_2022_2_OR_NEWER
            if (component is MonoBehaviour mb)
            {
                return mb.destroyCancellationToken;
            }
#endif

            return component.GetAsyncDestroyTrigger().CancellationToken;
        }
    }
}

namespace Cysharp.Threading.Tasks.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        // Util.

        static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
#if UNITY_2019_2_OR_NEWER
            if (!gameObject.TryGetComponent<T>(out var component))
            {
                component = gameObject.AddComponent<T>();
            }
#else
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
#endif

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

