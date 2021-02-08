#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using UnityEngine;
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT
using UnityEngine.EventSystems;
#endif

namespace Cysharp.Threading.Tasks.Triggers
{
#region FixedUpdate

    public interface IAsyncFixedUpdateHandler
    {
        UniTask FixedUpdateAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncFixedUpdateHandler
    {
        UniTask IAsyncFixedUpdateHandler.FixedUpdateAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncFixedUpdateTrigger GetAsyncFixedUpdateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncFixedUpdateTrigger>(gameObject);
        }
        
        public static AsyncFixedUpdateTrigger GetAsyncFixedUpdateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncFixedUpdateTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncFixedUpdateTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void FixedUpdate()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncFixedUpdateHandler GetFixedUpdateAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncFixedUpdateHandler GetFixedUpdateAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask FixedUpdateAsync()
        {
            return ((IAsyncFixedUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).FixedUpdateAsync();
        }

        public UniTask FixedUpdateAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncFixedUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).FixedUpdateAsync();
        }
    }
#endregion

#region LateUpdate

    public interface IAsyncLateUpdateHandler
    {
        UniTask LateUpdateAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncLateUpdateHandler
    {
        UniTask IAsyncLateUpdateHandler.LateUpdateAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncLateUpdateTrigger GetAsyncLateUpdateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncLateUpdateTrigger>(gameObject);
        }
        
        public static AsyncLateUpdateTrigger GetAsyncLateUpdateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncLateUpdateTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncLateUpdateTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void LateUpdate()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncLateUpdateHandler GetLateUpdateAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncLateUpdateHandler GetLateUpdateAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask LateUpdateAsync()
        {
            return ((IAsyncLateUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).LateUpdateAsync();
        }

        public UniTask LateUpdateAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncLateUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).LateUpdateAsync();
        }
    }
#endregion

#region AnimatorIK

    public interface IAsyncOnAnimatorIKHandler
    {
        UniTask<int> OnAnimatorIKAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnAnimatorIKHandler
    {
        UniTask<int> IAsyncOnAnimatorIKHandler.OnAnimatorIKAsync()
        {
            core.Reset();
            return new UniTask<int>((IUniTaskSource<int>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncAnimatorIKTrigger GetAsyncAnimatorIKTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAnimatorIKTrigger>(gameObject);
        }
        
        public static AsyncAnimatorIKTrigger GetAsyncAnimatorIKTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAnimatorIKTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncAnimatorIKTrigger : AsyncTriggerBase<int>
    {
        void OnAnimatorIK(int layerIndex)
        {
            RaiseEvent((layerIndex));
        }

        public IAsyncOnAnimatorIKHandler GetOnAnimatorIKAsyncHandler()
        {
            return new AsyncTriggerHandler<int>(this, false);
        }

        public IAsyncOnAnimatorIKHandler GetOnAnimatorIKAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<int>(this, cancellationToken, false);
        }

        public UniTask<int> OnAnimatorIKAsync()
        {
            return ((IAsyncOnAnimatorIKHandler)new AsyncTriggerHandler<int>(this, true)).OnAnimatorIKAsync();
        }

        public UniTask<int> OnAnimatorIKAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnAnimatorIKHandler)new AsyncTriggerHandler<int>(this, cancellationToken, true)).OnAnimatorIKAsync();
        }
    }
#endregion

