#pragma warning disable 0649

#if UNITASK_NETCORE || UNITY_2022_3_OR_NEWER
#define SUPPORT_VALUETASK
#endif

#if SUPPORT_VALUETASK

using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskValueTaskExtensions
    {
        public static ValueTask AsValueTask(this in UniTask task)
        {
#if (UNITASK_NETCORE && NETSTANDARD2_0)
            return new ValueTask(new UniTaskValueTaskSource(task), 0);
#else
            return task;
#endif
        }

        public static ValueTask<T> AsValueTask<T>(this in UniTask<T> task)
        {
#if (UNITASK_NETCORE && NETSTANDARD2_0)
            return new ValueTask<T>(new UniTaskValueTaskSource<T>(task), 0);
#else
            return task;
#endif
        }

        public static async UniTask<T> AsUniTask<T>(this ValueTask<T> task)
        {
            return await task;
        }

        public static async UniTask AsUniTask(this ValueTask task)
        {
            await task;
        }

#if (UNITASK_NETCORE && NETSTANDARD2_0)

        class UniTaskValueTaskSource : IValueTaskSource
        {
            readonly UniTask task;
            readonly UniTask.Awaiter awaiter;

            public UniTaskValueTaskSource(UniTask task)
            {
                this.task = task;
                this.awaiter = task.GetAwaiter();
            }

            public void GetResult(short token)
            {
                awaiter.GetResult();
            }

            public ValueTaskSourceStatus GetStatus(short token)
            {
                return (ValueTaskSourceStatus)task.Status;
            }

            public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
            {
                awaiter.SourceOnCompleted(continuation, state);
            }
        }

        class UniTaskValueTaskSource<T> : IValueTaskSource<T>
        {
            readonly UniTask<T> task;
            readonly UniTask<T>.Awaiter awaiter;

            public UniTaskValueTaskSource(UniTask<T> task)
            {
                this.task = task;
                this.awaiter = task.GetAwaiter();
            }

            public T GetResult(short token)
            {
                return awaiter.GetResult();
            }

            public ValueTaskSourceStatus GetStatus(short token)
            {
                return (ValueTaskSourceStatus)task.Status;
            }

            public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
            {
                awaiter.SourceOnCompleted(continuation, state);
            }
        }

#endif
    }
}
#endif
