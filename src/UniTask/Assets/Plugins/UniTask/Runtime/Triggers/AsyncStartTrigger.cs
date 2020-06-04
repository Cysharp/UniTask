#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        public static AsyncStartTrigger GetAsyncStartTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncStartTrigger>(gameObject);
        }

        public static AsyncStartTrigger GetAsyncStartTrigger(this Component component)
        {
            return component.gameObject.GetAsyncStartTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncStartTrigger : AsyncTriggerBase<AsyncUnit>
    {
        bool called;

        void Start()
        {
            called = true;
            RaiseEvent(AsyncUnit.Default);
        }

        public UniTask StartAsync()
        {
            if (called) return UniTask.CompletedTask;

            return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(this, true)).OneShotAsync();
        }
    }
}