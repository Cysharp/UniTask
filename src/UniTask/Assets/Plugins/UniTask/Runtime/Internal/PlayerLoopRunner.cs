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

        public void Clear()
        {
            lock (arrayLock)
            {
                for (var index = 0; index < loopItems.Length; index++)
                {
                    loopItems[index] = null;
                }
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

        
        [System.Diagnostics.DebuggerHidden]
        void RunCore()
        {
            lock (runningAndQueueLock)
            {
                running = true;
            }

            lock (arrayLock)
            {
                var pivot = ArrayUtil.Partition(loopItems, playerLoopItem =>
                {
                    try
                    {
                        return playerLoopItem != null && playerLoopItem.MoveNext();
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            unhandledExceptionCallback(e);
                            return false;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                });
                
                for (var i = pivot; i < loopItems.Length; ++i)
                    loopItems[i] = null;
                
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

