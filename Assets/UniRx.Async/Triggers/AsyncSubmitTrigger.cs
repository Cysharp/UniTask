
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncSubmitTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<BaseEventData> onSubmit;
        AsyncTriggerPromiseDictionary<BaseEventData> onSubmits;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onSubmit, onSubmits);
        }


        void OnSubmit(BaseEventData eventData)
        {
            TrySetResult(onSubmit, onSubmits, eventData);
        }


        public UniTask<BaseEventData> OnSubmitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onSubmit, ref onSubmits, cancellationToken);
        }


    }
}

#endif

