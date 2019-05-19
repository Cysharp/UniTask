#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or 

using System;

namespace UniRx.Async
{
    public struct AsyncUnit : IEquatable<AsyncUnit>
    {
        public static readonly AsyncUnit Default = new AsyncUnit();

        public override int GetHashCode()
        {
            return 0;
        }

        public bool Equals(AsyncUnit other)
        {
            return true;
        }

        public override string ToString()
        {
            return "()";
        }
    }
}
#endif