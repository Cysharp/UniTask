
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncTransformChangedTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<AsyncUnit> onBeforeTransformParentChanged;
        AsyncTriggerPromiseDictionary<AsyncUnit> onBeforeTransformParentChangeds;
        AsyncTriggerPromise<AsyncUnit> onTransformParentChanged;
        AsyncTriggerPromiseDictionary<AsyncUnit> onTransformParentChangeds;
        AsyncTriggerPromise<AsyncUnit> onTransformChildrenChanged;
        AsyncTriggerPromiseDictionary<AsyncUnit> onTransformChildrenChangeds;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onBeforeTransformParentChanged, onBeforeTransformParentChangeds, onTransformParentChanged, onTransformParentChangeds, onTransformChildrenChanged, onTransformChildrenChangeds);
        }


        void OnBeforeTransformParentChanged()
        {
            TrySetResult(onBeforeTransformParentChanged, onBeforeTransformParentChangeds, AsyncUnit.Default);
        }


        public UniTask OnBeforeTransformParentChangedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onBeforeTransformParentChanged, ref onBeforeTransformParentChangeds, cancellationToken);
        }


        void OnTransformParentChanged()
        {
            TrySetResult(onTransformParentChanged, onTransformParentChangeds, AsyncUnit.Default);
        }


        public UniTask OnTransformParentChangedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTransformParentChanged, ref onTransformParentChangeds, cancellationToken);
        }


        void OnTransformChildrenChanged()
        {
            TrySetResult(onTransformChildrenChanged, onTransformChildrenChangeds, AsyncUnit.Default);
        }


        public UniTask OnTransformChildrenChangedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTransformChildrenChanged, ref onTransformChildrenChangeds, cancellationToken);
        }


    }
}

#endif

