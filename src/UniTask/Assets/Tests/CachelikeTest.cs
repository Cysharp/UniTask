using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Cysharp.Threading.TasksTests
{
    public class Cachelike
    {
        [UnityTest]
        public IEnumerator Check() => UniTask.ToCoroutine(async () =>
        {
            {
                var v = await CachedCheck("foo", 10);
                v.Should().Be(10);

                var v2 = await CachedCheck("bar", 20);
                v2.Should().Be(20);

                var v3 = await CachedCheck("baz", 30);
                v3.Should().Be(30);
            }
            {
                var v = await CachedCheck("foo", 10);
                v.Should().Be(10);

                var v2 = await CachedCheck("bar", 20);
                v2.Should().Be(20);

                var v3 = await CachedCheck("baz", 30);
                v3.Should().Be(30);
            }
            {
                var v = CachedCheck("foo", 10);
                var v2 = CachedCheck("bar", 20);
                var v3 = CachedCheck("baz", 30);

                (await v).Should().Be(10);
                (await v2).Should().Be(20);
                (await v3).Should().Be(30);
            }
            {
                var v = CachedCheck("foo", 10, true);
                var v2 = CachedCheck("bar", 20, true);
                var v3 = CachedCheck("baz", 30, true);

                (await v).Should().Be(10);
                (await v2).Should().Be(20);
                (await v3).Should().Be(30);
            }
        });


        static Dictionary<string, int> cacheDict = new Dictionary<string, int>();

        async UniTask<int> CachedCheck(string cache, int value, bool yield = false)
        {
            if (!cacheDict.ContainsKey(cache))
            {
                await UniTask.Yield();
            }

            if (yield)
            {
                await UniTask.Yield();
            }

            if (cacheDict.TryGetValue(cache, out var v))
            {
                return v;
            }

            cacheDict.Add(cache, value);

            return value;
        }

     
    }








}
