
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncDeselectTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<BaseEventData> onDeselect;
        AsyncTriggerPromiseDictionary<BaseEventData> onDeselects;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onDeselect, onDeselects);
        }


        void OnDeselect(BaseEventData eventData)
        {
            TrySetResult(onDeselect, onDeselects, eventData);
        }


        public UniTask OnDeselectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onDeselect, ref onDeselects, cancellationToken);
        }


    }
}

#endif

