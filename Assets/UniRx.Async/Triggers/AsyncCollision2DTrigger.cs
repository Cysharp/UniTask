
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncCollision2DTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<Collision2D> onCollisionEnter2D;
        AsyncTriggerPromiseDictionary<Collision2D> onCollisionEnter2Ds;
        AsyncTriggerPromise<Collision2D> onCollisionExit2D;
        AsyncTriggerPromiseDictionary<Collision2D> onCollisionExit2Ds;
        AsyncTriggerPromise<Collision2D> onCollisionStay2D;
        AsyncTriggerPromiseDictionary<Collision2D> onCollisionStay2Ds;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onCollisionEnter2D, onCollisionEnter2Ds, onCollisionExit2D, onCollisionExit2Ds, onCollisionStay2D, onCollisionStay2Ds);
        }


        void OnCollisionEnter2D(Collision2D coll)
        {
            TrySetResult(onCollisionEnter2D, onCollisionEnter2Ds, coll);
        }


        public UniTask<Collision2D> OnCollisionEnter2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCollisionEnter2D, ref onCollisionEnter2Ds, cancellationToken);
        }


        void OnCollisionExit2D(Collision2D coll)
        {
            TrySetResult(onCollisionExit2D, onCollisionExit2Ds, coll);
        }


        public UniTask<Collision2D> OnCollisionExit2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCollisionExit2D, ref onCollisionExit2Ds, cancellationToken);
        }


        void OnCollisionStay2D(Collision2D coll)
        {
            TrySetResult(onCollisionStay2D, onCollisionStay2Ds, coll);
        }


        public UniTask<Collision2D> OnCollisionStay2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCollisionStay2D, ref onCollisionStay2Ds, cancellationToken);
        }


    }
}

#endif

