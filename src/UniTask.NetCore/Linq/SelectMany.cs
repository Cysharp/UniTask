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

            return new SelectMany<TSource, TResult>(source, selector);
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, IUniTaskAsyncEnumerable<TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, IUniTaskAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwait<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TResult>>> selector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IUniTaskAsyncEnumerable<TResult> SelectManyAwaitWithCancellation<TSource, TCollection, TResult>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UniTask<IUniTaskAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, UniTask<TResult>> resultSelector)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class SelectMany<TSource, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TSource> source;
        readonly Func<TSource, IUniTaskAsyncEnumerable<TResult>> selector;

        public SelectMany(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new Enumerator(source, selector, cancellationToken);
        }

        sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> sourceMoveNextCoreDelegate = SourceMoveNextCore;
            static readonly Action<object> selectedSourceMoveNextCoreDelegate = SeletedSourceMoveNextCore;
            static readonly Action<object> selectedEnumeratorDisposeAsyncCoreDelegate = SelectedEnumeratorDisposeAsyncCore;

            readonly IUniTaskAsyncEnumerable<TSource> source;
            readonly Func<TSource, IUniTaskAsyncEnumerable<TResult>> selector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<TSource> sourceEnumerator;
            IUniTaskAsyncEnumerator<TResult> selectedEnumerator;
            UniTask<bool>.Awaiter sourceAwaiter;
            UniTask<bool>.Awaiter selectedAwaiter;
            UniTask.Awaiter selectedDisposeAsyncAwaiter;

            public Enumerator(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, IUniTaskAsyncEnumerable<TResult>> selector, CancellationToken cancellationToken)
            {
                this.source = source;
                this.selector = selector;
                this.cancellationToken = cancellationToken;
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
                var self = (Enumerator)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            var current = self.sourceEnumerator.Current;
                            self.selectedEnumerator = self.selector(current).GetAsyncEnumerator(self.cancellationToken);
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
                var self = (Enumerator)state;

                if (self.TryGetResult(self.selectedAwaiter, out var result))
                {
                    if (result)
                    {
                        try
                        {
                            self.Current = self.selectedEnumerator.Current;
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
                var self = (Enumerator)state;

                if (self.TryGetResult(self.selectedDisposeAsyncAwaiter))
                {
                    self.selectedEnumerator = null;
                    self.selectedAwaiter = default;

                    self.MoveNextSource(); // iterate next source
                }
            }

            public async UniTask DisposeAsync()
            {
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
