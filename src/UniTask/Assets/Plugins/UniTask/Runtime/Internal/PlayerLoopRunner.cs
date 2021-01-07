
using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class PlayerLoopRunner
    {
        const int InitialSize = 16;

        readonly PlayerLoopTiming timing;
        readonly object runningAndQueueLock = new object();
        readonly object arrayLock = new object();
        readonly Action<Exception> unhandledExceptionCallback;

        int tail = 0;
        bool running = false;
        IPlayerLoopItem[] loopItems = new IPlayerLoopItem[InitialSize];
        MinimumQueue<IPlayerLoopItem> waitQueue = new MinimumQueue<IPlayerLoopItem>(InitialSize);



        public PlayerLoopRunner(PlayerLoopTiming timing)
        {
            this.unhandledExceptionCallback = ex => Debug.LogException(ex);
            this.timing = timing;
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

        public int Clear()
        {
            lock (arrayLock)
            {
                var rest = 0;

                for (var index = 0; index < loopItems.Length; index++)
                {
                    if (loopItems[index] != null)
                    {
                        rest++;
                    }

                    loopItems[index] = null;
                }

                tail = 0;
                return rest;
            }
        }

        // delegate entrypoint.
        public void Run()
        {
            // for debugging, create named stacktrace.
#if DEBUG
            switch (timing)
            {
                case PlayerLoopTiming.Initialization:
                    Initialization();
                    break;
                case PlayerLoopTiming.LastInitialization:
                    LastInitialization();
                    break;
                case PlayerLoopTiming.EarlyUpdate:
                    EarlyUpdate();
                    break;
                case PlayerLoopTiming.LastEarlyUpdate:
                    LastEarlyUpdate();
                    break;
                case PlayerLoopTiming.FixedUpdate:
                    FixedUpdate();
                    break;
                case PlayerLoopTiming.LastFixedUpdate:
                    LastFixedUpdate();
                    break;
                case PlayerLoopTiming.PreUpdate:
                    PreUpdate();
                    break;
                case PlayerLoopTiming.LastPreUpdate:
                    LastPreUpdate();
                    break;
                case PlayerLoopTiming.Update:
                    Update();
                    break;
                case PlayerLoopTiming.LastUpdate:
                    LastUpdate();
                    break;
                case PlayerLoopTiming.PreLateUpdate:
                    PreLateUpdate();
                    break;
                case PlayerLoopTiming.LastPreLateUpdate:
                    LastPreLateUpdate();
                    break;
                case PlayerLoopTiming.PostLateUpdate:
                    PostLateUpdate();
                    break;
                case PlayerLoopTiming.LastPostLateUpdate:
                    LastPostLateUpdate();
                    break;
#if UNITY_2020_2_OR_NEWER
                case PlayerLoopTiming.TimeUpdate:
                    TimeUpdate();
                    break;
                case PlayerLoopTiming.LastTimeUpdate:
                    LastTimeUpdate();
                    break;
#endif
                default:
                    break;
            }
#else
            RunCore();
#endif
        }

        void Initialization() => RunCore();
        void LastInitialization() => RunCore();
        void EarlyUpdate() => RunCore();
        void LastEarlyUpdate() => RunCore();
        void FixedUpdate() => RunCore();
        void LastFixedUpdate() => RunCore();
        void PreUpdate() => RunCore();
        void LastPreUpdate() => RunCore();
        void Update() => RunCore();
        void LastUpdate() => RunCore();
        void PreLateUpdate() => RunCore();
        void LastPreLateUpdate() => RunCore();
        void PostLateUpdate() => RunCore();
        void LastPostLateUpdate() => RunCore();
#if UNITY_2020_2_OR_NEWER
        void TimeUpdate() => RunCore();
        void LastTimeUpdate() => RunCore();
#endif

        [System.Diagnostics.DebuggerHidden]
        void RunCore()
        {
            lock (runningAndQueueLock)
            {
                running = true;
            }

            lock (arrayLock)
            {
                var j = tail - 1;

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

