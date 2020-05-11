#pragma warning disable 0649

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskValueTaskExtensions
    {
        public static ValueTask AsValueTask(this UniTask task)
        {
            ref var core = ref Unsafe.As<UniTask, UniTaskToValueTask>(ref task);
            if (core.source == null)
            {
                return default;
            }
            
            return new ValueTask(new UniTaskValueTaskSource(core.source), core.token);
        }

        public static ValueTask<T> AsValueTask<T>(this UniTask<T> task)
        {
            ref var core = ref Unsafe.As<UniTask<T>, UniTaskToValueTask<T>>(ref task);
            if (core.source == null)
            {
                return new ValueTask<T>(core.result);
            }

            return new ValueTask<T>(new UniTaskValueTaskSource<T>(core.source), core.token);
        }

        struct UniTaskToValueTask
        {
            public IUniTaskSource source;
            public short token;
        }

        class UniTaskValueTaskSource : IValueTaskSource
        {
            readonly IUniTaskSource source;

            public UniTaskValueTaskSource(IUniTaskSource source)
            {
                this.source = source;
            }

            public void GetResult(short token)
            {
                source.GetResult(token);
            }

            public ValueTaskSourceStatus GetStatus(short token)
            {
                var status = source.GetStatus(token);
                switch (status)
                {
                    case UniTaskStatus.Pending:
                        return ValueTaskSourceStatus.Pending;
                    case UniTaskStatus.Succeeded:
                        return ValueTaskSourceStatus.Succeeded;
                    case UniTaskStatus.Faulted:
                        return ValueTaskSourceStatus.Faulted;
                    case UniTaskStatus.Canceled:
                        return ValueTaskSourceStatus.Canceled;
                    default:
                        return (ValueTaskSourceStatus)status;
                }
            }

            public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
            {
                source.OnCompleted(continuation, state, token);
            }
        }

        struct UniTaskToValueTask<T>
        {
            public IUniTaskSource<T> source;
            public T result;
            public short token;
        }

        class UniTaskValueTaskSource<T> : IValueTaskSource<T>
        {
            readonly IUniTaskSource<T> source;

            public UniTaskValueTaskSource(IUniTaskSource<T> source)
            {
                this.source = source;
            }

            public T GetResult(short token)
            {
                return source.GetResult(token);
            }

            public ValueTaskSourceStatus GetStatus(short token)
            {
                var status = source.GetStatus(token);
                switch (status)
                {
                    case UniTaskStatus.Pending:
                        return ValueTaskSourceStatus.Pending;
                    case UniTaskStatus.Succeeded:
                        return ValueTaskSourceStatus.Succeeded;
                    case UniTaskStatus.Faulted:
                        return ValueTaskSourceStatus.Faulted;
                    case UniTaskStatus.Canceled:
                        return ValueTaskSourceStatus.Canceled;
                    default:
                        return (ValueTaskSourceStatus)status;
                }
            }

            public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
            {
                source.OnCompleted(continuation, state, token);
            }
        }
    }
}
