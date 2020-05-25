
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
            callMoveNext = Run;
        }

        public static void SetRunner(ref AsyncUniTaskMethodBuilder builder, ref TStateMachine stateMachine)
        {
            var result = pool.TryRent();
            if (result == null)
            {
                result = new MoveNextRunner<TStateMachine>();
            }

            builder.runner = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        public static void SetRunner<T>(ref AsyncUniTaskMethodBuilder<T> builder, ref TStateMachine stateMachine)
        {
            var result = pool.TryRent();
            if (result == null)
            {
                result = new MoveNextRunner<TStateMachine>();
            }

            builder.runner = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        public static void SetRunner(ref AsyncUniTaskVoidMethodBuilder builder, ref TStateMachine stateMachine)
        {
            var result = pool.TryRent();
            if (result == null)
            {
                result = new MoveNextRunner<TStateMachine>();
            }

            builder.runner = result; // set runner before copied.
            result.stateMachine = stateMachine; // copy struct StateMachine(in release build).
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Run()
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

