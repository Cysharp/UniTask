using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectMany<TSource, TResult, TResult>(source, selector, (x, y) => y);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, IUniTaskAsyncEnumerable<TResult>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectMany<TSource, TResult, TResult>(source, selector, (x, y) => y);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectMany<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, IUniTaskAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectMany<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwait<TSource, TResult, TResult>(source, selector, (x, y) => UniTask.FromResult(y));
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwait<TSource, TResult, TResult>(source, selector, (x, y) => UniTask.FromResult(y));
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwait<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwait<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwaitWithCancellation<TSource, TResult, TResult>(source, selector, (x, y, c) => UniTask.FromResult(y));
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(selector, nameof(selector));

            return new SelectManyAwaitWithCancellation<TSource, TResult, TResult>(source, selector, (x, y, c) => UniTask.FromResult(y));
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(collectionSelector, nameof(collectionSelector));

            return new SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }
    }

    internal sealed class SelectMany<TSource, TCollection, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, IUniTaskAsyncEnumerable<TCollection>> selector1;
        readonly Func<TSource, int, IUniTaskAsyncEnumerable<TCollection>> selector2;
        readonly Func<TSource, TCollection, TResult> resultSelector;

        public SelectMany(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TCollection>> selector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.selector1 = selector;
            this.selector2 = null;
            this.resultSelector = resultSelector;
        }

        public SelectMany(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, IUniTaskAsyncEnumerable<TCollection>> selector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.selector1 = null;
            this.selector2 = selector;
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectMany(source, selector1, selector2, resultSelector, cancellationToken);
        }

        sealed class _SelectMany : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;
            static readonly Action<object> selectedSourceMoveNextCoreDelegate = SeletedSourceMoveNextCore;
            static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate = SelectedEnumeratorDisposeAsyncCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;

            readonly Func<TSource, IUniTaskAsyncEnumerable<TCollection>> selector1;
            readonly Func<TSource, int, IUniTaskAsyncEnumerable<TCollection>> selector2;
            readonly Func<TSource, TCollection, TResult> resultSelector;
            CancellationToken cancellationToken;

            TSource sourceCurrent;
            int sourceIndex;
            IUniTaskAsyncEnumerator<TSource> sourceEnumerator;
            IUniTaskAsyncEnumerator<TCollection> selectedEnumerator;
            UniTask<bool>.Awaiter sourceAwaiter;
            UniTask<bool>.Awaiter selectedAwaiter;
            UniTask.Awaiter selectedDisposeAsyncAwaiter;

            public _SelectMany(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TCollection>> selector1, Func<TSource, int, IUniTaskAsyncEnumerable<TCollection>> selector2, Func<TSource, TCollection, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector1 = selector1;
                this.selector2 = selector2;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                // iterate selected field
                if (selectedEnumerator != null)
                {
                    MoveNextSelected();
                }
                else
                {
                    // iterate source field
                    if (sourceEnumerator == null)
                    {
                        sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    }
                    MoveNextSource();
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNextSource()
            {
                try
                {
                    sourceAwaiter = sourceEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (sourceAwaiter.IsCompleted)
                {
                    SourceMoveNextCore(this);
                }
                else
                {
                    sourceAwaiter.SourceOnCompleted(sourceMoveNextCoreDelegate, this);
                }
            }

            void MoveNextSelected()
            {
                try
                {
                    selectedAwaiter = selectedEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (selectedAwaiter.IsCompleted)
                {
                    SeletedSourceMoveNextCore(this);
                }
                else
                {
                    selectedAwaiter.SourceOnCompleted(selectedSourceMoveNextCoreDelegate, this);
                }
            }

            static void SourceMoveNextCore(object state)
            {
                var self = (_SelectMany)state;

                if (self.TryGetResult(self.sourceAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.sourceCurrent = self.sourceEnumerator.Current;
                            if (self.selector1 != null)
                            {
                                self.selectedEnumerator = self.selector1(self.sourceCurrent).GetAsyncEnumerator(self.cancellationToken);
                            }
                            else
                            {
                                self.selectedEnumerator = self.selector2(self.sourceCurrent, checked(self.sourceIndex++)).GetAsyncEnumerator(self.cancellationToken);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        self.MoveNextSelected(); // iterated selected source.
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void SeletedSourceMoveNextCore(object state)
            {
                var self = (_SelectMany)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.Current = self.resultSelector(self.sourceCurrent, self.selectedEnumerator.Current);
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }

                        self.completionSource.TrySetResult(true);
                    }
                    else
                    {
                        // dispose selected source and try iterate source.
                        try
                        {
                            self.selectedDisposeAsyncAwaiter = self.selectedEnumerator.DisposeAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                        if (self.selectedDisposeAsyncAwaiter.IsCompleted)
                        {
                            SelectedEnumeratorDisposeAsyncCore(self);
                        }
                        else
                        {
                            self.selectedDisposeAsyncAwaiter.SourceOnCompleted(selectedEnumeratorDisposeAsyncCoreDelegate, self);
                        }
                    }
                }
            }

            static void SelectedEnumeratorDisposeAsyncCore(object state)
            {
                var self = (_SelectMany)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (selectedEnumerator != null)
                {
                    await selectedEnumerator.DisposeAsync();
                }
                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }
            }
        }
    }

    internal sealed class SelectManyAwait<TSource, TCollection, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector1;
        readonly Func<TSource, int, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector2;
        readonly Func<TSource, TCollection, UniTask<TResult>> resultSelector;

        public SelectManyAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            this.source = source;
            this.selector1 = selector;
            this.selector2 = null;
            this.resultSelector = resultSelector;
        }

        public SelectManyAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            this.source = source;
            this.selector1 = null;
            this.selector2 = selector;
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectManyAwait(source, selector1, selector2, resultSelector, cancellationToken);
        }

        sealed class _SelectManyAwait : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;
            static readonly Action<object> selectedSourceMoveNextCoreDelegate = SeletedSourceMoveNextCore;
            static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate = SelectedEnumeratorDisposeAsyncCore;
            static readonly Action<object> selectorAwaitCoreDelegate = SelectorAwaitCore;
            static readonly Action<object> resultSelectorAwaitCoreDelegate = ResultSelectorAwaitCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;

            readonly Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector1;
            readonly Func<TSource, int, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector2;
            readonly Func<TSource, TCollection, UniTask<TResult>> resultSelector;
            CancellationToken cancellationToken;

            TSource sourceCurrent;
            int sourceIndex;
            IUniTaskAsyncEnumerator<TSource> sourceEnumerator;
            IUniTaskAsyncEnumerator<TCollection> selectedEnumerator;
            UniTask<bool>.Awaiter sourceAwaiter;
            UniTask<bool>.Awaiter selectedAwaiter;
            UniTask.Awaiter selectedDisposeAsyncAwaiter;

            // await additional
            UniTask<IUniTaskAsyncEnumerable<TCollection>>.Awaiter collectionSelectorAwaiter;
            UniTask<TResult>.Awaiter resultSelectorAwaiter;

            public _SelectManyAwait(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector1, Func<TSource, int, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector2, Func<TSource, TCollection, UniTask<TResult>> resultSelector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector1 = selector1;
                this.selector2 = selector2;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                // iterate selected field
                if (selectedEnumerator != null)
                {
                    MoveNextSelected();
                }
                else
                {
                    // iterate source field
                    if (sourceEnumerator == null)
                    {
                        sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    }
                    MoveNextSource();
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNextSource()
            {
                try
                {
                    sourceAwaiter = sourceEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (sourceAwaiter.IsCompleted)
                {
                    SourceMoveNextCore(this);
                }
                else
                {
                    sourceAwaiter.SourceOnCompleted(sourceMoveNextCoreDelegate, this);
                }
            }

            void MoveNextSelected()
            {
                try
                {
                    selectedAwaiter = selectedEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (selectedAwaiter.IsCompleted)
                {
                    SeletedSourceMoveNextCore(this);
                }
                else
                {
                    selectedAwaiter.SourceOnCompleted(selectedSourceMoveNextCoreDelegate, this);
                }
            }

            static void SourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.sourceAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.sourceCurrent = self.sourceEnumerator.Current;

                            if (self.selector1 != null)
                            {
                                self.collectionSelectorAwaiter = self.selector1(self.sourceCurrent).GetAwaiter();
                            }
                            else
                            {
                                self.collectionSelectorAwaiter = self.selector2(self.sourceCurrent, checked(self.sourceIndex++)).GetAwaiter();
                            }

                            if (self.collectionSelectorAwaiter.IsCompleted)
                            {
                                SelectorAwaitCore(self);
                            }
                            else
                            {
                                self.collectionSelectorAwaiter.SourceOnCompleted(selectorAwaitCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void SeletedSourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.resultSelectorAwaiter = self.resultSelector(self.sourceCurrent, self.selectedEnumerator.Current).GetAwaiter();
                            if (self.resultSelectorAwaiter.IsCompleted)
                            {
                                ResultSelectorAwaitCore(self);
                            }
                            else
                            {
                                self.resultSelectorAwaiter.SourceOnCompleted(resultSelectorAwaitCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                    }
                    else
                    {
                        // dispose selected source and try iterate source.
                        try
                        {
                            self.selectedDisposeAsyncAwaiter = self.selectedEnumerator.DisposeAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                        if (self.selectedDisposeAsyncAwaiter.IsCompleted)
                        {
                            SelectedEnumeratorDisposeAsyncCore(self);
                        }
                        else
                        {
                            self.selectedDisposeAsyncAwaiter.SourceOnCompleted(selectedEnumeratorDisposeAsyncCoreDelegate, self);
                        }
                    }
                }
            }

            static void SelectedEnumeratorDisposeAsyncCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            static void SelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.collectionSelectorAwaiter, out var result))
                {
                    self.selectedEnumerator = result.GetAsyncEnumerator(self.cancellationToken);
                    self.MoveNextSelected(); // iterated selected source.
                }
            }

            static void ResultSelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwait)state;

                if (self.TryGetResult(self.resultSelectorAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (selectedEnumerator != null)
                {
                    await selectedEnumerator.DisposeAsync();
                }
                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }
            }
        }
    }

    internal sealed class SelectManyAwaitWithCancellation<TSource, TCollection, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector1;
        readonly Func<TSource, int, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector2;
        readonly Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector;

        public SelectManyAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            this.source = source;
            this.selector1 = selector;
            this.selector2 = null;
            this.resultSelector = resultSelector;
        }

        public SelectManyAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            this.source = source;
            this.selector1 = null;
            this.selector2 = selector;
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _SelectManyAwaitWithCancellation(source, selector1, selector2, resultSelector, cancellationToken);
        }

        sealed class _SelectManyAwaitWithCancellation : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;
            static readonly Action<object> selectedSourceMoveNextCoreDelegate = SeletedSourceMoveNextCore;
            static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate = SelectedEnumeratorDisposeAsyncCore;
            static readonly Action<object> selectorAwaitCoreDelegate = SelectorAwaitCore;
            static readonly Action<object> resultSelectorAwaitCoreDelegate = ResultSelectorAwaitCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;

            readonly Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector1;
            readonly Func<TSource, int, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector2;
            readonly Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector;
            CancellationToken cancellationToken;

            TSource sourceCurrent;
            int sourceIndex;
            IUniTaskAsyncEnumerator<TSource> sourceEnumerator;
            IUniTaskAsyncEnumerator<TCollection> selectedEnumerator;
            UniTask<bool>.Awaiter sourceAwaiter;
            UniTask<bool>.Awaiter selectedAwaiter;
            UniTask.Awaiter selectedDisposeAsyncAwaiter;

            // await additional
            UniTask<IUniTaskAsyncEnumerable<TCollection>>.Awaiter collectionSelectorAwaiter;
            UniTask<TResult>.Awaiter resultSelectorAwaiter;

            public _SelectManyAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector1, Func<TSource, int, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> selector2, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector1 = selector1;
                this.selector2 = selector2;
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                completionSource.Reset();

                // iterate selected field
                if (selectedEnumerator != null)
                {
                    MoveNextSelected();
                }
                else
                {
                    // iterate source field
                    if (sourceEnumerator == null)
                    {
                        sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    }
                    MoveNextSource();
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            void MoveNextSource()
            {
                try
                {
                    sourceAwaiter = sourceEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (sourceAwaiter.IsCompleted)
                {
                    SourceMoveNextCore(this);
                }
                else
                {
                    sourceAwaiter.SourceOnCompleted(sourceMoveNextCoreDelegate, this);
                }
            }

            void MoveNextSelected()
            {
                try
                {
                    selectedAwaiter = selectedEnumerator.MoveNextAsync().GetAwaiter();
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }

                if (selectedAwaiter.IsCompleted)
                {
                    SeletedSourceMoveNextCore(this);
                }
                else
                {
                    selectedAwaiter.SourceOnCompleted(selectedSourceMoveNextCoreDelegate, this);
                }
            }

            static void SourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.sourceAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.sourceCurrent = self.sourceEnumerator.Current;

                            if (self.selector1 != null)
                            {
                                self.collectionSelectorAwaiter = self.selector1(self.sourceCurrent, self.cancellationToken).GetAwaiter();
                            }
                            else
                            {
                                self.collectionSelectorAwaiter = self.selector2(self.sourceCurrent, checked(self.sourceIndex++), self.cancellationToken).GetAwaiter();
                            }

                            if (self.collectionSelectorAwaiter.IsCompleted)
                            {
                                SelectorAwaitCore(self);
                            }
                            else
                            {
                                self.collectionSelectorAwaiter.SourceOnCompleted(selectorAwaitCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                    }
                    else
                    {
                        self.completionSource.TrySetResult(false);
                    }
                }
            }

            static void SeletedSourceMoveNextCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.resultSelectorAwaiter = self.resultSelector(self.sourceCurrent, self.selectedEnumerator.Current, self.cancellationToken).GetAwaiter();
                            if (self.resultSelectorAwaiter.IsCompleted)
                            {
                                ResultSelectorAwaitCore(self);
                            }
                            else
                            {
                                self.resultSelectorAwaiter.SourceOnCompleted(resultSelectorAwaitCoreDelegate, self);
                            }
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                    }
                    else
                    {
                        // dispose selected source and try iterate source.
                        try
                        {
                            self.selectedDisposeAsyncAwaiter = self.selectedEnumerator.DisposeAsync().GetAwaiter();
                        }
                        catch (Exception ex)
                        {
                            self.completionSource.TrySetException(ex);
                            return;
                        }
                        if (self.selectedDisposeAsyncAwaiter.IsCompleted)
                        {
                            SelectedEnumeratorDisposeAsyncCore(self);
                        }
                        else
                        {
                            self.selectedDisposeAsyncAwaiter.SourceOnCompleted(selectedEnumeratorDisposeAsyncCoreDelegate, self);
                        }
                    }
                }
            }

            static void SelectedEnumeratorDisposeAsyncCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            static void SelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.collectionSelectorAwaiter, out var result))
                {
                    self.selectedEnumerator = result.GetAsyncEnumerator(self.cancellationToken);
                    self.MoveNextSelected(); // iterated selected source.
                }
            }

            static void ResultSelectorAwaitCore(object state)
            {
                var self = (_SelectManyAwaitWithCancellation)state;

                if (self.TryGetResult(self.resultSelectorAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (selectedEnumerator != null)
                {
                    await selectedEnumerator.DisposeAsync();
                }
                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }
            }
        }
    }
}