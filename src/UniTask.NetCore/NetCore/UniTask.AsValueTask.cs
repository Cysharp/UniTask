#pragma warning disable 0649

using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskValueTaskExtensions
    {
        public static ValueTask AsValueTask(this in UniTask task)
        {
            return task;
        }

        public static ValueTask<T> AsValueTask<T>(this in UniTask<T> task)
        {
            return task;
        }

        public static UniTask<T> AsUniTask<T>(this ValueTask<T> task, bool useCurrentSynchronizationContext = true)
        {
            // NOTE: get _obj and _token directly for low overhead conversion but not yet implemented.
            return task.AsTask().AsUniTask(useCurrentSynchronizationContext);
        }

        public static UniTask AsUniTask(this ValueTask task, bool useCurrentSynchronizationContext = true)
        {
            return task.AsTask().AsUniTask(useCurrentSynchronizationContext);
        }
    }
}
