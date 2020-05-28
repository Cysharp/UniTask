using BenchmarkDotNet.Attributes;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Cysharp.Threading.Tasks;
using PooledAwait;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.CompilerServices;

//class Program
//{
//    static void Main(string[] args)
//    {
//        var switcher = new BenchmarkSwitcher(new[]
//        {
//            typeof(StandardBenchmark)
//        });

//#if DEBUG
//        var b = new StandardBenchmark();

//#else
//        switcher.Run(args);
//#endif
//    }
//}

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddDiagnoser(MemoryDiagnoser.Default);
        AddJob(Job.ShortRun.WithLaunchCount(1).WithIterationCount(1).WithWarmupCount(1)/*.RunOncePerIteration()*/);
    }
}

// borrowed from PooledAwait

[Config(typeof(BenchmarkConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class ComparisonBenchmarks
{
    // note: all the benchmarks use Task/Task<T> for the public API, because BenchmarkDotNet
    // doesn't work reliably with more exotic task-types (even just ValueTask fails); instead,
    // we'll obscure the cost of the outer awaitable by doing a relatively large number of
    // iterations, so that we're only really measuring the inner loop
    private const int InnerOps = 1000;

    public bool ConfigureAwait { get; set; } = false;

    [Benchmark(OperationsPerInvoke = InnerOps, Description = ".NET")]
    [BenchmarkCategory("Task<T>")]
    public async Task<int> ViaTaskT()
    {
        int sum = 0;
        for (int i = 0; i < InnerOps; i++)
            sum += await Inner(1, 2).ConfigureAwait(ConfigureAwait);
        return sum;

        static async Task<int> Inner(int x, int y)
        {
            int i = x;
            await Task.Yield();
            i *= y;
            await Task.Yield();
            return 5 * i;
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = ".NET")]
    [BenchmarkCategory("Task")]
    public async Task ViaTask()
    {
        for (int i = 0; i < InnerOps; i++)
            await Inner().ConfigureAwait(ConfigureAwait);

        static async Task Inner()
        {
            await Task.Yield();
            await Task.Yield();
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = ".NET")]
    [BenchmarkCategory("ValueTask<T>")]
    public async Task<int> ViaValueTaskT()
    {
        int sum = 0;
        for (int i = 0; i < InnerOps; i++)
            sum += await Inner(1, 2).ConfigureAwait(ConfigureAwait);
        return sum;

        static async ValueTask<int> Inner(int x, int y)
        {
            int i = x;
            await Task.Yield();
            i *= y;
            await Task.Yield();
            return 5 * i;
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = ".NET")]
    [BenchmarkCategory("ValueTask")]
    public async Task ViaValueTask()
    {
        for (int i = 0; i < InnerOps; i++)
            await Inner().ConfigureAwait(ConfigureAwait);

        static async ValueTask Inner()
        {
            await Task.Yield();
            await Task.Yield();
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = "Pooled")]
    [BenchmarkCategory("ValueTask<T>")]
    public async Task<int> ViaPooledValueTaskT()
    {
        int sum = 0;
        for (int i = 0; i < InnerOps; i++)
            sum += await Inner(1, 2).ConfigureAwait(ConfigureAwait);
        return sum;

        static async PooledValueTask<int> Inner(int x, int y)
        {
            int i = x;
            await Task.Yield();
            i *= y;
            await Task.Yield();
            return 5 * i;
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = "Pooled")]
    [BenchmarkCategory("ValueTask")]
    public async Task ViaPooledValueTask()
    {
        for (int i = 0; i < InnerOps; i++)
            await Inner().ConfigureAwait(ConfigureAwait);

        static async PooledValueTask Inner()
        {
            await Task.Yield();
            await Task.Yield();
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = "Pooled")]
    [BenchmarkCategory("Task<T>")]
    public async Task<int> ViaPooledTaskT()
    {
        int sum = 0;
        for (int i = 0; i < InnerOps; i++)
            sum += await Inner(1, 2).ConfigureAwait(ConfigureAwait);
        return sum;

        static async PooledTask<int> Inner(int x, int y)
        {
            int i = x;
            await Task.Yield();
            i *= y;
            await Task.Yield();
            return 5 * i;
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = "Pooled")]
    [BenchmarkCategory("Task")]
    public async Task ViaPooledTask()
    {
        for (int i = 0; i < InnerOps; i++)
            await Inner().ConfigureAwait(ConfigureAwait);

        static async PooledTask Inner()
        {
            await Task.Yield();
            await Task.Yield();
        }
    }

    // ---

    //[Benchmark(OperationsPerInvoke = InnerOps, Description = "UniTaskVoid")]
    //[BenchmarkCategory("UniTask")]
    //public async Task ViaUniTaskVoid()
    //{
    //    for (int i = 0; i < InnerOps; i++)
    //    {
    //        await Inner();
    //    }

    //    static async UniTaskVoid Inner()
    //    {
    //        await UniTask.Yield();
    //        await UniTask.Yield();
    //    }
    //}

    [Benchmark(OperationsPerInvoke = InnerOps, Description = "UniTask")]
    [BenchmarkCategory("UniTask")]
    public async Task ViaUniTask()
    {
        for (int i = 0; i < InnerOps; i++)
        {
            await Inner();
        }

        static async UniTask Inner()
        {
            await UniTask.Yield();
            await UniTask.Yield();
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps, Description = "UniTaskT")]
    [BenchmarkCategory("UniTask")]
    public async Task<int> ViaUniTaskT()
    {
        var sum = 0;
        for (int i = 0; i < InnerOps; i++)
        {
            sum += await Inner(1, 2);
        }
        return sum;

        static async UniTask<int> Inner(int x, int y)
        {
            int i = x;
            await UniTask.Yield();
            i *= y;
            await UniTask.Yield();
            return 5 * i;
        }
    }
}

public struct MyAwaiter : ICriticalNotifyCompletion
{
    public MyAwaiter GetAwaiter() => this;

    public bool IsCompleted => false;

    public void GetResult()
    {
    }

    public void OnCompleted(Action continuation)
    {
        continuation();
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        continuation();
    }
}

public struct MyTestStateMachine : IAsyncStateMachine
{
    public void MoveNext()
    {
        //throw new NotImplementedException();




    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        //throw new NotImplementedException();
    }
}