#if NET_4_6 || NET_STANDARD_2_0 || CSHARP_7_OR_LATER

using System;

namespace UniRx.Async.Internal
{
    internal static class FuncExtensions
    {
        // avoid lambda capture

        internal static Action<T> AsFuncOfT<T>(this Action action)
        {
            return new Action<T>(action.Invoke);
        }

        static void Invoke<T>(this Action action, T unused)
        {
            action();
        }
    }
}

#endif