#region AnimatorMove

    public interface IAsyncOnAnimatorMoveHandler
    {
        UniTask OnAnimatorMoveAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnAnimatorMoveHandler
    {
        UniTask IAsyncOnAnimatorMoveHandler.OnAnimatorMoveAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncAnimatorMoveTrigger GetAsyncAnimatorMoveTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAnimatorMoveTrigger>(gameObject);
        }
        
        public static AsyncAnimatorMoveTrigger GetAsyncAnimatorMoveTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAnimatorMoveTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncAnimatorMoveTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnAnimatorMove()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnAnimatorMoveHandler GetOnAnimatorMoveAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnAnimatorMoveHandler GetOnAnimatorMoveAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnAnimatorMoveAsync()
        {
            return ((IAsyncOnAnimatorMoveHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnAnimatorMoveAsync();
        }

        public UniTask OnAnimatorMoveAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnAnimatorMoveHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnAnimatorMoveAsync();
        }
    }
#endregion

#region ApplicationFocus

    public interface IAsyncOnApplicationFocusHandler
    {
        UniTask<bool> OnApplicationFocusAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnApplicationFocusHandler
    {
        UniTask<bool> IAsyncOnApplicationFocusHandler.OnApplicationFocusAsync()
        {
            core.Reset();
            return new UniTask<bool>((IUniTaskSource<bool>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncApplicationFocusTrigger GetAsyncApplicationFocusTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncApplicationFocusTrigger>(gameObject);
        }
        
        public static AsyncApplicationFocusTrigger GetAsyncApplicationFocusTrigger(this Component component)
        {
            return component.gameObject.GetAsyncApplicationFocusTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncApplicationFocusTrigger : AsyncTriggerBase<bool>
    {
        void OnApplicationFocus(bool hasFocus)
        {
            RaiseEvent((hasFocus));
        }

        public IAsyncOnApplicationFocusHandler GetOnApplicationFocusAsyncHandler()
        {
            return new AsyncTriggerHandler<bool>(this, false);
        }

        public IAsyncOnApplicationFocusHandler GetOnApplicationFocusAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<bool>(this, cancellationToken, false);
        }

        public UniTask<bool> OnApplicationFocusAsync()
        {
            return ((IAsyncOnApplicationFocusHandler)new AsyncTriggerHandler<bool>(this, true)).OnApplicationFocusAsync();
        }

        public UniTask<bool> OnApplicationFocusAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnApplicationFocusHandler)new AsyncTriggerHandler<bool>(this, cancellationToken, true)).OnApplicationFocusAsync();
        }
    }
#endregion

#region ApplicationPause

    public interface IAsyncOnApplicationPauseHandler
    {
        UniTask<bool> OnApplicationPauseAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnApplicationPauseHandler
    {
        UniTask<bool> IAsyncOnApplicationPauseHandler.OnApplicationPauseAsync()
        {
            core.Reset();
            return new UniTask<bool>((IUniTaskSource<bool>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncApplicationPauseTrigger GetAsyncApplicationPauseTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncApplicationPauseTrigger>(gameObject);
        }
        
        public static AsyncApplicationPauseTrigger GetAsyncApplicationPauseTrigger(this Component component)
        {
            return component.gameObject.GetAsyncApplicationPauseTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncApplicationPauseTrigger : AsyncTriggerBase<bool>
    {
        void OnApplicationPause(bool pauseStatus)
        {
            RaiseEvent((pauseStatus));
        }

        public IAsyncOnApplicationPauseHandler GetOnApplicationPauseAsyncHandler()
        {
            return new AsyncTriggerHandler<bool>(this, false);
        }

        public IAsyncOnApplicationPauseHandler GetOnApplicationPauseAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<bool>(this, cancellationToken, false);
        }

        public UniTask<bool> OnApplicationPauseAsync()
        {
            return ((IAsyncOnApplicationPauseHandler)new AsyncTriggerHandler<bool>(this, true)).OnApplicationPauseAsync();
        }

        public UniTask<bool> OnApplicationPauseAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnApplicationPauseHandler)new AsyncTriggerHandler<bool>(this, cancellationToken, true)).OnApplicationPauseAsync();
        }
    }
#endregion

#region ApplicationQuit

    public interface IAsyncOnApplicationQuitHandler
    {
        UniTask OnApplicationQuitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnApplicationQuitHandler
    {
        UniTask IAsyncOnApplicationQuitHandler.OnApplicationQuitAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncApplicationQuitTrigger GetAsyncApplicationQuitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncApplicationQuitTrigger>(gameObject);
        }
        
        public static AsyncApplicationQuitTrigger GetAsyncApplicationQuitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncApplicationQuitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncApplicationQuitTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnApplicationQuit()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnApplicationQuitHandler GetOnApplicationQuitAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnApplicationQuitHandler GetOnApplicationQuitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnApplicationQuitAsync()
        {
            return ((IAsyncOnApplicationQuitHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnApplicationQuitAsync();
        }

        public UniTask OnApplicationQuitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnApplicationQuitHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnApplicationQuitAsync();
        }
    }
#endregion

#region AudioFilterRead

    public interface IAsyncOnAudioFilterReadHandler
    {
        UniTask<(float[] data, int channels)> OnAudioFilterReadAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnAudioFilterReadHandler
    {
        UniTask<(float[] data, int channels)> IAsyncOnAudioFilterReadHandler.OnAudioFilterReadAsync()
        {
            core.Reset();
            return new UniTask<(float[] data, int channels)>((IUniTaskSource<(float[] data, int channels)>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncAudioFilterReadTrigger GetAsyncAudioFilterReadTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncAudioFilterReadTrigger>(gameObject);
        }
        
        public static AsyncAudioFilterReadTrigger GetAsyncAudioFilterReadTrigger(this Component component)
        {
            return component.gameObject.GetAsyncAudioFilterReadTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncAudioFilterReadTrigger : AsyncTriggerBase<(float[] data, int channels)>
    {
        void OnAudioFilterRead(float[] data, int channels)
        {
            RaiseEvent((data, channels));
        }

        public IAsyncOnAudioFilterReadHandler GetOnAudioFilterReadAsyncHandler()
        {
            return new AsyncTriggerHandler<(float[] data, int channels)>(this, false);
        }

        public IAsyncOnAudioFilterReadHandler GetOnAudioFilterReadAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<(float[] data, int channels)>(this, cancellationToken, false);
        }

        public UniTask<(float[] data, int channels)> OnAudioFilterReadAsync()
        {
            return ((IAsyncOnAudioFilterReadHandler)new AsyncTriggerHandler<(float[] data, int channels)>(this, true)).OnAudioFilterReadAsync();
        }

        public UniTask<(float[] data, int channels)> OnAudioFilterReadAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnAudioFilterReadHandler)new AsyncTriggerHandler<(float[] data, int channels)>(this, cancellationToken, true)).OnAudioFilterReadAsync();
        }
    }
#endregion

#region BecameInvisible

    public interface IAsyncOnBecameInvisibleHandler
    {
        UniTask OnBecameInvisibleAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnBecameInvisibleHandler
    {
        UniTask IAsyncOnBecameInvisibleHandler.OnBecameInvisibleAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncBecameInvisibleTrigger GetAsyncBecameInvisibleTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncBecameInvisibleTrigger>(gameObject);
        }
        
        public static AsyncBecameInvisibleTrigger GetAsyncBecameInvisibleTrigger(this Component component)
        {
            return component.gameObject.GetAsyncBecameInvisibleTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncBecameInvisibleTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnBecameInvisible()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnBecameInvisibleHandler GetOnBecameInvisibleAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnBecameInvisibleHandler GetOnBecameInvisibleAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnBecameInvisibleAsync()
        {
            return ((IAsyncOnBecameInvisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBecameInvisibleAsync();
        }

        public UniTask OnBecameInvisibleAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnBecameInvisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBecameInvisibleAsync();
        }
    }
#endregion

#region BecameVisible

    public interface IAsyncOnBecameVisibleHandler
    {
        UniTask OnBecameVisibleAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnBecameVisibleHandler
    {
        UniTask IAsyncOnBecameVisibleHandler.OnBecameVisibleAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncBecameVisibleTrigger GetAsyncBecameVisibleTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncBecameVisibleTrigger>(gameObject);
        }
        
        public static AsyncBecameVisibleTrigger GetAsyncBecameVisibleTrigger(this Component component)
        {
            return component.gameObject.GetAsyncBecameVisibleTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncBecameVisibleTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnBecameVisible()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnBecameVisibleHandler GetOnBecameVisibleAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnBecameVisibleHandler GetOnBecameVisibleAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnBecameVisibleAsync()
        {
            return ((IAsyncOnBecameVisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBecameVisibleAsync();
        }

        public UniTask OnBecameVisibleAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnBecameVisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBecameVisibleAsync();
        }
    }
#endregion

#region BeforeTransformParentChanged

    public interface IAsyncOnBeforeTransformParentChangedHandler
    {
        UniTask OnBeforeTransformParentChangedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnBeforeTransformParentChangedHandler
    {
        UniTask IAsyncOnBeforeTransformParentChangedHandler.OnBeforeTransformParentChangedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncBeforeTransformParentChangedTrigger GetAsyncBeforeTransformParentChangedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncBeforeTransformParentChangedTrigger>(gameObject);
        }
        
        public static AsyncBeforeTransformParentChangedTrigger GetAsyncBeforeTransformParentChangedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncBeforeTransformParentChangedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncBeforeTransformParentChangedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnBeforeTransformParentChanged()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnBeforeTransformParentChangedHandler GetOnBeforeTransformParentChangedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnBeforeTransformParentChangedHandler GetOnBeforeTransformParentChangedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnBeforeTransformParentChangedAsync()
        {
            return ((IAsyncOnBeforeTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBeforeTransformParentChangedAsync();
        }

        public UniTask OnBeforeTransformParentChangedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnBeforeTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBeforeTransformParentChangedAsync();
        }
    }
#endregion

#region OnCanvasGroupChanged

    public interface IAsyncOnCanvasGroupChangedHandler
    {
        UniTask OnCanvasGroupChangedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCanvasGroupChangedHandler
    {
        UniTask IAsyncOnCanvasGroupChangedHandler.OnCanvasGroupChangedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncOnCanvasGroupChangedTrigger GetAsyncOnCanvasGroupChangedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncOnCanvasGroupChangedTrigger>(gameObject);
        }
        
        public static AsyncOnCanvasGroupChangedTrigger GetAsyncOnCanvasGroupChangedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncOnCanvasGroupChangedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncOnCanvasGroupChangedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnCanvasGroupChanged()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnCanvasGroupChangedHandler GetOnCanvasGroupChangedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnCanvasGroupChangedHandler GetOnCanvasGroupChangedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnCanvasGroupChangedAsync()
        {
            return ((IAsyncOnCanvasGroupChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnCanvasGroupChangedAsync();
        }

        public UniTask OnCanvasGroupChangedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCanvasGroupChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnCanvasGroupChangedAsync();
        }
    }
#endregion

#region CollisionEnter
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnCollisionEnterHandler
    {
        UniTask<Collision> OnCollisionEnterAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCollisionEnterHandler
    {
        UniTask<Collision> IAsyncOnCollisionEnterHandler.OnCollisionEnterAsync()
        {
            core.Reset();
            return new UniTask<Collision>((IUniTaskSource<Collision>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCollisionEnterTrigger GetAsyncCollisionEnterTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionEnterTrigger>(gameObject);
        }
        
        public static AsyncCollisionEnterTrigger GetAsyncCollisionEnterTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionEnterTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCollisionEnterTrigger : AsyncTriggerBase<Collision>
    {
        void OnCollisionEnter(Collision coll)
        {
            RaiseEvent((coll));
        }

        public IAsyncOnCollisionEnterHandler GetOnCollisionEnterAsyncHandler()
        {
            return new AsyncTriggerHandler<Collision>(this, false);
        }

        public IAsyncOnCollisionEnterHandler GetOnCollisionEnterAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
        }

        public UniTask<Collision> OnCollisionEnterAsync()
        {
            return ((IAsyncOnCollisionEnterHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionEnterAsync();
        }

        public UniTask<Collision> OnCollisionEnterAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCollisionEnterHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionEnterAsync();
        }
    }
#endif
#endregion

#region CollisionEnter2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnCollisionEnter2DHandler
    {
        UniTask<Collision2D> OnCollisionEnter2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCollisionEnter2DHandler
    {
        UniTask<Collision2D> IAsyncOnCollisionEnter2DHandler.OnCollisionEnter2DAsync()
        {
            core.Reset();
            return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCollisionEnter2DTrigger GetAsyncCollisionEnter2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionEnter2DTrigger>(gameObject);
        }
        
        public static AsyncCollisionEnter2DTrigger GetAsyncCollisionEnter2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionEnter2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCollisionEnter2DTrigger : AsyncTriggerBase<Collision2D>
    {
        void OnCollisionEnter2D(Collision2D coll)
        {
            RaiseEvent((coll));
        }

        public IAsyncOnCollisionEnter2DHandler GetOnCollisionEnter2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Collision2D>(this, false);
        }

        public IAsyncOnCollisionEnter2DHandler GetOnCollisionEnter2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collision2D>(this, cancellationToken, false);
        }

        public UniTask<Collision2D> OnCollisionEnter2DAsync()
        {
            return ((IAsyncOnCollisionEnter2DHandler)new AsyncTriggerHandler<Collision2D>(this, true)).OnCollisionEnter2DAsync();
        }

        public UniTask<Collision2D> OnCollisionEnter2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCollisionEnter2DHandler)new AsyncTriggerHandler<Collision2D>(this, cancellationToken, true)).OnCollisionEnter2DAsync();
        }
    }
#endif
#endregion

#region CollisionExit
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnCollisionExitHandler
    {
        UniTask<Collision> OnCollisionExitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCollisionExitHandler
    {
        UniTask<Collision> IAsyncOnCollisionExitHandler.OnCollisionExitAsync()
        {
            core.Reset();
            return new UniTask<Collision>((IUniTaskSource<Collision>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCollisionExitTrigger GetAsyncCollisionExitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionExitTrigger>(gameObject);
        }
        
        public static AsyncCollisionExitTrigger GetAsyncCollisionExitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionExitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCollisionExitTrigger : AsyncTriggerBase<Collision>
    {
        void OnCollisionExit(Collision coll)
        {
            RaiseEvent((coll));
        }

        public IAsyncOnCollisionExitHandler GetOnCollisionExitAsyncHandler()
        {
            return new AsyncTriggerHandler<Collision>(this, false);
        }

        public IAsyncOnCollisionExitHandler GetOnCollisionExitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
        }

        public UniTask<Collision> OnCollisionExitAsync()
        {
            return ((IAsyncOnCollisionExitHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionExitAsync();
        }

        public UniTask<Collision> OnCollisionExitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCollisionExitHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionExitAsync();
        }
    }
#endif
#endregion

#region CollisionExit2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnCollisionExit2DHandler
    {
        UniTask<Collision2D> OnCollisionExit2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCollisionExit2DHandler
    {
        UniTask<Collision2D> IAsyncOnCollisionExit2DHandler.OnCollisionExit2DAsync()
        {
            core.Reset();
            return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCollisionExit2DTrigger GetAsyncCollisionExit2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionExit2DTrigger>(gameObject);
        }
        
        public static AsyncCollisionExit2DTrigger GetAsyncCollisionExit2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionExit2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCollisionExit2DTrigger : AsyncTriggerBase<Collision2D>
    {
        void OnCollisionExit2D(Collision2D coll)
        {
            RaiseEvent((coll));
        }

        public IAsyncOnCollisionExit2DHandler GetOnCollisionExit2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Collision2D>(this, false);
        }

        public IAsyncOnCollisionExit2DHandler GetOnCollisionExit2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collision2D>(this, cancellationToken, false);
        }

        public UniTask<Collision2D> OnCollisionExit2DAsync()
        {
            return ((IAsyncOnCollisionExit2DHandler)new AsyncTriggerHandler<Collision2D>(this, true)).OnCollisionExit2DAsync();
        }

        public UniTask<Collision2D> OnCollisionExit2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCollisionExit2DHandler)new AsyncTriggerHandler<Collision2D>(this, cancellationToken, true)).OnCollisionExit2DAsync();
        }
    }
#endif
#endregion

#region CollisionStay
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnCollisionStayHandler
    {
        UniTask<Collision> OnCollisionStayAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCollisionStayHandler
    {
        UniTask<Collision> IAsyncOnCollisionStayHandler.OnCollisionStayAsync()
        {
            core.Reset();
            return new UniTask<Collision>((IUniTaskSource<Collision>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCollisionStayTrigger GetAsyncCollisionStayTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionStayTrigger>(gameObject);
        }
        
        public static AsyncCollisionStayTrigger GetAsyncCollisionStayTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionStayTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCollisionStayTrigger : AsyncTriggerBase<Collision>
    {
        void OnCollisionStay(Collision coll)
        {
            RaiseEvent((coll));
        }

        public IAsyncOnCollisionStayHandler GetOnCollisionStayAsyncHandler()
        {
            return new AsyncTriggerHandler<Collision>(this, false);
        }

        public IAsyncOnCollisionStayHandler GetOnCollisionStayAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
        }

        public UniTask<Collision> OnCollisionStayAsync()
        {
            return ((IAsyncOnCollisionStayHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionStayAsync();
        }

        public UniTask<Collision> OnCollisionStayAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCollisionStayHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionStayAsync();
        }
    }
#endif
#endregion

#region CollisionStay2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnCollisionStay2DHandler
    {
        UniTask<Collision2D> OnCollisionStay2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCollisionStay2DHandler
    {
        UniTask<Collision2D> IAsyncOnCollisionStay2DHandler.OnCollisionStay2DAsync()
        {
            core.Reset();
            return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCollisionStay2DTrigger GetAsyncCollisionStay2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCollisionStay2DTrigger>(gameObject);
        }
        
        public static AsyncCollisionStay2DTrigger GetAsyncCollisionStay2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCollisionStay2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCollisionStay2DTrigger : AsyncTriggerBase<Collision2D>
    {
        void OnCollisionStay2D(Collision2D coll)
        {
            RaiseEvent((coll));
        }

        public IAsyncOnCollisionStay2DHandler GetOnCollisionStay2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Collision2D>(this, false);
        }

        public IAsyncOnCollisionStay2DHandler GetOnCollisionStay2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collision2D>(this, cancellationToken, false);
        }

        public UniTask<Collision2D> OnCollisionStay2DAsync()
        {
            return ((IAsyncOnCollisionStay2DHandler)new AsyncTriggerHandler<Collision2D>(this, true)).OnCollisionStay2DAsync();
        }

        public UniTask<Collision2D> OnCollisionStay2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCollisionStay2DHandler)new AsyncTriggerHandler<Collision2D>(this, cancellationToken, true)).OnCollisionStay2DAsync();
        }
    }
#endif
#endregion

#region ControllerColliderHit
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnControllerColliderHitHandler
    {
        UniTask<ControllerColliderHit> OnControllerColliderHitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnControllerColliderHitHandler
    {
        UniTask<ControllerColliderHit> IAsyncOnControllerColliderHitHandler.OnControllerColliderHitAsync()
        {
            core.Reset();
            return new UniTask<ControllerColliderHit>((IUniTaskSource<ControllerColliderHit>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncControllerColliderHitTrigger GetAsyncControllerColliderHitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncControllerColliderHitTrigger>(gameObject);
        }
        
        public static AsyncControllerColliderHitTrigger GetAsyncControllerColliderHitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncControllerColliderHitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncControllerColliderHitTrigger : AsyncTriggerBase<ControllerColliderHit>
    {
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            RaiseEvent((hit));
        }

        public IAsyncOnControllerColliderHitHandler GetOnControllerColliderHitAsyncHandler()
        {
            return new AsyncTriggerHandler<ControllerColliderHit>(this, false);
        }

        public IAsyncOnControllerColliderHitHandler GetOnControllerColliderHitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<ControllerColliderHit>(this, cancellationToken, false);
        }

        public UniTask<ControllerColliderHit> OnControllerColliderHitAsync()
        {
            return ((IAsyncOnControllerColliderHitHandler)new AsyncTriggerHandler<ControllerColliderHit>(this, true)).OnControllerColliderHitAsync();
        }

        public UniTask<ControllerColliderHit> OnControllerColliderHitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnControllerColliderHitHandler)new AsyncTriggerHandler<ControllerColliderHit>(this, cancellationToken, true)).OnControllerColliderHitAsync();
        }
    }
#endif
#endregion

#region Disable

    public interface IAsyncOnDisableHandler
    {
        UniTask OnDisableAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnDisableHandler
    {
        UniTask IAsyncOnDisableHandler.OnDisableAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDisableTrigger GetAsyncDisableTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDisableTrigger>(gameObject);
        }
        
        public static AsyncDisableTrigger GetAsyncDisableTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDisableTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDisableTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnDisable()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnDisableHandler GetOnDisableAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnDisableHandler GetOnDisableAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnDisableAsync()
        {
            return ((IAsyncOnDisableHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDisableAsync();
        }

        public UniTask OnDisableAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnDisableHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDisableAsync();
        }
    }
#endregion

#region DrawGizmos

    public interface IAsyncOnDrawGizmosHandler
    {
        UniTask OnDrawGizmosAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnDrawGizmosHandler
    {
        UniTask IAsyncOnDrawGizmosHandler.OnDrawGizmosAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDrawGizmosTrigger GetAsyncDrawGizmosTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDrawGizmosTrigger>(gameObject);
        }
        
        public static AsyncDrawGizmosTrigger GetAsyncDrawGizmosTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDrawGizmosTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDrawGizmosTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnDrawGizmos()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnDrawGizmosHandler GetOnDrawGizmosAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnDrawGizmosHandler GetOnDrawGizmosAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnDrawGizmosAsync()
        {
            return ((IAsyncOnDrawGizmosHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDrawGizmosAsync();
        }

        public UniTask OnDrawGizmosAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnDrawGizmosHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDrawGizmosAsync();
        }
    }
#endregion

#region DrawGizmosSelected

    public interface IAsyncOnDrawGizmosSelectedHandler
    {
        UniTask OnDrawGizmosSelectedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnDrawGizmosSelectedHandler
    {
        UniTask IAsyncOnDrawGizmosSelectedHandler.OnDrawGizmosSelectedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDrawGizmosSelectedTrigger GetAsyncDrawGizmosSelectedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDrawGizmosSelectedTrigger>(gameObject);
        }
        
        public static AsyncDrawGizmosSelectedTrigger GetAsyncDrawGizmosSelectedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDrawGizmosSelectedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDrawGizmosSelectedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnDrawGizmosSelected()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnDrawGizmosSelectedHandler GetOnDrawGizmosSelectedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnDrawGizmosSelectedHandler GetOnDrawGizmosSelectedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnDrawGizmosSelectedAsync()
        {
            return ((IAsyncOnDrawGizmosSelectedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDrawGizmosSelectedAsync();
        }

        public UniTask OnDrawGizmosSelectedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnDrawGizmosSelectedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDrawGizmosSelectedAsync();
        }
    }
#endregion

#region Enable

    public interface IAsyncOnEnableHandler
    {
        UniTask OnEnableAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnEnableHandler
    {
        UniTask IAsyncOnEnableHandler.OnEnableAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncEnableTrigger GetAsyncEnableTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncEnableTrigger>(gameObject);
        }
        
        public static AsyncEnableTrigger GetAsyncEnableTrigger(this Component component)
        {
            return component.gameObject.GetAsyncEnableTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncEnableTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnEnable()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnEnableHandler GetOnEnableAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnEnableHandler GetOnEnableAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnEnableAsync()
        {
            return ((IAsyncOnEnableHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnEnableAsync();
        }

        public UniTask OnEnableAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnEnableHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnEnableAsync();
        }
    }
#endregion

#region GUI

    public interface IAsyncOnGUIHandler
    {
        UniTask OnGUIAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnGUIHandler
    {
        UniTask IAsyncOnGUIHandler.OnGUIAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncGUITrigger GetAsyncGUITrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncGUITrigger>(gameObject);
        }
        
        public static AsyncGUITrigger GetAsyncGUITrigger(this Component component)
        {
            return component.gameObject.GetAsyncGUITrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncGUITrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnGUI()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnGUIHandler GetOnGUIAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnGUIHandler GetOnGUIAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnGUIAsync()
        {
            return ((IAsyncOnGUIHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnGUIAsync();
        }

        public UniTask OnGUIAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnGUIHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnGUIAsync();
        }
    }
#endregion

#region JointBreak
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnJointBreakHandler
    {
        UniTask<float> OnJointBreakAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnJointBreakHandler
    {
        UniTask<float> IAsyncOnJointBreakHandler.OnJointBreakAsync()
        {
            core.Reset();
            return new UniTask<float>((IUniTaskSource<float>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncJointBreakTrigger GetAsyncJointBreakTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncJointBreakTrigger>(gameObject);
        }
        
        public static AsyncJointBreakTrigger GetAsyncJointBreakTrigger(this Component component)
        {
            return component.gameObject.GetAsyncJointBreakTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncJointBreakTrigger : AsyncTriggerBase<float>
    {
        void OnJointBreak(float breakForce)
        {
            RaiseEvent((breakForce));
        }

        public IAsyncOnJointBreakHandler GetOnJointBreakAsyncHandler()
        {
            return new AsyncTriggerHandler<float>(this, false);
        }

        public IAsyncOnJointBreakHandler GetOnJointBreakAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<float>(this, cancellationToken, false);
        }

        public UniTask<float> OnJointBreakAsync()
        {
            return ((IAsyncOnJointBreakHandler)new AsyncTriggerHandler<float>(this, true)).OnJointBreakAsync();
        }

        public UniTask<float> OnJointBreakAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnJointBreakHandler)new AsyncTriggerHandler<float>(this, cancellationToken, true)).OnJointBreakAsync();
        }
    }
#endif
#endregion

#region JointBreak2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnJointBreak2DHandler
    {
        UniTask<Joint2D> OnJointBreak2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnJointBreak2DHandler
    {
        UniTask<Joint2D> IAsyncOnJointBreak2DHandler.OnJointBreak2DAsync()
        {
            core.Reset();
            return new UniTask<Joint2D>((IUniTaskSource<Joint2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncJointBreak2DTrigger GetAsyncJointBreak2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncJointBreak2DTrigger>(gameObject);
        }
        
        public static AsyncJointBreak2DTrigger GetAsyncJointBreak2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncJointBreak2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncJointBreak2DTrigger : AsyncTriggerBase<Joint2D>
    {
        void OnJointBreak2D(Joint2D brokenJoint)
        {
            RaiseEvent((brokenJoint));
        }

        public IAsyncOnJointBreak2DHandler GetOnJointBreak2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Joint2D>(this, false);
        }

        public IAsyncOnJointBreak2DHandler GetOnJointBreak2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Joint2D>(this, cancellationToken, false);
        }

        public UniTask<Joint2D> OnJointBreak2DAsync()
        {
            return ((IAsyncOnJointBreak2DHandler)new AsyncTriggerHandler<Joint2D>(this, true)).OnJointBreak2DAsync();
        }

        public UniTask<Joint2D> OnJointBreak2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnJointBreak2DHandler)new AsyncTriggerHandler<Joint2D>(this, cancellationToken, true)).OnJointBreak2DAsync();
        }
    }
#endif
#endregion

#region MouseDown
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseDownHandler
    {
        UniTask OnMouseDownAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseDownHandler
    {
        UniTask IAsyncOnMouseDownHandler.OnMouseDownAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseDownTrigger GetAsyncMouseDownTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseDownTrigger>(gameObject);
        }
        
        public static AsyncMouseDownTrigger GetAsyncMouseDownTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseDownTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseDownTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseDown()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseDownHandler GetOnMouseDownAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseDownHandler GetOnMouseDownAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseDownAsync()
        {
            return ((IAsyncOnMouseDownHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseDownAsync();
        }

        public UniTask OnMouseDownAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseDownHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseDownAsync();
        }
    }
#endif
#endregion

#region MouseDrag
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseDragHandler
    {
        UniTask OnMouseDragAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseDragHandler
    {
        UniTask IAsyncOnMouseDragHandler.OnMouseDragAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseDragTrigger GetAsyncMouseDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseDragTrigger>(gameObject);
        }
        
        public static AsyncMouseDragTrigger GetAsyncMouseDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseDragTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseDragTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseDrag()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseDragHandler GetOnMouseDragAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseDragHandler GetOnMouseDragAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseDragAsync()
        {
            return ((IAsyncOnMouseDragHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseDragAsync();
        }

        public UniTask OnMouseDragAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseDragHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseDragAsync();
        }
    }
#endif
#endregion

#region MouseEnter
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseEnterHandler
    {
        UniTask OnMouseEnterAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseEnterHandler
    {
        UniTask IAsyncOnMouseEnterHandler.OnMouseEnterAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseEnterTrigger GetAsyncMouseEnterTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseEnterTrigger>(gameObject);
        }
        
        public static AsyncMouseEnterTrigger GetAsyncMouseEnterTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseEnterTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseEnterTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseEnter()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseEnterHandler GetOnMouseEnterAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseEnterHandler GetOnMouseEnterAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseEnterAsync()
        {
            return ((IAsyncOnMouseEnterHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseEnterAsync();
        }

        public UniTask OnMouseEnterAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseEnterHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseEnterAsync();
        }
    }
#endif
#endregion

#region MouseExit
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseExitHandler
    {
        UniTask OnMouseExitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseExitHandler
    {
        UniTask IAsyncOnMouseExitHandler.OnMouseExitAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseExitTrigger GetAsyncMouseExitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseExitTrigger>(gameObject);
        }
        
        public static AsyncMouseExitTrigger GetAsyncMouseExitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseExitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseExitTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseExit()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseExitHandler GetOnMouseExitAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseExitHandler GetOnMouseExitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseExitAsync()
        {
            return ((IAsyncOnMouseExitHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseExitAsync();
        }

        public UniTask OnMouseExitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseExitHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseExitAsync();
        }
    }
#endif
#endregion

#region MouseOver
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseOverHandler
    {
        UniTask OnMouseOverAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseOverHandler
    {
        UniTask IAsyncOnMouseOverHandler.OnMouseOverAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseOverTrigger GetAsyncMouseOverTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseOverTrigger>(gameObject);
        }
        
        public static AsyncMouseOverTrigger GetAsyncMouseOverTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseOverTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseOverTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseOver()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseOverHandler GetOnMouseOverAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseOverHandler GetOnMouseOverAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseOverAsync()
        {
            return ((IAsyncOnMouseOverHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseOverAsync();
        }

        public UniTask OnMouseOverAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseOverHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseOverAsync();
        }
    }
#endif
#endregion

#region MouseUp
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseUpHandler
    {
        UniTask OnMouseUpAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseUpHandler
    {
        UniTask IAsyncOnMouseUpHandler.OnMouseUpAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseUpTrigger GetAsyncMouseUpTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseUpTrigger>(gameObject);
        }
        
        public static AsyncMouseUpTrigger GetAsyncMouseUpTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseUpTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseUpTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseUp()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseUpHandler GetOnMouseUpAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseUpHandler GetOnMouseUpAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseUpAsync()
        {
            return ((IAsyncOnMouseUpHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseUpAsync();
        }

        public UniTask OnMouseUpAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseUpHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseUpAsync();
        }
    }
#endif
#endregion

#region MouseUpAsButton
#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

    public interface IAsyncOnMouseUpAsButtonHandler
    {
        UniTask OnMouseUpAsButtonAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMouseUpAsButtonHandler
    {
        UniTask IAsyncOnMouseUpAsButtonHandler.OnMouseUpAsButtonAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMouseUpAsButtonTrigger GetAsyncMouseUpAsButtonTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMouseUpAsButtonTrigger>(gameObject);
        }
        
        public static AsyncMouseUpAsButtonTrigger GetAsyncMouseUpAsButtonTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMouseUpAsButtonTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMouseUpAsButtonTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnMouseUpAsButton()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnMouseUpAsButtonHandler GetOnMouseUpAsButtonAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnMouseUpAsButtonHandler GetOnMouseUpAsButtonAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnMouseUpAsButtonAsync()
        {
            return ((IAsyncOnMouseUpAsButtonHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseUpAsButtonAsync();
        }

        public UniTask OnMouseUpAsButtonAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMouseUpAsButtonHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseUpAsButtonAsync();
        }
    }
#endif
#endregion

#region ParticleCollision

    public interface IAsyncOnParticleCollisionHandler
    {
        UniTask<GameObject> OnParticleCollisionAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnParticleCollisionHandler
    {
        UniTask<GameObject> IAsyncOnParticleCollisionHandler.OnParticleCollisionAsync()
        {
            core.Reset();
            return new UniTask<GameObject>((IUniTaskSource<GameObject>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncParticleCollisionTrigger GetAsyncParticleCollisionTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncParticleCollisionTrigger>(gameObject);
        }
        
        public static AsyncParticleCollisionTrigger GetAsyncParticleCollisionTrigger(this Component component)
        {
            return component.gameObject.GetAsyncParticleCollisionTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncParticleCollisionTrigger : AsyncTriggerBase<GameObject>
    {
        void OnParticleCollision(GameObject other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnParticleCollisionHandler GetOnParticleCollisionAsyncHandler()
        {
            return new AsyncTriggerHandler<GameObject>(this, false);
        }

        public IAsyncOnParticleCollisionHandler GetOnParticleCollisionAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<GameObject>(this, cancellationToken, false);
        }

        public UniTask<GameObject> OnParticleCollisionAsync()
        {
            return ((IAsyncOnParticleCollisionHandler)new AsyncTriggerHandler<GameObject>(this, true)).OnParticleCollisionAsync();
        }

        public UniTask<GameObject> OnParticleCollisionAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnParticleCollisionHandler)new AsyncTriggerHandler<GameObject>(this, cancellationToken, true)).OnParticleCollisionAsync();
        }
    }
#endregion

#region ParticleSystemStopped

    public interface IAsyncOnParticleSystemStoppedHandler
    {
        UniTask OnParticleSystemStoppedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnParticleSystemStoppedHandler
    {
        UniTask IAsyncOnParticleSystemStoppedHandler.OnParticleSystemStoppedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncParticleSystemStoppedTrigger GetAsyncParticleSystemStoppedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncParticleSystemStoppedTrigger>(gameObject);
        }
        
        public static AsyncParticleSystemStoppedTrigger GetAsyncParticleSystemStoppedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncParticleSystemStoppedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncParticleSystemStoppedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnParticleSystemStopped()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnParticleSystemStoppedHandler GetOnParticleSystemStoppedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnParticleSystemStoppedHandler GetOnParticleSystemStoppedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnParticleSystemStoppedAsync()
        {
            return ((IAsyncOnParticleSystemStoppedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnParticleSystemStoppedAsync();
        }

        public UniTask OnParticleSystemStoppedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnParticleSystemStoppedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnParticleSystemStoppedAsync();
        }
    }
#endregion

#region ParticleTrigger

    public interface IAsyncOnParticleTriggerHandler
    {
        UniTask OnParticleTriggerAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnParticleTriggerHandler
    {
        UniTask IAsyncOnParticleTriggerHandler.OnParticleTriggerAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncParticleTriggerTrigger GetAsyncParticleTriggerTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncParticleTriggerTrigger>(gameObject);
        }
        
        public static AsyncParticleTriggerTrigger GetAsyncParticleTriggerTrigger(this Component component)
        {
            return component.gameObject.GetAsyncParticleTriggerTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncParticleTriggerTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnParticleTrigger()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnParticleTriggerHandler GetOnParticleTriggerAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnParticleTriggerHandler GetOnParticleTriggerAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnParticleTriggerAsync()
        {
            return ((IAsyncOnParticleTriggerHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnParticleTriggerAsync();
        }

        public UniTask OnParticleTriggerAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnParticleTriggerHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnParticleTriggerAsync();
        }
    }
#endregion

#region ParticleUpdateJobScheduled
#if UNITY_2019_3_OR_NEWER && (!UNITY_2019_1_OR_NEWER || UNITASK_PARTICLESYSTEM_SUPPORT)

    public interface IAsyncOnParticleUpdateJobScheduledHandler
    {
        UniTask<UnityEngine.ParticleSystemJobs.ParticleSystemJobData> OnParticleUpdateJobScheduledAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnParticleUpdateJobScheduledHandler
    {
        UniTask<UnityEngine.ParticleSystemJobs.ParticleSystemJobData> IAsyncOnParticleUpdateJobScheduledHandler.OnParticleUpdateJobScheduledAsync()
        {
            core.Reset();
            return new UniTask<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>((IUniTaskSource<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncParticleUpdateJobScheduledTrigger GetAsyncParticleUpdateJobScheduledTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncParticleUpdateJobScheduledTrigger>(gameObject);
        }
        
        public static AsyncParticleUpdateJobScheduledTrigger GetAsyncParticleUpdateJobScheduledTrigger(this Component component)
        {
            return component.gameObject.GetAsyncParticleUpdateJobScheduledTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncParticleUpdateJobScheduledTrigger : AsyncTriggerBase<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>
    {
        void OnParticleUpdateJobScheduled(UnityEngine.ParticleSystemJobs.ParticleSystemJobData particles)
        {
            RaiseEvent((particles));
        }

        public IAsyncOnParticleUpdateJobScheduledHandler GetOnParticleUpdateJobScheduledAsyncHandler()
        {
            return new AsyncTriggerHandler<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>(this, false);
        }

        public IAsyncOnParticleUpdateJobScheduledHandler GetOnParticleUpdateJobScheduledAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>(this, cancellationToken, false);
        }

        public UniTask<UnityEngine.ParticleSystemJobs.ParticleSystemJobData> OnParticleUpdateJobScheduledAsync()
        {
            return ((IAsyncOnParticleUpdateJobScheduledHandler)new AsyncTriggerHandler<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>(this, true)).OnParticleUpdateJobScheduledAsync();
        }

        public UniTask<UnityEngine.ParticleSystemJobs.ParticleSystemJobData> OnParticleUpdateJobScheduledAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnParticleUpdateJobScheduledHandler)new AsyncTriggerHandler<UnityEngine.ParticleSystemJobs.ParticleSystemJobData>(this, cancellationToken, true)).OnParticleUpdateJobScheduledAsync();
        }
    }
#endif
#endregion

#region PostRender

    public interface IAsyncOnPostRenderHandler
    {
        UniTask OnPostRenderAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPostRenderHandler
    {
        UniTask IAsyncOnPostRenderHandler.OnPostRenderAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPostRenderTrigger GetAsyncPostRenderTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPostRenderTrigger>(gameObject);
        }
        
        public static AsyncPostRenderTrigger GetAsyncPostRenderTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPostRenderTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPostRenderTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnPostRender()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnPostRenderHandler GetOnPostRenderAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnPostRenderHandler GetOnPostRenderAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnPostRenderAsync()
        {
            return ((IAsyncOnPostRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPostRenderAsync();
        }

        public UniTask OnPostRenderAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPostRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPostRenderAsync();
        }
    }
#endregion

#region PreCull

    public interface IAsyncOnPreCullHandler
    {
        UniTask OnPreCullAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPreCullHandler
    {
        UniTask IAsyncOnPreCullHandler.OnPreCullAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPreCullTrigger GetAsyncPreCullTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPreCullTrigger>(gameObject);
        }
        
        public static AsyncPreCullTrigger GetAsyncPreCullTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPreCullTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPreCullTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnPreCull()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnPreCullHandler GetOnPreCullAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnPreCullHandler GetOnPreCullAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnPreCullAsync()
        {
            return ((IAsyncOnPreCullHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPreCullAsync();
        }

        public UniTask OnPreCullAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPreCullHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPreCullAsync();
        }
    }
#endregion

#region PreRender

    public interface IAsyncOnPreRenderHandler
    {
        UniTask OnPreRenderAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPreRenderHandler
    {
        UniTask IAsyncOnPreRenderHandler.OnPreRenderAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPreRenderTrigger GetAsyncPreRenderTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPreRenderTrigger>(gameObject);
        }
        
        public static AsyncPreRenderTrigger GetAsyncPreRenderTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPreRenderTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPreRenderTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnPreRender()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnPreRenderHandler GetOnPreRenderAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnPreRenderHandler GetOnPreRenderAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnPreRenderAsync()
        {
            return ((IAsyncOnPreRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPreRenderAsync();
        }

        public UniTask OnPreRenderAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPreRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPreRenderAsync();
        }
    }
#endregion

#region RectTransformDimensionsChange

    public interface IAsyncOnRectTransformDimensionsChangeHandler
    {
        UniTask OnRectTransformDimensionsChangeAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnRectTransformDimensionsChangeHandler
    {
        UniTask IAsyncOnRectTransformDimensionsChangeHandler.OnRectTransformDimensionsChangeAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncRectTransformDimensionsChangeTrigger GetAsyncRectTransformDimensionsChangeTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncRectTransformDimensionsChangeTrigger>(gameObject);
        }
        
        public static AsyncRectTransformDimensionsChangeTrigger GetAsyncRectTransformDimensionsChangeTrigger(this Component component)
        {
            return component.gameObject.GetAsyncRectTransformDimensionsChangeTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncRectTransformDimensionsChangeTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnRectTransformDimensionsChange()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnRectTransformDimensionsChangeHandler GetOnRectTransformDimensionsChangeAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnRectTransformDimensionsChangeHandler GetOnRectTransformDimensionsChangeAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnRectTransformDimensionsChangeAsync()
        {
            return ((IAsyncOnRectTransformDimensionsChangeHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRectTransformDimensionsChangeAsync();
        }

        public UniTask OnRectTransformDimensionsChangeAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnRectTransformDimensionsChangeHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRectTransformDimensionsChangeAsync();
        }
    }
#endregion

#region RectTransformRemoved

    public interface IAsyncOnRectTransformRemovedHandler
    {
        UniTask OnRectTransformRemovedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnRectTransformRemovedHandler
    {
        UniTask IAsyncOnRectTransformRemovedHandler.OnRectTransformRemovedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncRectTransformRemovedTrigger GetAsyncRectTransformRemovedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncRectTransformRemovedTrigger>(gameObject);
        }
        
        public static AsyncRectTransformRemovedTrigger GetAsyncRectTransformRemovedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncRectTransformRemovedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncRectTransformRemovedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnRectTransformRemoved()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnRectTransformRemovedHandler GetOnRectTransformRemovedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnRectTransformRemovedHandler GetOnRectTransformRemovedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnRectTransformRemovedAsync()
        {
            return ((IAsyncOnRectTransformRemovedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRectTransformRemovedAsync();
        }

        public UniTask OnRectTransformRemovedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnRectTransformRemovedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRectTransformRemovedAsync();
        }
    }
#endregion

#region RenderImage

    public interface IAsyncOnRenderImageHandler
    {
        UniTask<(RenderTexture source, RenderTexture destination)> OnRenderImageAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnRenderImageHandler
    {
        UniTask<(RenderTexture source, RenderTexture destination)> IAsyncOnRenderImageHandler.OnRenderImageAsync()
        {
            core.Reset();
            return new UniTask<(RenderTexture source, RenderTexture destination)>((IUniTaskSource<(RenderTexture source, RenderTexture destination)>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncRenderImageTrigger GetAsyncRenderImageTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncRenderImageTrigger>(gameObject);
        }
        
        public static AsyncRenderImageTrigger GetAsyncRenderImageTrigger(this Component component)
        {
            return component.gameObject.GetAsyncRenderImageTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncRenderImageTrigger : AsyncTriggerBase<(RenderTexture source, RenderTexture destination)>
    {
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            RaiseEvent((source, destination));
        }

        public IAsyncOnRenderImageHandler GetOnRenderImageAsyncHandler()
        {
            return new AsyncTriggerHandler<(RenderTexture source, RenderTexture destination)>(this, false);
        }

        public IAsyncOnRenderImageHandler GetOnRenderImageAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<(RenderTexture source, RenderTexture destination)>(this, cancellationToken, false);
        }

        public UniTask<(RenderTexture source, RenderTexture destination)> OnRenderImageAsync()
        {
            return ((IAsyncOnRenderImageHandler)new AsyncTriggerHandler<(RenderTexture source, RenderTexture destination)>(this, true)).OnRenderImageAsync();
        }

        public UniTask<(RenderTexture source, RenderTexture destination)> OnRenderImageAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnRenderImageHandler)new AsyncTriggerHandler<(RenderTexture source, RenderTexture destination)>(this, cancellationToken, true)).OnRenderImageAsync();
        }
    }
#endregion

#region RenderObject

    public interface IAsyncOnRenderObjectHandler
    {
        UniTask OnRenderObjectAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnRenderObjectHandler
    {
        UniTask IAsyncOnRenderObjectHandler.OnRenderObjectAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncRenderObjectTrigger GetAsyncRenderObjectTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncRenderObjectTrigger>(gameObject);
        }
        
        public static AsyncRenderObjectTrigger GetAsyncRenderObjectTrigger(this Component component)
        {
            return component.gameObject.GetAsyncRenderObjectTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncRenderObjectTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnRenderObject()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnRenderObjectHandler GetOnRenderObjectAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnRenderObjectHandler GetOnRenderObjectAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnRenderObjectAsync()
        {
            return ((IAsyncOnRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRenderObjectAsync();
        }

        public UniTask OnRenderObjectAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRenderObjectAsync();
        }
    }
#endregion

#region ServerInitialized

    public interface IAsyncOnServerInitializedHandler
    {
        UniTask OnServerInitializedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnServerInitializedHandler
    {
        UniTask IAsyncOnServerInitializedHandler.OnServerInitializedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncServerInitializedTrigger GetAsyncServerInitializedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncServerInitializedTrigger>(gameObject);
        }
        
        public static AsyncServerInitializedTrigger GetAsyncServerInitializedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncServerInitializedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncServerInitializedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnServerInitialized()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnServerInitializedHandler GetOnServerInitializedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnServerInitializedHandler GetOnServerInitializedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnServerInitializedAsync()
        {
            return ((IAsyncOnServerInitializedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnServerInitializedAsync();
        }

        public UniTask OnServerInitializedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnServerInitializedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnServerInitializedAsync();
        }
    }
#endregion

#region TransformChildrenChanged

    public interface IAsyncOnTransformChildrenChangedHandler
    {
        UniTask OnTransformChildrenChangedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTransformChildrenChangedHandler
    {
        UniTask IAsyncOnTransformChildrenChangedHandler.OnTransformChildrenChangedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTransformChildrenChangedTrigger GetAsyncTransformChildrenChangedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTransformChildrenChangedTrigger>(gameObject);
        }
        
        public static AsyncTransformChildrenChangedTrigger GetAsyncTransformChildrenChangedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTransformChildrenChangedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTransformChildrenChangedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnTransformChildrenChanged()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnTransformChildrenChangedHandler GetOnTransformChildrenChangedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnTransformChildrenChangedHandler GetOnTransformChildrenChangedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnTransformChildrenChangedAsync()
        {
            return ((IAsyncOnTransformChildrenChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnTransformChildrenChangedAsync();
        }

        public UniTask OnTransformChildrenChangedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTransformChildrenChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnTransformChildrenChangedAsync();
        }
    }
#endregion

#region TransformParentChanged

    public interface IAsyncOnTransformParentChangedHandler
    {
        UniTask OnTransformParentChangedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTransformParentChangedHandler
    {
        UniTask IAsyncOnTransformParentChangedHandler.OnTransformParentChangedAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTransformParentChangedTrigger GetAsyncTransformParentChangedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTransformParentChangedTrigger>(gameObject);
        }
        
        public static AsyncTransformParentChangedTrigger GetAsyncTransformParentChangedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTransformParentChangedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTransformParentChangedTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnTransformParentChanged()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnTransformParentChangedHandler GetOnTransformParentChangedAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnTransformParentChangedHandler GetOnTransformParentChangedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnTransformParentChangedAsync()
        {
            return ((IAsyncOnTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnTransformParentChangedAsync();
        }

        public UniTask OnTransformParentChangedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnTransformParentChangedAsync();
        }
    }
#endregion

#region TriggerEnter
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnTriggerEnterHandler
    {
        UniTask<Collider> OnTriggerEnterAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTriggerEnterHandler
    {
        UniTask<Collider> IAsyncOnTriggerEnterHandler.OnTriggerEnterAsync()
        {
            core.Reset();
            return new UniTask<Collider>((IUniTaskSource<Collider>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTriggerEnterTrigger GetAsyncTriggerEnterTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerEnterTrigger>(gameObject);
        }
        
        public static AsyncTriggerEnterTrigger GetAsyncTriggerEnterTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerEnterTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTriggerEnterTrigger : AsyncTriggerBase<Collider>
    {
        void OnTriggerEnter(Collider other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnTriggerEnterHandler GetOnTriggerEnterAsyncHandler()
        {
            return new AsyncTriggerHandler<Collider>(this, false);
        }

        public IAsyncOnTriggerEnterHandler GetOnTriggerEnterAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
        }

        public UniTask<Collider> OnTriggerEnterAsync()
        {
            return ((IAsyncOnTriggerEnterHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerEnterAsync();
        }

        public UniTask<Collider> OnTriggerEnterAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTriggerEnterHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerEnterAsync();
        }
    }
#endif
#endregion

#region TriggerEnter2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnTriggerEnter2DHandler
    {
        UniTask<Collider2D> OnTriggerEnter2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTriggerEnter2DHandler
    {
        UniTask<Collider2D> IAsyncOnTriggerEnter2DHandler.OnTriggerEnter2DAsync()
        {
            core.Reset();
            return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTriggerEnter2DTrigger GetAsyncTriggerEnter2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerEnter2DTrigger>(gameObject);
        }
        
        public static AsyncTriggerEnter2DTrigger GetAsyncTriggerEnter2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerEnter2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTriggerEnter2DTrigger : AsyncTriggerBase<Collider2D>
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnTriggerEnter2DHandler GetOnTriggerEnter2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Collider2D>(this, false);
        }

        public IAsyncOnTriggerEnter2DHandler GetOnTriggerEnter2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
        }

        public UniTask<Collider2D> OnTriggerEnter2DAsync()
        {
            return ((IAsyncOnTriggerEnter2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerEnter2DAsync();
        }

        public UniTask<Collider2D> OnTriggerEnter2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTriggerEnter2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerEnter2DAsync();
        }
    }
#endif
#endregion

#region TriggerExit
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnTriggerExitHandler
    {
        UniTask<Collider> OnTriggerExitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTriggerExitHandler
    {
        UniTask<Collider> IAsyncOnTriggerExitHandler.OnTriggerExitAsync()
        {
            core.Reset();
            return new UniTask<Collider>((IUniTaskSource<Collider>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTriggerExitTrigger GetAsyncTriggerExitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerExitTrigger>(gameObject);
        }
        
        public static AsyncTriggerExitTrigger GetAsyncTriggerExitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerExitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTriggerExitTrigger : AsyncTriggerBase<Collider>
    {
        void OnTriggerExit(Collider other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnTriggerExitHandler GetOnTriggerExitAsyncHandler()
        {
            return new AsyncTriggerHandler<Collider>(this, false);
        }

        public IAsyncOnTriggerExitHandler GetOnTriggerExitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
        }

        public UniTask<Collider> OnTriggerExitAsync()
        {
            return ((IAsyncOnTriggerExitHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerExitAsync();
        }

        public UniTask<Collider> OnTriggerExitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTriggerExitHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerExitAsync();
        }
    }
#endif
#endregion

#region TriggerExit2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnTriggerExit2DHandler
    {
        UniTask<Collider2D> OnTriggerExit2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTriggerExit2DHandler
    {
        UniTask<Collider2D> IAsyncOnTriggerExit2DHandler.OnTriggerExit2DAsync()
        {
            core.Reset();
            return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTriggerExit2DTrigger GetAsyncTriggerExit2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerExit2DTrigger>(gameObject);
        }
        
        public static AsyncTriggerExit2DTrigger GetAsyncTriggerExit2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerExit2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTriggerExit2DTrigger : AsyncTriggerBase<Collider2D>
    {
        void OnTriggerExit2D(Collider2D other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnTriggerExit2DHandler GetOnTriggerExit2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Collider2D>(this, false);
        }

        public IAsyncOnTriggerExit2DHandler GetOnTriggerExit2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
        }

        public UniTask<Collider2D> OnTriggerExit2DAsync()
        {
            return ((IAsyncOnTriggerExit2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerExit2DAsync();
        }

        public UniTask<Collider2D> OnTriggerExit2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTriggerExit2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerExit2DAsync();
        }
    }
#endif
#endregion

#region TriggerStay
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS_SUPPORT

    public interface IAsyncOnTriggerStayHandler
    {
        UniTask<Collider> OnTriggerStayAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTriggerStayHandler
    {
        UniTask<Collider> IAsyncOnTriggerStayHandler.OnTriggerStayAsync()
        {
            core.Reset();
            return new UniTask<Collider>((IUniTaskSource<Collider>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTriggerStayTrigger GetAsyncTriggerStayTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerStayTrigger>(gameObject);
        }
        
        public static AsyncTriggerStayTrigger GetAsyncTriggerStayTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerStayTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTriggerStayTrigger : AsyncTriggerBase<Collider>
    {
        void OnTriggerStay(Collider other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnTriggerStayHandler GetOnTriggerStayAsyncHandler()
        {
            return new AsyncTriggerHandler<Collider>(this, false);
        }

        public IAsyncOnTriggerStayHandler GetOnTriggerStayAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
        }

        public UniTask<Collider> OnTriggerStayAsync()
        {
            return ((IAsyncOnTriggerStayHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerStayAsync();
        }

        public UniTask<Collider> OnTriggerStayAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTriggerStayHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerStayAsync();
        }
    }
#endif
#endregion

#region TriggerStay2D
#if !UNITY_2019_1_OR_NEWER || UNITASK_PHYSICS2D_SUPPORT

    public interface IAsyncOnTriggerStay2DHandler
    {
        UniTask<Collider2D> OnTriggerStay2DAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnTriggerStay2DHandler
    {
        UniTask<Collider2D> IAsyncOnTriggerStay2DHandler.OnTriggerStay2DAsync()
        {
            core.Reset();
            return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncTriggerStay2DTrigger GetAsyncTriggerStay2DTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncTriggerStay2DTrigger>(gameObject);
        }
        
        public static AsyncTriggerStay2DTrigger GetAsyncTriggerStay2DTrigger(this Component component)
        {
            return component.gameObject.GetAsyncTriggerStay2DTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncTriggerStay2DTrigger : AsyncTriggerBase<Collider2D>
    {
        void OnTriggerStay2D(Collider2D other)
        {
            RaiseEvent((other));
        }

        public IAsyncOnTriggerStay2DHandler GetOnTriggerStay2DAsyncHandler()
        {
            return new AsyncTriggerHandler<Collider2D>(this, false);
        }

        public IAsyncOnTriggerStay2DHandler GetOnTriggerStay2DAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
        }

        public UniTask<Collider2D> OnTriggerStay2DAsync()
        {
            return ((IAsyncOnTriggerStay2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerStay2DAsync();
        }

        public UniTask<Collider2D> OnTriggerStay2DAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnTriggerStay2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerStay2DAsync();
        }
    }
#endif
#endregion

#region Validate

    public interface IAsyncOnValidateHandler
    {
        UniTask OnValidateAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnValidateHandler
    {
        UniTask IAsyncOnValidateHandler.OnValidateAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncValidateTrigger GetAsyncValidateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncValidateTrigger>(gameObject);
        }
        
        public static AsyncValidateTrigger GetAsyncValidateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncValidateTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncValidateTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnValidate()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnValidateHandler GetOnValidateAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnValidateHandler GetOnValidateAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnValidateAsync()
        {
            return ((IAsyncOnValidateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnValidateAsync();
        }

        public UniTask OnValidateAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnValidateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnValidateAsync();
        }
    }
#endregion

#region WillRenderObject

    public interface IAsyncOnWillRenderObjectHandler
    {
        UniTask OnWillRenderObjectAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnWillRenderObjectHandler
    {
        UniTask IAsyncOnWillRenderObjectHandler.OnWillRenderObjectAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncWillRenderObjectTrigger GetAsyncWillRenderObjectTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncWillRenderObjectTrigger>(gameObject);
        }
        
        public static AsyncWillRenderObjectTrigger GetAsyncWillRenderObjectTrigger(this Component component)
        {
            return component.gameObject.GetAsyncWillRenderObjectTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncWillRenderObjectTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void OnWillRenderObject()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncOnWillRenderObjectHandler GetOnWillRenderObjectAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncOnWillRenderObjectHandler GetOnWillRenderObjectAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask OnWillRenderObjectAsync()
        {
            return ((IAsyncOnWillRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnWillRenderObjectAsync();
        }

        public UniTask OnWillRenderObjectAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnWillRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnWillRenderObjectAsync();
        }
    }
#endregion

#region Reset

    public interface IAsyncResetHandler
    {
        UniTask ResetAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncResetHandler
    {
        UniTask IAsyncResetHandler.ResetAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncResetTrigger GetAsyncResetTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncResetTrigger>(gameObject);
        }
        
        public static AsyncResetTrigger GetAsyncResetTrigger(this Component component)
        {
            return component.gameObject.GetAsyncResetTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncResetTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void Reset()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncResetHandler GetResetAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncResetHandler GetResetAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask ResetAsync()
        {
            return ((IAsyncResetHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).ResetAsync();
        }

        public UniTask ResetAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncResetHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).ResetAsync();
        }
    }
#endregion

#region Update

    public interface IAsyncUpdateHandler
    {
        UniTask UpdateAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncUpdateHandler
    {
        UniTask IAsyncUpdateHandler.UpdateAsync()
        {
            core.Reset();
            return new UniTask((IUniTaskSource)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncUpdateTrigger GetAsyncUpdateTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncUpdateTrigger>(gameObject);
        }
        
        public static AsyncUpdateTrigger GetAsyncUpdateTrigger(this Component component)
        {
            return component.gameObject.GetAsyncUpdateTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncUpdateTrigger : AsyncTriggerBase<AsyncUnit>
    {
        void Update()
        {
            RaiseEvent(AsyncUnit.Default);
        }

        public IAsyncUpdateHandler GetUpdateAsyncHandler()
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, false);
        }

        public IAsyncUpdateHandler GetUpdateAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
        }

        public UniTask UpdateAsync()
        {
            return ((IAsyncUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).UpdateAsync();
        }

        public UniTask UpdateAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).UpdateAsync();
        }
    }
#endregion

#region BeginDrag
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnBeginDragHandler
    {
        UniTask<PointerEventData> OnBeginDragAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnBeginDragHandler
    {
        UniTask<PointerEventData> IAsyncOnBeginDragHandler.OnBeginDragAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncBeginDragTrigger GetAsyncBeginDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncBeginDragTrigger>(gameObject);
        }
        
        public static AsyncBeginDragTrigger GetAsyncBeginDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncBeginDragTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncBeginDragTrigger : AsyncTriggerBase<PointerEventData>, IBeginDragHandler
    {
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnBeginDragHandler GetOnBeginDragAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnBeginDragHandler GetOnBeginDragAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnBeginDragAsync()
        {
            return ((IAsyncOnBeginDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnBeginDragAsync();
        }

        public UniTask<PointerEventData> OnBeginDragAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnBeginDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnBeginDragAsync();
        }
    }
#endif
#endregion

#region Cancel
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnCancelHandler
    {
        UniTask<BaseEventData> OnCancelAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnCancelHandler
    {
        UniTask<BaseEventData> IAsyncOnCancelHandler.OnCancelAsync()
        {
            core.Reset();
            return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncCancelTrigger GetAsyncCancelTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncCancelTrigger>(gameObject);
        }
        
        public static AsyncCancelTrigger GetAsyncCancelTrigger(this Component component)
        {
            return component.gameObject.GetAsyncCancelTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncCancelTrigger : AsyncTriggerBase<BaseEventData>, ICancelHandler
    {
        void ICancelHandler.OnCancel(BaseEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnCancelHandler GetOnCancelAsyncHandler()
        {
            return new AsyncTriggerHandler<BaseEventData>(this, false);
        }

        public IAsyncOnCancelHandler GetOnCancelAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
        }

        public UniTask<BaseEventData> OnCancelAsync()
        {
            return ((IAsyncOnCancelHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnCancelAsync();
        }

        public UniTask<BaseEventData> OnCancelAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnCancelHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnCancelAsync();
        }
    }
#endif
#endregion

#region Deselect
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnDeselectHandler
    {
        UniTask<BaseEventData> OnDeselectAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnDeselectHandler
    {
        UniTask<BaseEventData> IAsyncOnDeselectHandler.OnDeselectAsync()
        {
            core.Reset();
            return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDeselectTrigger GetAsyncDeselectTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDeselectTrigger>(gameObject);
        }
        
        public static AsyncDeselectTrigger GetAsyncDeselectTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDeselectTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDeselectTrigger : AsyncTriggerBase<BaseEventData>, IDeselectHandler
    {
        void IDeselectHandler.OnDeselect(BaseEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnDeselectHandler GetOnDeselectAsyncHandler()
        {
            return new AsyncTriggerHandler<BaseEventData>(this, false);
        }

        public IAsyncOnDeselectHandler GetOnDeselectAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
        }

        public UniTask<BaseEventData> OnDeselectAsync()
        {
            return ((IAsyncOnDeselectHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnDeselectAsync();
        }

        public UniTask<BaseEventData> OnDeselectAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnDeselectHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnDeselectAsync();
        }
    }
#endif
#endregion

#region Drag
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnDragHandler
    {
        UniTask<PointerEventData> OnDragAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnDragHandler
    {
        UniTask<PointerEventData> IAsyncOnDragHandler.OnDragAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDragTrigger GetAsyncDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDragTrigger>(gameObject);
        }
        
        public static AsyncDragTrigger GetAsyncDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDragTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDragTrigger : AsyncTriggerBase<PointerEventData>, IDragHandler
    {
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnDragHandler GetOnDragAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnDragHandler GetOnDragAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnDragAsync()
        {
            return ((IAsyncOnDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnDragAsync();
        }

        public UniTask<PointerEventData> OnDragAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnDragAsync();
        }
    }
#endif
#endregion

#region Drop
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnDropHandler
    {
        UniTask<PointerEventData> OnDropAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnDropHandler
    {
        UniTask<PointerEventData> IAsyncOnDropHandler.OnDropAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncDropTrigger GetAsyncDropTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncDropTrigger>(gameObject);
        }
        
        public static AsyncDropTrigger GetAsyncDropTrigger(this Component component)
        {
            return component.gameObject.GetAsyncDropTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncDropTrigger : AsyncTriggerBase<PointerEventData>, IDropHandler
    {
        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnDropHandler GetOnDropAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnDropHandler GetOnDropAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnDropAsync()
        {
            return ((IAsyncOnDropHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnDropAsync();
        }

        public UniTask<PointerEventData> OnDropAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnDropHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnDropAsync();
        }
    }
#endif
#endregion

#region EndDrag
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnEndDragHandler
    {
        UniTask<PointerEventData> OnEndDragAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnEndDragHandler
    {
        UniTask<PointerEventData> IAsyncOnEndDragHandler.OnEndDragAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncEndDragTrigger GetAsyncEndDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncEndDragTrigger>(gameObject);
        }
        
        public static AsyncEndDragTrigger GetAsyncEndDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncEndDragTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncEndDragTrigger : AsyncTriggerBase<PointerEventData>, IEndDragHandler
    {
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnEndDragHandler GetOnEndDragAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnEndDragHandler GetOnEndDragAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnEndDragAsync()
        {
            return ((IAsyncOnEndDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnEndDragAsync();
        }

        public UniTask<PointerEventData> OnEndDragAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnEndDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnEndDragAsync();
        }
    }
#endif
#endregion

#region InitializePotentialDrag
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnInitializePotentialDragHandler
    {
        UniTask<PointerEventData> OnInitializePotentialDragAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnInitializePotentialDragHandler
    {
        UniTask<PointerEventData> IAsyncOnInitializePotentialDragHandler.OnInitializePotentialDragAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncInitializePotentialDragTrigger GetAsyncInitializePotentialDragTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncInitializePotentialDragTrigger>(gameObject);
        }
        
        public static AsyncInitializePotentialDragTrigger GetAsyncInitializePotentialDragTrigger(this Component component)
        {
            return component.gameObject.GetAsyncInitializePotentialDragTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncInitializePotentialDragTrigger : AsyncTriggerBase<PointerEventData>, IInitializePotentialDragHandler
    {
        void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnInitializePotentialDragHandler GetOnInitializePotentialDragAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnInitializePotentialDragHandler GetOnInitializePotentialDragAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnInitializePotentialDragAsync()
        {
            return ((IAsyncOnInitializePotentialDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnInitializePotentialDragAsync();
        }

        public UniTask<PointerEventData> OnInitializePotentialDragAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnInitializePotentialDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnInitializePotentialDragAsync();
        }
    }
#endif
#endregion

#region Move
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnMoveHandler
    {
        UniTask<AxisEventData> OnMoveAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnMoveHandler
    {
        UniTask<AxisEventData> IAsyncOnMoveHandler.OnMoveAsync()
        {
            core.Reset();
            return new UniTask<AxisEventData>((IUniTaskSource<AxisEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncMoveTrigger GetAsyncMoveTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncMoveTrigger>(gameObject);
        }
        
        public static AsyncMoveTrigger GetAsyncMoveTrigger(this Component component)
        {
            return component.gameObject.GetAsyncMoveTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncMoveTrigger : AsyncTriggerBase<AxisEventData>, IMoveHandler
    {
        void IMoveHandler.OnMove(AxisEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnMoveHandler GetOnMoveAsyncHandler()
        {
            return new AsyncTriggerHandler<AxisEventData>(this, false);
        }

        public IAsyncOnMoveHandler GetOnMoveAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<AxisEventData>(this, cancellationToken, false);
        }

        public UniTask<AxisEventData> OnMoveAsync()
        {
            return ((IAsyncOnMoveHandler)new AsyncTriggerHandler<AxisEventData>(this, true)).OnMoveAsync();
        }

        public UniTask<AxisEventData> OnMoveAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnMoveHandler)new AsyncTriggerHandler<AxisEventData>(this, cancellationToken, true)).OnMoveAsync();
        }
    }
#endif
#endregion

#region PointerClick
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnPointerClickHandler
    {
        UniTask<PointerEventData> OnPointerClickAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPointerClickHandler
    {
        UniTask<PointerEventData> IAsyncOnPointerClickHandler.OnPointerClickAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPointerClickTrigger GetAsyncPointerClickTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerClickTrigger>(gameObject);
        }
        
        public static AsyncPointerClickTrigger GetAsyncPointerClickTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerClickTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPointerClickTrigger : AsyncTriggerBase<PointerEventData>, IPointerClickHandler
    {
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnPointerClickHandler GetOnPointerClickAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnPointerClickHandler GetOnPointerClickAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnPointerClickAsync()
        {
            return ((IAsyncOnPointerClickHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerClickAsync();
        }

        public UniTask<PointerEventData> OnPointerClickAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPointerClickHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerClickAsync();
        }
    }
#endif
#endregion

#region PointerDown
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnPointerDownHandler
    {
        UniTask<PointerEventData> OnPointerDownAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPointerDownHandler
    {
        UniTask<PointerEventData> IAsyncOnPointerDownHandler.OnPointerDownAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPointerDownTrigger GetAsyncPointerDownTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerDownTrigger>(gameObject);
        }
        
        public static AsyncPointerDownTrigger GetAsyncPointerDownTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerDownTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPointerDownTrigger : AsyncTriggerBase<PointerEventData>, IPointerDownHandler
    {
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnPointerDownHandler GetOnPointerDownAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnPointerDownHandler GetOnPointerDownAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnPointerDownAsync()
        {
            return ((IAsyncOnPointerDownHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerDownAsync();
        }

        public UniTask<PointerEventData> OnPointerDownAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPointerDownHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerDownAsync();
        }
    }
#endif
#endregion

#region PointerEnter
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnPointerEnterHandler
    {
        UniTask<PointerEventData> OnPointerEnterAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPointerEnterHandler
    {
        UniTask<PointerEventData> IAsyncOnPointerEnterHandler.OnPointerEnterAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPointerEnterTrigger GetAsyncPointerEnterTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerEnterTrigger>(gameObject);
        }
        
        public static AsyncPointerEnterTrigger GetAsyncPointerEnterTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerEnterTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPointerEnterTrigger : AsyncTriggerBase<PointerEventData>, IPointerEnterHandler
    {
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnPointerEnterHandler GetOnPointerEnterAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnPointerEnterHandler GetOnPointerEnterAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnPointerEnterAsync()
        {
            return ((IAsyncOnPointerEnterHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerEnterAsync();
        }

        public UniTask<PointerEventData> OnPointerEnterAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPointerEnterHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerEnterAsync();
        }
    }
#endif
#endregion

#region PointerExit
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnPointerExitHandler
    {
        UniTask<PointerEventData> OnPointerExitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPointerExitHandler
    {
        UniTask<PointerEventData> IAsyncOnPointerExitHandler.OnPointerExitAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPointerExitTrigger GetAsyncPointerExitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerExitTrigger>(gameObject);
        }
        
        public static AsyncPointerExitTrigger GetAsyncPointerExitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerExitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPointerExitTrigger : AsyncTriggerBase<PointerEventData>, IPointerExitHandler
    {
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnPointerExitHandler GetOnPointerExitAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnPointerExitHandler GetOnPointerExitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnPointerExitAsync()
        {
            return ((IAsyncOnPointerExitHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerExitAsync();
        }

        public UniTask<PointerEventData> OnPointerExitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPointerExitHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerExitAsync();
        }
    }
#endif
#endregion

#region PointerUp
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnPointerUpHandler
    {
        UniTask<PointerEventData> OnPointerUpAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnPointerUpHandler
    {
        UniTask<PointerEventData> IAsyncOnPointerUpHandler.OnPointerUpAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncPointerUpTrigger GetAsyncPointerUpTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncPointerUpTrigger>(gameObject);
        }
        
        public static AsyncPointerUpTrigger GetAsyncPointerUpTrigger(this Component component)
        {
            return component.gameObject.GetAsyncPointerUpTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncPointerUpTrigger : AsyncTriggerBase<PointerEventData>, IPointerUpHandler
    {
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnPointerUpHandler GetOnPointerUpAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnPointerUpHandler GetOnPointerUpAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnPointerUpAsync()
        {
            return ((IAsyncOnPointerUpHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerUpAsync();
        }

        public UniTask<PointerEventData> OnPointerUpAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnPointerUpHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerUpAsync();
        }
    }
#endif
#endregion

#region Scroll
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnScrollHandler
    {
        UniTask<PointerEventData> OnScrollAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnScrollHandler
    {
        UniTask<PointerEventData> IAsyncOnScrollHandler.OnScrollAsync()
        {
            core.Reset();
            return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncScrollTrigger GetAsyncScrollTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncScrollTrigger>(gameObject);
        }
        
        public static AsyncScrollTrigger GetAsyncScrollTrigger(this Component component)
        {
            return component.gameObject.GetAsyncScrollTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncScrollTrigger : AsyncTriggerBase<PointerEventData>, IScrollHandler
    {
        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnScrollHandler GetOnScrollAsyncHandler()
        {
            return new AsyncTriggerHandler<PointerEventData>(this, false);
        }

        public IAsyncOnScrollHandler GetOnScrollAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
        }

        public UniTask<PointerEventData> OnScrollAsync()
        {
            return ((IAsyncOnScrollHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnScrollAsync();
        }

        public UniTask<PointerEventData> OnScrollAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnScrollHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnScrollAsync();
        }
    }
#endif
#endregion

#region Select
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnSelectHandler
    {
        UniTask<BaseEventData> OnSelectAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnSelectHandler
    {
        UniTask<BaseEventData> IAsyncOnSelectHandler.OnSelectAsync()
        {
            core.Reset();
            return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncSelectTrigger GetAsyncSelectTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncSelectTrigger>(gameObject);
        }
        
        public static AsyncSelectTrigger GetAsyncSelectTrigger(this Component component)
        {
            return component.gameObject.GetAsyncSelectTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncSelectTrigger : AsyncTriggerBase<BaseEventData>, ISelectHandler
    {
        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnSelectHandler GetOnSelectAsyncHandler()
        {
            return new AsyncTriggerHandler<BaseEventData>(this, false);
        }

        public IAsyncOnSelectHandler GetOnSelectAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
        }

        public UniTask<BaseEventData> OnSelectAsync()
        {
            return ((IAsyncOnSelectHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnSelectAsync();
        }

        public UniTask<BaseEventData> OnSelectAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnSelectHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnSelectAsync();
        }
    }
#endif
#endregion

#region Submit
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnSubmitHandler
    {
        UniTask<BaseEventData> OnSubmitAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnSubmitHandler
    {
        UniTask<BaseEventData> IAsyncOnSubmitHandler.OnSubmitAsync()
        {
            core.Reset();
            return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncSubmitTrigger GetAsyncSubmitTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncSubmitTrigger>(gameObject);
        }
        
        public static AsyncSubmitTrigger GetAsyncSubmitTrigger(this Component component)
        {
            return component.gameObject.GetAsyncSubmitTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncSubmitTrigger : AsyncTriggerBase<BaseEventData>, ISubmitHandler
    {
        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnSubmitHandler GetOnSubmitAsyncHandler()
        {
            return new AsyncTriggerHandler<BaseEventData>(this, false);
        }

        public IAsyncOnSubmitHandler GetOnSubmitAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
        }

        public UniTask<BaseEventData> OnSubmitAsync()
        {
            return ((IAsyncOnSubmitHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnSubmitAsync();
        }

        public UniTask<BaseEventData> OnSubmitAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnSubmitHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnSubmitAsync();
        }
    }
#endif
#endregion

#region UpdateSelected
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT

    public interface IAsyncOnUpdateSelectedHandler
    {
        UniTask<BaseEventData> OnUpdateSelectedAsync();
    }

    public partial class AsyncTriggerHandler<T> : IAsyncOnUpdateSelectedHandler
    {
        UniTask<BaseEventData> IAsyncOnUpdateSelectedHandler.OnUpdateSelectedAsync()
        {
            core.Reset();
            return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)(object)this, core.Version);
        }
    }

    public static partial class AsyncTriggerExtensions
    {
        public static AsyncUpdateSelectedTrigger GetAsyncUpdateSelectedTrigger(this GameObject gameObject)
        {
            return GetOrAddComponent<AsyncUpdateSelectedTrigger>(gameObject);
        }
        
        public static AsyncUpdateSelectedTrigger GetAsyncUpdateSelectedTrigger(this Component component)
        {
            return component.gameObject.GetAsyncUpdateSelectedTrigger();
        }
    }

    [DisallowMultipleComponent]
    public sealed class AsyncUpdateSelectedTrigger : AsyncTriggerBase<BaseEventData>, IUpdateSelectedHandler
    {
        void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
        {
            RaiseEvent((eventData));
        }

        public IAsyncOnUpdateSelectedHandler GetOnUpdateSelectedAsyncHandler()
        {
            return new AsyncTriggerHandler<BaseEventData>(this, false);
        }

        public IAsyncOnUpdateSelectedHandler GetOnUpdateSelectedAsyncHandler(CancellationToken cancellationToken)
        {
            return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
        }

        public UniTask<BaseEventData> OnUpdateSelectedAsync()
        {
            return ((IAsyncOnUpdateSelectedHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnUpdateSelectedAsync();
        }

        public UniTask<BaseEventData> OnUpdateSelectedAsync(CancellationToken cancellationToken)
        {
            return ((IAsyncOnUpdateSelectedHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnUpdateSelectedAsync();
        }
    }
#endif
#endregion

}