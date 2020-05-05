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
            var v = await DoAsync().AsTask();

            Console.WriteLine("Bar:" + v);
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
