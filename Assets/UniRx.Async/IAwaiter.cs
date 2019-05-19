#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace UniRx.Async
{
    public enum AwaiterStatus
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

    public interface IAwaiter : ICriticalNotifyCompletion
    {
        AwaiterStatus Status { get; }
        bool IsCompleted { get; }
        void GetResult();
    }

    public interface IAwaiter<out T> : IAwaiter
    {
        new T GetResult();
    }

    public static class AwaiterStatusExtensions
    {
        /// <summary>!= Pending.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCompleted(this AwaiterStatus status)
        {
            return status != AwaiterStatus.Pending;
        }

        /// <summary>== Succeeded.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCompletedSuccessfully(this AwaiterStatus status)
        {
            return status == AwaiterStatus.Succeeded;
        }

        /// <summary>== Canceled.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCanceled(this AwaiterStatus status)
        {
            return status == AwaiterStatus.Canceled;
        }

        /// <summary>== Faulted.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFaulted(this AwaiterStatus status)
        {
            return status == AwaiterStatus.Faulted;
        }
    }
}

#endif