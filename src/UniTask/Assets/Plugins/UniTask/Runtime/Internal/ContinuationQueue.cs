#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class ContinuationQueue
    {
        const int MaxArrayLength = 0X7FEFFFFF;
        const int InitialSize = 16;

        readonly PlayerLoopTiming timing;

        SpinLock gate = new SpinLock();
        bool dequing = false;

        int actionListCount = 0;
        Action[] actionList = new Action[InitialSize];

        int waitingListCount = 0;
        Action[] waitingList = new Action[InitialSize];

        public ContinuationQueue(PlayerLoopTiming timing)
        {
            this.timing = timing;
        }

        public void Enqueue(Action continuation)
        {
            bool lockTaken = false;
            try
            {
                gate.Enter(ref lockTaken);

                if (dequing)
                {
                    // Ensure Capacity
                    if (waitingList.Length == waitingListCount)
                    {
                        var newLength = waitingListCount * 2;
                        if ((uint)newLength > MaxArrayLength) newLength = MaxArrayLength;

                        var newArray = new Action[newLength];
                        Array.Copy(waitingList, newArray, waitingListCount);
                        waitingList = newArray;
                    }
                    waitingList[waitingListCount] = continuation;
                    waitingListCount++;
                }
                else
                {
                    // Ensure Capacity
                    if (actionList.Length == actionListCount)
                    {
                        var newLength = actionListCount * 2;
                        if ((uint)newLength > MaxArrayLength) newLength = MaxArrayLength;

                        var newArray = new Action[newLength];
                        Array.Copy(actionList, newArray, actionListCount);
                        actionList = newArray;
                    }
                    actionList[actionListCount] = continuation;
                    actionListCount++;
                }
            }
            finally
            {
                if (lockTaken) gate.Exit(false);
            }
        }

        public void Clear()
        {
            actionListCount = 0;
            actionList = new Action[InitialSize];

            waitingListCount = 0;
            waitingList = new Action[InitialSize];
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
            {
                bool lockTaken = false;
                try
                {
                    gate.Enter(ref lockTaken);
                    if (actionListCount == 0) return;
                    dequing = true;
                }
                finally
                {
                    if (lockTaken) gate.Exit(false);
                }
            }

            for (int i = 0; i < actionListCount; i++)
            {

                var action = actionList[i];
                actionList[i] = null;
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogException(ex);
                }
            }

            {
                bool lockTaken = false;
                try
                {
                    gate.Enter(ref lockTaken);
                    dequing = false;

                    var swapTempActionList = actionList;

                    actionListCount = waitingListCount;
                    actionList = waitingList;

                    waitingListCount = 0;
                    waitingList = swapTempActionList;
                }
                finally
                {
                    if (lockTaken) gate.Exit(false);
                }
            }
        }
    }
}

