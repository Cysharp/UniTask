
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncEnableDisableTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<AsyncUnit> onEnable;
        AsyncTriggerPromiseDictionary<AsyncUnit> onEnables;
        AsyncTriggerPromise<AsyncUnit> onDisable;
        AsyncTriggerPromiseDictionary<AsyncUnit> onDisables;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onEnable, onEnables, onDisable, onDisables);
        }


        void OnEnable()
        {
            TrySetResult(onEnable, onEnables, AsyncUnit.Default);
        }


        public UniTask OnEnableAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onEnable, ref onEnables, cancellationToken);
        }


        void OnDisable()
        {
            TrySetResult(onDisable, onDisables, AsyncUnit.Default);
        }


        public UniTask OnDisableAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onDisable, ref onDisables, cancellationToken);
        }


    }
}

#endif

