#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace UniRx.Async
{
    public partial struct UniTask
    {
        public static async UniTask<(T1 result1, T2 result2)> WhenAll<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
        {
            return await new WhenAllPromise<T1, T2>(task1, task2);
        }

        public static async UniTask<(T1 result1, T2 result2, T3 result3)> WhenAll<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
        {
            return await new WhenAllPromise<T1, T2, T3>(task1, task2, task3);
        }

        public static async UniTask<(T1 result1, T2 result2, T3 result3, T4 result4)> WhenAll<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
        {
            return await new WhenAllPromise<T1, T2, T3, T4>(task1, task2, task3, task4);
        }

        public static async UniTask<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WhenAll<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
        {
            return await new WhenAllPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5);
        }

        public static async UniTask<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WhenAll<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
        {
            return await new WhenAllPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6);
        }

        public static async UniTask<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> WhenAll<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
        {
            return await new WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7);
        }

        class WhenAllPromise<T1, T2>
        {
            const int MaxCount = 2;

            T1 result1;
            T2 result2;
            ExceptionDispatchInfo exception;
            int completeCount;
            Action whenComplete;

            public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2)
            {
                this.completeCount = 0;
                this.whenComplete = null;
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.exception = null;

                RunTask1(task1);
                RunTask2(task2);
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            void RunTask1(UniTask<T1> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result1 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask1Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask1Async(UniTask<T1> task)
            {
                try
                {
                    result1 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask2(UniTask<T2> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result2 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask2Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask2Async(UniTask<T2> task)
            {
                try
                {
                    result2 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAllPromise<T1, T2> parent;

                public Awaiter(WhenAllPromise<T1, T2> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.exception != null || parent.completeCount == MaxCount;
                    }
                }

                public (T1, T2) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    return (parent.result1, parent.result2);
                }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    parent.whenComplete = continuation;
                    if (IsCompleted)
                    {
                        var action = Interlocked.Exchange(ref parent.whenComplete, null);
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
        }

        class WhenAllPromise<T1, T2, T3>
        {
            const int MaxCount = 3;

            T1 result1;
            T2 result2;
            T3 result3;
            ExceptionDispatchInfo exception;
            int completeCount;
            Action whenComplete;

            public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
            {
                this.completeCount = 0;
                this.whenComplete = null;
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.exception = null;

                RunTask1(task1);
                RunTask2(task2);
                RunTask3(task3);
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            void RunTask1(UniTask<T1> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result1 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask1Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask1Async(UniTask<T1> task)
            {
                try
                {
                    result1 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask2(UniTask<T2> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result2 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask2Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask2Async(UniTask<T2> task)
            {
                try
                {
                    result2 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask3(UniTask<T3> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result3 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask3Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask3Async(UniTask<T3> task)
            {
                try
                {
                    result3 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAllPromise<T1, T2, T3> parent;

                public Awaiter(WhenAllPromise<T1, T2, T3> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.exception != null || parent.completeCount == MaxCount;
                    }
                }

                public (T1, T2, T3) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    return (parent.result1, parent.result2, parent.result3);
                }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    parent.whenComplete = continuation;
                    if (IsCompleted)
                    {
                        var action = Interlocked.Exchange(ref parent.whenComplete, null);
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
        }

        class WhenAllPromise<T1, T2, T3, T4>
        {
            const int MaxCount = 4;

            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            ExceptionDispatchInfo exception;
            int completeCount;
            Action whenComplete;

            public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
            {
                this.completeCount = 0;
                this.whenComplete = null;
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.exception = null;

                RunTask1(task1);
                RunTask2(task2);
                RunTask3(task3);
                RunTask4(task4);
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            void RunTask1(UniTask<T1> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result1 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask1Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask1Async(UniTask<T1> task)
            {
                try
                {
                    result1 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask2(UniTask<T2> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result2 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask2Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask2Async(UniTask<T2> task)
            {
                try
                {
                    result2 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask3(UniTask<T3> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result3 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask3Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask3Async(UniTask<T3> task)
            {
                try
                {
                    result3 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask4(UniTask<T4> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result4 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask4Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask4Async(UniTask<T4> task)
            {
                try
                {
                    result4 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAllPromise<T1, T2, T3, T4> parent;

                public Awaiter(WhenAllPromise<T1, T2, T3, T4> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.exception != null || parent.completeCount == MaxCount;
                    }
                }

                public (T1, T2, T3, T4) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    return (parent.result1, parent.result2, parent.result3, parent.result4);
                }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    parent.whenComplete = continuation;
                    if (IsCompleted)
                    {
                        var action = Interlocked.Exchange(ref parent.whenComplete, null);
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
        }

        class WhenAllPromise<T1, T2, T3, T4, T5>
        {
            const int MaxCount = 5;

            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            T5 result5;
            ExceptionDispatchInfo exception;
            int completeCount;
            Action whenComplete;

            public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
            {
                this.completeCount = 0;
                this.whenComplete = null;
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.result5 = default(T5);
                this.exception = null;

                RunTask1(task1);
                RunTask2(task2);
                RunTask3(task3);
                RunTask4(task4);
                RunTask5(task5);
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            void RunTask1(UniTask<T1> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result1 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask1Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask1Async(UniTask<T1> task)
            {
                try
                {
                    result1 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask2(UniTask<T2> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result2 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask2Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask2Async(UniTask<T2> task)
            {
                try
                {
                    result2 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask3(UniTask<T3> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result3 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask3Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask3Async(UniTask<T3> task)
            {
                try
                {
                    result3 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask4(UniTask<T4> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result4 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask4Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask4Async(UniTask<T4> task)
            {
                try
                {
                    result4 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask5(UniTask<T5> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result5 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask5Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask5Async(UniTask<T5> task)
            {
                try
                {
                    result5 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAllPromise<T1, T2, T3, T4, T5> parent;

                public Awaiter(WhenAllPromise<T1, T2, T3, T4, T5> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.exception != null || parent.completeCount == MaxCount;
                    }
                }

                public (T1, T2, T3, T4, T5) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    return (parent.result1, parent.result2, parent.result3, parent.result4, parent.result5);
                }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    parent.whenComplete = continuation;
                    if (IsCompleted)
                    {
                        var action = Interlocked.Exchange(ref parent.whenComplete, null);
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
        }

        class WhenAllPromise<T1, T2, T3, T4, T5, T6>
        {
            const int MaxCount = 6;

            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            T5 result5;
            T6 result6;
            ExceptionDispatchInfo exception;
            int completeCount;
            Action whenComplete;

            public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
            {
                this.completeCount = 0;
                this.whenComplete = null;
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.result5 = default(T5);
                this.result6 = default(T6);
                this.exception = null;

                RunTask1(task1);
                RunTask2(task2);
                RunTask3(task3);
                RunTask4(task4);
                RunTask5(task5);
                RunTask6(task6);
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            void RunTask1(UniTask<T1> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result1 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask1Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask1Async(UniTask<T1> task)
            {
                try
                {
                    result1 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask2(UniTask<T2> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result2 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask2Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask2Async(UniTask<T2> task)
            {
                try
                {
                    result2 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask3(UniTask<T3> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result3 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask3Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask3Async(UniTask<T3> task)
            {
                try
                {
                    result3 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask4(UniTask<T4> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result4 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask4Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask4Async(UniTask<T4> task)
            {
                try
                {
                    result4 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask5(UniTask<T5> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result5 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask5Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask5Async(UniTask<T5> task)
            {
                try
                {
                    result5 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask6(UniTask<T6> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result6 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask6Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask6Async(UniTask<T6> task)
            {
                try
                {
                    result6 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAllPromise<T1, T2, T3, T4, T5, T6> parent;

                public Awaiter(WhenAllPromise<T1, T2, T3, T4, T5, T6> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.exception != null || parent.completeCount == MaxCount;
                    }
                }

                public (T1, T2, T3, T4, T5, T6) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    return (parent.result1, parent.result2, parent.result3, parent.result4, parent.result5, parent.result6);
                }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    parent.whenComplete = continuation;
                    if (IsCompleted)
                    {
                        var action = Interlocked.Exchange(ref parent.whenComplete, null);
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
        }

        class WhenAllPromise<T1, T2, T3, T4, T5, T6, T7>
        {
            const int MaxCount = 7;

            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            T5 result5;
            T6 result6;
            T7 result7;
            ExceptionDispatchInfo exception;
            int completeCount;
            Action whenComplete;

            public WhenAllPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
            {
                this.completeCount = 0;
                this.whenComplete = null;
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.result5 = default(T5);
                this.result6 = default(T6);
                this.result7 = default(T7);
                this.exception = null;

                RunTask1(task1);
                RunTask2(task2);
                RunTask3(task3);
                RunTask4(task4);
                RunTask5(task5);
                RunTask6(task6);
                RunTask7(task7);
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            void RunTask1(UniTask<T1> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result1 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask1Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask1Async(UniTask<T1> task)
            {
                try
                {
                    result1 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask2(UniTask<T2> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result2 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask2Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask2Async(UniTask<T2> task)
            {
                try
                {
                    result2 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask3(UniTask<T3> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result3 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask3Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask3Async(UniTask<T3> task)
            {
                try
                {
                    result3 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask4(UniTask<T4> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result4 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask4Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask4Async(UniTask<T4> task)
            {
                try
                {
                    result4 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask5(UniTask<T5> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result5 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask5Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask5Async(UniTask<T5> task)
            {
                try
                {
                    result5 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask6(UniTask<T6> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result6 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask6Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask6Async(UniTask<T6> task)
            {
                try
                {
                    result6 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }

            void RunTask7(UniTask<T7> task)
            {
                if (task.IsCompleted)
                {
                    try
                    {
                        result7 = task.Result;
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryCallContinuation();
                        return;
                    }

                    var count = Interlocked.Increment(ref completeCount);
                    if (count == MaxCount)
                    {
                        TryCallContinuation();
                    }
                }
                else
                {
                    RunTask7Async(task).Forget();
                }
            }

            async UniTaskVoid RunTask7Async(UniTask<T7> task)
            {
                try
                {
                    result7 = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == MaxCount)
                {
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> parent;

                public Awaiter(WhenAllPromise<T1, T2, T3, T4, T5, T6, T7> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.exception != null || parent.completeCount == MaxCount;
                    }
                }

                public (T1, T2, T3, T4, T5, T6, T7) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    return (parent.result1, parent.result2, parent.result3, parent.result4, parent.result5, parent.result6, parent.result7);
                }

                public void OnCompleted(Action continuation)
                {
                    UnsafeOnCompleted(continuation);
                }

                public void UnsafeOnCompleted(Action continuation)
                {
                    parent.whenComplete = continuation;
                    if (IsCompleted)
                    {
                        var action = Interlocked.Exchange(ref parent.whenComplete, null);
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
        }

    }
}
#endif