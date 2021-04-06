#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks.Internal;
using System.Threading;

#if UNITY_2019_3_OR_NEWER
using UnityEngine.LowLevel;
using PlayerLoopType = UnityEngine.PlayerLoop;
#else
using UnityEngine.Experimental.LowLevel;
using PlayerLoopType = UnityEngine.Experimental.PlayerLoop;
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

#if UNITY_2020_2_OR_NEWER
        public struct UniTaskLoopRunnerTimeUpdate { };
        public struct UniTaskLoopRunnerLastTimeUpdate { };
        public struct UniTaskLoopRunnerYieldTimeUpdate { };
        public struct UniTaskLoopRunnerLastYieldTimeUpdate { };
#endif
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
        LastPostLateUpdate = 13,

#if UNITY_2020_2_OR_NEWER
        // Unity 2020.2 added TimeUpdate https://docs.unity3d.com/2020.2/Documentation/ScriptReference/PlayerLoop.TimeUpdate.html
        TimeUpdate = 14,
        LastTimeUpdate = 15,
#endif
    }

    [Flags]
    public enum InjectPlayerLoopTimings
    {
        /// <summary>
        /// Preset: All loops(default).
        /// </summary>
        All =
            Initialization | LastInitialization |
            EarlyUpdate | LastEarlyUpdate |
            FixedUpdate | LastFixedUpdate |
            PreUpdate | LastPreUpdate |
            Update | LastUpdate |
            PreLateUpdate | LastPreLateUpdate |
            PostLateUpdate | LastPostLateUpdate
#if UNITY_2020_2_OR_NEWER
            | TimeUpdate | LastTimeUpdate,
#else
            ,
#endif

        /// <summary>
        /// Preset: All without last except LastPostLateUpdate.
        /// </summary>
        Standard =
            Initialization |
            EarlyUpdate |
            FixedUpdate |
            PreUpdate |
            Update |
            PreLateUpdate |
            PostLateUpdate | LastPostLateUpdate
#if UNITY_2020_2_OR_NEWER
            | TimeUpdate
#endif
            ,

        /// <summary>
        /// Preset: Minimum pattern, Update | FixedUpdate | LastPostLateUpdate
        /// </summary>
        Minimum =
            Update | FixedUpdate | LastPostLateUpdate,

        // PlayerLoopTiming

        Initialization = 1,
        LastInitialization = 2,

        EarlyUpdate = 4,
        LastEarlyUpdate = 8,

        FixedUpdate = 16,
        LastFixedUpdate = 32,

        PreUpdate = 64,
        LastPreUpdate = 128,

        Update = 256,
        LastUpdate = 512,

        PreLateUpdate = 1024,
        LastPreLateUpdate = 2048,

        PostLateUpdate = 4096,
        LastPostLateUpdate = 8192

#if UNITY_2020_2_OR_NEWER
        ,
        // Unity 2020.2 added TimeUpdate https://docs.unity3d.com/2020.2/Documentation/ScriptReference/PlayerLoop.TimeUpdate.html
        TimeUpdate = 16384,
        LastTimeUpdate = 32768
#endif
    }

    public interface IPlayerLoopItem
    {
        bool MoveNext();
    }

    public static class PlayerLoopHelper
    {
        static readonly ContinuationQueue ThrowMarkerContinuationQueue = new ContinuationQueue(PlayerLoopTiming.Initialization);
        static readonly PlayerLoopRunner ThrowMarkerPlayerLoopRunner = new PlayerLoopRunner(PlayerLoopTiming.Initialization);

        public static SynchronizationContext UnitySynchronizationContext => unitySynchronizationContext;
        public static int MainThreadId => mainThreadId;
        internal static string ApplicationDataPath => applicationDataPath;

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;

        static int mainThreadId;
        static string applicationDataPath;
        static SynchronizationContext unitySynchronizationContext;
        static ContinuationQueue[] yielders;
        static PlayerLoopRunner[] runners;
        internal static bool IsEditorApplicationQuitting { get; private set; }
        static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem loopSystem,
            bool injectOnFirst,
            Type loopRunnerYieldType, ContinuationQueue cq,
            Type loopRunnerType, PlayerLoopRunner runner)
        {

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += (state) =>
            {
                if (state == PlayModeStateChange.EnteredEditMode || state == PlayModeStateChange.ExitingEditMode)
                {
                    IsEditorApplicationQuitting = true;
                    // run rest action before clear.
                    if (runner != null)
                    {
                        runner.Run();
                        runner.Clear();
                    }
                    if (cq != null)
                    {
                        cq.Run();
                        cq.Clear();
                    }
                    IsEditorApplicationQuitting = false;
                }
            };
#endif

            var yieldLoop = new PlayerLoopSystem
            {
                type = loopRunnerYieldType,
                updateDelegate = cq.Run
            };

            var runnerLoop = new PlayerLoopSystem
            {
                type = loopRunnerType,
                updateDelegate = runner.Run
            };

            // Remove items from previous initializations.
            var source = RemoveRunner(loopSystem, loopRunnerYieldType, loopRunnerType);
            var dest = new PlayerLoopSystem[source.Length + 2];

            Array.Copy(source, 0, dest, injectOnFirst ? 2 : 0, source.Length);
            if (injectOnFirst)
            {
                dest[0] = yieldLoop;
                dest[1] = runnerLoop;
            }
            else
            {
                dest[dest.Length - 2] = yieldLoop;
                dest[dest.Length - 1] = runnerLoop;
            }

            return dest;
        }

        static PlayerLoopSystem[] RemoveRunner(PlayerLoopSystem loopSystem, Type loopRunnerYieldType, Type loopRunnerType)
        {
            return loopSystem.subSystemList
                .Where(ls => ls.type != loopRunnerYieldType && ls.type != loopRunnerType)
                .ToArray();
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
            unitySynchronizationContext = SynchronizationContext.Current;
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

        private static int FindLoopSystemIndex(PlayerLoopSystem[] playerLoopList, Type systemType)
        {
            for (int i = 0; i < playerLoopList.Length; i++)
            {
                if (playerLoopList[i].type == systemType)
                {
                    return i;
                }
            }

            throw new Exception("Target PlayerLoopSystem does not found. Type:" + systemType.FullName);
        }

        static void InsertLoop(PlayerLoopSystem[] copyList, InjectPlayerLoopTimings injectTimings, Type loopType, InjectPlayerLoopTimings targetTimings,
            int index, bool injectOnFirst, Type loopRunnerYieldType, Type loopRunnerType, PlayerLoopTiming playerLoopTiming)
        {
            var i = FindLoopSystemIndex(copyList, loopType);
            if ((injectTimings & targetTimings) == targetTimings)
            {
                copyList[i].subSystemList = InsertRunner(copyList[i], injectOnFirst,
                    loopRunnerYieldType, yielders[index] = new ContinuationQueue(playerLoopTiming),
                    loopRunnerType, runners[index] = new PlayerLoopRunner(playerLoopTiming));
            }
            else
            {
                copyList[i].subSystemList = RemoveRunner(copyList[i], loopRunnerYieldType, loopRunnerType);
            }
        }

        public static void Initialize(ref PlayerLoopSystem playerLoop, InjectPlayerLoopTimings injectTimings = InjectPlayerLoopTimings.All)
        {
#if UNITY_2020_2_OR_NEWER
            yielders = new ContinuationQueue[16];
            runners = new PlayerLoopRunner[16];
#else
            yielders = new ContinuationQueue[14];
            runners = new PlayerLoopRunner[14];
#endif

            var copyList = playerLoop.subSystemList.ToArray();

            // Initialization
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.Initialization),
                InjectPlayerLoopTimings.Initialization, 0, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldInitialization), typeof(UniTaskLoopRunners.UniTaskLoopRunnerInitialization), PlayerLoopTiming.Initialization);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.Initialization),
                InjectPlayerLoopTimings.LastInitialization, 1, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldInitialization), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastInitialization), PlayerLoopTiming.LastInitialization);

            // EarlyUpdate
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.EarlyUpdate),
                InjectPlayerLoopTimings.EarlyUpdate, 2, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldEarlyUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerEarlyUpdate), PlayerLoopTiming.EarlyUpdate);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.EarlyUpdate),
                InjectPlayerLoopTimings.LastEarlyUpdate, 3, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldEarlyUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastEarlyUpdate), PlayerLoopTiming.LastEarlyUpdate);

            // FixedUpdate
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.FixedUpdate),
                InjectPlayerLoopTimings.FixedUpdate, 4, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldFixedUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerFixedUpdate), PlayerLoopTiming.FixedUpdate);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.FixedUpdate),
                InjectPlayerLoopTimings.LastFixedUpdate, 5, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldFixedUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastFixedUpdate), PlayerLoopTiming.LastFixedUpdate);

            // PreUpdate
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.PreUpdate),
                InjectPlayerLoopTimings.PreUpdate, 6, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreUpdate), PlayerLoopTiming.PreUpdate);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.PreUpdate),
                InjectPlayerLoopTimings.LastPreUpdate, 7, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreUpdate), PlayerLoopTiming.LastPreUpdate);

            // Update
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.Update),
                InjectPlayerLoopTimings.Update, 8, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerUpdate), PlayerLoopTiming.Update);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.Update),
                InjectPlayerLoopTimings.LastUpdate, 9, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastUpdate), PlayerLoopTiming.LastUpdate);

            // PreLateUpdate
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.PreLateUpdate),
                InjectPlayerLoopTimings.PreLateUpdate, 10, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPreLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPreLateUpdate), PlayerLoopTiming.PreLateUpdate);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.PreLateUpdate),
                InjectPlayerLoopTimings.LastPreLateUpdate, 11, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPreLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPreLateUpdate), PlayerLoopTiming.LastPreLateUpdate);

            // PostLateUpdate
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.PostLateUpdate),
                InjectPlayerLoopTimings.PostLateUpdate, 12, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldPostLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerPostLateUpdate), PlayerLoopTiming.PostLateUpdate);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.PostLateUpdate),
                InjectPlayerLoopTimings.LastPostLateUpdate, 13, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldPostLateUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastPostLateUpdate), PlayerLoopTiming.LastPostLateUpdate);

