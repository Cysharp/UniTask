using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public interface IUniTaskAsyncEnumerable<out T>
    {
        IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    }

    public interface IUniTaskAsyncEnumerator<out T> : IUniTaskAsyncDisposable
    {
        T Current { get; }
        UniTask<bool> MoveNextAsync();
    }

    public interface IUniTaskAsyncDisposable
    {
        UniTask DisposeAsync();
    }
}