using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || UNITASK_WEBREQUEST_SUPPORT)
using UnityEngine.Networking;
#endif
#if UNITASK_ADDRESSABLE_SUPPORT
using UnityEngine.ResourceManagement.AsyncOperations;
#endif
#if UNITASK_DOTWEEN_SUPPORT
using DG.Tweening;
#endif

namespace Cysharp.Threading.Tasks
{
    public readonly struct UniTaskWithToken
    {
        public readonly CancellationToken cancellationToken;
        public UniTaskWithToken(CancellationToken cancellationToken)
            => this.cancellationToken = cancellationToken;
        #region UniTask.Delay
        public UniTask Yield(PlayerLoopTiming timing = PlayerLoopTiming.Update)
            => UniTask.Yield(timing, cancellationToken);
        /// <summary>Similar as UniTask.Yield but guaranteed run on next frame.</summary>
        public UniTask NextFrame(PlayerLoopTiming timing = PlayerLoopTiming.Update)
            => UniTask.NextFrame(timing, cancellationToken);
        /// <summary>Same as UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancellationToken).</summary>
        public UniTask WaitForEndOfFrame()
            => UniTask.WaitForEndOfFrame(cancellationToken);
        /// <summary>Same as UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken).</summary>
        public UniTask WaitForFixedUpdate()
            => UniTask.WaitForFixedUpdate(cancellationToken);
        public UniTask DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
            => UniTask.DelayFrame(delayFrameCount, delayTiming, cancellationToken);
        public UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
            => UniTask.Delay(millisecondsDelay, ignoreTimeScale, delayTiming, cancellationToken);
        public UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
            => UniTask.Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cancellationToken);
        public UniTask Delay(int millisecondsDelay, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
            => UniTask.Delay(millisecondsDelay, delayType, delayTiming, cancellationToken);
        public UniTask Delay(TimeSpan delayTimeSpan, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
            => UniTask.Delay(delayTimeSpan, delayType, delayTiming, cancellationToken);
        #endregion
        #region UniTask.Factory
        public UniTask FromCanceled()
            => UniTask.FromCanceled(cancellationToken);
        public UniTask<T> FromCanceled<T>()
            => UniTask.FromCanceled<T>(cancellationToken);
        /// <summary>helper of fire and forget void action.</summary>
        public void Void(Func<CancellationToken, UniTaskVoid> asyncAction)
            => UniTask.Void(asyncAction, cancellationToken);
        /// <summary>helper of create add UniTaskVoid to delegate.</summary>
        public Action Action(Func<CancellationToken, UniTaskVoid> asyncAction)
            => UniTask.Action(asyncAction, cancellationToken);
#if UNITY_2018_3_OR_NEWER
        /// <summary>Create async void(UniTaskVoid) UnityAction.</summary>
        public UnityEngine.Events.UnityAction UnityAction(Func<CancellationToken, UniTaskVoid> asyncAction)
            => UniTask.UnityAction(asyncAction, cancellationToken);
#endif
        /// <summary>Never complete.</summary>
        public UniTask Never()
            => UniTask.Never(cancellationToken);
        /// <summary>Never complete.</summary>
        public UniTask<T> Never<T>()
            => UniTask.Never<T>(cancellationToken);
        #endregion
        #region UniTask.Run
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask RunOnThreadPool(Action action, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask RunOnThreadPool(Action<object> action, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask RunOnThreadPool(Func<UniTask> action, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask RunOnThreadPool(Func<object, UniTask> action, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask<T> RunOnThreadPool<T>(Func<T> func, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask<T> RunOnThreadPool<T>(Func<UniTask<T>> func, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask<T> RunOnThreadPool<T>(Func<object, T> func, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, state, configureAwait, cancellationToken);
        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public async UniTask<T> RunOnThreadPool<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, state, configureAwait, cancellationToken);
        #endregion
        #region UniTask.Threading
#if UNITY_2018_3_OR_NEWER
        /// <summary>If running on mainthread, do nothing. Otherwise, same as UniTask.Yield(PlayerLoopTiming.Update, cancellationToken).</summary>
        public SwitchToMainThreadAwaitable SwitchToMainThread()
            => UniTask.SwitchToMainThread(cancellationToken);
        /// <summary>If running on mainthread, do nothing. Otherwise, same as UniTask.Yield(timing, cancellationToken).</summary>
        public SwitchToMainThreadAwaitable SwitchToMainThread(PlayerLoopTiming timing)
            => UniTask.SwitchToMainThread(timing, cancellationToken);
        /// <summary>Return to mainthread(same as await SwitchToMainThread) after using scope is closed.</summary>
        public ReturnToMainThread ReturnToMainThread()
            => UniTask.ReturnToMainThread(cancellationToken);
        /// <summary>Return to mainthread(same as await SwitchToMainThread) after using scope is closed.</summary>
        public ReturnToMainThread ReturnToMainThread(PlayerLoopTiming timing)
            => UniTask.ReturnToMainThread(timing, cancellationToken);
#endif
        public SwitchToSynchronizationContextAwaitable SwitchToSynchronizationContext(SynchronizationContext synchronizationContext)
            => UniTask.SwitchToSynchronizationContext(synchronizationContext, cancellationToken);
        public ReturnToSynchronizationContext ReturnToCurrentSynchronizationContext(bool dontPostWhenSameContext = true)
            => UniTask.ReturnToCurrentSynchronizationContext(dontPostWhenSameContext, cancellationToken);
        #endregion
        #region UniTask.WaitUntil
        public UniTask WaitUntil(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update)
            => UniTask.WaitUntil(predicate, timing, cancellationToken);
        public UniTask WaitWhile(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update)
            => UniTask.WaitWhile(predicate, timing, cancellationToken);
        public UniTask WaitUntilCanceled(CancellationToken cancellationToken, PlayerLoopTiming timing = PlayerLoopTiming.Update)
            => UniTask.WaitUntilCanceled(cancellationToken, timing);
        public UniTask<U> WaitUntilValueChanged<T, U>(T target, Func<T, U> monitorFunction, PlayerLoopTiming monitorTiming = PlayerLoopTiming.Update, IEqualityComparer<U> equalityComparer = null) where T : class
            => UniTask.WaitUntilValueChanged(target, monitorFunction, monitorTiming, equalityComparer, cancellationToken);
        #endregion
        #region Wrap
        public UniTask Wrap(IEnumerator enumerator)
            => enumerator.WithCancellation(cancellationToken);
        public UniTaskCancelableAsyncEnumerable<T> Wrap<T>(IUniTaskAsyncEnumerable<T> source)
            => source.WithCancellation(cancellationToken);
        /// <summary>Ignore task result when cancel raised first.</summary>
        public UniTask Wrap(UniTask task)
            => task.WithCancellation(cancellationToken);
        /// <summary>Ignore task result when cancel raised first.</summary>
        public UniTask<T> Wrap<T>(UniTask<T> task)
            => task.WithCancellation(cancellationToken);
        public UniTask<AsyncGPUReadbackRequest> Wrap(AsyncGPUReadbackRequest asyncOperation)
            => asyncOperation.WithCancellation(cancellationToken);
        public UniTask Wrap(AsyncOperation asyncOperation)
            => asyncOperation.WithCancellation(cancellationToken);
        public UniTask<UnityEngine.Object> Wrap(ResourceRequest asyncOperation)
            => asyncOperation.WithCancellation(cancellationToken);
#if UNITASK_ASSETBUNDLE_SUPPORT
        public UniTask<UnityEngine.Object> Wrap(AssetBundleRequest asyncOperation)
            => asyncOperation.WithCancellation(cancellationToken);
#endif
#if UNITASK_ASSETBUNDLE_SUPPORT
        public UniTask<AssetBundle> Wrap(AssetBundleCreateRequest asyncOperation)
            => asyncOperation.WithCancellation(cancellationToken);
#endif
#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || UNITASK_WEBREQUEST_SUPPORT)
        public UniTask<UnityWebRequest> Wrap(UnityWebRequestAsyncOperation asyncOperation)
            => asyncOperation.WithCancellation(cancellationToken);
#endif
#if UNITASK_ADDRESSABLE_SUPPORT
        public static UniTask Wrap(AsyncOperationHandle handle)
            => handle.WithCancellation(cancellationToken);
#endif
#if UNITASK_DOTWEEN_SUPPORT
        public static UniTask Wrap(Tween tween)
            => tween.WithCancellation(cancellationToken);
#endif
        #endregion
    }
}
