#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks.Internal;
using System.Threading;

#if UNITY_2019_3_OR_NEWER
using UnityEngine.LowLevel;
#else
using UnityEngine.Experimental.LowLevel;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cysharp.Threading.Tasks
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

        // Last

        public struct UniTaskLoopRunnerLastInitialization { };
        public struct UniTaskLoopRunnerLastEarlyUpdate { };
        public struct UniTaskLoopRunnerLastFixedUpdate { };
        public struct UniTaskLoopRunnerLastPreUpdate { };
        public struct UniTaskLoopRunnerLastUpdate { };
        public struct UniTaskLoopRunnerLastPreLateUpdate { };
        public struct UniTaskLoopRunnerLastPostLateUpdate { };

        // Yield

        public struct UniTaskLoopRunnerYieldInitialization { };
        public struct UniTaskLoopRunnerYieldEarlyUpdate { };
        public struct UniTaskLoopRunnerYieldFixedUpdate { };
        public struct UniTaskLoopRunnerYieldPreUpdate { };
        public struct UniTaskLoopRunnerYieldUpdate { };
        public struct UniTaskLoopRunnerYieldPreLateUpdate { };
        public struct UniTaskLoopRunnerYieldPostLateUpdate { };

        // Yield Last

        public struct UniTaskLoopRunnerLastYieldInitialization { };
        public struct UniTaskLoopRunnerLastYieldEarlyUpdate { };
        public struct UniTaskLoopRunnerLastYieldFixedUpdate { };
        public struct UniTaskLoopRunnerLastYieldPreUpdate { };
        public struct UniTaskLoopRunnerLastYieldUpdate { };
        public struct UniTaskLoopRunnerLastYieldPreLateUpdate { };
        public struct UniTaskLoopRunnerLastYieldPostLateUpdate { };
    }

    public enum PlayerLoopTiming
    {
        Initialization = 0,
        LastInitialization = 1,

        EarlyUpdate = 2,
        LastEarlyUpdate = 3,

        FixedUpdate = 4,
        LastFixedUpdate = 5,

        PreUpdate = 6,
        LastPreUpdate = 7,

        Update = 8,
        LastUpdate = 9,

        PreLateUpdate = 10,
        LastPreLateUpdate = 11,

        PostLateUpdate = 12,
        LastPostLateUpdate = 13
    }

    public interface IPlayerLoopItem
    {
        bool MoveNext();
    }

    public static class PlayerLoopHelper
    {
        public static SynchronizationContext UnitySynchronizationContext => unitySynchronizationContetext;
        public static int MainThreadId => mainThreadId;
        internal static string ApplicationDataPath => applicationDataPath;

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;

        static int mainThreadId;
        static string applicationDataPath;
        static SynchronizationContext unitySynchronizationContetext;
        static ContinuationQueue[] yielders;
        static PlayerLoopRunner[] runners;

        static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem loopSystem,
            Type loopRunnerYieldType, ContinuationQueue cq, Type lastLoopRunnerYieldType, ContinuationQueue lastCq,
            Type loopRunnerType, PlayerLoopRunner runner, Type lastLoopRunnerType, PlayerLoopRunner lastRunner)
        {

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += (state) =>
            {
                if (state == PlayModeStateChange.EnteredEditMode || state == PlayModeStateChange.EnteredPlayMode)
                {
                    return;
                }

                if (runner != null)
                {
                    runner.Clear();
                }
                if (lastRunner != null)
                {
                    lastRunner.Clear();
                }

                if (cq != null)
                {
                    cq.Clear();
                }
                if (lastCq != null)
                {
                    lastCq.Clear();
                }
            };
#endif

            var yieldLoop = new PlayerLoopSystem
            {
                type = loopRunnerYieldType,
                updateDelegate = cq.Run
            };

            var lastYieldLoop = new PlayerLoopSystem
            {
                type = lastLoopRunnerYieldType,
                updateDelegate = lastCq.Run
            };

            var runnerLoop = new PlayerLoopSystem
            {
                type = loopRunnerType,
                updateDelegate = runner.Run
            };

            var lastRunnerLoop = new PlayerLoopSystem
            {
                type = lastLoopRunnerType,
                updateDelegate = lastRunner.Run
            };

            // Remove items from previous initializations.
            var source = loopSystem.subSystemList
                .Where(ls => ls.type != loopRunnerYieldType && ls.type != loopRunnerType && ls.type != lastLoopRunnerYieldType && ls.type != lastLoopRunnerType)
                .ToArray();

            var dest = new PlayerLoopSystem[source.Length + 4];

            Array.Copy(source, 0, dest, 2, source.Length);
            dest[0] = yieldLoop;
            dest[1] = runnerLoop;
            dest[dest.Length - 2] = lastYieldLoop;
            dest[dest.Length - 1] = lastRunnerLoop;

            return dest;
        }

        static PlayerLoopSystem[] InsertUniTaskSynchronizationContext(PlayerLoopSystem loopSystem)
        {
            var loop = new PlayerLoopSystem
            {
                type = typeof(UniTaskSynchronizationContext),
                updateDelegate = UniTaskSynchronizationContext.Run
            };

            // Remove items from previous initializations.
            var source = loopSystem.subSystemList
                .Where(ls => ls.type != typeof(UniTaskSynchronizationContext))
                .ToArray();

            var dest = new System.Collections.Generic.List<PlayerLoopSystem>(source);

            var index = dest.FindIndex(x => x.type.Name == "ScriptRunDelayedTasks");
            if (index == -1)
            {
                index = dest.FindIndex(x => x.type.Name == "UniTaskLoopRunnerUpdate");
            }

            dest.Insert(index + 1, loop);

            return dest.ToArray();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            // capture default(unity) sync-context.
            unitySynchronizationContetext = SynchronizationContext.Current;
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
            try
            {
                applicationDataPath = Application.dataPath;
            }
            catch { }

#if UNITY_EDITOR && UNITY_2019_3_OR_NEWER
            // When domain reload is disabled, re-initialization is required when entering play mode; 
            // otherwise, pending tasks will leak between play mode sessions.
            var domainReloadDisabled = UnityEditor.EditorSettings.enterPlayModeOptionsEnabled &&
                UnityEditor.EditorSettings.enterPlayModeOptions.HasFlag(UnityEditor.EnterPlayModeOptions.DisableDomainReload);
            if (!domainReloadDisabled && runners != null) return;
#else
            if (runners != null) return; // already initialized
#endif

            var playerLoop =
#if UNITY_2019_3_OR_NEWER
                PlayerLoop.GetCurrentPlayerLoop();
#else
                PlayerLoop.GetDefaultPlayerLoop();
#endif

            Initialize(ref playerLoop);
        }


