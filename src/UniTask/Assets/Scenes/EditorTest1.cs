#if UNITY_EDITOR

using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Test1
{
    [MenuItem("Test/Test1")]
    public static async UniTaskVoid TestFunc()
    {
        await DoSomeThing();
        //string[] scenes = new string[]
        //{
        //    "Assets/Scenes/SandboxMain.unity",
        //};

        //try
        //{
        //    Debug.Log("Build Begin");
        //    BuildPipeline.BuildPlayer(scenes, Application.dataPath + "../target", BuildTarget.StandaloneWindows, BuildOptions.CompressWithLz4);
        //    Debug.Log("Build After");
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError(e.Message);
        //}
    }

    public static async UniTask DoSomeThing()
    {
        Debug.Log("Dosomething");
        await UniTask.Delay(1500, DelayType.DeltaTime);
        Debug.Log("Dosomething 2");
        await UniTask.Delay(1000, DelayType.DeltaTime);
        Debug.Log("Dosomething 3");
        Debug.Log("and Quit.");

        Environment.Exit(0);
    }
}

#endif