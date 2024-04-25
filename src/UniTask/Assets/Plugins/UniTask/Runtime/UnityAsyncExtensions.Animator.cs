using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        public static UniTask WaitAnimationComplete(this Animator animator, string animationName, int layer = 0,
            IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.PostLateUpdate)
        {
            return WaitAnimationComplete(animator, Animator.StringToHash(animationName), layer, progress, timing,
                cancellationToken: animator.GetCancellationTokenOnDestroy());
        }

        public static UniTask WaitAnimationComplete(this Animator animator, int layer = 0,
            IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.PostLateUpdate)
        {
            return WaitAnimationComplete(animator, -1, layer, progress, timing,
                cancellationToken: animator.GetCancellationTokenOnDestroy());
        }

        public static UniTask WaitAnimationComplete(this Animator animator, int animationHash = -1, int layer = 0,
            IProgress<float> progress = null,
            PlayerLoopTiming timing = PlayerLoopTiming.PostLateUpdate, CancellationToken cancellationToken = default,
            bool cancelImmediately = false)
        {
            if (animator == null)
                throw new ArgumentNullException(nameof(animator));

            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled(cancellationToken);

            return new UniTask(
                AnimatorStateSource.Create(animator, animationHash, layer, timing, progress, cancellationToken,
                    cancelImmediately,
                    out var token), token);
        }

        sealed class AnimatorStateSource : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<AnimatorStateSource>
        {
            private static TaskPool<AnimatorStateSource> _pool;
            private AnimatorStateSource _nextNode;

            public ref AnimatorStateSource NextNode => ref _nextNode;

            static AnimatorStateSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AnimatorStateSource), () => _pool.Size);
            }

            private Animator _animator;
            private int _animationHash;
            private int _layer;
            private IProgress<float> _progress;
            private CancellationToken _cancellationToken;
            private CancellationTokenRegistration _cancellationTokenRegistration;
            private bool _cancelImmediately;
            private bool _completed;

            private UniTaskCompletionSourceCore<AsyncUnit> _core;

            AnimatorStateSource()
            {
            }

            public static IUniTaskSource Create(Animator animator, int animation, int layer, PlayerLoopTiming timing,
                IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!_pool.TryPop(out var result))
                {
                    result = new AnimatorStateSource();
                }

                result._animator = animator;
                result._animationHash = animation;
                result._layer = layer;
                result._progress = progress;
                result._cancellationToken = cancellationToken;
                result._cancelImmediately = cancelImmediately;
                result._completed = false;

                if (result._cancelImmediately && result._cancellationToken.CanBeCanceled)
                {
                    result._cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(
                        state =>
                        {
                            var source = (AnimatorStateSource)state;
                            source._core.TrySetCanceled(source._cancellationToken);
                        }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);
                PlayerLoopHelper.AddAction(timing, result);

                token = result._core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    _core.GetResult(token);
                }
                finally
                {
                    if (!(_cancelImmediately && _cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return _core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return _core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                _core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (_completed || _animator == null || !_animator.enabled)
                {
                    return false;
                }

                if (_cancellationToken.IsCancellationRequested)
                {
                    _core.TrySetCanceled(_cancellationToken);
                    return false;
                }

                AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(_layer);

                if (_animationHash != -1 && stateInfo.shortNameHash != _animationHash)
                    return true;

                float normalizedTime = stateInfo.normalizedTime;
                float progressValue = Mathf.Clamp01(normalizedTime);

                _progress?.Report(progressValue);

                if (progressValue < 1f)
                    return true;

                _core.TrySetResult(AsyncUnit.Default);
                return false;
            }

            private bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);

                _core.Reset();
                _animator = default;
                _progress = default;
                _cancellationToken = default;
                _cancellationTokenRegistration.Dispose();
                _cancelImmediately = default;

                return _pool.TryPush(this);
            }
        }
    }
}