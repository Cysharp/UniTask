# UniTask

[![CircleCI](https://circleci.com/gh/Cysharp/UniTask.svg?style=svg)](https://circleci.com/gh/Cysharp/UniTask)

Provides an efficient async/await integration to Unity.

> UniTask was included in UniRx before v6 but now completely separated, it no dependent each other.

Getting started
---
Install package(`UniRx.Async.unitypackage`) is available in [UniTask/releases](https://github.com/Cysharp/UniTask/releases) page.

```csharp
// extension awaiter/methods can be used by this namespace
using UniRx.Async;

// You can return type as struct UniTask<T>(or UniTask), it is unity specialized lightweight alternative of Task<T>
// no(or less) allocation and fast excution for zero overhead async/await integrate with Unity
async UniTask<string> DemoAsync()
{
    // You can await Unity's AsyncObject
    var asset = await Resources.LoadAsync<TextAsset>("foo");
    
    // .ConfigureAwait accepts progress callback
    await SceneManager.LoadSceneAsync("scene2").ConfigureAwait(Progress.Create<float>(x => Debug.Log(x)));
    
    // await frame-based operation like coroutine
    await UniTask.DelayFrame(100); 

    // replacement of WaitForSeconds/WaitForSecondsRealtime
    await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);
    
    // replacement of WaitForEndOfFrame(or other timing like yield return null, yield return WaitForFixedUpdate)
    await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
    
    // replacement of yield return WaitUntil
    await UniTask.WaitUntil(() => isActive == false);

    // You can await IEnumerator coroutine
    await FooCoroutineEnumerator();

    // You can await standard task
    await Task.Run(() => 100);

    // Multithreading, run on ThreadPool under this code(use SwitchToMainThread, same as `ObserveOnMainThreadDispatcher`)
    await UniTask.SwitchToThreadPool();

    // get async webrequest
    async UniTask<string> GetTextAsync(UnityWebRequest req)
    {
        var op = await req.SendWebRequest();
        return op.downloadHandler.text;
    }

    var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
    var task2 = GetTextAsync(UnityWebRequest.Get("http://bing.com"));
    var task3 = GetTextAsync(UnityWebRequest.Get("http://yahoo.com"));

    // concurrent async-wait and get result easily by tuple syntax
    var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);
    
    // You can handle timeout easily
    await GetTextAsync(UnityWebRequest.Get("http://unity.com")).Timeout(TimeSpan.FromMilliseconds(300));

    // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
    return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
}
```

`UniTask<T>`
---
UniTask feature rely on C# 7.0([task-like custom async method builder feature](https://github.com/dotnet/roslyn/blob/master/docs/features/task-types.md)) so required  Unity version is after `Unity 2018.3`.

Why UniTask(custom task-like object) is required? Because Task is too heavy, not matched to Unity threading(single-thread). UniTask does not use thread and SynchronizationContext because almost Unity's asynchronous object is automaticaly dispatched by Unity's engine layer. It acquires more fast and more less allocation, completely integrated with Unity.

You can await `AsyncOperation`, `ResourceRequest`, `UnityWebRequestAsyncOperation`, `IEnumerator` and others when using `UniRx.Async`.

`UniTask.Delay`, `UniTask.Yield`, `UniTask.Timeout` that is frame-based timer operators(no uses thread so works on WebGL publish) driven by custom PlayerLoop(Unity 2018 experimental feature). In default, UniTask initialize automatically when application begin, but it is override all. If you want to append PlayerLoop, please call `PlayerLoopHelper.Initialize(ref yourCustomizedPlayerLoop)` manually.

> Before Unity 2019.3, Unity does not have `PlayerLooop.GetCurrentlayerLoop` so you can't use with Unity ECS package in default. If you want to use with ECS and before Unity 2019.3, you can use this hack below.

```csharp
// Get ECS Loop.
var playerLoop = ScriptBehaviourUpdateOrder.CurrentPlayerLoop;

// Setup UniTask's PlayerLoop.
PlayerLoopHelper.Initialize(ref playerLoop);
```

`UniTask.WhenAll`, `UniTask.WhenAny` is like Task.WhenAll/WhenAny but return type is more useful.

`UniTask.ctor(Func<UniTask>)` is like the embeded [`AsyncLazy<T>`](https://blogs.msdn.microsoft.com/pfxteam/2011/01/15/asynclazyt/)

```csharp
public class SceneAssets
{
    public readonly UniTask<Sprite> Front;
    public readonly UniTask<Sprite> Background;
    public readonly UniTask<Sprite> Effect;

    public SceneAssets()
    {
        // ctor(Func) overload is AsyncLazy, initialized once when await.
        // and after it, await returns zero-allocation value immediately.
        Front = new UniTask<Sprite>(() => LoadAsSprite("foo"));
        Background = new UniTask<Sprite>(() => LoadAsSprite("bar"));
        Effect = new UniTask<Sprite>(() => LoadAsSprite("baz"));
    }

    async UniTask<Sprite> LoadAsSprite(string path)
    {
        var resource = await Resources.LoadAsync<Sprite>(path);
        return (resource as Sprite);
    }
}
```

If you want to convert callback to UniTask, you can use `UniTaskCompletionSource<T>` that is the lightweight edition of `TaskCompletionSource<T>`. 

```csharp
public UniTask<int> WrapByUniTaskCompletionSource()
{
    var utcs = new UniTaskCompletionSource<int>();

    // when complete, call utcs.TrySetResult();
    // when failed, call utcs.TrySetException();
    // when cancel, call utcs.TrySetCanceled();

    return utcs.Task; //return UniTask<int>
}
```

You can convert Task -> UniTask: `AsUniTask`, `UniTask` -> `UniTask<AsyncUnit>`: `AsAsyncUnitUniTask`(this is useful to use WhenAll/WhenAny), `UniTask<T>` -> `UniTask`: `AsUniTask`.

If you want to convert async to coroutine, you can use `UniTask.ToCoroutine`, this is useful to use only allow coroutine system.

Reusable Promises
---

Exception handling
---
`OperationCanceledException` and `UniTaskScheduler.UnobservedTaskException`, `UniTaskVoid`. and what is `UniTask.SuppressCancellationThrow`.

UniTaskTracker
---
useful for check(leak) UniTasks.

![](https://user-images.githubusercontent.com/46207/50421527-abf1cf80-0883-11e9-928a-ffcd47b8c454.png)

awaitable Events
---

```csharp
async UniTask TripleClick(CancellationToken token)
{
    await button.OnClickAsync(token);
    await button.OnClickAsync(token);
    await button.OnClickAsync(token);
    Debug.Log("Three times clicked");
}

// more efficient way
async UniTask TripleClick(CancellationToken token)
{
    using (var handler = button.GetAsyncClickEventHandler(token))
    {
        await handler.OnClickAsync();
        await handler.OnClickAsync();
        await handler.OnClickAsync();
        Debug.Log("Three times clicked");
    }
}
```

Method List
---
```csharp
UniTask.WaitUntil
UniTask.WaitWhile
UniTask.WaitUntilValueChanged
UniTask.WaitUntilValueChangedWithIsDestroyed
UniTask.SwitchToThreadPool
UniTask.SwitchToTaskPool
UniTask.SwitchToMainThread
UniTask.SwitchToSynchronizationContext
UniTask.Yield
UniTask.Run
UniTask.Lazy
UniTask.Void
UniTask.ConfigureAwait
UniTask.DelayFrame
UniTask.Delay(..., bool ignoreTimeScale = false, ...) parameter
```


For Unit Testing
---

Reference
---

License
---
This library is under the MIT License.