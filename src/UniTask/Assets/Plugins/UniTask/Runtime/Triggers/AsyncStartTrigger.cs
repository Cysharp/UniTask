#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
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
    public class AsyncStartTrigger : MonoBehaviour
    {
        bool awakeCalled = false;
        bool called = false;
        TriggerEvent<AsyncUnit> triggerEvent;

        void Awake()
        {
            awakeCalled = true;
        }

        void Start()
        {
            called = true;
            triggerEvent?.TrySetResult(AsyncUnit.Default);
            triggerEvent = null;
        }

        public UniTask StartAsync()
        {
            if (called) return UniTask.CompletedTask;

            if (!awakeCalled)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AwakeMonitor(this));
            }

            if (triggerEvent == null)
            {
                triggerEvent = new TriggerEvent<AsyncUnit>();
            }

            return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(triggerEvent, true)).OneShotAsync();
        }

        private void OnDestroy()
        {
            triggerEvent?.TrySetCanceled(CancellationToken.None);
        }

        class AwakeMonitor : IPlayerLoopItem
        {
            readonly AsyncStartTrigger trigger;

            public AwakeMonitor(AsyncStartTrigger trigger)
            {
                this.trigger = trigger;
            }

            public bool MoveNext()
            {
                if (trigger.called) return false;
                if (trigger == null)
                {
                    trigger.OnDestroy();
                    return false;
                }
                return true;
            }
        }
    }

}

