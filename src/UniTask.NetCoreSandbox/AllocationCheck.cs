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
using System.Collections.Concurrent;

[Config(typeof(BenchmarkConfig))]
public class AllocationCheck
{
    // note: all the benchmarks use Task/Task<T> for the public API, because BenchmarkDotNet
    // doesn't work reliably with more exotic task-types (even just ValueTask fails); instead,
    // we'll obscure the cost of the outer awaitable by doing a relatively large number of
    // iterations, so that we're only really measuring the inner loop
    private const int InnerOps = 1000;

    [Benchmark(OperationsPerInvoke = InnerOps)]
    public async Task ViaUniTask()
    {
        for (int i = 0; i < InnerOps; i++)
        {
            var a = Core();
            var b = Core();
            var c = Core();
            await a;
            await b;
            await c;
        }

        static async UniTask Core()
        {
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
        }
    }

    [Benchmark(OperationsPerInvoke = InnerOps)]
    public async Task<int> ViaUniTaskT()
    {
        var sum = 0;
        for (int i = 0; i < InnerOps; i++)
        {
            var a = Core();
            var b = Core();
            var c = Core();
            sum += await a;
            sum += await b;
            sum += await c;
        }
        return sum;

        static async UniTask<int> Core()
        {
            var a = await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 10);
            var b = await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 10);
            var c = await new TestAwaiter<int>(false, UniTaskStatus.Succeeded, 10);
            return 10;
        }
    }

    //[Benchmark(OperationsPerInvoke = InnerOps)]
    //[Benchmark]
    public void ViaUniTaskVoid()
    {
        for (int i = 0; i < InnerOps; i++)
        {
            Core().Forget();
            Core().Forget();
            Core().Forget();
        }

        static async UniTaskVoid Core()
        {
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
        }
    }

    struct Foo : IAsyncStateMachine
    {
        public AsyncUniTaskVoidMethodBuilder builder;
        public TestAwaiter awaiter;
        public TestAwaiter awaiterawaiter;

        public int state;

        public void MoveNext()
        {
            switch (state)
            {
                case -1:
                    awaiterawaiter = awaiter.GetAwaiter();
                    if (awaiterawaiter.IsCompleted)
                    {
                        goto case 0;
                    }
                    else
                    {
                        state = 0;
                        builder.AwaitUnsafeOnCompleted(ref awaiterawaiter, ref this);
                        return;
                    }

                case 0:
                default:
                    goto END;
            }

            END:
            builder.SetResult();
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {

        }
    }
}

public class TaskTestException : Exception
{

}

public struct TestAwaiter : ICriticalNotifyCompletion
{
    readonly UniTaskStatus status;
    readonly bool isCompleted;

    public TestAwaiter(bool isCompleted, UniTaskStatus status)
    {
        this.isCompleted = isCompleted;
        this.status = status;
    }

    public TestAwaiter GetAwaiter() => this;

    public bool IsCompleted => isCompleted;

    public void GetResult()
    {
        switch (status)
        {
            case UniTaskStatus.Faulted:
                throw new TaskTestException();
            case UniTaskStatus.Canceled:
                throw new OperationCanceledException();
            case UniTaskStatus.Pending:
            case UniTaskStatus.Succeeded:
            default:
                break;
        }
    }

    public void OnCompleted(Action continuation)
    {
        ThreadPool.UnsafeQueueUserWorkItem(ThreadPoolWorkItem.Create(continuation), false);
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        ThreadPool.UnsafeQueueUserWorkItem(ThreadPoolWorkItem.Create(continuation), false);
    }


}

public struct TestAwaiter<T> : ICriticalNotifyCompletion
{
    readonly UniTaskStatus status;
    readonly bool isCompleted;
    readonly T value;

    public TestAwaiter(bool isCompleted, UniTaskStatus status, T value)
    {
        this.isCompleted = isCompleted;
        this.status = status;
        this.value = value;
    }

    public TestAwaiter<T> GetAwaiter() => this;

    public bool IsCompleted => isCompleted;

    public T GetResult()
    {
        switch (status)
        {
            case UniTaskStatus.Faulted:
                throw new TaskTestException();
            case UniTaskStatus.Canceled:
                throw new OperationCanceledException();
            case UniTaskStatus.Pending:
            case UniTaskStatus.Succeeded:
            default:
                return value;
        }
    }

    public void OnCompleted(Action continuation)
    {
        ThreadPool.UnsafeQueueUserWorkItem(ThreadPoolWorkItem.Create(continuation), false);
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        ThreadPool.UnsafeQueueUserWorkItem(ThreadPoolWorkItem.Create(continuation), false);
    }
}

public sealed class ThreadPoolWorkItem : IThreadPoolWorkItem
{
    public static readonly ConcurrentQueue<ThreadPoolWorkItem> pool = new ConcurrentQueue<ThreadPoolWorkItem>();

    public static void CreatePoolItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pool.Enqueue(new ThreadPoolWorkItem());
        }
    }

    Action continuation;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ThreadPoolWorkItem Create(Action continuation)
    {
        if (!pool.TryDequeue(out var item))
        {
            item = new ThreadPoolWorkItem();
        }

        item.continuation = continuation;
        return item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute()
    {
        var call = continuation;
        continuation = null;
        pool.Enqueue(this);

        call.Invoke();
    }
}