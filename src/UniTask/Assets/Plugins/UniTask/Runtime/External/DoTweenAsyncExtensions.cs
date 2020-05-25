// asmdef Version Defines, enabled when com.demigiant.dotween is imported.

#if UNITASK_DOTWEEN_SUPPORT

using Cysharp.Threading.Tasks.Internal;
using DG.Tweening;
using System;
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
                // convert Action -> TweenCallback allocation.
                // onKill is called after OnCompleted, both Complete(false/true) and Kill(false/true).
                tween.onKill = new TweenCallback(continuation);
            }
        }

        sealed class TweenConfiguredSource : IUniTaskSource, IPromisePoolItem
        {
            static readonly PromisePool<TweenConfiguredSource> pool = new PromisePool<TweenConfiguredSource>();
            static readonly Action<object> CancellationCallbackDelegate = CancellationCallback;
            static readonly TweenCallback EmptyTweenCallback = () => { };

            Tween tween;
            TweenCancelBehaviour cancelBehaviour;
            CancellationToken cancellationToken;
            bool canceled;

            CancellationTokenRegistration cancellationTokenRegistration;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            TweenConfiguredSource()
            {

            }

            public static IUniTaskSource Create(Tween tween, TweenCancelBehaviour cancelBehaviour, CancellationToken cancellationToken, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                var result = pool.TryRent() ?? new TweenConfiguredSource();

                result.tween = tween;
                result.cancelBehaviour = cancelBehaviour;
                result.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(result, 3);

                result.RegisterEvent();

                token = result.core.Version;
                return result;
            }

            void RegisterEvent()
            {
                if (cancellationToken.CanBeCanceled)
                {
                    cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(CancellationCallbackDelegate, this);
                }

                // allocate delegate.
                tween.OnKill(new TweenCallback(OnKill));
            }

            void OnKill()
            {
                cancellationTokenRegistration.Dispose();
                if (canceled)
                {
                    core.TrySetCanceled(cancellationToken);
                }
                else
                {
                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            static void CancellationCallback(object state)
            {
                var self = (TweenConfiguredSource)state;

                switch (self.cancelBehaviour)
                {
                    case TweenCancelBehaviour.Kill:
                    default:
                        self.tween.Kill(false);
                        break;
                    case TweenCancelBehaviour.KillAndCancelAwait:
                        self.canceled = true;
                        self.tween.Kill(false);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallback:
                        self.tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallbackAndCancelAwait:
                        self.canceled = true;
                        self.tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.Complete:
                        self.tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteAndCancelAwait:
                        self.canceled = true;
                        self.tween.Complete(false);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallback:
                        self.tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CompleteWithSeqeunceCallbackAndCancelAwait:
                        self.canceled = true;
                        self.tween.Complete(true);
                        break;
                    case TweenCancelBehaviour.CancelAwait:
                        self.tween.onKill = EmptyTweenCallback; // replace to empty(avoid callback after Caceled(instance is returned to pool.)
                        self.core.TrySetCanceled(self.cancellationToken);
                        break;
                }
            }

            public void GetResult(short token)
            {
                try
                {
                    TaskTracker.RemoveTracking(this);
                    core.GetResult(token);
                }
                finally
                {
                    pool.TryReturn(this);
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

            public void Reset()
            {
                core.Reset();
                tween = default;
                cancellationToken = default;
            }

            ~TweenConfiguredSource()
            {
                if (pool.TryReturn(this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }
    }
}

#endif
