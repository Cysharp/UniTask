#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAwakeTrigger>(gameObject);
        }

        public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAwakeTrigger();
        }
    }

    [DisallowMultipleComponent]
    public class AsyncAwakeTrigger : MonoBehaviour
    {
        bool called = false;
        TriggerEvent<AsyncUnit> triggerEvent;

        void Awake()
        {
            called = true;
            triggerEvent?.TrySetResult(AsyncUnit.Default);
            triggerEvent = null;
        }

        public UniTask AwakeAsync()
        {
            if (called) return UniTask.CompletedTask;

            PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AwakeMonitor(this));

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
            readonly AsyncAwakeTrigger trigger;

            public AwakeMonitor(AsyncAwakeTrigger trigger)
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

