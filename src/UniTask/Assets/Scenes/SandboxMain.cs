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
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.IO;
using System.Linq.Expressions;


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
    public Camera mycamera;

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
        // var r = await TcsAsync("https://bing.com/");
        await Task.Yield();
        Debug.Log("TASKASYNC");
    }

    //async UniTaskVoid Test2()
    //{
    //    try
    //    {
    //        //var cts = new CancellationTokenSource();
    //        //var r = UniAsync("https://bing.com/", cts.Token);
    //        //cts.Cancel();
    //        //await r;
    //        Debug.Log("SendWebRequestDone:" + PlayerLoopInfo.CurrentLoopType);


    //        //        var foo = await UnityWebRequest.Get("https://bing.com/").SendWebRequest();
    //        //          foo.downloadHandler.text;
    //        //
    //        _ = await UnityWebRequest.Get("https://bing.com/").SendWebRequest().WithCancellation(CancellationToken.None);
    //        Debug.Log("SendWebRequestWithCancellationDone:" + PlayerLoopInfo.CurrentLoopType);
    //    }
    //    catch
    //    {
    //        Debug.Log("Canceled");
    //    }
    //}

    IEnumerator Test3(string url)
    {
        var req = UnityWebRequest.Get(url).SendWebRequest();
        yield return req;
        Debug.Log("COROUTINE");
    }

    //static async Task<UnityWebRequest> TcsAsync(string url)
    //{
    //    var req = await UnityWebRequest.Get(url).SendWebRequest();
    //    return req;
    //}

    //static async UniTask<UnityWebRequest> UniAsync(string url, CancellationToken cancellationToken)
    //{
    //    var req = await UnityWebRequest.Get(url).SendWebRequest().WithCancellation(cancellationToken);
    //    return req;
    //}

    async Task<int> Test()
    {
        await Task.Yield();
        return 10;
    }

    async UniTask<int> Ex()
    {
        await UniTask.Yield();
        //throw new Exception();
        await UniTask.Delay(TimeSpan.FromSeconds(15));
        return 0;
    }

    IEnumerator CoroutineRun()
    {
        UnityEngine.Debug.Log("Before Coroutine yield return null," + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
        yield return null;
        UnityEngine.Debug.Log("After Coroutine yield return null," + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
    }

    IEnumerator CoroutineRun2()
    {
        UnityEngine.Debug.Log("Before Coroutine yield return WaitForEndOfFrame," + Time.frameCount);
        yield return new WaitForEndOfFrame();
        UnityEngine.Debug.Log("After Coroutine yield return WaitForEndOfFrame," + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
        yield return new WaitForEndOfFrame();
        UnityEngine.Debug.Log("Onemore After Coroutine yield return WaitForEndOfFrame," + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
    }


    async UniTaskVoid AsyncRun()
    {
        UnityEngine.Debug.Log("Before async Yield(default)," + Time.frameCount);
        await UniTask.Yield();
        UnityEngine.Debug.Log("After async Yield(default)," + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
    }

    async UniTaskVoid AsyncLastUpdate()
    {
        UnityEngine.Debug.Log("Before async Yield(LastUpdate)," + Time.frameCount);
        await UniTask.Yield(PlayerLoopTiming.LastUpdate);
        UnityEngine.Debug.Log("After async Yield(LastUpdate)," + Time.frameCount);
    }

    async UniTaskVoid AsyncLastLast()
    {
        UnityEngine.Debug.Log("Before async Yield(LastPostLateUpdate)," + Time.frameCount);
        await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
        UnityEngine.Debug.Log("After async Yield(LastPostLateUpdate)," + Time.frameCount);
    }

    async UniTaskVoid Yieldding()
    {
        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        StartCoroutine(CoroutineRun());
    }

    async UniTaskVoid AsyncFixedUpdate()
    {
        while (true)
        {
            await UniTask.WaitForFixedUpdate();
            Debug.Log("Async:" + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
        }
    }

    IEnumerator CoroutineFixedUpdate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Debug.Log("Coroutine:" + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
        }
    }

    private void FixedUpdate()
    {
        // Debug.Log("FixedUpdate:" + Time.frameCount + ", " + PlayerLoopInfo.CurrentLoopType);
    }

    async UniTaskVoid DelayFrame3_Pre()
    {
        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        Debug.Log("Before framecount:" + Time.frameCount);
        await UniTask.DelayFrame(3);
        Debug.Log("After framecount:" + Time.frameCount);
    }

    async UniTaskVoid DelayFrame3_Post()
    {
        await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        Debug.Log("Before framecount:" + Time.frameCount);
        await UniTask.DelayFrame(3);
        Debug.Log("After framecount:" + Time.frameCount);
    }

    async UniTask TestCoroutine()
    {
        await UniTask.Yield();
        throw new Exception("foobarbaz");
    }

    async UniTask DelayCheck()
    {
        await UniTask.Yield(PlayerLoopTiming.PreUpdate);
        Debug.Log("before");
        var t = UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        await t;
        Debug.Log("after");
    }

    //private async UniTaskVoid ExecuteAsync()
    //{
    //    var req = UnityWebRequest.Get("https://google.com/");

    //    var v = await req.SendWebRequest().ToUniTask();
    //    // req.Dispose();
    //    Debug.Log($"{v.isDone} {v.isHttpError} {v.isNetworkError}");
    //    Debug.Log(v.downloadHandler.text);
    //}
    private async void Go()
    {
        await UniTask.DelayFrame(0);
    }

    async UniTask Foo()
    {
        await UniTask.DelayFrame(10);
        throw new Exception("yeah");
    }




    async void Nanika()
    {
        await UniTask.Yield();
        Debug.Log("Here");
        throw new Exception();
    }







    private void Awake()
    {
        // PlayerLoopInfo.Inject();
        PrepareCamera();
    }

    public IUniTaskAsyncEnumerable<int> MyEveryUpdate()
    {
        return UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
        {
            var frameCount = 0;
            await UniTask.Yield();
            while (!token.IsCancellationRequested)
            {
                await writer.YieldAsync(frameCount++); // instead of `yield return`
                await UniTask.Yield();
            }
        });
    }

    async void RunStandardTaskAsync()
    {
        Debug.Log("Wait 3 seconds");
        await Task.Delay(TimeSpan.FromSeconds(3));

        Debug.Log("Current SyncContext:" + SynchronizationContext.Current.GetType().FullName);
    }


    async UniTask QuitCheck()
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromMinutes(1), cancellationToken: quitSource.Token);
        }
        finally
        {
            Debug.Log("End QuitCheck async");
        }
    }

    CancellationTokenSource quitSource = new CancellationTokenSource();


    IEnumerator TestCor()
    {
        Debug.Log("start cor");
        yield return null;
        yield return new WaitForEndOfFrame();
        Debug.Log("end cor");
    }

    IEnumerator LastYieldCore()
    {
        Debug.Log("YieldBegin:" + Time.frameCount);
        yield return new WaitForEndOfFrame();
        Debug.Log("YieldEnd:" + Time.frameCount);
    }

    private static async UniTask TestAsync(CancellationToken ct)
    {
        Debug.Log("TestAsync Start.");
        var count = 0;
        while (!ct.IsCancellationRequested)
        {
            try
            {
                Debug.Log($"TestAsync try count:{++count}");
                var task1 = new WaitUntil(() => UnityEngine.Random.Range(0, 10) == 0).ToUniTask();
                var task2 = new WaitUntil(() => UnityEngine.Random.Range(0, 10) == 0).ToUniTask();
                var task3 = new WaitUntil(() => UnityEngine.Random.Range(0, 10) == 0).ToUniTask();

                await UniTask.WhenAny(task1, task2, task3);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;


            }
        }
        Debug.Log("TestAsync Finished.");
    }




    async UniTaskVoid Start()
    {


        // UniTask.Delay(TimeSpan.FromSeconds(1)).TimeoutWithoutException


        var currentLoop = PlayerLoop.GetDefaultPlayerLoop();
        PlayerLoopHelper.Initialize(ref currentLoop, InjectPlayerLoopTimings.Minimum); // minimum is Update | FixedUpdate | LastPostLateUpdate



        


        // TestAsync(cts.Token).Forget();

        okButton.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            await UniTask.WaitForEndOfFrame(this);
            var texture = new Texture2D(Screen.width, Screen.height);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            var jpg = texture.EncodeToJPG();
            File.WriteAllBytes("testscreencapture.jpg", jpg);
            Debug.Log("ok?");

            //var texture = ScreenCapture.CaptureScreenshotAsTexture();
            //if (texture == null)
            //{
            //    Debug.Log("fail");
            //}
            //else
            //{
            //    var jpg = texture.EncodeToJPG();
            //    File.WriteAllBytes("testscreencapture.jpg", jpg);
            //    Debug.Log("ok?");
            //}
        }));

        cancelButton.onClick.AddListener(UniTask.UnityAction(async () =>
        {
            //clickCancelSource.Cancel();

            //RunCheck(PlayerLoopTiming.Initialization).Forget();
            //RunCheck(PlayerLoopTiming.LastInitialization).Forget();
            //RunCheck(PlayerLoopTiming.EarlyUpdate).Forget();
            //RunCheck(PlayerLoopTiming.LastEarlyUpdate).Forget();
            //RunCheck(PlayerLoopTiming.FixedUpdate).Forget();
            //RunCheck(PlayerLoopTiming.LastFixedUpdate).Forget();
            //RunCheck(PlayerLoopTiming.PreUpdate).Forget();
            //RunCheck(PlayerLoopTiming.LastPreUpdate).Forget();
            //RunCheck(PlayerLoopTiming.Update).Forget();
            //RunCheck(PlayerLoopTiming.LastUpdate).Forget();
            //RunCheck(PlayerLoopTiming.PreLateUpdate).Forget();
            //RunCheck(PlayerLoopTiming.LastPreLateUpdate).Forget();
            //RunCheck(PlayerLoopTiming.PostLateUpdate).Forget();
            //RunCheck(PlayerLoopTiming.LastPostLateUpdate).Forget();

            await UniTask.Yield();
        }));

        await UniTask.Yield();
    }

    async UniTaskVoid RunCheck(PlayerLoopTiming timing)
    {
        //await UniTask.Yield(timing);
        //UnityEngine.Debug.Log("Yield:" + timing);
        await UniTask.DelayFrame(1, timing);
        UnityEngine.Debug.Log("Delay:" + timing);
    }

    private void Application_logMessageReceived2(string condition, string stackTrace, LogType type)
    {
        throw new NotImplementedException();
    }

    private void Application_logMessageReceived1(string condition, string stackTrace, LogType type)
    {
        throw new NotImplementedException();
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


    // GPU Screenshot Sample

    void PrepareCamera()
    {
        //Debug.Log("Support AsyncGPUReadback:" + SystemInfo.supportsAsyncGPUReadback);

        //var width = 480;
        //var height = 240;
        //var depth = 24;

        //mycamera.targetTexture = new RenderTexture(width, height, depth, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default)
        //{
        //    antiAliasing = 8
        //};
        //mycamera.enabled = true;

        //myRenderTexture = new RenderTexture(width, height, depth, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default)
        //{
        //    antiAliasing = 8
        //};
    }

    RenderTexture myRenderTexture;

    async UniTask ShootAsync()
    {
        var rt = mycamera.targetTexture;



        var req = await AsyncGPUReadback.Request(rt, 0);

        Debug.Log("GPU Readback done?:" + req.done);

        var rawByteArray = req.GetData<byte>().ToArray();
        var graphicsFormat = rt.graphicsFormat;
        var width = (uint)rt.width;
        var height = (uint)rt.height;

        Debug.Log("BytesSize:" + rawByteArray.Length);


        var imageBytes = ImageConversion.EncodeArrayToPNG(rawByteArray, graphicsFormat, width, height);


        File.WriteAllBytes("my_screenshot.png", imageBytes); // test







    }
}


public class SyncContextInjecter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Inject()
    {
        SynchronizationContext.SetSynchronizationContext(new UniTaskSynchronizationContext());
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

    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
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