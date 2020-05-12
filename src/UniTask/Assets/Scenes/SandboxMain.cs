using System;

using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public struct MyJob : IJob
{
    public int loopCount;
    public NativeArray<int> inOut;
    public int result;

    public void Execute()
    {
        result = 0;
        for (int i = 0; i < loopCount; i++)
        {
            result++;
        }
        inOut[0] = result;
    }
}

public enum MyEnum
{
    A, B, C
}

public class SandboxMain : MonoBehaviour
{
    public Button okButton;
    public Button cancelButton;
    public Text text;

    CancellationTokenSource cts;

    UniTaskCompletionSource ucs;
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


    async UniTask RunStandardDelayAsync()
    {
        UnityEngine.Debug.Log("DEB");

        await UniTask.DelayFrame(30);

        UnityEngine.Debug.Log("DEB END");
    }

    async UniTask RunJobAsync()
    {
        var job = new MyJob() { loopCount = 999, inOut = new NativeArray<int>(1, Allocator.TempJob) };
        JobHandle.ScheduleBatchedJobs();

        var scheduled = job.Schedule();




        UnityEngine.Debug.Log("OK");
        await scheduled; // .ConfigureAwait(PlayerLoopTiming.Update); // .WaitAsync(PlayerLoopTiming.Update);
        UnityEngine.Debug.Log("OK2");

        job.inOut.Dispose();
    }


    async UniTaskVoid Update2()
    {

        UnityEngine.Debug.Log("async linq!");

        await UniTaskAsyncEnumerable.Range(1, 10)
            .Where(x => x % 2 == 0)
            .Select(x => x * x)
            .ForEachAsync(x =>
            {
                UnityEngine.Debug.Log(x);
            });

        UnityEngine.Debug.Log("done");


    }

    async void Start()
    {
        Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
        Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.Full);

        var playerLoop = UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
        //ShowPlayerLoop.DumpPlayerLoop("Current", playerLoop);


        //Update2().Forget();

        //RunStandardDelayAsync().Forget();

        //for (int i = 0; i < 14; i++)
        //{
        //    TimingDump((PlayerLoopTiming)i).Forget();
        //}

        //StartCoroutine(CoroutineDump("yield WaitForEndOfFrame", new WaitForEndOfFrame()));
        //StartCoroutine(CoroutineDump("yield WaitForFixedUpdate", new WaitForFixedUpdate()));
        //StartCoroutine(CoroutineDump("yield null", null));

        // -----

        // RunJobAsync().Forget();

        //ClickOnce().Forget();
        //ClickForever().Forget();

        //var cor = UniTask.ToCoroutine(async () =>
        // {
        //     var job = new MyJob() { loopCount = 999, inOut = new NativeArray<int>(1, Allocator.TempJob) };
        //     JobHandle.ScheduleBatchedJobs();
        //     await job.Schedule().WaitAsync(PlayerLoopTiming.Update);
        //     job.inOut.Dispose();
        // });

        //StartCoroutine(cor);


        Debug.Log("E:" + DateTime.Now.ToString());

