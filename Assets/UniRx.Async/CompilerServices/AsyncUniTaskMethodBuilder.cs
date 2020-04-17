#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace UniRx.Async.CompilerServices
{
    // TODO:Remove
    public struct AsyncUniTaskMethodBuilder
    {
        UniTaskCompletionSource promise;
        Action moveNext;

        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncUniTaskMethodBuilder Create()
        {
            var builder = new AsyncUniTaskMethodBuilder();
            return builder;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public UniTask Task
        {
            get
            {
                if (promise != null)
                {
                    return promise.Task;
                }

                if (moveNext == null)
                {
                    return UniTask.CompletedTask;
                }
                else
                {
                    promise = new UniTaskCompletionSource();
                    return promise.Task;
                }
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (promise == null)
            {
                promise = new UniTaskCompletionSource();
            }
            if (exception is OperationCanceledException ex)
            {
                promise.TrySetCanceled(ex);
            }
            else
            {
                promise.TrySetException(exception);
            }
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            if (moveNext == null)
            {
            }
            else
            {
                if (promise == null)
                {
                    promise = new UniTaskCompletionSource();
                }
                promise.TrySetResult();
            }
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new UniTaskCompletionSource(); // built future.
                }

                var runner = new MoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.OnCompleted(moveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new UniTaskCompletionSource(); // built future.
                }

                var runner = new MoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }

    // TODO:Remove
    public struct AsyncUniTaskMethodBuilder<T>
    {
        T result;
        UniTaskCompletionSource<T> promise;
        Action moveNext;

        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncUniTaskMethodBuilder<T> Create()
        {
            var builder = new AsyncUniTaskMethodBuilder<T>();
            return builder;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public UniTask<T> Task
        {
            get
            {
                if (promise != null)
                {
                    return new UniTask<T>(promise);
                }

                if (moveNext == null)
                {
                    return new UniTask<T>(result);
                }
                else
                {
                    promise = new UniTaskCompletionSource<T>();
                    return new UniTask<T>(promise);
                }
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (promise == null)
            {
                promise = new UniTaskCompletionSource<T>();
            }
            if (exception is OperationCanceledException ex)
            {
                promise.TrySetCanceled(ex);
            }
            else
            {
                promise.TrySetException(exception);
            }
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult(T result)
        {
            if (moveNext == null)
            {
                this.result = result;
            }
            else
            {
                if (promise == null)
                {
                    promise = new UniTaskCompletionSource<T>();
                }
                promise.TrySetResult(result);
            }
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new UniTaskCompletionSource<T>(); // built future.
                }

                var runner = new MoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.OnCompleted(moveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new UniTaskCompletionSource<T>(); // built future.
                }

                var runner = new MoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }





    [StructLayout(LayoutKind.Auto)]
    public struct AsyncUniTask2MethodBuilder
    {
        // cache items.
        AutoResetUniTaskCompletionSource promise;
        IMoveNextRunner runner;

        // 1. Static Create method.
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncUniTask2MethodBuilder Create()
        {
            return default;
        }

        // 2. TaskLike Task property.
        public UniTask2 Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (promise == null)
                {
                    promise = AutoResetUniTaskCompletionSource.Create();
                }
                return promise.Task;
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            // runner is finished, return first.
            if (runner != null)
            {
                runner.Return();
                runner = null;
            }

            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource.Create();
            }
            promise.SetException(exception);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            // runner is finished, return first.
            if (runner != null)
            {
                runner.Return();
                runner = null;
            }

            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource.Create();
            }
            promise.SetResult();
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource.Create();
            }
            if (runner == null)
            {
                runner = MoveNextRunner2<TStateMachine>.Create(ref stateMachine);
            }

            awaiter.OnCompleted(runner.CallMoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource.Create();
            }
            if (runner == null)
            {
                runner = MoveNextRunner2<TStateMachine>.Create(ref stateMachine);
            }

            awaiter.OnCompleted(runner.CallMoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public struct AsyncUniTask2MethodBuilder<T>
    {
        // cache items.
        AutoResetUniTaskCompletionSource<T> promise;
        IMoveNextRunner runner;

        // 1. Static Create method.
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncUniTask2MethodBuilder<T> Create()
        {
            return default;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public UniTask2<T> Task
        {
            get
            {
                if (promise == null)
                {
                    promise = AutoResetUniTaskCompletionSource<T>.Create();
                }
                return promise.Task;
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            // runner is finished, return first.
            if (runner != null)
            {
                runner.Return();
                runner = null;
            }

            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource<T>.Create();
            }
            promise.SetException(exception);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult(T result)
        {
            // runner is finished, return first.
            if (runner != null)
            {
                runner.Return();
                runner = null;
            }

            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource<T>.Create();
            }
            promise.SetResult(result);
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource<T>.Create();
            }
            if (runner == null)
            {
                runner = MoveNextRunner2<TStateMachine>.Create(ref stateMachine);
            }

            awaiter.OnCompleted(runner.CallMoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (promise == null)
            {
                promise = AutoResetUniTaskCompletionSource<T>.Create();
            }
            if (runner == null)
            {
                runner = MoveNextRunner2<TStateMachine>.Create(ref stateMachine);
            }

            awaiter.OnCompleted(runner.CallMoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }
    }
}

#endif