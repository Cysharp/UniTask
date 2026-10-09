#pragma warning disable CS1998

using System;
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
   }
}
