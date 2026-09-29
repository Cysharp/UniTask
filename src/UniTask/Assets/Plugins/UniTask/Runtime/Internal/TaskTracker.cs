#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        /// <summary>
        /// One tracked async operation: an async method (state machine) or a promise source such as Delay or WhenAll.
        /// Parent is the async method that was executing when this operation was created, so the chain of parents is
        /// the async call stack that a Debug.Log stack trace loses after the first await. EnableTracking alone keeps
        /// the chain; EnableStackTrace adds the await and call-site lines plus a full stack for root operations
        /// (those started from synchronous code): a few StackFrame lookups per operation instead of a full StackTrace.
        /// </summary>
        public sealed class AsyncOrigin
        {
            internal AsyncOrigin(AsyncOrigin parent, Type stateMachineType)
            {
                Parent = parent;
                StateMachineType = stateMachineType;
            }

            public AsyncOrigin Parent { get; }

            /// <summary>Compiler-generated state machine type; null for promise sources (Delay, WhenAll, triggers...).</summary>
            public Type StateMachineType { get; }

            /// <summary>Runner or promise type. Null while an async method is still in its synchronous part and has not suspended.</summary>
            public Type TaskType { get; internal set; }

            public int TrackingId { get; internal set; }
            public DateTime CreatedAt { get; internal set; }

            /// <summary>Frame of this operation's method at the await where it first suspended. Null without EnableStackTrace.</summary>
            public StackFrame AwaitFrame { get; internal set; }

            /// <summary>Frame of the parent at the call that started this operation. Null for roots or without EnableStackTrace.</summary>
            public StackFrame CallSiteFrame { get; internal set; }

            internal StackTrace rootStackTrace;

            string methodName;
            string taskTypeName;
            string rootStackTraceText;
            string chainText;

            public bool IsTracked => TaskType != null;
            public bool IsRoot => Parent == null;

            /// <summary>"Namespace.Type.Method" for async methods; the beautified task type for promise sources.</summary>
            public string MethodName
            {
                get
                {
                    if (methodName == null)
                    {
                        methodName = ResolveMethodName();
                    }
                    return methodName;
                }
            }

            public string TaskTypeName
            {
                get
                {
                    if (taskTypeName == null)
                    {
                        if (TaskType == null) return MethodName;
                        var sb = new StringBuilder();
                        TypeBeautify(TaskType, sb);
                        taskTypeName = sb.ToString();
                    }
                    return taskTypeName;
                }
            }

            /// <summary>Full stack at creation, captured only for roots with EnableStackTrace. Formatted on first access.</summary>
            public string RootStackTrace
            {
                get
                {
                    if (rootStackTraceText == null)
                    {
                        rootStackTraceText = rootStackTrace == null ? "" : rootStackTrace.CleanupAsyncStackTrace();
                    }
                    return rootStackTraceText;
                }
            }

            /// <summary>This operation with its await line, each ancestor with the line that started its child, then the root stack.</summary>
            public string FormatChain()
            {
                if (chainText == null)
                {
                    var sb = new StringBuilder();
                    AppendChain(sb);
                    chainText = sb.ToString();
                }
                return chainText;
            }

            public void AppendChain(StringBuilder sb)
            {
                sb.Append("async ").Append(MethodName);
                AppendLocation(sb, AwaitFrame);
                sb.AppendLine();

                var child = this;
                for (var node = Parent; node != null; node = node.Parent)
                {
                    sb.Append("async ").Append(node.MethodName);
                    AppendLocation(sb, child.CallSiteFrame);
                    sb.AppendLine();
                    child = node;
                }

                if (child.rootStackTrace != null)
                {
                    sb.Append(child.RootStackTrace);
                }
            }

            string ResolveMethodName()
            {
                if (StateMachineType == null)
                {
                    return TaskType == null ? "?" : TaskTypeName;
                }

                var definition = StateMachineType.IsGenericType ? StateMachineType.GetGenericTypeDefinition() : StateMachineType;
                var parentType = StateMachineType.DeclaringType;
                if (parentType != null)
                {
                    var methods = parentType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    foreach (var candidate in methods)
                    {
                        foreach (var attribute in candidate.GetCustomAttributes<StateMachineAttribute>(false))
                        {
                            if (attribute.StateMachineType == definition)
                            {
                                var declaring = candidate.DeclaringType;
                                return (declaring?.FullName ?? declaring?.Name ?? "") + "." + candidate.Name;
                            }
                        }
                    }
                }

                return StateMachineType.FullName ?? StateMachineType.Name;
            }

            static void AppendLocation(StringBuilder sb, StackFrame frame)
            {
                sb.Append(FormatLocation(frame));
            }

            /// <summary>" (at Assets/...:line)" with a console hyperlink, or an empty string when the frame has no file info.</summary>
            public static string FormatLocation(StackFrame frame)
            {
                if (frame == null) return "";

                string fileName;
                try
                {
                    fileName = frame.GetFileName();
                }
                catch (NotSupportedException)
                {
                    return "";
                }
                catch (System.Security.SecurityException)
                {
                    return "";
                }

                if (fileName == null) return "";
                return " (at " + DiagnosticsExtensions.AppendHyperLink(fileName, frame.GetFileLineNumber().ToString()) + ")";
            }
        }

        struct Entry
        {
            public AsyncOrigin Node;
            public bool Resumed;
        }

        // Async methods executing on this thread, innermost last: a state machine in its synchronous Start,
        // or a continuation resumed by the scheduler in AsyncUniTask.Run.
        [ThreadStatic] static Entry[] entries;
        [ThreadStatic] static int depth;

        /// <summary>Innermost async operation executing on this thread. Null in synchronous code.</summary>
        public static AsyncOrigin Current => depth > 0 ? entries[depth - 1].Node : null;

        /// <summary>
        /// True when the calling code runs inside a continuation resumed by the scheduler, i.e. the thread stack is cut
        /// at AsyncUniTask.Run and the callers live only in the parent chain. node is the resumed async method;
        /// it is null when that method started before tracking was enabled.
        /// </summary>
        public static bool TryGetCurrentResumed(out AsyncOrigin node)
        {
            for (var i = depth - 1; i >= 0; i--)
            {
                if (entries[i].Resumed)
                {
                    node = entries[i].Node;
                    return true;
                }
            }

            node = null;
            return false;
        }

        /// <summary>Called by the async method builders around the synchronous start of a state machine. Returns true when Exit must follow.</summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool EnterStart(Type stateMachineType)
        {
            if (!EditorEnableState.EnableTracking) return false;
            Push(new AsyncOrigin(Current, stateMachineType), false);
            return true;
        }

        /// <summary>Called by the state machine runners around a resumed continuation. Returns true when Exit must follow.</summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool EnterRun(IUniTaskSource runner)
        {
            if (!EditorEnableState.EnableTracking) return false;
            tracking.TryGetValue(runner, out var node);
            Push(node, true);
            return true;
        }

        public static void Exit()
        {
            if (depth == 0) return;
            depth--;
            entries[depth] = default;
        }

        static void Push(AsyncOrigin node, bool resumed)
        {
            if (entries == null)
            {
                entries = new Entry[64];
            }
            else if (depth == entries.Length)
            {
                Array.Resize(ref entries, entries.Length * 2);
            }

            entries[depth++] = new Entry { Node = node, Resumed = resumed };
        }

        static List<KeyValuePair<IUniTaskSource, AsyncOrigin>> listPool = new List<KeyValuePair<IUniTaskSource, AsyncOrigin>>();

        static readonly WeakDictionary<IUniTaskSource, AsyncOrigin> tracking = new WeakDictionary<IUniTaskSource, AsyncOrigin>();

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void Register(AsyncOrigin node, IUniTaskSource task, int skipFrame)
        {
            node.TaskType = task.GetType();
            node.TrackingId = Interlocked.Increment(ref trackingId);
            node.CreatedAt = DateTime.UtcNow;

            if (EditorEnableState.EnableStackTrace)
            {
                // skipFrame counts from this method to the suspending MoveNext (or to the method creating a promise).
                node.AwaitFrame = (node.StateMachineType != null ? FindFrame(skipFrame - 1, node.StateMachineType) : null)
                                  ?? new StackFrame(skipFrame, true);

                if (node.Parent == null)
                {
                    node.rootStackTrace = new StackTrace(skipFrame, true);
                }
                else if (node.Parent.StateMachineType != null)
                {
                    node.CallSiteFrame = FindFrame(skipFrame + 1, node.Parent.StateMachineType);
                }
            }

            tracking.TryAdd(task, node);
        }

        /// <summary>First frame at or above depthFromCaller (counted from the calling method) declared by the given type, within 12 frames.</summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        static StackFrame FindFrame(int depthFromCaller, Type declaringType)
        {
            for (var i = 0; i < 12; i++)
            {
                var frame = new StackFrame(depthFromCaller + 1 + i, true);
                var method = frame.GetMethod();
                if (method == null) return null;
                if (method.DeclaringType == declaringType) return frame;
            }

            return null;
        }

