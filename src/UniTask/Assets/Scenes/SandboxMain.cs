using Cysharp.Threading.Tasks;
using System.Linq;
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
using UnityEngine.LowLevel;
using UnityEngine.Networking;
using UnityEngine.UI;

// using DG.Tweening;

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


    void Start2()
    {

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
        try
        {
            JobHandle.ScheduleBatchedJobs();

            var scheduled = job.Schedule();

            UnityEngine.Debug.Log("OK");
            await scheduled; // .ConfigureAwait(PlayerLoopTiming.Update); // .WaitAsync(PlayerLoopTiming.Update);
            UnityEngine.Debug.Log("OK2");
        }
        finally
        {
            job.inOut.Dispose();
        }
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

    async Task Test1()
    {
        var r = await TcsAsync("https://bing.com/");
        Debug.Log("TASKASYNC");
    }

    async UniTaskVoid Test2()
    {
        try
        {
            //var cts = new CancellationTokenSource();
            //var r = UniAsync("https://bing.com/", cts.Token);
            //cts.Cancel();
            //await r;
            _ = await UnityWebRequest.Get("https://bing.com/").SendWebRequest();
            Debug.Log("UNIASYNC1 ");

            _ = await UnityWebRequest.Get("https://bing.com/").SendWebRequest();
            Debug.Log("UNIASYNC2");
        }
        catch
        {
            Debug.Log("Canceled");
        }
    }

    IEnumerator Test3(string url)
    {
        var req = UnityWebRequest.Get(url).SendWebRequest();
        yield return req;
        Debug.Log("COROUTINE");
    }

    static async Task<UnityWebRequest> TcsAsync(string url)
    {
        var req = await UnityWebRequest.Get(url).SendWebRequest();
        return req;
    }

    static async UniTask<UnityWebRequest> UniAsync(string url, CancellationToken cancellationToken)
    {
        var req = await UnityWebRequest.Get(url).SendWebRequest().WithCancellation(cancellationToken);
        return req;
    }

    async Task<int> Test()
    {
        await Task.Yield();
        return 10;
    }


    async UniTaskVoid Start()
    {
        await UniTask.SwitchToMainThread();

        //UniTaskAsyncEnumerable.EveryValueChanged(mcc, x => x.MyProperty)
        //    .Do(_ => { }, () => Debug.Log("COMPLETED"))
        //    .ForEachAsync(x =>
        //    {
        //        Debug.Log("VALUE_CHANGED:" + x);
        //    })
        //    .Forget();

        //_ = Test1();
        Test2().Forget();
        //StartCoroutine(Test3("https://bing.com/"));


        // DG.Tweening.Core.TweenerCore<int>
        //Debug.Log("GO MOVEX");
        //await okButton.GetComponent<RectTransform>().DOMoveX(-10.2f, 3).WithCancellation(CancellationToken.None);
        //Debug.Log("END MOVEX");


        //Debug.Log("AGAIN MOVE");
        //await okButton.GetComponent<RectTransform>().DOMoveY(10.2f, 3).WithCancellation(CancellationToken.None);
        //Debug.Log("AGAIN END MOVE");

        Debug.Log(Test().GetType().FullName);



        // check stacktrace
        // await UniTaskAsyncEnumerable.EveryUpdate().Where((x, i) => i % 2 == 0).Select(x => x).DistinctUntilChanged().ForEachAsync(x =>
        //{
        // Debug.Log("test");
        //});




        //// DOTween.To(

        //var cts = new CancellationTokenSource();

        ////var tween = okButton.GetComponent<RectTransform>().DOLocalMoveX(100, 5.0f);

        //cancelButton.OnClickAsAsyncEnumerable().ForEachAsync(_ =>
        //{
        //    cts.Cancel();
        //}).Forget();


        //// await tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cts.Token);

        ////tween.SetRecyclable(true);

        //Debug.Log("END");

        //// tween.Play();

        //// DOTween.

        //// DOVirtual.Float(0, 1, 1, x => { }).ToUniTask();


        //await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate())
        //{
        //    Debug.Log("Update() " + Time.frameCount);
        //}



        //await okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
        //{
        //});


        //okButton.OnClickAsAsyncEnumerable().ForEachAsync(_ =>
        //{


        okButton.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            await UniTask.Yield();
            Debug.Log("Yeha");
        }));


        //}).Forget();

        //CloseAsync(this.GetCancellationTokenOnDestroy()).Forget();

        //okButton.onClick.AddListener(UniTask.UnityAction(async () => await UniTask.Yield()));

        PlayerLoopInfo.Inject();

        //UpdateUniTask().Forget();

        //StartCoroutine(Coroutine());

        //await UniTask.Delay(TimeSpan.FromSeconds(1));


        _ = ReturnToMainThreadTest();

        //GameObject.Destroy(this.gameObject);


        SynchronizationContext.Current.Post(_ =>
        {
            //UnityEngine.Debug.Log("Post:" + PlayerLoopInfo.CurrentLoopType);
        }, null);
    }

    async UniTaskVoid UpdateUniTask()
    {
        while (true)
        {
            await UniTask.Yield();
            UnityEngine.Debug.Log("UniTaskYield:" + PlayerLoopInfo.CurrentLoopType);
        }
    }


    async UniTaskVoid ReturnToMainThreadTest()
    {
        var d = UniTask.ReturnToCurrentSynchronizationContext();
        try
        {
            UnityEngine.Debug.Log("In MainThread?" + Thread.CurrentThread.ManagedThreadId);
            UnityEngine.Debug.Log("SyncContext is null?" + (SynchronizationContext.Current == null));
            await UniTask.SwitchToThreadPool();
            UnityEngine.Debug.Log("In ThreadPool?" + Thread.CurrentThread.ManagedThreadId);
            UnityEngine.Debug.Log("SyncContext is null?" + (SynchronizationContext.Current == null));
        }
        finally
        {
            await d.DisposeAsync();
        }

        UnityEngine.Debug.Log("In ThreadPool?" + Thread.CurrentThread.ManagedThreadId);
        UnityEngine.Debug.Log("SyncContext is null2" + (SynchronizationContext.Current == null));
    }


    private void Update()
    {
        // UnityEngine.Debug.Log("Update:" + PlayerLoopInfo.CurrentLoopType);
    }

    IEnumerator Coroutine()
    {
        try
        {
            while (true)
            {
                yield return null;
                //UnityEngine.Debug.Log("Coroutine null:" + PlayerLoopInfo.CurrentLoopType);
            }
        }
        finally
        {
            UnityEngine.Debug.Log("Coroutine Finally");
        }
    }

    async UniTaskVoid CloseAsync(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
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

public class PlayerLoopInfo
{
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
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

    public static Type CurrentLoopType { get; private set; }

    public static void Inject()
    {
        var system = PlayerLoop.GetCurrentPlayerLoop();

        for (int i = 0; i < system.subSystemList.Length; i++)
        {
            var loop = system.subSystemList[i].subSystemList.SelectMany(x =>
            {
                var t = typeof(WrapLoop<>).MakeGenericType(x.type);
                var instance = (ILoopRunner)Activator.CreateInstance(t, x.type);
                return new[] { new PlayerLoopSystem { type = t, updateDelegate = instance.Run }, x };
            }).ToArray();

            system.subSystemList[i].subSystemList = loop;
        }

        PlayerLoop.SetPlayerLoop(system);
    }

    interface ILoopRunner
    {
        void Run();
    }

    class WrapLoop<T> : ILoopRunner
    {
        readonly Type type;

        public WrapLoop(Type type)
        {
            this.type = type;
        }

        public void Run()
        {
            CurrentLoopType = type;
        }
    }
}