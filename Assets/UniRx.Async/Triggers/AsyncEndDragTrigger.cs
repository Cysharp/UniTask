
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncEndDragTrigger : AsyncTriggerBase, IEndDragHandler
    {
        AsyncTriggerPromise<PointerEventData> onEndDrag;
        AsyncTriggerPromiseDictionary<PointerEventData> onEndDrags;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onEndDrag, onEndDrags);
        }


        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            TrySetResult(onEndDrag, onEndDrags, eventData);
        }


        public UniTask<PointerEventData> OnEndDragAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onEndDrag, ref onEndDrags, cancellationToken);
        }


    }
}

#endif

