using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using FluentAssertions;
namespace Cysharp.Threading.TasksTests
{
    public class SwitchToSyncParamsTest
    {
        [UnityTest]
        public IEnumerator SyncParamsTest()
        {
            return UniTask.ToCoroutine(async () =>
            {
                var switchToMainThread = UniTask.SwitchToSyncParams(SyncParams.MainThread);
                switchToMainThread.GetAwaiter().IsCompleted.Should().BeTrue();
                await switchToMainThread;
                var switchToThreadPool = UniTask.SwitchToSyncParams(SyncParams.ThreadPool);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToThreadPool;
                PlayerLoopHelper.IsMainThread.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.IsMainThread.Should().BeTrue();

                await UniTask.Yield(PlayerLoopTiming.Initialization);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.TryGetCurrentPlayerLoopTiming().Should().Be(PlayerLoopTiming.EarlyUpdate);
                await UniTask.Yield(PlayerLoopTiming.LastEarlyUpdate);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.TryGetCurrentPlayerLoopTiming().Should().Be(PlayerLoopTiming.PreUpdate);
                await UniTask.Yield(PlayerLoopTiming.LastPreUpdate);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.TryGetCurrentPlayerLoopTiming().Should().Be(PlayerLoopTiming.Update);
                await UniTask.Yield(PlayerLoopTiming.LastUpdate);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.TryGetCurrentPlayerLoopTiming().Should().Be(PlayerLoopTiming.PreLateUpdate);
                await UniTask.Yield(PlayerLoopTiming.LastPreLateUpdate);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.TryGetCurrentPlayerLoopTiming().Should().Be(PlayerLoopTiming.PostLateUpdate);
                await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
                switchToThreadPool.GetAwaiter().IsCompleted.Should().BeFalse();
                await switchToMainThread;
                PlayerLoopHelper.TryGetCurrentPlayerLoopTiming().Should().Be(PlayerLoopTiming.EarlyUpdate);
            });
        }
    }
}