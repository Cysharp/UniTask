using System;
using System.Threading;

namespace UniRx.Async
{
    public static partial class UnityAsyncExtensions
    {
        public static void StartAsyncCoroutine(this UnityEngine.MonoBehaviour monoBehaviour, Func<CancellationToken, UniTask> asyncCoroutine)
        {
            var token = monoBehaviour.GetCancellationTokenOnDestroy();
            asyncCoroutine(token).Forget();
        }
    }
}