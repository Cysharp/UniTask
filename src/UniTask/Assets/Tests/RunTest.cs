#pragma warning disable CS0618

#if !(UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
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
using UnityEngine.SceneManagement;
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
using System.Threading.Tasks;
#endif
using UnityEngine.Networking;

#if !UNITY_2019_3_OR_NEWER
using UnityEngine.Experimental.LowLevel;
#else
using UnityEngine.LowLevel;
#endif

#if !UNITY_WSA
using Unity.Jobs;
#endif
using Unity.Collections;
using System.Threading;
using NUnit.Framework;
using UnityEngine.TestTools;
using FluentAssertions;

namespace Cysharp.Threading.TasksTests
{
    public class RunTest
    {
#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#if !UNITY_WSA
#if !UNITY_WEBGL

        //[UnityTest]
        //public IEnumerator RunThread() => UniTask.ToCoroutine(async () =>
        //{
        //    var main = Thread.CurrentThread.ManagedThreadId;
        //    var v = await UniTask.Run(() => { return System.Threading.Thread.CurrentThread.ManagedThreadId; }, false);
        //    UnityEngine.Debug.Log("Ret Value is:" + v);
        //    UnityEngine.Debug.Log("Run Here and id:" + System.Threading.Thread.CurrentThread.ManagedThreadId);
        //    //v.Should().Be(3);
        //    main.Should().NotBe(Thread.CurrentThread.ManagedThreadId);
        //});

        [UnityTest]
        public IEnumerator RunThreadConfigure() => UniTask.ToCoroutine(async () =>
        {
            var main = Thread.CurrentThread.ManagedThreadId;
            var v = await UniTask.Run(() => 3, true);
            v.Should().Be(3);
            main.Should().Be(Thread.CurrentThread.ManagedThreadId);
        });

        //[UnityTest]
        //public IEnumerator RunThreadException() => UniTask.ToCoroutine(async () =>
        //{
        //    var main = Thread.CurrentThread.ManagedThreadId;
        //    try
        //    {
        //        await UniTask.Run<int>(() => throw new Exception(), false);
        //    }
        //    catch
        //    {
        //        main.Should().NotBe(Thread.CurrentThread.ManagedThreadId);
        //    }
        //});

        [UnityTest]
        public IEnumerator RunThreadExceptionConfigure() => UniTask.ToCoroutine(async () =>
        {
            var main = Thread.CurrentThread.ManagedThreadId;
            try
            {
#pragma warning disable CS1998
                await UniTask.Run<int>(async () => throw new Exception(), true);
#pragma warning restore CS1998
            }
            catch
            {
                main.Should().Be(Thread.CurrentThread.ManagedThreadId);
            }
        });

#endif
#endif
#endif
    }
}

#endif