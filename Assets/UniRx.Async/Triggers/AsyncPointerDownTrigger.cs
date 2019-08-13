
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncPointerDownTrigger : AsyncTriggerBase, IPointerDownHandler
    {
        AsyncTriggerPromise<PointerEventData> onPointerDown;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerDowns;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onPointerDown, onPointerDowns);
        }


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            TrySetResult(onPointerDown, onPointerDowns, eventData);
        }


        public UniTask<PointerEventData> OnPointerDownAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerDown, ref onPointerDowns, cancellationToken);
        }


    }
}

#endif

