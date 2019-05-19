#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))

using System;

namespace UniRx.Async
{
    public static class ExceptionExtensions
    {
        public static bool IsOperationCanceledException(this Exception exception)
        {
            return exception is OperationCanceledException;
        }
    }
}

#endif