#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))

using System;
using UnityEngine;

namespace UniRx.Async.Internal
{
    internal sealed class PlayerLoopRunner
    {
        const int InitialSize = 16;

        readonly object runningAndQueueLock = new object();
        readonly object arrayLock = new object();
        readonly Action<Exception> unhandledExceptionCallback;

        int tail = 0;
        bool running = false;
        IPlayerLoopItem[] loopItems = new IPlayerLoopItem[InitialSize];
        MinimumQueue<IPlayerLoopItem> waitQueue = new MinimumQueue<IPlayerLoopItem>(InitialSize);

        public PlayerLoopRunner()
        {
            this.unhandledExceptionCallback = ex => Debug.LogException(ex);
        }

        public void AddAction(IPlayerLoopItem item)
        {
            lock (runningAndQueueLock)
            {
                if (running)
                {
                    waitQueue.Enqueue(item);
                    return;
                }
            }

            lock (arrayLock)
            {
                // Ensure Capacity
                if (loopItems.Length == tail)
                {
                    Array.Resize(ref loopItems, checked(tail * 2));
                }
                loopItems[tail++] = item;
            }
        }

        public void Run()
        {
            lock (runningAndQueueLock)
            {
                running = true;
            }

            lock (arrayLock)
            {
                var j = tail - 1;

                // eliminate array-bound check for i
                for (int i = 0; i < loopItems.Length; i++)
                {
                    var action = loopItems[i];
                    if (action != null)
                    {
                        try
                        {
                            if (!action.MoveNext())
                            {
                                loopItems[i] = null;
                            }
                            else
                            {
                                continue; // next i 
                            }
                        }
                        catch (Exception ex)
                        {
                            loopItems[i] = null;
                            try
                            {
                                unhandledExceptionCallback(ex);
                            }
                            catch { }
                        }
                    }

                    // find null, loop from tail
                    while (i < j)
                    {
                        var fromTail = loopItems[j];
                        if (fromTail != null)
                        {
                            try
                            {
                                if (!fromTail.MoveNext())
                                {
                                    loopItems[j] = null;
                                    j--;
                                    continue; // next j
                                }
                                else
                                {
                                    // swap
                                    loopItems[i] = fromTail;
                                    loopItems[j] = null;
                                    j--;
                                    goto NEXT_LOOP; // next i
                                }
                            }
                            catch (Exception ex)
                            {
                                loopItems[j] = null;
                                j--;
                                try
                                {
                                    unhandledExceptionCallback(ex);
                                }
                                catch { }
                                continue; // next j
                            }
                        }
                        else
                        {
                            j--;
                        }
                    }

                    tail = i; // loop end
                    break; // LOOP END

                    NEXT_LOOP:
                    continue;
                }


                lock (runningAndQueueLock)
                {
                    running = false;
                    while (waitQueue.Count != 0)
                    {
                        if (loopItems.Length == tail)
                        {
                            Array.Resize(ref loopItems, checked(tail * 2));
                        }
                        loopItems[tail++] = waitQueue.Dequeue();
                    }
                }
            }
        }
    }
}

#endif