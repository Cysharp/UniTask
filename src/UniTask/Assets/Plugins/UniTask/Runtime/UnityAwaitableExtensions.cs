#if UNITY_2023_1_OR_NEWER
namespace Cysharp.Threading.Tasks
{
    public static class UnityAwaitableExtensions
    {
        public static async UniTask AsUniTask(this UnityEngine.Awaitable awaitable)
        {
            await awaitable;
        }
        
        public static async UniTask<T> AsUniTask<T>(this UnityEngine.Awaitable<T> awaitable)
        {
            return await awaitable;
        }
    }
}
#endif
