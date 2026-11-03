#if UNITASK_DOTWEEN_SUPPORT
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework;

namespace Cysharp.Threading.TasksTests
{
    public class DOTweenAsyncExtensionsTests
    {
        [Test]
        public void AwaitForComplete_KillAndCancelAwait_CancelsAwaitable()
        {
            var value = 0f;
            var tween = DOTween.To(() => value, x => value = x, 1f, 1f)
                .SetEase(Ease.Linear)
                .SetAutoKill(true);
            tween.Goto(0.4f, true);

            using var cancellationTokenSource = new CancellationTokenSource();
            var awaitable = tween.AwaitForComplete(
                TweenCancelBehaviour.KillAndCancelAwait,
                cancellationTokenSource.Token);

            cancellationTokenSource.Cancel();

            Assert.AreEqual(UniTaskStatus.Canceled, awaitable.Status);
            Assert.Throws<OperationCanceledException>(() => awaitable.GetAwaiter().GetResult());
        }
    }
}
#endif
