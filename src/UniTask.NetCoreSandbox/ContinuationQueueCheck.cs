using BenchmarkDotNet.Attributes;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NetCoreSandbox
{
    [StructLayout(LayoutKind.Auto)]
    struct WrapedAction
    {
        public Action action;

        public WrapedAction(Action action)
        {
            this.action = action;
        }
    }

    //[Config(typeof(BenchmarkConfig))]
    [CategoriesColumn]
    public class ContinuationQueueCheck
    {
        const int actionListLength = 1<<24;
        Action[] actionList;
        int actionListCount;
        WrapedAction[] wrapedActionList;
        int wrapedActionListCount;
        [GlobalSetup]
        public void GlobalSetup()
        {
            actionList = new Action[actionListLength];
            actionListCount = actionListLength;
            wrapedActionList = new WrapedAction[actionListLength];
            wrapedActionListCount = actionListLength;
        }

        [IterationSetup]

        public void IterationSetup()
        {
            var actionList = this.actionList;
            for (int i = 0; i < actionList.Length; i++)
            {
                actionList[i] = () => { };
            }
            var wrapedActionList = this.wrapedActionList;
            for (int i = 0; i < wrapedActionList.Length; i++)
            {
                wrapedActionList[i] = new WrapedAction(() => { });
            }
        }

        [Benchmark]
        [BenchmarkCategory("WithoutRef","RawAction")]
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
        [BenchmarkCategory("WithoutRef", "RawAction")]
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
        [BenchmarkCategory("WithoutRef", "RawAction")]
        public void WithoutRefLocalListCount()
        {
            var actionList = this.actionList;
            var count = actionListCount;
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
        [BenchmarkCategory("WithRef", "RawAction")]
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
        [BenchmarkCategory("WithRef", "RawAction")]
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
        [BenchmarkCategory("WithRef", "RawAction")]
        public void WithRefLocalListCount()
        {
            var actionList = this.actionList;
            var count = actionListCount;
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
        [BenchmarkCategory("WithoutRef", "StructWrapedAction")]
        public void WithoutRefWraped()
        {
            for (int i = 0; i < wrapedActionListCount; i++)
            {
                var wrapedAction = wrapedActionList[i];
                wrapedActionList[i] = default;

                try
                {
                    wrapedAction.action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        [BenchmarkCategory("WithoutRef", "StructWrapedAction")]
        public void WithoutRefNoBoundsCheckWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            for (int i = 0; i < wrapedActionList.Length; i++)
            {
                var wrapedAction = wrapedActionList[i];
                wrapedActionList[i] = default;

                try
                {
                    wrapedAction.action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        [BenchmarkCategory("WithoutRef", "StructWrapedAction")]
        public void WithoutRefLocalListCountWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            var count = wrapedActionListCount;
            for (int i = 0; i < count; i++)
            {
                var wrapedAction = wrapedActionList[i];
                wrapedActionList[i] = default;

                try
                {
                    wrapedAction.action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        [Benchmark]
        [BenchmarkCategory("WithRef", "StructWrapedAction")]
        public void WithRefWraped()
        {
            for (int i = 0; i < wrapedActionListCount; i++)
            {
                ref var wrapedAction = ref wrapedActionList[i].action;
                

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
        [BenchmarkCategory("WithRef", "StructWrapedAction")]
        public void WithRefNoBoundsCheckWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            for (int i = 0; i < wrapedActionList.Length; i++)
            {
                ref var wrapedAction = ref wrapedActionList[i].action;


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
        [BenchmarkCategory("WithRef", "StructWrapedAction")]
        public void WithRefLocalListCountWraped()
        {
            var wrapedActionList = this.wrapedActionList;
            var count = wrapedActionListCount;
            for (int i = 0; i < count; i++)
            {
                ref var wrapedAction = ref wrapedActionList[i].action;


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
