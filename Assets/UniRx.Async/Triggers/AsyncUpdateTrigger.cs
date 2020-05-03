
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx.Async.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Async.Triggers
{
    [DisallowMultipleComponent]
    public class AsyncUpdateTrigger : AsyncTriggerBase
    {
        AsyncTriggerPromise<AsyncUnit> update;
        AsyncTriggerPromiseDictionary<AsyncUnit> updates;


        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            return Concat(update, updates);
        }


        void Update()
        {
            TrySetResult(update, updates, AsyncUnit.Default);
        }


        public UniTask UpdateAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetOrAddPromise(ref update, ref updates, cancellationToken);
        }


    }



    // TODO:remove 2.
    public abstract class AsyncTriggerBase2 : MonoBehaviour
    {
        static readonly Action<object> Callback = CancelCallback;

        bool calledAwake = false;
        bool destroyCalled = false;
        CancellationTokenRegistration[] registeredCancellations;
        int registeredCancellationsCount;

        protected abstract IEnumerable<ICancelablePromise> GetPromises();

        void Awake()
        {
            calledAwake = true;
        }

        void OnDestroy()
        {
            if (destroyCalled) return;
            destroyCalled = true;
            foreach (var item in GetPromises())
            {
                item.TrySetCanceled();
            }
            if (registeredCancellations != null)
            {
                for (int i = 0; i < registeredCancellationsCount; i++)
                {
                    registeredCancellations[i].Dispose();
                    registeredCancellations[i] = default(CancellationTokenRegistration);
                }
                ArrayPool<CancellationTokenRegistration>.Shared.Return(registeredCancellations);
            }
        }

        protected void TrySetResult<T>(MinimumQueue<UniTaskCompletionSource<AsyncUnit>> promise, AsyncTriggerPromiseDictionary<T> promises, T result)
        {
            if (promise != null)
            {
                // TODO:
            }

            if (promises != null)
            {
                PromiseHelper.TrySetResultAll(promises.Values, result);
            }
        }

        public UniTask<T> CreatePromise<T>(ref MinimumQueue<AutoResetUniTaskCompletionSource<T>> promise, ref AsyncTriggerPromiseDictionary<T> promises, CancellationToken cancellationToken)
        {
            if (destroyCalled) return UniTask.FromCanceled<T>();

            if (!calledAwake)
            {
                PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AwakeMonitor(this));
            }

            if (!cancellationToken.CanBeCanceled)
            {
                if (promise == null)
                {
                    promise = new MinimumQueue<AutoResetUniTaskCompletionSource<T>>(4); // kakko kari.(ArrayPool?)
                }

                var tcs = AutoResetUniTaskCompletionSource<T>.Create();
                promise.Enqueue(tcs);
                return tcs.Task;
            }


            CancellationTokenRegistration registrationToken = default;
            // TODO:atode.
            // var registrationToken = cancellationToken.RegisterWithoutCaptureExecutionContext(Callback, Tuple.Create((ICancellationTokenKeyDictionary)promises, (ICancelablePromise)cancellablePromise));
            if (registeredCancellations == null)
            {
                registeredCancellations = ArrayPool<CancellationTokenRegistration>.Shared.Rent(4);
            }

            ArrayPoolUtil.EnsureCapacity(ref registeredCancellations, registeredCancellationsCount + 1, ArrayPool<CancellationTokenRegistration>.Shared);
            registeredCancellations[registeredCancellationsCount++] = registrationToken;

            // TODO:Å™use at registration
            {
                if (promises == null)
                {
                    promises = new AsyncTriggerPromiseDictionary<T>();
                }

                var tcs = AutoResetUniTaskCompletionSource<T>.Create();
                promises.Add(cancellationToken, tcs);
                return tcs.Task;
            }
        }

        static void CancelCallback(object state)
        {
            // TODO:nantokasuru.

            //var tuple = (Tuple<ICancellationTokenKeyDictionary, ICancelablePromise>)state;
            //var dict = tuple.Item1;
            //var promise = tuple.Item2;

            //promise.TrySetCanceled();
            //dict.Remove(promise.RegisteredCancellationToken);
        }

        class AwakeMonitor : IPlayerLoopItem
        {
            readonly AsyncTriggerBase2 trigger;

            public AwakeMonitor(AsyncTriggerBase2 trigger)
            {
                this.trigger = trigger;
            }

            public bool MoveNext()
            {
                if (trigger.calledAwake) return false;
                if (trigger == null)
                {
                    trigger.OnDestroy();
                    return false;
                }
                return true;
            }
        }
    }


    // TODO:remove 2.
    [DisallowMultipleComponent]
    public class AsyncUpdateTrigger2 : AsyncTriggerBase2
    {
        MinimumQueue<UniTaskCompletionSource<AsyncUnit>> promise;
        AsyncTriggerPromiseDictionary<AsyncUnit> promises;

        protected override IEnumerable<ICancelablePromise> GetPromises()
        {
            // TODO:
            throw new NotImplementedException();
        }

        void Update()
        {
            // TrySetResult
        }

        public UniTask UpdateAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return CreatePromise<AsyncUnit>(ref promise, ref promises, cancellationToken).AsUniTask();
        }
    }
}

#endif

