using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    // UniTaskScheduler.FlowExecutionContext is global, so these run apart from the other tests.
    [CollectionDefinition(nameof(ExecutionContextTest), DisableParallelization = true)]
    public class ExecutionContextTestCollection
    {
    }

    // With FlowExecutionContext set, an async UniTask method treats the ExecutionContext as an
    // async Task method does: it is restored when the method resumes, and what the method sets
    // before its first await does not reach its caller.
    [Collection(nameof(ExecutionContextTest))]
    public class ExecutionContextTest : IDisposable
    {
        const string Before = "set before the call";

        static readonly AsyncLocal<string> ambient = new AsyncLocal<string>();

        public ExecutionContextTest()
        {
            UniTaskScheduler.FlowExecutionContext = true;
        }

        public void Dispose()
        {
            UniTaskScheduler.FlowExecutionContext = false;
        }

        [Fact]
        public async Task ResumedByAFlowWithoutTheContext_KeepsAsyncLocal()
        {
            ambient.Value = Before;
            TaskCompletionSource<bool> read = new TaskCompletionSource<bool>();

            async UniTask<string> Body()
            {
                await read.Task;
                return ambient.Value;
            }

            // Suspends at its await before anything completes the read, so the resume is real.
            UniTask<string> body = Body();
            await CompleteFromAFlowWithoutTheContext(read);

            (await body).Should().Be(Before);
        }

        [Fact]
        public async Task WithoutResult_ResumedByAFlowWithoutTheContext_KeepsAsyncLocal()
        {
            ambient.Value = Before;
            TaskCompletionSource<bool> read = new TaskCompletionSource<bool>();
            string seen = null;

            async UniTask Body()
            {
                await read.Task;
                seen = ambient.Value;
            }

            UniTask body = Body();
            await CompleteFromAFlowWithoutTheContext(read);
            await body;

            seen.Should().Be(Before);
        }

        [Fact]
        public async Task UniTaskVoid_ResumedByAFlowWithoutTheContext_KeepsAsyncLocal()
        {
            ambient.Value = Before;
            TaskCompletionSource<bool> read = new TaskCompletionSource<bool>();
            TaskCompletionSource<string> seen = new TaskCompletionSource<string>();

            async UniTaskVoid Body()
            {
                await read.Task;
                seen.SetResult(ambient.Value);
            }

            Body().Forget();
            await CompleteFromAFlowWithoutTheContext(read);

            (await seen.Task).Should().Be(Before);
        }

        [Fact]
        public async Task WriteBeforeTheFirstAwait_DoesNotReachTheCaller()
        {
            ambient.Value = Before;
            TaskCompletionSource<bool> read = new TaskCompletionSource<bool>();

            async UniTask Callee()
            {
                ambient.Value = "set by the callee";
                await read.Task;
            }

            UniTask callee = Callee();

            ambient.Value.Should().Be(Before);
            read.SetResult(true);
            await callee;
        }

        // Restoring the context on resume without also isolating Start would hand a callee's
        // scope back to its caller.
        [Fact]
        public async Task ScopeOpenedAcrossAnAwait_DoesNotLeakIntoTheCallerOnResume()
        {
            ambient.Value = Before;
            TaskCompletionSource<bool> read = new TaskCompletionSource<bool>();

            async UniTask Callee()
            {
                string previous = ambient.Value;
                ambient.Value = "callee scope";
                try
                {
                    await read.Task;
                }
                finally
                {
                    ambient.Value = previous;
                }
            }

            async UniTask<string> Caller()
            {
                await Callee();
                return ambient.Value;
            }

            UniTask<string> caller = Caller();
            await CompleteFromAFlowWithoutTheContext(read);

            (await caller).Should().Be(Before);
        }

        // Completes the read from a flow that never saw the value, as I/O completion does.
        static Task CompleteFromAFlowWithoutTheContext(TaskCompletionSource<bool> read)
        {
            using (ExecutionContext.SuppressFlow())
            {
                return Task.Run(() => read.SetResult(true));
            }
        }
    }
}
