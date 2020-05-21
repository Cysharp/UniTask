using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
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


public class SimplePresenter
{
    // View
    public UnityEngine.UI.InputField Input;


    // Presenter


    public SimplePresenter()
    {
        //Input.OnValueChangedAsAsyncEnumerable()
        //   .Queue()
        //   .SelectAwait(async x =>
        //   {
        //       await UniTask.Delay(TimeSpan.FromSeconds(1));
        //       return x;
        //   })
        //   .Select(x=> x.ToUpper())
        //   .BindTo(



    }

}







public static partial class UnityUIComponentExtensions
{

}




public class AsyncMessageBroker<T> : IDisposable
{
    Channel<T> channel;

    IConnectableUniTaskAsyncEnumerable<T> multicastSource;
    IDisposable connection;

    public AsyncMessageBroker()
    {
        channel = Channel.CreateSingleConsumerUnbounded<T>();
        multicastSource = channel.Reader.ReadAllAsync().Publish();
        connection = multicastSource.Connect();
    }

    public void Publish(T value)
    {
        channel.Writer.TryWrite(value);
    }

    public IUniTaskAsyncEnumerable<T> Subscribe()
    {
        return multicastSource;
    }

    public void Dispose()
    {
        channel.Writer.TryComplete();
        connection.Dispose();
    }
}


public class SandboxMain : MonoBehaviour
{
    public Button okButton;
    public Button cancelButton;


    CancellationTokenSource cts;

    public AsyncReactiveProperty<int> RP1;


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





    public class Model
    {
        // State<int> Hp { get; }

        AsyncReactiveProperty<int> hp;
        IReadOnlyAsyncReactiveProperty<int> Hp => hp;



        public Model()
        {
            // hp = new AsyncReactiveProperty<int>();






            //setHp = Hp.GetSetter();
        }

        void Increment(int value)
        {


            // setHp(Hp.Value += value);
        }
    }



    public Text text;
    public Button button;

    [SerializeField]
    State<int> count;

    void Start2()
    {
        count = 10;

        var countS = count.GetSetter();

        count.BindTo(text);
        button.OnClickAsAsyncEnumerable().ForEachAsync(_ =>
        {
            // int foo = countS;
            //countS.Set(countS += 10);

            // setter.SetValue(count.Value + 10);
        });
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
    private async UniTaskVoid HogeAsync()
    {
        // await is not over
        await UniTaskAsyncEnumerable
            .TimerFrame(10)
            .ForEachAwaitAsync(async _ =>
            // .ForEachAsync(_ =>
            {
                await UniTask.Delay(1000);
                Debug.Log(Time.time);
            });

        Debug.Log("Done");
    }

    public int MyProperty { get; set; }

    public class MyClass
    {
        public int MyProperty { get; set; }
    }

    MyClass mcc;

    void Start()
    {
        this.mcc = new MyClass();
        this.MyProperty = 999;

        CheckDest().Forget();


        //UniTaskAsyncEnumerable.EveryValueChanged(mcc, x => x.MyProperty)
        //    .Do(_ => { }, () => Debug.Log("COMPLETED"))
        //    .ForEachAsync(x =>
        //    {
        //        Debug.Log("VALUE_CHANGED:" + x);
        //    })
        //    .Forget();



        


        okButton.OnClickAsAsyncEnumerable().ForEachAsync(_ =>
        {

            mcc.MyProperty += 10;



        }).Forget();

        cancelButton.OnClickAsAsyncEnumerable().ForEachAsync(_ =>
        {
            this.mcc = null;
        });

    }

    async UniTaskVoid CheckDest()
    {
        try
        {
            Debug.Log("WAIT");
            await UniTask.WaitUntilValueChanged(mcc, x => x.MyProperty);
            Debug.Log("CHANGED?");
        }
        finally
        {
            Debug.Log("END");
        }
    }

    async UniTaskVoid Running(CancellationToken ct)
    {
        Debug.Log("BEGIN");
        await UniTask.WaitUntilCanceled(ct);
        Debug.Log("DONE");
    }

    async UniTaskVoid WaitForChannelAsync(ChannelReader<int> reader, CancellationToken token)
    {
        try
        {
            //var result1 = await reader.ReadAsync(token);
            //Debug.Log(result1);

            await reader.ReadAllAsync().ForEachAsync(x => Debug.Log(x)/*, token*/);

            Debug.Log("done");
        }
        catch (Exception ex)
        {
            Debug.Log("here");
            Debug.LogException(ex);
        }
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