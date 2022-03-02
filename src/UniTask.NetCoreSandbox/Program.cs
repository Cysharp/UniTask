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
    public class Program
    {
        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();


            // OK.
            await FooAsync(10, cts.Token);

            // NG(Compiler Error)
            // await FooAsync(10);



            






        }

        static async UniTask FooAsync(int x, CancellationToken cancellationToken = default)
        {
            await UniTask.Yield();
        }
    }

}
