#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    // public for add user custom.

    public static class TaskTracker
    {
#if UNITY_EDITOR

        static int trackingId = 0;

        public const string EnableAutoReloadKey = "UniTaskTrackerWindow_EnableAutoReloadKey";
        public const string EnableTrackingKey = "UniTaskTrackerWindow_EnableTrackingKey";
        public const string EnableStackTraceKey = "UniTaskTrackerWindow_EnableStackTraceKey";

        public static class EditorEnableState
        {
            static bool enableAutoReload;
            public static bool EnableAutoReload
            {
                get { return enableAutoReload; }
                set
                {
                    enableAutoReload = value;
                    UnityEditor.EditorPrefs.SetBool(EnableAutoReloadKey, value);
                }
            }

            static bool enableTracking;
            public static bool EnableTracking
            {
                get { return enableTracking; }
                set
                {
                    enableTracking = value;
                    UnityEditor.EditorPrefs.SetBool(EnableTrackingKey, value);
                }
            }

            static bool enableStackTrace;
            public static bool EnableStackTrace
            {
                get { return enableStackTrace; }
                set
                {
                    enableStackTrace = value;
                    UnityEditor.EditorPrefs.SetBool(EnableStackTraceKey, value);
                }
            }
        }

#endif


        static List<KeyValuePair<IUniTaskSource, (string formattedType, int trackingId, DateTime addTime, string stackTrace)>> listPool = new List<KeyValuePair<IUniTaskSource, (string formattedType, int trackingId, DateTime addTime, string stackTrace)>>();

        static readonly WeakDictionary<IUniTaskSource, (string formattedType, int trackingId, DateTime addTime, string stackTrace)> tracking = new WeakDictionary<IUniTaskSource, (string formattedType, int trackingId, DateTime addTime, string stackTrace)>();

        [Conditional("UNITY_EDITOR")]
        public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
        {
#if UNITY_EDITOR
            dirty = true;
            if (!EditorEnableState.EnableTracking) return;
            var stackTrace = EditorEnableState.EnableStackTrace ? new StackTrace(skipFrame, true).CleanupAsyncStackTrace() : "";

            string typeName;
            if (EditorEnableState.EnableStackTrace)
            {
                var sb = new StringBuilder();
                TypeBeautify(task.GetType(), sb);
                typeName = sb.ToString();
            }
            else
            {
                typeName = task.GetType().Name;
            }
            tracking.TryAdd(task, (typeName, Interlocked.Increment(ref trackingId), DateTime.UtcNow, stackTrace));
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void RemoveTracking(IUniTaskSource task)
        {
#if UNITY_EDITOR
            dirty = true;
            if (!EditorEnableState.EnableTracking) return;
            var success = tracking.TryRemove(task);
#endif
        }

        static bool dirty;

        public static bool CheckAndResetDirty()
        {
            var current = dirty;
            dirty = false;
            return current;
        }

        /// <summary>(trackingId, awaiterType, awaiterStatus, createdTime, stackTrace)</summary>
        public static void ForEachActiveTask(Action<int, string, UniTaskStatus, DateTime, string> action)
        {
            lock (listPool)
            {
                var count = tracking.ToList(ref listPool, clear: false);
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        action(listPool[i].Value.trackingId, listPool[i].Value.formattedType, listPool[i].Key.UnsafeGetStatus(), listPool[i].Value.addTime, listPool[i].Value.stackTrace);
                        listPool[i] = default;
                    }
                }
                catch
                {
                    listPool.Clear();
                    throw;
                }
            }
        }

        static void TypeBeautify(Type type, StringBuilder sb)
        {
            if (type.IsNested)
            {
                // TypeBeautify(type.DeclaringType, sb);
                sb.Append(type.DeclaringType.Name.ToString());
                sb.Append(".");
            }

            if (type.IsGenericType)
            {
                var genericsStart = type.Name.IndexOf("`");
                if (genericsStart != -1)
                {
                    sb.Append(type.Name.Substring(0, genericsStart));
                }
                else
                {
                    sb.Append(type.Name);
                }
                sb.Append("<");
                var first = true;
                foreach (var item in type.GetGenericArguments())
                {
                    if (!first)
                    {
                        sb.Append(", ");
                    }
                    first = false;
                    TypeBeautify(item, sb);
                }
                sb.Append(">");
            }
            else
            {
                sb.Append(type.Name);
            }
        }

        //static string RemoveUniTaskNamespace(string str)
        //{
        //    return str.Replace("Cysharp.Threading.Tasks.CompilerServices", "")
        //        .Replace("Cysharp.Threading.Tasks.Linq", "")
        //        .Replace("Cysharp.Threading.Tasks", "");
        //}
    }
}

