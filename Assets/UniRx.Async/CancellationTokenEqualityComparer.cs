#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Threading;

namespace UniRx.Async
{
    public class CancellationTokenEqualityComparer : IEqualityComparer<CancellationToken>
    {
        public static readonly IEqualityComparer<CancellationToken> Default = new CancellationTokenEqualityComparer();

        public bool Equals(CancellationToken x, CancellationToken y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(CancellationToken obj)
        {
            return obj.GetHashCode();
        }
    }
}

#endif