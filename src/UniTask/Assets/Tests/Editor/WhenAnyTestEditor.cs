//#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
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
//using Unity.Collections;
//using System.Threading;
//using NUnit.Framework;
//using UnityEngine.TestTools;
//using FluentAssertions;

//namespace Cysharp.Threading.TasksTests
//{
//    public class WhenAnyTest
//    {
//        [UnityTest]
//        public IEnumerator WhenAnyCanceled() => UniTask.ToCoroutine(async () =>
//        {
//            var cts = new CancellationTokenSource();
//            var successDelayTask = UniTask.Delay(TimeSpan.FromSeconds(1));
//            var cancelTask = UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cts.Token);
//            cts.CancelAfterSlim(200);

//            try
//            {
//                var r = await UniTask.WhenAny(new[] { successDelayTask, cancelTask });
//            }
//            catch (Exception ex)
//            {
//                ex.Should().BeAssignableTo<OperationCanceledException>();
//            }
//        });
//    }
//}

//#endif