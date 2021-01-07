using System;
using System.Diagnostics;

namespace Cysharp.Threading.Tasks.Internal
{
    internal readonly struct ValueStopwatch
    {
        static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;

        readonly long startTimestamp;

        public static ValueStopwatch StartNew() => new ValueStopwatch(Stopwatch.GetTimestamp());

        ValueStopwatch(long startTimestamp)
        {
            this.startTimestamp = startTimestamp;
        }

        public TimeSpan Elapsed => TimeSpan.FromTicks(this.ElapsedTicks);

        public bool IsInvalid => startTimestamp == 0;

        public long ElapsedTicks
        {
            get
            {
                if (startTimestamp == 0)
                {
                    throw new InvalidOperationException("Detected invalid initialization(use 'default'), only to create from StartNew().");
                }

                var delta = Stopwatch.GetTimestamp() - startTimestamp;
                return (long)(delta * TimestampToTicks);
            }
        }
    }
}