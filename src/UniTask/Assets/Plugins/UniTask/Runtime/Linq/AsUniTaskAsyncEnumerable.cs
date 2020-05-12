namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static IUniTaskAsyncEnumerable<TSource> AsUniTaskAsyncEnumerable<TSource>(this IUniTaskAsyncEnumerable<TSource> source)
        {
            return source;
        }
    }
}
