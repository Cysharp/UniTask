using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SandboxMain : MonoBehaviour
{
    public Button okButton;
    public Button cancelButton;
    public Text text;

    CancellationTokenSource cts;

    UniTaskCompletionSource ucs;

    void Start()
    {
        // Setup unobserverd tskexception handling
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        // Optional: disable ExecutionContext if you don't use AsyncLocal.
        //if (!ExecutionContext.IsFlowSuppressed())
        //{
        //    ExecutionContext.SuppressFlow();
        //}

        //// Optional: disable SynchronizationContext(to boostup performance) if you completely use UniTask only
        //SynchronizationContext.SetSynchronizationContext(null);

        // -----

        Application.logMessageReceived += Application_logMessageReceived;


        ucs = new UniTaskCompletionSource();

        okButton.onClick.AddListener(async () =>
        {
            await InnerAsync(false);
        });

        cancelButton.onClick.AddListener(async () =>
        {
            text.text = "";

            ucs.TrySetResult();

            await ucs.Task;
        });
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        text.text += "\n" + condition;
    }

    async UniTask OuterAsync(bool b)
    {
        UnityEngine.Debug.Log("START OUTER");

        await InnerAsync(b);
        await InnerAsync(b);

        UnityEngine.Debug.Log("END OUTER");

        // throw new InvalidOperationException("NAZO ERROR!?"); // error!?
    }

    async UniTask InnerAsync(bool b)
    {
        if (b)
        {
            UnityEngine.Debug.Log("Start delay:" + Time.frameCount);
            await UniTask.DelayFrame(60);
            UnityEngine.Debug.Log("End delay:" + Time.frameCount);
            await UniTask.DelayFrame(60);
            UnityEngine.Debug.Log("Onemore end delay:" + Time.frameCount);
        }
        else
        {
            UnityEngine.Debug.Log("Empty END");
            throw new InvalidOperationException("FOOBARBAZ");
        }
    }


























    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        // e.SetObserved();
        // or other custom write code.
        UnityEngine.Debug.LogError("Unobserved:" + e.Exception.ToString());
    }
}

public class ShowPlayerLoop
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        var playerLoop = UnityEngine.LowLevel.PlayerLoop.GetDefaultPlayerLoop();

        var sb = new StringBuilder();
        sb.AppendLine("Default Playerloop List");
        foreach (var header in playerLoop.subSystemList)
        {
            sb.AppendFormat("------{0}------", header.type.Name);
            sb.AppendLine();
            foreach (var subSystem in header.subSystemList)
            {
                sb.AppendFormat("{0}.{1}", header.type.Name, subSystem.type.Name);
                sb.AppendLine();
            }
        }

        UnityEngine.Debug.Log(sb.ToString());
    }
}