#endif

        /// <summary>Promise sources (Delay, WhenAll, triggers...). skipFrame points at the method creating the promise.</summary>
        [Conditional("UNITY_EDITOR")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
        {
#if UNITY_EDITOR
            dirty = true;
            if (!EditorEnableState.EnableTracking) return;
            Register(new AsyncOrigin(Current, null), task, skipFrame + 1);
#endif
        }

        /// <summary>State machine runners at the first suspension of an async method. skipFrame points at the state machine's MoveNext.</summary>
        [Conditional("UNITY_EDITOR")]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void TrackActiveStateMachine(IUniTaskSource runner, Type stateMachineType, int skipFrame)
        {
#if UNITY_EDITOR
            dirty = true;
            if (!EditorEnableState.EnableTracking) return;

            // The suspending method is the innermost one: reuse the node pushed by its synchronous Start, so that
            // operations it created before suspending already point at it as their parent.
            AsyncOrigin node = null;
            if (depth > 0)
            {
                var top = entries[depth - 1];
                if (!top.Resumed && top.Node != null && !top.Node.IsTracked && top.Node.StateMachineType == stateMachineType)
                {
                    node = top.Node;
                }
            }

            if (node == null)
            {
                node = new AsyncOrigin(Current, stateMachineType);
            }

            Register(node, runner, skipFrame + 1);
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
#if UNITY_EDITOR
            lock (listPool)
            {
                var count = tracking.ToList(ref listPool, clear: false);
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        var node = listPool[i].Value;
                        action(node.TrackingId, node.TaskTypeName, listPool[i].Key.UnsafeGetStatus(), node.CreatedAt, node.FormatChain());
                        listPool[i] = default;
                    }
                }
                catch
                {
                    listPool.Clear();
                    throw;
                }
            }
#endif
        }

#if UNITY_EDITOR

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

#endif
    }
}
