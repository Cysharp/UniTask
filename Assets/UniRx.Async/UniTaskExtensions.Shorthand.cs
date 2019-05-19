#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;

namespace UniRx.Async
{
    public static partial class UniTaskExtensions
    {
        // shorthand of WhenAll

        public static UniTask.Awaiter GetAwaiter(this IEnumerable<UniTask> tasks)
        {
            return UniTask.WhenAll(tasks).GetAwaiter();
        }

        public static UniTask<T[]>.Awaiter GetAwaiter<T>(this IEnumerable<UniTask<T>> tasks)
        {
            return UniTask.WhenAll(tasks).GetAwaiter();
        }

        public static UniTask<(T1 result1, T2 result2)>.Awaiter GetAwaiter<T1, T2>(this (UniTask<T1> task1, UniTask<T2> task2) tasks)
        {
            return UniTask.WhenAll(tasks.task1, tasks.task2).GetAwaiter();
        }

        public static UniTask<(T1 result1, T2 result2, T3 result3)> WhenAll<T1, T2, T3>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3) tasks)
        {
            return UniTask.WhenAll<T1, T2, T3>(tasks.task1, tasks.task2, tasks.task3);
        }

        public static UniTask<(T1 result1, T2 result2, T3 result3, T4 result4)> WhenAll<T1, T2, T3, T4>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4) tasks)
        {
            return UniTask.WhenAll<T1, T2, T3, T4>(tasks.task1, tasks.task2, tasks.task3, tasks.task4);
        }

        public static UniTask<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WhenAll<T1, T2, T3, T4, T5>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5) tasks)
        {
            return UniTask.WhenAll<T1, T2, T3, T4, T5>(tasks.task1, tasks.task2, tasks.task3, tasks.task4, tasks.task5);
        }

        public static UniTask<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WhenAll<T1, T2, T3, T4, T5, T6>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6) tasks)
        {
            return UniTask.WhenAll<T1, T2, T3, T4, T5, T6>(tasks.task1, tasks.task2, tasks.task3, tasks.task4, tasks.task5, tasks.task6);
        }

        public static UniTask<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> WhenAll<T1, T2, T3, T4, T5, T6, T7>(this (UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7) tasks)
        {
            return UniTask.WhenAll<T1, T2, T3, T4, T5, T6, T7>(tasks.task1, tasks.task2, tasks.task3, tasks.task4, tasks.task5, tasks.task6, tasks.task7);
        }
    }
}
#endif