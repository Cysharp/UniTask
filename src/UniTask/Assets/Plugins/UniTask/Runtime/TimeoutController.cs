#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    // CancellationTokenSource itself can not reuse but CancelAfter(Timeout.InfiniteTimeSpan) allows reuse if did not reach timeout.
    // Similar discussion:
    // https://github.com/dotnet/runtime/issues/4694
    // https://github.com/dotnet/runtime/issues/48492
    // This TimeoutController emulate similar implementation, using CancelAfterSlim; to achieve zero allocation timeout.

    public sealed class TimeoutController : IDisposable
    {
        readonly static Action<object> CancelCancellationTokenSourceStateDelegate = new Action<object>(CancelCancellationTokenSourceState);

        static void CancelCancellationTokenSourceState(object state)
        {
            var cts = (CancellationTokenSource)state;
            cts.Cancel();
        }

        CancellationTokenSource timeoutSource;
        CancellationTokenSource linkedSource;
        PlayerLoopTimer timer;
        bool isDisposed;

        readonly DelayType delayType;
        readonly PlayerLoopTiming delayTiming;
        readonly CancellationTokenSource originalLinkCancellationTokenSource;

        public TimeoutController(DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            this.timeoutSource = new CancellationTokenSource();
            this.originalLinkCancellationTokenSource = null;
            this.linkedSource = null;
            this.delayType = delayType;
            this.delayTiming = delayTiming;
        }

        public TimeoutController(CancellationTokenSource linkCancellationTokenSource, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
        {
            this.timeoutSource = new CancellationTokenSource();
            this.originalLinkCancellationTokenSource = linkCancellationTokenSource;
            this.linkedSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutSource.Token, linkCancellationTokenSource.Token);
            this.delayType = delayType;
            this.delayTiming = delayTiming;
        }

        public CancellationToken Timeout(int millisecondsTimeout)
        {
            return Timeout(TimeSpan.FromMilliseconds(millisecondsTimeout));
        }

        public CancellationToken Timeout(TimeSpan timeout)
        {
            if (originalLinkCancellationTokenSource != null && originalLinkCancellationTokenSource.IsCancellationRequested)
            {
                return originalLinkCancellationTokenSource.Token;
            }

            // Timeouted, create new source and timer.
            if (timeoutSource.IsCancellationRequested)
            {
                timeoutSource.Dispose();
                timeoutSource = new CancellationTokenSource();
                if (linkedSource != null)
                {
                    this.linkedSource.Cancel();
                    this.linkedSource.Dispose();
                    this.linkedSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutSource.Token, originalLinkCancellationTokenSource.Token);
                }

                timer?.Dispose();
                timer = null;
            }

            var useSource = (linkedSource != null) ? linkedSource : timeoutSource;
            var token = useSource.Token;
            if (timer == null)
            {
                // Timer complete => timeoutSource.Cancel() -> linkedSource will be canceled.
                // (linked)token is canceled => stop timer
                timer = PlayerLoopTimer.StartNew(timeout, false, delayType, delayTiming, token, CancelCancellationTokenSourceStateDelegate, timeoutSource);
            }
            else
            {
                timer.Restart(timeout);
            }

            return token;
        }

        public bool IsTimeout()
        {
            return timeoutSource.IsCancellationRequested;
        }

        public void Reset()
        {
            timer?.Stop();
        }

        public void Dispose()
        {
            if (isDisposed) return;

            try
            {
                // stop timer.
                timer?.Dispose();

                // cancel and dispose.
                timeoutSource.Cancel();
                timeoutSource.Dispose();
                if (linkedSource != null)
                {
                    linkedSource.Cancel();
                    linkedSource.Dispose();
                }
            }
            finally
            {
                isDisposed = true;
            }
        }
    }
}