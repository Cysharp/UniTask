#pragma warning disable CS1591 // Missing XML comment for publicly visible type or 

using System;

namespace Cysharp.Threading.Tasks
{
    public readonly struct AsyncUnit : IEquatable<AsyncUnit>
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
