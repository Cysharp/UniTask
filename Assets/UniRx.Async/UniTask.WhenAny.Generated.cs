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
        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1))> WhenAny<T0, T1>(UniTask<T0> task0, UniTask<T1> task1)
        {
            return await new WhenAnyPromise<T0, T1>(task0, task1);
        }

        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2))> WhenAny<T0, T1, T2>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2)
        {
            return await new WhenAnyPromise<T0, T1, T2>(task0, task1, task2);
        }

        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3))> WhenAny<T0, T1, T2, T3>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
        {
            return await new WhenAnyPromise<T0, T1, T2, T3>(task0, task1, task2, task3);
        }

        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4))> WhenAny<T0, T1, T2, T3, T4>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
        {
            return await new WhenAnyPromise<T0, T1, T2, T3, T4>(task0, task1, task2, task3, task4);
        }

        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4), (bool hasResult, T5 result5))> WhenAny<T0, T1, T2, T3, T4, T5>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
        {
            return await new WhenAnyPromise<T0, T1, T2, T3, T4, T5>(task0, task1, task2, task3, task4, task5);
        }

        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4), (bool hasResult, T5 result5), (bool hasResult, T6 result6))> WhenAny<T0, T1, T2, T3, T4, T5, T6>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
        {
            return await new WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6>(task0, task1, task2, task3, task4, task5, task6);
        }

        public static async UniTask<(int winArgumentIndex, (bool hasResult, T0 result0), (bool hasResult, T1 result1), (bool hasResult, T2 result2), (bool hasResult, T3 result3), (bool hasResult, T4 result4), (bool hasResult, T5 result5), (bool hasResult, T6 result6), (bool hasResult, T7 result7))> WhenAny<T0, T1, T2, T3, T4, T5, T6, T7>(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
        {
            return await new WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7>(task0, task1, task2, task3, task4, task5, task6, task7);
        }

        class WhenAnyPromise<T0, T1>
        {
            T0 result0;
            T1 result1;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1> parent;

                public Awaiter(WhenAnyPromise<T0, T1> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1));
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

        class WhenAnyPromise<T0, T1, T2>
        {
            T0 result0;
            T1 result1;
            T2 result2;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);
                this.result2 = default(T2);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
                RunTask2(task2).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask2(UniTask<T2> task)
            {
                T2 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result2 = value;
                    Volatile.Write(ref winArgumentIndex, 2);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1, T2> parent;

                public Awaiter(WhenAnyPromise<T0, T1, T2> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1), (bool, T2)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1), (i == 2, parent.result2));
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

        class WhenAnyPromise<T0, T1, T2, T3>
        {
            T0 result0;
            T1 result1;
            T2 result2;
            T3 result3;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
                RunTask2(task2).Forget();
                RunTask3(task3).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask2(UniTask<T2> task)
            {
                T2 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result2 = value;
                    Volatile.Write(ref winArgumentIndex, 2);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask3(UniTask<T3> task)
            {
                T3 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result3 = value;
                    Volatile.Write(ref winArgumentIndex, 3);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1, T2, T3> parent;

                public Awaiter(WhenAnyPromise<T0, T1, T2, T3> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1), (i == 2, parent.result2), (i == 3, parent.result3));
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

        class WhenAnyPromise<T0, T1, T2, T3, T4>
        {
            T0 result0;
            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
                RunTask2(task2).Forget();
                RunTask3(task3).Forget();
                RunTask4(task4).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask2(UniTask<T2> task)
            {
                T2 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result2 = value;
                    Volatile.Write(ref winArgumentIndex, 2);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask3(UniTask<T3> task)
            {
                T3 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result3 = value;
                    Volatile.Write(ref winArgumentIndex, 3);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask4(UniTask<T4> task)
            {
                T4 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result4 = value;
                    Volatile.Write(ref winArgumentIndex, 4);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1, T2, T3, T4> parent;

                public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1), (i == 2, parent.result2), (i == 3, parent.result3), (i == 4, parent.result4));
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

        class WhenAnyPromise<T0, T1, T2, T3, T4, T5>
        {
            T0 result0;
            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            T5 result5;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.result5 = default(T5);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
                RunTask2(task2).Forget();
                RunTask3(task3).Forget();
                RunTask4(task4).Forget();
                RunTask5(task5).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask2(UniTask<T2> task)
            {
                T2 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result2 = value;
                    Volatile.Write(ref winArgumentIndex, 2);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask3(UniTask<T3> task)
            {
                T3 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result3 = value;
                    Volatile.Write(ref winArgumentIndex, 3);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask4(UniTask<T4> task)
            {
                T4 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result4 = value;
                    Volatile.Write(ref winArgumentIndex, 4);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask5(UniTask<T5> task)
            {
                T5 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result5 = value;
                    Volatile.Write(ref winArgumentIndex, 5);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1, T2, T3, T4, T5> parent;

                public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4, T5> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1), (i == 2, parent.result2), (i == 3, parent.result3), (i == 4, parent.result4), (i == 5, parent.result5));
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

        class WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6>
        {
            T0 result0;
            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            T5 result5;
            T6 result6;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.result5 = default(T5);
                this.result6 = default(T6);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
                RunTask2(task2).Forget();
                RunTask3(task3).Forget();
                RunTask4(task4).Forget();
                RunTask5(task5).Forget();
                RunTask6(task6).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask2(UniTask<T2> task)
            {
                T2 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result2 = value;
                    Volatile.Write(ref winArgumentIndex, 2);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask3(UniTask<T3> task)
            {
                T3 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result3 = value;
                    Volatile.Write(ref winArgumentIndex, 3);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask4(UniTask<T4> task)
            {
                T4 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result4 = value;
                    Volatile.Write(ref winArgumentIndex, 4);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask5(UniTask<T5> task)
            {
                T5 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result5 = value;
                    Volatile.Write(ref winArgumentIndex, 5);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask6(UniTask<T6> task)
            {
                T6 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result6 = value;
                    Volatile.Write(ref winArgumentIndex, 6);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> parent;

                public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1), (i == 2, parent.result2), (i == 3, parent.result3), (i == 4, parent.result4), (i == 5, parent.result5), (i == 6, parent.result6));
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

        class WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7>
        {
            T0 result0;
            T1 result1;
            T2 result2;
            T3 result3;
            T4 result4;
            T5 result5;
            T6 result6;
            T7 result7;
            ExceptionDispatchInfo exception;
            Action whenComplete;
            int completeCount;
            int winArgumentIndex;

            bool IsCompleted => exception != null || Volatile.Read(ref winArgumentIndex) != -1;

            public WhenAnyPromise(UniTask<T0> task0, UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
            {
                this.whenComplete = null;
                this.exception = null;
                this.completeCount = 0;
                this.winArgumentIndex = -1;
                this.result0 = default(T0);
                this.result1 = default(T1);
                this.result2 = default(T2);
                this.result3 = default(T3);
                this.result4 = default(T4);
                this.result5 = default(T5);
                this.result6 = default(T6);
                this.result7 = default(T7);

                RunTask0(task0).Forget();
                RunTask1(task1).Forget();
                RunTask2(task2).Forget();
                RunTask3(task3).Forget();
                RunTask4(task4).Forget();
                RunTask5(task5).Forget();
                RunTask6(task6).Forget();
                RunTask7(task7).Forget();
            }

            void TryCallContinuation()
            {
                var action = Interlocked.Exchange(ref whenComplete, null);
                if (action != null)
                {
                    action.Invoke();
                }
            }

            async UniTaskVoid RunTask0(UniTask<T0> task)
            {
                T0 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result0 = value;
                    Volatile.Write(ref winArgumentIndex, 0);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask1(UniTask<T1> task)
            {
                T1 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result1 = value;
                    Volatile.Write(ref winArgumentIndex, 1);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask2(UniTask<T2> task)
            {
                T2 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result2 = value;
                    Volatile.Write(ref winArgumentIndex, 2);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask3(UniTask<T3> task)
            {
                T3 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result3 = value;
                    Volatile.Write(ref winArgumentIndex, 3);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask4(UniTask<T4> task)
            {
                T4 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result4 = value;
                    Volatile.Write(ref winArgumentIndex, 4);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask5(UniTask<T5> task)
            {
                T5 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result5 = value;
                    Volatile.Write(ref winArgumentIndex, 5);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask6(UniTask<T6> task)
            {
                T6 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result6 = value;
                    Volatile.Write(ref winArgumentIndex, 6);
                    TryCallContinuation();
                }
            }

            async UniTaskVoid RunTask7(UniTask<T7> task)
            {
                T7 value;
                try
                {
                    value = await task;
                }
                catch (Exception ex)
                {
                    exception = ExceptionDispatchInfo.Capture(ex);
                    TryCallContinuation();
                    return;
                }

                var count = Interlocked.Increment(ref completeCount);
                if (count == 1)
                {
                    result7 = value;
                    Volatile.Write(ref winArgumentIndex, 7);
                    TryCallContinuation();
                }
            }


            public Awaiter GetAwaiter()
            {
                return new Awaiter(this);
            }

            public struct Awaiter : ICriticalNotifyCompletion
            {
                WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> parent;

                public Awaiter(WhenAnyPromise<T0, T1, T2, T3, T4, T5, T6, T7> parent)
                {
                    this.parent = parent;
                }

                public bool IsCompleted
                {
                    get
                    {
                        return parent.IsCompleted;
                    }
                }

                public (int, (bool, T0), (bool, T1), (bool, T2), (bool, T3), (bool, T4), (bool, T5), (bool, T6), (bool, T7)) GetResult()
                {
                    if (parent.exception != null)
                    {
                        parent.exception.Throw();
                    }

                    var i = parent.winArgumentIndex;
                    return (i, (i == 0, parent.result0), (i == 1, parent.result1), (i == 2, parent.result2), (i == 3, parent.result3), (i == 4, parent.result4), (i == 5, parent.result5), (i == 6, parent.result6), (i == 7, parent.result7));
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