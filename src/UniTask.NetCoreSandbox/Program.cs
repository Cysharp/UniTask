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

namespace NetCoreSandbox
{
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


        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();


            await foreach (var item in UniTaskAsyncEnumerable.Range(1, 3).WithCancellation(cts.Token))
            {
                Console.WriteLine(item);
                cts.Cancel();
            }


            /*
            .ForEachAsync(x =>
           {
               if (x == 2) throw new Exception();

               Console.WriteLine(x);
           });
           */





        }



        void Foo()
        {
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
