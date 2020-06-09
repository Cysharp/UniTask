using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Cysharp.Threading.TasksTests
{
    public class DelayTest
    {
        //[UnityTest]
        //public IEnumerator DelayFrame() => UniTask.ToCoroutine(async () =>
        //{
        //    for (int i = 1; i < 5; i++)
        //    {
        //        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        //        var frameCount = Time.frameCount;
        //        await UniTask.DelayFrame(i);
        //        Time.frameCount.Should().Be(frameCount + i);
        //    }

        //    for (int i = 1; i < 5; i++)
        //    {
        //        await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        //        var frameCount = Time.frameCount;
        //        await UniTask.DelayFrame(i);
        //        Time.frameCount.Should().Be(frameCount + i);
        //    }
        //});

        //[UnityTest]
        //public IEnumerator DelayFrameZero() => UniTask.ToCoroutine(async () =>
        //{
        //    {
        //        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        //        var frameCount = Time.frameCount;
        //        await UniTask.DelayFrame(0);
        //        Time.frameCount.Should().Be(frameCount); // same frame
        //    }
        //    {
        //        await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        //        var frameCount = Time.frameCount;
        //        await UniTask.DelayFrame(0);
        //        Time.frameCount.Should().Be(frameCount + 1); // next frame
        //    }
        //});



        //[UnityTest]
        //public IEnumerator TimerFramePre() => UniTask.ToCoroutine(async () =>
        //{
        //    await UniTask.Yield(PlayerLoopTiming.PreUpdate);

        //    var initialFrame = Time.frameCount;
        //    var xs = await UniTaskAsyncEnumerable.TimerFrame(2, 3).Take(5).Select(_ => Time.frameCount).ToArrayAsync();

        //    xs[0].Should().Be(initialFrame + 2);
        //    xs[1].Should().Be(initialFrame + 2 + (3 * 1));
        //    xs[2].Should().Be(initialFrame + 2 + (3 * 2));
        //    xs[3].Should().Be(initialFrame + 2 + (3 * 3));
        //    xs[4].Should().Be(initialFrame + 2 + (3 * 4));
        //});


        //[UnityTest]
        //public IEnumerator TimerFramePost() => UniTask.ToCoroutine(async () =>
        //{
        //    await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

        //    var initialFrame = Time.frameCount;
        //    var xs = await UniTaskAsyncEnumerable.TimerFrame(2, 3).Take(5).Select(_ => Time.frameCount).ToArrayAsync();

        //    xs[0].Should().Be(initialFrame + 2);
        //    xs[1].Should().Be(initialFrame + 2 + (3 * 1));
        //    xs[2].Should().Be(initialFrame + 2 + (3 * 2));
        //    xs[3].Should().Be(initialFrame + 2 + (3 * 3));
        //    xs[4].Should().Be(initialFrame + 2 + (3 * 4));
        //});


        //[UnityTest]
        //public IEnumerator TimerFrameTest() => UniTask.ToCoroutine(async () =>
        //{
        //    await UniTask.Yield(PlayerLoopTiming.PreUpdate);

        //    var initialFrame = Time.frameCount;
        //    var xs = await UniTaskAsyncEnumerable.TimerFrame(0, 0).Take(5).Select(_ => Time.frameCount).ToArrayAsync();

        //    xs[0].Should().Be(initialFrame);
        //    xs[1].Should().Be(initialFrame + 1);
        //    xs[2].Should().Be(initialFrame + 2);
        //    xs[3].Should().Be(initialFrame + 3);
        //    xs[4].Should().Be(initialFrame + 4);
        //});

        [UnityTest]
        public IEnumerator TimerFrameSinglePre2() => UniTask.ToCoroutine(async () =>
        {
            {
                var xs = await UniTaskAsyncEnumerable.TimerFrame(1).ToArrayAsync();
            }
            //Debug.Log("------------------");
            //{
            //    var xs = await UniTaskAsyncEnumerable.TimerFrame(1).ToArrayAsync();
            //}
        });


        //[UnityTest]
        //public IEnumerator TimerFrameSinglePre2() => UniTask.ToCoroutine(async () =>
        //{
        //    {
        //        var initialFrame = Time.frameCount;
        //        var xs = await new MyTimerFrame(0, null)/*.Select(_ => Time.frameCount)*/.ToArrayAsync();
        //        Debug.Log("OK 0 ------------------");
        //    }
        //    {
        //        var xs = await new MyTimerFrame(1, null)/*.Select(_ =>
        //        {
        //            var t = Time.frameCount;
        //            UnityEngine.Debug.Log("store frameCount:" + t);
        //            return t;
        //        })*/.ToArrayAsync();
        //    }
        //});

        //[UnityTest]
        //public IEnumerator TimerFrameSinglePre() => UniTask.ToCoroutine(async () =>
        //{
        //    {
        //        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        //        var initialFrame = Time.frameCount;
        //        var xs = await UniTaskAsyncEnumerable.Return(UniTask.Yield(PlayerLoopTiming.Update, CancellationToken.None))/*.Select(_ => Time.frameCount)*/.ToArrayAsync();
        //        xs[0].Should().Be(initialFrame);
        //        Debug.Log("OK 0 ------------------");
        //    }
        //    {
        //        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        //        var initialFrame = Time.frameCount;
        //        Debug.Log("initialFrame:" + initialFrame);
        //        var xs = await UniTaskAsyncEnumerable.Return(UniTask.Yield(PlayerLoopTiming.Update, CancellationToken.None))/*.Select(_ =>
        //        {
        //            var t = Time.frameCount;
        //            UnityEngine.Debug.Log("store frameCount:" + t);
        //            return t;
        //        })*/.ToArrayAsync();
        //        Debug.Log("xs len:" + xs.Length);
        //        Debug.Log("xs[0]:" + xs[0]);

        //        xs[0].Should().Be(initialFrame + 1);
        //        Debug.Log("OK 1");
        //    }
        //    {
        //        //await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        //        var initialFrame = Time.frameCount;
        //        var xs = await UniTaskAsyncEnumerable.TimerFrame(2).Select(_ => Time.frameCount).ToArrayAsync();
        //        xs[0].Should().Be(initialFrame + 2);
        //        Debug.Log("OK 2");
        //    }
        //});


        //[UnityTest]
        //public IEnumerator TimerFrameSinglePost() => UniTask.ToCoroutine(async () =>
        //{
        //    {
        //        //await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        //        //var initialFrame = Time.frameCount;
        //        //var xs = await UniTaskAsyncEnumerable.TimerFrame(0).Select(_ => Time.frameCount).ToArrayAsync();
        //        //xs[0].Should().Be(initialFrame);
        //    }
        //    {
        //        //await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        //        var initialFrame = Time.frameCount;
        //        var xs = await UniTaskAsyncEnumerable.TimerFrame(1).Select(_ => Time.frameCount).ToArrayAsync();
        //        xs[0].Should().Be(initialFrame + 1);
        //    }
        //    {
        //        //await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        //        var initialFrame = Time.frameCount;
        //        var xs = await UniTaskAsyncEnumerable.TimerFrame(2).Select(_ => Time.frameCount).ToArrayAsync();
        //        xs[0].Should().Be(initialFrame + 2);
        //    }
        //});



        //[UnityTest]
        //public IEnumerator Timer() => UniTask.ToCoroutine(async () =>
        //{
        //    await UniTask.Yield(PlayerLoopTiming.PreUpdate);

        //    {
        //        var initialSeconds = Time.realtimeSinceStartup;
        //        var xs = await UniTaskAsyncEnumerable.Timer(TimeSpan.FromSeconds(2)).Select(_ => Time.realtimeSinceStartup).ToArrayAsync();

        //        Mathf.Approximately(initialSeconds, xs[0]).Should().BeFalse();
        //        Debug.Log("Init:" + initialSeconds);
        //        Debug.Log("After:" + xs[0]);
        //    }
        //});




    }

    public class DelayTest2
    {
        [UnityTest]
        public IEnumerator TimerFrameSinglePre2() => UniTask.ToCoroutine(async () =>
        {
            {
                var xs = await UniTaskAsyncEnumerable.TimerFrame(1).ToArrayAsync();
            }
            Debug.Log("------------------");
            {
                var xs = await UniTaskAsyncEnumerable.TimerFrame(1).ToArrayAsync();
            }
        });
    }


    public class ThreadRunner
    {
        Thread thread;

        public void Start(IPlayerLoopItem runner)
        {
            thread = new Thread(() =>
            {
                Thread.Sleep(30);
                while (runner.MoveNext())
                {
                    Thread.Sleep(30);
                }
            });

            thread.Start();
        }
    }

    internal class MyTimerFrame : IUniTaskAsyncEnumerable<AsyncUnit>
    {
        //readonly PlayerLoopTiming updateTiming;
        readonly int dueTimeFrameCount;
        readonly int? periodFrameCount;

        public MyTimerFrame(int dueTimeFrameCount, int? periodFrameCount)
        {
            //this.updateTiming = updateTiming;
            this.dueTimeFrameCount = dueTimeFrameCount;
            this.periodFrameCount = periodFrameCount;
        }

        public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TimerFrame(dueTimeFrameCount, periodFrameCount, cancellationToken);
        }

        class _TimerFrame : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            readonly int dueTimeFrameCount;
            readonly int? periodFrameCount;
            CancellationToken cancellationToken;

            int initialFrame;
            int currentFrame;
            bool dueTimePhase;
            bool completed;
            bool disposed;
            ThreadRunner runner;

            public _TimerFrame(int dueTimeFrameCount, int? periodFrameCount, CancellationToken cancellationToken)
            {
                if (dueTimeFrameCount <= 0) dueTimeFrameCount = 0;
                if (periodFrameCount != null)
                {
                    if (periodFrameCount <= 0) periodFrameCount = 1;
                }

                //this.initialFrame = Time.frameCount;
                this.dueTimePhase = true;
                this.dueTimeFrameCount = dueTimeFrameCount;
                this.periodFrameCount = periodFrameCount;

                //TaskTracker.TrackActiveTask(this, 2);
                //PlayerLoopHelper.AddAction(updateTiming, this);

                runner = new ThreadRunner();
                runner.Start(this);
            }

            public AsyncUnit Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested || completed) return default;


                // reset value here.
                this.currentFrame = 0;

                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }
                return default;
            }

            public bool MoveNext()
            {
                UnityEngine.Debug.Log("Called MoveNext");
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    UnityEngine.Debug.Log("Disposing");
                    completionSource.TrySetResult(false);
                    return false;
                }

                if (dueTimePhase)
                {
                    UnityEngine.Debug.Log("In DueTime Phase");
                    if (currentFrame == 0)
                    {
                        if (dueTimeFrameCount == 0)
                        {
                            dueTimePhase = false;
                            completionSource.TrySetResult(true);
                            return true;
                        }

                        // skip in initial frame.
                        /*
                        UnityEngine.Debug.Log("(Init, frameConut)" + (initialFrame, Time.frameCount));
                        if (initialFrame == Time.frameCount)
                        {
                            UnityEngine.Debug.Log("Skip Here");
                            return true;
                        }
                        */
                    }

                    UnityEngine.Debug.Log("Which Go?");
                    if (++currentFrame >= dueTimeFrameCount)
                    {
                        UnityEngine.Debug.Log("END Go?");
                        dueTimePhase = false;
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("NG Go?");
                    }
                }
                else
                {
                    if (periodFrameCount == null)
                    {
                        UnityEngine.Debug.Log("PERIOD");
                        completed = true;
                        completionSource.TrySetResult(false);
                        return false;
                    }

                    if (++currentFrame >= periodFrameCount)
                    {
                        completionSource.TrySetResult(true);
                    }
                }

                return true;
            }
        }
    }

}
