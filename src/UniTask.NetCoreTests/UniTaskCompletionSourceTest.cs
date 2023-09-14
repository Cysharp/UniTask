using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NetCoreTests.Linq;
using Xunit;

namespace NetCoreTests
{
    public class AutoResetUniTaskCompletionSourceTest
    {
        [Fact]
        public async Task SetResultAfterReturn()
        {
            var source1 = AutoResetUniTaskCompletionSource.Create();
            source1.TrySetResult();
            await source1.Task;
            
            source1.TrySetResult().Should().BeFalse();
            
            var source2 = AutoResetUniTaskCompletionSource.Create();
            source2.TrySetResult();
            await source2.Task;

            source2.TrySetResult().Should().BeFalse();
        }

        [Fact]
        public async Task SetCancelAfterReturn()
        {
            var source = AutoResetUniTaskCompletionSource.Create();
            source.TrySetResult();
            await source.Task;
            
            source.TrySetCanceled().Should().BeFalse();
        }
        
        [Fact]
        public async Task SetExceptionAfterReturn()
        {
            var source = AutoResetUniTaskCompletionSource.Create();
            source.TrySetResult();
            await source.Task;
            
            source.TrySetException(new UniTaskTestException()).Should().BeFalse();
        }
        
        [Fact]
        public async Task SetResultWithValueAfterReturn()
        {
            var source1 = AutoResetUniTaskCompletionSource<int>.Create();
            source1.TrySetResult(100);
            (await source1.Task).Should().Be(100);
            
            source1.TrySetResult(100).Should().BeFalse();
            
            var source2 = AutoResetUniTaskCompletionSource.Create();
            source2.TrySetResult();
            await source2.Task;
            source2.TrySetResult().Should().BeFalse();
        }

        [Fact]
        public async Task SetCancelWithValueAfterReturn()
        {
            var source = AutoResetUniTaskCompletionSource<int>.Create();
            source.TrySetResult(100);
            (await source.Task).Should().Be(100);
            source.TrySetCanceled().Should().BeFalse();
        }
        
        [Fact]
        public async Task SetExceptionWithValueAfterReturn()
        {
            var source = AutoResetUniTaskCompletionSource<int>.Create();
            source.TrySetResult(100);
            (await source.Task).Should().Be(100);
            source.TrySetException(new UniTaskTestException()).Should().BeFalse();
        }
    }
}