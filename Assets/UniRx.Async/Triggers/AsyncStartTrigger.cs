#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncStartTrigger : MonoBehaviour
    {
        bool awakeCalled = false;
        bool called = false;
        UniTaskCompletionSource promise;

        void Awake()
        {
            awakeCalled = true;
        }

        void Start()
        {
            called = true;
            promise?.TrySetResult();
        }

        public UniTask StartAsync()
        {
            if (called) return UniTask.CompletedTask;
            if (!awakeCalled)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AwakeMonitor(this));
            }
            return new UniTask(promise ?? (promise = new UniTaskCompletionSource()));
        }

        private void OnDestroy()
        {
            promise?.TrySetCanceled();
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
                if (trigger.awakeCalled) return false;
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

#endif