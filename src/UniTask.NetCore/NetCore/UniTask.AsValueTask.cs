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
    }
}
