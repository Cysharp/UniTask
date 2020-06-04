using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace NetCoreTests.Linq
{
    public class UniTaskTestException : Exception
    {
        public static IUniTaskAsyncEnumerable<int> ThrowImmediate()
        {
            return UniTaskAsyncEnumerable.Throw<int>(new UniTaskTestException());
        }
        public static IUniTaskAsyncEnumerable<int> ThrowAfter()
        {
            return new ThrowAfter<int>(new UniTaskTestException());
        }
        public static IUniTaskAsyncEnumerable<int> ThrowInMoveNext()
        {
            return new ThrowIn<int>(new UniTaskTestException());
        }


        public static IEnumerable<IUniTaskAsyncEnumerable<int>> Throws(int count = 3)
        {
            yield return ThrowImmediate();
            yield return ThrowAfter();
            yield return ThrowInMoveNext();
            yield return UniTaskAsyncEnumerable.Range(1, count).Concat(ThrowImmediate());
            yield return UniTaskAsyncEnumerable.Range(1, count).Concat(ThrowAfter());
            yield return UniTaskAsyncEnumerable.Range(1, count).Concat(ThrowInMoveNext());
        }
    }

    internal class ThrowIn<TValue> : IUniTaskAsyncEnumerable<TValue>
    {
        readonly Exception exception;

        public ThrowIn(Exception exception)
        {
            this.exception = exception;
        }

        public IUniTaskAsyncEnumerator<TValue> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(exception, cancellationToken);
        }

        class Enumerator : IUniTaskAsyncEnumerator<TValue>
        {
            readonly Exception exception;
            CancellationToken cancellationToken;

            public Enumerator(Exception exception, CancellationToken cancellationToken)
            {
                this.exception = exception;
                this.cancellationToken = cancellationToken;
            }

            public TValue Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
                return new UniTask<bool>(false);
            }

            public UniTask DisposeAsync()
            {
                return default;
            }
        }
    }

    internal class ThrowAfter<TValue> : IUniTaskAsyncEnumerable<TValue>
    {
        readonly Exception exception;

        public ThrowAfter(Exception exception)
        {
            this.exception = exception;
        }

        public IUniTaskAsyncEnumerator<TValue> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(exception, cancellationToken);
        }

        class Enumerator : IUniTaskAsyncEnumerator<TValue>
        {
            readonly Exception exception;
            CancellationToken cancellationToken;

            public Enumerator(Exception exception, CancellationToken cancellationToken)
            {
                this.exception = exception;
                this.cancellationToken = cancellationToken;
            }

            public TValue Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                var tcs = new UniTaskCompletionSource<bool>();

                var awaiter = UniTask.Yield().GetAwaiter();
                awaiter.UnsafeOnCompleted(() =>
                {
                    Thread.Sleep(1);
                    tcs.TrySetException(exception);
                });

                return tcs.Task;
            }

            public UniTask DisposeAsync()
            {
                return default;
            }
        }
    }
}
