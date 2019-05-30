
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncPointerUpTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<PointerEventData> onPointerUp;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerUps;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onPointerUp, onPointerUps);
        }


        void OnPointerUp(PointerEventData eventData)
        {
            TrySetResult(onPointerUp, onPointerUps, eventData);
        }


        public UniTask<PointerEventData> OnPointerUpAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerUp, ref onPointerUps, cancellationToken);
        }


    }
}

#endif

