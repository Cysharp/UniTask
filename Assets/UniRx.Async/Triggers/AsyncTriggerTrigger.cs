
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncTriggerTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<Collider> onTriggerEnter;
        AsyncTriggerPromiseDictionary<Collider> onTriggerEnters;
        AsyncTriggerPromise<Collider> onTriggerExit;
        AsyncTriggerPromiseDictionary<Collider> onTriggerExits;
        AsyncTriggerPromise<Collider> onTriggerStay;
        AsyncTriggerPromiseDictionary<Collider> onTriggerStays;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onTriggerEnter, onTriggerEnters, onTriggerExit, onTriggerExits, onTriggerStay, onTriggerStays);
        }


        void OnTriggerEnter(Collider other)
        {
            TrySetResult(onTriggerEnter, onTriggerEnters, other);
        }


        public UniTask OnTriggerEnterAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTriggerEnter, ref onTriggerEnters, cancellationToken);
        }


        void OnTriggerExit(Collider other)
        {
            TrySetResult(onTriggerExit, onTriggerExits, other);
        }


        public UniTask OnTriggerExitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTriggerExit, ref onTriggerExits, cancellationToken);
        }


        void OnTriggerStay(Collider other)
        {
            TrySetResult(onTriggerStay, onTriggerStays, other);
        }


        public UniTask OnTriggerStayAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTriggerStay, ref onTriggerStays, cancellationToken);
        }


    }
}

#endif

