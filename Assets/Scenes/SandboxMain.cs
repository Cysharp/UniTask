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

    async void Start()
    {
        UnityEngine.Debug.Log("DOWNLOAD START:" + Time.frameCount);

        var req = await UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "test.txt")).SendWebRequest();

        UnityEngine.Debug.Log("DOWNLOAD RESULT:" + Time.frameCount + ", " + req.downloadHandler.text);
    }
}

