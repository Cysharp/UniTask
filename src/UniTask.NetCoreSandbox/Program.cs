#pragma warning disable CS1998

using Cysharp.Threading.Tasks;

using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Linq;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace NetCoreSandbox
{
    public class MySyncContext : SynchronizationContext
    {
        public MySyncContext()
        {
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            Console.WriteLine("Called SyncContext Post!");
            base.Post(d, state);
        }
    }

    public class Text
    {

        public string text { get; set; }
    }

    public class ZeroAllocAsyncAwaitInDotNetCore
    {
        public ValueTask<int> NanikaAsync(int x, int y)
        {
            return Core(this, x, y);

            static async UniTask<int> Core(ZeroAllocAsyncAwaitInDotNetCore self, int x, int y)
            {
                // nanika suru...
                await Task.Delay(TimeSpan.FromSeconds(x + y));

                return 10;
            }
        }
    }


    public class TaskTestException : Exception
    {

    }

    class Foo
    {
        public async UniTask MethodFooAsync()
        {
            await MethodBarAsync();
        }

        private async UniTask MethodBarAsync()

        {
            Throw();
        }

        private void Throw()
        {
            throw new Exception();
        }
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
            ThreadPool.QueueUserWorkItem(_ => continuation(), null);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ => continuation(), null);
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
            ThreadPool.QueueUserWorkItem(_ => continuation(), null);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ => continuation(), null);
        }
    }


    public static partial class UnityUIComponentExtensions
    {
        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Text text)
        {
            AAAACORECORE(source, text).Forget();

            async UniTaskVoid AAAACORECORE(IUniTaskAsyncEnumerable<string> source2, Text text2)
            {
                var e = source2.GetAsyncEnumerator();
                try
                {
                    while (await e.MoveNextAsync())
                    {
                        text2.text = e.Current;
                        // action(e.Current);
                    }
                }
                finally
                {
                    if (e != null)
                    {
                        await e.DisposeAsync();
                    }
                }
            }
        }

        //public static IDisposable SubscribeToText<T>(this IObservable<T> source, Text text)
        //{
        //    return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
        //}

        //public static IDisposable SubscribeToText<T>(this IObservable<T> source, Text text, Func<T, string> selector)
        //{
        //    return source.SubscribeWithState2(text, selector, (x, t, s) => t.text = s(x));
        //}

        //public static IDisposable SubscribeToInteractable(this IObservable<bool> source, Selectable selectable)
        //{
        //    return source.SubscribeWithState(selectable, (x, s) => s.interactable = x);
        //}
    }

    class Program
    {
        static string FlattenGenArgs(Type type)
        {
            if (type.IsGenericType)
            {
                var t = string.Join(", ", type.GetGenericArguments().Select(x => FlattenGenArgs(x)));
                return Regex.Replace(type.Name, "`.+", "") + "<" + t + ">";
            }
            //x.ReturnType.GetGenericArguments()
            else
            {
                return type.Name;
            }
        }

        static async IAsyncEnumerable<int> FooAsync([EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            yield return 1;
            await Task.Delay(10, cancellationToken);
        }

        public class MyDisposable : IDisposable
        {
            public void Dispose()
            {

            }
        }

        static void Test()
        {
            var disp = new MyDisposable();

            using var _ = new MyDisposable();

            Console.WriteLine("tako");
        }


        static async UniTask FooBarAsync()
        {
            await using (UniTask.ReturnToCurrentSynchronizationContext())
            {
                await UniTask.SwitchToThreadPool();




            }
        }




        static async UniTask Aaa()
        {
            await FooBarAsync();

            Console.WriteLine("FooBarAsync End");
        }

        static async UniTask WhereSelect()
        {
            await foreach (var item in UniTaskAsyncEnumerable.Range(1, 10)
                .SelectAwait(async x =>
                {
                    await UniTask.Yield();
                    return x;
                })
                .Where(x => x % 2 == 0))
            {
                Console.WriteLine(item);
            }
        }


        static async Task Main(string[] args)
        {
#if !DEBUG
         
            


            //await new AllocationCheck().ViaUniTaskVoid();
            //Console.ReadLine();
            BenchmarkDotNet.Running.BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            //await new ComparisonBenchmarks().ViaUniTaskT();
            return;
#endif

            var e = UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Start {i}");
                    await writer.YieldAsync(i);
                    Console.WriteLine($"End {i}");
                }
            });

            var ee = e.GetAsyncEnumerator();
            while (await ee.MoveNextAsync())
            {
                Console.WriteLine("ForEach " + ee.Current);
            }



        }

        static async UniTask YieldCore()
        {
            await UniTask.Yield();
        }

