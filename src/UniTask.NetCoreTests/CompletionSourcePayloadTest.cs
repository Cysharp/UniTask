using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class CompletionSourcePayloadTest
    {
        const int Trials = 2048;
        static readonly TimeSpan BarrierTimeout = TimeSpan.FromSeconds(10);

        [StructLayout(LayoutKind.Explicit, Size = 4096)]
        struct WideResult
        {
            [FieldOffset(0)] public int First;
            [FieldOffset(4092)] public int Last;
        }

        [Fact]
        public void ConcurrentResultsKeepTheWinningValue()
        {
            Race(
                () => new UniTaskCompletionSource<WideResult>(),
                (source, writer) => source.TrySetResult(new WideResult { First = writer, Last = writer }),
                source =>
                {
                    var result = source.Task.GetAwaiter().GetResult();
                    return result.First == result.Last ? result.First : -1;
                });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConcurrentCancellationKeepsTheWinningToken(bool generic)
        {
            using var first = new CancellationTokenSource();
            using var second = new CancellationTokenSource();
            first.Cancel();
            second.Cancel();
            var firstToken = first.Token;
            var secondToken = second.Token;

            Race(
                () => CreateSource(generic),
                (source, writer) => ((ICancelPromise)source).TrySetCanceled(writer == 1 ? firstToken : secondToken),
                source =>
                {
                    var error = Assert.Throws<OperationCanceledException>(() => source.GetResult(0));
                    return error.CancellationToken == firstToken ? 1 : error.CancellationToken == secondToken ? 2 : -1;
                });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConcurrentExceptionsKeepTheWinningError(bool generic)
        {
            Race(
                () => CreateSource(generic),
                (source, writer) => ((IRejectPromise)source).TrySetException(new WriterException(writer)),
                source => Assert.Throws<WriterException>(() => source.GetResult(0)).Writer);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void NullExceptionDoesNotChangeTheExistingCompletionState(bool generic)
        {
            var source = CreateSource(generic);
            var rejecter = (IRejectPromise)source;

            Assert.Throws<ArgumentNullException>(() => rejecter.TrySetException(null));
            Assert.Equal(UniTaskStatus.Pending, source.GetStatus(0));

            Assert.True(((ICancelPromise)source).TrySetCanceled());
            Assert.False(rejecter.TrySetException(null));
            Assert.Equal(UniTaskStatus.Canceled, source.GetStatus(0));
        }

        static IUniTaskSource CreateSource(bool generic)
        {
            return generic ? new UniTaskCompletionSource<int>() : new UniTaskCompletionSource();
        }

        static void Race<TSource>(Func<TSource> create, Func<TSource, int, bool> complete, Func<TSource, int> read)
            where TSource : class, IUniTaskSource
        {
            using var barrier = new Barrier(3);
            TSource source = null;
            bool firstWon = false;
            bool secondWon = false;
            int invalidWinners = 0;
            int wrongPayloads = 0;
            int incorrectNotifications = 0;
            int notifications = 0;
            Action<object> recordNotification = _ => Interlocked.Increment(ref notifications);

            void Advance()
            {
                if (!barrier.SignalAndWait(BarrierTimeout))
                {
                    throw new TimeoutException("The completion writers did not reach the barrier.");
                }
            }

            Task StartWriter(int writer, Action<bool> recordResult)
            {
                return Task.Factory.StartNew(() =>
                {
                    for (var trial = 0; trial < Trials; trial++)
                    {
                        Advance();
                        recordResult(complete(source, writer));
                        Advance();
                    }
                }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }

            var first = StartWriter(1, won => firstWon = won);
            var second = StartWriter(2, won => secondWon = won);
            for (var trial = 0; trial < Trials; trial++)
            {
                source = create();
                notifications = 0;
                source.OnCompleted(recordNotification, null, 0);
                Advance();
                Advance();

                if (firstWon == secondWon) invalidWinners++;
                if (read(source) != (firstWon ? 1 : 2)) wrongPayloads++;
                if (notifications != 1) incorrectNotifications++;
            }
            Task.WaitAll(first, second);

            Assert.Equal(0, invalidWinners);
            Assert.Equal(0, wrongPayloads);
            Assert.Equal(0, incorrectNotifications);
        }

        sealed class WriterException : Exception
        {
            public int Writer { get; }

            public WriterException(int writer)
            {
                Writer = writer;
            }
        }
    }
}
