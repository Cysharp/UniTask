#pragma warning disable CS1591

using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
    public enum UniTaskStatus
    {
        /// <summary>The operation has not yet completed.</summary>
        Pending = 0,
        /// <summary>The operation completed successfully.</summary>
        Succeeded = 1,
        /// <summary>The operation completed with an error.</summary>
        Faulted = 2,
        /// <summary>The operation completed due to cancellation.</summary>
        Canceled = 3
    }

    // similar as IValueTaskSource
    public interface IUniTaskSource
#if !UNITY_2018_3_OR_NEWER && !NETSTANDARD2_0
        : System.Threading.Tasks.Sources.IValueTaskSource
#pragma warning disable CS0108
#endif
    {
        UniTaskStatus GetStatus(short token);
        void OnCompleted(Action<object> continuation, object state, short token);
        void GetResult(short token);

        UniTaskStatus UnsafeGetStatus(); // only for debug use.

#if !UNITY_2018_3_OR_NEWER && !NETSTANDARD2_0
#pragma warning restore CS0108

        System.Threading.Tasks.Sources.ValueTaskSourceStatus System.Threading.Tasks.Sources.IValueTaskSource.GetStatus(short token)
        {
            return (System.Threading.Tasks.Sources.ValueTaskSourceStatus)(int)((IUniTaskSource)this).GetStatus(token);
        }

        void System.Threading.Tasks.Sources.IValueTaskSource.GetResult(short token)
        {
            ((IUniTaskSource)this).GetResult(token);
        }

        void System.Threading.Tasks.Sources.IValueTaskSource.OnCompleted(Action<object> continuation, object state, short token, System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags flags)
        {
            // ignore flags, always none.
            ((IUniTaskSource)this).OnCompleted(continuation, state, token);
        }

#endif
    }

    public interface IUniTaskSource<out T> : IUniTaskSource
#if !UNITY_2018_3_OR_NEWER && !NETSTANDARD2_0
        , System.Threading.Tasks.Sources.IValueTaskSource<T>
#endif
    {
        new T GetResult(short token);

#if !UNITY_2018_3_OR_NEWER && !NETSTANDARD2_0

        new public UniTaskStatus GetStatus(short token)
        {
            return ((IUniTaskSource)this).GetStatus(token);
        }

        new public void OnCompleted(Action<object> continuation, object state, short token)
        {
            ((IUniTaskSource)this).OnCompleted(continuation, state, token);
        }

        System.Threading.Tasks.Sources.ValueTaskSourceStatus System.Threading.Tasks.Sources.IValueTaskSource<T>.GetStatus(short token)
        {
            return (System.Threading.Tasks.Sources.ValueTaskSourceStatus)(int)((IUniTaskSource)this).GetStatus(token);
        }

        T System.Threading.Tasks.Sources.IValueTaskSource<T>.GetResult(short token)
        {
            return ((IUniTaskSource<T>)this).GetResult(token);
        }

        void System.Threading.Tasks.Sources.IValueTaskSource<T>.OnCompleted(Action<object> continuation, object state, short token, System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags flags)
        {
            // ignore flags, always none.
            ((IUniTaskSource)this).OnCompleted(continuation, state, token);
        }

#endif
    }

    public static class UniTaskStatusExtensions
    {
        /// <summary>status != Pending.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCompleted(this UniTaskStatus status)
        {
            return status != UniTaskStatus.Pending;
        }

        /// <summary>status == Succeeded.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCompletedSuccessfully(this UniTaskStatus status)
        {
            return status == UniTaskStatus.Succeeded;
        }

        /// <summary>status == Canceled.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCanceled(this UniTaskStatus status)
        {
            return status == UniTaskStatus.Canceled;
        }

        /// <summary>status == Faulted.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFaulted(this UniTaskStatus status)
        {
            return status == UniTaskStatus.Faulted;
        }
    }
}

