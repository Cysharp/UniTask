#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Linq;
using UnityEngine;
using UniRx.Async.Internal;
using System.Threading;

#if UNITY_2019_3_OR_NEWER
using UnityEngine.LowLevel;
#else
using UnityEngine.Experimental.LowLevel;
#endif

namespace UniRx.Async
{
    public static class UniTaskLoopRunners
    {
        public struct UniTaskLoopRunnerInitialization { };
        public struct UniTaskLoopRunnerEarlyUpdate { };
        public struct UniTaskLoopRunnerFixedUpdate { };
        public struct UniTaskLoopRunnerPreUpdate { };
        public struct UniTaskLoopRunnerUpdate { };
        public struct UniTaskLoopRunnerPreLateUpdate { };
        public struct UniTaskLoopRunnerPostLateUpdate { };

        // Yield

        public struct UniTaskLoopRunnerYieldInitialization { };
        public struct UniTaskLoopRunnerYieldEarlyUpdate { };
        public struct UniTaskLoopRunnerYieldFixedUpdate { };
        public struct UniTaskLoopRunnerYieldPreUpdate { };
        public struct UniTaskLoopRunnerYieldUpdate { };
        public struct UniTaskLoopRunnerYieldPreLateUpdate { };
        public struct UniTaskLoopRunnerYieldPostLateUpdate { };
    }

    public enum PlayerLoopTiming
    {
        Initialization = 0,
        EarlyUpdate = 1,
        FixedUpdate = 2,
        PreUpdate = 3,
        Update = 4,
        PreLateUpdate = 5,
        PostLateUpdate = 6
    }

    public interface IPlayerLoopItem
    {
        bool MoveNext();
    }

    public static class PlayerLoopHelper
    {
        public static SynchronizationContext UnitySynchronizationContext => unitySynchronizationContetext;
        public static int MainThreadId => mainThreadId;

        static int mainThreadId;
        static SynchronizationContext unitySynchronizationContetext;
        static ContinuationQueue[] yielders;
        static PlayerLoopRunner[] runners;

        static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem loopSystem, Type loopRunnerYieldType, ContinuationQueue cq, Type loopRunnerType, PlayerLoopRunner runner)
        {
            var yieldLoop = new PlayerLoopSystem
            {
                type = loopRunnerYieldType,
#if UNITY_EDITOR
                updateDelegate = () =>
                {
                    if (Application.isPlaying)
                    {
                        cq.Run();
                    }
                }
#else
                updateDelegate = cq.Run
#endif
            };

            var runnerLoop = new PlayerLoopSystem
            {
                type = loopRunnerType,
#if UNITY_EDITOR
                updateDelegate = () =>
                {
                    if (Application.isPlaying)
                    {
                        runner.Run();
                    }
                }
#else
                updateDelegate = runner.Run
#endif
            };

            var dest = new PlayerLoopSystem[loopSystem.subSystemList.Length + 2];
            Array.Copy(loopSystem.subSystemList, 0, dest, 2, loopSystem.subSystemList.Length);
            dest[0] = yieldLoop;
            dest[1] = runnerLoop;
            return dest;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            // capture default(unity) sync-context.
            unitySynchronizationContetext = SynchronizationContext.Current;
            mainThreadId = Thread.CurrentThread.ManagedThreadId;

            if (runners != null) return; // already initialized

            var playerLoop =
#if UNITY_2019_3_OR_NEWER
                PlayerLoop.GetCurrentPlayerLoop();
#else
                PlayerLoop.GetDefaultPlayerLoop();
#endif

            Initialize(ref playerLoop);
        }

        public static void Initialize(ref PlayerLoopSystem playerLoop)
        {
            yielders = new ContinuationQueue[7];
            runners = new PlayerLoopRunner[7];

            var copyList = playerLoop.subSystemList.ToArray();

            copyList[0].subSystemList = InsertRunner(copyList[0], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldInitialization), yielders[0] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization), runners[0] = new PlayerLoopRunner());
            copyList[1].subSystemList = InsertRunner(copyList[1], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldEarlyUpdate), yielders[1] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerEarlyUpdate), runners[1] = new PlayerLoopRunner());
            copyList[2].subSystemList = InsertRunner(copyList[2], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldFixedUpdate), yielders[2] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerFixedUpdate), runners[2] = new PlayerLoopRunner());
            copyList[3].subSystemList = InsertRunner(copyList[3], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreUpdate), yielders[3] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreUpdate), runners[3] = new PlayerLoopRunner());
            copyList[4].subSystemList = InsertRunner(copyList[4], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldUpdate), yielders[4] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerUpdate), runners[4] = new PlayerLoopRunner());
            copyList[5].subSystemList = InsertRunner(copyList[5], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreLateUpdate), yielders[5] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreLateUpdate), runners[5] = new PlayerLoopRunner());
            copyList[6].subSystemList = InsertRunner(copyList[6], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPostLateUpdate), yielders[6] = new ContinuationQueue(), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPostLateUpdate), runners[6] = new PlayerLoopRunner());

            playerLoop.subSystemList = copyList;
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
        {
            runners[(int)timing].AddAction(action);
        }

        public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
        {
            yielders[(int)timing].Enqueue(continuation);
        }
    }
}

#endif