using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections;
using System.Threading;
using UnityEngine.TestTools;

namespace Cysharp.Threading.TasksTests
{
    public class AsyncReactivePropertyTest
    {
        private int _callCounter;
        
        [UnityTest]
        public IEnumerator WaitCancelWait() => UniTask.ToCoroutine(async () =>
        {
            // Test case for https://github.com/Cysharp/UniTask/issues/444
            
            var property = new AsyncReactiveProperty<int>(0);
            
            var cts1 = new CancellationTokenSource();
            var cts2 = new CancellationTokenSource();
            WaitForProperty(property, cts1.Token);
            WaitForProperty(property, cts2.Token);

            _callCounter = 0;
            property.Value = 1;
            _callCounter.Should().Be(2);

            cts2.Cancel();
            cts2.Dispose();
            cts1.Cancel();
            cts1.Dispose();

            var cts3 = new CancellationTokenSource();
            WaitForProperty(property, cts3.Token);

            _callCounter = 0;
            property.Value = 2;
            _callCounter.Should().Be(1);
            
            cts3.Cancel();
            cts3.Dispose();
            await UniTask.CompletedTask;
        });

        private async void WaitForProperty(AsyncReactiveProperty<int> property, CancellationToken token)
        {
            while (true)
            {
                try
                {
                    await property.WaitAsync(token);
                    _callCounter++;
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
