#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using System;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public abstract class PlayerLoopTimer : IDisposable, IPlayerLoopItem
    {
        readonly CancellationToken cancellationToken;
        readonly Action<object> timerCallback;
        readonly object state;
        readonly PlayerLoopTiming playerLoopTiming;
        readonly bool periodic;

        bool isPlaying;
        bool isDisposed;

        protected PlayerLoopTimer(bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
        {
            this.periodic = periodic;
            this.playerLoopTiming = playerLoopTiming;
            this.cancellationToken = cancellationToken;
            this.timerCallback = timerCallback;
            this.state = state;
        }

        public static PlayerLoopTimer Create(TimeSpan delayTimeSpan, bool periodic, DelayType delayType, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
        {
            switch (delayType)
            {
                case DelayType.UnscaledDeltaTime:
                    return new IgnoreTimeScalePlayerLoopTimer(delayTimeSpan, periodic, playerLoopTiming, cancellationToken, timerCallback, state);
                case DelayType.Realtime:
                    return new RealtimePlayerLoopTimer(delayTimeSpan, periodic, playerLoopTiming, cancellationToken, timerCallback, state);
                case DelayType.DeltaTime:
                default:
                    return new DeltaTimePlayerLoopTimer(delayTimeSpan, periodic, playerLoopTiming, cancellationToken, timerCallback, state);
            }
        }

        public static PlayerLoopTimer StartNew(TimeSpan delayTimeSpan, bool periodic, DelayType delayType, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
        {
            var timer = Create(delayTimeSpan, periodic, delayType, playerLoopTiming, cancellationToken, timerCallback, state);
            timer.Restart();
            return timer;
        }

        /// <summary>
        /// Restart(Reset and Start) timer.
        /// </summary>
        public void Restart()
        {
            if (isDisposed) throw new ObjectDisposedException(null);

            ResetCore(); // init state
            isPlaying = true;
            PlayerLoopHelper.AddAction(playerLoopTiming, this);
        }

        /// <summary>
        /// Stop timer.
        /// </summary>
        public void Stop()
        {
            isPlaying = false;
        }

        protected abstract void ResetCore();

        public void Dispose()
        {
            isDisposed = true;
        }

        bool IPlayerLoopItem.MoveNext()
        {
            if (isDisposed) return false;
            if (!isPlaying) return false;
            if (cancellationToken.IsCancellationRequested) return false;

            if (!MoveNextCore())
            {
                timerCallback(state);

                if (periodic)
                {
                    ResetCore();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        protected abstract bool MoveNextCore();
    }

    sealed class DeltaTimePlayerLoopTimer : PlayerLoopTimer
    {
        int initialFrame;
        float elapsed;
        readonly float delayTimeSpan;

        public DeltaTimePlayerLoopTimer(TimeSpan delayTimeSpan, bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
            : base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
        {
            this.elapsed = 0.0f;
            this.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
            this.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
        }

        protected override bool MoveNextCore()
        {
            if (elapsed == 0.0f)
            {
                if (initialFrame == Time.frameCount)
                {
                    return true;
                }
            }

            elapsed += Time.deltaTime;
            if (elapsed >= delayTimeSpan)
            {
                return false;
            }

            return true;
        }

        protected override void ResetCore()
        {
            elapsed = 0.0f;
        }
    }

    sealed class IgnoreTimeScalePlayerLoopTimer : PlayerLoopTimer
    {
        int initialFrame;
        float elapsed;
        readonly float delayTimeSpan;

        public IgnoreTimeScalePlayerLoopTimer(TimeSpan delayTimeSpan, bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
            : base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
        {
            this.elapsed = 0.0f;
            this.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
            this.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
        }

        protected override bool MoveNextCore()
        {
            if (elapsed == 0.0f)
            {
                if (initialFrame == Time.frameCount)
                {
                    return true;
                }
            }

            elapsed += Time.unscaledDeltaTime;
            if (elapsed >= delayTimeSpan)
            {
                return false;
            }

            return true;
        }

        protected override void ResetCore()
        {
            elapsed = 0.0f;
        }
    }

    sealed class RealtimePlayerLoopTimer : PlayerLoopTimer
    {
        ValueStopwatch stopwatch;
        readonly long delayTimeSpanTicks;

        public RealtimePlayerLoopTimer(TimeSpan delayTimeSpan, bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
            : base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
        {
            this.stopwatch = ValueStopwatch.StartNew();
            this.delayTimeSpanTicks = delayTimeSpan.Ticks;
        }

        protected override bool MoveNextCore()
        {
            if (stopwatch.ElapsedTicks >= delayTimeSpanTicks)
            {
                return false;
            }

            return true;
        }

        protected override void ResetCore()
        {
            this.stopwatch = ValueStopwatch.StartNew();
        }
    }
}

