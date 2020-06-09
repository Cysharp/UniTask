//#if !(UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine.UI;
//using UnityEngine.Scripting;
//using Cysharp.Threading.Tasks;
//using UnityEngine.SceneManagement;
//#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
//using System.Threading.Tasks;
//#endif
//using UnityEngine.Networking;

//#if !UNITY_2019_3_OR_NEWER
//using UnityEngine.Experimental.LowLevel;
//#else
//using UnityEngine.LowLevel;
//#endif

//#if !UNITY_WSA
//using Unity.Jobs;
//#endif
//using Unity.Collections;
//using System.Threading;
//using NUnit.Framework;
//using UnityEngine.TestTools;
//using FluentAssertions;

//namespace Cysharp.Threading.TasksTests
//{
//    public class AsyncTest
//    {
//#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
//#if !UNITY_WSA

//        public struct MyJob : IJob
//        {
//            public int loopCount;
//            public NativeArray<int> inOut;
//            public int result;

//            public void Execute()
//            {
//                result = 0;
//                for (int i = 0; i < loopCount; i++)
//                {
//                    result++;
//                }
//                inOut[0] = result;
//            }
//        }

//        [UnityTest]
//        public IEnumerator DelayAnd() => UniTask.ToCoroutine(async () =>
//        {
//            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

//            var time = Time.realtimeSinceStartup;

//            Time.timeScale = 0.5f;
//            try
//            {
//                await UniTask.Delay(TimeSpan.FromSeconds(3));

//                var elapsed = Time.realtimeSinceStartup - time;
//                ((int)Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven)).Should().Be(6);
//            }
//            finally
//            {
//                Time.timeScale = 1.0f;
//            }
//        });

//        [UnityTest]
//        public IEnumerator DelayIgnore() => UniTask.ToCoroutine(async () =>
//        {
//            var time = Time.realtimeSinceStartup;

//            Time.timeScale = 0.5f;
//            try
//            {
//                await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: true);

//                var elapsed = Time.realtimeSinceStartup - time;
//                ((int)Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven)).Should().Be(3);
//            }
//            finally
//            {
//                Time.timeScale = 1.0f;
//            }
//        });

//        [UnityTest]
//        public IEnumerator WhenAll() => UniTask.ToCoroutine(async () =>
//        {
//            var a = UniTask.FromResult(999);
//            var b = UniTask.Yield(PlayerLoopTiming.Update, CancellationToken.None).AsAsyncUnitUniTask();
//            var c = UniTask.DelayFrame(99).AsAsyncUnitUniTask();

//            var (a2, b2, c2) = await UniTask.WhenAll(a, b, c);
//            a2.Should().Be(999);
//            b2.Should().Be(AsyncUnit.Default);
//            c2.Should().Be(AsyncUnit.Default);
//        });

//        [UnityTest]
//        public IEnumerator WhenAny() => UniTask.ToCoroutine(async () =>
//        {
//            var a = UniTask.FromResult(999);
//            var b = UniTask.Yield(PlayerLoopTiming.Update, CancellationToken.None).AsAsyncUnitUniTask();
//            var c = UniTask.DelayFrame(99).AsAsyncUnitUniTask();

//            var (win, a2, b2, c2) = await UniTask.WhenAny(a, b, c);
//            win.Should().Be(0);
//            a2.Should().Be(999);
//        });

//        [UnityTest]
//        public IEnumerator BothEnumeratorCheck() => UniTask.ToCoroutine(async () =>
//        {
//            await ToaruCoroutineEnumerator(); // wait 5 frame:)
//        });

//        [UnityTest]
//        public IEnumerator JobSystem() => UniTask.ToCoroutine(async () =>
//        {
//            var job = new MyJob() { loopCount = 999, inOut = new NativeArray<int>(1, Allocator.TempJob) };
//            JobHandle.ScheduleBatchedJobs();
//            await job.Schedule();
//            job.inOut[0].Should().Be(999);
//            job.inOut.Dispose();
//        });

//        class MyMyClass
//        {
//            public int MyProperty { get; set; }
//        }

//        [UnityTest]
//        public IEnumerator WaitUntil() => UniTask.ToCoroutine(async () =>
//        {
//            bool t = false;

//            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

//            UniTask.DelayFrame(10,PlayerLoopTiming.PostLateUpdate).ContinueWith(() => t = true).Forget();

//            var startFrame = Time.frameCount;
//            await UniTask.WaitUntil(() => t, PlayerLoopTiming.EarlyUpdate);

//            var diff = Time.frameCount - startFrame;
//            diff.Should().Be(11);
//        });

//        [UnityTest]
//        public IEnumerator WaitWhile() => UniTask.ToCoroutine(async () =>
//        {
//            bool t = true;

//            UniTask.DelayFrame(10, PlayerLoopTiming.PostLateUpdate).ContinueWith(() => t = false).Forget();

//            var startFrame = Time.frameCount;
//            await UniTask.WaitWhile(() => t, PlayerLoopTiming.EarlyUpdate);

//            var diff = Time.frameCount - startFrame;
//            diff.Should().Be(11);
//        });

//        [UnityTest]
//        public IEnumerator WaitUntilValueChanged() => UniTask.ToCoroutine(async () =>
//        {
//            var v = new MyMyClass { MyProperty = 99 };

//            UniTask.DelayFrame(10, PlayerLoopTiming.PostLateUpdate).ContinueWith(() => v.MyProperty = 1000).Forget();

//            var startFrame = Time.frameCount;
//            await UniTask.WaitUntilValueChanged(v, x => x.MyProperty, PlayerLoopTiming.EarlyUpdate);

//            var diff = Time.frameCount - startFrame;
//            diff.Should().Be(11);
//        });