#pragma warning disable CS1998


        static async UniTask<int> AsyncTest()
        {
            // empty 
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
            await new TestAwaiter(true, UniTaskStatus.Succeeded);
            await new TestAwaiter(false, UniTaskStatus.Succeeded);
            return 10;
        }



#pragma warning restore CS1998

        void Foo()
        {

            // AsyncEnumerable.Range(1,10).Do(

            // AsyncEnumerable.t

            var sb = new StringBuilder();
            sb.AppendLine(@"using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
");



            var chako = typeof(AsyncEnumerable).GetMethods()
                .OrderBy(x => x.Name)
                .Select(x =>
                {
                    var ret = FlattenGenArgs(x.ReturnType);


                    var generics = string.Join(", ", x.GetGenericArguments().Select(x => x.Name));

                    if (x.GetParameters().Length == 0) return "";

                    var self = x.GetParameters().First();
                    if (x.GetCustomAttributes(typeof(ExtensionAttribute), true).Length == 0)
                    {
                        return "";
                    }

                    var arg1Type = FlattenGenArgs(x.GetParameters().First().ParameterType);

                    var others = string.Join(", ", x.GetParameters().Skip(1).Select(y => FlattenGenArgs(y.ParameterType) + " " + y.Name));

                    if (!string.IsNullOrEmpty(others))
                    {
                        others = ", " + others;
                    }

                    var template = $"public static {ret} {x.Name}<{generics}>(this {arg1Type} {self.Name}{others})";



                    return template.Replace("ValueTask", "UniTask").Replace("IAsyncEnumerable", "IUniTaskAsyncEnumerable").Replace("<>", "");
                })
                .Where(x => x != "")
                .Select(x => x + "\r\n{\r\n    throw new NotImplementedException();\r\n}")
                .ToArray();

            var huga = string.Join("\r\n\r\n", chako);




            foreach (var item in typeof(AsyncEnumerable).GetMethods().Select(x => x.Name).Distinct())
            {
                if (item.EndsWith("AwaitAsync") || item.EndsWith("AwaitWithCancellationAsync") || item.EndsWith("WithCancellation"))
                {
                    continue;
                }

                var item2 = item.Replace("Async", "");
                item2 = item2.Replace("Await", "");

                var format = @"
    internal sealed class {0}
    {{
    }}
";

                sb.Append(string.Format(format, item2));

            }

            sb.Append("}");


            Console.WriteLine(sb.ToString());




        }


        public static async IAsyncEnumerable<int> AsyncGen()
        {
            await UniTask.SwitchToThreadPool();
            yield return 10;
            await UniTask.SwitchToThreadPool();
            yield return 100;
        }
    }

    class MyEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            return new MyEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class MyEnumerator : IEnumerator<int>
    {
        public int Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            Console.WriteLine("Called Dispose");
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }



    public class MyClass<T>
    {
        public CustomAsyncEnumerator<T> GetAsyncEnumerator()
        {
            //IAsyncEnumerable
            return new CustomAsyncEnumerator<T>();
        }
    }


    public struct CustomAsyncEnumerator<T>
    {
        int count;

        public T Current
        {
            get
            {
                return default;
            }
        }

        public UniTask<bool> MoveNextAsync()
        {
            if (count++ == 3)
            {
                return UniTask.FromResult(false);
                //return false;
            }
            return UniTask.FromResult(true);
        }

        public UniTask DisposeAsync()
        {
            return default;
        }
    }



}
