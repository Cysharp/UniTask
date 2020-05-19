using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public interface ITriggerHandler<T>
    {
        void OnNext(T value);
        void OnError(Exception ex);
        void OnCompleted();
        void OnCanceled(CancellationToken cancellationToken);
    }

    // be careful to use, itself is struct.
    public struct TriggerEvent<T>
    {
        // optimize: many cases, handler is single.
        ITriggerHandler<T> singleHandler;

        ITriggerHandler<T>[] handlers;

        // when running(in TrySetResult), does not add immediately(trampoline).
        bool isRunning;
        ITriggerHandler<T> waitHandler;
        MinimumQueue<ITriggerHandler<T>> waitQueue;

        public void SetResult(T value)
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    singleHandler.OnNext(value);
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
                            handlers[i].OnNext(value);
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
                    (singleHandler).OnCanceled(cancellationToken);
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
                            (handlers[i]).OnCanceled(cancellationToken);
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

        public void SetCompleted()
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    (singleHandler).OnCompleted();
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
                            (handlers[i]).OnCompleted();
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

        public void SetError(Exception exception)
        {
            isRunning = true;

            if (singleHandler != null)
            {
                try
                {
                    singleHandler.OnError(exception);
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
                            handlers[i].OnError(exception);
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

        public void Add(ITriggerHandler<T> handler)
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
                    waitQueue = new MinimumQueue<ITriggerHandler<T>>(4);
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
                    handlers = new ITriggerHandler<T>[4];
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

        static void EnsureCapacity(ref ITriggerHandler<T>[] array)
        {
            var newSize = array.Length * 2;
            var newArray = new ITriggerHandler<T>[newSize];
            Array.Copy(array, 0, newArray, 0, array.Length);
            array = newArray;
        }

        public void Remove(ITriggerHandler<T> handler)
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
