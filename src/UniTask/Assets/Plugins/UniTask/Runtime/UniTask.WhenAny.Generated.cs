#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2)> WhenAny<T1, T2>(UniTask<T1> task1, UniTask<T2> task2)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2)>(new WhenAnyPromise<T1, T2>(task1, task2), 0);
        }

        sealed class WhenAnyPromise<T1, T2> : IUniTaskSource<(int, T1 result1, T2 result2)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result));
                }
            }


            public (int, T1 result1, T2 result2) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3)> WhenAny<T1, T2, T3>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3)>(new WhenAnyPromise<T1, T2, T3>(task1, task2, task3), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4)> WhenAny<T1, T2, T3, T4>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4)>(new WhenAnyPromise<T1, T2, T3, T4>(task1, task2, task3, task4), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WhenAny<T1, T2, T3, T4, T5>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)>(new WhenAnyPromise<T1, T2, T3, T4, T5>(task1, task2, task3, task4, task5), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WhenAny<T1, T2, T3, T4, T5, T6>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6>(task1, task2, task3, task4, task5, task6), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> WhenAny<T1, T2, T3, T4, T5, T6, T7>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>(task1, task2, task3, task4, task5, task6, task7), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>(task1, task2, task3, task4, task5, task6, task7, task8), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>(task1, task2, task3, task4, task5, task6, task7, task8, task9), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, UniTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> self, in UniTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, UniTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> self, in UniTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, UniTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> self, in UniTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task13.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT13(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, UniTask<T13>.Awaiter>)state)
                            {
                                TryInvokeContinuationT13(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT13(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> self, in UniTask<T13>.Awaiter awaiter)
            {
                T13 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((12, default, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task13.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT13(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T13>.Awaiter>)state)
                            {
                                TryInvokeContinuationT13(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task14.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT14(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, UniTask<T14>.Awaiter>)state)
                            {
                                TryInvokeContinuationT14(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT13(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T13>.Awaiter awaiter)
            {
                T13 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((12, default, default, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT14(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> self, in UniTask<T14>.Awaiter awaiter)
            {
                T14 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((13, default, default, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

        public static UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)> WhenAny<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
        {
            return new UniTask<(int winArgumentIndex, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)>(new WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15), 0);
        }

        sealed class WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IUniTaskSource<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)>
        {
            int completedCount;
            UniTaskCompletionSourceCore<(int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15)> core;

            public WhenAnyPromise(UniTask<T1> task1, UniTask<T2> task2, UniTask<T3> task3, UniTask<T4> task4, UniTask<T5> task5, UniTask<T6> task6, UniTask<T7> task7, UniTask<T8> task8, UniTask<T9> task9, UniTask<T10> task10, UniTask<T11> task11, UniTask<T12> task12, UniTask<T13> task13, UniTask<T14> task14, UniTask<T15> task15)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completedCount = 0;
                {
                    var awaiter = task1.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT1(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T1>.Awaiter>)state)
                            {
                                TryInvokeContinuationT1(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task2.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT2(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T2>.Awaiter>)state)
                            {
                                TryInvokeContinuationT2(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task3.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT3(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T3>.Awaiter>)state)
                            {
                                TryInvokeContinuationT3(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task4.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT4(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T4>.Awaiter>)state)
                            {
                                TryInvokeContinuationT4(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task5.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT5(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T5>.Awaiter>)state)
                            {
                                TryInvokeContinuationT5(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task6.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT6(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T6>.Awaiter>)state)
                            {
                                TryInvokeContinuationT6(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task7.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT7(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T7>.Awaiter>)state)
                            {
                                TryInvokeContinuationT7(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task8.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT8(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T8>.Awaiter>)state)
                            {
                                TryInvokeContinuationT8(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task9.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT9(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T9>.Awaiter>)state)
                            {
                                TryInvokeContinuationT9(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task10.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT10(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T10>.Awaiter>)state)
                            {
                                TryInvokeContinuationT10(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task11.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT11(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T11>.Awaiter>)state)
                            {
                                TryInvokeContinuationT11(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task12.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT12(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T12>.Awaiter>)state)
                            {
                                TryInvokeContinuationT12(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task13.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT13(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T13>.Awaiter>)state)
                            {
                                TryInvokeContinuationT13(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task14.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT14(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T14>.Awaiter>)state)
                            {
                                TryInvokeContinuationT14(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
                {
                    var awaiter = task15.GetAwaiter();

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuationT15(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, UniTask<T15>.Awaiter>)state)
                            {
                                TryInvokeContinuationT15(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuationT1(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T1>.Awaiter awaiter)
            {
                T1 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((0, result, default, default, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT2(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T2>.Awaiter awaiter)
            {
                T2 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((1, default, result, default, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT3(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T3>.Awaiter awaiter)
            {
                T3 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((2, default, default, result, default, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT4(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T4>.Awaiter awaiter)
            {
                T4 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((3, default, default, default, result, default, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT5(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T5>.Awaiter awaiter)
            {
                T5 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((4, default, default, default, default, result, default, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT6(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T6>.Awaiter awaiter)
            {
                T6 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((5, default, default, default, default, default, result, default, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT7(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T7>.Awaiter awaiter)
            {
                T7 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((6, default, default, default, default, default, default, result, default, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT8(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T8>.Awaiter awaiter)
            {
                T8 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((7, default, default, default, default, default, default, default, result, default, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT9(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T9>.Awaiter awaiter)
            {
                T9 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((8, default, default, default, default, default, default, default, default, result, default, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT10(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T10>.Awaiter awaiter)
            {
                T10 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((9, default, default, default, default, default, default, default, default, default, result, default, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT11(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T11>.Awaiter awaiter)
            {
                T11 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((10, default, default, default, default, default, default, default, default, default, default, result, default, default, default, default));
                }
            }

            static void TryInvokeContinuationT12(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T12>.Awaiter awaiter)
            {
                T12 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((11, default, default, default, default, default, default, default, default, default, default, default, result, default, default, default));
                }
            }

            static void TryInvokeContinuationT13(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T13>.Awaiter awaiter)
            {
                T13 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((12, default, default, default, default, default, default, default, default, default, default, default, default, result, default, default));
                }
            }

            static void TryInvokeContinuationT14(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T14>.Awaiter awaiter)
            {
                T14 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((13, default, default, default, default, default, default, default, default, default, default, default, default, default, result, default));
                }
            }

            static void TryInvokeContinuationT15(WhenAnyPromise<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> self, in UniTask<T15>.Awaiter awaiter)
            {
                T15 result;
                try
                {
                    result = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completedCount) == 1)
                {
                    self.core.TrySetResult((14, default, default, default, default, default, default, default, default, default, default, default, default, default, default, result));
                }
            }


            public (int, T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6, T7 result7, T8 result8, T9 result9, T10 result10, T11 result11, T12 result12, T13 result13, T14 result14, T15 result15) GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }
        }

    }
}