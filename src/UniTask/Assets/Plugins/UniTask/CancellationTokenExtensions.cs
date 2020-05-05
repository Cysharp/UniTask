#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
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

            var promise = new UniTaskCompletionSource();
            return (promise.Task, cts.RegisterWithoutCaptureExecutionContext(cancellationTokenCallback, promise));
        }

        static void Callback(object state)
        {
            var promise = (UniTaskCompletionSource)state;
            promise.TrySetResult();
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

