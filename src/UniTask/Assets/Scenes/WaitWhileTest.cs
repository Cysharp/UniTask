using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// https://github.com/Cysharp/UniTask/issues/617

public class WaitWhileTest : MonoBehaviour
{
    private const float c_CallInterval = 0.3f;
    private float m_JustBeforeCallTime;

    private TaskObj m_TestObj;

    // Start is called before the first frame update
    void Start()
    {
        m_JustBeforeCallTime = Time.unscaledTime;
        m_TestObj = new TaskObj();
        // m_TestObj.Test(CancellationToken.None).Forget();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime - m_JustBeforeCallTime > c_CallInterval)
        {
            m_JustBeforeCallTime = Time.unscaledTime;
            m_TestObj.Test(CancellationToken.None).Forget();
        }
    }
}


public class TaskObj
{
    private CancellationTokenSource m_CancelTokenSource;
    private const float c_FinishElapsedTime = 0.1f;
    private float m_StartTime;
    public async UniTask Test(CancellationToken token)
    {
        try
        {
            CancelAndDisposeTokenSource();
            m_CancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            m_StartTime = Time.unscaledTime;
            await UniTask.WaitWhile(IsContinued, cancellationToken: m_CancelTokenSource.Token, cancelImmediately: true);
            Debug.Log("Task Finished");
        }
        catch (OperationCanceledException)
        {
            Debug.LogWarning("Task Canceled");
        }
        finally
        {
            CancelAndDisposeTokenSource();
        }
    }

    private void CancelAndDisposeTokenSource()
    {
        m_CancelTokenSource?.Cancel();
        m_CancelTokenSource?.Dispose();
        m_CancelTokenSource = null;
    }

    private bool IsContinued()
    {
        return Time.unscaledTime - m_StartTime > c_FinishElapsedTime;
    }
}

