#pragma warning disable CS1591
#pragma warning disable CS0436

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Cysharp.Threading.Tasks
{
    [AsyncMethodBuilder(typeof(AsyncUniTaskVoidMethodBuilder))]
    public readonly struct UniTaskVoid
    {
        public void Forget()
        {
        }

        //        [DebuggerHidden]
        //        public Awaiter GetAwaiter()
        //        {
        //            return new Awaiter();
        //        }

        //        public struct Awaiter : ICriticalNotifyCompletion
        //        {
        //            [DebuggerHidden]
        //            public bool IsCompleted => true;

        //            [DebuggerHidden]
        //            public void GetResult()
        //            {
        //#if UNITY_2018_3_OR_NEWER
        //                UnityEngine.Debug.LogWarning("UniTaskVoid can't await, always fire-and-forget. use Forget instead of await.");
        //#endif
        //            }

        //            [DebuggerHidden]
        //            public void OnCompleted(Action continuation)
        //            {
        //            }

        //            [DebuggerHidden]
        //            public void UnsafeOnCompleted(Action continuation)
        //            {
        //            }
        //        }
    }
}

