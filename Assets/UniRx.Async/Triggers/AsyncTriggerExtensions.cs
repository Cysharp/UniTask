#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
using UniRx.Async.Triggers;

namespace UniRx.Async
{
    public static class UniTaskCancellationExtensions
    {
        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this GameObject gameObject)
        {
            return gameObject.GetAsyncDestroyTrigger().CancellationToken;
        }

        /// <summary>This CancellationToken is canceled when the MonoBehaviour will be destroyed.</summary>
        public static CancellationToken GetCancellationTokenOnDestroy(this Component component)
        {
            return component.GetAsyncDestroyTrigger().CancellationToken;
        }
    }
}

namespace UniRx.Async.Triggers
{
    public static class AsyncTriggerExtensions
    {
        // Util.

        static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        // Special for single operation.

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        public static UniTask OnDestroyAsync(this GameObject gameObject)
        {
            return gameObject.GetAsyncDestroyTrigger().OnDestroyAsync();
        }

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        public static UniTask OnDestroyAsync(this Component component)
        {
            return component.GetAsyncDestroyTrigger().OnDestroyAsync();
        }

        public static UniTask StartAsync(this GameObject gameObject)
        {
            return gameObject.GetAsyncStartTrigger().StartAsync();
        }

        public static UniTask StartAsync(this Component component)
        {
            return component.GetAsyncStartTrigger().StartAsync();
        }

        public static UniTask AwakeAsync(this GameObject gameObject)
        {
            return gameObject.GetAsyncAwakeTrigger().AwakeAsync();
        }

        public static UniTask AwakeAsync(this Component component)
        {
            return component.GetAsyncAwakeTrigger().AwakeAsync();
        }

        // Get Triggers.

        /// <summary>Get for OnAnimatorIKAsync | OnAnimatorMoveAsync.</summary>
        public static AsyncAnimatorTrigger GetAsyncAnimatorTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAnimatorTrigger>(gameObject);
        }

