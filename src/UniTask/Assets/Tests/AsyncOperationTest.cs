using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Cysharp.Threading.TasksTests
{
    public class AsyncOperationTest
    {
        [UnityTest]
        public IEnumerator ResourcesLoad_Completed() => UniTask.ToCoroutine(async () =>
        {
            var asyncOperation = Resources.LoadAsync<Texture2D>("sample_texture");
            await asyncOperation.ToUniTask();
            asyncOperation.isDone.Should().BeTrue();
            asyncOperation.asset.GetType().Should().Be(typeof(Texture2D));
        });
        
        [UnityTest]
        public IEnumerator ResourcesLoad_CancelOnPlayerLoop() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();
            var task = Resources.LoadAsync<Texture>("sample_texture").ToUniTask(cancellationToken: cts.Token, cancelImmediately: false);
            
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
                var task = Resources.LoadAsync<Texture>("sample_texture").ToUniTask(cancellationToken: cts.Token, cancelImmediately: true);

                cts.Cancel();
                task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

#if ENABLE_UNITYWEBREQUEST && (!UNITY_2019_1_OR_NEWER || UNITASK_WEBREQUEST_SUPPORT)
        [UnityTest]
        public IEnumerator UnityWebRequest_Completed() => UniTask.ToCoroutine(async () =>
        {
            var filePath = System.IO.Path.Combine(Application.dataPath, "Tests", "Resources", "sample_texture.png");
            var asyncOperation = UnityWebRequest.Get($"file://{filePath}").SendWebRequest();
            await asyncOperation.ToUniTask();

            asyncOperation.isDone.Should().BeTrue();
            asyncOperation.webRequest.result.Should().Be(UnityWebRequest.Result.Success);
        });
        
        [UnityTest]
        public IEnumerator UnityWebRequest_CancelOnPlayerLoop() => UniTask.ToCoroutine(async () =>
        {
            var cts = new CancellationTokenSource();
            var filePath = System.IO.Path.Combine(Application.dataPath, "Tests", "Resources", "sample_texture.png");
            var task = UnityWebRequest.Get($"file://{filePath}").SendWebRequest().ToUniTask(cancellationToken: cts.Token);
            
            cts.Cancel();
            task.Status.Should().Be(UniTaskStatus.Pending);
            
            await UniTask.NextFrame();
            task.Status.Should().Be(UniTaskStatus.Canceled);
        });
        
        [Test]
        public void UnityWebRequest_CancelImmediately()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var filePath = System.IO.Path.Combine(Application.dataPath, "Tests", "Resources", "sample_texture.png");
            var task = UnityWebRequest.Get($"file://{filePath}").SendWebRequest().ToUniTask(cancellationToken: cts.Token, cancelImmediately: true);
            
            task.Status.Should().Be(UniTaskStatus.Canceled);
        }
#endif
    }
}
#endif
