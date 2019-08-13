
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncUpdateSelectedTrigger : AsyncTriggerBase, IUpdateSelectedHandler
    {
        AsyncTriggerPromise<BaseEventData> onUpdateSelected;
        AsyncTriggerPromiseDictionary<BaseEventData> onUpdateSelecteds;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onUpdateSelected, onUpdateSelecteds);
        }


        void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
        {
            TrySetResult(onUpdateSelected, onUpdateSelecteds, eventData);
        }


        public UniTask<BaseEventData> OnUpdateSelectedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onUpdateSelected, ref onUpdateSelecteds, cancellationToken);
        }


    }
}

#endif

