#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        public static UniTask<T[]> WhenAll<T>(params UniTask<T>[] tasks)
        {
            if (tasks.Length == 0)
            {
                return UniTask.FromResult(Array.Empty<T>());
            }

            return new UniTask<T[]>(new WhenAllPromise<T>(tasks, tasks.Length), 0);
        }

        public static UniTask<T[]> WhenAll<T>(IEnumerable<UniTask<T>> tasks)
        {
            using (var span = ArrayPoolUtil.Materialize(tasks))
            {
                var promise = new WhenAllPromise<T>(span.Array, span.Length); // consumed array in constructor.
                return new UniTask<T[]>(promise, 0);
            }
        }

        public static UniTask WhenAll(params UniTask[] tasks)
        {
            if (tasks.Length == 0)
            {
                return UniTask.CompletedTask;
            }

            return new UniTask(new WhenAllPromise(tasks, tasks.Length), 0);
        }

        public static UniTask WhenAll(IEnumerable<UniTask> tasks)
        {
            using (var span = ArrayPoolUtil.Materialize(tasks))
            {
                var promise = new WhenAllPromise(span.Array, span.Length); // consumed array in constructor.
                return new UniTask(promise, 0);
            }
        }

        sealed class WhenAllPromise<T> : IUniTaskSource<T[]>
        {
            T[] result;
            int completeCount;
            UniTaskCompletionSourceCore<T[]> core; // don't reset(called after GetResult, will invoke TrySetException.)

            public WhenAllPromise(UniTask<T>[] tasks, int tasksLength)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.completeCount = 0;

                if (tasksLength == 0)
                {
                    this.result = Array.Empty<T>();
                    core.TrySetResult(result);
                    return;
                }

                this.result = new T[tasksLength];

                for (int i = 0; i < tasksLength; i++)
                {
                    UniTask<T>.Awaiter awaiter;
                    try
                    {
                        awaiter = tasks[i].GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        core.TrySetException(ex);
                        continue;
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuation(this, awaiter, i);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAllPromise<T>, UniTask<T>.Awaiter, int>)state)
                            {
                                TryInvokeContinuation(t.Item1, t.Item2, t.Item3);
                            }
                        }, StateTuple.Create(this, awaiter, i));
                    }
                }
            }

            static void TryInvokeContinuation(WhenAllPromise<T> self, in UniTask<T>.Awaiter awaiter, int i)
            {
                try
                {
                    self.result[i] = awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completeCount) == self.result.Length)
                {
                    self.core.TrySetResult(self.result);
                }
            }

            public T[] GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                return core.GetResult(token);
            }

            void IUniTaskSource.GetResult(short token)
            {
                GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }
        }

        sealed class WhenAllPromise : IUniTaskSource
        {
            int completeCount;
            int tasksLength;
            UniTaskCompletionSourceCore<AsyncUnit> core; // don't reset(called after GetResult, will invoke TrySetException.)

            public WhenAllPromise(UniTask[] tasks, int tasksLength)
            {
                TaskTracker.TrackActiveTask(this, 3);

                this.tasksLength = tasksLength;
                this.completeCount = 0;

                if (tasksLength == 0)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return;
                }

                for (int i = 0; i < tasksLength; i++)
                {
                    UniTask.Awaiter awaiter;
                    try
                    {
                        awaiter = tasks[i].GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        core.TrySetException(ex);
                        continue;
                    }

                    if (awaiter.IsCompleted)
                    {
                        TryInvokeContinuation(this, awaiter);
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(state =>
                        {
                            using (var t = (StateTuple<WhenAllPromise, UniTask.Awaiter>)state)
                            {
                                TryInvokeContinuation(t.Item1, t.Item2);
                            }
                        }, StateTuple.Create(this, awaiter));
                    }
                }
            }

            static void TryInvokeContinuation(WhenAllPromise self, in UniTask.Awaiter awaiter)
            {
                try
                {
                    awaiter.GetResult();
                }
                catch (Exception ex)
                {
                    self.core.TrySetException(ex);
                    return;
                }

                if (Interlocked.Increment(ref self.completeCount) == self.tasksLength)
                {
                    self.core.TrySetResult(AsyncUnit.Default);
                }
            }

            public void GetResult(short token)
            {
                TaskTracker.RemoveTracking(this);
                GC.SuppressFinalize(this);
                core.GetResult(token);
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }
        }
    }
}
