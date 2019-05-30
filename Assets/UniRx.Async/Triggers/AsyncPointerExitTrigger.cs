
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncPointerExitTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<PointerEventData> onPointerExit;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerExits;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onPointerExit, onPointerExits);
        }


        void OnPointerExit(PointerEventData eventData)
        {
            TrySetResult(onPointerExit, onPointerExits, eventData);
        }


        public UniTask<PointerEventData> OnPointerExitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerExit, ref onPointerExits, cancellationToken);
        }


    }
}

#endif

