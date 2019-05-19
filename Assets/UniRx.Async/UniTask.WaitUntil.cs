#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Threading;
using UniRx.Async.Internal;

namespace UniRx.Async
{
    public partial struct UniTask
    {
        public static UniTask WaitUntil(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var promise = new WaitUntilPromise(predicate, timing, cancellationToken);
            return promise.Task;
        }

        public static UniTask WaitWhile(Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var promise = new WaitWhilePromise(predicate, timing, cancellationToken);
            return promise.Task;
        }

        public static UniTask<U> WaitUntilValueChanged<T, U>(T target, Func<T, U> monitorFunction, PlayerLoopTiming monitorTiming = PlayerLoopTiming.Update, IEqualityComparer<U> equalityComparer = null, CancellationToken cancellationToken = default(CancellationToken))
          where T : class
        {
            var unityObject = target as UnityEngine.Object;
            var isUnityObject = !object.ReferenceEquals(target, null); // don't use (unityObject == null)

            return (isUnityObject)
                ? new WaitUntilValueChangedUnityObjectPromise<T, U>(target, monitorFunction, equalityComparer, monitorTiming, cancellationToken).Task
                : new WaitUntilValueChangedStandardObjectPromise<T, U>(target, monitorFunction, equalityComparer, monitorTiming, cancellationToken).Task;
        }

        class WaitUntilPromise : PlayerLoopReusablePromiseBase
        {
            readonly Func<bool> predicate;

            public WaitUntilPromise(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 1)
            {
                this.predicate = predicate;
            }

            protected override void OnRunningStart()
            {
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                bool result = default(bool);
                try
                {
                    result = predicate();
                }
                catch (Exception ex)
                {
                    Complete();
                    TrySetException(ex);
                    return false;
                }

                if (result)
                {
                    Complete();
                    TrySetResult();
                    return false;
                }

                return true;
            }
        }

        class WaitWhilePromise : PlayerLoopReusablePromiseBase
        {
            readonly Func<bool> predicate;

            public WaitWhilePromise(Func<bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 1)
            {
                this.predicate = predicate;
            }

            protected override void OnRunningStart()
            {
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                bool result = default(bool);
                try
                {
                    result = predicate();
                }
                catch (Exception ex)
                {
                    Complete();
                    TrySetException(ex);
                    return false;
                }

                if (!result)
                {
                    Complete();
                    TrySetResult();
                    return false;
                }

                return true;
            }
        }

        // where T : UnityEngine.Object, can not add constraint
        class WaitUntilValueChangedUnityObjectPromise<T, U> : PlayerLoopReusablePromiseBase<U>
        {
            readonly T target;
            readonly Func<T, U> monitorFunction;
            readonly IEqualityComparer<U> equalityComparer;
            U currentValue;

            public WaitUntilValueChangedUnityObjectPromise(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 1)
            {
                this.target = target;
                this.monitorFunction = monitorFunction;
                this.equalityComparer = equalityComparer ?? UnityEqualityComparer.GetDefault<U>();
                this.currentValue = monitorFunction(target);
            }

            protected override void OnRunningStart()
            {
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested || target == null) // destroyed = cancel.
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                U nextValue = default(U);
                try
                {
                    nextValue = monitorFunction(target);
                    if (equalityComparer.Equals(currentValue, nextValue))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Complete();
                    TrySetException(ex);
                    return false;
                }

                Complete();
                currentValue = nextValue;
                TrySetResult(nextValue);
                return false;
            }
        }

        class WaitUntilValueChangedStandardObjectPromise<T, U> : PlayerLoopReusablePromiseBase<U>
            where T : class
        {
            readonly WeakReference<T> target;
            readonly Func<T, U> monitorFunction;
            readonly IEqualityComparer<U> equalityComparer;
            U currentValue;

            public WaitUntilValueChangedStandardObjectPromise(T target, Func<T, U> monitorFunction, IEqualityComparer<U> equalityComparer, PlayerLoopTiming timing, CancellationToken cancellationToken)
                : base(timing, cancellationToken, 1)
            {
                this.target = new WeakReference<T>(target, false); // wrap in WeakReference.
                this.monitorFunction = monitorFunction;
                this.equalityComparer = equalityComparer ?? UnityEqualityComparer.GetDefault<U>();
                this.currentValue = monitorFunction(target);
            }

            protected override void OnRunningStart()
            {
            }

            public override bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested || !target.TryGetTarget(out var t))
                {
                    Complete();
                    TrySetCanceled();
                    return false;
                }

                U nextValue = default(U);
                try
                {
                    nextValue = monitorFunction(t);
                    if (equalityComparer.Equals(currentValue, nextValue))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Complete();
                    TrySetException(ex);
                    return false;
                }

                Complete();
                currentValue = nextValue;
                TrySetResult(nextValue);
                return false;
            }
        }
    }
}
#endif