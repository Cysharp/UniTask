using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public class UniTaskSynchronizationContext : SynchronizationContext
    {
        const int MaxArrayLength = 0X7FEFFFFF;
        const int InitialSize = 16;

        static SpinLock gate = new SpinLock();
        static bool dequing = false;

        static int actionListCount = 0;
        static Callback[] actionList = new Callback[InitialSize];

        static int waitingListCount = 0;
        static Callback[] waitingList = new Callback[InitialSize];

        static int opCount;

        public override void Send(SendOrPostCallback d, object state)
        {
            d(state);
        }

        public override void Post(SendOrPostCallback d, object state)
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

                        var newArray = new Callback[newLength];
                        Array.Copy(waitingList, newArray, waitingListCount);
                        waitingList = newArray;
                    }
                    waitingList[waitingListCount] = new Callback(d, state);
                    waitingListCount++;
                }
                else
                {
                    // Ensure Capacity
                    if (actionList.Length == actionListCount)
                    {
                        var newLength = actionListCount * 2;
                        if ((uint)newLength > MaxArrayLength) newLength = MaxArrayLength;

                        var newArray = new Callback[newLength];
                        Array.Copy(actionList, newArray, actionListCount);
                        actionList = newArray;
                    }
                    actionList[actionListCount] = new Callback(d, state);
                    actionListCount++;
                }
            }
            finally
            {
                if (lockTaken) gate.Exit(false);
            }
        }

        public override void OperationStarted()
        {
            Interlocked.Increment(ref opCount);
        }

        public override void OperationCompleted()
        {
            Interlocked.Decrement(ref opCount);
        }

        public override SynchronizationContext CreateCopy()
        {
            return this;
        }

        // delegate entrypoint.
        internal static void Run()
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
                actionList[i] = default;
                action.Invoke();
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

        [StructLayout(LayoutKind.Auto)]
        readonly struct Callback
        {
            readonly SendOrPostCallback callback;
            readonly object state;

            public Callback(SendOrPostCallback callback, object state)
            {
                this.callback = callback;
                this.state = state;
            }

            public void Invoke()
            {
                try
                {
                    callback(state);
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogException(ex);
                }
            }
        }
    }
}