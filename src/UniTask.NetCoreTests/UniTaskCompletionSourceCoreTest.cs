using System;
using Cysharp.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class UniTaskCompletionSourceCoreTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RejectingNullExceptionLeavesCoreAvailable(bool hasContinuation)
        {
            var core = new UniTaskCompletionSourceCore<int>();
            var version = core.Version;
            var continuationCalls = 0;

            if (hasContinuation)
            {
                core.OnCompleted(_ => continuationCalls++, null, version);
            }

            Assert.Throws<ArgumentNullException>(() => core.TrySetException(null));
            Assert.Equal(UniTaskStatus.Pending, core.GetStatus(version));
            Assert.Equal(0, continuationCalls);

            Assert.True(core.TrySetResult(42));
            Assert.Equal(42, core.GetResult(version));
            Assert.Equal(hasContinuation ? 1 : 0, continuationCalls);
        }

        [Fact]
        public void CompletedCoreRejectsNullExceptionWithFalse()
        {
            var core = new UniTaskCompletionSourceCore<int>();
            Assert.True(core.TrySetResult(42));

            Assert.False(core.TrySetException(null));
            Assert.Equal(42, core.GetResult(core.Version));
        }
    }
}
