using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    // note: refactor all inherit class and should remove this.
    // see Select and Where.
    internal abstract class AsyncEnumeratorBase<TSource, TResult> : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
    {
        static readonly Action<object> moveNextCallbackDelegate = MoveNextCallBack;

        readonly IUniTaskAsyncEnumerable<TSource> source;
        protected CancellationToken cancellationToken;

        IUniTaskAsyncEnumerator<TSource> enumerator;
        UniTask<bool>.Awaiter sourceMoveNext;

        public AsyncEnumeratorBase(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
            TaskTracker.TrackActiveTask(this, 4);
        }

        // abstract

        /// <summary>
        /// If return value is false, continue source.MoveNext.
        /// </summary>
        protected abstract bool TryMoveNextCore(bool sourceHasCurrent, out bool result);

        // Util
        protected TSource SourceCurrent => enumerator.Current;

        // IUniTaskAsyncEnumerator<T>

        public TResult Current { get; protected set; }

        public UniTask<bool> MoveNextAsync()
        {
            if (enumerator == null)
            {
                enumerator = source.GetAsyncEnumerator(cancellationToken);
            }

            completionSource.Reset();
            if (!OnFirstIteration())
            {
                SourceMoveNext();
            }
            return new UniTask<bool>(this, completionSource.Version);
        }

        protected virtual bool OnFirstIteration()
        {
            return false;
        }

        protected void SourceMoveNext()
        {
            CONTINUE:
            sourceMoveNext = enumerator.MoveNextAsync().GetAwaiter();
            if (sourceMoveNext.IsCompleted)
            {
                bool result = false;
                try
                {
                    if (!TryMoveNextCore(sourceMoveNext.GetResult(), out result))
                    {
                        goto CONTINUE;
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled(cancellationToken);
                }
                else
                {
                    completionSource.TrySetResult(result);
                }
            }
            else
            {
                sourceMoveNext.SourceOnCompleted(moveNextCallbackDelegate, this);
            }
        }

        static void MoveNextCallBack(object state)
        {
            var self = (AsyncEnumeratorBase<TSource, TResult>)state;
            bool result;
            try
            {
                if (!self.TryMoveNextCore(self.sourceMoveNext.GetResult(), out result))
                {
                    self.SourceMoveNext();
                    return;
                }
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
                return;
            }

            if (self.cancellationToken.IsCancellationRequested)
            {
                self.completionSource.TrySetCanceled(self.cancellationToken);
            }
            else
            {
                self.completionSource.TrySetResult(result);
            }
        }

        // if require additional resource to dispose, override and call base.DisposeAsync.
        public virtual UniTask DisposeAsync()
        {
            TaskTracker.RemoveTracking(this);
            if (enumerator != null)
            {
                return enumerator.DisposeAsync();
            }
            return default;
        }
    }

    internal abstract class AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TAwait> : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
    {
        static readonly Action<object> moveNextCallbackDelegate = MoveNextCallBack;
        static readonly Action<object> setCurrentCallbackDelegate = SetCurrentCallBack;


        readonly IUniTaskAsyncEnumerable<TSource> source;
        protected CancellationToken cancellationToken;

        IUniTaskAsyncEnumerator<TSource> enumerator;
        UniTask<bool>.Awaiter sourceMoveNext;

        UniTask<TAwait>.Awaiter resultAwaiter;

        public AsyncEnumeratorAwaitSelectorBase(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            this.source = source;
            this.cancellationToken = cancellationToken;
            TaskTracker.TrackActiveTask(this, 4);
        }

        // abstract

        protected abstract UniTask<TAwait> TransformAsync(TSource sourceCurrent);
        protected abstract bool TrySetCurrentCore(TAwait awaitResult, out bool terminateIteration);

        // Util
        protected TSource SourceCurrent { get; private set; }

        protected (bool waitCallback, bool requireNextIteration) ActionCompleted(bool trySetCurrentResult, out bool moveNextResult)
        {
            if (trySetCurrentResult)
            {
                moveNextResult = true;
                return (false, false);
            }
            else
            {
                moveNextResult = default;
                return (false, true);
            }
        }
        protected (bool waitCallback, bool requireNextIteration) WaitAwaitCallback(out bool moveNextResult) { moveNextResult = default; return (true, false); }
        protected (bool waitCallback, bool requireNextIteration) IterateFinished(out bool moveNextResult) { moveNextResult = false; return (false, false); }

        // IUniTaskAsyncEnumerator<T>

        public TResult Current { get; protected set; }

        public UniTask<bool> MoveNextAsync()
        {
            if (enumerator == null)
            {
                enumerator = source.GetAsyncEnumerator(cancellationToken);
            }

            completionSource.Reset();
            SourceMoveNext();
            return new UniTask<bool>(this, completionSource.Version);
        }

        protected void SourceMoveNext()
        {
            CONTINUE:
            sourceMoveNext = enumerator.MoveNextAsync().GetAwaiter();
            if (sourceMoveNext.IsCompleted)
            {
                bool result = false;
                try
                {
                    (bool waitCallback, bool requireNextIteration) = TryMoveNextCore(sourceMoveNext.GetResult(), out result);

                    if (waitCallback)
                    {
                        return;
                    }

                    if (requireNextIteration)
                    {
                        goto CONTINUE;
                    }
                    else
                    {
                        completionSource.TrySetResult(result);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }
            }
            else
            {
                sourceMoveNext.SourceOnCompleted(moveNextCallbackDelegate, this);
            }
        }

        (bool waitCallback, bool requireNextIteration) TryMoveNextCore(bool sourceHasCurrent, out bool result)
        {
            if (sourceHasCurrent)
            {
                SourceCurrent = enumerator.Current;
                var task = TransformAsync(SourceCurrent);
                if (UnwarapTask(task, out var taskResult))
                {
                    var currentResult = TrySetCurrentCore(taskResult, out var terminateIteration);
                    if (terminateIteration)
                    {
                        return IterateFinished(out result);
                    }

                    return ActionCompleted(currentResult, out result);
                }
                else
                {
                    return WaitAwaitCallback(out result);
                }
            }

            return IterateFinished(out result);
        }

        protected bool UnwarapTask(UniTask<TAwait> taskResult, out TAwait result)
        {
            resultAwaiter = taskResult.GetAwaiter();

            if (resultAwaiter.IsCompleted)
            {
                result = resultAwaiter.GetResult();
                return true;
            }
            else
            {
                resultAwaiter.SourceOnCompleted(setCurrentCallbackDelegate, this);
                result = default;
                return false;
            }
        }

        static void MoveNextCallBack(object state)
        {
            var self = (AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TAwait>)state;
            bool result = false;
            try
            {
                (bool waitCallback, bool requireNextIteration) = self.TryMoveNextCore(self.sourceMoveNext.GetResult(), out result);

                if (waitCallback)
                {
                    return;
                }

                if (requireNextIteration)
                {
                    self.SourceMoveNext();
                    return;
                }
                else
                {
                    self.completionSource.TrySetResult(result);
                }
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
                return;
            }
        }

        static void SetCurrentCallBack(object state)
        {
            var self = (AsyncEnumeratorAwaitSelectorBase<TSource, TResult, TAwait>)state;

            bool doneSetCurrent;
            bool terminateIteration;
            try
            {
                var result = self.resultAwaiter.GetResult();
                doneSetCurrent = self.TrySetCurrentCore(result, out terminateIteration);
            }
            catch (Exception ex)
            {
                self.completionSource.TrySetException(ex);
                return;
            }

            if (self.cancellationToken.IsCancellationRequested)
            {
                self.completionSource.TrySetCanceled(self.cancellationToken);
            }
            else
            {
                if (doneSetCurrent)
                {
                    self.completionSource.TrySetResult(true);
                }
                else
                {
                    if (terminateIteration)
                    {
                        self.completionSource.TrySetResult(false);
                    }
                    else
                    {
                        self.SourceMoveNext();
                    }
                }
            }
        }

        // if require additional resource to dispose, override and call base.DisposeAsync.
        public virtual UniTask DisposeAsync()
        {
            TaskTracker.RemoveTracking(this);
            if (enumerator != null)
            {
                return enumerator.DisposeAsync();
            }
            return default;
        }
    }
}