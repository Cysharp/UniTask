#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UniRx.Async.Editor
{
    // reflection call of UnityEditor.SplitterGUILayout
    internal static class SplitterGUILayout
    {
        static BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        static Lazy<Type> splitterStateType = new Lazy<Type>(() =>
        {
            var type = typeof(EditorWindow).Assembly.GetTypes().First(x => x.FullName == "UnityEditor.SplitterState");
            return type;
        });

        static Lazy<ConstructorInfo> splitterStateCtor = new Lazy<ConstructorInfo>(() =>
        {
            var type = splitterStateType.Value;
            return type.GetConstructor(flags, null, new Type[] { typeof(float[]), typeof(int[]), typeof(int[]) }, null);
        });

        static Lazy<Type> splitterGUILayoutType = new Lazy<Type>(() =>
        {
            var type = typeof(EditorWindow).Assembly.GetTypes().First(x => x.FullName == "UnityEditor.SplitterGUILayout");
            return type;
        });

        static Lazy<MethodInfo> beginVerticalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = splitterGUILayoutType.Value;
            return type.GetMethod("BeginVerticalSplit", flags, null, new Type[] { splitterStateType.Value, typeof(GUILayoutOption[]) }, null);
        });

        static Lazy<MethodInfo> endVerticalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = splitterGUILayoutType.Value;
            return type.GetMethod("EndVerticalSplit", flags, null, Type.EmptyTypes, null);
        });

        public static object CreateSplitterState(float[] relativeSizes, int[] minSizes, int[] maxSizes)
        {
            return splitterStateCtor.Value.Invoke(new object[] { relativeSizes, minSizes, maxSizes });
        }

        public static void BeginVerticalSplit(object splitterState, params GUILayoutOption[] options)
        {
            beginVerticalSplit.Value.Invoke(null, new object[] { splitterState, options });
        }

        public static void EndVerticalSplit()
        {
            endVerticalSplit.Value.Invoke(null, Type.EmptyTypes);
        }
    }
}

#endif