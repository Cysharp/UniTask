using Cysharp.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
    [Flags]
    public enum SyncParams
    {
        ThreadPool = 0,

        #region PlayerLoopTiming
        Initialization = 1 << PlayerLoopTiming.Initialization,
        LastInitialization = 1 << PlayerLoopTiming.LastInitialization,

        EarlyUpdate = 1 << PlayerLoopTiming.EarlyUpdate,
        LastEarlyUpdate = 1 << PlayerLoopTiming.LastEarlyUpdate,

        FixedUpdate = 1 << PlayerLoopTiming.FixedUpdate,
        LastFixedUpdate = 1 << PlayerLoopTiming.LastFixedUpdate,

        PreUpdate = 1 << PlayerLoopTiming.PreUpdate,
        LastPreUpdate = 1 << PlayerLoopTiming.LastPreUpdate,

        Update = 1 << PlayerLoopTiming.Update,
        LastUpdate = 1 << PlayerLoopTiming.LastUpdate,

        PreLateUpdate = 1 << PlayerLoopTiming.PreLateUpdate,
        LastPreLateUpdate = 1 << PlayerLoopTiming.LastPreLateUpdate,

        PostLateUpdate = 1 << PlayerLoopTiming.PostLateUpdate,
        LastPostLateUpdate = 1 << PlayerLoopTiming.LastPostLateUpdate,
        #endregion

        MainThread = EarlyUpdate | PreUpdate | Update | PreLateUpdate | PostLateUpdate,
    }
    public struct SyncParamsPlayerLoopTimingEnumerable
    {
        internal SyncParams syncParams;
        public SyncParamsPlayerLoopTimingEnumerator GetEnumerator() => new SyncParamsPlayerLoopTimingEnumerator() { syncParams = syncParams };

    }

    public static class SyncParamsHelpers
    {
        static readonly byte[] TrailingZeroCountDeBruijn =
        {
            00, 01, 28, 02, 29, 14, 24, 03,
            30, 22, 20, 15, 25, 17, 04, 08,
            31, 27, 13, 23, 21, 19, 16, 07,
            26, 12, 18, 06, 11, 05, 10, 09
        };
        /// <summary>
        /// From System.Numerics.BitOperations
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>License:https://github.com/dotnet/runtime/blob/6072e4d3a7a2a1493f514cdf4be75a3d56580e84/LICENSE.TXT </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int TrailingZeroCount(uint value)
        {
            // Using deBruijn sequence, k=2, n=5 (2^5=32) : 0b_0000_0111_0111_1100_1011_0101_0011_0001u
            //TODO:Avoid array bounds check
            return TrailingZeroCountDeBruijn[(int)(((value & (uint)-(int)value) * 0x077CB531u) >> 27)];
        }
        public static SyncParamsPlayerLoopTimingEnumerable EnumeratePlayerLoopTimings(this SyncParams syncParams) => new SyncParamsPlayerLoopTimingEnumerable() { syncParams = syncParams };

        public static SyncParams FromPlayerLoopTiming(PlayerLoopTiming playerLoopTiming) => (SyncParams)(1 << (int)playerLoopTiming);
    }

    public struct SyncParamsPlayerLoopTimingEnumerator
    {
        internal SyncParams syncParams;
        public bool MoveNext()
        {
            if (syncParams == SyncParams.ThreadPool)
            {
                return false;
            }
            var value = (int)syncParams;
            var tzcnt = SyncParamsHelpers.TrailingZeroCount((uint)value);
            Current = (PlayerLoopTiming)tzcnt;
            syncParams = (SyncParams)(value ^ (1 << tzcnt));
            return true;
        }
        public PlayerLoopTiming Current { get; private set; }
    }


}
