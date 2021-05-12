using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    public Toggle toggle = null;
    public Text text = null;

    async Task Start()
    {
        text.text = "";
        var result = await DemoAsync();
        text.text = $"RESULT is: {result}";
    }

    async UniTask<string> DemoAsync()
    {
        // You can await Unity's AsyncObject
        text.text = "loading assets";
        var asset = await Resources.LoadAsync<TextAsset>("foo");
        text.text = "get data from github.com";
        var txt = (await UnityWebRequest.Get("https://github.com").SendWebRequest()).downloadHandler.text;
        text.text = "load scene2";
        await SceneManager.LoadSceneAsync("scene2", LoadSceneMode.Additive);

        // .WithCancellation enables Cancel, GetCancellationTokenOnDestroy synchornizes with lifetime of GameObject
        text.text = "load text asset bar";
        var asset2 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(this.GetCancellationTokenOnDestroy());

        // .ToUniTask accepts progress callback(and all options), Progress.Create is a lightweight alternative of IProgress<T>
        text.text = "load text asset baz";
        var asset3 = await Resources.LoadAsync<TextAsset>("baz").ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

        // await frame-based operation like a coroutine
        text.text = "delay frame";
        await UniTask.DelayFrame(100);

        // replacement of yield return new WaitForSeconds/WaitForSecondsRealtime
        text.text = "wait 3sec.";
        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);

        // yield any playerloop timing(PreUpdate, Update, LateUpdate, etc...)
        text.text = "wait for PreLateUpdate";
        await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

        // replacement of yield return null
        text.text = "yield";
        await UniTask.Yield();
        text.text = "wait nextframe";
        await UniTask.NextFrame();

        // replacement of WaitForEndOfFrame(same as UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate))
        text.text = "wait end of frame";
        await UniTask.WaitForEndOfFrame();

        // replacement of yield return new WaitForFixedUpdate(same as UniTask.Yield(PlayerLoopTiming.FixedUpdate))
        text.text = "wait fixed update";
        await UniTask.WaitForFixedUpdate();

        // replacement of yield return WaitUntil
        text.text = "wait for toggle => false";
        await UniTask.WaitUntil(() => toggle.isOn == false);

        // special helper of WaitUntil
        text.text = "wait for toggle";
        await UniTask.WaitUntilValueChanged(this, x => toggle.isOn);

        // You can await IEnumerator coroutines
        text.text = "corouting";
        await FooCoroutineEnumerator();

        // You can await a standard task
        text.text = "run standard task";
        await Task.Run(() => 100);

        // Multithreading, run on ThreadPool under this code
        text.text = "switch to thread pool";
        await UniTask.SwitchToThreadPool();

        /* work on ThreadPool */

        // return to MainThread(same as `ObserveOnMainThread` in UniRx)
        await UniTask.SwitchToMainThread();
        text.text = "switch to main pool";

        // get async webrequest
        async UniTask<string> GetTextAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.text;
        }

        var task1 = GetTextAsync(UnityWebRequest.Get("https://google.com/"));
        var task2 = GetTextAsync(UnityWebRequest.Get("https://bing.com/"));
        var task3 = GetTextAsync(UnityWebRequest.Get("https://yahoo.com/"));

        // concurrent async-wait and get results easily by tuple syntax
        text.text = "get data from google,bing,yahoo (concurrent)";
        var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

        // can not call same task twice. let's define again for simple demo.
        var tasknew1 = GetTextAsync(UnityWebRequest.Get("https://google.com/"));
        var tasknew2 = GetTextAsync(UnityWebRequest.Get("https://bing.com/"));
        var tasknew3 = GetTextAsync(UnityWebRequest.Get("https://yahoo.com/"));

        // shorthand of WhenAll, tuple can await directly.
        text.text = "get data from google,bing,yahoo (shorthand)";
        var (google2, bing2, yahoo2) = await (tasknew1, tasknew2, tasknew3);

        // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
        text.text = "return asset result";
        return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
    }

    private IEnumerator FooCoroutineEnumerator()
    {
        Debug.Log("cotoutine 1");
        yield return new WaitForSeconds(1);
        Debug.Log("cotoutine 2");
        yield return new WaitForSeconds(1);
        Debug.Log("cotoutine end");
    }
}
