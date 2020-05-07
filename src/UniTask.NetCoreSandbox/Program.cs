using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;

namespace NetCoreSandbox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Foo");
            var v = await outer().AsTask();

            Console.WriteLine("Bar:" + v);
        }

        static async UniTask<int> outer()
        {
            //await Task.WhenAll();

            //var foo = await Task.WhenAny(Array.Empty<Task<int>>());


            await UniTask.WhenAny(new UniTask[0]);

            return 10;
            //var v = await DoAsync();
            //return v;
        }


        static async UniTask<int> DoAsync()
        {
            var tcs = new UniTaskCompletionSource<int>();

            tcs.TrySetResult(100);


            var v = await tcs.Task;

            return v;
        }


    }
}
