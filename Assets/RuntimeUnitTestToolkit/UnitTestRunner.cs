using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace RuntimeUnitTestToolkit
{
    public class UnitTestRunner : MonoBehaviour
    {
        // object is IEnumerator or Func<IEnumerator>
        Dictionary<string, List<KeyValuePair<string, object>>> tests = new Dictionary<string, List<KeyValuePair<string, object>>>();

        List<Pair> additionalActionsOnFirst = new List<Pair>();

        public Button clearButton;
        public RectTransform list;
        public Scrollbar listScrollBar;

        public Text logText;
        public Scrollbar logScrollBar;

        readonly Color passColor = new Color(0f, 1f, 0f, 1f); // green
        readonly Color failColor = new Color(1f, 0f, 0f, 1f); // red
        readonly Color normalColor = new Color(1f, 1f, 1f, 1f); // white

        bool allTestGreen = true;

        void Start()
        {
            try
            {
                UnityEngine.Application.logMessageReceived += (a, b, c) =>
                {
                    logText.text += "[" + c + "]" + a + "\n";
                };

                // register all test types
                foreach (var item in GetTestTargetTypes())
                {
                    RegisterAllMethods(item);
                }

                var executeAll = new List<Func<Coroutine>>();
                foreach (var ___item in tests)
                {
                    var actionList = ___item; // be careful, capture in lambda

                    executeAll.Add(() => StartCoroutine(RunTestInCoroutine(actionList)));
                    Add(actionList.Key, () => StartCoroutine(RunTestInCoroutine(actionList)));
                }

                var executeAllButton = Add("Run All Tests", () => StartCoroutine(ExecuteAllInCoroutine(executeAll)));

                clearButton.gameObject.GetComponent<Image>().color = new Color(170 / 255f, 170 / 255f, 170 / 255f, 1);
                executeAllButton.gameObject.GetComponent<Image>().color = new Color(250 / 255f, 150 / 255f, 150 / 255f, 1);
                executeAllButton.transform.SetSiblingIndex(1);

                additionalActionsOnFirst.Reverse();
                foreach (var item in additionalActionsOnFirst)
                {
                    var newButton = GameObject.Instantiate(clearButton);
                    newButton.name = item.Name;
                    newButton.onClick.RemoveAllListeners();
                    newButton.GetComponentInChildren<Text>().text = item.Name;
                    newButton.onClick.AddListener(item.Action);
                    newButton.transform.SetParent(list);
                    newButton.transform.SetSiblingIndex(1);
                }

                clearButton.onClick.AddListener(() =>
                {
                    logText.text = "";
                    foreach (var btn in list.GetComponentsInChildren<Button>())
                    {
                        btn.interactable = true;
                        btn.GetComponent<Image>().color = normalColor;
                    }
                    executeAllButton.gameObject.GetComponent<Image>().color = new Color(250 / 255f, 150 / 255f, 150 / 255f, 1);
                });

                listScrollBar.value = 1;
                logScrollBar.value = 1;

                if (Application.isBatchMode)
                {
                    // run immediately in player
                    StartCoroutine(ExecuteAllInCoroutine(executeAll));
                }
            }
            catch (Exception ex)
            {
                if (Application.isBatchMode)
                {
                    // when failed(can not start runner), quit immediately.
                    WriteToConsole(ex.ToString());
                    Application.Quit(1);
                }
                else
                {
                    throw;
                }
            }
        }

        Button Add(string title, UnityAction test)
        {
            var newButton = GameObject.Instantiate(clearButton);
            newButton.name = title;
            newButton.onClick.RemoveAllListeners();
            newButton.GetComponentInChildren<Text>().text = title;
            newButton.onClick.AddListener(test);

            newButton.transform.SetParent(list);
            return newButton;
        }

        static IEnumerable<Type> GetTestTargetTypes()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var n = assembly.FullName;
                if (n.StartsWith("UnityEngine")) continue;
                if (n.StartsWith("mscorlib")) continue;
                if (n.StartsWith("System")) continue;

                foreach (var item in assembly.GetTypes())
                {
                    foreach (var method in item.GetMethods())
                    {
                        var t1 = method.GetCustomAttribute<TestAttribute>(true);
                        if (t1 != null)
                        {
                            yield return item;
                            break;
                        }
                        var t2 = method.GetCustomAttribute<UnityTestAttribute>(true);
                        if (t2 != null)
                        {
                            yield return item;
                            break;
                        }
                    }
                }
            }
        }

        public void AddTest(string group, string title, Action test)
        {
            List<KeyValuePair<string, object>> list;
            if (!tests.TryGetValue(group, out list))
            {
                list = new List<KeyValuePair<string, object>>();
                tests[group] = list;
            }

            list.Add(new KeyValuePair<string, object>(title, test));
        }

        public void AddAsyncTest(string group, string title, Func<IEnumerator> asyncTestCoroutine)
        {
            List<KeyValuePair<string, object>> list;
            if (!tests.TryGetValue(group, out list))
            {
                list = new List<KeyValuePair<string, object>>();
                tests[group] = list;
            }

            list.Add(new KeyValuePair<string, object>(title, asyncTestCoroutine));
        }

        public void AddCutomAction(string name, UnityAction action)
        {
            additionalActionsOnFirst.Add(new Pair { Name = name, Action = action });
        }


        public void RegisterAllMethods<T>()
            where T : new()
        {
            RegisterAllMethods(typeof(T));
        }

        public void RegisterAllMethods(Type testType)
        {
            try
            {
                var test = Activator.CreateInstance(testType);

                var methods = testType.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                foreach (var item in methods)
                {
                    try
                    {
                        var iteratorTest = item.GetCustomAttribute<UnityEngine.TestTools.UnityTestAttribute>(true);
                        if (iteratorTest != null)
                        {
                            if (item.GetParameters().Length == 0 && item.ReturnType == typeof(IEnumerator))
                            {
                                var factory = (Func<IEnumerator>)Delegate.CreateDelegate(typeof(Func<IEnumerator>), test, item);
                                AddAsyncTest(factory.Target.GetType().Name, factory.Method.Name, factory);
                            }
                            else
                            {
                                UnityEngine.Debug.Log(testType.Name + "." + item.Name + " currently does not supported in RuntumeUnitTestToolkit(multiple parameter or return type is invalid).");
                            }
                        }

                        var standardTest = item.GetCustomAttribute<NUnit.Framework.TestAttribute>(true);
                        if (standardTest != null)
                        {
                            if (item.GetParameters().Length == 0 && item.ReturnType == typeof(void))
                            {
                                var invoke = (Action)Delegate.CreateDelegate(typeof(Action), test, item);
                                AddTest(invoke.Target.GetType().Name, invoke.Method.Name, invoke);
                            }
                            else
                            {
                                UnityEngine.Debug.Log(testType.Name + "." + item.Name + " currently does not supported in RuntumeUnitTestToolkit(multiple parameter or return type is invalid).");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError(testType.Name + "." + item.Name + " failed to register method, exception: " + e.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        System.Collections.IEnumerator ScrollLogToEndNextFrame()
        {
            yield return null;
            yield return null;
            logScrollBar.value = 0;
        }

        IEnumerator RunTestInCoroutine(KeyValuePair<string, List<KeyValuePair<string, object>>> actionList)
        {
            Button self = null;
            foreach (var btn in list.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
                if (btn.name == actionList.Key) self = btn;
            }
            if (self != null)
            {
                self.GetComponent<Image>().color = normalColor;
            }

            var allGreen = true;

            logText.text += "<color=yellow>" + actionList.Key + "</color>\n";
            WriteToConsole("Begin Test Class: " + actionList.Key);
            yield return null;

            var totalExecutionTime = new List<double>();
            foreach (var item2 in actionList.Value)
            {
                // before start, cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                logText.text += "<color=teal>" + item2.Key + "</color>\n";
                yield return null;

                var v = item2.Value;

                var methodStopwatch = System.Diagnostics.Stopwatch.StartNew();
                Exception exception = null;
                if (v is Action)
                {
                    try
                    {
                        ((Action)v).Invoke();
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }
                else
                {
                    var coroutineFactory = (Func<IEnumerator>)v;
                    IEnumerator coroutine = null;
                    try
                    {
                        coroutine = coroutineFactory();
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                    if (exception == null)
                    {
                        yield return StartCoroutine(UnwrapEnumerator(coroutine, ex =>
                        {
                            exception = ex;
                        }));
                    }
                }

                methodStopwatch.Stop();
                totalExecutionTime.Add(methodStopwatch.Elapsed.TotalMilliseconds);
                if (exception == null)
                {
                    logText.text += "OK, " + methodStopwatch.Elapsed.TotalMilliseconds.ToString("0.00") + "ms\n";
                    WriteToConsoleResult(item2.Key + ", " + methodStopwatch.Elapsed.TotalMilliseconds.ToString("0.00") + "ms", true);
                }
                else
                {
                    // found match line...
                    var line = string.Join("\n", exception.StackTrace.Split('\n').Where(x => x.Contains(actionList.Key) || x.Contains(item2.Key)).ToArray());
                    logText.text += "<color=red>" + exception.Message + "\n" + line + "</color>\n";
                    WriteToConsoleResult(item2.Key + ", " + exception.Message, false);
                    WriteToConsole(line);
                    allGreen = false;
                    allTestGreen = false;
                }
            }

            logText.text += "[" + actionList.Key + "]" + totalExecutionTime.Sum().ToString("0.00") + "ms\n\n";
            foreach (var btn in list.GetComponentsInChildren<Button>()) btn.interactable = true;
            if (self != null)
            {
                self.GetComponent<Image>().color = allGreen ? passColor : failColor;
            }

            yield return StartCoroutine(ScrollLogToEndNextFrame());


        }

        IEnumerator ExecuteAllInCoroutine(List<Func<Coroutine>> tests)
        {
            allTestGreen = true;

            foreach (var item in tests)
            {
                yield return item();
            }

            if (Application.isBatchMode)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                bool disableAutoClose = (scene.name.Contains("DisableAutoClose"));

                if (allTestGreen)
                {
                    WriteToConsole("Test Complete Successfully");
                    if (!disableAutoClose)
                    {
                        Application.Quit();
                    }
                }
                else
                {
                    WriteToConsole("Test Failed, please see [NG] log.");
                    if (!disableAutoClose)
                    {
                        Application.Quit(1);
                    }
                }
            }
        }

        IEnumerator UnwrapEnumerator(IEnumerator enumerator, Action<Exception> exceptionCallback)
        {
            var hasNext = true;
            while (hasNext)
            {
                try
                {
                    hasNext = enumerator.MoveNext();
                }
                catch (Exception ex)
                {
                    exceptionCallback(ex);
                    hasNext = false;
                }

                if (hasNext)
                {
                    // unwrap self for bug of Unity
                    // https://issuetracker.unity3d.com/issues/does-not-stop-coroutine-when-it-throws-exception-in-movenext-at-first-frame
                    var moreCoroutine = enumerator.Current as IEnumerator;
                    if (moreCoroutine != null)
                    {
                        yield return StartCoroutine(UnwrapEnumerator(moreCoroutine, ex =>
                        {
                            exceptionCallback(ex);
                            hasNext = false;
                        }));
                    }
                    else
                    {
                        yield return enumerator.Current;
                    }
                }
            }
        }

        static void WriteToConsole(string msg)
        {
            if (Application.isBatchMode)
            {
                Console.WriteLine(msg);
            }
        }

        static void WriteToConsoleResult(string msg, bool green)
        {
            if (Application.isBatchMode)
            {
                if (!green)
                {
                    var currentForeground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[NG]");
                    Console.ForegroundColor = currentForeground;
                }
                else
                {
                    var currentForeground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[OK]");
                    Console.ForegroundColor = currentForeground;
                }

                System.Console.WriteLine(msg);
            }
        }

        struct Pair
        {
            public string Name;
            public UnityAction Action;
        }
    }
}
