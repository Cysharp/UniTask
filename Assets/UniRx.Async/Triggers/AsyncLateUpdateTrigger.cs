
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncLateUpdateTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<AsyncUnit> lateUpdate;
        AsyncTriggerPromiseDictionary<AsyncUnit> lateUpdates;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(lateUpdate, lateUpdates);
        }


        void LateUpdate()
        {
            TrySetResult(lateUpdate, lateUpdates, AsyncUnit.Default);
        }


        public UniTask LateUpdateAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref lateUpdate, ref lateUpdates, cancellationToken);
        }


    }
}

#endif