        /// <summary>Get for OnAnimatorIKAsync | OnAnimatorMoveAsync.</summary>
        public static AsyncAnimatorTrigger GetAsyncAnimatorTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAnimatorTrigger();
        }

        /// <summary>Get for AwakeAsync.</summary>
        public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAwakeTrigger>(gameObject);
        }

        /// <summary>Get for AwakeAsync.</summary>
        public static AsyncAwakeTrigger GetAsyncAwakeTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAwakeTrigger();
        }

        /// <summary>Get for OnBeginDragAsync.</summary>
        public static AsyncBeginDragTrigger GetAsyncBeginDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncBeginDragTrigger>(gameObject);
        }

        /// <summary>Get for OnBeginDragAsync.</summary>
        public static AsyncBeginDragTrigger GetAsyncBeginDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncBeginDragTrigger();
        }

        /// <summary>Get for OnCancelAsync.</summary>
        public static AsyncCancelTrigger GetAsyncCancelTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCancelTrigger>(gameObject);
        }

        /// <summary>Get for OnCancelAsync.</summary>
        public static AsyncCancelTrigger GetAsyncCancelTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCancelTrigger();
        }

        /// <summary>Get for OnCanvasGroupChangedAsync.</summary>
        public static AsyncCanvasGroupChangedTrigger GetAsyncCanvasGroupChangedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCanvasGroupChangedTrigger>(gameObject);
        }

        /// <summary>Get for OnCanvasGroupChangedAsync.</summary>
        public static AsyncCanvasGroupChangedTrigger GetAsyncCanvasGroupChangedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCanvasGroupChangedTrigger();
        }

        /// <summary>Get for OnCollisionEnter2DAsync | OnCollisionExit2DAsync | OnCollisionStay2DAsync.</summary>
        public static AsyncCollision2DTrigger GetAsyncCollision2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollision2DTrigger>(gameObject);
        }

        /// <summary>Get for OnCollisionEnter2DAsync | OnCollisionExit2DAsync | OnCollisionStay2DAsync.</summary>
        public static AsyncCollision2DTrigger GetAsyncCollision2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollision2DTrigger();
        }

        /// <summary>Get for OnCollisionEnterAsync | OnCollisionExitAsync | OnCollisionStayAsync.</summary>
        public static AsyncCollisionTrigger GetAsyncCollisionTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionTrigger>(gameObject);
        }

        /// <summary>Get for OnCollisionEnterAsync | OnCollisionExitAsync | OnCollisionStayAsync.</summary>
        public static AsyncCollisionTrigger GetAsyncCollisionTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionTrigger();
        }

        /// <summary>Get for OnDeselectAsync.</summary>
        public static AsyncDeselectTrigger GetAsyncDeselectTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDeselectTrigger>(gameObject);
        }

        /// <summary>Get for OnDeselectAsync.</summary>
        public static AsyncDeselectTrigger GetAsyncDeselectTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDeselectTrigger();
        }

        /// <summary>Get for OnDestroyAsync.</summary>
        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDestroyTrigger>(gameObject);
        }

        /// <summary>Get for OnDestroyAsync.</summary>
        public static AsyncDestroyTrigger GetAsyncDestroyTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDestroyTrigger();
        }

        /// <summary>Get for OnDragAsync.</summary>
        public static AsyncDragTrigger GetAsyncDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDragTrigger>(gameObject);
        }

        /// <summary>Get for OnDragAsync.</summary>
        public static AsyncDragTrigger GetAsyncDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDragTrigger();
        }

        /// <summary>Get for OnDropAsync.</summary>
        public static AsyncDropTrigger GetAsyncDropTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDropTrigger>(gameObject);
        }

        /// <summary>Get for OnDropAsync.</summary>
        public static AsyncDropTrigger GetAsyncDropTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDropTrigger();
        }

        /// <summary>Get for OnEnableAsync | OnDisableAsync.</summary>
        public static AsyncEnableDisableTrigger GetAsyncEnableDisableTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncEnableDisableTrigger>(gameObject);
        }

        /// <summary>Get for OnEnableAsync | OnDisableAsync.</summary>
        public static AsyncEnableDisableTrigger GetAsyncEnableDisableTrigger(this Component component)
        {
            return component.gameObject.GetAsyncEnableDisableTrigger();
        }

        /// <summary>Get for OnEndDragAsync.</summary>
        public static AsyncEndDragTrigger GetAsyncEndDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncEndDragTrigger>(gameObject);
        }

        /// <summary>Get for OnEndDragAsync.</summary>
        public static AsyncEndDragTrigger GetAsyncEndDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncEndDragTrigger();
        }

        /// <summary>Get for FixedUpdateAsync.</summary>
        public static AsyncFixedUpdateTrigger GetAsyncFixedUpdateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncFixedUpdateTrigger>(gameObject);
        }

        /// <summary>Get for FixedUpdateAsync.</summary>
        public static AsyncFixedUpdateTrigger GetAsyncFixedUpdateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncFixedUpdateTrigger();
        }

        /// <summary>Get for OnInitializePotentialDragAsync.</summary>
        public static AsyncInitializePotentialDragTrigger GetAsyncInitializePotentialDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncInitializePotentialDragTrigger>(gameObject);
        }

        /// <summary>Get for OnInitializePotentialDragAsync.</summary>
        public static AsyncInitializePotentialDragTrigger GetAsyncInitializePotentialDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncInitializePotentialDragTrigger();
        }

        /// <summary>Get for OnJointBreakAsync.</summary>
        public static AsyncJointTrigger GetAsyncJointTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncJointTrigger>(gameObject);
        }

        /// <summary>Get for OnJointBreakAsync.</summary>
        public static AsyncJointTrigger GetAsyncJointTrigger(this Component component)
        {
            return component.gameObject.GetAsyncJointTrigger();
        }

        /// <summary>Get for LateUpdateAsync.</summary>
        public static AsyncLateUpdateTrigger GetAsyncLateUpdateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncLateUpdateTrigger>(gameObject);
        }

        /// <summary>Get for LateUpdateAsync.</summary>
        public static AsyncLateUpdateTrigger GetAsyncLateUpdateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncLateUpdateTrigger();
        }

