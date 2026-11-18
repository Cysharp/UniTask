using Cysharp.Threading.Tasks;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using Cysharp.Threading.Tasks.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests
{
    public class AsyncLazyTest
    {
        [Fact]
        public async Task LazyLazy()
        {
            {
                var l = UniTask.Lazy(() => After());
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                await a;
                await b;
                await c;
            }
            {
                var l = UniTask.Lazy(() => AfterException());
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                await Assert.ThrowsAsync<TaskTestException>(async () => await a);
                await Assert.ThrowsAsync<TaskTestException>(async () => await b);
                await Assert.ThrowsAsync<TaskTestException>(async () => await c);
            }
        }

        [Fact]
        public async Task LazyImmediate()
        {
            {
                var l = UniTask.Lazy(() => UniTask.FromResult(1).AsUniTask());
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                await a;
                await b;
                await c;
            }
            {
                var l = UniTask.Lazy(() => UniTask.FromException(new TaskTestException()));
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                await Assert.ThrowsAsync<TaskTestException>(async () => await a);
                await Assert.ThrowsAsync<TaskTestException>(async () => await b);
                await Assert.ThrowsAsync<TaskTestException>(async () => await c);
            }
        }

        [Fact]
        public void FactoryThrowIsCached()
        {
            var error = new TaskTestException();
            var calls = 0;
            var lazy = UniTask.Lazy(() =>
            {
                calls++;
                throw error;
            });

            Assert.Same(error, Assert.Throws<TaskTestException>(() => lazy.GetAwaiter().GetResult()));

            var task = lazy.Task;
            Assert.Equal(UniTaskStatus.Faulted, task.Status);
            Assert.Same(error, Assert.Throws<TaskTestException>(() => task.GetAwaiter().GetResult()));
            Assert.Equal(1, calls);
        }

        [Fact]
        public void FactoryFailureCallbackCanReadTaskOnAnotherThread()
        {
            var error = new TaskTestException();
            var readCompleted = false;
            var observedStatus = UniTaskStatus.Pending;
            AsyncLazy lazy = null;
            lazy = UniTask.Lazy(() =>
            {
                lazy.Task.GetAwaiter().OnCompleted(() =>
                {
                    var reader = new Thread(() => observedStatus = lazy.Task.Status) { IsBackground = true };
                    reader.Start();
                    readCompleted = reader.Join(TimeSpan.FromSeconds(10));
                });
                throw error;
            });

            var task = lazy.Task;

            Assert.True(readCompleted, "The failure continuation could not read the lazy task on another thread.");
            Assert.Equal(UniTaskStatus.Faulted, observedStatus);
            Assert.Same(error, Assert.Throws<TaskTestException>(() => task.GetAwaiter().GetResult()));
        }

        static async UniTask AwaitAwait(UniTask t)
        {
            await t;
        }


        async UniTask After()
        {
            await UniTask.Yield();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            await UniTask.Yield();
            await UniTask.Yield();
        }

        async UniTask AfterException()
        {
            await UniTask.Yield();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            await UniTask.Yield();
            throw new TaskTestException();
        }
    }

    public class AsyncLazyTest2
    {
        [Fact]
        public async Task LazyLazy()
        {
            {
                var l = UniTask.Lazy(() => After());
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                var a2 = await a;
                var b2 = await b;
                var c2 = await c;
                (a2, b2, c2).Should().Be((10, 10, 10));
            }
            {
                var l = UniTask.Lazy(() => AfterException());
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                await Assert.ThrowsAsync<TaskTestException>(async () => await a);
                await Assert.ThrowsAsync<TaskTestException>(async () => await b);
                await Assert.ThrowsAsync<TaskTestException>(async () => await c);
            }
        }

        [Fact]
        public async Task LazyImmediate()
        {
            {
                var l = UniTask.Lazy(() => UniTask.FromResult(1));
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                var a2 = await a;
                var b2 = await b;
                var c2 = await c;
                (a2, b2, c2).Should().Be((1, 1, 1));
            }
            {
                var l = UniTask.Lazy(() => UniTask.FromException<int>(new TaskTestException()));
                var a = AwaitAwait(l.Task);
                var b = AwaitAwait(l.Task);
                var c = AwaitAwait(l.Task);

                await Assert.ThrowsAsync<TaskTestException>(async () => await a);
                await Assert.ThrowsAsync<TaskTestException>(async () => await b);
                await Assert.ThrowsAsync<TaskTestException>(async () => await c);
            }
        }

        [Fact]
        public void FactoryThrowIsCached()
        {
            var error = new TaskTestException();
            var calls = 0;
            var lazy = UniTask.Lazy<int>(() =>
            {
                calls++;
                throw error;
            });

            Assert.Same(error, Assert.Throws<TaskTestException>(() => lazy.GetAwaiter().GetResult()));

            var task = lazy.Task;
            Assert.Equal(UniTaskStatus.Faulted, task.Status);
            Assert.Same(error, Assert.Throws<TaskTestException>(() => task.GetAwaiter().GetResult()));
            Assert.Equal(1, calls);
        }

        static async UniTask<int> AwaitAwait(UniTask<int> t)
        {
            return await t;
        }


        async UniTask<int> After()
        {
            await UniTask.Yield();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            await UniTask.Yield();
            await UniTask.Yield();
            return 10;
        }

        async UniTask<int> AfterException()
        {
            await UniTask.Yield();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            await UniTask.Yield();
            throw new TaskTestException();
        }
    }
}
