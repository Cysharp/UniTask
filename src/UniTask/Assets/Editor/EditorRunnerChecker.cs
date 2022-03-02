#if UNITY_EDITOR

using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public static class EditorRunnerChecker
{
    [MenuItem("Tools/UniTaskEditorRunnerChecker")]
    public static void RunUniTaskAsync()
    {
        RunCore().Forget();
    }

    static async UniTaskVoid RunCore()
    {
        Debug.Log("Start");

        //var r = await UnityWebRequest.Get("https://bing.com/").SendWebRequest().ToUniTask();
        //Debug.Log(r.downloadHandler.text.Substring(0, 100));
        //await UniTask.Yield();

        await UniTask.DelayFrame(30);

        Debug.Log("End");
    }
}

#endif