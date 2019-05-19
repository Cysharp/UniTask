
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncVisibleTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<AsyncUnit> onBecameInvisible;
        AsyncTriggerPromiseDictionary<AsyncUnit> onBecameInvisibles;
        AsyncTriggerPromise<AsyncUnit> onBecameVisible;
        AsyncTriggerPromiseDictionary<AsyncUnit> onBecameVisibles;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onBecameInvisible, onBecameInvisibles, onBecameVisible, onBecameVisibles);
        }


        void OnBecameInvisible()
        {
            TrySetResult(onBecameInvisible, onBecameInvisibles, AsyncUnit.Default);
        }


        public UniTask OnBecameInvisibleAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onBecameInvisible, ref onBecameInvisibles, cancellationToken);
        }


        void OnBecameVisible()
        {
            TrySetResult(onBecameVisible, onBecameVisibles, AsyncUnit.Default);
        }


        public UniTask OnBecameVisibleAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onBecameVisible, ref onBecameVisibles, cancellationToken);
        }


    }
}

#endif

