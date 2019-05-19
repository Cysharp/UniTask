
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncTrigger2DTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<Collider2D> onTriggerEnter2D;
        AsyncTriggerPromiseDictionary<Collider2D> onTriggerEnter2Ds;
        AsyncTriggerPromise<Collider2D> onTriggerExit2D;
        AsyncTriggerPromiseDictionary<Collider2D> onTriggerExit2Ds;
        AsyncTriggerPromise<Collider2D> onTriggerStay2D;
        AsyncTriggerPromiseDictionary<Collider2D> onTriggerStay2Ds;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onTriggerEnter2D, onTriggerEnter2Ds, onTriggerExit2D, onTriggerExit2Ds, onTriggerStay2D, onTriggerStay2Ds);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            TrySetResult(onTriggerEnter2D, onTriggerEnter2Ds, other);
        }


        public UniTask OnTriggerEnter2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTriggerEnter2D, ref onTriggerEnter2Ds, cancellationToken);
        }


        void OnTriggerExit2D(Collider2D other)
        {
            TrySetResult(onTriggerExit2D, onTriggerExit2Ds, other);
        }


        public UniTask OnTriggerExit2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTriggerExit2D, ref onTriggerExit2Ds, cancellationToken);
        }


        void OnTriggerStay2D(Collider2D other)
        {
            TrySetResult(onTriggerStay2D, onTriggerStay2Ds, other);
        }


        public UniTask OnTriggerStay2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onTriggerStay2D, ref onTriggerStay2Ds, cancellationToken);
        }


    }
}

#endif

