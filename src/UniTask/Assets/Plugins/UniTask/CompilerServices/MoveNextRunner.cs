
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.CompilerServices
{
    internal interface IMoveNextRunner
    {
        Action CallMoveNext { get; }
        void Return();
    }

    internal sealed class MoveNextRunner<TStateMachine> : IMoveNextRunner, IPromisePoolItem
        where TStateMachine : IAsyncStateMachine
    {
        static PromisePool<MoveNextRunner<TStateMachine>> pool = new PromisePool<MoveNextRunner<TStateMachine>>();

        TStateMachine stateMachine;
        internal readonly Action callMoveNext;

        public Action CallMoveNext => callMoveNext;

        MoveNextRunner()
        {
            callMoveNext = MoveNext;
        }

        public static MoveNextRunner<TStateMachine> Create(ref TStateMachine stateMachine)
        {
            var result = pool.TryRent() ?? new MoveNextRunner<TStateMachine>();
            result.stateMachine = stateMachine;
            return result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void MoveNext()
        {
            stateMachine.MoveNext();
        }

        public void Return()
        {
            pool.TryReturn(this);
        }

        void IPromisePoolItem.Reset()
        {
            stateMachine = default;
        }
    }
}

