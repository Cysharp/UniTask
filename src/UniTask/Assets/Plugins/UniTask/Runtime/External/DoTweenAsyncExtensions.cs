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
    // The idea of TweenCancelBehaviour is borrowed from https://www.shibuya24.info/entry/dotween_async_await
    public enum TweenCancelBehaviour
    {
        Kill,
        KillWithCompleteCallback,
        Complete,
        CompleteWithSeqeunceCallback,
        CancelAwait,

        // AndCancelAwait
        KillAndCancelAwait,
        KillWithCompleteCallbackAndCancelAwait,
        CompleteAndCancelAwait,
        CompleteWithSeqeunceCallbackAndCancelAwait
    }

    public static class DOTweenAsyncExtensions
    {
        public static TweenAwaiter GetAwaiter(this Tween tween)
        {
            return new TweenAwaiter(tween);
        }

        public static UniTask WithCancellation(this Tween tween, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, TweenCancelBehaviour.Kill, cancellationToken, out var token), token);
        }

        public static UniTask ToUniTask(this Tween tween, TweenCancelBehaviour tweenCancelBehaviour = TweenCancelBehaviour.Kill, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(tween, nameof(tween));

            if (!tween.IsActive()) return UniTask.CompletedTask;
            return new UniTask(TweenConfiguredSource.Create(tween, tweenCancelBehaviour, cancellationToken, out var token), token);
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
            public TweenConfiguredSource NextNode { get; set; }

            static TweenConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(TweenConfiguredSource), () => pool.Size);
            }

            static readonly TweenCallback EmptyTweenCallback = () => { };

            readonly TweenCallback onKillDelegate;
            readonly TweenCallback onUpdateDelegate;

            Tween tween;
            TweenCancelBehaviour cancelBehaviour;
            CancellationToken cancellationToken;
            bool canceled;

            TweenCallback originalUpdateAction;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            TweenConfiguredSource()
            {
                onKillDelegate = OnKill;
                onUpdateDelegate = OnUpdate;
            }

            public static IUniTaskSource Create(Tween tween, TweenCancelBehaviour cancelBehaviour, CancellationToken cancellationToken, out short token)
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

                result.originalUpdateAction = tween.onUpdate;
                result.canceled = false;

                if (result.originalUpdateAction == result.onUpdateDelegate)
                {
                    result.originalUpdateAction = null;
                }

                tween.onUpdate = result.onUpdateDelegate;
                tween.onKill = result.onKillDelegate;

                TaskTracker.TrackActiveTask(result, 3);

                token = result.core.Version;
                return result;
            }

            void OnKill()
            {
                if (canceled)
                {
                    core.TrySetCanceled(cancellationToken);
                }
                else
                {
                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            void OnUpdate()
            {
                originalUpdateAction?.Invoke();

                if (!cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                switch (this.cancelBehaviour)
                {
                    case TweenCancelBehaviour.Kill:
                    default:
                        this.tween.Kill(false);
                        break;
                    case TweenCancelBehaviour.KillAndCancelAwait:
                        this.canceled = true;
                        this.tween.Kill(false);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallback:
                        this.tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait:
                        this.canceled = true;
                        this.tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.Complete:
                        this.tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteAndCancelAwait:
                        this.canceled = true;
                        this.tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallback:
                        this.tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallbackAndCancelAwait:
                        this.canceled = true;
                        this.tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CancelAwait:
                        this.tween.onKill = EmptyTweenCallback; // replace to empty(avoid callback after Canceled(instance is returned to pool.)
                        this.core.TrySetCanceled(this.cancellationToken);
                        break;
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
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallback:
                        tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallbackAndCancelAwait:
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
                tween.onUpdate = originalUpdateAction;
                tween.onKill = null;
                tween = default;
                cancellationToken = default;
                originalUpdateAction = default;
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
