#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public class AsyncLazy
    {
        Func<UniTask> valueFactory;
        UniTask target;
        object syncLock;
        bool initialized;

        public AsyncLazy(Func<UniTask> valueFactory)
        {
            this.valueFactory = valueFactory;
            this.target = default;
            this.syncLock = new object();
            this.initialized = false;
        }

        internal AsyncLazy(UniTask value)
        {
            this.valueFactory = null;
            this.target = value;
            this.syncLock = null;
            this.initialized = true;
        }

        public UniTask Task => EnsureInitialized();

        public UniTask.Awaiter GetAwaiter() => EnsureInitialized().GetAwaiter();

        UniTask EnsureInitialized()
        {
            if (Volatile.Read(ref initialized))
            {
                return target;
            }

            return EnsureInitializedCore();
        }

        UniTask EnsureInitializedCore()
        {
            lock (syncLock)
            {
                if (!Volatile.Read(ref initialized))
                {
                    var f = Interlocked.Exchange(ref valueFactory, null);
                    if (f != null)
                    {
                        target = f().Preserve(); // with preserve(allow multiple await).
                        Volatile.Write(ref initialized, true);
                    }
                }
            }

            return target;
        }
    }

    public class AsyncLazy<T>
    {
        Func<UniTask<T>> valueFactory;
        UniTask<T> target;
        object syncLock;
        bool initialized;

        public AsyncLazy(Func<UniTask<T>> valueFactory)
        {
            this.valueFactory = valueFactory;
            this.target = default;
            this.syncLock = new object();
            this.initialized = false;
        }

        internal AsyncLazy(UniTask<T> value)
        {
            this.valueFactory = null;
            this.target = value;
            this.syncLock = null;
            this.initialized = true;
        }

        public UniTask<T> Task => EnsureInitialized();

        public UniTask<T>.Awaiter GetAwaiter() => EnsureInitialized().GetAwaiter();

        UniTask<T> EnsureInitialized()
        {
            if (Volatile.Read(ref initialized))
            {
                return target;
            }

            return EnsureInitializedCore();
        }

        UniTask<T> EnsureInitializedCore()
        {
            lock (syncLock)
            {
                if (!Volatile.Read(ref initialized))
                {
                    var f = Interlocked.Exchange(ref valueFactory, null);
                    if (f != null)
                    {
                        target = f().Preserve(); // with preserve(allow multiple await).
                        Volatile.Write(ref initialized, true);
                    }
                }
            }

            return target;
        }
    }
}
