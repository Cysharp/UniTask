
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncDropTrigger : AsyncTriggerBase, IDropHandler
    {
        AsyncTriggerPromise<PointerEventData> onDrop;
        AsyncTriggerPromiseDictionary<PointerEventData> onDrops;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onDrop, onDrops);
        }


        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            TrySetResult(onDrop, onDrops, eventData);
        }


        public UniTask<PointerEventData> OnDropAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onDrop, ref onDrops, cancellationToken);
        }


    }
}

#endif

