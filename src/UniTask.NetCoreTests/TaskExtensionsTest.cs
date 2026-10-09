#pragma warning disable CS1998

using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class TaskExtensionsTest
    {
        [Fact]
        public async Task PropagateException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await ThrowAsync().AsUniTask();
            });
            
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await ThrowOrValueAsync().AsUniTask();
            });
        }

        [Fact]
        public async Task PropagateExceptionWhenAll()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await Task.WhenAll(ThrowAsync(), ThrowAsync()).AsUniTask();
            });
        }
 
        async Task ThrowAsync()
        {
            throw new InvalidOperationException();
        }

        async Task<int> ThrowOrValueAsync()
        {
            throw new InvalidOperationException();
        }

        [Fact]
        public void AsUniTask_CompletedTask_IsSucceeded()
        {
            UniTask task = Task.CompletedTask.AsUniTask();

            Assert.Equal(UniTaskStatus.Succeeded, task.Status);
        }

        [Fact]
        public void AsUniTask_CompletedTaskWithResult_IsSucceededWithSameResult()
        {
            UniTask<int> task = Task.FromResult(42).AsUniTask();

            Assert.Equal(UniTaskStatus.Succeeded, task.Status);
            Assert.Equal(42, task.GetAwaiter().GetResult());
        }

        [Fact]
        public void AsUniTask_FaultedTask_IsFaultedWithSameException()
        {
            var exception = new InvalidOperationException("expected");

            UniTask task = Task.FromException(exception).AsUniTask();

            Assert.Equal(UniTaskStatus.Faulted, task.Status);
            Assert.Same(exception, Assert.Throws<InvalidOperationException>(() => task.GetAwaiter().GetResult()));
        }

        [Fact]
        public void AsUniTask_FaultedTaskWithResult_IsFaultedWithSameException()
        {
            var exception = new InvalidOperationException("expected");

            UniTask<int> task = Task.FromException<int>(exception).AsUniTask();

            Assert.Equal(UniTaskStatus.Faulted, task.Status);
            Assert.Same(exception, Assert.Throws<InvalidOperationException>(() => task.GetAwaiter().GetResult()));
        }

        [Fact]
        public void AsUniTask_CanceledTask_IsCanceled()
        {
            UniTask task = Task.FromCanceled(new CancellationToken(true)).AsUniTask();

            Assert.Equal(UniTaskStatus.Canceled, task.Status);
            Assert.Throws<OperationCanceledException>(() => task.GetAwaiter().GetResult());
        }

        [Fact]
        public void AsUniTask_CanceledTaskWithResult_IsCanceled()
        {
            UniTask<int> task = Task.FromCanceled<int>(new CancellationToken(true)).AsUniTask();

            Assert.Equal(UniTaskStatus.Canceled, task.Status);
            Assert.Throws<OperationCanceledException>(() => task.GetAwaiter().GetResult());
        }

        [Fact]
        public void AsUniTask_OperationCanceledFaultedTask_IsCanceled()
        {
            UniTask task = Task.FromException(new OperationCanceledException()).AsUniTask();

            Assert.Equal(UniTaskStatus.Canceled, task.Status);
            Assert.Throws<OperationCanceledException>(() => task.GetAwaiter().GetResult());
        }

        [Fact]
        public void AsUniTask_OperationCanceledFaultedTaskWithResult_IsCanceled()
        {
            UniTask<int> task = Task.FromException<int>(new OperationCanceledException()).AsUniTask();

            Assert.Equal(UniTaskStatus.Canceled, task.Status);
            Assert.Throws<OperationCanceledException>(() => task.GetAwaiter().GetResult());
        }

        [Fact]
        public async Task AsUniTask_PendingTask_SucceedsWhenTaskCompletes()
        {
            var source = new TaskCompletionSource();

            UniTask task = source.Task.AsUniTask();

            Assert.Equal(UniTaskStatus.Pending, task.Status);

            source.SetResult();
            await task;

            Assert.Equal(UniTaskStatus.Succeeded, task.Status);
        }

        [Fact]
        public async Task AsUniTask_PendingTaskWithResult_SucceedsWithSameResultWhenTaskCompletes()
        {
            var source = new TaskCompletionSource<int>();

            UniTask<int> task = source.Task.AsUniTask();

            Assert.Equal(UniTaskStatus.Pending, task.Status);

            source.SetResult(42);

            Assert.Equal(42, await task);
        }

        [Fact]
        public async Task AsUniTask_PendingTask_FaultsWithSameExceptionWhenTaskFaults()
        {
            var exception = new InvalidOperationException("expected");
            var source = new TaskCompletionSource();

            UniTask task = source.Task.AsUniTask();

            Assert.Equal(UniTaskStatus.Pending, task.Status);

            source.SetException(exception);

            Assert.Same(exception, await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await task;
            }));
        }

        [Fact]
        public async Task AsUniTask_PendingTaskWithResult_FaultsWithSameExceptionWhenTaskFaults()
        {
            var exception = new InvalidOperationException("expected");
            var source = new TaskCompletionSource<int>();

            UniTask<int> task = source.Task.AsUniTask();

            Assert.Equal(UniTaskStatus.Pending, task.Status);

            source.SetException(exception);

            Assert.Same(exception, await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await task;
            }));
        }

        [Fact]
        public async Task AsUniTask_PendingTask_IsCanceledWhenTaskIsCanceled()
        {
            var source = new TaskCompletionSource();

            UniTask task = source.Task.AsUniTask();

            Assert.Equal(UniTaskStatus.Pending, task.Status);

            source.SetCanceled();

            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                await task;
            });
        }

        [Fact]
        public async Task AsUniTask_PendingTaskWithResult_IsCanceledWhenTaskIsCanceled()
        {
            var source = new TaskCompletionSource<int>();

            UniTask<int> task = source.Task.AsUniTask();

            Assert.Equal(UniTaskStatus.Pending, task.Status);

            source.SetCanceled();

            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                await task;
            });
        }
   }
}
