#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;

namespace UniRx.Async
{
    public static class EnumerableAsyncExtensions
    {
        // overload resolver - .Select(async x => { }) : IEnumerable<UniTask<T>>

        public static IEnumerable<UniTask> Select<T>(this IEnumerable<T> source, Func<T, UniTask> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<UniTask<TR>> Select<T, TR>(this IEnumerable<T> source, Func<T, UniTask<TR>> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<UniTask> Select<T>(this IEnumerable<T> source, Func<T, int, UniTask> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<UniTask<TR>> Select<T, TR>(this IEnumerable<T> source, Func<T, int, UniTask<TR>> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }
    }
}


#endif