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
    public class GenericsWhenAllAny
    {

        [UnityTest]
        public IEnumerator WhenAllT15() => UniTask.ToCoroutine(async () =>
        {
            var t01 = Tes<int>();
            var t02 = Tes<int>();
            var t03 = Tes<int>();
            var t04 = Tes<int>();
            var t05 = Tes<int>();
            var t06 = Tes<int>();
            var t07 = Tes<int>();
            var t08 = Tes<int>();
            var t09 = Tes<int>();
            var t10 = Tes<int>();
            var t11 = Tes<int>();
            var t12 = Tes<int>();
            var t13 = Tes<int>();
            var t14 = Tes<int>();
            var t15 = Tes<int>();

            await UniTask.WhenAll(t01, t02, t03, t04, t05, t06, t07, t08, t09, t10, t11, t12, t13, t14, t15);
        });

        [UnityTest]
        public IEnumerator WhenAllT01_Generics1() => UniTask.ToCoroutine(async () =>
        {
            var t01 = Tes<MyGenerics<int>>();

            await UniTask.WhenAll(t01);
        });

        [UnityTest]
        public IEnumerator WhenAllT02_Generics1() => UniTask.ToCoroutine(async () =>
        {
            var t01 = Tes<MyGenerics<int>>();
            var t02 = Tes<MyGenerics<int>>();

            await UniTask.WhenAll(t01, t02);
        });

        [UnityTest]
        public IEnumerator WhenAllT03_Generics1() => UniTask.ToCoroutine(async () =>
        {
            var t01 = Tes<MyGenerics<int>>();
            var t02 = Tes<MyGenerics<int>>();
            var t03 = Tes<MyGenerics<int>>();

            await UniTask.WhenAll(t01, t02, t03);
        });

        [UnityTest]
        public IEnumerator WhenAllT04_Generics1() => UniTask.ToCoroutine(async () =>
        {
            var t01 = Tes<MyGenerics<int>>();
            var t02 = Tes<MyGenerics<int>>();
            var t03 = Tes<MyGenerics<int>>();
            var t04 = Tes<MyGenerics<int>>();

            await UniTask.WhenAll(t01, t02, t03, t04);
        });

        // will fail.

        //[UnityTest]
        //public IEnumerator WhenAllT05_Generics1() => UniTask.ToCoroutine(async () =>
        //{
        //    var t01 = Tes<MyGenerics<int>>();
        //    var t02 = Tes<MyGenerics<int>>();
        //    var t03 = Tes<MyGenerics<int>>();
        //    var t04 = Tes<MyGenerics<int>>();
        //    var t05 = Tes<MyGenerics<int>>();

        //    await UniTask.WhenAll(t01, t02, t03, t04, t05);
        //});

        //[UnityTest]
        //public IEnumerator WhenAllT06_Generics1() => UniTask.ToCoroutine(async () =>
        //{
        //    var t01 = Tes<MyGenerics<int>>();
        //    var t02 = Tes<MyGenerics<int>>();
        //    var t03 = Tes<MyGenerics<int>>();
        //    var t04 = Tes<MyGenerics<int>>();
        //    var t05 = Tes<MyGenerics<int>>();
        //    var t06 = Tes<MyGenerics<int>>();

        //    await UniTask.WhenAll(t01, t02, t03, t04, t05, t06);
        //});

        //[UnityTest]
        //public IEnumerator WhenAllT07_Generics1() => UniTask.ToCoroutine(async () =>
        //{
        //    var t01 = Tes<MyGenerics<int>>();
        //    var t02 = Tes<MyGenerics<int>>();
        //    var t03 = Tes<MyGenerics<int>>();
        //    var t04 = Tes<MyGenerics<int>>();
        //    var t05 = Tes<MyGenerics<int>>();
        //    var t06 = Tes<MyGenerics<int>>();
        //    var t07 = Tes<MyGenerics<int>>();

        //    await UniTask.WhenAll(t01, t02, t03, t04, t05, t06, t07);
        //});

        //[UnityTest]
        //public IEnumerator WhenAllT15_Generics1() => UniTask.ToCoroutine(async () =>
        //{
        //    var t01 = Tes<MyGenerics<int>>();
        //    var t02 = Tes<MyGenerics<int>>();
        //    var t03 = Tes<MyGenerics<int>>();
        //    var t04 = Tes<MyGenerics<int>>();
        //    var t05 = Tes<MyGenerics<int>>();
        //    var t06 = Tes<MyGenerics<int>>();
        //    var t07 = Tes<MyGenerics<int>>();
        //    var t08 = Tes<MyGenerics<int>>();
        //    var t09 = Tes<MyGenerics<int>>();
        //    var t10 = Tes<MyGenerics<int>>();
        //    var t11 = Tes<MyGenerics<int>>();
        //    var t12 = Tes<MyGenerics<int>>();
        //    var t13 = Tes<MyGenerics<int>>();
        //    var t14 = Tes<MyGenerics<int>>();
        //    var t15 = Tes<MyGenerics<int>>();

        //    await UniTask.WhenAll(t01, t02, t03, t04, t05, t06, t07, t08, t09, t10, t11, t12, t13, t14, t15);
        //});

        async UniTask<T> Tes<T>()
        {
            await UniTask.Yield();
            return default;
        }
    }

    public class MyGenerics<T>
    {

    }

    public class MyGenerics2
    {

    }
}

#endif