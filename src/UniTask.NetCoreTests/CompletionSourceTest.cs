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
    public class CompletionSourceTest
    {
        [Fact]
        public async Task SetFirst()
        {
            {
                var tcs = new UniTaskCompletionSource();

                tcs.TrySetResult();
                await tcs.Task; // ok.
                await tcs.Task; // ok.
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource();

                tcs.TrySetException(new TestException());

                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);

                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource();

                tcs.TrySetException(new OperationCanceledException(cts.Token));

                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);

                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }

            {
                var tcs = new UniTaskCompletionSource();

                tcs.TrySetCanceled(cts.Token);

                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);

                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task SingleOnFirst()
        {
            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();

                tcs.TrySetResult();
                await a;
                await tcs.Task; // ok.
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();

                tcs.TrySetException(new TestException());
                await Assert.ThrowsAsync<TestException>(async () => await a);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();

                tcs.TrySetException(new OperationCanceledException(cts.Token));
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();

                tcs.TrySetCanceled(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task MultiOne()
        {
            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();
                tcs.TrySetResult();
                await a;
                await b;
                await tcs.Task; // ok.
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();

                tcs.TrySetException(new TestException());
                await Assert.ThrowsAsync<TestException>(async () => await a);
                await Assert.ThrowsAsync<TestException>(async () => await b);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();

                tcs.TrySetException(new OperationCanceledException(cts.Token));
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();

                tcs.TrySetCanceled(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task MultiTwo()
        {
            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();
                tcs.TrySetResult();
                await a;
                await b;
                await c;
                await tcs.Task; // ok.
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();

                tcs.TrySetException(new TestException());
                await Assert.ThrowsAsync<TestException>(async () => await a);
                await Assert.ThrowsAsync<TestException>(async () => await b);
                await Assert.ThrowsAsync<TestException>(async () => await c);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();

                tcs.TrySetException(new OperationCanceledException(cts.Token));
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await c)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
            {
                var tcs = new UniTaskCompletionSource();

                async UniTask Await()
                {
                    await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();

                tcs.TrySetCanceled(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await c)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        class TestException : Exception
        {

        }
    }

    public class CompletionSourceTest2
    {
        [Fact]
        public async Task SetFirst()
        {
            {
                var tcs = new UniTaskCompletionSource<int>();

                tcs.TrySetResult(10);
                var a = await tcs.Task; // ok.
                var b = await tcs.Task; // ok.
                a.Should().Be(10);
                b.Should().Be(10);
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource<int>();

                tcs.TrySetException(new TestException());

                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);

                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource<int>();

                tcs.TrySetException(new OperationCanceledException(cts.Token));

                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);

                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }

            {
                var tcs = new UniTaskCompletionSource<int>();

                tcs.TrySetCanceled(cts.Token);

                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);

                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task SingleOnFirst()
        {
            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();

                tcs.TrySetResult(10);
                var r1 = await a;
                var r2 = await tcs.Task; // ok.
                r1.Should().Be(10);
                r2.Should().Be(10);
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();

                tcs.TrySetException(new TestException());
                await Assert.ThrowsAsync<TestException>(async () => await a);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();

                tcs.TrySetException(new OperationCanceledException(cts.Token));
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();

                tcs.TrySetCanceled(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task MultiOne()
        {
            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();
                tcs.TrySetResult(10);
                var r1 = await a;
                var r2 = await b;
                var r3 = await tcs.Task; // ok.
                (r1, r2, r3).Should().Be((10, 10, 10));
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();

                tcs.TrySetException(new TestException());
                await Assert.ThrowsAsync<TestException>(async () => await a);
                await Assert.ThrowsAsync<TestException>(async () => await b);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();

                tcs.TrySetException(new OperationCanceledException(cts.Token));
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();

                tcs.TrySetCanceled(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        [Fact]
        public async Task MultiTwo()
        {
            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();
                tcs.TrySetResult(10);
                var r1 = await a;
                var r2 = await b;
                var r3 = await c;
                var r4 = await tcs.Task; // ok.
                (r1, r2, r3, r4).Should().Be((10, 10, 10, 10));
                tcs.Task.Status.Should().Be(UniTaskStatus.Succeeded);
            }

            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();

                tcs.TrySetException(new TestException());
                await Assert.ThrowsAsync<TestException>(async () => await a);
                await Assert.ThrowsAsync<TestException>(async () => await b);
                await Assert.ThrowsAsync<TestException>(async () => await c);
                await Assert.ThrowsAsync<TestException>(async () => await tcs.Task);
                tcs.Task.Status.Should().Be(UniTaskStatus.Faulted);
            }

            var cts = new CancellationTokenSource();

            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();

                tcs.TrySetException(new OperationCanceledException(cts.Token));
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await c)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
            {
                var tcs = new UniTaskCompletionSource<int>();

                async UniTask<int> Await()
                {
                    return await tcs.Task;
                }

                var a = Await();
                var b = Await();
                var c = Await();

                tcs.TrySetCanceled(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await a)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await b)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await c)).CancellationToken.Should().Be(cts.Token);
                (await Assert.ThrowsAsync<OperationCanceledException>(async () => await tcs.Task)).CancellationToken.Should().Be(cts.Token);
                tcs.Task.Status.Should().Be(UniTaskStatus.Canceled);
            }
        }

        class TestException : Exception
        {

        }
    }
}