        await UniTaskAsyncEnumerable.Timer(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), PlayerLoopTiming.Update).ForEachAsync(_ =>
        {
            Debug.Log("Call:" + DateTime.Now.ToString());
        }, cancellationToken: this.GetCancellationTokenOnDestroy());

        //try
        //{
        //    await this.GetAsyncUpdateTrigger().ForEachAsync(_ =>
        //    {
        //        UnityEngine.Debug.Log("EveryUpdate:" + Time.frameCount);
        //    });
        //}
        //catch (OperationCanceledException ex)
        //{
        //    UnityEngine.Debug.Log("END");
        //}



        CancellationTokenSource cts = new CancellationTokenSource();

        //var trigger = this.GetAsyncUpdateTrigger();
        //Go(trigger, 1, cts.Token).Forget();
        //Go(trigger, 2, cts.Token).Forget();
        //Go(trigger, 3, cts.Token).Forget();
        //Go(trigger, 4, cts.Token).Forget();
        //Go(trigger, 5, cts.Token).Forget();


        Application.logMessageReceived += Application_logMessageReceived;



        // foo.Status.IsCanceled


        // 5回クリックされるまで待つ、とか。
        //Debug.Log("Await start.");



        //await okButton.GetAsyncClickEventHandler().DisableAutoClose()
        //    .Select((_, clickCount) => clickCount + 1)
        //    .FirstAsync(x => x == 5);

        //Debug.Log("Click 5 times.");



        // await this.GetAsyncUpdateTrigger().UpdateAsAsyncEnumerable()














        //ucs = new UniTaskCompletionSource();

        //okButton.onClick.AddListener(async () =>
        //{
        //    await InnerAsync(false);
        //});

        okButton.onClick.AddListener(() =>
        {
            // FooAsync().Forget();
        });

        cancelButton.onClick.AddListener(() =>
        {
            text.text = "";

            // ucs.TrySetResult();

            cts.Cancel();
        });
    }

    static void Foo(UniTask t)
    {
    }

    async UniTaskVoid Go(AsyncUpdateTrigger trigger, int i, CancellationToken ct)
    {
        await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
        UnityEngine.Debug.Log("AWAIT BEFO:" + Time.frameCount);
        var handler = trigger.GetUpdateAsyncHandler(ct);

        try
        {
            while (!ct.IsCancellationRequested)
            {
                await handler.UpdateAsync();
                //await handler.UpdateAsync();
                Debug.Log("OK:" + i);
            }
        }
        finally
        {
            UnityEngine.Debug.Log("AWAIT END:" + Time.frameCount + ": No," + i);
        }
    }

    async UniTaskVoid ClickOnce()
    {
        try
        {
            await okButton.OnClickAsync();
            UnityEngine.Debug.Log("CLICKED ONCE");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex.ToString());
        }
        finally
        {
            UnityEngine.Debug.Log("END ONCE");
        }
    }

    async UniTaskVoid ClickForever()
    {
        try
        {
            using (var handler = okButton.GetAsyncClickEventHandler())
            {
                while (true)
                {
                    await handler.OnClickAsync();
                    UnityEngine.Debug.Log("Clicked");
                }
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex.ToString());
        }
        finally
        {
            UnityEngine.Debug.Log("END");
        }
    }

    async UniTask SimpleAwait()
    {
        await UniTask.Yield();
        await UniTask.Yield();
        await UniTask.Yield();
        throw new InvalidOperationException("bar!!!");
    }

    IEnumerator SimpleCoroutine()
    {
        yield return null;
        yield return null;
        yield return null;
        throw new InvalidOperationException("foo!!!");
    }

    async UniTask TimingDump(PlayerLoopTiming timing)
    {
        while (true)
        {
            await UniTask.Yield(timing);
            Debug.Log("PlayerLoopTiming." + timing);
        }
    }

    IEnumerator CoroutineDump(string msg, YieldInstruction waitObj)
    {
        while (true)
        {
            yield return waitObj;
            Debug.Log(msg);
        }
    }

    //private void Update()
    //{
    //    Debug.Log("Update");
    //}

    //private void LateUpdate()
    //{
    //    Debug.Log("LateUpdate");
    //}

    //private void FixedUpdate()
    //{
    //    Debug.Log("FixedUpdate");
    //}


    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (text != null)
        {
            text.text += "\n" + condition;
        }
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

    /*
    PlayerLoopTiming.Initialization
    PlayerLoopTiming.LastInitialization
    PlayerLoopTiming.EarlyUpdate
    PlayerLoopTiming.LastEarlyUpdate
    PlayerLoopTiming.PreUpdate
    PlayerLoopTiming.LastPreUpdate
    PlayerLoopTiming.Update
    Update
    yield null
    yield WaitForSeconds
    yield WWW
    yield StartCoroutine
    PlayerLoopTiming.LastUpdate
    PlayerLoopTiming.PreLateUpdate
    LateUpdate
    PlayerLoopTiming.LastPreLateUpdate
    PlayerLoopTiming.PostLateUpdate
    PlayerLoopTiming.LastPostLateUpdate
    yield WaitForEndOfFrame

    // --- Physics Loop
    PlayerLoopTiming.FixedUpdate
    FixedUpdate
    yield WaitForFixedUpdate
    PlayerLoopTiming.LastFixedUpdate
    */






















    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        // e.SetObserved();
        // or other custom write code.
        UnityEngine.Debug.LogError("Unobserved:" + e.Exception.ToString());
    }
}

public class ShowPlayerLoop
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        var playerLoop = UnityEngine.LowLevel.PlayerLoop.GetDefaultPlayerLoop();
        DumpPlayerLoop("Default", playerLoop);
    }

    public static void DumpPlayerLoop(string which, UnityEngine.LowLevel.PlayerLoopSystem playerLoop)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{which} PlayerLoop List");
        foreach (var header in playerLoop.subSystemList)
        {
            sb.AppendFormat("------{0}------", header.type.Name);
            sb.AppendLine();
            foreach (var subSystem in header.subSystemList)
            {
                sb.AppendFormat("{0}.{1}", header.type.Name, subSystem.type.Name);
                sb.AppendLine();

                if (subSystem.subSystemList != null)
                {
                    UnityEngine.Debug.LogWarning("More Subsystem:" + subSystem.subSystemList.Length);
                }
            }
        }

        UnityEngine.Debug.Log(sb.ToString());
    }
}