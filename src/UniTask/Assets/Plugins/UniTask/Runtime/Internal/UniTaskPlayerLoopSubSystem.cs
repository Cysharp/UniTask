using System;
using System.Threading;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class UniTaskPlayerLoopSubSystem : IPlayerLoopItem
    {
        
        public UniTaskPlayerLoopSubSystem()
        {
            this.unhandledExceptionCallback = ex => Debug.LogException(ex);
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        const int MaxArrayLength = 0X7FEFFFFF;
        const int InitialSize = 16;

        void Run() 
        {
            RunContinuations();
            RunLoopItems();
        }
        void Clear()
        {
            ClearContinuations();
            ClearLoopItems();
        }

        public bool MoveNext()
        {
            Run();
            return true;
        }
#if UNITY_EDITOR
        void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode || state == PlayModeStateChange.EnteredPlayMode)
            {
                return;
            }
            Clear();
        }

#endif

        #region ContinuationQueue
        SpinLock gate = new SpinLock();
        bool dequing = false;

        int actionListCount = 0;
        Action[] actionList = new Action[InitialSize];

        int waitingListCount = 0;
        Action[] waitingList = new Action[InitialSize];


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


        void RunContinuations()
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
        void ClearContinuations()
        {
            actionListCount = 0;
            actionList = new Action[InitialSize];

            waitingListCount = 0;
            waitingList = new Action[InitialSize];
        }
        #endregion

        #region PlayerLoopRunner
        readonly object runningAndQueueLock = new object();
        readonly object arrayLock = new object();
        readonly Action<Exception> unhandledExceptionCallback;

        int tail = 0;
        bool running = false;
        IPlayerLoopItem[] loopItems = new IPlayerLoopItem[InitialSize];
        MinimumQueue<IPlayerLoopItem> waitQueue = new MinimumQueue<IPlayerLoopItem>(InitialSize);
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
        void RunLoopItems()
        {

            lock (runningAndQueueLock)
            {
                running = true;
            }

            lock (arrayLock)
            {
                var j = tail - 1;
                var loopItems = this.loopItems;
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




        void ClearLoopItems()
        {
            lock (arrayLock)
            {
                for (var index = 0; index < loopItems.Length; index++)
                {
                    loopItems[index] = null;
                }
            }
        }
        #endregion

    }

    

}
