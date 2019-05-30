using System;
using System.Collections;
using System.Collections.Generic;
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

    async void Start()
    {
        await UniTask.CompletedTask;  // ok

        // var subject = new Subject<Unit>();
        //subject.OnCompleted();
        IObservable<AsyncUnit> subject = default;
        try
        {
            await subject.ToUniTask();  // exception
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }
}

