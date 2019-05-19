#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;

namespace UniRx.Async.Internal
{
    internal static class Error
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowArgumentNullException<T>(T value, string paramName)
          where T : class
        {
            if (value == null) ThrowArgumentNullExceptionCore(paramName);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void ThrowArgumentNullExceptionCore(string paramName)
        {
            throw new ArgumentNullException(paramName);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentException<T>(string message)
        {
            throw new ArgumentException(message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowNotYetCompleted()
        {
            throw new InvalidOperationException("Not yet completed.");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T ThrowNotYetCompleted<T>()
        {
            throw new InvalidOperationException("Not yet completed.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowWhenContinuationIsAlreadyRegistered<T>(T continuationField)
          where T : class
        {
            if (continuationField != null) ThrowInvalidOperationExceptionCore("continuation is already registered.");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void ThrowInvalidOperationExceptionCore(string message)
        {
            throw new InvalidOperationException(message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowOperationCanceledException()
        {
            throw new OperationCanceledException();
        }
    }
}

#endif