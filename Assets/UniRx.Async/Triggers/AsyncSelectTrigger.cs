
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncSelectTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<BaseEventData> onSelect;
        AsyncTriggerPromiseDictionary<BaseEventData> onSelects;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onSelect, onSelects);
        }


        void OnSelect(BaseEventData eventData)
        {
            TrySetResult(onSelect, onSelects, eventData);
        }


        public UniTask OnSelectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onSelect, ref onSelects, cancellationToken);
        }


    }
}

#endif

