using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NetCoreTests
{
    public class CompletionSourceContinuationTest
    {
        [Theory]
        [InlineData(false, false, UniTaskStatus.Succeeded)]
        [InlineData(false, false, UniTaskStatus.Faulted)]
        [InlineData(false, false, UniTaskStatus.Canceled)]
        [InlineData(false, true, UniTaskStatus.Succeeded)]
        [InlineData(false, true, UniTaskStatus.Faulted)]
        [InlineData(false, true, UniTaskStatus.Canceled)]
        [InlineData(true, false, UniTaskStatus.Succeeded)]
        [InlineData(true, false, UniTaskStatus.Faulted)]
        [InlineData(true, false, UniTaskStatus.Canceled)]
        [InlineData(true, true, UniTaskStatus.Succeeded)]
        [InlineData(true, true, UniTaskStatus.Faulted)]
        [InlineData(true, true, UniTaskStatus.Canceled)]
        public async Task ContinuationsCanRegisterAcrossThreads(bool generic, bool registerBeforeCompletion, UniTaskStatus status)
        {
            var (source, setResult) = CreateSource(generic);
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            var error = new InvalidOperationException("Test failure.");
            var registeredCalls = new List<int>();
            Task workerRegistration = null;
            var workerFinishedDuringCallback = false;
            var workerContinuationCalls = 0;

            void RegisterFromAnotherThread(object _)
            {
                registeredCalls.Add(1);
                workerRegistration = Task.Factory.StartNew(
                    () => source.OnCompleted(__ => workerContinuationCalls++, null, 0),
                    CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                workerFinishedDuringCallback = workerRegistration.Wait(TimeSpan.FromSeconds(2));
            }

            source.GetStatus(0).Should().Be(UniTaskStatus.Pending);
            if (registerBeforeCompletion)
            {
                source.OnCompleted(RegisterFromAnotherThread, null, 0);
                source.OnCompleted(_ => registeredCalls.Add(2), null, 0);
            }

            var completed = status switch
            {
                UniTaskStatus.Succeeded => setResult(),
                UniTaskStatus.Faulted => ((IRejectPromise)source).TrySetException(error),
                UniTaskStatus.Canceled => ((ICancelPromise)source).TrySetCanceled(cancellation.Token),
                _ => throw new ArgumentOutOfRangeException(nameof(status))
            };
            completed.Should().BeTrue();

            if (!registerBeforeCompletion)
            {
                source.OnCompleted(RegisterFromAnotherThread, null, 0);
                source.OnCompleted(_ => registeredCalls.Add(2), null, 0);
            }

            await workerRegistration;
            workerFinishedDuringCallback.Should().BeTrue();
            workerContinuationCalls.Should().Be(1);
            registeredCalls.Should().Equal(1, 2);
            source.GetStatus(0).Should().Be(status);

            switch (status)
            {
                case UniTaskStatus.Succeeded:
                    source.GetResult(0);
                    break;
                case UniTaskStatus.Faulted:
                    Assert.Same(error, Assert.Throws<InvalidOperationException>(() => source.GetResult(0)));
                    break;
                case UniTaskStatus.Canceled:
                    Assert.Equal(cancellation.Token, Assert.Throws<OperationCanceledException>(() => source.GetResult(0)).CancellationToken);
                    break;
            }
        }

        static (IUniTaskSource Source, Func<bool> SetResult) CreateSource(bool generic)
        {
            if (generic)
            {
                var source = new UniTaskCompletionSource<int>();
                return (source, () => source.TrySetResult(42));
            }

            var untypedSource = new UniTaskCompletionSource();
            return (untypedSource, untypedSource.TrySetResult);
        }
    }
}
