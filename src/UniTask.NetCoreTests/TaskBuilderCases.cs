#pragma warning disable CS1998 

using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using Cysharp.Threading.Tasks.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Runtime.CompilerServices;

namespace NetCoreTests
{
    public class UniTaskBuilderTest
    {
        [Fact]
        public async Task Empty()
        {
            await Core();

            static async UniTask Core()
            {
            }
        }

        [Fact]
        public async Task EmptyThrow()
        {
            await Assert.ThrowsAsync<TaskTestException>(async () => await Core());

            static async UniTask Core()
            {
                throw new TaskTestException();
            }
        }

        [Fact]
        public async Task Task_Done()
        {
            await Core();

            static async UniTask Core()
            {
                await new TestAwaiter(true, UniTaskStatus.Succeeded);
            }
        }

        [Fact]
        public async Task Task_Fail()
        {
            await Assert.ThrowsAsync<TaskTestException>(async () => await Core());

            static async UniTask Core()
            {
                await new TestAwaiter(true, UniTaskStatus.Faulted);
            }
        }

        [Fact]
        public async Task Task_Cancel()
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await Core());

            static async UniTask Core()
            {
                await new TestAwaiter(true, UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task AwaitUnsafeOnCompletedCall_Task_SetResult()
        {
            await Core();

            static async UniTask Core()
            {
                await new TestAwaiter(false, UniTaskStatus.Succeeded);
                await new TestAwaiter(false, UniTaskStatus.Succeeded);
                await new TestAwaiter(false, UniTaskStatus.Succeeded);
            }
        }

        [Fact]
        public async Task AwaitUnsafeOnCompletedCall_Task_SetException()
        {
            await Assert.ThrowsAsync<TaskTestException>(async () => await Core());

            static async UniTask Core()
            {
                await new TestAwaiter(false, UniTaskStatus.Succeeded);
                await new TestAwaiter(false, UniTaskStatus.Faulted);
                throw new InvalidOperationException();
            }
        }

        [Fact]
        public async Task AwaitUnsafeOnCompletedCall_Task_SetCancelException()
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await Core());

            static async UniTask Core()
            {
                await new TestAwaiter(false, UniTaskStatus.Succeeded);
                await new TestAwaiter(false, UniTaskStatus.Canceled);
                throw new InvalidOperationException();
            }
        }
    }

    public class UniTask_T_BuilderTest
    {
        [Fact]
        public async Task Empty()
        {
            (await Core()).Should().Be(10);

            static async UniTask<int> Core()
            {
                return 10;
            }
        }

        [Fact]
        public async Task EmptyThrow()
        {
            await Assert.ThrowsAsync<TaskTestException>(async () => await Core());

            static async UniTask<int> Core()
            {
                throw new TaskTestException();
            }
        }

        [Fact]
        public async Task Task_Done()
        {
            (await Core()).Should().Be(10);

            static async UniTask<int> Core()
            {
                return await new TestAwaiter<int>(true, UniTaskStatus.Succeeded, 10);
            }
        }

        [Fact]
        public async Task Task_Fail()
        {
            await Assert.ThrowsAsync<TaskTestException>(async () => await Core());

            static async UniTask<int> Core()
            {
                return await new TestAwaiter<int>(true, UniTaskStatus.Faulted, 10);
            }
        }

        [Fact]
        public async Task Task_Cancel()
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await Core());

            static async UniTask<int> Core()
            {
                return await new TestAwaiter<int>(true, UniTaskStatus.Canceled, 10);
            }
        }

        [Fact]
        public async Task AwaitUnsafeOnCompletedCall_Task_SetResult()
        {
            (await Core()).Should().Be(6);

            static async UniTask<int> Core()
            {
                var sum = 0;
                sum += await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 1);
                sum += await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 2);
                sum += await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 3);
                return sum;
            }
        }

        [Fact]
        public async Task AwaitUnsafeOnCompletedCall_Task_SetException()
        {
            await Assert.ThrowsAsync<TaskTestException>(async () => await Core());

            static async UniTask<int> Core()
            {
                await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 10);
                await new TestAwaiter<int>(false, UniTaskStatus.Faulted, 10);
                throw new InvalidOperationException();
            }
        }

        [Fact]
        public async Task AwaitUnsafeOnCompletedCall_Task_SetCancelException()
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await Core());

            static async UniTask<int> Core()
            {
                await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 10);
                await new TestAwaiter<int>(false, UniTaskStatus.Canceled, 10);
                throw new InvalidOperationException();
            }
        }
    }

    public class TaskTestException : Exception
    {

    }

    public struct TestAwaiter : ICriticalNotifyCompletion
    {
        readonly UniTaskStatus status;
        readonly bool isCompleted;

        public TestAwaiter(bool isCompleted, UniTaskStatus status)
        {
            this.isCompleted = isCompleted;
            this.status = status;
        }

        public TestAwaiter GetAwaiter() => this;

        public bool IsCompleted => isCompleted;

        public void GetResult()
        {
            switch (status)
            {
                case UniTaskStatus.Faulted:
                    throw new TaskTestException();
                case UniTaskStatus.Canceled:
                    throw new OperationCanceledException();
                case UniTaskStatus.Pending:
                case UniTaskStatus.Succeeded:
                default:
                    break;
            }
        }

        public void OnCompleted(Action continuation)
        {
            ThreadPool.QueueUserWorkItem(_ => continuation(), null);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ => continuation(), null);
        }
    }

    public struct TestAwaiter<T> : ICriticalNotifyCompletion
    {
        readonly UniTaskStatus status;
        readonly bool isCompleted;
        readonly T value;

        public TestAwaiter(bool isCompleted, UniTaskStatus status, T value)
        {
            this.isCompleted = isCompleted;
            this.status = status;
            this.value = value;
        }

        public TestAwaiter<T> GetAwaiter() => this;

        public bool IsCompleted => isCompleted;

        public T GetResult()
        {
            switch (status)
            {
                case UniTaskStatus.Faulted:
                    throw new TaskTestException();
                case UniTaskStatus.Canceled:
                    throw new OperationCanceledException();
                case UniTaskStatus.Pending:
                case UniTaskStatus.Succeeded:
                default:
                    return value;
            }
        }

        public void OnCompleted(Action continuation)
        {
            ThreadPool.QueueUserWorkItem(_ => continuation(), null);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ => continuation(), null);
        }
    }
}
