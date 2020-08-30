using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace NetCoreSandbox
{
    struct WrapedAction
    {
        Action action;

        public WrapedAction(Action action)
        {
            this.action = action;
        }

        public void Invoke() => action.Invoke();
    }

    [Config(typeof(BenchmarkConfig))]
    [CategoriesColumn]
    public class ContinuationQueueCheck
    {
        const int actionListLength = 16;
        Action[] actionList;
        int actionListCount = actionListLength;
        WrapedAction[] wrapedActionList;
        int wrapedActionListCount = actionListLength;
        [GlobalSetup]

        public void Setup()
        {
            actionList = new Action[actionListLength];
            for (int i = 0; i < actionList.Length; i++)
            {
                actionList[i] = () => { };
            }
            wrapedActionList = new WrapedAction[actionListLength];
            for (int i = 0; i < wrapedActionList.Length; i++)
            {
                wrapedActionList[i] = new WrapedAction(() => { });
            }
        }

        [Benchmark]
        public void WithoutRef()
        {
            for (int i = 0; i < actionListCount; i++)
            {
                var action = actionList[i];
                actionList[i] = default;

                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        public void WithoutRefNoBoundsCheck()
        {
            var actionList = this.actionList;
            for (int i = 0; i < actionList.Length; i++)
            {
                var action = actionList[i];
                actionList[i] = default;

                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        public void WithoutRefLocalListCount()
        {
            var actionList = this.actionList;
            var count = Math.Min(actionList.Length, actionListCount);
            for (int i = 0; i < count; i++)
            {
                var action = actionList[i];
                actionList[i] = default;

                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        public void WithRef()
        {
            for (int i = 0; i < actionListCount; i++)
            {
                ref var action = ref actionList[i];


                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                action = default;
            }
        }
        [Benchmark]
        public void WithRefNoBoundsCheck()
        {
            var actionList = this.actionList;
            for (int i = 0; i < actionList.Length; i++)
            {
                ref var action = ref actionList[i];


                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                action = default;
            }
        }
        [Benchmark]
        public void WithRefLocalListCount()
        {
            var actionList = this.actionList;
            var count = Math.Min(actionList.Length, actionListCount);
            for (int i = 0; i < count; i++)
            {
                ref var action = ref actionList[i];


                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                action = default;
            }
        }
        [Benchmark]
        public void WithoutRefWraped()
        {
            for (int i = 0; i < wrapedActionListCount; i++)
            {
                var wrapedAction = wrapedActionList[i];
                wrapedActionList[i] = default;

                try
                {
                    wrapedAction.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        public void WithoutRefNoBoundsCheckWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            for (int i = 0; i < wrapedActionList.Length; i++)
            {
                var wrapedAction = wrapedActionList[i];
                wrapedActionList[i] = default;

                try
                {
                    wrapedAction.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        public void WithoutRefLocalListCountWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            var count = Math.Min(wrapedActionList.Length, wrapedActionListCount);
            for (int i = 0; i < count; i++)
            {
                var wrapedAction = wrapedActionList[i];
                wrapedActionList[i] = default;

                try
                {
                    wrapedAction.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        public void WithRefWraped()
        {
            for (int i = 0; i < wrapedActionListCount; i++)
            {
                ref var wrapedAction = ref wrapedActionList[i];


                try
                {
                    wrapedAction.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                wrapedAction = default;
            }
        }
        [Benchmark]
        public void WithRefNoBoundsCheckWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            for (int i = 0; i < wrapedActionList.Length; i++)
            {
                ref var wrapedAction = ref wrapedActionList[i];


                try
                {
                    wrapedAction.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                wrapedAction = default;
            }
        }
        [Benchmark]
        public void WithRefLocalListCountWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            var count = Math.Min(wrapedActionList.Length, wrapedActionListCount);
            for (int i = 0; i < count; i++)
            {
                ref var wrapedAction = ref wrapedActionList[i];


                try
                {
                    wrapedAction.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                wrapedAction = default;
            }
        }
    }
}
