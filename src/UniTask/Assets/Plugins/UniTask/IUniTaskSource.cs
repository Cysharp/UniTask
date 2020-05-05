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
    {
        UniTaskStatus GetStatus(short token);
        void OnCompleted(Action<object> continuation, object state, short token);
        void GetResult(short token);

        UniTaskStatus UnsafeGetStatus(); // only for debug use.
    }

    public interface IUniTaskSource<out T> : IUniTaskSource
    {
        new T GetResult(short token);
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