#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

        /// <summary>Get for OnMouseDownAsync | OnMouseDragAsync | OnMouseEnterAsync | OnMouseExitAsync | OnMouseOverAsync | OnMouseUpAsync | OnMouseUpAsButtonAsync.</summary>
        public static AsyncMouseTrigger GetAsyncMouseTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseTrigger>(gameObject);
        }

        /// <summary>Get for OnMouseDownAsync | OnMouseDragAsync | OnMouseEnterAsync | OnMouseExitAsync | OnMouseOverAsync | OnMouseUpAsync | OnMouseUpAsButtonAsync.</summary>
        public static AsyncMouseTrigger GetAsyncMouseTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseTrigger();
        }

#endif

        /// <summary>Get for OnMoveAsync.</summary>
        public static AsyncMoveTrigger GetAsyncMoveTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMoveTrigger>(gameObject);
        }

        /// <summary>Get for OnMoveAsync.</summary>
        public static AsyncMoveTrigger GetAsyncMoveTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMoveTrigger();
        }

        /// <summary>Get for OnParticleCollisionAsync | OnParticleTriggerAsync.</summary>
        public static AsyncParticleTrigger GetAsyncParticleTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncParticleTrigger>(gameObject);
        }

        /// <summary>Get for OnParticleCollisionAsync | OnParticleTriggerAsync.</summary>
        public static AsyncParticleTrigger GetAsyncParticleTrigger(this Component component)
        {
            return component.gameObject.GetAsyncParticleTrigger();
        }

        /// <summary>Get for OnPointerClickAsync.</summary>
        public static AsyncPointerClickTrigger GetAsyncPointerClickTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerClickTrigger>(gameObject);
        }

        /// <summary>Get for OnPointerClickAsync.</summary>
        public static AsyncPointerClickTrigger GetAsyncPointerClickTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerClickTrigger();
        }

        /// <summary>Get for OnPointerDownAsync.</summary>
        public static AsyncPointerDownTrigger GetAsyncPointerDownTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerDownTrigger>(gameObject);
        }

        /// <summary>Get for OnPointerDownAsync.</summary>
        public static AsyncPointerDownTrigger GetAsyncPointerDownTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerDownTrigger();
        }

        /// <summary>Get for OnPointerEnterAsync.</summary>
        public static AsyncPointerEnterTrigger GetAsyncPointerEnterTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerEnterTrigger>(gameObject);
        }

        /// <summary>Get for OnPointerEnterAsync.</summary>
        public static AsyncPointerEnterTrigger GetAsyncPointerEnterTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerEnterTrigger();
        }

        /// <summary>Get for OnPointerExitAsync.</summary>
        public static AsyncPointerExitTrigger GetAsyncPointerExitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerExitTrigger>(gameObject);
        }

        /// <summary>Get for OnPointerExitAsync.</summary>
        public static AsyncPointerExitTrigger GetAsyncPointerExitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerExitTrigger();
        }

        /// <summary>Get for OnPointerUpAsync.</summary>
        public static AsyncPointerUpTrigger GetAsyncPointerUpTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerUpTrigger>(gameObject);
        }

        /// <summary>Get for OnPointerUpAsync.</summary>
        public static AsyncPointerUpTrigger GetAsyncPointerUpTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerUpTrigger();
        }

        /// <summary>Get for OnRectTransformDimensionsChange | OnRectTransformDimensionsChangeAsync | OnRectTransformRemoved | OnRectTransformRemovedAsync.</summary>
        public static AsyncRectTransformTrigger GetAsyncRectTransformTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncRectTransformTrigger>(gameObject);
        }

        /// <summary>Get for OnRectTransformDimensionsChange | OnRectTransformDimensionsChangeAsync | OnRectTransformRemoved | OnRectTransformRemovedAsync.</summary>
        public static AsyncRectTransformTrigger GetAsyncRectTransformTrigger(this Component component)
        {
            return component.gameObject.GetAsyncRectTransformTrigger();
        }

        /// <summary>Get for OnScrollAsync.</summary>
        public static AsyncScrollTrigger GetAsyncScrollTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncScrollTrigger>(gameObject);
        }

        /// <summary>Get for OnScrollAsync.</summary>
        public static AsyncScrollTrigger GetAsyncScrollTrigger(this Component component)
        {
            return component.gameObject.GetAsyncScrollTrigger();
        }

        /// <summary>Get for OnSelectAsync.</summary>
        public static AsyncSelectTrigger GetAsyncSelectTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncSelectTrigger>(gameObject);
        }

        /// <summary>Get for OnSelectAsync.</summary>
        public static AsyncSelectTrigger GetAsyncSelectTrigger(this Component component)
        {
            return component.gameObject.GetAsyncSelectTrigger();
        }

        /// <summary>Get for StartAsync.</summary>
        public static AsyncStartTrigger GetAsyncStartTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncStartTrigger>(gameObject);
        }

        /// <summary>Get for StartAsync.</summary>
        public static AsyncStartTrigger GetAsyncStartTrigger(this Component component)
        {
            return component.gameObject.GetAsyncStartTrigger();
        }

        /// <summary>Get for OnSubmitAsync.</summary>
        public static AsyncSubmitTrigger GetAsyncSubmitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncSubmitTrigger>(gameObject);
        }

        /// <summary>Get for OnSubmitAsync.</summary>
        public static AsyncSubmitTrigger GetAsyncSubmitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncSubmitTrigger();
        }

        /// <summary>Get for OnBeforeTransformParentChangedAsync | OnTransformParentChangedAsync | OnTransformChildrenChangedAsync.</summary>
        public static AsyncTransformChangedTrigger GetAsyncTransformChangedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTransformChangedTrigger>(gameObject);
        }

        /// <summary>Get for OnBeforeTransformParentChangedAsync | OnTransformParentChangedAsync | OnTransformChildrenChangedAsync.</summary>
        public static AsyncTransformChangedTrigger GetAsyncTransformChangedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTransformChangedTrigger();
        }

        /// <summary>Get for OnTriggerEnter2DAsync | OnTriggerExit2DAsync | OnTriggerStay2DAsync.</summary>
        public static AsyncTrigger2DTrigger GetAsyncTrigger2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTrigger2DTrigger>(gameObject);
        }

        /// <summary>Get for OnTriggerEnter2DAsync | OnTriggerExit2DAsync | OnTriggerStay2DAsync.</summary>
        public static AsyncTrigger2DTrigger GetAsyncTrigger2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTrigger2DTrigger();
        }

        /// <summary>Get for OnTriggerEnterAsync | OnTriggerExitAsync | OnTriggerStayAsync.</summary>
        public static AsyncTriggerTrigger GetAsyncTriggerTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerTrigger>(gameObject);
        }

        /// <summary>Get for OnTriggerEnterAsync | OnTriggerExitAsync | OnTriggerStayAsync.</summary>
        public static AsyncTriggerTrigger GetAsyncTriggerTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerTrigger();
        }

        /// <summary>Get for OnUpdateSelectedAsync.</summary>
        public static AsyncUpdateSelectedTrigger GetAsyncUpdateSelectedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncUpdateSelectedTrigger>(gameObject);
        }

        /// <summary>Get for OnUpdateSelectedAsync.</summary>
        public static AsyncUpdateSelectedTrigger GetAsyncUpdateSelectedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncUpdateSelectedTrigger();
        }

        /// <summary>Get for UpdateAsync.</summary>
        public static AsyncUpdateTrigger GetAsyncUpdateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncUpdateTrigger>(gameObject);
        }

        /// <summary>Get for UpdateAsync.</summary>
        public static AsyncUpdateTrigger GetAsyncUpdateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncUpdateTrigger();
        }

        /// <summary>Get for OnBecameInvisibleAsync | OnBecameVisibleAsync.</summary>
        public static AsyncVisibleTrigger GetAsyncVisibleTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncVisibleTrigger>(gameObject);
        }

        /// <summary>Get for OnBecameInvisibleAsync | OnBecameVisibleAsync.</summary>
        public static AsyncVisibleTrigger GetAsyncVisibleTrigger(this Component component)
        {
            return component.gameObject.GetAsyncVisibleTrigger();
        }
    }
}

#endif