using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;

public class SandboxMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RunAsync().Forget();
    }

    async UniTaskVoid RunAsync()
    {
        var id = await UniTask.Run(() => System.Threading.Thread.CurrentThread.ManagedThreadId, configureAwait: false);
        UnityEngine.Debug.Log("ReturnId:" + id);
        UnityEngine.Debug.Log("CurrentId:" + System.Threading.Thread.CurrentThread.ManagedThreadId);
    }
}
