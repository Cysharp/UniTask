#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace UniRx.Async
{
    public static class CancellationTokenExtensions
    {
        static readonly Action<object> cancellationTokenCallback = Callback;

        public static (UniTask, CancellationTokenRegistration) ToUniTask(this CancellationToken cts)
        {
            if (cts.IsCancellationRequested)
            {
                return (UniTask.FromCanceled(cts), default(CancellationTokenRegistration));
            }

            var promise = new UniTaskCompletionSource<AsyncUnit>();
            return (promise.Task, cts.RegisterWithoutCaptureExecutionContext(cancellationTokenCallback, promise));
        }

        static void Callback(object state)
        {
            var promise = (UniTaskCompletionSource<AsyncUnit>)state;
            promise.TrySetResult(AsyncUnit.Default);
        }

        public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(this CancellationToken cancellationToken, Action callback)
        {
            var restoreFlow = false;
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
                restoreFlow = true;
            }

            try
            {
                return cancellationToken.Register(callback, false);
            }
            finally
            {
                if (restoreFlow)
                {
                    ExecutionContext.RestoreFlow();
                }
            }
        }

        public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(this CancellationToken cancellationToken, Action<object> callback, object state)
        {
            var restoreFlow = false;
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
                restoreFlow = true;
            }

            try
            {
                return cancellationToken.Register(callback, state, false);
            }
            finally
            {
                if (restoreFlow)
                {
                    ExecutionContext.RestoreFlow();
                }
            }
        }
    }
}

#endif