//        [UnityTest]
//        public IEnumerator SwitchTo() => UniTask.ToCoroutine(async () =>
//        {
//            await UniTask.Yield();

//            var currentThreadId = Thread.CurrentThread.ManagedThreadId;



//            await UniTask.SwitchToThreadPool();
//            //await UniTask.SwitchToThreadPool();
//            //await UniTask.SwitchToThreadPool();


            



//            var switchedThreadId = Thread.CurrentThread.ManagedThreadId;



//            currentThreadId.Should().NotBe(switchedThreadId);


//            await UniTask.Yield();

//            var switchedThreadId2 = Thread.CurrentThread.ManagedThreadId;

//            currentThreadId.Should().Be(switchedThreadId2);
//        });

//        //[UnityTest]
//        //public IEnumerator ObservableConversion() => UniTask.ToCoroutine(async () =>
//        //{
//        //    var v = await Observable.Range(1, 10).ToUniTask();
//        //    v.Is(10);

//        //    v = await Observable.Range(1, 10).ToUniTask(useFirstValue: true);
//        //    v.Is(1);

//        //    v = await UniTask.DelayFrame(10).ToObservable().ToTask();
//        //    v.Is(10);

//        //    v = await UniTask.FromResult(99).ToObservable();
//        //    v.Is(99);
//        //});

//        //[UnityTest]
//        //public IEnumerator AwaitableReactiveProperty() => UniTask.ToCoroutine(async () =>
//        //{
//        //    var rp1 = new ReactiveProperty<int>(99);

//        //    UniTask.DelayFrame(100).ContinueWith(x => rp1.Value = x).Forget();

//        //    await rp1;

//        //    rp1.Value.Is(100);

//        //    // var delay2 = UniTask.DelayFrame(10);
//        //    // var (a, b ) = await UniTask.WhenAll(rp1.WaitUntilValueChangedAsync(), delay2);

//        //});

//        //[UnityTest]
//        //public IEnumerator AwaitableReactiveCommand() => UniTask.ToCoroutine(async () =>
//        //{
//        //    var rc = new ReactiveCommand<int>();

//        //    UniTask.DelayFrame(100).ContinueWith(x => rc.Execute(x)).Forget();

//        //    var v = await rc;

//        //    v.Is(100);
//        //});

//        [UnityTest]
//        public IEnumerator ExceptionlessCancellation() => UniTask.ToCoroutine(async () =>
//        {
//            var cts = new CancellationTokenSource();

//            UniTask.DelayFrame(10).ContinueWith(() => cts.Cancel()).Forget();

//            var first = Time.frameCount;
//            var canceled = await UniTask.DelayFrame(100, cancellationToken: cts.Token).SuppressCancellationThrow();

//            (Time.frameCount - first).Should().Be(11); // 10 frame canceled
//            canceled.Should().Be(true);
//        });

//        [UnityTest]
//        public IEnumerator ExceptionCancellation() => UniTask.ToCoroutine(async () =>
//        {
//            var cts = new CancellationTokenSource();

//            UniTask.DelayFrame(10).ContinueWith(() => cts.Cancel()).Forget();

//            bool occur = false;
//            try
//            {
//                await UniTask.DelayFrame(100, cancellationToken: cts.Token);
//            }
//            catch (OperationCanceledException)
//            {
//                occur = true;
//            }
//            occur.Should().BeTrue();
//        });

//        IEnumerator ToaruCoroutineEnumerator()
//        {
//            yield return null;
//            yield return null;
//            yield return null;
//            yield return null;
//            yield return null;
//        }

//        [UnityTest]
//        public IEnumerator ExceptionUnobserved1() => UniTask.ToCoroutine(async () =>
//        {
//            bool calledEx = false;
//            Action<Exception> action = exx =>
//            {
//                calledEx = true;
//                exx.Message.Should().Be("MyException");
//            };

//            UniTaskScheduler.UnobservedTaskException += action;

//            var ex = InException1();
//            ex = default(UniTask);

//            await UniTask.DelayFrame(3);

//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//            GC.Collect();

//            await UniTask.DelayFrame(1);

//            calledEx.Should().BeTrue();

//            UniTaskScheduler.UnobservedTaskException -= action;
//        });

//        [UnityTest]
//        public IEnumerator ExceptionUnobserved2() => UniTask.ToCoroutine(async () =>
//        {
//            bool calledEx = false;
//            Action<Exception> action = exx =>
//            {
//                calledEx = true;
//                exx.Message.Should().Be("MyException");
//            };

//            UniTaskScheduler.UnobservedTaskException += action;

//            var ex = InException2();
//            ex = default(UniTask<int>);

//            await UniTask.DelayFrame(3);

//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//            GC.Collect();

//            await UniTask.DelayFrame(1);

//            calledEx.Should().BeTrue();

//            UniTaskScheduler.UnobservedTaskException -= action;
//        });

//        async UniTask InException1()
//        {
//            await UniTask.Yield();
//            throw new Exception("MyException");
//        }

//        async UniTask<int> InException2()
//        {
//            await UniTask.Yield();
//            throw new Exception("MyException");
//        }

//        [UnityTest]
//        public IEnumerator NestedEnumerator() => UniTask.ToCoroutine(async () =>
//        {
//            var time = Time.realtimeSinceStartup;

//            await ParentCoroutineEnumerator();

//            var elapsed = Time.realtimeSinceStartup - time;
//            ((int)Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven)).Should().Be(3);
//        });

//        IEnumerator ParentCoroutineEnumerator()
//        {
//            yield return ChildCoroutineEnumerator();
//        }

//        IEnumerator ChildCoroutineEnumerator()
//        {
//            yield return new WaitForSeconds(3);
//        }

//#endif
//#endif
//    }
//}

//#endif