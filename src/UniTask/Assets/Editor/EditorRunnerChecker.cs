#if UNITY_EDITOR

using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class EditorRunnerChecker
{
    [MenuItem("Tools/UniTaskEditorRunnerChecker")]
    public static void RunUniTaskAsync()
    {
        RunCore().Forget();
    }

    static async UniTaskVoid RunCore()
    {
        Debug.Log("Start, Wait 5 seconds. deltaTime?" + Time.deltaTime);

        await UniTask.Delay(TimeSpan.FromSeconds(5));

        Debug.Log("End, Wait 5 seconds. deltaTime?" + Time.deltaTime);
    }
}

#endif