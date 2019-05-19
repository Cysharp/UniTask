#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;

namespace UniRx.Async.Internal
{
    internal static class CancellationTokenHelper
    {
        public static bool TrySetOrLinkCancellationToken(ref CancellationToken field, CancellationToken newCancellationToken)
        {
            if (newCancellationToken == CancellationToken.None)
            {
                return false;
            }
            else if (field == CancellationToken.None)
            {
                field = newCancellationToken;
                return true;
            }
            else if (field == newCancellationToken)
            {
                return false;
            }

            field = CancellationTokenSource.CreateLinkedTokenSource(field, newCancellationToken).Token;
            return true;
        }
    }
}

#endif