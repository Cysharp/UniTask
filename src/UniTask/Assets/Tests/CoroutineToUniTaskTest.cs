#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Scripting;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using System.Threading;
using NUnit.Framework;
using UnityEngine.TestTools;
using FluentAssertions;

namespace Cysharp.Threading.TasksTests
{
    public class CoroutineToUniTaskTest
    {
        [UnityTest]
        public IEnumerator EarlyUpdate() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.EarlyUpdate);

            var l = new List<(int, int)>();
            var currentFrame = Time.frameCount;
            var t = Worker(l).ToUniTask();

            l.Count.Should().Be(1);
            l[0].Should().Be((0, currentFrame));

            await t;

            l[1].Should().Be((1, Time.frameCount));
            l[1].Item2.Should().NotBe(currentFrame);
        });

        [UnityTest]
        public IEnumerator LateUpdate() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

            var l = new List<(int, int)>();
            var currentFrame = Time.frameCount;
            var t = Worker(l).ToUniTask();

            l.Count.Should().Be(1);
            l[0].Should().Be((0, currentFrame));

            await t;

            l[1].Should().Be((1, Time.frameCount));
            l[1].Item2.Should().NotBe(currentFrame);
        });

        //[UnityTest]
        //public IEnumerator TestCoroutine()
        //{
        //    yield return UniTask.Yield(PlayerLoopTiming.EarlyUpdate).ToUniTask().ToCoroutine();

        //    var nanika = (UnityEngine.MonoBehaviour)GameObject.FindObjectOfType(typeof(UnityEngine.MonoBehaviour));

        //    var l = new List<(int, int)>();
        //    var currentFrame = Time.frameCount;
        //    var t = nanika.StartCoroutine(Worker(l));

        //    l.Count.Should().Be(1);
        //    l[0].Should().Be((0, currentFrame));

        //    yield return t;

        //    l[1].Should().Be((1, Time.frameCount));
        //    l[1].Item2.Should().NotBe(currentFrame);
        //}

        //[UnityTest]
        //public IEnumerator TestCoroutine2()
        //{
        //    yield return UniTask.Yield(PlayerLoopTiming.PostLateUpdate).ToUniTask().ToCoroutine();

        //    var nanika = (UnityEngine.MonoBehaviour)GameObject.FindObjectOfType(typeof(UnityEngine.MonoBehaviour));

        //    var l = new List<(int, int)>();
        //    var currentFrame = Time.frameCount;
        //    var t = nanika.StartCoroutine(Worker(l));

        //    l.Count.Should().Be(1);
        //    l[0].Should().Be((0, currentFrame));

        //    yield return t;

        //    l[1].Should().Be((1, Time.frameCount));
        //    l[1].Item2.Should().NotBe(currentFrame);
        //}

        [UnityTest]
        public IEnumerator ImmediateRunTest() => UniTask.ToCoroutine(async () =>
        {
            var l = new List<int>();
            var x1 = Immediate(l);
            var x2 = Immediate(l);
            var x3 = DelayOne(l);

            var t1 = x1.ToUniTask();
            CollectionAssert.AreEqual(l, new[] { 1, 2, 3 });
            await t1;

            var t2 = x2.ToUniTask();
            CollectionAssert.AreEqual(l, new[] { 1, 2, 3, 1, 2, 3 });

            var t3 = x3.ToUniTask();
            CollectionAssert.AreEqual(l, new[] { 1, 2, 3, 1, 2, 3, 10 });

            await UniTask.WhenAll(t2, t3);
            CollectionAssert.AreEqual(l, new[] { 1, 2, 3, 1, 2, 3, 10, 20, 30 });

        });

        IEnumerator Immediate(List<int> l)
        {
            l.Add(1);
            l.Add(2);
            l.Add(3);
            yield break;
        }

        IEnumerator DelayOne(List<int> l)
        {
            l.Add(10);
            yield return null;
            l.Add(20);
            l.Add(30);
        }

#if !UNITY_WEBGL

        [UnityTest]
        public IEnumerator WaitForSecondsTest() => UniTask.ToCoroutine(async () =>
        {
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

            Time.timeScale = 0.5f;
            try
            {
                var now = DateTimeOffset.UtcNow;

                await WaitFor();

                var elapsed = DateTimeOffset.UtcNow - now;

                (5.8f <= elapsed.TotalSeconds && elapsed.TotalSeconds <= 6.2f).Should().BeTrue();
            }
            finally
            {
                Time.timeScale = 1.0f;
            }
        });

        IEnumerator WaitFor()
        {
            yield return new WaitForSeconds(3.0f);
        }

#endif

        IEnumerator Worker(List<(int, int)> l)
        {
            l.Add((0, Time.frameCount));
            yield return null;
            l.Add((1, Time.frameCount));
        }

        public async UniTask Foo()
        {
            var tasks = new List<UniTask>();
            var t = Bar<int>();
            tasks.Add(t);

            t = Bar<int>();
            tasks.Add(t);

            await UniTask.WhenAll(tasks);
        }

        public async UniTask<T> Bar<T>()
        {
            await UniTask.Yield();
            return default(T);
        }
    }
}

#endif