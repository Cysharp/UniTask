// asmdef Version Defines, enabled when com.demigiant.dotween is imported.

#if UNITASK_DOTWEEN_SUPPORT

using Cysharp.Threading.Tasks.Internal;
using DG.Tweening;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public enum TweenCancelBehaviour
    {
        Kill,
        KillWithCompleteCallback,
        Complete,
        CompleteWithSequenceCallback,
        CancelAwait,

        // AndCancelAwait
        KillAndCancelAwait,
        KillWithCompleteCallbackAndCancelAwait,
        CompleteAndCancelAwait,
        CompleteWithSequenceCallbackAndCancelAwait
    }

    public static class DOTweenAsyncExtensions
    {
        enum CallbackType
        {
            Kill,
            Complete,
            Pause,
            Play,
            Rewind,
            StepComplete
        }

        public static TweenAwaiter GetAwaiter(this Tween tween)
        {
            return new TweenAwaiter(tween);
        }

        public static UniTask WithCancellation(this Tween tween, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, TweenCancelBehaviour.Kill, cancellationToken, CallbackType.Kill, out var token), token);
        }

        public static UniTask ToUniTask(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, CallbackType.Kill, out var token), token);
        }

        public static UniTask AwaitForComplete(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, CallbackType.Complete, out var token), token);
        }

        public static UniTask AwaitForPause(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, CallbackType.Pause, out var token), token);
        }

        public static UniTask AwaitForPlay(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, CallbackType.Play, out var token), token);
        }

        public static UniTask AwaitForRewind(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, CallbackType.Rewind, out var token), token);
        }

        public static UniTask AwaitForStepComplete(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, CallbackType.StepComplete, out var token), token);
        }

        public struct TweenAwaiter : ICriticalNotifyCompletion
        {
            readonly Tween tween;

            // killed(non active) as completed.
            public bool IsCompleted => !tween.IsActive();

            public TweenAwaiter(Tween tween)
            {
                this.tween = tween;
            }

            public TweenAwaiter GetAwaiter()
            {
                return this;
            }

            public void GetResult()
            {
            }

            public void OnCompleted(System.Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(System.Action continuation)
            {
                // onKill is called after OnCompleted, both Complete(false/true) and Kill(false/true).
                tween.onKill = PooledTweenCallback.Create(continuation);
            }
        }

        sealed class TweenConfiguredSource : IUniTaskSource, ITaskPoolNode<TweenConfiguredSource>
        {
            static TaskPool<TweenConfiguredSource> pool;
            TweenConfiguredSource nextNode;
            public ref TweenConfiguredSource NextNode => ref nextNode;

            static TweenConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(TweenConfiguredSource), () => pool.Size);
            }

            readonly TweenCallback onCompleteCallbackDelegate;

            Tween tween;
            TweenCancelBehaviour cancelBehaviour;
            CancellationToken cancellationToken;
            CallbackType callbackType;
            bool canceled;

            TweenCallback originalCompleteAction;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            TweenConfiguredSource()
            {
                onCompleteCallbackDelegate = OnCompleteCallbackDelegate;
            }

            public static IUniTaskSource Create(Tween tween, TweenCancelBehaviour cancelBehaviour, CancellationToken cancellationToken, CallbackType callbackType, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    DoCancelBeforeCreate(tween, cancelBehaviour);
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new TweenConfiguredSource();
                }

                result.tween = tween;
                result.cancelBehaviour = cancelBehaviour;
                result.cancellationToken = cancellationToken;
                result.callbackType = callbackType;
                result.canceled = false;

                switch (callbackType)
                {
                    case CallbackType.Kill:
                        result.originalCompleteAction = tween.onKill;
                        tween.onKill = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Complete:
                        result.originalCompleteAction = tween.onComplete;
                        tween.onComplete = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Pause:
                        result.originalCompleteAction = tween.onPause;
                        tween.onPause = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Play:
                        result.originalCompleteAction = tween.onPlay;
                        tween.onPlay = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.Rewind:
                        result.originalCompleteAction = tween.onRewind;
                        tween.onRewind = result.onCompleteCallbackDelegate;
                        break;
                    case CallbackType.StepComplete:
                        result.originalCompleteAction = tween.onStepComplete;
                        tween.onStepComplete = result.onCompleteCallbackDelegate;
                        break;
                    default:
                        break;
                }
                
                if (result.originalCompleteAction == result.onCompleteCallbackDelegate)
                {
                    result.originalCompleteAction = null;
                }

                if (cancellationToken.CanBeCanceled)
                {
                    cancellationToken.RegisterWithoutCaptureExecutionContext(x =>
                    {
                        var source = (TweenConfiguredSource)x;
                        switch (source.cancelBehaviour)
                        {
                            case TweenCancelBehaviour.Kill:
                            default:
                                source.tween.Kill(false);
                                break;
                            case TweenCancelBehaviour.KillAndCancelAwait:
                                source.canceled = true;
                                source.tween.Kill(false);
                                break;
                            case TweenCancelBehaviour.KillWithCompleteCallback:
                                source.tween.Kill(true);
                                break;
                            case TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait:
                                source.canceled = true;
                                source.tween.Kill(true);
                                break;
                            case TweenCancelBehaviour.Complete:
                                source.tween.Complete(false);
                                break;
                            case TweenCancelBehaviour.CompleteAndCancelAwait:
                                source.canceled = true;
                                source.tween.Complete(false);
                                break;
                            case TweenCancelBehaviour.CompleteWithSequenceCallback:
                                source.tween.Complete(true);
                                break;
                            case TweenCancelBehaviour.CompleteWithSequenceCallbackAndCancelAwait:
                                source.canceled = true;
                                source.tween.Complete(true);
                                break;
                            case TweenCancelBehaviour.CancelAwait:
                                // restore to original callback
                                switch (callbackType)
                                {
                                    case CallbackType.Kill:
                                        tween.onKill = source.originalCompleteAction;
                                        break;
                                    case CallbackType.Complete:
                                        tween.onComplete = source.originalCompleteAction;
                                        break;
                                    case CallbackType.Pause:
                                        tween.onPause = source.originalCompleteAction;
                                        break;
                                    case CallbackType.Play:
                                        tween.onPlay = source.originalCompleteAction;
                                        break;
                                    case CallbackType.Rewind:
                                        tween.onRewind = source.originalCompleteAction;
                                        break;
                                    case CallbackType.StepComplete:
                                        tween.onStepComplete = source.originalCompleteAction;
                                        break;
                                    default:
                                        break;
                                }
                                source.core.TrySetCanceled(source.cancellationToken);
                                break;
                        }
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            void OnCompleteCallbackDelegate()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    if (this.cancelBehaviour == TweenCancelBehaviour.KillAndCancelAwait
                        || this.cancelBehaviour == TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait
                        || this.cancelBehaviour == TweenCancelBehaviour.CompleteAndCancelAwait
                        || this.cancelBehaviour == TweenCancelBehaviour.CompleteWithSequenceCallbackAndCancelAwait
                        || this.cancelBehaviour == TweenCancelBehaviour.CancelAwait)
                    {
                        canceled = true;
                    }
                }
                if (canceled)
                {
                    core.TrySetCanceled(cancellationToken);
                }
                else
                {
                    originalCompleteAction?.Invoke();
                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            static void DoCancelBeforeCreate(Tween tween, TweenCancelBehaviour tweenCancelBehaviour)
            {

                switch (tweenCancelBehaviour)
                {
                    case TweenCancelBehaviour.Kill:
                    default:
                        tween.Kill(false);
                        break;
                    case TweenCancelBehaviour.KillAndCancelAwait:
                        tween.Kill(false);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallback:
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait:
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.Complete:
                        tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteAndCancelAwait:
                        tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteWithSequenceCallback:
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CompleteWithSequenceCallbackAndCancelAwait:
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CancelAwait:
                        break;
                }
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    TryReturn();
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();

                switch (callbackType)
                {
                    case CallbackType.Kill:
                        tween.onKill = originalCompleteAction;
                        break;
                    case CallbackType.Complete:
                        tween.onComplete = originalCompleteAction;
                        break;
                    case CallbackType.Pause:
                        tween.onPause = originalCompleteAction;
                        break;
                    case CallbackType.Play:
                        tween.onPlay = originalCompleteAction;
                        break;
                    case CallbackType.Rewind:
                        tween.onRewind = originalCompleteAction;
                        break;
                    case CallbackType.StepComplete:
                        tween.onStepComplete = originalCompleteAction;
                        break;
                    default:
                        break;
                }

                tween = default;
                cancellationToken = default;
                originalCompleteAction = default;
                return pool.TryPush(this);
            }
        }
    }

    sealed class PooledTweenCallback
    {
        static readonly ConcurrentQueue<PooledTweenCallback> pool = new ConcurrentQueue<PooledTweenCallback>();

        readonly TweenCallback runDelegate;

        Action continuation;


        PooledTweenCallback()
        {
            runDelegate = Run;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenCallback Create(Action continuation)
        {
            if (!pool.TryDequeue(out var item))
            {
                item = new PooledTweenCallback();
            }

            item.continuation = continuation;
            return item.runDelegate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
        {
            var call = continuation;
            continuation = null;
            if (call != null)
            {
                pool.Enqueue(this);
                call.Invoke();
            }
        }
    }
}

#endif
