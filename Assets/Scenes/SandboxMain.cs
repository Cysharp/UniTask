using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SandboxMain : MonoBehaviour
{
    public Button okButton;
    public Button cancelButton;
    CancellationTokenSource cts;

    void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            FooAsync().Forget();
        });
        cancelButton.onClick.AddListener(() =>
        {
            BarAsync().Forget();
        });
    }

    async UniTask<int> FooAsync()
    {
        // use F10, will crash.
        var loop = int.Parse("9");
        await UniTask.DelayFrame(loop);

        Debug.Log("OK");
        await UniTask.DelayFrame(loop);

        Debug.Log("Again");

        return 10;
    }

    async UniTaskVoid BarAsync()
    {
        var loop = int.Parse("10");


        var foo = await UniTask.FromResult(100);

        Debug.Log("OK");


        Debug.Log("Again");


    }


}

