
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncParticleTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<GameObject> onParticleCollision;
        AsyncTriggerPromiseDictionary<GameObject> onParticleCollisions;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onParticleCollision, onParticleCollisions);
        }


        void OnParticleCollision(GameObject other)
        {
            TrySetResult(onParticleCollision, onParticleCollisions, other);
        }


        public UniTask OnParticleCollisionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onParticleCollision, ref onParticleCollisions, cancellationToken);
        }


    }
}

#endif

