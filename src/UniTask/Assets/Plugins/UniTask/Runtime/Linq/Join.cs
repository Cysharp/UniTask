using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(outer, nameof(outer));
            Error.ThrowArgumentNullException(inner, nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new Join<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IUniTaskAsyncEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer, nameof(outer));
            Error.ThrowArgumentNullException(inner, nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new Join<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, UniTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(outer, nameof(outer));
            Error.ThrowArgumentNullException(inner, nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new JoinAwait<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwait<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer, nameof(outer));
            Error.ThrowArgumentNullException(inner, nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new JoinAwait<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector)
        {
            Error.ThrowArgumentNullException(outer, nameof(outer));
            Error.ThrowArgumentNullException(inner, nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IUniTaskAsyncEnumerable<TResult> JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(this IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Error.ThrowArgumentNullException(outer, nameof(outer));
            Error.ThrowArgumentNullException(inner, nameof(inner));
            Error.ThrowArgumentNullException(outerKeySelector, nameof(outerKeySelector));
            Error.ThrowArgumentNullException(innerKeySelector, nameof(innerKeySelector));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));
            Error.ThrowArgumentNullException(comparer, nameof(comparer));

            return new JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }

    internal sealed class Join<TOuter, TInner, TKey, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TOuter> outer;
        readonly IUniTaskAsyncEnumerable<TInner> inner;
        readonly Func<TOuter, TKey> outerKeySelector;
        readonly Func<TInner, TKey> innerKeySelector;
        readonly Func<TOuter, TInner, TResult> resultSelector;
        readonly IEqualityComparer<TKey> comparer;

        public Join(IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer, cancellationToken);
        }

        sealed class _Join : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;

            readonly IUniTaskAsyncEnumerable<TOuter> outer;
            readonly IUniTaskAsyncEnumerable<TInner> inner;
            readonly Func<TOuter, TKey> outerKeySelector;
            readonly Func<TInner, TKey> innerKeySelector;
            readonly Func<TOuter, TInner, TResult> resultSelector;
            readonly IEqualityComparer<TKey> comparer;
            CancellationToken cancellationToken;

            ILookup<TKey, TInner> lookup;
            IUniTaskAsyncEnumerator<TOuter> enumerator;
            UniTask<bool>.Awaiter awaiter;
            TOuter currentOuterValue;
            IEnumerator<TInner> valueEnumerator;

            bool continueNext;

            public _Join(IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
            {
                this.outer = outer;
                this.inner = inner;
                this.outerKeySelector = outerKeySelector;
                this.innerKeySelector = innerKeySelector;
                this.resultSelector = resultSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (lookup == null)
                {
                    CreateInnerHashSet().Forget();
                }
                else
                {
                    SourceMoveNext();
                }
                return new UniTask<bool>(this, completionSource.Version);
            }

            async UniTaskVoid CreateInnerHashSet()
            {
                try
                {
                    lookup = await inner.ToLookupAsync(innerKeySelector, comparer, cancellationToken);
                    enumerator = outer.GetAsyncEnumerator(cancellationToken);
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }
                SourceMoveNext();
            }

            void SourceMoveNext()
            {
                try
                {
                    LOOP:
                    if (valueEnumerator != null)
                    {
                        if (valueEnumerator.MoveNext())
                        {
                            Current = resultSelector(currentOuterValue, valueEnumerator.Current);
                            goto TRY_SET_RESULT_TRUE;
                        }
                        else
                        {
                            valueEnumerator.Dispose();
                            valueEnumerator = null;
                        }
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP; // avoid recursive
                        }
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }

                return;

                TRY_SET_RESULT_TRUE:
                completionSource.TrySetResult(true);
            }


            static void MoveNextCore(object state)
            {
                var self = (_Join)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.currentOuterValue = self.enumerator.Current;
                        var key = self.outerKeySelector(self.currentOuterValue);
                        self.valueEnumerator = self.lookup[key].GetEnumerator();

                        if (self.continueNext)
                        {
                            return;
                        }
                        else
                        {
                            self.SourceMoveNext();
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (valueEnumerator != null)
                {
                    valueEnumerator.Dispose();
                }

                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

    internal sealed class JoinAwait<TOuter, TInner, TKey, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TOuter> outer;
        readonly IUniTaskAsyncEnumerable<TInner> inner;
        readonly Func<TOuter, UniTask<TKey>> outerKeySelector;
        readonly Func<TInner, UniTask<TKey>> innerKeySelector;
        readonly Func<TOuter, TInner, UniTask<TResult>> resultSelector;
        readonly IEqualityComparer<TKey> comparer;

        public JoinAwait(IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _JoinAwait(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer, cancellationToken);
        }

        sealed class _JoinAwait : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;
            static readonly Action<object> OuterSelectCoreDelegate = OuterSelectCore;
            static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;

            readonly IUniTaskAsyncEnumerable<TOuter> outer;
            readonly IUniTaskAsyncEnumerable<TInner> inner;
            readonly Func<TOuter, UniTask<TKey>> outerKeySelector;
            readonly Func<TInner, UniTask<TKey>> innerKeySelector;
            readonly Func<TOuter, TInner, UniTask<TResult>> resultSelector;
            readonly IEqualityComparer<TKey> comparer;
            CancellationToken cancellationToken;

            ILookup<TKey, TInner> lookup;
            IUniTaskAsyncEnumerator<TOuter> enumerator;
            UniTask<bool>.Awaiter awaiter;
            TOuter currentOuterValue;
            IEnumerator<TInner> valueEnumerator;

            UniTask<TResult>.Awaiter resultAwaiter;
            UniTask<TKey>.Awaiter outerKeyAwaiter;

            bool continueNext;

            public _JoinAwait(IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, UniTask<TKey>> outerKeySelector, Func<TInner, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
            {
                this.outer = outer;
                this.inner = inner;
                this.outerKeySelector = outerKeySelector;
                this.innerKeySelector = innerKeySelector;
                this.resultSelector = resultSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (lookup == null)
                {
                    CreateInnerHashSet().Forget();
                }
                else
                {
                    SourceMoveNext();
                }
                return new UniTask<bool>(this, completionSource.Version);
            }

            async UniTaskVoid CreateInnerHashSet()
            {
                try
                {
                    lookup = await inner.ToLookupAwaitAsync(innerKeySelector, comparer, cancellationToken);
                    enumerator = outer.GetAsyncEnumerator(cancellationToken);
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }
                SourceMoveNext();
            }

            void SourceMoveNext()
            {
                try
                {
                    LOOP:
                    if (valueEnumerator != null)
                    {
                        if (valueEnumerator.MoveNext())
                        {
                            resultAwaiter = resultSelector(currentOuterValue, valueEnumerator.Current).GetAwaiter();
                            if (resultAwaiter.IsCompleted)
                            {
                                ResultSelectCore(this);
                            }
                            else
                            {
                                resultAwaiter.SourceOnCompleted(ResultSelectCoreDelegate, this);
                            }
                            return;
                        }
                        else
                        {
                            valueEnumerator.Dispose();
                            valueEnumerator = null;
                        }
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP; // avoid recursive
                        }
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }


            static void MoveNextCore(object state)
            {
                var self = (_JoinAwait)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.currentOuterValue = self.enumerator.Current;

                        self.outerKeyAwaiter = self.outerKeySelector(self.currentOuterValue).GetAwaiter();

                        if (self.outerKeyAwaiter.IsCompleted)
                        {
                            OuterSelectCore(self);
                        }
                        else
                        {
                            self.continueNext = false;
                            self.outerKeyAwaiter.SourceOnCompleted(OuterSelectCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            static void OuterSelectCore(object state)
            {
                var self = (_JoinAwait)state;

                if (self.TryGetResult(self.outerKeyAwaiter, out var key))
                {
                    self.valueEnumerator = self.lookup[key].GetEnumerator();

                    if (self.continueNext)
                    {
                        return;
                    }
                    else
                    {
                        self.SourceMoveNext();
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            static void ResultSelectCore(object state)
            {
                var self = (_JoinAwait)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (valueEnumerator != null)
                {
                    valueEnumerator.Dispose();
                }

                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

    internal sealed class JoinAwaitWithCancellation<TOuter, TInner, TKey, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<TOuter> outer;
        readonly IUniTaskAsyncEnumerable<TInner> inner;
        readonly Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector;
        readonly Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector;
        readonly Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector;
        readonly IEqualityComparer<TKey> comparer;

        public JoinAwaitWithCancellation(IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _JoinAwaitWithCancellation(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer, cancellationToken);
        }

        sealed class _JoinAwaitWithCancellation : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> MoveNextCoreDelegate = MoveNextCore;
            static readonly Action<object> OuterSelectCoreDelegate = OuterSelectCore;
            static readonly Action<object> ResultSelectCoreDelegate = ResultSelectCore;

            readonly IUniTaskAsyncEnumerable<TOuter> outer;
            readonly IUniTaskAsyncEnumerable<TInner> inner;
            readonly Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector;
            readonly Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector;
            readonly Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector;
            readonly IEqualityComparer<TKey> comparer;
            CancellationToken cancellationToken;

            ILookup<TKey, TInner> lookup;
            IUniTaskAsyncEnumerator<TOuter> enumerator;
            UniTask<bool>.Awaiter awaiter;
            TOuter currentOuterValue;
            IEnumerator<TInner> valueEnumerator;

            UniTask<TResult>.Awaiter resultAwaiter;
            UniTask<TKey>.Awaiter outerKeyAwaiter;

            bool continueNext;

            public _JoinAwaitWithCancellation(IUniTaskAsyncEnumerable<TOuter> outer, IUniTaskAsyncEnumerable<TInner> inner, Func<TOuter, CancellationToken, UniTask<TKey>> outerKeySelector, Func<TInner, CancellationToken, UniTask<TKey>> innerKeySelector, Func<TOuter, TInner, CancellationToken, UniTask<TResult>> resultSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
            {
                this.outer = outer;
                this.inner = inner;
                this.outerKeySelector = outerKeySelector;
                this.innerKeySelector = innerKeySelector;
                this.resultSelector = resultSelector;
                this.comparer = comparer;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current { get; private set; }

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (lookup == null)
                {
                    CreateInnerHashSet().Forget();
                }
                else
                {
                    SourceMoveNext();
                }
                return new UniTask<bool>(this, completionSource.Version);
            }

            async UniTaskVoid CreateInnerHashSet()
            {
                try
                {
                    lookup = await inner.ToLookupAwaitWithCancellationAsync(innerKeySelector, comparer, cancellationToken: cancellationToken);
                    enumerator = outer.GetAsyncEnumerator(cancellationToken);
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                    return;
                }
                SourceMoveNext();
            }

            void SourceMoveNext()
            {
                try
                {
                    LOOP:
                    if (valueEnumerator != null)
                    {
                        if (valueEnumerator.MoveNext())
                        {
                            resultAwaiter = resultSelector(currentOuterValue, valueEnumerator.Current, cancellationToken).GetAwaiter();
                            if (resultAwaiter.IsCompleted)
                            {
                                ResultSelectCore(this);
                            }
                            else
                            {
                                resultAwaiter.SourceOnCompleted(ResultSelectCoreDelegate, this);
                            }
                            return;
                        }
                        else
                        {
                            valueEnumerator.Dispose();
                            valueEnumerator = null;
                        }
                    }

                    awaiter = enumerator.MoveNextAsync().GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        continueNext = true;
                        MoveNextCore(this);
                        if (continueNext)
                        {
                            continueNext = false;
                            goto LOOP; // avoid recursive
                        }
                    }
                    else
                    {
                        awaiter.SourceOnCompleted(MoveNextCoreDelegate, this);
                    }
                }
                catch (Exception ex)
                {
                    completionSource.TrySetException(ex);
                }
            }


            static void MoveNextCore(object state)
            {
                var self = (_JoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.awaiter, out var result))
                {
                    if (result)
                    {
                        self.currentOuterValue = self.enumerator.Current;

                        self.outerKeyAwaiter = self.outerKeySelector(self.currentOuterValue, self.cancellationToken).GetAwaiter();

                        if (self.outerKeyAwaiter.IsCompleted)
                        {
                            OuterSelectCore(self);
                        }
                        else
                        {
                            self.continueNext = false;
                            self.outerKeyAwaiter.SourceOnCompleted(OuterSelectCoreDelegate, self);
                        }
                    }
                    else
                    {
                        self.continueNext = false;
                        self.completionSource.TrySetResult(false);
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            static void OuterSelectCore(object state)
            {
                var self = (_JoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.outerKeyAwaiter, out var key))
                {
                    self.valueEnumerator = self.lookup[key].GetEnumerator();

                    if (self.continueNext)
                    {
                        return;
                    }
                    else
                    {
                        self.SourceMoveNext();
                    }
                }
                else
                {
                    self.continueNext = false;
                }
            }

            static void ResultSelectCore(object state)
            {
                var self = (_JoinAwaitWithCancellation)state;

                if (self.TryGetResult(self.resultAwaiter, out var result))
                {
                    self.Current = result;
                    self.completionSource.TrySetResult(true);
                }
            }

            public UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (valueEnumerator != null)
                {
                    valueEnumerator.Dispose();
                }

                if (enumerator != null)
                {
                    return enumerator.DisposeAsync();
                }

                return default;
            }
        }
    }

}