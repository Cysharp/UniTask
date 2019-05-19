
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncMouseTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<AsyncUnit> onMouseDown;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseDowns;
        AsyncTriggerPromise<AsyncUnit> onMouseDrag;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseDrags;
        AsyncTriggerPromise<AsyncUnit> onMouseEnter;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseEnters;
        AsyncTriggerPromise<AsyncUnit> onMouseExit;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseExits;
        AsyncTriggerPromise<AsyncUnit> onMouseOver;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseOvers;
        AsyncTriggerPromise<AsyncUnit> onMouseUp;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseUps;
        AsyncTriggerPromise<AsyncUnit> onMouseUpAsButton;
        AsyncTriggerPromiseDictionary<AsyncUnit> onMouseUpAsButtons;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onMouseDown, onMouseDowns, onMouseDrag, onMouseDrags, onMouseEnter, onMouseEnters, onMouseExit, onMouseExits, onMouseOver, onMouseOvers, onMouseUp, onMouseUps, onMouseUpAsButton, onMouseUpAsButtons);
        }


        void OnMouseDown()
        {
            TrySetResult(onMouseDown, onMouseDowns, AsyncUnit.Default);
        }


        public UniTask OnMouseDownAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseDown, ref onMouseDowns, cancellationToken);
        }


        void OnMouseDrag()
        {
            TrySetResult(onMouseDrag, onMouseDrags, AsyncUnit.Default);
        }


        public UniTask OnMouseDragAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseDrag, ref onMouseDrags, cancellationToken);
        }


        void OnMouseEnter()
        {
            TrySetResult(onMouseEnter, onMouseEnters, AsyncUnit.Default);
        }


        public UniTask OnMouseEnterAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseEnter, ref onMouseEnters, cancellationToken);
        }


        void OnMouseExit()
        {
            TrySetResult(onMouseExit, onMouseExits, AsyncUnit.Default);
        }


        public UniTask OnMouseExitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseExit, ref onMouseExits, cancellationToken);
        }


        void OnMouseOver()
        {
            TrySetResult(onMouseOver, onMouseOvers, AsyncUnit.Default);
        }


        public UniTask OnMouseOverAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseOver, ref onMouseOvers, cancellationToken);
        }


        void OnMouseUp()
        {
            TrySetResult(onMouseUp, onMouseUps, AsyncUnit.Default);
        }


        public UniTask OnMouseUpAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseUp, ref onMouseUps, cancellationToken);
        }


        void OnMouseUpAsButton()
        {
            TrySetResult(onMouseUpAsButton, onMouseUpAsButtons, AsyncUnit.Default);
        }


        public UniTask OnMouseUpAsButtonAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMouseUpAsButton, ref onMouseUpAsButtons, cancellationToken);
        }


    }
}

#endif


#endif
