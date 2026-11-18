using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class AsTaskTest
    {
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public async Task AsTaskPreservesCancellation(bool hasResult, bool convertBeforeCancellation, bool useDefaultToken)
        {
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            var token = useDefaultToken ? default : cancellation.Token;

            if (hasResult)
            {
                var source = new UniTaskCompletionSource<int>();
                await AssertCancellationPreserved(() => source.Task.AsTask(), source.TrySetCanceled, token, convertBeforeCancellation);
            }
            else
            {
                var source = new UniTaskCompletionSource();
                await AssertCancellationPreserved(() => source.Task.AsTask(), source.TrySetCanceled, token, convertBeforeCancellation);
            }
        }

        static async Task AssertCancellationPreserved(
            Func<Task> convert, Func<CancellationToken, bool> cancel, CancellationToken token, bool convertBeforeCancellation)
        {
            var task = convertBeforeCancellation ? convert() : null;
            cancel(token);
            task = task ?? convert();

            Assert.True(task.IsCanceled);
            var error = await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await task);
            Assert.Equal(token, error.CancellationToken);
        }
    }
}
