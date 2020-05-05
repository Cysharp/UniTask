#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDestroyTrigger>(gameObject);
        }

        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDestroyTrigger();
        }
    }

    [DisallowMultipleComponent]
    public class AsyncDestroyTrigger : MonoBehaviour
    {
        bool awakeCalled = false;
        bool called = false;
        TriggerEvent<AsyncUnit> triggerEvent;
        CancellationTokenSource cancellationTokenSource;

        public CancellationToken CancellationToken
        {
            get
            {
                if (cancellationTokenSource == null)
                {
                    cancellationTokenSource = new CancellationTokenSource();
                }
                return cancellationTokenSource.Token;
            }
        }

        void Awake()
        {
            awakeCalled = true;
        }

        void OnDestroy()
        {
            called = true;

            triggerEvent?.TrySetResult(AsyncUnit.Default);
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();

            triggerEvent = null;
        }

        public UniTask OnDestroyAsync()
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

        class AwakeMonitor : IPlayerLoopItem
        {
            readonly AsyncDestroyTrigger trigger;

            public AwakeMonitor(AsyncDestroyTrigger trigger)
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

