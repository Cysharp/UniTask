
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncJointTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<float> onJointBreak;
        AsyncTriggerPromiseDictionary<float> onJointBreaks;
        AsyncTriggerPromise<Joint2D> onJointBreak2D;
        AsyncTriggerPromiseDictionary<Joint2D> onJointBreak2Ds;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onJointBreak, onJointBreaks, onJointBreak2D, onJointBreak2Ds);
        }


        void OnJointBreak(float breakForce)
        {
            TrySetResult(onJointBreak, onJointBreaks, breakForce);
        }


        public UniTask OnJointBreakAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onJointBreak, ref onJointBreaks, cancellationToken);
        }


        void OnJointBreak2D(Joint2D brokenJoint)
        {
            TrySetResult(onJointBreak2D, onJointBreak2Ds, brokenJoint);
        }


        public UniTask OnJointBreak2DAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onJointBreak2D, ref onJointBreak2Ds, cancellationToken);
        }


    }
}

#endif