#if UNITY_2020_2_OR_NEWER
            // TimeUpdate
            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.TimeUpdate),
                InjectPlayerLoopTimings.TimeUpdate, 14, true,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerYieldTimeUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerTimeUpdate), PlayerLoopTiming.TimeUpdate);

            InsertLoop(copyList, injectTimings, typeof(PlayerLoopType.TimeUpdate),
                InjectPlayerLoopTimings.LastTimeUpdate, 15, false,
                typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastYieldTimeUpdate), typeof(UniTaskLoopRunners.UniTaskLoopRunnerLastTimeUpdate), PlayerLoopTiming.LastTimeUpdate);
#endif

            // Insert UniTaskSynchronizationContext to Update loop
            var i = FindLoopSystemIndex(copyList, typeof(PlayerLoopType.Update));
            copyList[i].subSystemList = InsertUniTaskSynchronizationContext(copyList[i]);

            playerLoop.subSystemList = copyList;
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
        {
            var runner = runners[(int)timing];
            if (runner == null)
            {
                ThrowInvalidLoopTiming(timing);
            }
            runner.AddAction(action);
        }

        static void ThrowInvalidLoopTiming(PlayerLoopTiming playerLoopTiming)
        {
            throw new InvalidOperationException("Target playerLoopTiming is not injected. Please check PlayerLoopHelper.Initialize. PlayerLoopTiming:" + playerLoopTiming);
        }

        public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
        {
            var q = yielders[(int)timing];
            if (q == null)
            {
                ThrowInvalidLoopTiming(timing);
            }
            q.Enqueue(continuation);
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

