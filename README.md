UniTask
===
[![GitHub Actions](https://github.com/Cysharp/UniTask/workflows/Build-Debug/badge.svg)](https://github.com/Cysharp/UniTask/actions) [![Releases](https://img.shields.io/github/release/Cysharp/UniTask.svg)](https://github.com/Cysharp/UniTask/releases) [![Readme_CN](https://img.shields.io/badge/UniTask-%E4%B8%AD%E6%96%87%E6%96%87%E6%A1%A3-red)](https://github.com/Cysharp/UniTask/blob/master/README_CN.md)

Provides an efficient allocation free async/await integration for Unity.

* Struct based `UniTask<T>` and custom AsyncMethodBuilder to achieve zero allocation
* Makes all Unity AsyncOperations and Coroutines awaitable
* PlayerLoop based task(`UniTask.Yield`, `UniTask.Delay`, `UniTask.DelayFrame`, etc..) that enable replacing all coroutine operations
* MonoBehaviour Message Events and uGUI Events as awaitable/async-enumerable
* Runs completely on Unity's PlayerLoop so doesn't use threads and runs on WebGL, wasm, etc.
* Asynchronous LINQ, with Channel and AsyncReactiveProperty
* TaskTracker window to prevent memory leaks
* Highly compatible behaviour with Task/ValueTask/IValueTaskSource

For technical details, see blog post: [UniTask v2 — Zero Allocation async/await for Unity, with Asynchronous LINQ
](https://medium.com/@neuecc/unitask-v2-zero-allocation-async-await-for-unity-with-asynchronous-linq-1aa9c96aa7dd)  
For advanced tips, see blog post: [Extends UnityWebRequest via async decorator pattern — Advanced Techniques of UniTask](https://medium.com/@neuecc/extends-unitywebrequest-via-async-decorator-pattern-advanced-techniques-of-unitask-ceff9c5ee846)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Getting started](#getting-started)
- [Basics of UniTask and AsyncOperation](#basics-of-unitask-and-asyncoperation)
- [Cancellation and Exception handling](#cancellation-and-exception-handling)
- [Timeout handling](#timeout-handling)
- [Progress](#progress)
- [PlayerLoop](#playerloop)
- [async void vs async UniTaskVoid](#async-void-vs-async-unitaskvoid)
- [UniTaskTracker](#unitasktracker)
- [External Assets](#external-assets)
- [AsyncEnumerable and Async LINQ](#asyncenumerable-and-async-linq)
- [Awaitable Events](#awaitable-events)
- [Channel](#channel)
- [For Unit Testing](#for-unit-testing)
- [ThreadPool limitation](#threadpool-limitation)
- [IEnumerator.ToUniTask limitation](#ienumeratortounitask-limitation)
- [For UnityEditor](#for-unityeditor)
- [Compare with Standard Task API](#compare-with-standard-task-api)
- [Pooling Configuration](#pooling-configuration)
- [Allocation on Profiler](#allocation-on-profiler)
- [UniTaskSynchronizationContext](#unitasksynchronizationcontext)
- [API References](#api-references)
- [UPM Package](#upm-package)
  - [Install via git URL](#install-via-git-url)
- [.NET Core](#net-core)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Getting started
---
Install via [UPM package](#upm-package) with git reference or asset package(`UniTask.*.*.*.unitypackage`) available in [UniTask/releases](https://github.com/Cysharp/UniTask/releases).

```csharp
// extension awaiter/methods can be used by this namespace
using Cysharp.Threading.Tasks;

// You can return type as struct UniTask<T>(or UniTask), it is unity specialized lightweight alternative of Task<T>
// zero allocation and fast excution for zero overhead async/await integrate with Unity
async UniTask<string> DemoAsync()
{
    // You can await Unity's AsyncObject
    var asset = await Resources.LoadAsync<TextAsset>("foo");
    var txt = (await UnityWebRequest.Get("https://...").SendWebRequest()).downloadHandler.text;
    await SceneManager.LoadSceneAsync("scene2");

    // .WithCancellation enables Cancel, GetCancellationTokenOnDestroy synchornizes with lifetime of GameObject
    var asset2 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(this.GetCancellationTokenOnDestroy());

    // .ToUniTask accepts progress callback(and all options), Progress.Create is a lightweight alternative of IProgress<T>
    var asset3 = await Resources.LoadAsync<TextAsset>("baz").ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

    // await frame-based operation like a coroutine
    await UniTask.DelayFrame(100); 

    // replacement of yield return new WaitForSeconds/WaitForSecondsRealtime
    await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);
    
    // yield any playerloop timing(PreUpdate, Update, LateUpdate, etc...)
    await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

    // replacement of yield return null
    await UniTask.Yield();
    await UniTask.NextFrame();

    // replacement of WaitForEndOfFrame
#if UNITY_2023_1_OR_NEWER
    await UniTask.WaitForEndOfFrame();
#else
    // requires MonoBehaviour(CoroutineRunner))
    await UniTask.WaitForEndOfFrame(this); // this is MonoBehaviour
#endif

    // replacement of yield return new WaitForFixedUpdate(same as UniTask.Yield(PlayerLoopTiming.FixedUpdate))
    await UniTask.WaitForFixedUpdate();
    
    // replacement of yield return WaitUntil
    await UniTask.WaitUntil(() => isActive == false);

    // special helper of WaitUntil
    await UniTask.WaitUntilValueChanged(this, x => x.isActive);

    // You can await IEnumerator coroutines
    await FooCoroutineEnumerator();

    // You can await a standard task
    await Task.Run(() => 100);

    // Multithreading, run on ThreadPool under this code
    await UniTask.SwitchToThreadPool();

    /* work on ThreadPool */

    // return to MainThread(same as `ObserveOnMainThread` in UniRx)
    await UniTask.SwitchToMainThread();

    // get async webrequest
    async UniTask<string> GetTextAsync(UnityWebRequest req)
    {
        var op = await req.SendWebRequest();
        return op.downloadHandler.text;
    }

    var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
    var task2 = GetTextAsync(UnityWebRequest.Get("http://bing.com"));
    var task3 = GetTextAsync(UnityWebRequest.Get("http://yahoo.com"));

    // concurrent async-wait and get results easily by tuple syntax
    var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

    // shorthand of WhenAll, tuple can await directly
    var (google2, bing2, yahoo2) = await (task1, task2, task3);

    // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
    return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
}
```

Basics of UniTask and AsyncOperation
---
UniTask features rely on C# 7.0([task-like custom async method builder feature](https://github.com/dotnet/roslyn/blob/master/docs/features/task-types.md)) so the required Unity version is after `Unity 2018.3`, the official lowest version supported is `Unity 2018.4.13f1`.

Why is UniTask(custom task-like object) required? Because Task is too heavy and not matched to Unity threading (single-thread). UniTask does not use threads and SynchronizationContext/ExecutionContext because Unity's asynchronous object is automaticaly dispatched by Unity's engine layer. It achieves faster and lower allocation, and is completely integrated with Unity.

You can await `AsyncOperation`, `ResourceRequest`, `AssetBundleRequest`, `AssetBundleCreateRequest`, `UnityWebRequestAsyncOperation`, `AsyncGPUReadbackRequest`, `IEnumerator` and others when `using Cysharp.Threading.Tasks;`.

UniTask provides three pattern of extension methods.

```csharp
* await asyncOperation;
* .WithCancellation(CancellationToken);
* .ToUniTask(IProgress, PlayerLoopTiming, CancellationToken);
```

`WithCancellation` is a simple version of `ToUniTask`, both return `UniTask`. For details of cancellation, see: [Cancellation and Exception handling](#cancellation-and-exception-handling) section.

> Note: await directly is returned from native timing of PlayerLoop but WithCancellation and ToUniTask are returned from specified PlayerLoopTiming. For details of timing, see: [PlayerLoop](#playerloop) section.

> Note: AssetBundleRequest has `asset` and `allAssets`, default await returns `asset`. If you want to get `allAssets`, you can use `AwaitForAllAssets()` method.

The type of `UniTask` can use utilities like `UniTask.WhenAll`, `UniTask.WhenAny`. They are like `Task.WhenAll`/`Task.WhenAny` but the return type is more useful. They return value tuples so you can deconstruct each result and pass multiple types.

```csharp
public async UniTaskVoid LoadManyAsync()
{
    // parallel load.
    var (a, b, c) = await UniTask.WhenAll(
        LoadAsSprite("foo"),
        LoadAsSprite("bar"),
        LoadAsSprite("baz"));
}

async UniTask<Sprite> LoadAsSprite(string path)
{
    var resource = await Resources.LoadAsync<Sprite>(path);
    return (resource as Sprite);
}
```

If you want to convert a callback to UniTask, you can use `UniTaskCompletionSource<T>` which is a lightweight edition of `TaskCompletionSource<T>`. 

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

You can convert Task -> UniTask: `AsUniTask`, `UniTask` -> `UniTask<AsyncUnit>`: `AsAsyncUnitUniTask`, `UniTask<T>` -> `UniTask`: `AsUniTask`. `UniTask<T>` -> `UniTask`'s conversion cost is free.

If you want to convert async to coroutine, you can use `.ToCoroutine()`, this is useful if you want to only allow using the coroutine system.

UniTask can not await twice. This is a similar constraint to the [ValueTask/IValueTaskSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1?view=netcore-3.1) introduced in .NET Standard 2.1.

> The following operations should never be performed on a ValueTask<TResult> instance:
>
> * Awaiting the instance multiple times.
> * Calling AsTask multiple times.
> * Using .Result or .GetAwaiter().GetResult() when the operation hasn't yet completed, or using them multiple times.
> * Using more than one of these techniques to consume the instance.
>
> If you do any of the above, the results are undefined.

```csharp
var task = UniTask.DelayFrame(10);
await task;
await task; // NG, throws Exception
```

Store to the class field, you can use `UniTask.Lazy` that supports calling multiple times. `.Preserve()` allows for multiple calls (internally cached results). This is useful when there are multiple calls in a function scope.

Also `UniTaskCompletionSource` can await multiple times and await from many callers.

Cancellation and Exception handling
---
Some UniTask factory methods have a `CancellationToken cancellationToken = default` parameter. Also some async operations for Unity have `WithCancellation(CancellationToken)` and `ToUniTask(..., CancellationToken cancellation = default)` extension methods. 

You can pass `CancellationToken` to parameter by standard [`CancellationTokenSource`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource).

```csharp
var cts = new CancellationTokenSource();

cancelButton.onClick.AddListener(() =>
{
    cts.Cancel();
});

await UnityWebRequest.Get("http://google.co.jp").SendWebRequest().WithCancellation(cts.Token);

await UniTask.DelayFrame(1000, cancellationToken: cts.Token);
```

CancellationToken can be created by `CancellationTokenSource` or MonoBehaviour's extension method `GetCancellationTokenOnDestroy`.

```csharp
// this CancellationToken lifecycle is same as GameObject.
await UniTask.DelayFrame(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
```

For propagate Cancellation, all async method recommend to accept `CancellationToken cancellationToken` at last argument, and pass `CancellationToken` from root to end.

```csharp
await FooAsync(this.GetCancellationTokenOnDestroy());

// ---

async UniTask FooAsync(CancellationToken cancellationToken)
{
    await BarAsync(cancellationToken);
}

async UniTask BarAsync(CancellationToken cancellationToken)
{
    await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken);
}
```

`CancellationToken` means lifecycle of async. You can hold your own lifecycle insteadof default CancellationTokenOnDestroy.

```csharp
public class MyBehaviour : MonoBehaviour
{
    CancellationTokenSource disableCancellation = new CancellationTokenSource();
    CancellationTokenSource destroyCancellation = new CancellationTokenSource();

    private void OnEnable()
    {
        if (disableCancellation != null)
        {
            disableCancellation.Dispose();
        }
        disableCancellation = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        disableCancellation.Cancel();
    }

    private void OnDestroy()
    {
        destroyCancellation.Cancel();
        destroyCancellation.Dispose();
    }
}
```

When cancellation is detected, all methods throw `OperationCanceledException` and propagate upstream. When exception(not limited to `OperationCanceledException`) is not handled in async method, it is propagated finally to `UniTaskScheduler.UnobservedTaskException`. The default behaviour of received unhandled exception is to write log as exception. Log level can be changed using `UniTaskScheduler.UnobservedExceptionWriteLogType`. If you want to use custom behaviour, set an action to `UniTaskScheduler.UnobservedTaskException.`

And also `OperationCanceledException` is a special exception, this is silently ignored at `UnobservedTaskException`.

If you want to cancel behaviour in an async UniTask method, throw `OperationCanceledException` manually.

```csharp
public async UniTask<int> FooAsync()
{
    await UniTask.Yield();
    throw new OperationCanceledException();
}
```

If you handle an exception but want to ignore(propagate to global cancellation handling), use an exception filter.

```csharp
public async UniTask<int> BarAsync()
{
    try
    {
        var x = await FooAsync();
        return x * 2;
    }
    catch (Exception ex) when (!(ex is OperationCanceledException)) // when (ex is not OperationCanceledException) at C# 9.0
    {
        return -1;
    }
}
```

throws/catch `OperationCanceledException` is slightly heavy, so if performance is a concern, use `UniTask.SuppressCancellationThrow` to avoid OperationCanceledException throw. It returns `(bool IsCanceled, T Result)` instead of throwing.

```csharp
var (isCanceled, _) = await UniTask.DelayFrame(10, cancellationToken: cts.Token).SuppressCancellationThrow();
if (isCanceled)
{
    // ...
}
```

Note: Only suppress throws if you call directly into the most source method. Otherwise, the return value will be converted, but the entire pipeline will not suppress throws.

Some features that use Unity's player loop, such as `UniTask.Yield` and `UniTask.Delay` etc, determines CancellationToken state on the player loop. 
This means it does not cancel immediately upon `CancellationToken` fired. 

If you want to change this behaviour, the cancellation to be immediate, set the `cancelImmediately` flag as an argument.

```csharp
await UniTask.Yield(cancellationToken, cancelImmediately: true);
```

Note: Setting `cancelImmediately` to true and detecting an immediate cancellation is more costly than the default behavior.
This is because it uses `CancellationToken.Register`; it is heavier than checking CancellationToken on the player loop.

Timeout handling
---
Timeout is a variation of cancellation. You can set timeout by `CancellationTokenSouce.CancelAfterSlim(TimeSpan)` and pass CancellationToken to async methods.

```csharp
var cts = new CancellationTokenSource();
cts.CancelAfterSlim(TimeSpan.FromSeconds(5)); // 5sec timeout.

try
{
    await UnityWebRequest.Get("http://foo").SendWebRequest().WithCancellation(cts.Token);
}
catch (OperationCanceledException ex)
{
    if (ex.CancellationToken == cts.Token)
    {
        UnityEngine.Debug.Log("Timeout");
    }
}
```

> `CancellationTokenSouce.CancelAfter` is a standard api. However in Unity you should not use it because it depends threading timer. `CancelAfterSlim` is UniTask's extension methods, it uses PlayerLoop instead.

If you want to use timeout with other source of cancellation, use `CancellationTokenSource.CreateLinkedTokenSource`.

```csharp
var cancelToken = new CancellationTokenSource();
cancelButton.onClick.AddListener(() =>
{
    cancelToken.Cancel(); // cancel from button click.
});

var timeoutToken = new CancellationTokenSource();
timeoutToken.CancelAfterSlim(TimeSpan.FromSeconds(5)); // 5sec timeout.

try
{
    // combine token
    var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancelToken.Token, timeoutToken.Token);

    await UnityWebRequest.Get("http://foo").SendWebRequest().WithCancellation(linkedTokenSource.Token);
}
catch (OperationCanceledException ex)
{
    if (timeoutToken.IsCancellationRequested)
    {
        UnityEngine.Debug.Log("Timeout.");
    }
    else if (cancelToken.IsCancellationRequested)
    {
        UnityEngine.Debug.Log("Cancel clicked.");
    }
}
```

Optimize for reduce allocation of CancellationTokenSource for timeout per call async method, you can use UniTask's `TimeoutController`.

```csharp
TimeoutController timeoutController = new TimeoutController(); // setup to field for reuse.

async UniTask FooAsync()
{
    try
    {
        // you can pass timeoutController.Timeout(TimeSpan) to cancellationToken.
        await UnityWebRequest.Get("http://foo").SendWebRequest()
            .WithCancellation(timeoutController.Timeout(TimeSpan.FromSeconds(5)));
        timeoutController.Reset(); // call Reset(Stop timeout timer and ready for reuse) when succeed.
    }
    catch (OperationCanceledException ex)
    {
        if (timeoutController.IsTimeout())
        {
            UnityEngine.Debug.Log("timeout");
        }
    }
}
```

If you want to use timeout with other source of cancellation, use `new TimeoutController(CancellationToken)`.

```csharp
TimeoutController timeoutController;
CancellationTokenSource clickCancelSource;

void Start()
{
    this.clickCancelSource = new CancellationTokenSource();
    this.timeoutController = new TimeoutController(clickCancelSource);
}
```

Note: UniTask has `.Timeout`, `.TimeoutWithoutException` methods however, if possible, do not use these, please pass `CancellationToken`. Because `.Timeout` work from external of task, can not stop timeoutted task. `.Timeout` means ignore result when timeout. If you pass a `CancellationToken` to the method, it will act from inside of the task, so it is possible to stop a running task.

Progress
---
Some async operations for unity have `ToUniTask(IProgress<float> progress = null, ...)` extension methods. 

```csharp
var progress = Progress.Create<float>(x => Debug.Log(x));

var request = await UnityWebRequest.Get("http://google.co.jp")
    .SendWebRequest()
    .ToUniTask(progress: progress);
```

You should not use standard `new System.Progress<T>`, because it causes allocation every time. Use `Cysharp.Threading.Tasks.Progress` instead. This progress factory has two methods, `Create` and `CreateOnlyValueChanged`. `CreateOnlyValueChanged` calls only when the progress value has changed.

Implementing IProgress interface to caller is better as there is no lambda allocation.

```csharp
public class Foo : MonoBehaviour, IProgress<float>
{
    public void Report(float value)
    {
        UnityEngine.Debug.Log(value);
    }

    public async UniTaskVoid WebRequest()
    {
        var request = await UnityWebRequest.Get("http://google.co.jp")
            .SendWebRequest()
            .ToUniTask(progress: this); // pass this
    }
}
```

PlayerLoop
---
UniTask is run on a custom [PlayerLoop](https://docs.unity3d.com/ScriptReference/LowLevel.PlayerLoop.html). UniTask's playerloop based methods (such as `Delay`, `DelayFrame`, `asyncOperation.ToUniTask`, etc...) accept this `PlayerLoopTiming`.

```csharp
public enum PlayerLoopTiming
{
    Initialization = 0,
    LastInitialization = 1,

    EarlyUpdate = 2,
    LastEarlyUpdate = 3,

    FixedUpdate = 4,
    LastFixedUpdate = 5,

    PreUpdate = 6,
    LastPreUpdate = 7,

    Update = 8,
    LastUpdate = 9,

    PreLateUpdate = 10,
    LastPreLateUpdate = 11,

    PostLateUpdate = 12,
    LastPostLateUpdate = 13
    
#if UNITY_2020_2_OR_NEWER
    TimeUpdate = 14,
    LastTimeUpdate = 15,
#endif
}
```

It indicates when to run, you can check [PlayerLoopList.md](https://gist.github.com/neuecc/bc3a1cfd4d74501ad057e49efcd7bdae) to Unity's default playerloop and injected UniTask's custom loop.

`PlayerLoopTiming.Update` is similar to `yield return null` in a coroutine, but it is called before Update(Update and uGUI events(button.onClick, etc...) are called on `ScriptRunBehaviourUpdate`, yield return null is called on `ScriptRunDelayedDynamicFrameRate`). `PlayerLoopTiming.FixedUpdate` is similar to `WaitForFixedUpdate`.

> `PlayerLoopTiming.LastPostLateUpdate` is not equivalent to coroutine's `yield return new WaitForEndOfFrame()`. Coroutine's WaitForEndOfFrame seems to run after the PlayerLoop is done. Some methods that require coroutine's end of frame(`Texture2D.ReadPixels`, `ScreenCapture.CaptureScreenshotAsTexture`, `CommandBuffer`, etc) do not work correctly when replaced with async/await. In these cases, pass MonoBehaviour(coroutine runnner) to `UniTask.WaitForEndOfFrame`. For example, `await UniTask.WaitForEndOfFrame(this);` is lightweight allocation free alternative of `yield return new WaitForEndOfFrame()`.
> 
> Note: In Unity 2023.1 or newer, `await UniTask.WaitForEndOfFrame();` no longer requires MonoBehaviour. It uses `UnityEngine.Awaitable.EndOfFrameAsync`.

`yield return null` and `UniTask.Yield` are similar but different. `yield return null` always returns next frame but `UniTask.Yield` returns next called. That is, call `UniTask.Yield(PlayerLoopTiming.Update)` on `PreUpdate`, it returns same frame. `UniTask.NextFrame()` guarantees return next frame, you can expect this to behave exactly the same as `yield return null`.

> UniTask.Yield(without CancellationToken) is a special type, returns `YieldAwaitable` and runs on YieldRunner. It is the most lightweight and fastest.

`AsyncOperation` is returned from native timing. For example, await `SceneManager.LoadSceneAsync` is returned from `EarlyUpdate.UpdatePreloading` and after being called, the loaded scene's `Start` is called from `EarlyUpdate.ScriptRunDelayedStartupFrame`. Also `await UnityWebRequest` is returned from `EarlyUpdate.ExecuteMainThreadJobs`.

In UniTask, await directly uses native timing, while `WithCancellation` and `ToUniTask` use specified timing. This is usually not a particular problem, but with `LoadSceneAsync`, it causes a different order of Start and continuation after await. So it is recommended not to use `LoadSceneAsync.ToUniTask`.

In the stacktrace, you can check where it is running in playerloop.

![image](https://user-images.githubusercontent.com/46207/83735571-83caea80-a68b-11ea-8d22-5e22864f0d24.png)

By default, UniTask's PlayerLoop is initialized at `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]`.

The order in which methods are called in BeforeSceneLoad is nondeterministic, so if you want to use UniTask in other BeforeSceneLoad methods, you should try to initialize it before this.

```csharp
// AfterAssembliesLoaded is called before BeforeSceneLoad
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
public static void InitUniTaskLoop()
{
    var loop = PlayerLoop.GetCurrentPlayerLoop();
    Cysharp.Threading.Tasks.PlayerLoopHelper.Initialize(ref loop);
}
```

If you import Unity's `Entities` package, that resets the custom player loop to default at `BeforeSceneLoad` and injects ECS's loop. When Unity calls ECS's inject method after UniTask's initialize method, UniTask will no longer work.

To solve this issue, you can re-initialize the UniTask PlayerLoop after ECS is initialized.

```csharp
// Get ECS Loop.
var playerLoop = ScriptBehaviourUpdateOrder.CurrentPlayerLoop;

// Setup UniTask's PlayerLoop.
PlayerLoopHelper.Initialize(ref playerLoop);
```

You can diagnose whether UniTask's player loop is ready by calling `PlayerLoopHelper.IsInjectedUniTaskPlayerLoop()`. And also `PlayerLoopHelper.DumpCurrentPlayerLoop` logs all current playerloops to console.

```csharp
void Start()
{
    UnityEngine.Debug.Log("UniTaskPlayerLoop ready? " + PlayerLoopHelper.IsInjectedUniTaskPlayerLoop());
    PlayerLoopHelper.DumpCurrentPlayerLoop();
}
```

You can optimize loop cost slightly by remove unuse PlayerLoopTiming injection. You can call `PlayerLoopHelper.Initialize(InjectPlayerLoopTimings)` on initialize.

```csharp
var loop = PlayerLoop.GetCurrentPlayerLoop();
PlayerLoopHelper.Initialize(ref loop, InjectPlayerLoopTimings.Minimum); // minimum is Update | FixedUpdate | LastPostLateUpdate
```

`InjectPlayerLoopTimings` has three preset, `All` and `Standard`(All without last except LastPostLateUpdate), `Minimum`(`Update | FixedUpdate | LastPostLateUpdate`). Default is All and you can combine custom inject timings like `InjectPlayerLoopTimings.Update | InjectPlayerLoopTimings.FixedUpdate | InjectPlayerLoopTimings.PreLateUpdate`.

You can make error to use uninjected `PlayerLoopTiming` by [Microsoft.CodeAnalysis.BannedApiAnalyzers](https://github.com/dotnet/roslyn-analyzers/blob/master/src/Microsoft.CodeAnalysis.BannedApiAnalyzers/BannedApiAnalyzers.Help.md). For example, you can setup `BannedSymbols.txt` like this for `InjectPlayerLoopTimings.Minimum`.

```txt
F:Cysharp.Threading.Tasks.PlayerLoopTiming.Initialization; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastInitialization; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.EarlyUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastEarlyUpdate; Isn't injected this PlayerLoop in this project.d
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastFixedUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.PreUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastPreUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.PreLateUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastPreLateUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.PostLateUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.TimeUpdate; Isn't injected this PlayerLoop in this project.
F:Cysharp.Threading.Tasks.PlayerLoopTiming.LastTimeUpdate; Isn't injected this PlayerLoop in this project.
```

You can configure `RS0030` severity to error.

![image](https://user-images.githubusercontent.com/46207/109150837-bb933880-77ac-11eb-85ba-4fd15819dbd0.png)

async void vs async UniTaskVoid
---
`async void` is a standard C# task system so it does not run on UniTask systems. It is better not to use it. `async UniTaskVoid` is a lightweight version of `async UniTask` because it does not have awaitable completion and reports errors immediately to `UniTaskScheduler.UnobservedTaskException`. If you don't require awaiting (fire and forget), using `UniTaskVoid` is better. Unfortunately to dismiss warning, you're required to call `Forget()`.

```csharp
public async UniTaskVoid FireAndForgetMethod()
{
    // do anything...
    await UniTask.Yield();
}

public void Caller()
{
    FireAndForgetMethod().Forget();
}
```

Also UniTask has the `Forget` method, it is similar to `UniTaskVoid` and has the same effects. However `UniTaskVoid` is more efficient if you completely don't use `await`。

```csharp
public async UniTask DoAsync()
{
    // do anything...
    await UniTask.Yield();
}

public void Caller()
{
    DoAsync().Forget();
}
```

To use an async lambda registered to an event, don't use `async void`. Instead you can use `UniTask.Action` or `UniTask.UnityAction`, both of which create a delegate via `async UniTaskVoid` lambda.

```csharp
Action actEvent;
UnityAction unityEvent; // especially used in uGUI

// Bad: async void
actEvent += async () => { };
unityEvent += async () => { };

// Ok: create Action delegate by lambda
actEvent += UniTask.Action(async () => { await UniTask.Yield(); });
unityEvent += UniTask.UnityAction(async () => { await UniTask.Yield(); });
```

`UniTaskVoid` can also be used in MonoBehaviour's `Start` method.

```csharp
class Sample : MonoBehaviour
{
    async UniTaskVoid Start()
    {
        // async init code.
    }
}
```

UniTaskTracker
---
useful for checking (leaked) UniTasks. You can open tracker window in `Window -> UniTask Tracker`.

![image](https://user-images.githubusercontent.com/46207/83527073-4434bf00-a522-11ea-86e9-3b3975b26266.png)

* Enable AutoReload(Toggle) - Reload automatically.
* Reload - Reload view.
* GC.Collect - Invoke GC.Collect.
* Enable Tracking(Toggle) - Start to track async/await UniTask. Performance impact: low.
* Enable StackTrace(Toggle) - Capture StackTrace when task is started. Performance impact: high.

UniTaskTracker is intended for debugging use only as enabling tracking and capturing stacktraces is useful but has a heavy performance impact. Recommended usage is to enable both tracking and stacktraces to find task leaks and to disable them both when done.

External Assets
---
By default, UniTask supports TextMeshPro(`BindTo(TMP_Text)` and `TMP_InputField` event extensions like standard uGUI `InputField`), DOTween(`Tween` as awaitable) and Addressables(`AsyncOperationHandle` and `AsyncOperationHandle<T>` as awaitable).

There are defined in separated asmdefs like `UniTask.TextMeshPro`, `UniTask.DOTween`, `UniTask.Addressables`.

TextMeshPro and Addressables support are automatically enabled when importing their packages from package manager. 
However for DOTween support, after importing from the [DOTWeen assets](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676r) and define the scripting define symbol `UNITASK_DOTWEEN_SUPPORT` to enable it.

```csharp
// sequential
await transform.DOMoveX(2, 10);
await transform.DOMoveZ(5, 20);

// parallel with cancellation
var ct = this.GetCancellationTokenOnDestroy();

await UniTask.WhenAll(
    transform.DOMoveX(10, 3).WithCancellation(ct),
    transform.DOScale(10, 3).WithCancellation(ct));
```

DOTween support's default behaviour(`await`, `WithCancellation`, `ToUniTask`) awaits tween is killed. It works on both Complete(true/false) and Kill(true/false). But if you want to reuse tweens (`SetAutoKill(false)`), it does not work as expected. If you want to await for another timing, the following extension methods exist in Tween, `AwaitForComplete`, `AwaitForPause`, `AwaitForPlay`, `AwaitForRewind`, `AwaitForStepComplete`.

AsyncEnumerable and Async LINQ
---
Unity 2020.2 supports C# 8.0 so you can use `await foreach`. This is the new Update notation in the async era.

```csharp
// Unity 2020.2, C# 8.0
await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(token))
{
    Debug.Log("Update() " + Time.frameCount);
}
```

In a C# 7.3 environment, you can use the `ForEachAsync` method to work in almost the same way.

```csharp
// C# 7.3(Unity 2018.3~)
await UniTaskAsyncEnumerable.EveryUpdate().ForEachAsync(_ =>
{
    Debug.Log("Update() " + Time.frameCount);
}, token);
```

UniTaskAsyncEnumerable implements asynchronous LINQ, similar to LINQ in `IEnumerable<T>` or Rx in `IObservable<T>`. All standard LINQ query operators can be applied to asynchronous streams. For example, the following code shows how to apply a Where filter to a button-click asynchronous stream that runs once every two clicks.

```csharp
await okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
{
});
```

Fire and Forget style(for example, event handling), you can also use `Subscribe`.

```csharp
okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).Subscribe(_ =>
{
});
```

Async LINQ is enabled when `using Cysharp.Threading.Tasks.Linq;`, and `UniTaskAsyncEnumerable` is defined in `UniTask.Linq` asmdef.

It's closer to UniRx (Reactive Extensions), but UniTaskAsyncEnumerable is a pull-based asynchronous stream, whereas Rx was a push-based asynchronous stream. Note that although similar, the characteristics are different and the details behave differently along with them.

`UniTaskAsyncEnumerable` is the entry point like `Enumerable`. In addition to the standard query operators, there are other generators for Unity such as `EveryUpdate`, `Timer`, `TimerFrame`, `Interval`, `IntervalFrame`, and `EveryValueChanged`. And also added additional UniTask original query operators like `Append`, `Prepend`, `DistinctUntilChanged`, `ToHashSet`, `Buffer`, `CombineLatest`,`Merge` `Do`, `Never`, `ForEachAsync`, `Pairwise`, `Publish`, `Queue`, `Return`, `SkipUntil`, `TakeUntil`, `SkipUntilCanceled`, `TakeUntilCanceled`, `TakeLast`, `Subscribe`.

The method with Func as an argument has three additional overloads, `***Await`, `***AwaitWithCancellation`.

```csharp
Select(Func<T, TR> selector)
SelectAwait(Func<T, UniTask<TR>> selector)
SelectAwaitWithCancellation(Func<T, CancellationToken, UniTask<TR>> selector)
```

If you want to use the `async` method inside the func, use the `***Await` or `***AwaitWithCancellation`.

How to create an async iterator: C# 8.0 supports async iterator(`async yield return`) but it only allows `IAsyncEnumerable<T>` and of course requires C# 8.0. UniTask supports `UniTaskAsyncEnumerable.Create` method to create custom async iterator.

```csharp
// IAsyncEnumerable, C# 8.0 version of async iterator. ( do not use this style, IAsyncEnumerable is not controled in UniTask).
public async IAsyncEnumerable<int> MyEveryUpdate([EnumeratorCancellation]CancellationToken cancelationToken = default)
{
    var frameCount = 0;
    await UniTask.Yield();
    while (!token.IsCancellationRequested)
    {
        yield return frameCount++;
        await UniTask.Yield();
    }
}

// UniTaskAsyncEnumerable.Create and use `await writer.YieldAsync` instead of `yield return`.
public IUniTaskAsyncEnumerable<int> MyEveryUpdate()
{
    // writer(IAsyncWriter<T>) has `YieldAsync(value)` method.
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
```

Awaitable Events
---
All uGUI component implements `***AsAsyncEnumerable` to convert asynchronous streams of events.

```csharp
async UniTask TripleClick()
{
    // In default, used button.GetCancellationTokenOnDestroy to manage lieftime of async
    await button.OnClickAsync();
    await button.OnClickAsync();
    await button.OnClickAsync();
    Debug.Log("Three times clicked");
}

// more efficient way
async UniTask TripleClick()
{
    using (var handler = button.GetAsyncClickEventHandler())
    {
        await handler.OnClickAsync();
        await handler.OnClickAsync();
        await handler.OnClickAsync();
        Debug.Log("Three times clicked");
    }
}

// use async LINQ
async UniTask TripleClick(CancellationToken token)
{
    await button.OnClickAsAsyncEnumerable().Take(3).Last();
    Debug.Log("Three times clicked");
}

// use async LINQ2
async UniTask TripleClick(CancellationToken token)
{
    await button.OnClickAsAsyncEnumerable().Take(3).ForEachAsync(_ =>
    {
        Debug.Log("Every clicked");
    });
    Debug.Log("Three times clicked, complete.");
}
```

All MonoBehaviour message events can convert async-streams by `AsyncTriggers` that can be enabled by `using Cysharp.Threading.Tasks.Triggers;`. AsyncTrigger can be created using `GetAsync***Trigger` and triggers itself as UniTaskAsyncEnumerable.

```csharp
var trigger = this.GetOnCollisionEnterAsyncHandler();
await trigger.OnCollisionEnterAsync();
await trigger.OnCollisionEnterAsync();
await trigger.OnCollisionEnterAsync();

// every moves.
await this.GetAsyncMoveTrigger().ForEachAsync(axisEventData =>
{
});
```

`AsyncReactiveProperty`, `AsyncReadOnlyReactiveProperty` is UniTask's version of ReactiveProperty. `BindTo` extension method of `IUniTaskAsyncEnumerable<T>` for binding asynchronous stream values to Unity components(Text/Selectable/TMP/Text).

```csharp
var rp = new AsyncReactiveProperty<int>(99);

// AsyncReactiveProperty itself is IUniTaskAsyncEnumerable, you can query by LINQ
rp.ForEachAsync(x =>
{
    Debug.Log(x);
}, this.GetCancellationTokenOnDestroy()).Forget();

rp.Value = 10; // push 10 to all subscriber
rp.Value = 11; // push 11 to all subscriber

// WithoutCurrent ignore initial value
// BindTo bind stream value to unity components.
rp.WithoutCurrent().BindTo(this.textComponent);

await rp.WaitAsync(); // wait until next value set

// also exists ToReadOnlyAsyncReactiveProperty
var rp2 = new AsyncReactiveProperty<int>(99);
var rorp = rp.CombineLatest(rp2, (x, y) => (x, y)).ToReadOnlyAsyncReactiveProperty(CancellationToken.None);
```

A pull-type asynchronous stream does not get the next values until the asynchronous processing in the sequence is complete. This could spill data from push-type events such as buttons.

```csharp
// can not get click event during 3 seconds complete.
await button.OnClickAsAsyncEnumerable().ForEachAwaitAsync(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

It is useful (prevent double-click) but not useful sometimes.

Using the `Queue()` method will also queue events during asynchronous processing.

```csharp
// queued message in asynchronous processing
await button.OnClickAsAsyncEnumerable().Queue().ForEachAwaitAsync(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

Or use `Subscribe`, fire and forget style.

```csharp
button.OnClickAsAsyncEnumerable().Subscribe(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

Channel
---
`Channel` is the same as [System.Threading.Tasks.Channels](https://docs.microsoft.com/en-us/dotnet/api/system.threading.channels?view=netcore-3.1) which is similar to a GoLang Channel.

Currently it only supports multiple-producer, single-consumer unbounded channels. It can create by `Channel.CreateSingleConsumerUnbounded<T>()`.

For producer(`.Writer`), use `TryWrite` to push value and `TryComplete` to complete channel. For consumer(`.Reader`), use `TryRead`, `WaitToReadAsync`, `ReadAsync`, `Completion` and `ReadAllAsync` to read queued messages.

`ReadAllAsync` returns `IUniTaskAsyncEnumerable<T>` so query LINQ operators. Reader only allows single-consumer but uses `.Publish()` query operator to enable multicast message. For example, make pub/sub utility.

```csharp
public class AsyncMessageBroker<T> : IDisposable
{
    Channel<T> channel;

    IConnectableUniTaskAsyncEnumerable<T> multicastSource;
    IDisposable connection;

    public AsyncMessageBroker()
    {
        channel = Channel.CreateSingleConsumerUnbounded<T>();
        multicastSource = channel.Reader.ReadAllAsync().Publish();
        connection = multicastSource.Connect(); // Publish returns IConnectableUniTaskAsyncEnumerable.
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
```

For Unit Testing
---
Unity's `[UnityTest]` attribute can test coroutine(IEnumerator) but can not test async. `UniTask.ToCoroutine` bridges async/await to coroutine so you can test async methods.

```csharp
[UnityTest]
public IEnumerator DelayIgnore() => UniTask.ToCoroutine(async () =>
{
    var time = Time.realtimeSinceStartup;

    Time.timeScale = 0.5f;
    try
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: true);

        var elapsed = Time.realtimeSinceStartup - time;
        Assert.AreEqual(3, (int)Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven));
    }
    finally
    {
        Time.timeScale = 1.0f;
    }
});
```

UniTask's own unit tests are written using Unity Test Runner and [Cysharp/RuntimeUnitTestToolkit](https://github.com/Cysharp/RuntimeUnitTestToolkit) to integrate with CI and check if IL2CPP is working.

ThreadPool limitation
---
Most UniTask methods run on a single thread (PlayerLoop), with only `UniTask.Run`(`Task.Run` equivalent) and `UniTask.SwitchToThreadPool` running on a thread pool. If you use a thread pool, it won't work with WebGL and so on.

`UniTask.Run` is now deprecated. You can use `UniTask.RunOnThreadPool` instead. And also consider whether you can use `UniTask.Create` or `UniTask.Void`.

IEnumerator.ToUniTask limitation
---
You can convert coroutine(IEnumerator) to UniTask(or await directly) but it has some limitations.

* `WaitForEndOfFrame`/`WaitForFixedUpdate`/`Coroutine` is not supported.
* Consuming loop timing is not the same as `StartCoroutine`, it uses the specified `PlayerLoopTiming` and the default `PlayerLoopTiming.Update` is run before MonoBehaviour's `Update` and `StartCoroutine`'s loop.

If you want fully compatible conversion from coroutine to async, use the `IEnumerator.ToUniTask(MonoBehaviour coroutineRunner)` overload. It executes StartCoroutine on an instance of the argument MonoBehaviour and waits for it to complete in UniTask.

For UnityEditor
---
UniTask can run on Unity Editor like an Editor Coroutine. However, there are some limitations.

* UniTask.Delay's DelayType.DeltaTime, UnscaledDeltaTime do not work correctly because they can not get deltaTime in editor. Therefore run on EditMode, automatically change DelayType to `DelayType.Realtime` that wait for the right time.
* All PlayerLoopTiming run on the timing `EditorApplication.update`.
* `-batchmode` with `-quit` does not work because Unity does not run `EditorApplication.update` and quit after a single frame. Instead, don't use `-quit` and quit manually with `EditorApplication.Exit(0)`.

Compare with Standard Task API
---
UniTask has many standard Task-like APIs. This table shows what the alternative apis are.

Use standard type.

| .NET Type | UniTask Type | 
| --- | --- |
| `IProgress<T>` | --- |
| `CancellationToken` | --- | 
| `CancellationTokenSource` | --- |

Use UniTask type.

| .NET Type | UniTask Type | 
| --- | --- |
| `Task`/`ValueTask` | `UniTask` |
| `Task<T>`/`ValueTask<T>` | `UniTask<T>` |
| `async void` | `async UniTaskVoid` | 
| `+= async () => { }` | `UniTask.Void`, `UniTask.Action`, `UniTask.UnityAction` |
| --- | `UniTaskCompletionSource` |
| `TaskCompletionSource<T>` | `UniTaskCompletionSource<T>`/`AutoResetUniTaskCompletionSource<T>` |
| `ManualResetValueTaskSourceCore<T>` | `UniTaskCompletionSourceCore<T>` |
| `IValueTaskSource` | `IUniTaskSource` |
| `IValueTaskSource<T>` | `IUniTaskSource<T>` |
| `ValueTask.IsCompleted` | `UniTask.Status.IsCompleted()` |
| `ValueTask<T>.IsCompleted` | `UniTask<T>.Status.IsCompleted()` |
| `new Progress<T>` | `Progress.Create<T>` |
| `CancellationToken.Register(UnsafeRegister)` | `CancellationToken.RegisterWithoutCaptureExecutionContext` |
| `CancellationTokenSource.CancelAfter` | `CancellationTokenSource.CancelAfterSlim` |
| `Channel.CreateUnbounded<T>(false){ SingleReader = true }` | `Channel.CreateSingleConsumerUnbounded<T>` |
| `IAsyncEnumerable<T>` | `IUniTaskAsyncEnumerable<T>` |
| `IAsyncEnumerator<T>` | `IUniTaskAsyncEnumerator<T>` |
| `IAsyncDisposable` | `IUniTaskAsyncDisposable` |
| `Task.Delay` | `UniTask.Delay` |
| `Task.Yield` | `UniTask.Yield` |
| `Task.Run` | `UniTask.RunOnThreadPool` |
| `Task.WhenAll` | `UniTask.WhenAll` |
| `Task.WhenAny` | `UniTask.WhenAny` |
| `Task.CompletedTask` | `UniTask.CompletedTask` |
| `Task.FromException` | `UniTask.FromException` |
| `Task.FromResult` | `UniTask.FromResult` |
| `Task.FromCanceled` | `UniTask.FromCanceled` |
| `Task.ContinueWith` | `UniTask.ContinueWith` |
| `TaskScheduler.UnobservedTaskException` | `UniTaskScheduler.UnobservedTaskException` |

Pooling Configuration
---
UniTask aggressively caches async promise objects to achieve zero allocation (for technical details, see blog post [UniTask v2 — Zero Allocation async/await for Unity, with Asynchronous LINQ](https://medium.com/@neuecc/unitask-v2-zero-allocation-async-await-for-unity-with-asynchronous-linq-1aa9c96aa7dd)). By default, it caches all promises but you can configure `TaskPool.SetMaxPoolSize` to your value, the value indicates cache size per type. `TaskPool.GetCacheSizeInfo` returns currently cached objects in pool.

```csharp
foreach (var (type, size) in TaskPool.GetCacheSizeInfo())
{
    Debug.Log(type + ":" + size);
}
```

Allocation on Profiler
---
In UnityEditor the profiler shows allocation of compiler generated AsyncStateMachine but it only occurs in debug(development) build. C# Compiler generates AsyncStateMachine as class on Debug build and as struct on Release build.

Unity supports Code Optimization option starting in 2020.1 (right, footer).

![](https://user-images.githubusercontent.com/46207/89967342-2f944600-dc8c-11ea-99fc-0b74527a16f6.png)

You can change C# compiler optimization to release to remove AsyncStateMachine allocation in development builds. This optimization option can also be set via `Compilation.CompilationPipeline-codeOptimization`, and `Compilation.CodeOptimization`.

UniTaskSynchronizationContext
---
Unity's default SynchronizationContext(`UnitySynchronizationContext`) is a poor implementation for performance. UniTask bypasses `SynchronizationContext`(and `ExecutionContext`) so it does not use it but if exists in `async Task`, still used it. `UniTaskSynchronizationContext` is a replacement of `UnitySynchronizationContext` which is better for performance.

```csharp
public class SyncContextInjecter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Inject()
    {
        SynchronizationContext.SetSynchronizationContext(new UniTaskSynchronizationContext());
    }
}
```

This is an optional choice and is not always recommended; `UniTaskSynchronizationContext` is less performant than `async UniTask` and is not a complete UniTask replacement. It also does not guarantee full behavioral compatibility with the `UnitySynchronizationContext`.

API References
---
UniTask's API References are hosted at [cysharp.github.io/UniTask](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.html) by [DocFX](https://dotnet.github.io/docfx/) and [Cysharp/DocfXTemplate](https://github.com/Cysharp/DocfxTemplate).

For example, UniTask's factory methods can be seen at [UniTask#methods](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.UniTask.html#methods-1). UniTaskAsyncEnumerable's factory/extension methods can be seen at [UniTaskAsyncEnumerable#methods](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.Linq.UniTaskAsyncEnumerable.html#methods-1).

UPM Package
---
### Install via git URL

Requires a version of unity that supports path query parameter for git packages (Unity >= 2019.3.4f1, Unity >= 2020.1a21). You can add `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask` to Package Manager

![image](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

![image](https://user-images.githubusercontent.com/46207/83702872-e0f17c80-a648-11ea-8183-7469dcd4f810.png)

or add `"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask"` to `Packages/manifest.json`.

If you want to set a target version, UniTask uses the `*.*.*` release tag so you can specify a version like `#2.1.0`. For example `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.1.0`.


.NET Core
---
For .NET Core, use NuGet.

> PM> Install-Package [UniTask](https://www.nuget.org/packages/UniTask)

UniTask of .NET Core version is a subset of Unity UniTask with PlayerLoop dependent methods removed.

It runs at higher performance than the standard Task/ValueTask, but you should be careful to ignore the ExecutionContext/SynchronizationContext when using it. `AsyncLocal` also does not work because it ignores ExecutionContext.

If you use UniTask internally, but provide ValueTask as an external API, you can write it like the following(Inspired by [PooledAwait](https://github.com/mgravell/PooledAwait)).

```csharp
public class ZeroAllocAsyncAwaitInDotNetCore
{
    public ValueTask<int> DoAsync(int x, int y)
    {
        return Core(this, x, y);

        static async UniTask<int> Core(ZeroAllocAsyncAwaitInDotNetCore self, int x, int y)
        {
            // do anything...
            await Task.Delay(TimeSpan.FromSeconds(x + y));
            await UniTask.Yield();

            return 10;
        }
    }
}

// UniTask does not return to original SynchronizationContext but you can use helper `ReturnToCurrentSynchronizationContext`.
public ValueTask TestAsync()
{
    await using (UniTask.ReturnToCurrentSynchronizationContext())
    {
        await UniTask.SwitchToThreadPool();
        // do anything..
    }
}
```

.NET Core version is intended to allow users to use UniTask as an interface when sharing code with Unity (such as [Cysharp/MagicOnion](https://github.com/Cysharp/MagicOnion/)). .NET Core version of UniTask enables smooth code sharing.

Utility methods such as WhenAll which are equivalent to UniTask are provided as [Cysharp/ValueTaskSupplement](https://github.com/Cysharp/ValueTaskSupplement).

License
---
This library is under the MIT License.
