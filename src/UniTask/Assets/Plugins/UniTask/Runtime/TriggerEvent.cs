using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public interface ITriggerEvent<T>
    {
        void SetResult(T value);
        void SetCanceled(CancellationToken cancellationToken);
        void Add(IResolveCancelPromise<T> handler);
        void Remove(IResolveCancelPromise<T> handler);
    }

    // be careful to use, itself is struct.
    public struct TriggerEvent<T> : ITriggerEvent<T>
    {
        // optimize: many cases, handler is single.
        IResolveCancelPromise<T> singleHandler;

        IResolveCancelPromise<T>[] handlers;

        // when running(in TrySetResult), does not add immediately.
        bool isRunning;
        IResolveCancelPromise<T> waitHandler;
        MinimumQueue<IResolveCancelPromise<T>> waitQueue;

        public void SetResult(T value)
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    singleHandler.TrySetResult(value);
                }
                catch (Exception ex)
                {
#if UNITY_2018_3_OR_NEWER
                    UnityEngine.Debug.LogException(ex);
#else
                    Console.WriteLine(ex);
#endif
                }
            }

            if (handlers != null)
            {
                for (int i = 0; i < handlers.Length; i++)
                {
                    if (handlers[i] != null)
                    {
                        try
                        {
                            handlers[i].TrySetResult(value);
                        }
                        catch (Exception ex)
                        {
                            handlers[i] = null;
#if UNITY_2018_3_OR_NEWER
                            UnityEngine.Debug.LogException(ex);
#else
                            Console.WriteLine(ex);
#endif
                        }
                    }
                }
            }

            isRunning = false;

            if (waitHandler != null)
            {
                var h = waitHandler;
                waitHandler = null;
                Add(h);
            }

            if (waitQueue != null)
            {
                while (waitQueue.Count != 0)
                {
                    Add(waitQueue.Dequeue());
                }
            }
        }

        public void SetCanceled(CancellationToken cancellationToken)
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    ((ICancelPromise)singleHandler).TrySetCanceled(cancellationToken);
                }
                catch (Exception ex)
                {
#if UNITY_2018_3_OR_NEWER
                    UnityEngine.Debug.LogException(ex);
#else
                    Console.WriteLine(ex);
#endif
                }
            }

            if (handlers != null)
            {
                for (int i = 0; i < handlers.Length; i++)
                {
                    if (handlers[i] != null)
                    {
                        try
                        {
                            ((ICancelPromise)handlers[i]).TrySetCanceled(cancellationToken);
                        }
                        catch (Exception ex)
                        {
#if UNITY_2018_3_OR_NEWER
                            UnityEngine.Debug.LogException(ex);
#else
                            Console.WriteLine(ex);
#endif
                            handlers[i] = null;
                        }
                    }
                }
            }

            isRunning = false;

            if (waitHandler != null)
            {
                var h = waitHandler;
                waitHandler = null;
                Add(h);
            }

            if (waitQueue != null)
            {
                while (waitQueue.Count != 0)
                {
                    Add(waitQueue.Dequeue());
                }
            }
        }

        public void Add(IResolveCancelPromise<T> handler)
        {
            if (isRunning)
            {
                if (waitHandler == null)
                {
                    waitHandler = handler;
                    return;
                }

                if (waitQueue == null)
                {
                    waitQueue = new MinimumQueue<IResolveCancelPromise<T>>(4);
                }
                waitQueue.Enqueue(handler);
                return;
            }

            if (singleHandler == null)
            {
                singleHandler = handler;
            }
            else
            {
                if (handlers == null)
                {
                    handlers = new IResolveCancelPromise<T>[4];
                }

                // check empty
                for (int i = 0; i < handlers.Length; i++)
                {
                    if (handlers[i] == null)
                    {
                        handlers[i] = handler;
                        return;
                    }
                }

                // full, ensure capacity
                var last = handlers.Length;
                {
                    EnsureCapacity(ref handlers);
                    handlers[last] = handler;
                }
            }
        }

        static void EnsureCapacity(ref IResolveCancelPromise<T>[] array)
        {
            var newSize = array.Length * 2;
            var newArray = new IResolveCancelPromise<T>[newSize];
            Array.Copy(array, 0, newArray, 0, array.Length);
            array = newArray;
        }

        public void Remove(IResolveCancelPromise<T> handler)
        {
            if (singleHandler == handler)
            {
                singleHandler = null;
            }
            else
            {
                if (handlers != null)
                {
                    for (int i = 0; i < handlers.Length; i++)
                    {
                        if (handlers[i] == handler)
                        {
                            // fill null.
                            handlers[i] = null;
                            return;
                        }
                    }
                }
            }
        }
    }
}
