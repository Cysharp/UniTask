using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        public static IUniTaskAsyncEnumerable<WhenEachResult<T>> WhenEach<T>(IEnumerable<UniTask<T>> tasks)
        {
            return new WhenEachEnumerable<T>(tasks);
        }

        public static IUniTaskAsyncEnumerable<WhenEachResult<T>> WhenEach<T>(params UniTask<T>[] tasks)
        {
            return new WhenEachEnumerable<T>(tasks);
        }
    }

    public readonly struct WhenEachResult<T>
    {
        public T Result { get; }
        public Exception Exception { get; }

        //[MemberNotNullWhen(false, nameof(Exception))]
        public bool IsCompletedSuccessfully => Exception == null;

        //[MemberNotNullWhen(true, nameof(Exception))]
        public bool IsFaulted => Exception != null;

        public WhenEachResult(T result)
        {
            this.Result = result;
            this.Exception = null;
        }

        public WhenEachResult(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));
            this.Result = default;
            this.Exception = exception;
        }

        public void TryThrow()
        {
            if (IsFaulted)
            {
                ExceptionDispatchInfo.Capture(Exception).Throw();
            }
        }

        public T GetResult()
        {
            if (IsFaulted)
            {
                ExceptionDispatchInfo.Capture(Exception).Throw();
            }
            return Result;
        }

        public override string ToString()
        {
            if (IsCompletedSuccessfully)
            {
                return Result?.ToString() ?? "";
            }
            else
            {
                return $"Exception{{{Exception.Message}}}";
            }
        }
    }

    internal enum WhenEachState : byte
    {
        NotRunning,
        Running,
        Completed
    }

    internal sealed class WhenEachEnumerable<T> : IUniTaskAsyncEnumerable<WhenEachResult<T>>
    {
        IEnumerable<UniTask<T>> source;

        public WhenEachEnumerable(IEnumerable<UniTask<T>> source)
        {
            this.source = source;
        }

        public IUniTaskAsyncEnumerator<WhenEachResult<T>> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(source, cancellationToken);
        }

        sealed class Enumerator : IUniTaskAsyncEnumerator<WhenEachResult<T>>
        {
            readonly IEnumerable<UniTask<T>> source;
            CancellationToken cancellationToken;

            Channel<WhenEachResult<T>> channel;
            IUniTaskAsyncEnumerator<WhenEachResult<T>> channelEnumerator;
            int completeCount;
            WhenEachState state;

            public Enumerator(IEnumerable<UniTask<T>> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
            }

            public WhenEachResult<T> Current => channelEnumerator.Current;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (state == WhenEachState.NotRunning)
                {
                    state = WhenEachState.Running;
                    channel = Channel.CreateSingleConsumerUnbounded<WhenEachResult<T>>();
                    channelEnumerator = channel.Reader.ReadAllAsync().GetAsyncEnumerator(cancellationToken);

                    if (source is UniTask<T>[] array)
                    {
                        ConsumeAll(this, array, array.Length);
                    }
                    else
                    {
                        using (var rentArray = ArrayPoolUtil.Materialize(source))
                        {
                            ConsumeAll(this, rentArray.Array, rentArray.Length);
                        }
                    }
                }

                return channelEnumerator.MoveNextAsync();
            }

            static void ConsumeAll(Enumerator self, UniTask<T>[] array, int length)
            {
                for (int i = 0; i < length; i++)
                {
                    RunWhenEachTask(self, array[i], length).Forget();
                }
            }

            static async UniTaskVoid RunWhenEachTask(Enumerator self, UniTask<T> task, int length)
            {
                try
                {
                    var result = await task;
                    self.channel.Writer.TryWrite(new WhenEachResult<T>(result));
                }
                catch (Exception ex)
                {
                    self.channel.Writer.TryWrite(new WhenEachResult<T>(ex));
                }

                if (Interlocked.Increment(ref self.completeCount) == length)
                {
                    self.state = WhenEachState.Completed;
                    self.channel.Writer.TryComplete();
                }
            }

            public async UniTask DisposeAsync()
            {
                if (channelEnumerator != null)
                {
                    await channelEnumerator.DisposeAsync();
                }

                if (state != WhenEachState.Completed)
                {
                    state = WhenEachState.Completed;
                    channel.Writer.TryComplete(new OperationCanceledException());
                }
            }
        }
    }
}
