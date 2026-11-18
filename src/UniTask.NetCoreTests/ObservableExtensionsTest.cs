using Cysharp.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Xunit;

namespace NetCoreTests
{
    public class ObservableExtensionsTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SubscriptionFailureReleasesObserver(bool useFirstValue)
        {
            using var cancellation = new CancellationTokenSource();
            var observer = SubscribeAndObserveFailure(useFirstValue, cancellation.Token);

            GC.Collect();
            Assert.False(observer.IsAlive);
            GC.KeepAlive(cancellation);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static WeakReference SubscribeAndObserveFailure(bool useFirstValue, CancellationToken cancellationToken)
        {
            var observable = new ThrowingObservable();
            var task = observable.ToUniTask(useFirstValue, cancellationToken);

            var exception = Assert.Throws<InvalidOperationException>(() => task.GetAwaiter().GetResult());
            Assert.Same(observable.Exception, exception);
            return observable.Observer;
        }

        sealed class ThrowingObservable : IObservable<int>
        {
            public InvalidOperationException Exception { get; } = new InvalidOperationException();
            public WeakReference Observer { get; private set; }

            public IDisposable Subscribe(IObserver<int> observer)
            {
                Observer = new WeakReference(observer);
                throw Exception;
            }
        }
    }
}
