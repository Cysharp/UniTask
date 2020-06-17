using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<AsyncUnit> Timer(TimeSpan dueTime, PlayerLoopTiming updateTiming = PlayerLoopTiming.Update, bool ignoreTimeScale = false)
        {
            return new Timer(dueTime, null, updateTiming, ignoreTimeScale);
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> Timer(TimeSpan dueTime, TimeSpan period, PlayerLoopTiming updateTiming = PlayerLoopTiming.Update, bool ignoreTimeScale = false)
        {
            return new Timer(dueTime, period, updateTiming, ignoreTimeScale);
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> Interval(TimeSpan period, PlayerLoopTiming updateTiming = PlayerLoopTiming.Update, bool ignoreTimeScale = false)
        {
            return new Timer(period, period, updateTiming, ignoreTimeScale);
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> TimerFrame(int dueTimeFrameCount, PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            if (dueTimeFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. dueTimeFrameCount:" + dueTimeFrameCount);
            }

            return new TimerFrame(dueTimeFrameCount, null, updateTiming);
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> TimerFrame(int dueTimeFrameCount, int periodFrameCount, PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            if (dueTimeFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. dueTimeFrameCount:" + dueTimeFrameCount);
            }
            if (periodFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus periodFrameCount. periodFrameCount:" + dueTimeFrameCount);
            }

            return new TimerFrame(dueTimeFrameCount, periodFrameCount, updateTiming);
        }

        public static IUniTaskAsyncEnumerable<AsyncUnit> IntervalFrame(int intervalFrameCount, PlayerLoopTiming updateTiming = PlayerLoopTiming.Update)
        {
            if (intervalFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus intervalFrameCount. intervalFrameCount:" + intervalFrameCount);
            }
            return new TimerFrame(intervalFrameCount, intervalFrameCount, updateTiming);
        }
    }

    internal class Timer : IUniTaskAsyncEnumerable<AsyncUnit>
    {
        readonly PlayerLoopTiming updateTiming;
        readonly TimeSpan dueTime;
        readonly TimeSpan? period;
        readonly bool ignoreTimeScale;

        public Timer(TimeSpan dueTime, TimeSpan? period, PlayerLoopTiming updateTiming, bool ignoreTimeScale)
        {
            this.updateTiming = updateTiming;
            this.dueTime = dueTime;
            this.period = period;
            this.ignoreTimeScale = ignoreTimeScale;
        }

        public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Timer(dueTime, period, updateTiming, ignoreTimeScale, cancellationToken);
        }

        class _Timer : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            readonly float dueTime;
            readonly float? period;
            readonly PlayerLoopTiming updateTiming;
            readonly bool ignoreTimeScale;
            CancellationToken cancellationToken;

            int initialFrame;
            float elapsed;
            bool dueTimePhase;
            bool completed;
            bool disposed;

            public _Timer(TimeSpan dueTime, TimeSpan? period, PlayerLoopTiming updateTiming, bool ignoreTimeScale, CancellationToken cancellationToken)
            {
                this.dueTime = (float)dueTime.TotalSeconds;
                this.period = (period == null) ? null : (float?)period.Value.TotalSeconds;

                if (this.dueTime <= 0) this.dueTime = 0;
                if (this.period != null)
                {
                    if (this.period <= 0) this.period = 1;
                }

                this.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
                this.dueTimePhase = true;
                this.updateTiming = updateTiming;
                this.ignoreTimeScale = ignoreTimeScale;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 2);
                PlayerLoopHelper.AddAction(updateTiming, this);
            }

            public AsyncUnit Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested || completed) return CompletedTasks.False;

                // reset value here.
                this.elapsed = 0;

                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }
                return default;
            }

            public bool MoveNext()
            {
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetResult(false);
                    return false;
                }

                if (dueTimePhase)
                {
                    if (elapsed == 0)
                    {
                        // skip in initial frame.
                        if (initialFrame == Time.frameCount)
                        {
                            return true;
                        }
                    }

                    elapsed += (ignoreTimeScale) ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime;

                    if (elapsed >= dueTime)
                    {
                        dueTimePhase = false;
                        completionSource.TrySetResult(true);
                    }
                }
                else
                {
                    if (period == null)
                    {
                        completed = true;
                        completionSource.TrySetResult(false);
                        return false;
                    }

                    elapsed += (ignoreTimeScale) ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime;

                    if (elapsed >= period)
                    {
                        completionSource.TrySetResult(true);
                    }
                }

                return true;
            }
        }
    }

    internal class TimerFrame : IUniTaskAsyncEnumerable<AsyncUnit>
    {
        readonly PlayerLoopTiming updateTiming;
        readonly int dueTimeFrameCount;
        readonly int? periodFrameCount;

        public TimerFrame(int dueTimeFrameCount, int? periodFrameCount, PlayerLoopTiming updateTiming)
        {
            this.updateTiming = updateTiming;
            this.dueTimeFrameCount = dueTimeFrameCount;
            this.periodFrameCount = periodFrameCount;
        }

        public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _TimerFrame(dueTimeFrameCount, periodFrameCount, updateTiming, cancellationToken);
        }

        class _TimerFrame : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>, IPlayerLoopItem
        {
            readonly int dueTimeFrameCount;
            readonly int? periodFrameCount;
            CancellationToken cancellationToken;

            int initialFrame;
            int currentFrame;
            bool dueTimePhase;
            bool completed;
            bool disposed;

            public _TimerFrame(int dueTimeFrameCount, int? periodFrameCount, PlayerLoopTiming updateTiming, CancellationToken cancellationToken)
            {
                if (dueTimeFrameCount <= 0) dueTimeFrameCount = 0;
                if (periodFrameCount != null)
                {
                    if (periodFrameCount <= 0) periodFrameCount = 1;
                }

                this.initialFrame = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
                this.dueTimePhase = true;
                this.dueTimeFrameCount = dueTimeFrameCount;
                this.periodFrameCount = periodFrameCount;
                this.cancellationToken = cancellationToken;

                TaskTracker.TrackActiveTask(this, 2);
                PlayerLoopHelper.AddAction(updateTiming, this);
            }

            public AsyncUnit Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                // return false instead of throw
                if (disposed || cancellationToken.IsCancellationRequested || completed) return CompletedTasks.False;


                // reset value here.
                this.currentFrame = 0;

                completionSource.Reset();
                return new UniTask<bool>(this, completionSource.Version);
            }

            public UniTask DisposeAsync()
            {
                if (!disposed)
                {
                    disposed = true;
                    TaskTracker.RemoveTracking(this);
                }
                return default;
            }

            public bool MoveNext()
            {
                if (disposed || cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetResult(false);
                    return false;
                }

                if (dueTimePhase)
                {
                    if (currentFrame == 0)
                    {
                        if (dueTimeFrameCount == 0)
                        {
                            dueTimePhase = false;
                            completionSource.TrySetResult(true);
                            return true;
                        }

                        // skip in initial frame.
                        if (initialFrame == Time.frameCount)
                        {
                            return true;
                        }
                    }

                    if (++currentFrame >= dueTimeFrameCount)
                    {
                        dueTimePhase = false;
                        completionSource.TrySetResult(true);
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (periodFrameCount == null)
                    {
                        completed = true;
                        completionSource.TrySetResult(false);
                        return false;
                    }

                    if (++currentFrame >= periodFrameCount)
                    {
                        completionSource.TrySetResult(true);
                    }
                }

                return true;
            }
        }
    }
}