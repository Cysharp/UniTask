UniTask
===
[![GitHub Actions](https://github.com/Cysharp/UniTask/workflows/Build-Debug/badge.svg)](https://github.com/Cysharp/UniTask/actions) [![Releases](https://img.shields.io/github/release/Cysharp/UniTask.svg)](https://github.com/Cysharp/UniTask/releases)

为Unity提供一个高性能，零堆内存分配的 async/await 异步方案。

- 基于值类型的`UniTask<T>`和自定义的 AsyncMethodBuilder 来实现零堆内存分配
- 使所有 Unity 的 AsyncOperations 和 Coroutines 可等待
- 基于 PlayerLoop 的任务（`UniTask.Yield`，`UniTask.Delay`，`UniTask.DelayFrame`等..）可以替换所有协程操作
- 对 MonoBehaviour 消息事件和 uGUI 事件进行可等待/异步枚举扩展
- 完全在 Unity 的 PlayerLoop 上运行，因此不使用Thread，并且同样能在 WebGL、wasm 等平台上运行。
- 带有 Channel 和 AsyncReactiveProperty 的异步 LINQ
- 提供一个 TaskTracker EditorWindow 以追踪所有 UniTask 分配来预防内存泄漏
- 与原生 Task/ValueTask/IValueTaskSource 高度兼容的行为

有关技术细节，请参阅博客文章：[UniTask v2 — 适用于 Unity 的零堆内存分配的async/await，支持异步 LINQ](https://medium.com/@neuecc/unitask-v2-zero-allocation-async-await-for-unity-with-asynchronous-linq-1aa9c96aa7dd)  
有关高级技巧，请参阅博客文章：[通过异步装饰器模式扩展 UnityWebRequest — UniTask 的高级技术](https://medium.com/@neuecc/extends-unitywebrequest-via-async-decorator-pattern-advanced-techniques-of-unitask-ceff9c5ee846)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## 目录

- [入门](#%E5%85%A5%E9%97%A8)
- [UniTask 和 AsyncOperation 的基础知识](#unitask-%E5%92%8C-asyncoperation-%E7%9A%84%E5%9F%BA%E7%A1%80%E7%9F%A5%E8%AF%86)
- [取消和异常处理](#%E5%8F%96%E6%B6%88%E5%92%8C%E5%BC%82%E5%B8%B8%E5%A4%84%E7%90%86)
- [超时处理](#%E8%B6%85%E6%97%B6%E5%A4%84%E7%90%86)
- [进度](#%E8%BF%9B%E5%BA%A6)
- [PlayerLoop](#playerloop)
- [async void 与 async UniTaskVoid 对比](#async-void-%E4%B8%8E-async-unitaskvoid-%E5%AF%B9%E6%AF%94)
- [UniTaskTracker](#unitasktracker)
- [外部拓展](#%E5%A4%96%E9%83%A8%E6%8B%93%E5%B1%95)
- [AsyncEnumerable 和 Async LINQ](#asyncenumerable-%E5%92%8C-async-linq)
- [可等待事件](#%E5%8F%AF%E7%AD%89%E5%BE%85%E4%BA%8B%E4%BB%B6)
- [Channel](#channel)
- [与 Awaitable 对比](#%E4%B8%8E-awaitable-%E5%AF%B9%E6%AF%94)
- [单元测试](#%E5%8D%95%E5%85%83%E6%B5%8B%E8%AF%95)
- [线程池的限制](#%E7%BA%BF%E7%A8%8B%E6%B1%A0%E7%9A%84%E9%99%90%E5%88%B6)
- [IEnumerator.ToUniTask 的限制](#ienumeratortounitask-%E7%9A%84%E9%99%90%E5%88%B6)
- [关于 UnityEditor](#%E5%85%B3%E4%BA%8E-unityeditor)
- [与原生 Task API 对比](#%E4%B8%8E%E5%8E%9F%E7%94%9F-task-api-%E5%AF%B9%E6%AF%94)
- [池化配置](#%E6%B1%A0%E5%8C%96%E9%85%8D%E7%BD%AE)
- [Profiler 下的堆内存分配](#profiler-%E4%B8%8B%E7%9A%84%E5%A0%86%E5%86%85%E5%AD%98%E5%88%86%E9%85%8D)
- [UniTaskSynchronizationContext](#unitasksynchronizationcontext)
- [API 文档](#api-%E6%96%87%E6%A1%A3)
- [UPM 包](#upm-%E5%8C%85)
- [通过 git URL 安装](#%E9%80%9A%E8%BF%87-git-url-%E5%AE%89%E8%A3%85)
- [关于 .NET Core](#%E5%85%B3%E4%BA%8E-net-core)
- [许可证](#%E8%AE%B8%E5%8F%AF%E8%AF%81)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

入门
---
通过[UniTask/releases](https://github.com/Cysharp/UniTask/releases)页面中提供的[UPM 包](https://github.com/Cysharp/UniTask#upm-package)或资产包（`UniTask.*.*.*.unitypackage`）安装。

```csharp
// 使用 UniTask 所需的命名空间
using Cysharp.Threading.Tasks;

// 您可以返回一个形如 UniTask<T>(或 UniTask) 的类型，这种类型事为Unity定制的，作为替代原生 Task<T> 的轻量级方案
// 为 Unity 集成的零堆内存分配，快速调用，0消耗的 async/await 方案
async UniTask<string> DemoAsync()
{
    // 您可以等待一个 Unity 异步对象
    var asset = await Resources.LoadAsync<TextAsset>("foo");
    var txt = (await UnityWebRequest.Get("https://...").SendWebRequest()).downloadHandler.text;
    await SceneManager.LoadSceneAsync("scene2");

    // .WithCancellation 会启用取消功能，GetCancellationTokenOnDestroy 表示获取一个依赖对象生命周期的 Cancel 句柄，当对象被销毁时，将会调用这个 Cancel 句柄，从而实现取消的功能
    // 在 Unity 2022.2之后，您可以在 MonoBehaviour 中使用`destroyCancellationToken`
    var asset2 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(this.GetCancellationTokenOnDestroy());

    // .ToUniTask 可接收一个 progress 回调以及一些配置参数，Progress.Create 是 IProgress<T> 的轻量级替代方案
    var asset3 = await Resources.LoadAsync<TextAsset>("baz").ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

    // 等待一个基于帧的延时操作（就像一个协程一样）
    await UniTask.DelayFrame(100); 

    // yield return new WaitForSeconds/WaitForSecondsRealtime 的替代方案
    await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);
    
    // 可以等待任何 playerloop 的生命周期（PreUpdate，Update，LateUpdate等）
    await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

    // yield return null 的替代方案
    await UniTask.Yield();
    await UniTask.NextFrame();

    // WaitForEndOfFrame 的替代方案
#if UNITY_2023_1_OR_NEWER
    await UniTask.WaitForEndOfFrame();
#else
    // 需要 MonoBehaviour（CoroutineRunner）
    await UniTask.WaitForEndOfFrame(this); // this是一个 MonoBehaviour
#endif
    
    // yield return new WaitForFixedUpdate 的替代方案，（等同于 UniTask.Yield(PlayerLoopTiming.FixedUpdate)）
    await UniTask.WaitForFixedUpdate();
    
    // yield return WaitUntil 的替代方案
    await UniTask.WaitUntil(() => isActive == false);

    // WaitUntil 扩展，指定某个值改变时触发
    await UniTask.WaitUntilValueChanged(this, x => x.isActive);

    // 您可以直接 await 一个 IEnumerator 协程
    await FooCoroutineEnumerator();

    // 您可以直接 await 一个原生 task
    await Task.Run(() => 100);

    // 多线程示例，在此行代码后的内容都运行在一个线程池上
    await UniTask.SwitchToThreadPool();

    /* 工作在线程池上的代码 */

    // 转回主线程（等同于 UniRx 的`ObserveOnMainThread`）
    await UniTask.SwitchToMainThread();

    // 获取异步的 webrequest
    async UniTask<string> GetTextAsync(UnityWebRequest req)
    {
        var op = await req.SendWebRequest();
        return op.downloadHandler.text;
    }

    var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
    var task2 = GetTextAsync(UnityWebRequest.Get("http://bing.com"));
    var task3 = GetTextAsync(UnityWebRequest.Get("http://yahoo.com"));

    // 构造一个 async-wait，并通过元组语义轻松获取所有结果
    var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

    // WhenAll 的简写形式，元组可以直接 await
    var (google2, bing2, yahoo2) = await (task1, task2, task3);

    // 返回一个异步值，或者您也可以使用`UniTask`（无结果），`UniTaskVoid`（不可等待）
    return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
}
```

UniTask 和 AsyncOperation 的基础知识
---
UniTask 功能依赖于 C# 7.0（[task-like custom async method builder feature](https://github.com/dotnet/roslyn/blob/master/docs/features/task-types.md)），所以需要`Unity 2018.3`之后的版本，官方支持的最低版本是`Unity 2018.4.13f1`。

为什么需要 UniTask（自定义task对象）？因为原生 Task 太重，与 Unity 线程（单线程）相性不好。因为 Unity 的异步对象由 Unity 的引擎层自动调度，所以 UniTask 不使用线程和 SynchronizationContext/ExecutionContext。它实现了更快和更低的分配，并且与Unity完全兼容。

您可以在使用`using Cysharp.Threading.Tasks;`时对`AsyncOperation`，`ResourceRequest`，`AssetBundleRequest`，`AssetBundleCreateRequest`，`UnityWebRequestAsyncOperation`，`AsyncGPUReadbackRequest`，`IEnumerator`以及其他的异步操作进行 await

UniTask 提供了三种模式的扩展方法。

```csharp
await asyncOperation;
.WithCancellation(CancellationToken);
.ToUniTask(IProgress, PlayerLoopTiming, CancellationToken);
```

`WithCancellation`是`ToUniTask`的简化版本，两者都返回`UniTask`。有关 cancellation 的详细信息，请参阅：[取消和异常处理](https://github.com/Cysharp/UniTask#cancellation-and-exception-handling)部分。

> 注意：await 会在 PlayerLoop 执行await对象的相应native生命周期方法时返回（如果条件满足的话），而 WithCancellation 和 ToUniTask 是从指定的 PlayerLoop 生命周期执行时返回。有关 PlayLoop生命周期 的详细信息，请参阅：[PlayerLoop](https://github.com/Cysharp/UniTask#playerloop)部分。

> 注意： AssetBundleRequest 有`asset`和`allAssets`，默认 await 返回`asset`。如果您想得到`allAssets`，您可以使用`AwaitForAllAssets()`方法。

`UniTask`可以使用`UniTask.WhenAll`，`UniTask.WhenAny`，`UniTask.WhenEach`等实用函数。它们就像`Task.WhenAll`和`Task.WhenAny`，但它们返回的数据类型更好用。它们会返回值元组，因此您可以传递多种类型并解构每个结果。

```csharp
public async UniTaskVoid LoadManyAsync()
{
    // 并行加载.
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

如果您想要将一个回调转换为 UniTask，您可以使用`UniTaskCompletionSource<T>`，它是`TaskCompletionSource<T>`的轻量级版本。

```csharp
public UniTask<int> WrapByUniTaskCompletionSource()
{
    var utcs = new UniTaskCompletionSource<int>();

    // 当操作完成时，调用 utcs.TrySetResult();
    // 当操作失败时，调用 utcs.TrySetException();
    // 当操作取消时，调用 utcs.TrySetCanceled();

    return utcs.Task; //本质上就是返回了一个 UniTask<int>
}
```

您可以进行如下转换：<br>-`Task` -> `UniTask `：使用`AsUniTask`<br>-`UniTask` -> `UniTask<AsyncUnit>`：使用 `AsAsyncUnitUniTask`<br>-`UniTask<T>` -> `UniTask`：使用 `AsUniTask`。`UniTask<T>` -> `UniTask`的转换是无消耗的。

如果您想将异步转换为协程，您可以使用`.ToCoroutine()`，这对于您想只允许使用协程系统大有帮助。

UniTask 不能 await 两次。这是与.NET Standard 2.1 中引入的[ValueTask/IValueTaskSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1?view=netcore-3.1)具有相同的约束。

> 千万不要对 `ValueTask<TResult>` 实例执行以下操作：
>
> - 多次await实例。
> - 多次调用 AsTask。
> - 在操作尚未完成时调用 .Result 或 .GetAwaiter().GetResult()，或对它们进行多次调用。
> - 对实例进行上述多种操作。
>
> 如果您执行了上述任何操作，则结果是未定义的。

```csharp
var task = UniTask.DelayFrame(10);
await task;
await task; // 错误，抛出异常
```

如果实在需要多次 await 一个异步操作，可以使用支持多次调用的`UniTask.Lazy`。`.Preserve()`同样允许多次调用（由 UniTask 内部缓存结果）。这种方法在函数范围内有多次调用时很有用。

同样的，`UniTaskCompletionSource`可以在同一个地方被 await 多次，或者在很多不同的地方被 await。

取消和异常处理
---
一些 UniTask 工厂方法中有一个`CancellationToken cancellationToken = default`参数。Unity 的一些异步操作也有`WithCancellation(CancellationToken)`和`ToUniTask(..., CancellationToken cancellation = default)`扩展方法。

可以通过原生的[`CancellationTokenSource`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource)将 CancellationToken 传递给参数

```csharp
var cts = new CancellationTokenSource();

cancelButton.onClick.AddListener(() =>
{
    cts.Cancel();
});

await UnityWebRequest.Get("http://google.co.jp").SendWebRequest().WithCancellation(cts.Token);

await UniTask.DelayFrame(1000, cancellationToken: cts.Token);
```

CancellationToken 可通过`CancellationTokenSource`或 MonoBehaviour 的扩展方法`GetCancellationTokenOnDestroy`来创建。

```csharp
// 这个 CancellationToken 的生命周期与 GameObject 的相同
await UniTask.DelayFrame(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
```

对于链式取消，建议所有异步方法的最后一个参数都接受`CancellationToken cancellationToken`，并将`CancellationToken`从头传递到尾。

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

`CancellationToken`代表了异步操作的生命周期。您可以不使用默认的 CancellationTokenOnDestroy ，通过自定义的`CancellationToken`自行管理生命周期。

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

在Unity 2022.2之后，Unity在[MonoBehaviour.destroyCancellationToken](https://docs.unity3d.com/ScriptReference/MonoBehaviour-destroyCancellationToken.html)和[Application.exitCancellationToken](https://docs.unity3d.com/ScriptReference/Application-exitCancellationToken.html)中添加了 CancellationToken。

当检测到取消时，所有方法都会向上游抛出并传播`OperationCanceledException`。当异常（不限于`OperationCanceledException`）没有在异步方法中处理时，它将被传播到`UniTaskScheduler.UnobservedTaskException`。默认情况下，将接收到的未处理异常作为一般异常写入日志。可以使用`UniTaskScheduler.UnobservedExceptionWriteLogType`更改日志级别。若想对接收到未处理异常时的处理进行自定义，请为`UniTaskScheduler.UnobservedTaskException`设置一个委托

而`OperationCanceledException`是一种特殊的异常，会被`UnobservedTaskException`无视

如果要取消异步 UniTask 方法中的行为，请手动抛出`OperationCanceledException`。

```csharp
public async UniTask<int> FooAsync()
{
    await UniTask.Yield();
    throw new OperationCanceledException();
}
```

如果您只想处理异常，忽略取消操作（让其传播到全局处理 cancellation 的地方），请使用异常过滤器。

```csharp
public async UniTask<int> BarAsync()
{
    try
    {
        var x = await FooAsync();
        return x * 2;
    }
    catch (Exception ex) when (!(ex is OperationCanceledException)) // 在 C# 9.0 下改成 when (ex is not OperationCanceledException) 
    {
        return -1;
    }
}
```

抛出和捕获`OperationCanceledException`有点重度，如果比较在意性能开销，请使用`UniTask.SuppressCancellationThrow`以避免抛出 OperationCanceledException 。它将返回`(bool IsCanceled, T Result)`而不是抛出异常。

```csharp
var (isCanceled, _) = await UniTask.DelayFrame(10, cancellationToken: cts.Token).SuppressCancellationThrow();
if (isCanceled)
{
    // ...
}
```

注意：仅当您在源头处直接调用`UniTask.SuppressCancellationThrow`时才会抑制异常抛出。否则，返回值将被转换，且整个管道不会抑制异常抛出。

`UniTask.Yield`和`UniTask.Delay`等功能依赖于 Unity 的 PlayerLoop，它们在 PlayerLoop 中确定`CancellationToken`状态。
这意味着当`CancellationToken`被触发时，它们并不会立即取消。

如果要更改此行为，实现立即取消，可将`cancelImmediately`标志设置为 true。

```csharp
await UniTask.Yield(cancellationToken, cancelImmediately: true);
```

注意：比起默认行为，设置 `cancelImmediately` 为 true 并检测立即取消会有更多的性能开销。
这是因为它使用了`CancellationToken.Register`；这比在 PlayerLoop 中检查 CancellationToken 更重度。

超时处理
---
超时是取消的一种变体。您可以通过`CancellationTokenSouce.CancelAfterSlim(TimeSpan)`设置超时并将 CancellationToken 传递给异步方法。

```csharp
var cts = new CancellationTokenSource();
cts.CancelAfterSlim(TimeSpan.FromSeconds(5)); // 设置5s超时。

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

> `CancellationTokenSouce.CancelAfter`是一个原生的 api。但是在 Unity 中您不应该使用它，因为它依赖于线程计时器。`CancelAfterSlim`是 UniTask 的扩展方法，它使用 PlayerLoop 代替了线程计时器。

如果您想将超时与其他 cancellation 一起使用，请使用`CancellationTokenSource.CreateLinkedTokenSource`。

```csharp
var cancelToken = new CancellationTokenSource();
cancelButton.onClick.AddListener(()=>
{
    cancelToken.Cancel(); // 点击按钮后取消。
});

var timeoutToken = new CancellationTokenSource();
timeoutToken.CancelAfterSlim(TimeSpan.FromSeconds(5)); // 设置5s超时。

try
{
    // 链接 token
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

为减少每次调用异步方法时用于超时的 CancellationTokenSource 的堆内存分配，您可以使用 UniTask 的`TimeoutController`进行优化。

```csharp
TimeoutController timeoutController = new TimeoutController(); // 提前创建好，以便复用。

async UniTask FooAsync()
{
    try
    {
        // 您可以通过 timeoutController.Timeout(TimeSpan) 把超时设置传递到 cancellationToken。
        await UnityWebRequest.Get("http://foo").SendWebRequest()
            .WithCancellation(timeoutController.Timeout(TimeSpan.FromSeconds(5)));
        timeoutController.Reset(); // 当 await 完成后调用 Reset（停止超时计时器，并准备下一次复用）。
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

如果您想将超时结合其他取消源一起使用，请使用`new TimeoutController(CancellationToken)`.

```csharp
TimeoutController timeoutController;
CancellationTokenSource clickCancelSource;

void Start()
{
    this.clickCancelSource = new CancellationTokenSource();
    this.timeoutController = new TimeoutController(clickCancelSource);
}
```

注意：UniTask 有`.Timeout`，`.TimeoutWithoutException`方法，但如果可以的话，尽量不要使用这些方法，请传递`CancellationToken`。因为`.Timeout`是在任务外部执行，所以无法停止超时任务。`.Timeout`意味着超时后忽略结果。如果您将一个`CancellationToken`传递给该方法，它将从任务内部执行，因此可以停止正在运行的任务。

进度
---
一些 Unity 的异步操作具有`ToUniTask(IProgress<float> progress = null, ...)`的扩展方法。

```csharp
var progress = Progress.Create<float>(x => Debug.Log(x));

var request = await UnityWebRequest.Get("http://google.co.jp")
    .SendWebRequest()
    .ToUniTask(progress: progress);
```

您不应该使用原生的`new System.Progress<T>`，因为每次调用它都会产生堆内存分配。请改用`Cysharp.Threading.Tasks.Progress`。这个 progress 工厂类有两个方法，`Create`和`CreateOnlyValueChanged`。`CreateOnlyValueChanged`仅在进度值更新时调用。

为调用者实现 IProgress 接口会更好，这样不会因使用 lambda 而产生堆内存分配。

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
            .ToUniTask(progress: this);
    }
}
```

PlayerLoop
---
UniTask 运行在自定义的[PlayerLoop](https://docs.unity3d.com/ScriptReference/LowLevel.PlayerLoop.html)中。UniTask 中基于 PlayerLoop 的方法（如`Delay`、`DelayFrame`、`asyncOperation.ToUniTask`等）接受这个`PlayerLoopTiming`。

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

它表明了异步任务会在哪个时机运行，您可以查阅[PlayerLoopList.md](https://gist.github.com/neuecc/bc3a1cfd4d74501ad057e49efcd7bdae)以了解 Unity 的默认 PlayerLoop 以及注入的 UniTask 的自定义循环。

`PlayerLoopTiming.Update`与协程中的`yield return null`类似，但它会在`ScriptRunBehaviourUpdate`时，Update（Update 和 uGUI 事件(button.onClick等）之前被调用，而 yield return null 是在`ScriptRunDelayedDynamicFrameRate`时被调用。`PlayerLoopTiming.FixedUpdate`类似于`WaitForFixedUpdate`。

> `PlayerLoopTiming.LastPostLateUpdate`不等同于协程的`yield return new WaitForEndOfFrame()`。协程的 WaitForEndOfFrame 似乎在 PlayerLoop 完成后运行。一些需要协程结束帧的方法(`Texture2D.ReadPixels`，`ScreenCapture.CaptureScreenshotAsTexture`，`CommandBuffer`等)在 async/await 时无法正常工作。在这些情况下，请将 MonoBehaviour（用于运行协程）传递给`UniTask.WaitForEndOfFrame`。例如，`await UniTask.WaitForEndOfFrame(this);`是`yield return new WaitForEndOfFrame()`轻量级无堆内存分配的替代方案。

> 注意：在 Unity 2023.1或更高的版本中，`await UniTask.WaitForEndOfFrame();`不再需要 MonoBehaviour。因为它使用了`UnityEngine.Awaitable.EndOfFrameAsync`。

`yield return null`和`UniTask.Yield`相似但不同。`yield return null`总是返回下一帧但`UniTask.Yield`返回下一次调用。也就是说，`UniTask.Yield(PlayerLoopTiming.Update)`在 `PreUpdate`上调用，它返回同一帧。`UniTask.NextFrame()`保证返回下一帧，您可以认为它的行为与`yield return null`一致。

> UniTask.Yield（不带 CancellationToken）是一种特殊类型，返回`YieldAwaitable`并在 YieldRunner 上运行。它是最轻量和最快的。

`AsyncOperation`在原生生命周期返回。例如，await `SceneManager.LoadSceneAsync`在`EarlyUpdate.UpdatePreloading`时返回，在此之后，在`EarlyUpdate.ScriptRunDelayedStartupFrame`时调用已加载场景的`Start`方法。同样的，`await UnityWebRequest`在`EarlyUpdate.ExecuteMainThreadJobs`时返回。

在 UniTask 中，直接 await 使用的是原生生命周期，而`WithCancellation`和`ToUniTask`使用的特定的生命周期。这通常不会有问题，但对于`LoadSceneAsync`，它会导致`Start`方法与 await 之后的逻辑的执行顺序错乱。所以建议不要使用`LoadSceneAsync.ToUniTask`。

> 注意：在 Unity 2023.1或更高的版本中，当您使用新的`UnityEngine.Awaitable`方法（如`SceneManager.LoadSceneAsync`）时，请确保您的文件的 using 指令区域中包含`using UnityEngine;`。
> 这可以通过避免使用`UnityEngine.AsyncOperation`版本来防止编译错误。

在堆栈跟踪中，您可以检查它在 PlayerLoop 中的运行位置。

![image](https://user-images.githubusercontent.com/46207/83735571-83caea80-a68b-11ea-8d22-5e22864f0d24.png)

默认情况下，UniTask 的 PlayerLoop 在`[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]`初始化。

在 BeforeSceneLoad 中调用的方法，它们的执行顺序是不确定的，所以如果您想在其他 BeforeSceneLoad 方法中使用 UniTask，您应该尝试在此之前初始化好 PlayerLoop。

```csharp
// AfterAssembliesLoaded 表示将会在 BeforeSceneLoad 之前调用
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
public static void InitUniTaskLoop()
{
    var loop = PlayerLoop.GetCurrentPlayerLoop();
    Cysharp.Threading.Tasks.PlayerLoopHelper.Initialize(ref loop);
}
```

如果您导入了 Unity 的`Entities`包，则会在`BeforeSceneLoad`将自定义 PlayerLoop 重置为默认值，并注入 ECS 的循环。当 Unity 在 UniTask 的初始化方法执行之后调用了 ECS 的注入方法，UniTask 将不再起作用。

为了解决这个问题，您可以在 ECS 初始化后重新初始化 UniTask PlayerLoop。

```csharp
// 获取 ECS Loop。
var playerLoop = ScriptBehaviourUpdateOrder.CurrentPlayerLoop;

// 设置 UniTask PlayerLoop。
PlayerLoopHelper.Initialize(ref playerLoop);
```

您可以通过调用`PlayerLoopHelper.IsInjectedUniTaskPlayerLoop()`来诊断 UniTask 的 PlayerLoop 是否准备就绪。并且`PlayerLoopHelper.DumpCurrentPlayerLoop`还会将所有当前 PlayerLoop 记录到控制台。

```csharp
void Start()
{
    UnityEngine.Debug.Log("UniTaskPlayerLoop ready? " + PlayerLoopHelper.IsInjectedUniTaskPlayerLoop());
    PlayerLoopHelper.DumpCurrentPlayerLoop();
}
```

您可以通过移除未使用的 PlayerLoopTiming 注入来稍微优化循环成本。您可以在初始化时调用`PlayerLoopHelper.Initialize(InjectPlayerLoopTimings)`。

```csharp
var loop = PlayerLoop.GetCurrentPlayerLoop();
PlayerLoopHelper.Initialize(ref loop, InjectPlayerLoopTimings.Minimum); // Minimum 就是 Update | FixedUpdate | LastPostLateUpdate
```

`InjectPlayerLoopTimings`有三个预设，`All`，`Standard`（All 除 LastPostLateUpdate 外），`Minimum`（`Update | FixedUpdate | LastPostLateUpdate`）。默认为 All，您可以通过组合来自定义要注入的时机，例如`InjectPlayerLoopTimings.Update | InjectPlayerLoopTimings.FixedUpdate | InjectPlayerLoopTimings.PreLateUpdate`。

使用未注入`PlayerLoopTiming`的[Microsoft.CodeAnalysis.BannedApiAnalyzers](https://github.com/dotnet/roslyn-analyzers/blob/master/src/Microsoft.CodeAnalysis.BannedApiAnalyzers/BannedApiAnalyzers.Help.md)可能会出错。例如，您可以像下列方式那样，为`InjectPlayerLoopTimings.Minimum`设置`BannedSymbols.txt`

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

您可以将`RS0030`的严重性配置为错误。

![image](https://user-images.githubusercontent.com/46207/109150837-bb933880-77ac-11eb-85ba-4fd15819dbd0.png)

async void 与 async UniTaskVoid 对比
---
`async void`是一个原生的 C# 任务系统，因此它不在 UniTask 系统上运行。也最好不要使用它。`async UniTaskVoid`是`async UniTask`的轻量级版本，因为它没有等待完成并立即向`UniTaskScheduler.UnobservedTaskException`报告错误。如果您不需要等待（即发即弃），那么使用`UniTaskVoid`会更好。不幸的是，要解除警告，您需要在尾部添加`Forget()`。

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

UniTask 也有`Forget`方法，与`UniTaskVoid`类似且效果相同。如果您完全不需要使用`await`，那么使用`UniTaskVoid`会更高效。

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

要使用注册到事件的异步 lambda，请不要使用`async void`。您可以使用`UniTask.Action` 或 `UniTask.UnityAction`来代替，这两者都通过`async UniTaskVoid` lambda 来创建委托。

```csharp
Action actEvent;
UnityAction unityEvent; // UGUI 特供

// 这样是不好的: async void
actEvent += async () => { };
unityEvent += async () => { };

// 这样是可以的: 通过 lamada 创建 Action
actEvent += UniTask.Action(async () => { await UniTask.Yield(); });
unityEvent += UniTask.UnityAction(async () => { await UniTask.Yield(); });
```

`UniTaskVoid`也可以用在 MonoBehaviour 的`Start`方法中。

```csharp
class Sample : MonoBehaviour
{
    async UniTaskVoid Start()
    {
        // 异步初始化代码。
    }
}
```

UniTaskTracker
---
对于检查（泄露的）UniTasks 很有用。您可以在`Window -> UniTask Tracker`中打开跟踪器窗口。

![image](https://user-images.githubusercontent.com/46207/83527073-4434bf00-a522-11ea-86e9-3b3975b26266.png)

- Enable AutoReload(Toggle) - 自动重新加载。
- Reload - 重新加载视图（重新扫描内存中UniTask实例，并刷新界面）。
- GC.Collect - 调用 GC.Collect。
- Enable Tracking(Toggle) - 开始跟踪异步/等待 UniTask。性能影响：低。
- Enable StackTrace(Toggle) - 在任务启动时捕获 StackTrace。性能影响：高。

UniTaskTracker 仅用于调试用途，因为启用跟踪和捕获堆栈跟踪很有用，但会对性能产生重大影响。推荐的用法是只在查找任务泄漏时启用跟踪和堆栈跟踪，并在使用完毕后禁用它们。

外部拓展
---
默认情况下，UniTask 支持 TextMeshPro（`BindTo(TMP_Text)`和像原生 uGUI `InputField` 那样的事件扩展，如`TMP_InputField`）、DOTween（`Tween`作为可等待的）和 Addressables（`AsyncOperationHandle`和`AsyncOperationHandle<T>`作为可等待的）。

它们被定义在了如`UniTask.TextMeshPro`，`UniTask.DOTween`，`UniTask.Addressables`等单独的 asmdef文件中。

从包管理器中导入软件包时，会自动启用对 TextMeshPro 和 Addressables 的支持。
但对于 DOTween 的支持，则需要从[DOTWeen assets](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676r)中导入并定义脚本定义符号`UNITASK_DOTWEEN_SUPPORT`后才能启用。

```csharp
// 动画序列
await transform.DOMoveX(2, 10);
await transform.DOMoveZ(5, 20);

// 并行，并传递 cancellation 用于取消
var ct = this.GetCancellationTokenOnDestroy();

await UniTask.WhenAll(
    transform.DOMoveX(10, 3).WithCancellation(ct),
    transform.DOScale(10, 3).WithCancellation(ct));
```

DOTween 支持的默认行为（`await`，`WithCancellation`，`ToUniTask`） 会等待到 tween 被终止。它适用于 Complete(true/false) 和 Kill(true/false)。但是如果您想复用 tweens（`SetAutoKill(false)`），它就不能按预期工作。如果您想等待另一个时间点，Tween 中存在以下扩展方法，`AwaitForComplete`，`AwaitForPause`，`AwaitForPlay`，`AwaitForRewind`，`AwaitForStepComplete`。

AsyncEnumerable 和 Async LINQ
---
Unity 2020.2 支持 C# 8.0，因此您可以使用`await foreach`。这是异步时代的新更新符号。

```csharp
// Unity 2020.2，C# 8.0
await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(token))
{
    Debug.Log("Update() " + Time.frameCount);
}
```

在 C# 7.3 环境中，您可以使用`ForEachAsync`方法以几乎相同的方式工作。

```csharp
// C# 7.3(Unity 2018.3~)
await UniTaskAsyncEnumerable.EveryUpdate().ForEachAsync(_ =>
{
    Debug.Log("Update() " + Time.frameCount);
}, token);
```

`UniTask.WhenEach`类似于 .NET 9 的`Task.WhenEach`，它可以使用新的方式来等待多个任务。

```csharp
await foreach (var result in UniTask.WhenEach(task1, task2, task3))
{
    // 结果的类型为 WhenEachResult<T>。
    // 它包含 `T Result` or `Exception Exception`。
    // 您可以检查 `IsCompletedSuccessfully` 或 `IsFaulted` 以确定是访 `.Result` 还是 `.Exception`。
    // 如果希望在 `IsFaulted` 时抛出异常并在成功时获取结果，可以使用 `GetResult()`。
    Debug.Log(result.GetResult());
}
```

UniTaskAsyncEnumerable 实现了异步 LINQ，类似于 LINQ 的`IEnumerable<T>`或 Rx 的 `IObservable<T>`。所有标准 LINQ 查询运算符都可以应用于异步流。例如，以下代码展示了如何将 Where 过滤器应用于每两次单击运行一次的按钮点击异步流。

```csharp
await okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
{
});
```

即发即弃（Fire and Forget）风格（例如，事件处理），您也可以使用`Subscribe`。

```csharp
okButton.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).Subscribe(_ =>
{
});
```

在引入`using Cysharp.Threading.Tasks.Linq;`后，异步 LINQ 将被启用，并且`UniTaskAsyncEnumerable`在 asmdef 文件`UniTask.Linq`中定义。

它更接近 UniRx（Reactive Extensions），但 UniTaskAsyncEnumerable 是基于 pull 的异步流，而 Rx 是基于 push 的异步流。请注意，尽管它们相似，但特性不同，细节也有所不同。

`UniTaskAsyncEnumerable`是类似`Enumerable`的入口点。除了标准查询操作符之外，还为 Unity 提供了其他生成器，例如`EveryUpdate`、`Timer`、`TimerFrame`、`Interval`、`IntervalFrame`和`EveryValueChanged`。此外，还添加了 UniTask 原生的查询操作符，如`Append`，`Prepend`，`DistinctUntilChanged`，`ToHashSet`，`Buffer`，`CombineLatest`，`Do`，`Never`，`ForEachAsync`，`Pairwise`，`Publish`，`Queue`，`Return`，`SkipUntil`，`TakeUntil`，`SkipUntilCanceled`，`TakeUntilCanceled`，`TakeLast`，`Subscribe`。

以 Func 作为参数的方法具有三个额外的重载，另外两个是`***Await`和`***AwaitWithCancellation`。

```csharp
Select(Func<T, TR> selector)
SelectAwait(Func<T, UniTask<TR>> selector)
SelectAwaitWithCancellation(Func<T, CancellationToken, UniTask<TR>> selector)
```

如果在 func 内部使用`async`方法，请使用`***Await`或`***AwaitWithCancellation`。

如何创建异步迭代器：C# 8.0 支持异步迭代器（`async yield return`），但它只允许`IAsyncEnumerable<T>`，当然也需要 C# 8.0。UniTask 支持使用`UniTaskAsyncEnumerable.Create`方法来创建自定义异步迭代器。

```csharp
// IAsyncEnumerable，C# 8.0 异步迭代器。（请不要这样使用，因为 IAsyncEnumerable 不被 UniTask 所控制）。
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

// UniTaskAsyncEnumerable.Create 并用 `await writer.YieldAsync` 代替 `yield return`.
public IUniTaskAsyncEnumerable<int> MyEveryUpdate()
{
    // writer(IAsyncWriter<T>) 有 `YieldAsync(value)` 方法。
    return UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
    {
        var frameCount = 0;
        await UniTask.Yield();
        while (!token.IsCancellationRequested)
        {
            await writer.YieldAsync(frameCount++); // 代替 `yield return`
            await UniTask.Yield();
        }
    });
}
```

可等待事件
---
所有 uGUI 组件都实现了`***AsAsyncEnumerable`，以实现对事件的异步流的转换。

```csharp
async UniTask TripleClick()
{
    // 默认情况下，使用了button.GetCancellationTokenOnDestroy 来管理异步生命周期
    await button.OnClickAsync();
    await button.OnClickAsync();
    await button.OnClickAsync();
    Debug.Log("Three times clicked");
}

// 更高效的方法
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

// 使用异步 LINQ
async UniTask TripleClick(CancellationToken token)
{
    await button.OnClickAsAsyncEnumerable().Take(3).Last();
    Debug.Log("Three times clicked");
}

// 使用异步 LINQ
async UniTask TripleClick(CancellationToken token)
{
    await button.OnClickAsAsyncEnumerable().Take(3).ForEachAsync(_ =>
    {
        Debug.Log("Every clicked");
    });
    Debug.Log("Three times clicked, complete.");
}
```

所有 MonoBehaviour 消息事件均可通过`AsyncTriggers`转换成异步流，`AsyncTriggers`可通过引入`using Cysharp.Threading.Tasks.Triggers;`来启用。`AsyncTriggers`可以使用`GetAsync***Trigger`来创建，并将它作为 UniTaskAsyncEnumerable 来触发。

```csharp
var trigger = this.GetOnCollisionEnterAsyncHandler();
await trigger.OnCollisionEnterAsync();
await trigger.OnCollisionEnterAsync();
await trigger.OnCollisionEnterAsync();

// 每次移动触发。
await this.GetAsyncMoveTrigger().ForEachAsync(axisEventData =>
{
});
```

`AsyncReactiveProperty`，`AsyncReadOnlyReactiveProperty`是 UniTask 的 ReactiveProperty 版本。`BindTo`的`IUniTaskAsyncEnumerable<T>`扩展方法，可以把异步流值绑定到 Unity 组件（Text/Selectable/TMP/Text）。

```csharp
var rp = new AsyncReactiveProperty<int>(99);

// AsyncReactiveProperty 本身是 IUniTaskAsyncEnumerable，可以通过 LINQ 进行查询
rp.ForEachAsync(x =>
{
    Debug.Log(x);
}, this.GetCancellationTokenOnDestroy()).Forget();

rp.Value = 10; // 推送10给所有订阅者
rp.Value = 11; // 推送11给所有订阅者

// WithoutCurrent 忽略初始值
// BindTo 绑定 stream value 到 unity 组件.
rp.WithoutCurrent().BindTo(this.textComponent);

await rp.WaitAsync(); // 一直等待，直到下一个值被设置

// 同样支持 ToReadOnlyAsyncReactiveProperty
var rp2 = new AsyncReactiveProperty<int>(99);
var rorp = rp.CombineLatest(rp2, (x, y) => (x, y)).ToReadOnlyAsyncReactiveProperty(CancellationToken.None);
```

在序列中的异步处理完成之前，pull-based异步流不会获取下一个值。这可能会从按钮等推送类型的事件中溢出数据。

```csharp
// 在3s延迟结束前，无法获取 event
await button.OnClickAsAsyncEnumerable().ForEachAwaitAsync(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

它（在防止双击方面）是有用的，但有时也并非都有用。

使用`Queue()`方法在异步处理期间也会对事件进行排队。

```csharp
// 异步处理中对 message 进行排队
await button.OnClickAsAsyncEnumerable().Queue().ForEachAwaitAsync(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

或使用即发即弃风格的`Subscribe`。

```csharp
button.OnClickAsAsyncEnumerable().Subscribe(async x =>
{
    await UniTask.Delay(TimeSpan.FromSeconds(3));
});
```

Channel
---
`Channel`与[System.Threading.Tasks.Channels](https://docs.microsoft.com/en-us/dotnet/api/system.threading.channels?view=netcore-3.1)相同，类似于 GoLang Channel。

目前只支持多生产者、单消费者无界 Channel。它可以通过`Channel.CreateSingleConsumerUnbounded<T>()`来创建。

对于生产者(`.Writer`)，使用`TryWrite`来推送值，使用`TryComplete`来完成 Channel。对于消费者(`.Reader`)，使用`TryRead`、`WaitToReadAsync`、`ReadAsync`和`Completion`，`ReadAllAsync`来读取队列的消息。

`ReadAllAsync`返回`IUniTaskAsyncEnumerable<T>` 因此可以使用 LINQ 操作符。Reader 只允许单消费者，但可以使用`.Publish()`查询操作符来启用多播消息。例如，可以制作发布/订阅工具。

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

与 Awaitable 对比
---
Unity 6 引入了可等待类型[Awaitable](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Awaitable.html)。简而言之，Awaitable 可以被认为是 UniTask 的一个子集，并且事实上，Awaitable的设计也受 UniTask 的影响。它应该能够处理基于 PlayerLoop 的 await，池化 Task，以及支持以类似的方式使用`CancellationToken`进行取消。随着它被包含在标准库中，您可能想知道是继续使用 UniTask 还是迁移到 Awaitable。以下是简要指南。

首先，Awaitable 提供的功能与协程提供的功能相同。使用 await 代替`yield return`；`await NextFrameAsync()`代替`yield return null`；`WaitForSeconds`和`EndOfFrame`等价。然而，这只是两者之间的差异。就功能而言，它是基于协程的，缺乏基于 Task 的特性。在使用 async/await 的实际应用程序开发中，像`WhenAll`这样的操作是必不可少的。此外，UniTask 支持许多基于帧的操作（如`DelayFrame`）和更灵活的 PlayerLoopTiming 控制，这些在 Awaitable 中是不可用的。当然，它也没有跟踪器窗口。

因此，我推荐在应用程序开发中使用 UniTask。UniTask 是 Awaitable 的超集，并包含了许多基本特性。对于库开发，如果您希望避免外部依赖，可以使用 Awaitable 作为方法的返回类型。因为 Awaitable 可以使用`AsUniTask`转换为 UniTask，所以支持在 UniTask 库中处理基于 Awaitable 的功能。即便是在库开发中，如果您不需要担心依赖关系，使用 UniTask 也会是您的最佳选择。

单元测试
---
Unity 的`[UnityTest]`属性可以测试协程（IEnumerator）但不能测试异步。`UniTask.ToCoroutine`将 async/await 桥接到协程，以便您可以测试异步方法。

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

UniTask 自身的单元测试是使用 Unity Test Runner 和[Cysharp/RuntimeUnitTestToolkit](https://github.com/Cysharp/RuntimeUnitTestToolkit)编写的，以集成到 CI 中并检查 IL2CPP 是否正常工作。

## 线程池的限制

大多数 UniTask 方法在单个线程 (PlayerLoop) 上运行，只有`UniTask.Run`（等同于`Task.Run`）和`UniTask.SwitchToThreadPool`在线程池上运行。如果您使用线程池，它将无法与 WebGL 等平台兼容。

`UniTask.Run`现在已弃用。您可以改用`UniTask.RunOnThreadPool`。并且还要考虑是否可以使用`UniTask.Create`或`UniTask.Void`。

## IEnumerator.ToUniTask 的限制

您可以将协程（IEnumerator）转换为 UniTask（或直接 await），但它有一些限制。

- 不支持`WaitForEndOfFrame`，`WaitForFixedUpdate`，`Coroutine`
- 生命周期与`StartCoroutine`不一样，它使用指定的`PlayerLoopTiming`，并且默认情况下，`PlayerLoopTiming.Update`在 MonoBehaviour 的`Update`和`StartCoroutine`的循环之前执行。

如果您想要实现从协程到异步的完全兼容转换，请使用`IEnumerator.ToUniTask(MonoBehaviour coroutineRunner)`重载。它会在传入的 MonoBehaviour 实例中执行 StartCoroutine 并在 UniTask 中等待它完成。

## 关于 UnityEditor

UniTask 可以像编辑器协程一样在 Unity 编辑器上运行。但它有一些限制。

- UniTask.Delay 的 DelayType.DeltaTime、UnscaledDeltaTime 无法正常工作，因为它们无法在编辑器中获取 deltaTime。因此在 EditMode 下运行时，会自动将 DelayType 更改为能等待正确的时间的`DelayType.Realtime`。
- 所有 PlayerLoopTiming 都在`EditorApplication.update`生命周期上运行。
- 带`-quit`的`-batchmode`不起作用，因为 Unity 不会执行 `EditorApplication.update` 并在一帧后退出。因此，不要使用`-quit`并使用`EditorApplication.Exit(0)`手动退出。

与原生 Task API 对比
---
UniTask 有许多原生的类Task API。此表展示了两者相对应的 API。

使用原生类型。

| .NET 类型                   | UniTask 类型 | 
|---------------------------| --- |
| `IProgress<T>`            | --- |
| `CancellationToken`       | --- | 
| `CancellationTokenSource` | --- |

使用 UniTask 类型。

| .NET 类型 | UniTask 类型 | 
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
| `Task.WhenEach` | `UniTask.WhenEach` |
| `Task.CompletedTask` | `UniTask.CompletedTask` |
| `Task.FromException` | `UniTask.FromException` |
| `Task.FromResult` | `UniTask.FromResult` |
| `Task.FromCanceled` | `UniTask.FromCanceled` |
| `Task.ContinueWith` | `UniTask.ContinueWith` |
| `TaskScheduler.UnobservedTaskException` | `UniTaskScheduler.UnobservedTaskException` |

池化配置
---
UniTask 通过积极缓存异步 promise 对象实现零堆内存分配（有关技术细节，请参阅博客文章[UniTask v2 — 适用于 Unity 的零堆内存分配的async/await，支持异步 LINQ](https://medium.com/@neuecc/unitask-v2-zero-allocation-async-await-for-unity-with-asynchronous-linq-1aa9c96aa7dd)）。默认情况下，它缓存所有 promise，但您可以通过调用`TaskPool.SetMaxPoolSize`方法来自定义每种类型的最大缓存大小。`TaskPool.GetCacheSizeInfo`返回池中当前缓存的对象。

```csharp
foreach (var (type, size) in TaskPool.GetCacheSizeInfo())
{
    Debug.Log(type + ":" + size);
}
```

Profiler 下的堆内存分配
---
在 UnityEditor 中，能从 profiler 中看到编译器生成的 AsyncStateMachine 的堆内存分配，但它只出现在Debug（development）构建中。C# 编译器在Debug 构建时将 AsyncStateMachine 生成为类，而在Release 构建时将其生成为结构。

Unity 从2020.1版本开始支持代码优化选项（位于右下角）。

![](https://user-images.githubusercontent.com/46207/89967342-2f944600-dc8c-11ea-99fc-0b74527a16f6.png)

在开发构建中，您可以通过将 C# 编译器优化设置为 release 模式来移除 AsyncStateMachine 的堆内存分配。此优化选项也可以通过`Compilation.CompilationPipeline-codeOptimization`和`Compilation.CodeOptimization`来设置。

UniTaskSynchronizationContext
---
Unity 的默认 SynchronizationContext(`UnitySynchronizationContext`) 在性能方面表现不佳。UniTask 绕过`SynchronizationContext`(和`ExecutionContext`) 因此 UniTask 不使用它，但如果存在`async Task`，则仍然使用它。`UniTaskSynchronizationContext`是`UnitySynchronizationContext`性能更好的替代品。

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

这是一个可选的选择，并不总是推荐；`UniTaskSynchronizationContext`性能不如`async UniTask`，并且不是完整的 UniTask 替代品。它也不保证与`UnitySynchronizationContext`完全兼容

API 文档
---
UniTask 的 API 文档托管在[cysharp.github.io/UniTask](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.html)，使用[DocFX](https://dotnet.github.io/docfx/)和[Cysharp/DocfXTemplate](https://github.com/Cysharp/DocfxTemplate)生成。

例如，UniTask 的工厂方法可以在[UniTask#methods](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.UniTask.html#methods-1)中查阅。UniTaskAsyncEnumerable 的工厂方法和扩展方法可以在[UniTaskAsyncEnumerable#methods](https://cysharp.github.io/UniTask/api/Cysharp.Threading.Tasks.Linq.UniTaskAsyncEnumerable.html#methods-1)中查阅。

UPM 包
---
### 通过 git URL 安装

需要支持 git 包路径查询参数的 Unity 版本（Unity >= 2019.3.4f1，Unity >= 2020.1a21）。您可以在包管理器中添加`https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask`

![image](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

![image](https://user-images.githubusercontent.com/46207/83702872-e0f17c80-a648-11ea-8183-7469dcd4f810.png)

或在`Packages/manifest.json`中添加`"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask"` 。

UniTask 使用`*.*.*`发布标签来指定版本，因此如果您要设置指定版本，您可以在后面添加像`#2.1.0`这样的版本标签。例如`https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.1.0` 。


关于 .NET Core
---
对于 .NET Core，请使用 NuGet。

> PM> Install-Package [UniTask](https://www.nuget.org/packages/UniTask)

.NET Core 版本的 UniTask 是 Unity 版本的 UniTask 的子集，它移除了依赖 PlayerLoop 的方法。

相比于原生 Task 和 ValueTask，它能以更高的性能运行，但在使用时应注意忽略 ExecutionContext 和 SynchronizationContext。因为它忽略了 ExecutionContext，`AsyncLocal`也不起作用。

如果您在内部使用 UniTask，但将 ValueTask 作为外部 API 提供，您可以编写如下代码（受[PooledAwait](https://github.com/mgravell/PooledAwait)启发）。

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

// UniTask 不会返回到原生 SynchronizationContext，但可以使用 `ReturnToCurrentSynchronizationContext`来让他返回
public ValueTask TestAsync()
{
    await using (UniTask.ReturnToCurrentSynchronizationContext())
    {
        await UniTask.SwitchToThreadPool();
        // do anything..
    }
}
```

.NET Core 版本的 UniTask 是为了让用户在与 Unity 共享代码时（例如使用[CysharpOnion](https://github.com/Cysharp/MagicOnion/)），能够将 UniTask 用作接口。.NET Core 版本的 UniTask 使得代码共享更加顺畅。

[Cysharp/ValueTaskSupplement](https://github.com/Cysharp/ValueTaskSupplement)提供了一些实用方法，如 WhenAll，这些方法等效于 UniTask。

许可证
---
此库采用MIT许可证
