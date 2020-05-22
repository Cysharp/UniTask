using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, Func<T1, T2, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, TResult>(source1, source2, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, Func<T1, T2, T3, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, TResult>(source1, source2, source3, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, Func<T1, T2, T3, T4, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, TResult>(source1, source2, source3, source4, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, Func<T1, T2, T3, T4, T5, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, TResult>(source1, source2, source3, source4, source5, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, TResult>(source1, source2, source3, source4, source5, source6, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, TResult>(source1, source2, source3, source4, source5, source6, source7, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(source10, nameof(source10));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(source10, nameof(source10));
            Error.ThrowArgumentNullException(source11, nameof(source11));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(source10, nameof(source10));
            Error.ThrowArgumentNullException(source11, nameof(source11));
            Error.ThrowArgumentNullException(source12, nameof(source12));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(source10, nameof(source10));
            Error.ThrowArgumentNullException(source11, nameof(source11));
            Error.ThrowArgumentNullException(source12, nameof(source12));
            Error.ThrowArgumentNullException(source13, nameof(source13));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, IUniTaskAsyncEnumerable<T14> source14, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(source10, nameof(source10));
            Error.ThrowArgumentNullException(source11, nameof(source11));
            Error.ThrowArgumentNullException(source12, nameof(source12));
            Error.ThrowArgumentNullException(source13, nameof(source13));
            Error.ThrowArgumentNullException(source14, nameof(source14));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, source14, resultSelector);
        }

        public static IUniTaskAsyncEnumerable<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, IUniTaskAsyncEnumerable<T14> source14, IUniTaskAsyncEnumerable<T15> source15, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> resultSelector)
        {
            Error.ThrowArgumentNullException(source1, nameof(source1));
            Error.ThrowArgumentNullException(source2, nameof(source2));
            Error.ThrowArgumentNullException(source3, nameof(source3));
            Error.ThrowArgumentNullException(source4, nameof(source4));
            Error.ThrowArgumentNullException(source5, nameof(source5));
            Error.ThrowArgumentNullException(source6, nameof(source6));
            Error.ThrowArgumentNullException(source7, nameof(source7));
            Error.ThrowArgumentNullException(source8, nameof(source8));
            Error.ThrowArgumentNullException(source9, nameof(source9));
            Error.ThrowArgumentNullException(source10, nameof(source10));
            Error.ThrowArgumentNullException(source11, nameof(source11));
            Error.ThrowArgumentNullException(source12, nameof(source12));
            Error.ThrowArgumentNullException(source13, nameof(source13));
            Error.ThrowArgumentNullException(source14, nameof(source14));
            Error.ThrowArgumentNullException(source15, nameof(source15));
            Error.ThrowArgumentNullException(resultSelector, nameof(resultSelector));

            return new CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, source14, source15, resultSelector);
        }

    }

    internal class CombineLatest<T1, T2, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        
        readonly Func<T1, T2, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, Func<T1, T2, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            const int CompleteCount = 2;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
       
            readonly Func<T1, T2, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, Func<T1, T2, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }

                if (!running1 || !running2)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2)
                {
                    result = resultSelector(current1, current2);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        
        readonly Func<T1, T2, T3, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, Func<T1, T2, T3, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            const int CompleteCount = 3;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
       
            readonly Func<T1, T2, T3, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, Func<T1, T2, T3, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3)
                {
                    result = resultSelector(current1, current2, current3);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        
        readonly Func<T1, T2, T3, T4, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, Func<T1, T2, T3, T4, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            const int CompleteCount = 4;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
       
            readonly Func<T1, T2, T3, T4, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, Func<T1, T2, T3, T4, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4)
                {
                    result = resultSelector(current1, current2, current3, current4);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        
        readonly Func<T1, T2, T3, T4, T5, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, Func<T1, T2, T3, T4, T5, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            const int CompleteCount = 5;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
       
            readonly Func<T1, T2, T3, T4, T5, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, Func<T1, T2, T3, T4, T5, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5)
                {
                    result = resultSelector(current1, current2, current3, current4, current5);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        
        readonly Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            const int CompleteCount = 6;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
       
            readonly Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            const int CompleteCount = 7;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            const int CompleteCount = 8;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            const int CompleteCount = 9;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        readonly IUniTaskAsyncEnumerable<T10> source10;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
            this.source10 = source10;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            static readonly Action<object> Completed10Delegate = Completed10;
            const int CompleteCount = 10;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
            readonly IUniTaskAsyncEnumerable<T10> source10;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            IUniTaskAsyncEnumerator<T10> enumerator10;
            UniTask<bool>.Awaiter awaiter10;
            bool hasCurrent10;
            bool running10;
            T10 current10;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                this.source10 = source10;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                    enumerator10 = source10.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }
                if (!running10)
                {
                    running10 = true;
                    awaiter10 = enumerator10.MoveNextAsync().GetAwaiter();
                    if (awaiter10.IsCompleted)
                    {
                        Completed10(this);
                    }
                    else
                    {
                        awaiter10.SourceOnCompleted(Completed10Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9 || !running10)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed10(object state)
            {
                var self = (_CombineLatest)state;
                self.running10 = false;

                try
                {
                    if (self.awaiter10.GetResult())
                    {
                        self.hasCurrent10 = true;
                        self.current10 = self.enumerator10.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running10 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter10 = self.enumerator10.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter10.SourceOnCompleted(Completed10Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9 && hasCurrent10)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9, current10);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
                if (enumerator10 != null)
                {
                    await enumerator10.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        readonly IUniTaskAsyncEnumerable<T10> source10;
        readonly IUniTaskAsyncEnumerable<T11> source11;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
            this.source10 = source10;
            this.source11 = source11;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            static readonly Action<object> Completed10Delegate = Completed10;
            static readonly Action<object> Completed11Delegate = Completed11;
            const int CompleteCount = 11;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
            readonly IUniTaskAsyncEnumerable<T10> source10;
            readonly IUniTaskAsyncEnumerable<T11> source11;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            IUniTaskAsyncEnumerator<T10> enumerator10;
            UniTask<bool>.Awaiter awaiter10;
            bool hasCurrent10;
            bool running10;
            T10 current10;

            IUniTaskAsyncEnumerator<T11> enumerator11;
            UniTask<bool>.Awaiter awaiter11;
            bool hasCurrent11;
            bool running11;
            T11 current11;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                this.source10 = source10;
                this.source11 = source11;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                    enumerator10 = source10.GetAsyncEnumerator(cancellationToken);
                    enumerator11 = source11.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }
                if (!running10)
                {
                    running10 = true;
                    awaiter10 = enumerator10.MoveNextAsync().GetAwaiter();
                    if (awaiter10.IsCompleted)
                    {
                        Completed10(this);
                    }
                    else
                    {
                        awaiter10.SourceOnCompleted(Completed10Delegate, this);
                    }
                }
                if (!running11)
                {
                    running11 = true;
                    awaiter11 = enumerator11.MoveNextAsync().GetAwaiter();
                    if (awaiter11.IsCompleted)
                    {
                        Completed11(this);
                    }
                    else
                    {
                        awaiter11.SourceOnCompleted(Completed11Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9 || !running10 || !running11)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed10(object state)
            {
                var self = (_CombineLatest)state;
                self.running10 = false;

                try
                {
                    if (self.awaiter10.GetResult())
                    {
                        self.hasCurrent10 = true;
                        self.current10 = self.enumerator10.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running10 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter10 = self.enumerator10.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter10.SourceOnCompleted(Completed10Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed11(object state)
            {
                var self = (_CombineLatest)state;
                self.running11 = false;

                try
                {
                    if (self.awaiter11.GetResult())
                    {
                        self.hasCurrent11 = true;
                        self.current11 = self.enumerator11.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running11 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter11 = self.enumerator11.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter11.SourceOnCompleted(Completed11Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9 && hasCurrent10 && hasCurrent11)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9, current10, current11);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
                if (enumerator10 != null)
                {
                    await enumerator10.DisposeAsync();
                }
                if (enumerator11 != null)
                {
                    await enumerator11.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        readonly IUniTaskAsyncEnumerable<T10> source10;
        readonly IUniTaskAsyncEnumerable<T11> source11;
        readonly IUniTaskAsyncEnumerable<T12> source12;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
            this.source10 = source10;
            this.source11 = source11;
            this.source12 = source12;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            static readonly Action<object> Completed10Delegate = Completed10;
            static readonly Action<object> Completed11Delegate = Completed11;
            static readonly Action<object> Completed12Delegate = Completed12;
            const int CompleteCount = 12;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
            readonly IUniTaskAsyncEnumerable<T10> source10;
            readonly IUniTaskAsyncEnumerable<T11> source11;
            readonly IUniTaskAsyncEnumerable<T12> source12;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            IUniTaskAsyncEnumerator<T10> enumerator10;
            UniTask<bool>.Awaiter awaiter10;
            bool hasCurrent10;
            bool running10;
            T10 current10;

            IUniTaskAsyncEnumerator<T11> enumerator11;
            UniTask<bool>.Awaiter awaiter11;
            bool hasCurrent11;
            bool running11;
            T11 current11;

            IUniTaskAsyncEnumerator<T12> enumerator12;
            UniTask<bool>.Awaiter awaiter12;
            bool hasCurrent12;
            bool running12;
            T12 current12;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                this.source10 = source10;
                this.source11 = source11;
                this.source12 = source12;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                    enumerator10 = source10.GetAsyncEnumerator(cancellationToken);
                    enumerator11 = source11.GetAsyncEnumerator(cancellationToken);
                    enumerator12 = source12.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }
                if (!running10)
                {
                    running10 = true;
                    awaiter10 = enumerator10.MoveNextAsync().GetAwaiter();
                    if (awaiter10.IsCompleted)
                    {
                        Completed10(this);
                    }
                    else
                    {
                        awaiter10.SourceOnCompleted(Completed10Delegate, this);
                    }
                }
                if (!running11)
                {
                    running11 = true;
                    awaiter11 = enumerator11.MoveNextAsync().GetAwaiter();
                    if (awaiter11.IsCompleted)
                    {
                        Completed11(this);
                    }
                    else
                    {
                        awaiter11.SourceOnCompleted(Completed11Delegate, this);
                    }
                }
                if (!running12)
                {
                    running12 = true;
                    awaiter12 = enumerator12.MoveNextAsync().GetAwaiter();
                    if (awaiter12.IsCompleted)
                    {
                        Completed12(this);
                    }
                    else
                    {
                        awaiter12.SourceOnCompleted(Completed12Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9 || !running10 || !running11 || !running12)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed10(object state)
            {
                var self = (_CombineLatest)state;
                self.running10 = false;

                try
                {
                    if (self.awaiter10.GetResult())
                    {
                        self.hasCurrent10 = true;
                        self.current10 = self.enumerator10.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running10 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter10 = self.enumerator10.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter10.SourceOnCompleted(Completed10Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed11(object state)
            {
                var self = (_CombineLatest)state;
                self.running11 = false;

                try
                {
                    if (self.awaiter11.GetResult())
                    {
                        self.hasCurrent11 = true;
                        self.current11 = self.enumerator11.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running11 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter11 = self.enumerator11.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter11.SourceOnCompleted(Completed11Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed12(object state)
            {
                var self = (_CombineLatest)state;
                self.running12 = false;

                try
                {
                    if (self.awaiter12.GetResult())
                    {
                        self.hasCurrent12 = true;
                        self.current12 = self.enumerator12.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running12 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter12 = self.enumerator12.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter12.SourceOnCompleted(Completed12Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9 && hasCurrent10 && hasCurrent11 && hasCurrent12)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9, current10, current11, current12);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
                if (enumerator10 != null)
                {
                    await enumerator10.DisposeAsync();
                }
                if (enumerator11 != null)
                {
                    await enumerator11.DisposeAsync();
                }
                if (enumerator12 != null)
                {
                    await enumerator12.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        readonly IUniTaskAsyncEnumerable<T10> source10;
        readonly IUniTaskAsyncEnumerable<T11> source11;
        readonly IUniTaskAsyncEnumerable<T12> source12;
        readonly IUniTaskAsyncEnumerable<T13> source13;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
            this.source10 = source10;
            this.source11 = source11;
            this.source12 = source12;
            this.source13 = source13;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            static readonly Action<object> Completed10Delegate = Completed10;
            static readonly Action<object> Completed11Delegate = Completed11;
            static readonly Action<object> Completed12Delegate = Completed12;
            static readonly Action<object> Completed13Delegate = Completed13;
            const int CompleteCount = 13;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
            readonly IUniTaskAsyncEnumerable<T10> source10;
            readonly IUniTaskAsyncEnumerable<T11> source11;
            readonly IUniTaskAsyncEnumerable<T12> source12;
            readonly IUniTaskAsyncEnumerable<T13> source13;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            IUniTaskAsyncEnumerator<T10> enumerator10;
            UniTask<bool>.Awaiter awaiter10;
            bool hasCurrent10;
            bool running10;
            T10 current10;

            IUniTaskAsyncEnumerator<T11> enumerator11;
            UniTask<bool>.Awaiter awaiter11;
            bool hasCurrent11;
            bool running11;
            T11 current11;

            IUniTaskAsyncEnumerator<T12> enumerator12;
            UniTask<bool>.Awaiter awaiter12;
            bool hasCurrent12;
            bool running12;
            T12 current12;

            IUniTaskAsyncEnumerator<T13> enumerator13;
            UniTask<bool>.Awaiter awaiter13;
            bool hasCurrent13;
            bool running13;
            T13 current13;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                this.source10 = source10;
                this.source11 = source11;
                this.source12 = source12;
                this.source13 = source13;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                    enumerator10 = source10.GetAsyncEnumerator(cancellationToken);
                    enumerator11 = source11.GetAsyncEnumerator(cancellationToken);
                    enumerator12 = source12.GetAsyncEnumerator(cancellationToken);
                    enumerator13 = source13.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }
                if (!running10)
                {
                    running10 = true;
                    awaiter10 = enumerator10.MoveNextAsync().GetAwaiter();
                    if (awaiter10.IsCompleted)
                    {
                        Completed10(this);
                    }
                    else
                    {
                        awaiter10.SourceOnCompleted(Completed10Delegate, this);
                    }
                }
                if (!running11)
                {
                    running11 = true;
                    awaiter11 = enumerator11.MoveNextAsync().GetAwaiter();
                    if (awaiter11.IsCompleted)
                    {
                        Completed11(this);
                    }
                    else
                    {
                        awaiter11.SourceOnCompleted(Completed11Delegate, this);
                    }
                }
                if (!running12)
                {
                    running12 = true;
                    awaiter12 = enumerator12.MoveNextAsync().GetAwaiter();
                    if (awaiter12.IsCompleted)
                    {
                        Completed12(this);
                    }
                    else
                    {
                        awaiter12.SourceOnCompleted(Completed12Delegate, this);
                    }
                }
                if (!running13)
                {
                    running13 = true;
                    awaiter13 = enumerator13.MoveNextAsync().GetAwaiter();
                    if (awaiter13.IsCompleted)
                    {
                        Completed13(this);
                    }
                    else
                    {
                        awaiter13.SourceOnCompleted(Completed13Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9 || !running10 || !running11 || !running12 || !running13)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed10(object state)
            {
                var self = (_CombineLatest)state;
                self.running10 = false;

                try
                {
                    if (self.awaiter10.GetResult())
                    {
                        self.hasCurrent10 = true;
                        self.current10 = self.enumerator10.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running10 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter10 = self.enumerator10.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter10.SourceOnCompleted(Completed10Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed11(object state)
            {
                var self = (_CombineLatest)state;
                self.running11 = false;

                try
                {
                    if (self.awaiter11.GetResult())
                    {
                        self.hasCurrent11 = true;
                        self.current11 = self.enumerator11.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running11 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter11 = self.enumerator11.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter11.SourceOnCompleted(Completed11Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed12(object state)
            {
                var self = (_CombineLatest)state;
                self.running12 = false;

                try
                {
                    if (self.awaiter12.GetResult())
                    {
                        self.hasCurrent12 = true;
                        self.current12 = self.enumerator12.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running12 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter12 = self.enumerator12.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter12.SourceOnCompleted(Completed12Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed13(object state)
            {
                var self = (_CombineLatest)state;
                self.running13 = false;

                try
                {
                    if (self.awaiter13.GetResult())
                    {
                        self.hasCurrent13 = true;
                        self.current13 = self.enumerator13.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running13 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running13 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running13 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter13 = self.enumerator13.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter13.SourceOnCompleted(Completed13Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9 && hasCurrent10 && hasCurrent11 && hasCurrent12 && hasCurrent13)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9, current10, current11, current12, current13);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
                if (enumerator10 != null)
                {
                    await enumerator10.DisposeAsync();
                }
                if (enumerator11 != null)
                {
                    await enumerator11.DisposeAsync();
                }
                if (enumerator12 != null)
                {
                    await enumerator12.DisposeAsync();
                }
                if (enumerator13 != null)
                {
                    await enumerator13.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        readonly IUniTaskAsyncEnumerable<T10> source10;
        readonly IUniTaskAsyncEnumerable<T11> source11;
        readonly IUniTaskAsyncEnumerable<T12> source12;
        readonly IUniTaskAsyncEnumerable<T13> source13;
        readonly IUniTaskAsyncEnumerable<T14> source14;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, IUniTaskAsyncEnumerable<T14> source14, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
            this.source10 = source10;
            this.source11 = source11;
            this.source12 = source12;
            this.source13 = source13;
            this.source14 = source14;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, source14, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            static readonly Action<object> Completed10Delegate = Completed10;
            static readonly Action<object> Completed11Delegate = Completed11;
            static readonly Action<object> Completed12Delegate = Completed12;
            static readonly Action<object> Completed13Delegate = Completed13;
            static readonly Action<object> Completed14Delegate = Completed14;
            const int CompleteCount = 14;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
            readonly IUniTaskAsyncEnumerable<T10> source10;
            readonly IUniTaskAsyncEnumerable<T11> source11;
            readonly IUniTaskAsyncEnumerable<T12> source12;
            readonly IUniTaskAsyncEnumerable<T13> source13;
            readonly IUniTaskAsyncEnumerable<T14> source14;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            IUniTaskAsyncEnumerator<T10> enumerator10;
            UniTask<bool>.Awaiter awaiter10;
            bool hasCurrent10;
            bool running10;
            T10 current10;

            IUniTaskAsyncEnumerator<T11> enumerator11;
            UniTask<bool>.Awaiter awaiter11;
            bool hasCurrent11;
            bool running11;
            T11 current11;

            IUniTaskAsyncEnumerator<T12> enumerator12;
            UniTask<bool>.Awaiter awaiter12;
            bool hasCurrent12;
            bool running12;
            T12 current12;

            IUniTaskAsyncEnumerator<T13> enumerator13;
            UniTask<bool>.Awaiter awaiter13;
            bool hasCurrent13;
            bool running13;
            T13 current13;

            IUniTaskAsyncEnumerator<T14> enumerator14;
            UniTask<bool>.Awaiter awaiter14;
            bool hasCurrent14;
            bool running14;
            T14 current14;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, IUniTaskAsyncEnumerable<T14> source14, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                this.source10 = source10;
                this.source11 = source11;
                this.source12 = source12;
                this.source13 = source13;
                this.source14 = source14;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                    enumerator10 = source10.GetAsyncEnumerator(cancellationToken);
                    enumerator11 = source11.GetAsyncEnumerator(cancellationToken);
                    enumerator12 = source12.GetAsyncEnumerator(cancellationToken);
                    enumerator13 = source13.GetAsyncEnumerator(cancellationToken);
                    enumerator14 = source14.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }
                if (!running10)
                {
                    running10 = true;
                    awaiter10 = enumerator10.MoveNextAsync().GetAwaiter();
                    if (awaiter10.IsCompleted)
                    {
                        Completed10(this);
                    }
                    else
                    {
                        awaiter10.SourceOnCompleted(Completed10Delegate, this);
                    }
                }
                if (!running11)
                {
                    running11 = true;
                    awaiter11 = enumerator11.MoveNextAsync().GetAwaiter();
                    if (awaiter11.IsCompleted)
                    {
                        Completed11(this);
                    }
                    else
                    {
                        awaiter11.SourceOnCompleted(Completed11Delegate, this);
                    }
                }
                if (!running12)
                {
                    running12 = true;
                    awaiter12 = enumerator12.MoveNextAsync().GetAwaiter();
                    if (awaiter12.IsCompleted)
                    {
                        Completed12(this);
                    }
                    else
                    {
                        awaiter12.SourceOnCompleted(Completed12Delegate, this);
                    }
                }
                if (!running13)
                {
                    running13 = true;
                    awaiter13 = enumerator13.MoveNextAsync().GetAwaiter();
                    if (awaiter13.IsCompleted)
                    {
                        Completed13(this);
                    }
                    else
                    {
                        awaiter13.SourceOnCompleted(Completed13Delegate, this);
                    }
                }
                if (!running14)
                {
                    running14 = true;
                    awaiter14 = enumerator14.MoveNextAsync().GetAwaiter();
                    if (awaiter14.IsCompleted)
                    {
                        Completed14(this);
                    }
                    else
                    {
                        awaiter14.SourceOnCompleted(Completed14Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9 || !running10 || !running11 || !running12 || !running13 || !running14)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed10(object state)
            {
                var self = (_CombineLatest)state;
                self.running10 = false;

                try
                {
                    if (self.awaiter10.GetResult())
                    {
                        self.hasCurrent10 = true;
                        self.current10 = self.enumerator10.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running10 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter10 = self.enumerator10.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter10.SourceOnCompleted(Completed10Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed11(object state)
            {
                var self = (_CombineLatest)state;
                self.running11 = false;

                try
                {
                    if (self.awaiter11.GetResult())
                    {
                        self.hasCurrent11 = true;
                        self.current11 = self.enumerator11.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running11 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter11 = self.enumerator11.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter11.SourceOnCompleted(Completed11Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed12(object state)
            {
                var self = (_CombineLatest)state;
                self.running12 = false;

                try
                {
                    if (self.awaiter12.GetResult())
                    {
                        self.hasCurrent12 = true;
                        self.current12 = self.enumerator12.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running12 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter12 = self.enumerator12.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter12.SourceOnCompleted(Completed12Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed13(object state)
            {
                var self = (_CombineLatest)state;
                self.running13 = false;

                try
                {
                    if (self.awaiter13.GetResult())
                    {
                        self.hasCurrent13 = true;
                        self.current13 = self.enumerator13.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running13 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running13 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running13 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter13 = self.enumerator13.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter13.SourceOnCompleted(Completed13Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed14(object state)
            {
                var self = (_CombineLatest)state;
                self.running14 = false;

                try
                {
                    if (self.awaiter14.GetResult())
                    {
                        self.hasCurrent14 = true;
                        self.current14 = self.enumerator14.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running14 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running14 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running14 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter14 = self.enumerator14.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter14.SourceOnCompleted(Completed14Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9 && hasCurrent10 && hasCurrent11 && hasCurrent12 && hasCurrent13 && hasCurrent14)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9, current10, current11, current12, current13, current14);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
                if (enumerator10 != null)
                {
                    await enumerator10.DisposeAsync();
                }
                if (enumerator11 != null)
                {
                    await enumerator11.DisposeAsync();
                }
                if (enumerator12 != null)
                {
                    await enumerator12.DisposeAsync();
                }
                if (enumerator13 != null)
                {
                    await enumerator13.DisposeAsync();
                }
                if (enumerator14 != null)
                {
                    await enumerator14.DisposeAsync();
                }
            }
        }
    }

    internal class CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> : IUniTaskAsyncEnumerable<TResult>
    {
        readonly IUniTaskAsyncEnumerable<T1> source1;
        readonly IUniTaskAsyncEnumerable<T2> source2;
        readonly IUniTaskAsyncEnumerable<T3> source3;
        readonly IUniTaskAsyncEnumerable<T4> source4;
        readonly IUniTaskAsyncEnumerable<T5> source5;
        readonly IUniTaskAsyncEnumerable<T6> source6;
        readonly IUniTaskAsyncEnumerable<T7> source7;
        readonly IUniTaskAsyncEnumerable<T8> source8;
        readonly IUniTaskAsyncEnumerable<T9> source9;
        readonly IUniTaskAsyncEnumerable<T10> source10;
        readonly IUniTaskAsyncEnumerable<T11> source11;
        readonly IUniTaskAsyncEnumerable<T12> source12;
        readonly IUniTaskAsyncEnumerable<T13> source13;
        readonly IUniTaskAsyncEnumerable<T14> source14;
        readonly IUniTaskAsyncEnumerable<T15> source15;
        
        readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> resultSelector;

        public CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, IUniTaskAsyncEnumerable<T14> source14, IUniTaskAsyncEnumerable<T15> source15, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> resultSelector)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.source3 = source3;
            this.source4 = source4;
            this.source5 = source5;
            this.source6 = source6;
            this.source7 = source7;
            this.source8 = source8;
            this.source9 = source9;
            this.source10 = source10;
            this.source11 = source11;
            this.source12 = source12;
            this.source13 = source13;
            this.source14 = source14;
            this.source15 = source15;
        
            this.resultSelector = resultSelector;
        }

        public IUniTaskAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new _CombineLatest(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, source14, source15, resultSelector, cancellationToken);
        }

        class _CombineLatest : MoveNextSource, IUniTaskAsyncEnumerator<TResult>
        {
            static readonly Action<object> Completed1Delegate = Completed1;
            static readonly Action<object> Completed2Delegate = Completed2;
            static readonly Action<object> Completed3Delegate = Completed3;
            static readonly Action<object> Completed4Delegate = Completed4;
            static readonly Action<object> Completed5Delegate = Completed5;
            static readonly Action<object> Completed6Delegate = Completed6;
            static readonly Action<object> Completed7Delegate = Completed7;
            static readonly Action<object> Completed8Delegate = Completed8;
            static readonly Action<object> Completed9Delegate = Completed9;
            static readonly Action<object> Completed10Delegate = Completed10;
            static readonly Action<object> Completed11Delegate = Completed11;
            static readonly Action<object> Completed12Delegate = Completed12;
            static readonly Action<object> Completed13Delegate = Completed13;
            static readonly Action<object> Completed14Delegate = Completed14;
            static readonly Action<object> Completed15Delegate = Completed15;
            const int CompleteCount = 15;

            readonly IUniTaskAsyncEnumerable<T1> source1;
            readonly IUniTaskAsyncEnumerable<T2> source2;
            readonly IUniTaskAsyncEnumerable<T3> source3;
            readonly IUniTaskAsyncEnumerable<T4> source4;
            readonly IUniTaskAsyncEnumerable<T5> source5;
            readonly IUniTaskAsyncEnumerable<T6> source6;
            readonly IUniTaskAsyncEnumerable<T7> source7;
            readonly IUniTaskAsyncEnumerable<T8> source8;
            readonly IUniTaskAsyncEnumerable<T9> source9;
            readonly IUniTaskAsyncEnumerable<T10> source10;
            readonly IUniTaskAsyncEnumerable<T11> source11;
            readonly IUniTaskAsyncEnumerable<T12> source12;
            readonly IUniTaskAsyncEnumerable<T13> source13;
            readonly IUniTaskAsyncEnumerable<T14> source14;
            readonly IUniTaskAsyncEnumerable<T15> source15;
       
            readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> resultSelector;
            CancellationToken cancellationToken;

            IUniTaskAsyncEnumerator<T1> enumerator1;
            UniTask<bool>.Awaiter awaiter1;
            bool hasCurrent1;
            bool running1;
            T1 current1;

            IUniTaskAsyncEnumerator<T2> enumerator2;
            UniTask<bool>.Awaiter awaiter2;
            bool hasCurrent2;
            bool running2;
            T2 current2;

            IUniTaskAsyncEnumerator<T3> enumerator3;
            UniTask<bool>.Awaiter awaiter3;
            bool hasCurrent3;
            bool running3;
            T3 current3;

            IUniTaskAsyncEnumerator<T4> enumerator4;
            UniTask<bool>.Awaiter awaiter4;
            bool hasCurrent4;
            bool running4;
            T4 current4;

            IUniTaskAsyncEnumerator<T5> enumerator5;
            UniTask<bool>.Awaiter awaiter5;
            bool hasCurrent5;
            bool running5;
            T5 current5;

            IUniTaskAsyncEnumerator<T6> enumerator6;
            UniTask<bool>.Awaiter awaiter6;
            bool hasCurrent6;
            bool running6;
            T6 current6;

            IUniTaskAsyncEnumerator<T7> enumerator7;
            UniTask<bool>.Awaiter awaiter7;
            bool hasCurrent7;
            bool running7;
            T7 current7;

            IUniTaskAsyncEnumerator<T8> enumerator8;
            UniTask<bool>.Awaiter awaiter8;
            bool hasCurrent8;
            bool running8;
            T8 current8;

            IUniTaskAsyncEnumerator<T9> enumerator9;
            UniTask<bool>.Awaiter awaiter9;
            bool hasCurrent9;
            bool running9;
            T9 current9;

            IUniTaskAsyncEnumerator<T10> enumerator10;
            UniTask<bool>.Awaiter awaiter10;
            bool hasCurrent10;
            bool running10;
            T10 current10;

            IUniTaskAsyncEnumerator<T11> enumerator11;
            UniTask<bool>.Awaiter awaiter11;
            bool hasCurrent11;
            bool running11;
            T11 current11;

            IUniTaskAsyncEnumerator<T12> enumerator12;
            UniTask<bool>.Awaiter awaiter12;
            bool hasCurrent12;
            bool running12;
            T12 current12;

            IUniTaskAsyncEnumerator<T13> enumerator13;
            UniTask<bool>.Awaiter awaiter13;
            bool hasCurrent13;
            bool running13;
            T13 current13;

            IUniTaskAsyncEnumerator<T14> enumerator14;
            UniTask<bool>.Awaiter awaiter14;
            bool hasCurrent14;
            bool running14;
            T14 current14;

            IUniTaskAsyncEnumerator<T15> enumerator15;
            UniTask<bool>.Awaiter awaiter15;
            bool hasCurrent15;
            bool running15;
            T15 current15;

            int completedCount;
            bool syncRunning;
            TResult result;

            public _CombineLatest(IUniTaskAsyncEnumerable<T1> source1, IUniTaskAsyncEnumerable<T2> source2, IUniTaskAsyncEnumerable<T3> source3, IUniTaskAsyncEnumerable<T4> source4, IUniTaskAsyncEnumerable<T5> source5, IUniTaskAsyncEnumerable<T6> source6, IUniTaskAsyncEnumerable<T7> source7, IUniTaskAsyncEnumerable<T8> source8, IUniTaskAsyncEnumerable<T9> source9, IUniTaskAsyncEnumerable<T10> source10, IUniTaskAsyncEnumerable<T11> source11, IUniTaskAsyncEnumerable<T12> source12, IUniTaskAsyncEnumerable<T13> source13, IUniTaskAsyncEnumerable<T14> source14, IUniTaskAsyncEnumerable<T15> source15, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> resultSelector, CancellationToken cancellationToken)
            {
                this.source1 = source1;
                this.source2 = source2;
                this.source3 = source3;
                this.source4 = source4;
                this.source5 = source5;
                this.source6 = source6;
                this.source7 = source7;
                this.source8 = source8;
                this.source9 = source9;
                this.source10 = source10;
                this.source11 = source11;
                this.source12 = source12;
                this.source13 = source13;
                this.source14 = source14;
                this.source15 = source15;
                
                this.resultSelector = resultSelector;
                this.cancellationToken = cancellationToken;
                TaskTracker.TrackActiveTask(this, 3);
            }

            public TResult Current => result;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (completedCount == CompleteCount) return CompletedTasks.False;

                if (enumerator1 == null)
                {
                    enumerator1 = source1.GetAsyncEnumerator(cancellationToken);
                    enumerator2 = source2.GetAsyncEnumerator(cancellationToken);
                    enumerator3 = source3.GetAsyncEnumerator(cancellationToken);
                    enumerator4 = source4.GetAsyncEnumerator(cancellationToken);
                    enumerator5 = source5.GetAsyncEnumerator(cancellationToken);
                    enumerator6 = source6.GetAsyncEnumerator(cancellationToken);
                    enumerator7 = source7.GetAsyncEnumerator(cancellationToken);
                    enumerator8 = source8.GetAsyncEnumerator(cancellationToken);
                    enumerator9 = source9.GetAsyncEnumerator(cancellationToken);
                    enumerator10 = source10.GetAsyncEnumerator(cancellationToken);
                    enumerator11 = source11.GetAsyncEnumerator(cancellationToken);
                    enumerator12 = source12.GetAsyncEnumerator(cancellationToken);
                    enumerator13 = source13.GetAsyncEnumerator(cancellationToken);
                    enumerator14 = source14.GetAsyncEnumerator(cancellationToken);
                    enumerator15 = source15.GetAsyncEnumerator(cancellationToken);
                }

                completionSource.Reset();

                AGAIN:
                syncRunning = true;
                if (!running1)
                {
                    running1 = true;
                    awaiter1 = enumerator1.MoveNextAsync().GetAwaiter();
                    if (awaiter1.IsCompleted)
                    {
                        Completed1(this);
                    }
                    else
                    {
                        awaiter1.SourceOnCompleted(Completed1Delegate, this);
                    }
                }
                if (!running2)
                {
                    running2 = true;
                    awaiter2 = enumerator2.MoveNextAsync().GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        Completed2(this);
                    }
                    else
                    {
                        awaiter2.SourceOnCompleted(Completed2Delegate, this);
                    }
                }
                if (!running3)
                {
                    running3 = true;
                    awaiter3 = enumerator3.MoveNextAsync().GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        Completed3(this);
                    }
                    else
                    {
                        awaiter3.SourceOnCompleted(Completed3Delegate, this);
                    }
                }
                if (!running4)
                {
                    running4 = true;
                    awaiter4 = enumerator4.MoveNextAsync().GetAwaiter();
                    if (awaiter4.IsCompleted)
                    {
                        Completed4(this);
                    }
                    else
                    {
                        awaiter4.SourceOnCompleted(Completed4Delegate, this);
                    }
                }
                if (!running5)
                {
                    running5 = true;
                    awaiter5 = enumerator5.MoveNextAsync().GetAwaiter();
                    if (awaiter5.IsCompleted)
                    {
                        Completed5(this);
                    }
                    else
                    {
                        awaiter5.SourceOnCompleted(Completed5Delegate, this);
                    }
                }
                if (!running6)
                {
                    running6 = true;
                    awaiter6 = enumerator6.MoveNextAsync().GetAwaiter();
                    if (awaiter6.IsCompleted)
                    {
                        Completed6(this);
                    }
                    else
                    {
                        awaiter6.SourceOnCompleted(Completed6Delegate, this);
                    }
                }
                if (!running7)
                {
                    running7 = true;
                    awaiter7 = enumerator7.MoveNextAsync().GetAwaiter();
                    if (awaiter7.IsCompleted)
                    {
                        Completed7(this);
                    }
                    else
                    {
                        awaiter7.SourceOnCompleted(Completed7Delegate, this);
                    }
                }
                if (!running8)
                {
                    running8 = true;
                    awaiter8 = enumerator8.MoveNextAsync().GetAwaiter();
                    if (awaiter8.IsCompleted)
                    {
                        Completed8(this);
                    }
                    else
                    {
                        awaiter8.SourceOnCompleted(Completed8Delegate, this);
                    }
                }
                if (!running9)
                {
                    running9 = true;
                    awaiter9 = enumerator9.MoveNextAsync().GetAwaiter();
                    if (awaiter9.IsCompleted)
                    {
                        Completed9(this);
                    }
                    else
                    {
                        awaiter9.SourceOnCompleted(Completed9Delegate, this);
                    }
                }
                if (!running10)
                {
                    running10 = true;
                    awaiter10 = enumerator10.MoveNextAsync().GetAwaiter();
                    if (awaiter10.IsCompleted)
                    {
                        Completed10(this);
                    }
                    else
                    {
                        awaiter10.SourceOnCompleted(Completed10Delegate, this);
                    }
                }
                if (!running11)
                {
                    running11 = true;
                    awaiter11 = enumerator11.MoveNextAsync().GetAwaiter();
                    if (awaiter11.IsCompleted)
                    {
                        Completed11(this);
                    }
                    else
                    {
                        awaiter11.SourceOnCompleted(Completed11Delegate, this);
                    }
                }
                if (!running12)
                {
                    running12 = true;
                    awaiter12 = enumerator12.MoveNextAsync().GetAwaiter();
                    if (awaiter12.IsCompleted)
                    {
                        Completed12(this);
                    }
                    else
                    {
                        awaiter12.SourceOnCompleted(Completed12Delegate, this);
                    }
                }
                if (!running13)
                {
                    running13 = true;
                    awaiter13 = enumerator13.MoveNextAsync().GetAwaiter();
                    if (awaiter13.IsCompleted)
                    {
                        Completed13(this);
                    }
                    else
                    {
                        awaiter13.SourceOnCompleted(Completed13Delegate, this);
                    }
                }
                if (!running14)
                {
                    running14 = true;
                    awaiter14 = enumerator14.MoveNextAsync().GetAwaiter();
                    if (awaiter14.IsCompleted)
                    {
                        Completed14(this);
                    }
                    else
                    {
                        awaiter14.SourceOnCompleted(Completed14Delegate, this);
                    }
                }
                if (!running15)
                {
                    running15 = true;
                    awaiter15 = enumerator15.MoveNextAsync().GetAwaiter();
                    if (awaiter15.IsCompleted)
                    {
                        Completed15(this);
                    }
                    else
                    {
                        awaiter15.SourceOnCompleted(Completed15Delegate, this);
                    }
                }

                if (!running1 || !running2 || !running3 || !running4 || !running5 || !running6 || !running7 || !running8 || !running9 || !running10 || !running11 || !running12 || !running13 || !running14 || !running15)
                {
                    goto AGAIN;
                }
                syncRunning = false;

                return new UniTask<bool>(this, completionSource.Version);
            }

            static void Completed1(object state)
            {
                var self = (_CombineLatest)state;
                self.running1 = false;

                try
                {
                    if (self.awaiter1.GetResult())
                    {
                        self.hasCurrent1 = true;
                        self.current1 = self.enumerator1.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running1 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running1 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter1 = self.enumerator1.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter1.SourceOnCompleted(Completed1Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed2(object state)
            {
                var self = (_CombineLatest)state;
                self.running2 = false;

                try
                {
                    if (self.awaiter2.GetResult())
                    {
                        self.hasCurrent2 = true;
                        self.current2 = self.enumerator2.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running2 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running2 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter2 = self.enumerator2.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter2.SourceOnCompleted(Completed2Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed3(object state)
            {
                var self = (_CombineLatest)state;
                self.running3 = false;

                try
                {
                    if (self.awaiter3.GetResult())
                    {
                        self.hasCurrent3 = true;
                        self.current3 = self.enumerator3.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running3 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running3 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter3 = self.enumerator3.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter3.SourceOnCompleted(Completed3Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed4(object state)
            {
                var self = (_CombineLatest)state;
                self.running4 = false;

                try
                {
                    if (self.awaiter4.GetResult())
                    {
                        self.hasCurrent4 = true;
                        self.current4 = self.enumerator4.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running4 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running4 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter4 = self.enumerator4.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter4.SourceOnCompleted(Completed4Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed5(object state)
            {
                var self = (_CombineLatest)state;
                self.running5 = false;

                try
                {
                    if (self.awaiter5.GetResult())
                    {
                        self.hasCurrent5 = true;
                        self.current5 = self.enumerator5.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running5 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running5 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter5 = self.enumerator5.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter5.SourceOnCompleted(Completed5Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed6(object state)
            {
                var self = (_CombineLatest)state;
                self.running6 = false;

                try
                {
                    if (self.awaiter6.GetResult())
                    {
                        self.hasCurrent6 = true;
                        self.current6 = self.enumerator6.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running6 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running6 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter6 = self.enumerator6.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter6.SourceOnCompleted(Completed6Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed7(object state)
            {
                var self = (_CombineLatest)state;
                self.running7 = false;

                try
                {
                    if (self.awaiter7.GetResult())
                    {
                        self.hasCurrent7 = true;
                        self.current7 = self.enumerator7.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running7 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running7 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter7 = self.enumerator7.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter7.SourceOnCompleted(Completed7Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed8(object state)
            {
                var self = (_CombineLatest)state;
                self.running8 = false;

                try
                {
                    if (self.awaiter8.GetResult())
                    {
                        self.hasCurrent8 = true;
                        self.current8 = self.enumerator8.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running8 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running8 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter8 = self.enumerator8.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter8.SourceOnCompleted(Completed8Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed9(object state)
            {
                var self = (_CombineLatest)state;
                self.running9 = false;

                try
                {
                    if (self.awaiter9.GetResult())
                    {
                        self.hasCurrent9 = true;
                        self.current9 = self.enumerator9.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running9 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running9 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter9 = self.enumerator9.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter9.SourceOnCompleted(Completed9Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed10(object state)
            {
                var self = (_CombineLatest)state;
                self.running10 = false;

                try
                {
                    if (self.awaiter10.GetResult())
                    {
                        self.hasCurrent10 = true;
                        self.current10 = self.enumerator10.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running10 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running10 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter10 = self.enumerator10.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter10.SourceOnCompleted(Completed10Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed11(object state)
            {
                var self = (_CombineLatest)state;
                self.running11 = false;

                try
                {
                    if (self.awaiter11.GetResult())
                    {
                        self.hasCurrent11 = true;
                        self.current11 = self.enumerator11.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running11 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running11 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter11 = self.enumerator11.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter11.SourceOnCompleted(Completed11Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed12(object state)
            {
                var self = (_CombineLatest)state;
                self.running12 = false;

                try
                {
                    if (self.awaiter12.GetResult())
                    {
                        self.hasCurrent12 = true;
                        self.current12 = self.enumerator12.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running12 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running12 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter12 = self.enumerator12.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter12.SourceOnCompleted(Completed12Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed13(object state)
            {
                var self = (_CombineLatest)state;
                self.running13 = false;

                try
                {
                    if (self.awaiter13.GetResult())
                    {
                        self.hasCurrent13 = true;
                        self.current13 = self.enumerator13.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running13 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running13 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running13 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter13 = self.enumerator13.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter13.SourceOnCompleted(Completed13Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed14(object state)
            {
                var self = (_CombineLatest)state;
                self.running14 = false;

                try
                {
                    if (self.awaiter14.GetResult())
                    {
                        self.hasCurrent14 = true;
                        self.current14 = self.enumerator14.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running14 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running14 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running14 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter14 = self.enumerator14.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter14.SourceOnCompleted(Completed14Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            static void Completed15(object state)
            {
                var self = (_CombineLatest)state;
                self.running15 = false;

                try
                {
                    if (self.awaiter15.GetResult())
                    {
                        self.hasCurrent15 = true;
                        self.current15 = self.enumerator15.Current;
                        goto SUCCESS;
                    }
                    else
                    {
                        self.running15 = true; // as complete, no more call MoveNextAsync.
                        if (Interlocked.Increment(ref self.completedCount) == CompleteCount)
                        {
                            goto COMPLETE;
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    self.running15 = true; // as complete, no more call MoveNextAsync.
                    self.completedCount = CompleteCount;
                    self.completionSource.TrySetException(ex);
                    return;
                }

                SUCCESS:
                if (!self.TrySetResult())
                {
                    if (self.syncRunning) return;
                    self.running15 = true; // as complete, no more call MoveNextAsync.
                    try
                    {
                        self.awaiter15 = self.enumerator15.MoveNextAsync().GetAwaiter();
                    }
                    catch (Exception ex)
                    {
                        self.completedCount = CompleteCount;
                        self.completionSource.TrySetException(ex);
                        return;
                    }

                    self.awaiter15.SourceOnCompleted(Completed15Delegate, self);
                }
                return;
                COMPLETE:
                self.completionSource.TrySetResult(false);
                return;
            }

            bool TrySetResult()
            {
                if (hasCurrent1 && hasCurrent2 && hasCurrent3 && hasCurrent4 && hasCurrent5 && hasCurrent6 && hasCurrent7 && hasCurrent8 && hasCurrent9 && hasCurrent10 && hasCurrent11 && hasCurrent12 && hasCurrent13 && hasCurrent14 && hasCurrent15)
                {
                    result = resultSelector(current1, current2, current3, current4, current5, current6, current7, current8, current9, current10, current11, current12, current13, current14, current15);
                    completionSource.TrySetResult(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async UniTask DisposeAsync()
            {
                TaskTracker.RemoveTracking(this);
                if (enumerator1 != null)
                {
                    await enumerator1.DisposeAsync();
                }
                if (enumerator2 != null)
                {
                    await enumerator2.DisposeAsync();
                }
                if (enumerator3 != null)
                {
                    await enumerator3.DisposeAsync();
                }
                if (enumerator4 != null)
                {
                    await enumerator4.DisposeAsync();
                }
                if (enumerator5 != null)
                {
                    await enumerator5.DisposeAsync();
                }
                if (enumerator6 != null)
                {
                    await enumerator6.DisposeAsync();
                }
                if (enumerator7 != null)
                {
                    await enumerator7.DisposeAsync();
                }
                if (enumerator8 != null)
                {
                    await enumerator8.DisposeAsync();
                }
                if (enumerator9 != null)
                {
                    await enumerator9.DisposeAsync();
                }
                if (enumerator10 != null)
                {
                    await enumerator10.DisposeAsync();
                }
                if (enumerator11 != null)
                {
                    await enumerator11.DisposeAsync();
                }
                if (enumerator12 != null)
                {
                    await enumerator12.DisposeAsync();
                }
                if (enumerator13 != null)
                {
                    await enumerator13.DisposeAsync();
                }
                if (enumerator14 != null)
                {
                    await enumerator14.DisposeAsync();
                }
                if (enumerator15 != null)
                {
                    await enumerator15.DisposeAsync();
                }
            }
        }
    }

}