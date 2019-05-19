
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncEventTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<BaseEventData> onDeselect;
        AsyncTriggerPromiseDictionary<BaseEventData> onDeselects;
        AsyncTriggerPromise<AxisEventData> onMove;
        AsyncTriggerPromiseDictionary<AxisEventData> onMoves;
        AsyncTriggerPromise<PointerEventData> onPointerDown;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerDowns;
        AsyncTriggerPromise<PointerEventData> onPointerEnter;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerEnters;
        AsyncTriggerPromise<PointerEventData> onPointerExit;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerExits;
        AsyncTriggerPromise<PointerEventData> onPointerUp;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerUps;
        AsyncTriggerPromise<BaseEventData> onSelect;
        AsyncTriggerPromiseDictionary<BaseEventData> onSelects;
        AsyncTriggerPromise<PointerEventData> onPointerClick;
        AsyncTriggerPromiseDictionary<PointerEventData> onPointerClicks;
        AsyncTriggerPromise<BaseEventData> onSubmit;
        AsyncTriggerPromiseDictionary<BaseEventData> onSubmits;
        AsyncTriggerPromise<PointerEventData> onDrag;
        AsyncTriggerPromiseDictionary<PointerEventData> onDrags;
        AsyncTriggerPromise<PointerEventData> onBeginDrag;
        AsyncTriggerPromiseDictionary<PointerEventData> onBeginDrags;
        AsyncTriggerPromise<PointerEventData> onEndDrag;
        AsyncTriggerPromiseDictionary<PointerEventData> onEndDrags;
        AsyncTriggerPromise<PointerEventData> onDrop;
        AsyncTriggerPromiseDictionary<PointerEventData> onDrops;
        AsyncTriggerPromise<BaseEventData> onUpdateSelected;
        AsyncTriggerPromiseDictionary<BaseEventData> onUpdateSelecteds;
        AsyncTriggerPromise<PointerEventData> onInitializePotentialDrag;
        AsyncTriggerPromiseDictionary<PointerEventData> onInitializePotentialDrags;
        AsyncTriggerPromise<BaseEventData> onCancel;
        AsyncTriggerPromiseDictionary<BaseEventData> onCancels;
        AsyncTriggerPromise<PointerEventData> onScroll;
        AsyncTriggerPromiseDictionary<PointerEventData> onScrolls;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(onDeselect, onDeselects, onMove, onMoves, onPointerDown, onPointerDowns, onPointerEnter, onPointerEnters, onPointerExit, onPointerExits, onPointerUp, onPointerUps, onSelect, onSelects, onPointerClick, onPointerClicks, onSubmit, onSubmits, onDrag, onDrags, onBeginDrag, onBeginDrags, onEndDrag, onEndDrags, onDrop, onDrops, onUpdateSelected, onUpdateSelecteds, onInitializePotentialDrag, onInitializePotentialDrags, onCancel, onCancels, onScroll, onScrolls);
        }

        void OnDeselect(BaseEventData eventData)
        {
            TrySetResult(onDeselect, onDeselects, eventData);
        }


        public UniTask OnDeselectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onDeselect, ref onDeselects, cancellationToken);
        }


        void OnMove(AxisEventData eventData)
        {
            TrySetResult(onMove, onMoves, eventData);
        }


        public UniTask OnMoveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onMove, ref onMoves, cancellationToken);
        }


        void OnPointerDown(PointerEventData eventData)
        {
            TrySetResult(onPointerDown, onPointerDowns, eventData);
        }


        public UniTask OnPointerDownAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerDown, ref onPointerDowns, cancellationToken);
        }


        void OnPointerEnter(PointerEventData eventData)
        {
            TrySetResult(onPointerEnter, onPointerEnters, eventData);
        }


        public UniTask OnPointerEnterAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerEnter, ref onPointerEnters, cancellationToken);
        }


        void OnPointerExit(PointerEventData eventData)
        {
            TrySetResult(onPointerExit, onPointerExits, eventData);
        }


        public UniTask OnPointerExitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerExit, ref onPointerExits, cancellationToken);
        }


        void OnPointerUp(PointerEventData eventData)
        {
            TrySetResult(onPointerUp, onPointerUps, eventData);
        }


        public UniTask OnPointerUpAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerUp, ref onPointerUps, cancellationToken);
        }


        void OnSelect(BaseEventData eventData)
        {
            TrySetResult(onSelect, onSelects, eventData);
        }


        public UniTask OnSelectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onSelect, ref onSelects, cancellationToken);
        }


        void OnPointerClick(PointerEventData eventData)
        {
            TrySetResult(onPointerClick, onPointerClicks, eventData);
        }


        public UniTask OnPointerClickAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onPointerClick, ref onPointerClicks, cancellationToken);
        }


        void OnSubmit(BaseEventData eventData)
        {
            TrySetResult(onSubmit, onSubmits, eventData);
        }


        public UniTask OnSubmitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onSubmit, ref onSubmits, cancellationToken);
        }


        void OnDrag(PointerEventData eventData)
        {
            TrySetResult(onDrag, onDrags, eventData);
        }


        public UniTask OnDragAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onDrag, ref onDrags, cancellationToken);
        }


        void OnBeginDrag(PointerEventData eventData)
        {
            TrySetResult(onBeginDrag, onBeginDrags, eventData);
        }


        public UniTask OnBeginDragAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onBeginDrag, ref onBeginDrags, cancellationToken);
        }


        void OnEndDrag(PointerEventData eventData)
        {
            TrySetResult(onEndDrag, onEndDrags, eventData);
        }


        public UniTask OnEndDragAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onEndDrag, ref onEndDrags, cancellationToken);
        }


        void OnDrop(PointerEventData eventData)
        {
            TrySetResult(onDrop, onDrops, eventData);
        }


        public UniTask OnDropAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onDrop, ref onDrops, cancellationToken);
        }


        void OnUpdateSelected(BaseEventData eventData)
        {
            TrySetResult(onUpdateSelected, onUpdateSelecteds, eventData);
        }


        public UniTask OnUpdateSelectedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onUpdateSelected, ref onUpdateSelecteds, cancellationToken);
        }


        void OnInitializePotentialDrag(PointerEventData eventData)
        {
            TrySetResult(onInitializePotentialDrag, onInitializePotentialDrags, eventData);
        }


        public UniTask OnInitializePotentialDragAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onInitializePotentialDrag, ref onInitializePotentialDrags, cancellationToken);
        }


        void OnCancel(BaseEventData eventData)
        {
            TrySetResult(onCancel, onCancels, eventData);
        }


        public UniTask OnCancelAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onCancel, ref onCancels, cancellationToken);
        }


        void OnScroll(PointerEventData eventData)
        {
            TrySetResult(onScroll, onScrolls, eventData);
        }


        public UniTask OnScrollAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref onScroll, ref onScrolls, cancellationToken);
        }


    }
}

#endif

