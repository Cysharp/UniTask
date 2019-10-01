
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncPointerEnterTrigger : AsyncTriggerBase, IPointerEnterHandler
    {
        AsyncTriggerPromise<PointerEventData> onPointerEnter;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerEnters;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onPointerEnter, onPointerEnters);
        }


        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            TrySetResult(onPointerEnter, onPointerEnters, eventData);
        }


        public UniTask<PointerEventData> OnPointerEnterAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerEnter, ref onPointerEnters, cancellationToken);
        }


    }
}

#endif