#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        static void InitOnEditor()
        {
            // Execute the play mode init method
            Init();

            // register an Editor update delegate, used to forcing playerLoop update
            EditorApplication.update += ForceEditorPlayerLoopUpdate;
        }

        private static void ForceEditorPlayerLoopUpdate()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                // Not in Edit mode, don't interfere
                return;
            }

            // EditorApplication.QueuePlayerLoopUpdate causes performance issue, don't call directly.
            // EditorApplication.QueuePlayerLoopUpdate();

            if (yielders != null)
            {
                foreach (var item in yielders)
                {
                    if (item != null) item.Run();
                }
            }

            if (runners != null)
            {
                foreach (var item in runners)
                {
                    if (item != null) item.Run();
                }
            }

            UniTaskSynchronizationContext.Run();
        }

#endif

        public static void Initialize(ref PlayerLoopSystem playerLoop)
        {
            yielders = new ContinuationQueue[14];
            runners = new PlayerLoopRunner[14];

            var copyList = playerLoop.subSystemList.ToArray();

            // Initialization
            copyList[0].subSystemList = InsertRunner(copyList[0], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldInitialization), yielders[0] = new ContinuationQueue(PlayerLoopTiming.Initialization),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldInitialization), yielders[1] = new ContinuationQueue(PlayerLoopTiming.LastInitialization),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization), runners[0] = new PlayerLoopRunner(PlayerLoopTiming.Initialization),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastInitialization), runners[1] = new PlayerLoopRunner(PlayerLoopTiming.LastInitialization));
            // EarlyUpdate
            copyList[1].subSystemList = InsertRunner(copyList[1], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldEarlyUpdate), yielders[2] = new ContinuationQueue(PlayerLoopTiming.EarlyUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldEarlyUpdate), yielders[3] = new ContinuationQueue(PlayerLoopTiming.LastEarlyUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerEarlyUpdate), runners[2] = new PlayerLoopRunner(PlayerLoopTiming.EarlyUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastEarlyUpdate), runners[3] = new PlayerLoopRunner(PlayerLoopTiming.LastEarlyUpdate));
            // FixedUpdate
            copyList[2].subSystemList = InsertRunner(copyList[2], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldFixedUpdate), yielders[4] = new ContinuationQueue(PlayerLoopTiming.FixedUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldFixedUpdate), yielders[5] = new ContinuationQueue(PlayerLoopTiming.LastFixedUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerFixedUpdate), runners[4] = new PlayerLoopRunner(PlayerLoopTiming.FixedUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastFixedUpdate), runners[5] = new PlayerLoopRunner(PlayerLoopTiming.LastFixedUpdate));
            // PreUpdate
            copyList[3].subSystemList = InsertRunner(copyList[3], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreUpdate), yielders[6] = new ContinuationQueue(PlayerLoopTiming.PreUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreUpdate), yielders[7] = new ContinuationQueue(PlayerLoopTiming.LastPreUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreUpdate), runners[6] = new PlayerLoopRunner(PlayerLoopTiming.PreUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreUpdate), runners[7] = new PlayerLoopRunner(PlayerLoopTiming.LastPreUpdate));
            // Update
            copyList[4].subSystemList = InsertRunner(copyList[4], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldUpdate), yielders[8] = new ContinuationQueue(PlayerLoopTiming.Update),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldUpdate), yielders[9] = new ContinuationQueue(PlayerLoopTiming.LastUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerUpdate), runners[8] = new PlayerLoopRunner(PlayerLoopTiming.Update),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastUpdate), runners[9] = new PlayerLoopRunner(PlayerLoopTiming.LastUpdate));
            // PreLateUpdate
            copyList[5].subSystemList = InsertRunner(copyList[5], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreLateUpdate), yielders[10] = new ContinuationQueue(PlayerLoopTiming.PreLateUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreLateUpdate), yielders[11] = new ContinuationQueue(PlayerLoopTiming.LastPreLateUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreLateUpdate), runners[10] = new PlayerLoopRunner(PlayerLoopTiming.PreLateUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreLateUpdate), runners[11] = new PlayerLoopRunner(PlayerLoopTiming.LastPreLateUpdate));
            // PostLateUpdate
            copyList[6].subSystemList = InsertRunner(copyList[6], typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPostLateUpdate), yielders[12] = new ContinuationQueue(PlayerLoopTiming.PostLateUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPostLateUpdate), yielders[13] = new ContinuationQueue(PlayerLoopTiming.LastPostLateUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerPostLateUpdate), runners[12] = new PlayerLoopRunner(PlayerLoopTiming.PostLateUpdate),
                                                                  typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPostLateUpdate), runners[13] = new PlayerLoopRunner(PlayerLoopTiming.LastPostLateUpdate));

            // Insert UniTaskSynchronizationContext to Update loop
            copyList[4].subSystemList = InsertUniTaskSynchronizationContext(copyList[4]);

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

        // Diagnostics helper

#if UNITY_2019_3_OR_NEWER

        public static void DumpCurrentPlayerLoop()
        {
            var playerLoop = UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"PlayerLoop List");
            foreach (var header in playerLoop.subSystemList)
            {
                sb.AppendFormat("------{0}------", header.type.Name);
                sb.AppendLine();
                foreach (var subSystem in header.subSystemList)
                {
                    sb.AppendFormat("{0}", subSystem.type.Name);
                    sb.AppendLine();

                    if (subSystem.subSystemList != null)
                    {
                        UnityEngine.Debug.LogWarning("More Subsystem:" + subSystem.subSystemList.Length);
                    }
                }
            }

            UnityEngine.Debug.Log(sb.ToString());
        }

        public static bool IsInjectedUniTaskPlayerLoop()
        {
            var playerLoop = UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();

            foreach (var header in playerLoop.subSystemList)
            {
                foreach (var subSystem in header.subSystemList)
                {
                    if (subSystem.type == typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

#endif

    }
}

