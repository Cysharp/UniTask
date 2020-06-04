using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
    /// <summary>
    /// Lightweight IProgress[T] factory.
    /// </summary>
    public static class Progress
    {
        public static IProgress<T> Create<T>(Action<T> handler)
        {
            if (handler == null) return NullProgress<T>.Instance;
            return new AnonymousProgress<T>(handler);
        }

        public static IProgress<T> CreateOnlyValueChanged<T>(Action<T> handler, IEqualityComparer<T> comparer = null)
        {
            if (handler == null) return NullProgress<T>.Instance;
#if UNITY_2018_3_OR_NEWER
            return new OnlyValueChangedProgress<T>(handler, comparer ?? UnityEqualityComparer.GetDefault<T>());
#else
            return new OnlyValueChangedProgress<T>(handler, comparer ?? EqualityComparer<T>.Default);
#endif
        }

        sealed class NullProgress<T> : IProgress<T>
        {
            public static readonly IProgress<T> Instance = new NullProgress<T>();

            NullProgress()
            {

            }

            public void Report(T value)
            {
            }
        }

        sealed class AnonymousProgress<T> : IProgress<T>
        {
            readonly Action<T> action;

            public AnonymousProgress(Action<T> action)
            {
                this.action = action;
            }

            public void Report(T value)
            {
                action(value);
            }
        }

        sealed class OnlyValueChangedProgress<T> : IProgress<T>
        {
            readonly Action<T> action;
            readonly IEqualityComparer<T> comparer;
            bool isFirstCall;
            T latestValue;

            public OnlyValueChangedProgress(Action<T> action, IEqualityComparer<T> comparer)
            {
                this.action = action;
                this.comparer = comparer;
                this.isFirstCall = true;
            }

            public void Report(T value)
            {
                if (isFirstCall)
                {
                    isFirstCall = false;
                }
                else if (comparer.Equals(value, latestValue))
                {
                    return;
                }

                latestValue = value;
                action(value);
            }
        }
    }
}