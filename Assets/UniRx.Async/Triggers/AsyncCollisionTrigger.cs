
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncCollisionTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<Collision> onCollisionEnter;
        AsyncTriggerPromiseDictionary<Collision> onCollisionEnters;
        AsyncTriggerPromise<Collision> onCollisionExit;
        AsyncTriggerPromiseDictionary<Collision> onCollisionExits;
        AsyncTriggerPromise<Collision> onCollisionStay;
        AsyncTriggerPromiseDictionary<Collision> onCollisionStays;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onCollisionEnter, onCollisionEnters, onCollisionExit, onCollisionExits, onCollisionStay, onCollisionStays);
        }


        void OnCollisionEnter(Collision collision)
        {
            TrySetResult(onCollisionEnter, onCollisionEnters, collision);
        }


        public UniTask OnCollisionEnterAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCollisionEnter, ref onCollisionEnters, cancellationToken);
        }


        void OnCollisionExit(Collision collisionInfo)
        {
            TrySetResult(onCollisionExit, onCollisionExits, collisionInfo);
        }


        public UniTask OnCollisionExitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCollisionExit, ref onCollisionExits, cancellationToken);
        }


        void OnCollisionStay(Collision collisionInfo)
        {
            TrySetResult(onCollisionStay, onCollisionStays, collisionInfo);
        }


        public UniTask OnCollisionStayAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCollisionStay, ref onCollisionStays, cancellationToken);
        }


    }
}

#endif

