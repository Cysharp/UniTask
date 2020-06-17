UniTask
===
[![GitHub Actions](https://github.com/Cysharp/UniTask/workflows/Build-Debug/badge.svg)](https://github.com/Cysharp/UniTask/actions) [![Releases](https://img.shields.io/github/release/Cysharp/UniTask.svg)](https://github.com/Cysharp/UniTask/releases)

Provides an efficient allocation free async/await integration to Unity.

* Struct based `UniTask<T>` and custom AsyncMethodBuilder to achive zero allocation
* All Unity AsyncOperations and Coroutine to awaitable
* PlayerLoop based task(`UniTask.Yield`, `UniTask.Delay`, `UniTask.DelayFrame`, etc..) that enable to replace all coroutine operation
* MonoBehaviour Message Events and uGUI Events as awaitable/async-enumerable
* Completely run on Unity's PlayerLoop so don't use thread and run on WebGL, wasm, etc.
* Asynchronous LINQ, with Channel and AsyncReactiveProperty
* TaskTracker window to prevent memory leak
* Highly compatible behaviour with Task/ValueTask/IValueTaskSource

Techinical details, see blog post: [UniTask v2 — Zero Allocation async/await for Unity, with Asynchronous LINQ
](https://medium.com/@neuecc/unitask-v2-zero-allocation-async-await-for-unity-with-asynchronous-linq-1aa9c96aa7dd)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Getting started](#getting-started)
- [Basics of UniTask and AsyncOperation](#basics-of-unitask-and-asyncoperation)
- [Cancellation and Exception handling](#cancellation-and-exception-handling)
- [Progress](#progress)
- [PlayerLoop](#playerloop)
- [async void vs async UniTaskVoid](#async-void-vs-async-unitaskvoid)
- [UniTaskTracker](#unitasktracker)
- [External Assets](#external-assets)
- [AsyncEnumerable and Async LINQ](#asyncenumerable-and-async-linq)
- [Awaitable Events](#awaitable-events)
- [Channel](#channel)
- [For Unit Testing](#for-unit-testing)
- [Compare with Standard Task API](#compare-with-standard-task-api)
- [Pooling Configuration](#pooling-configuration)
- [API References](#api-references)
- [UPM Package](#upm-package)
  - [Install via git URL](#install-via-git-url)
  - [Install via OpenUPM](#install-via-openupm)
- [.NET Core](#net-core)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Getting started
---
Install via [UPM package](#upm-package) or asset package(`UniTask.*.*.*.unitypackage`) available in [UniTask/releases](https://github.com/Cysharp/UniTask/releases) page.

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

    // await frame-based operation like coroutine
    await UniTask.DelayFrame(100); 

    // replacement of yield return new WaitForSeconds/WaitForSecondsRealtime
    await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);
    
    // yield any playerloop timing(PreUpdate, Update, LateUpdate, etc...)
    await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

    // replacement of yield return null
    await UniTask.Yield();
    await UniTask.NextFrame();

    // replacement of WaitForEndOfFrame(same as UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate))
    await UniTask.WaitForEndOfFrame();

    // replacement of yield return new WaitForFixedUpdate(same as UniTask.Yield(PlayerLoopTiming.FixedUpdate))
    await UniTask.WaitForFixedUpdate();
    
    // replacement of yield return WaitUntil
    await UniTask.WaitUntil(() => isActive == false);

    // special helper of WaitUntil
    await UniTask.WaitUntilValueChanged(this, x => x.isActive);

    // You can await IEnumerator coroutine
    await FooCoroutineEnumerator();

    // You can await standard task
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

    // concurrent async-wait and get result easily by tuple syntax
    var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

    // shorthand of WhenAll, tuple can await directly
    var (google2, bing2, yahoo2) = await (task1, task2, task3);
    
    // You can handle timeout easily
    await GetTextAsync(UnityWebRequest.Get("http://unity.com")).Timeout(TimeSpan.FromMilliseconds(300));

    // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
    return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
}
```

Basics of UniTask and AsyncOperation
---
UniTask feature rely on C# 7.0([task-like custom async method builder feature](https://github.com/dotnet/roslyn/blob/master/docs/features/task-types.md)) so required Unity version is after `Unity 2018.3`, officialy lower support version is `Unity 2018.4.13f1`.

Why UniTask(custom task-like object) is required? Because Task is too heavy, not matched to Unity threading(single-thread). UniTask does not use thread and SynchronizationContext/ExecutionContext because almost Unity's asynchronous object is automaticaly dispatched by Unity's engine layer. It acquires more fast and more less allocation, completely integrated with Unity.

You can await `AsyncOperation`, `ResourceRequest`, `AssetBundleRequest`, `AssetBundleCreateRequest`, `UnityWebRequestAsyncOperation`, `IEnumerator` and others when `using Cysharp.Threading.Tasks;`.

UniTask provides three pattern of extension methods.

```csharp
* await asyncOperation;
* .WithCancellation(CancellationToken);
* .ToUniTask(IProgress, PlayerLoopTiming, CancellationToken);
```

`WithCancellation` is a simple version of `ToUniTask`, both returns `UniTask`. Details of cancellation, see: [Cancellation and Exception handling](#cancellation-and-exception-handling) section.

> Note: WithCancellation is returned from native timing of PlayerLoop but ToUniTask is returned from specified PlayerLoopTiming. Details of timing, see: [PlayerLoop](#playerloop) section.

The type of `UniTask` can use utility like `UniTask.WhenAll`, `UniTask.WhenAny`. It is like Task.WhenAll/WhenAny but return type is more useful, returns value tuple so can deconsrtuct each result and pass multiple type.

```csharp
public class SceneAssets
{
    public SceneAssets()
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

You can convert Task -> UniTask: `AsUniTask`, `UniTask` -> `UniTask<AsyncUnit>`: `AsAsyncUnitUniTask`, `UniTask<T>` -> `UniTask`: `AsUniTask`. `UniTask<T>` -> `UniTask`'s conversion cost is free.

If you want to convert async to coroutine, you can use `.ToCoroutine()`, this is useful to use only allow coroutine system.

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

Store to the class field, you can use `UniTask.Lazy` that gurantee call multipletimes. `.Prevent()` allows for multiple calls (internally cached results). This is useful when multiple calls in a function scope.

Cancellation and Exception handling
---
Some UniTask factory methods have `CancellationToken cancellationToken = default` parameter. Andalso some async operation for unity have `WithCancellation(CancellationToken)` and `ToUniTask(..., CancellationToken cancellation = default)` extension methods. 

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

CancellationToken can create by `CancellationTokenSource` or MonoBehaviour's extension method `GetCancellationTokenOnDestroy`.

```csharp
// this CancellationToken lifecycle is same as GameObject.
await UniTask.DelayFrame(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
```

When detect cancellation, all methods throws `OperationCanceledException` and propagate to upstream. `OperationCanceledException` is special exception, if not handled this exception, finally it is propagated to `UniTaskScheduler.UnobservedTaskException`.

Default behaviour of received unhandled exception is write log as exception. Log level can change by `UniTaskScheduler.UnobservedExceptionWriteLogType`. If you want to change custom beavhiour, set action to `UniTaskScheduler.UnobservedTaskException.`

If you want to cancel behaviour in async UniTask method, throws `OperationCanceledException` manually.

```csharp
public async UniTask<int> FooAsync()
{
    await UniTask.Yield();
    throw new OperationCanceledException();
}
```

If you handle exception but want to ignore(propagete to global cancellation handling), use exception filter.

```csharp
public async UniTask<int> BarAsync()
{
    try
    {
        var x = await FooAsync();
        return x * 2;
    }
    catch (Exception ex) when (!(ex is OperationCanceledException))
    {
        return -1;
    }
}
```

throws/catch `OperationCanceledException` is slightly heavy, if you want to care performance, use `UniTask.SuppressCancellationThrow` to avoid OperationCanceledException throw. It returns `(bool IsCanceled, T Result)` instead of throw.

```csharp
var (isCanceled, _) = await UniTask.DelayFrame(10, cancellationToken: cts.Token).SuppressCancellationThrow();
if (isCanceled)
{
    // ...
}
```

Note: Only suppress throws if you call it directly into the most source method. Otherwise, the return value will be converted, but the entire pipeline will not be suppressed throws.

Progress
---
Some async operation for unity have `ToUniTask(IProgress<float> progress = null, ...)` extension methods. 

```csharp
var progress = Progress.Create<float>(x => Debug.Log(x));

var request = await UnityWebRequest.Get("http://google.co.jp")
    .SendWebRequest()
    .ToUniTask(progress: progress);
```

You should not use standard `new System.Progress<T>`, because it causes allocation every times. Use `Cysharp.Threading.Tasks.Progress` instead. This progress factory has two methods, `Create` and `CreateOnlyValueChanged`. `CreateOnlyValueChanged` calls only when progress value changed.

Implements IProgress interface to caller is more better, there is no allocation of lambda.

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
UniTask is run on custom [PlayerLoop](https://docs.unity3d.com/ScriptReference/LowLevel.PlayerLoop.html). UniTask's playerloop based method(such as `Delay`, `DelayFrame`, `asyncOperation.ToUniTask`, etc...) accepts this `PlayerLoopTiming`.

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
}
```

It indicates when to run, you can check [PlayerLoopList.md](https://gist.github.com/neuecc/bc3a1cfd4d74501ad057e49efcd7bdae) to Unity's default playerloop and injected UniTask's custom loop.

`PlayerLoopTiming.Update` is similar as `yield return null` in coroutine, but it is called before Update(Update and uGUI events(button.onClick, etc...) are called on `ScriptRunBehaviourUpdate`, yield return null is called on `ScriptRunDelayedDynamicFrameRate`). `PlayerLoopTiming.FixedUpdate` is similar as `WaitForFixedUpdate`, `PlayerLoopTiming.LastPostLateUpdate` is similar as `WaitForEndOfFrame` in coroutine.

`yield return null` and `UniTask.Yield` is similar but different. `yield return null` always return next frame but `UniTask.Yield` return next called, that is, call `UniTask.Yield(PlayerLoopTiming.Update)` on `PreUpdate`, it returns same frame. `UniTask.NextFrame()` gurantees return next frame, this would be expected to behave exactly the same as `yield return null`.

> UniTask.Yield(without CancellationToken) is a special type, returns `YieldAwaitable` and run on YieldRunner. It is most lightweight and faster.

AsyncOperation is returned from native timing. For example, await `SceneManager.LoadSceneAsync` is returned from `EarlyUpdate.UpdatePreloading` and after called, loaded scene's `Start` called from `EarlyUpdate.ScriptRunDelayedStartupFrame`. Also `await UnityWebRequest` is returned from `EarlyUpdate.ExecuteMainThreadJobs`.

In UniTask, await directly and `WithCancellation` use native timing, `ToUniTask` use specified timing. This is usually not a particular problem, but with `LoadSceneAsync`, causes different order of Start and continuation after await. so recommend not to use `LoadSceneAsync.ToUniTask`.

In stacktrace, you can check where is running in playerloop.

![image](https://user-images.githubusercontent.com/46207/83735571-83caea80-a68b-11ea-8d22-5e22864f0d24.png)

In default, UniTask's PlayerLoop is initialized at `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]`.

The order in which methods are called in BeforeSceneLoad is indeterminate, so if you want to use UniTask in other BeforeSceneLoad methods, you should try to initialize it before this.

```csharp
// AfterAssembliesLoaded is called before BeforeSceneLoad
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
public static void InitUniTaskLoop()
{
    var loop = PlayerLoop.GetCurrentPlayerLoop();
    Cysharp.Threading.Tasks.PlayerLoopHelper.Initialize(ref loop);
}
```

If you import Unity's `Entities` package, that reset custom player loop to default at `BeforeSceneLoad` and inject ECS's loop. When Unity call ECS's inject method after UniTask's initialize method, UniTask will no longer work.

To solve this issue, you can re-initialize UniTask PlayerLoop after ECS initialized.

```csharp
// Get ECS Loop.
var playerLoop = ScriptBehaviourUpdateOrder.CurrentPlayerLoop;

// Setup UniTask's PlayerLoop.
PlayerLoopHelper.Initialize(ref playerLoop);
```

async void vs async UniTaskVoid
---
`async void` is a standard C# taks system so does not run on UniTask systems. It is better not to use. `async UniTaskVoid` is a lightweight version of `async UniTask` because it does not have awaitable completion and report error immediately to `UniTaskScheduler.UnobservedTaskException`. If you don't require to await it(fire and forget), use `UniTaskVoid` is better. Unfortunately to dismiss warning, require to using with `Forget()`.

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

Also UniTask have `Forget` method, it is similar with UniTaskVoid and same effects with it. However still UniTaskVoid is more efficient if completely do not use await。

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

Using async lambda in register event, it is used `async void`. To avoid it, you can use `UniTask.Action` or `UniTask.UnityAction` that creates delegate via `async UniTaskVoid` lambda.

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

UniTaskTracker
---
useful for check(leak) UniTasks. You can open tracker window in `Window -> UniTask Tracker`.

![image](https://user-images.githubusercontent.com/46207/83527073-4434bf00-a522-11ea-86e9-3b3975b26266.png)

* Enable AutoReload(Toggle) - Reload automatically.
* Reload - Reload view.
* GC.Collect - Invoke GC.Collect.
* Enable Tracking(Toggle) - Start to track async/await UniTask. Performance impact: low.
* Enable StackTrace(Toggle) - Capture StackTrace when task is started. Performance impact: high.

For debug use, enable tracking and capture stacktrace is useful but it it decline performance. Recommended usage is enable both to find task leak, and when done, finally disable both.

External Assets
---
In default, UniTask supports DOTween and Addressables(`AsyncOperationHandle` and `AsyncOpereationHandle<T>` as awaitable).

For DOTween support, require to `com.demigiant.dotween` import from [OpenUPM](https://openupm.com/packages/com.demigiant.dotween/) or define `UNITASK_DOTWEEN_SUPPORT` to enable it.

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

AsyncEnumerable and Async LINQ
---
Unity 2020.2.0a12 supports C# 8.0 so you can use `await foreach`. This is the new Update notation in async era.

```csharp
// Unity 2020.2.0a12, C# 8.0
await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate(token))
{
    Debug.Log("Update() " + Time.frameCount);
}
```

In a C# 7.3 environment, you can use the `ForEachAsync` method to work in almost the same way.

```csharp
// C# 7.3(Unity 2018.3~)
await UniTaskAsyncEnumerable.EveryUpdate(token).ForEachAsync(_ =>
{
    Debug.Log("Update() " + Time.frameCount);
});
```

UniTaskAsyncEnumerable implements asynchronous LINQ, similar to LINQ in `IEnumerable<T>` or Rx in `IObservable<T>`. All standard LINQ query operators can be applied to asynchronous streams. For example, the following code shows how to apply a Where filter to a button-click asynchronous stream that runs once every two clicks.

```csharp
await okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
{
});
```

Fire and Forget style(for example, event handling), also you can use `Subscribe`.

```csharp
okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).Subscribe(_ =>
{
});
```

Async LINQ is enabled when `using Cysharp.Threading.Tasks.Linq;`, and `UniTaskAsyncEnumerable` is defined in `UniTask.Linq` asmdef.

It's closer to UniRx (Reactive Extensions), but UniTaskAsyncEnumerable is a pull-based asynchronous stream, whereas Rx was a push-based asynchronous stream. Note that although similar, the characteristics are different and the details behave differently along with them.

`UniTaskAsyncEnumerable` is the entry point like `Enumerbale`. In addition to the standard query operators, there are other generators for Unity such as `EveryUpdate`, `Timer`, `TimerFrame`, `Interval`, `IntervalFrame`, and `EveryValueChanged`. And also added additional UniTask original query operators like `Append`, `Prepend`, `DistinctUntilChanged`, `ToHashSet`, `Buffer`, `CombineLatest`, `Do`, `Never`, `ForEachAsync`, `Pairwise`, `Publish`, `Queue`, `Return`, `SkipUntilCanceled`, `TakeUntilCanceled`, `TakeLast`, `Subscribe`.

The method with Func as an argument has three additional overloads, `***Await`, `***AwaitWithCancellation`.

```csharp
Select(Func<T, TR> selector)
SelectAwait(Func<T, UniTask<TR>> selector)
SelectAwaitWithCancellation(Func<T, CancellationToken, UniTask<TR>> selector)
```

If you want to use the `async` method inside the func, use the `***Await` or `***AwaitWithCancellation`.

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

All MonoBehaviour message events can convert async-streams by `AsyncTriggers` that can enable by `using Cysharp.Threading.Tasks.Triggers;`.

```csharp
using Cysharp.Threading.Tasks.Triggers;

async UniTaskVoid MonitorCollision()
{
    await gameObject.OnCollisionEnterAsync();
    Debug.Log("Collision Enter");
    /* do anything */

    await gameObject.OnCollisionExitAsync();
    Debug.Log("Collision Exit");
}
```

Similar as uGUI event, AsyncTrigger can get by `GetAsync***Trigger` and trigger it self is UniTaskAsyncEnumerable.

```csharp
// use await multiple times, get AsyncTriggerHandler is more efficient.
using(var trigger = this.GetOnCollisionEnterAsyncHandler())
{
    await OnCollisionEnterAsync();
    await OnCollisionEnterAsync();
    await OnCollisionEnterAsync();
}

// every moves.
await this.GetAsyncMoveTrigger().ForEachAsync(axisEventData =>
{
});
```

`AsyncReactiveProperty`, `AsyncReadOnlyReactiveProperty` is UniTask version of UniTask's ReactiveProperty. `BindTo` extension method of `IUniTaskAsyncEnumerable<T>` for binding asynchronous stream values to Unity components(Text/Selectable/TMP/Text).

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

// also exists ToReadOnlyReactiveProperty
var rp2 = new AsyncReactiveProperty<int>(99);
var rorp = rp.CombineLatest(rp2, (x, y) => (x, y)).ToReadOnlyReactiveProperty();
```

A pull-type asynchronous stream does not get the next values until the asynchronous processing in the sequence is complete. This could spill data from push-type events such as buttons.

```csharp
// can not get click event during 3 seconds complete.
await button.OnClickAsAsyncEnumerable().ForEachAwaitAsync(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

It is useful(prevent double-click) but not useful in sometimes.

Using `Queue()` method, which will also queue events during asynchronous processing.

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
`Channel` is same as [System.Threading.Tasks.Channels](https://docs.microsoft.com/ja-jp/dotnet/api/system.threading.channels?view=netcore-3.1) that is similar as GoLang Channel.

Currently only supports multiple-producer, single-consumer unbounded channel. It can create by `Channel.CreateSingleConsumerUnbounded<T>()`.

For producer(`.Writer`), `TryWrite` to push value and `TryComplete` to complete channel. For consumer(`.Reader`), `TryRead`, `WaitToReadAsync`, `ReadAsync`, `Completion` and `ReadAllAsync` to read queued messages.

`ReadAllAsync` returns `IUniTaskAsyncEnumerable<T>` so query LINQ operators. Reader only allows single-consumer but use `.Publish()` query operator to enable multicast message. For example, make pub/sub utility.

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
Unity's `[UnityTest]` attribute can test coroutine(IEnumerator) but can not test async. `UniTask.ToCoroutine` bridges async/await to coroutine so you can test async method.

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

UniTask itself's unit test is written by Unity Test Runner and [Cysharp/RuntimeUnitTestToolkit](https://github.com/Cysharp/RuntimeUnitTestToolkit) to check on CI and IL2CPP working.

Compare with Standard Task API
---
UniTask has many standard Task-like APIs. This table shows what is the alternative apis.

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
| `Task.Run` | `UniTask.Run` |
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
UniTask is aggressively caching async promise object to achive zero allocation. In default, cache all promises but you can configure `TaskPool.SetMaxPoolSize` to your value, the value indicates cache size per type. `TaskPool.GetCacheSizeInfo` returns current cached object in pool.

```csharp
foreach (var (type, size) in TaskPool.GetCacheSizeInfo())
{
    Debug.Log(type + ":" + size);
}
```

> In UnityEditor profiler shows allocation of compiler generated AsyncStateMachine but it only occurs in debug(development) build. C# Compiler generate AsyncStateMachine as class on Debug build and as struct on Release build.

API References
---
UniTask's API References is hosted at [cysharp.github.io/UniTask](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.html) by [DocFX](https://dotnet.github.io/docfx/) and [Cysharp/DocfXTemplate](https://github.com/Cysharp/DocfxTemplate).

For example, UniTask's factory methods can see at [UniTask#methods](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.UniTask.html#methods-1). UniTaskAsyncEnumerable's factory/extension methods can see at [UniTaskAsyncEnumerable#methods](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.Linq.UniTaskAsyncEnumerable.html#methods-1).

UPM Package
---
### Install via git URL

After Unity 2019.3.4f1, Unity 2020.1a21, that support path query parameter of git package. You can add `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask` to Package Manager

![image](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

![image](https://user-images.githubusercontent.com/46207/83702872-e0f17c80-a648-11ea-8183-7469dcd4f810.png)

or add `"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask"` to `Packages/manifest.json`.

If you want to set a target version, UniTask is using `*.*.*` release tag so you can specify a version like `#2.0.19`. For example `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.0.19`.

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.cysharp.unitask
```

.NET Core
---
For .NET Core, use NuGet.

> PM> Install-Package [UniTask](https://www.nuget.org/packages/UniTask)

UniTask of .NET Core version is a subset of Unity UniTask, removed PlayerLoop dependent methods.

It runs at higher performance than the standard Task/ValueTask, but you should be careful to ignore the ExecutionContext/SynchronizationContext when using it. `AysncLocal` also does not work because it ignores ExecutionContext.

If you use UniTask internally, but provide ValueTask as an external API, you can write like the following(Inspired by [PooledAwait](https://github.com/mgravell/PooledAwait)).

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

Utility methods such as WhenAll which is equivalent to UniTask are provided as [Cysharp/ValueTaskSupplement](https://github.com/Cysharp/ValueTaskSupplement).

License
---
This library is under the MIT License.
