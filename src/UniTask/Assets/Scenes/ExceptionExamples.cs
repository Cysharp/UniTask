using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AddressableAssets;

/*UNniTastWhenAnyTester*/

[ExecuteInEditMode]
public class ExceptionExamples : MonoBehaviour
{
    public bool apply = false;

    private async UniTaskVoid Update()
    {
        if (apply)
        {
            apply = false;
            await LaunchTasksAndDetectWhenAnyDone(5);
        }
    }

    private async UniTask LaunchTasksAndDetectWhenAnyDone(int nbTasks)
    {
        List<UniTask<int>> sleeptasks = new List<UniTask<int>>();
        for (int i = 0; i < nbTasks; i++)
        {
            sleeptasks.Add(SleepAndReturnTrue(i).ToAsyncLazy().Task);
        }
        while (sleeptasks.Count > 0)
        {
            Debug.Log(DateTime.Now.ToString() + " waiting for " + sleeptasks.Count + " tasks...");
            try
            {
                (int index, int taskID) = await UniTask.WhenAny(sleeptasks);
                Debug.Log(DateTime.Now.ToString() + " Sleep task " + taskID + " done");
                sleeptasks.RemoveAt(index);
            }
            catch
            {
                throw;
                //Debug.Log("Error: " + e.Message);
                //return;
            }
        }
    }

    private async UniTask<int> SleepAndReturnTrue(int taskIndex)
    {
        await UniTask.Delay(100);
        return taskIndex;
    }

    //void AddressablesTest()
    //{
    //    Addressables.ClearDependencyCacheAsync("key", true);
    //}
}