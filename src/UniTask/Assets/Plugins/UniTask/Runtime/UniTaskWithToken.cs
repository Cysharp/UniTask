using System;
using System.Collections.Generic;
using System.Threading;

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
        public UniTask NextFrame(PlayerLoopTiming timing = PlayerLoopTiming.Update)
            => UniTask.NextFrame(timing, cancellationToken);
        public UniTask WaitForEndOfFrame()
            => UniTask.WaitForEndOfFrame(cancellationToken);
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
        public void Void(Func<CancellationToken, UniTaskVoid> asyncAction)
            => UniTask.Void(asyncAction, cancellationToken);
        public Action Action(Func<CancellationToken, UniTaskVoid> asyncAction)
            => UniTask.Action(asyncAction, cancellationToken);
#if UNITY_2018_3_OR_NEWER
        public UnityEngine.Events.UnityAction UnityAction(Func<CancellationToken, UniTaskVoid> asyncAction)
            => UniTask.UnityAction(asyncAction, cancellationToken);
#endif
        public UniTask Never()
            => UniTask.Never(cancellationToken);
        public UniTask<T> Never<T>()
            => UniTask.Never<T>(cancellationToken);
        #endregion
        #region UniTask.Run
        public async UniTask RunOnThreadPool(Action action, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
        public async UniTask RunOnThreadPool(Action<object> action, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
        public async UniTask RunOnThreadPool(Func<UniTask> action, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, configureAwait, cancellationToken);
        public async UniTask RunOnThreadPool(Func<object, UniTask> action, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(action, state, configureAwait, cancellationToken);
        public async UniTask<T> RunOnThreadPool<T>(Func<T> func, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, configureAwait, cancellationToken);
        public async UniTask<T> RunOnThreadPool<T>(Func<UniTask<T>> func, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, configureAwait, cancellationToken);
        public async UniTask<T> RunOnThreadPool<T>(Func<object, T> func, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, state, configureAwait, cancellationToken);
        public async UniTask<T> RunOnThreadPool<T>(Func<object, UniTask<T>> func, object state, bool configureAwait = true)
            => await UniTask.RunOnThreadPool(func, state, configureAwait, cancellationToken);
        #endregion
        #region UniTask.Threading
#if UNITY_2018_3_OR_NEWER
        public SwitchToMainThreadAwaitable SwitchToMainThread()
            => UniTask.SwitchToMainThread(cancellationToken);
        public SwitchToMainThreadAwaitable SwitchToMainThread(PlayerLoopTiming timing)
            => UniTask.SwitchToMainThread(timing, cancellationToken);
        public ReturnToMainThread ReturnToMainThread()
            => UniTask.ReturnToMainThread(cancellationToken);
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
    }
}
