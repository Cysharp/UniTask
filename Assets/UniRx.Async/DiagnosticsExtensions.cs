#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniRx.Async
{
    public static class DiagnosticsExtensions
    {
        static bool displayFilenames = true;

        static readonly Regex typeBeautifyRegex = new Regex("`.+$", RegexOptions.Compiled);

        static readonly Dictionary<Type, string> builtInTypeNames = new Dictionary<Type, string>
        {
            { typeof(void), "void" },
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(ushort), "ushort" },
            { typeof(Task), "Task" },
            { typeof(UniTask), "UniTask" },
            { typeof(UniTaskVoid), "UniTaskVoid" }
        };

        public static string ToStringWithCleanupAsyncStackTrace(this Exception exception)
        {
            if (exception == null) return "";

            String message = exception.Message;
            String s;

            if (message == null || message.Length <= 0)
            {
                s = exception.GetType().ToString();
            }
            else
            {
                s = exception.GetType().ToString() + ": " + message;
            }

            if (exception.InnerException != null)
            {
                s = s + " ---> " + exception.InnerException.ToString() + Environment.NewLine + "   Exception_EndOfInnerExceptionStack";
            }

            string stackTrace = new StackTrace(exception).CleanupAsyncStackTrace();
            if (stackTrace != null)
            {
                s += Environment.NewLine + stackTrace;
            }

            return s;
        }

        public static string CleanupAsyncStackTrace(this StackTrace stackTrace)
        {
            if (stackTrace == null) return "";

            var sb = new StringBuilder();
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var sf = stackTrace.GetFrame(i);

                var mb = sf.GetMethod();

                if (IgnoreLine(mb)) continue;
                if (IsAsync(mb))
                {
                    sb.Append("async ");
                    TryResolveStateMachineMethod(ref mb, out var decType);
                }

                // return type
                if (mb is MethodInfo mi)
                {
                    sb.Append(BeautifyType(mi.ReturnType, false));
                    sb.Append(" ");
                }

                // method name
                sb.Append(BeautifyType(mb.DeclaringType, false));
                if (!mb.IsConstructor)
                {
                    sb.Append(".");
                }
                sb.Append(mb.Name);
                if (mb.IsGenericMethod)
                {
                    sb.Append("<");
                    foreach (var item in mb.GetGenericArguments())
                    {
                        sb.Append(BeautifyType(item, true));
                    }
                    sb.Append(">");
                }

                // parameter
                sb.Append("(");
                sb.Append(string.Join(", ", mb.GetParameters().Select(p => BeautifyType(p.ParameterType, true) + " " + p.Name)));
                sb.Append(")");

                // file name
                if (displayFilenames && (sf.GetILOffset() != -1))
                {
                    String fileName = null;

                    try
                    {
                        fileName = sf.GetFileName();
                    }
                    catch (NotSupportedException)
                    {
                        displayFilenames = false;
                    }
                    catch (SecurityException)
                    {
                        displayFilenames = false;
                    }

                    if (fileName != null)
                    {
                        sb.Append(' ');
                        sb.AppendFormat(CultureInfo.InvariantCulture, "in {0}:{1}", SimplifyPath(fileName), sf.GetFileLineNumber());
                    }
                }

                sb.AppendLine();
            }
            return sb.ToString();
        }


        static bool IsAsync(MethodBase methodInfo)
        {
            var declareType = methodInfo.DeclaringType;
            return typeof(IAsyncStateMachine).IsAssignableFrom(declareType);
        }

        // code from Ben.Demystifier/EnhancedStackTrace.Frame.cs
        static bool TryResolveStateMachineMethod(ref MethodBase method, out Type declaringType)
        {
            declaringType = method.DeclaringType;

            var parentType = declaringType.DeclaringType;
            if (parentType == null)
            {
                return false;
            }

            var methods = parentType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (methods == null)
            {
                return false;
            }

            foreach (var candidateMethod in methods)
            {
                var attributes = candidateMethod.GetCustomAttributes<StateMachineAttribute>();
                if (attributes == null)
                {
                    continue;
                }

                foreach (var asma in attributes)
                {
                    if (asma.StateMachineType == declaringType)
                    {
                        method = candidateMethod;
                        declaringType = candidateMethod.DeclaringType;
                        // Mark the iterator as changed; so it gets the + annotation of the original method
                        // async statemachines resolve directly to their builder methods so aren't marked as changed
                        return asma is IteratorStateMachineAttribute;
                    }
                }
            }

            return false;
        }

        static string BeautifyType(Type t, bool shortName)
        {
            if (builtInTypeNames.TryGetValue(t, out var builtin))
            {
                return builtin;
            }
            if (t.IsGenericParameter) return t.Name;
            if (t.IsArray) return BeautifyType(t.GetElementType(), shortName) + "[]";
            if (t.FullName?.StartsWith("System.ValueTuple") ?? false)
            {
                return "(" + string.Join(", ", t.GetGenericArguments().Select(x => BeautifyType(x, true))) + ")";
            }
            if (!t.IsGenericType) return shortName ? t.Name : t.FullName ?? t.Name;

            var innerFormat = string.Join(", ", t.GetGenericArguments().Select(x => BeautifyType(x, true)));

            var genericType = t.GetGenericTypeDefinition().FullName;
            if (genericType == "System.Threading.Tasks.Task`1")
            {
                genericType = "Task";
            }

            return typeBeautifyRegex.Replace(genericType, "") + "<" + innerFormat + ">";
        }

        static bool IgnoreLine(MethodBase methodInfo)
        {
            var declareType = methodInfo.DeclaringType.FullName;
            if (declareType == "System.Threading.ExecutionContext")
            {
                return true;
            }
            else if (declareType.StartsWith("System.Runtime.CompilerServices"))
            {
                return true;
            }
            else if (declareType.StartsWith("UniRx.Async.CompilerServices"))
            {
                return true;
            }
            else if (declareType == "System.Threading.Tasks.AwaitTaskContinuation")
            {
                return true;
            }
            else if (declareType.StartsWith("System.Threading.Tasks.Task"))
            {
                return true;
            }

            return false;
        }

        static string SimplifyPath(string path)
        {
            var fi = new FileInfo(path);
            if (fi.Directory == null)
            {
                return fi.Name;
            }
            else
            {
                return fi.Directory.Name + "/" + fi.Name;
            }
        }
    }
}

#endif