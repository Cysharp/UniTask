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
        public async Task PropagateWhenAllException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await Task.WhenAll(ThrowAsync(), ThrowAsync(), ThrowAsync()).AsUniTask();
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
   }
}
