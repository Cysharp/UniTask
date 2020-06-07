// Provided from: https://github.com/Cysharp/UniTask/issues/40

using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Example script for comparing how exceptions in unobserved tasks are handled between
/// UniTask and normal tasks. This helps in verifying that unobserved exceptions are
/// logged in a way that it useful to developers.
/// </summary>
public class ExceptionExamples : MonoBehaviour
{
    [SerializeField] private LogType _unobservedExceptionLogType = LogType.Exception;

    private void Awake()
    {
        UniTaskScheduler.UnobservedExceptionWriteLogType = _unobservedExceptionLogType;
    }

    private void Start()
    {
        UnityEngine.Debug.Log("ExceptionScene, LoopType:" + PlayerLoopInfo.CurrentLoopType + ":" + Time.frameCount);

        //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        //ThrowFromAsyncVoid();
        //_ = ThrowFromTask();
        //_ = ThrowFromUniTask();

        //ThrowFromNonAsync();
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        UnityEngine.Debug.LogException(e.Exception);
    }

    private void ThrowFromNonAsync()
    {
        throw new Exception("Thrown from non-async function");
    }

    private async void ThrowFromAsyncVoid()
    {
        await ThrowInner();

        async Task ThrowInner()
        {
            await UniTask.Yield();
            throw new Exception("Thrown from `async void` function");
        }
    }

    private async Task ThrowFromTask()
    {
        await ThrowInner();

        async Task ThrowInner()
        {
            await UniTask.Yield();
            throw new Exception("Thrown from `async Task` function");
        }
    }

    private async UniTask ThrowFromUniTask()
    {
        await ThrowInner();

        async UniTask ThrowInner()
        {
            await UniTask.Yield();
            throw new Exception("Thrown from `async UniTask` function");
        }
    }
}