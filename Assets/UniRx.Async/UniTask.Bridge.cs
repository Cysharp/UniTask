#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections;

namespace UniRx.Async
{
    // UnityEngine Bridges.

    public partial struct UniTask
    {
        public static IEnumerator ToCoroutine(Func<UniTask> taskFactory)
        {
            return taskFactory().ToCoroutine();
        }
    }
}

#endif