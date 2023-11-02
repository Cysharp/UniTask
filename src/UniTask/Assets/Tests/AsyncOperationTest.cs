using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Cysharp.Threading.TasksTests
{
    public class AsyncOperationTest
    {
        [UnityTest]
        public IEnumerator ResourcesLoad_CancelOnPlayerLoop() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();
            var task = Resources.LoadAsync<Texture>("sample_texture").ToUniTask(cancellationToken: cts.Token, handleImmediately: false);
            
            cts.Cancel();
            task.Status.Should().Be(UniTaskStatus.Pending);

            await UniTask.NextFrame();
            task.Status.Should().Be(UniTaskStatus.Canceled);
        });
        
        [Test]
        public void ResourcesLoad_CancelImmediately()
        {
            {
                var cts = new CancellationTokenSource();
                var task = Resources.LoadAsync<Texture>("sample_texture").ToUniTask(cancellationToken: cts.Token, handleImmediately: true);

                cts.Cancel();
                task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }
    }
}
#endif
