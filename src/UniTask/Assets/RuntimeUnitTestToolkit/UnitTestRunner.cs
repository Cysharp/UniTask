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
        Dictionary<string, List<TestKeyValuePair>> tests = new Dictionary<string, List<TestKeyValuePair>>();

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
        bool logClear = false;

        void Start()
        {
            try
            {
                UnityEngine.Application.logMessageReceived += (a, b, c) =>
                {
                    if (a.Contains("Mesh can not have more than 65000 vertices"))
                    {
                        logClear = true;
                    }
                    else
                    {
                        AppendToGraphicText("[" + c + "]" + a + "\n");
                        WriteToConsole("[" + c + "]" + a);
                    }
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
                        TestAttribute t1 = null;
                        try
                        {
                            t1 = method.GetCustomAttribute<TestAttribute>(true);
                        }
                        catch (Exception ex)
                        {
                            Debug.Log("TestAttribute Load Fail, Assembly:" + assembly.FullName);
                            Debug.LogException(ex);
                            goto NEXT_ASSEMBLY;
                        }
                        if (t1 != null)
                        {
                            yield return item;
                            break;
                        }

                        UnityTestAttribute t2 = null;
                        try
                        {
                            t2 = method.GetCustomAttribute<UnityTestAttribute>(true);
                        }
                        catch (Exception ex)
                        {
                            Debug.Log("UnityTestAttribute Load Fail, Assembly:" + assembly.FullName);
                            Debug.LogException(ex);
                            goto NEXT_ASSEMBLY;
                        }
                        if (t2 != null)
                        {
                            yield return item;
                            break;
                        }
                    }
                }

NEXT_ASSEMBLY:
                continue;
            }
        }

        public void AddTest(string group, string title, Action test, List<Action> setups, List<Action> teardowns)
        {
            List<TestKeyValuePair> list;
            if (!tests.TryGetValue(group, out list))
            {
                list = new List<TestKeyValuePair>();
                tests[group] = list;
            }

            list.Add(new TestKeyValuePair(title, test, setups, teardowns));
        }

        public void AddAsyncTest(string group, string title, Func<IEnumerator> asyncTestCoroutine, List<Action> setups, List<Action> teardowns)
        {
            List<TestKeyValuePair> list;
            if (!tests.TryGetValue(group, out list))
            {
                list = new List<TestKeyValuePair>();
                tests[group] = list;
            }

            list.Add(new TestKeyValuePair(title, asyncTestCoroutine, setups, teardowns));
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
                List<Action> setups = new List<Action>();
                List<Action> teardowns = new List<Action>();
                foreach (var item in methods)
                {
                    try
                    {
                        var setup = item.GetCustomAttribute<NUnit.Framework.SetUpAttribute>(true);
                        if (setup != null)
                        {
                            setups.Add((Action)Delegate.CreateDelegate(typeof(Action), test, item));
                        }
                        var teardown = item.GetCustomAttribute<NUnit.Framework.TearDownAttribute>(true);
                        if (teardown != null)
                        {
                            teardowns.Add((Action)Delegate.CreateDelegate(typeof(Action), test, item));
                        }
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError(testType.Name + "." + item.Name + " failed to register setup/teardown method, exception: " + e.ToString());
                    }
                }

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
                                AddAsyncTest(factory.Target.GetType().Name, factory.Method.Name, factory, setups, teardowns);
                            }
                            else
                            {
                                var testData = GetTestData(item);
                                if (testData.Count != 0)
                                {
                                    foreach (var item2 in testData)
                                    {
                                        Func<IEnumerator> factory;
                                        if (item.IsGenericMethod)
                                        {
                                            var method2 = InferGenericType(item, item2);
                                            factory = () => (IEnumerator)method2.Invoke(test, item2);
                                        }
                                        else
                                        {
                                            factory = () => (IEnumerator)item.Invoke(test, item2);
                                        }
                                        var name = item.Name + "(" + string.Join(", ", item2.Select(x => x?.ToString() ?? "null")) + ")";
                                        name = name.Replace(Char.MinValue, ' ').Replace(Char.MaxValue, ' ').Replace("<", "[").Replace(">", "]");
                                        AddAsyncTest(test.GetType().Name, name, factory, setups, teardowns);
                                    }
                                }
                                else
                                {
                                    UnityEngine.Debug.Log(testType.Name + "." + item.Name + " currently does not supported in RuntumeUnitTestToolkit(multiple parameter without TestCase or return type is invalid).");
                                }
                            }
                        }

                        var standardTest = item.GetCustomAttribute<NUnit.Framework.TestAttribute>(true);
                        if (standardTest != null)
                        {
                            if (item.GetParameters().Length == 0 && item.ReturnType == typeof(void))
                            {
                                var invoke = (Action)Delegate.CreateDelegate(typeof(Action), test, item);
                                AddTest(invoke.Target.GetType().Name, invoke.Method.Name, invoke, setups, teardowns);
                            }
                            else
                            {
                                var testData = GetTestData(item);
                                if (testData.Count != 0)
                                {
                                    foreach (var item2 in testData)
                                    {
                                        Action invoke = null;
                                        if (item.IsGenericMethod)
                                        {
                                            var method2 = InferGenericType(item, item2);
                                            invoke = () => method2.Invoke(test, item2);
                                        }
                                        else
                                        {
                                            invoke = () => item.Invoke(test, item2);
                                        }
                                        var name = item.Name + "(" + string.Join(", ", item2.Select(x => x?.ToString() ?? "null")) + ")";
                                        name = name.Replace(Char.MinValue, ' ').Replace(Char.MaxValue, ' ').Replace("<", "[").Replace(">", "]");
                                        AddTest(test.GetType().Name, name, invoke, setups, teardowns);
                                    }
                                }
                                else
                                {
                                    UnityEngine.Debug.Log(testType.Name + "." + item.Name + " currently does not supported in RuntumeUnitTestToolkit(multiple parameter without TestCase or return type is invalid).");
                                }
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

        List<object[]> GetTestData(MethodInfo methodInfo)
        {
            List<object[]> testCases = new List<object[]>();

            var inlineData = methodInfo.GetCustomAttributes<NUnit.Framework.TestCaseAttribute>(true);
            foreach (var item in inlineData)
            {
                testCases.Add(item.Arguments);
            }

            var sourceData = methodInfo.GetCustomAttributes<NUnit.Framework.TestCaseSourceAttribute>(true);
            foreach (var item in sourceData)
            {
                var enumerator = GetTestCaseSource(methodInfo, item.SourceType, item.SourceName, item.MethodParams);
                foreach (var item2 in enumerator)
                {
                    var item3 = item2 as IEnumerable; // object[][]
                    if (item3 != null)
                    {
                        var l = new List<object>();
                        foreach (var item4 in item3)
                        {
                            l.Add(item4);
                        }
                        testCases.Add(l.ToArray());
                    }
                }
            }

            return testCases;
        }

        IEnumerable GetTestCaseSource(MethodInfo method, Type sourceType, string sourceName, object[] methodParams)
        {
            Type type = sourceType ?? method.DeclaringType;

            MemberInfo[] member = type.GetMember(sourceName, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            if (member.Length == 1)
            {
                MemberInfo memberInfo = member[0];
                FieldInfo fieldInfo = memberInfo as FieldInfo;
                if ((object)fieldInfo != null)
                {
                    return (!fieldInfo.IsStatic) ? ReturnErrorAsParameter("The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.") : ((methodParams == null) ? ((IEnumerable)fieldInfo.GetValue(null)) : ReturnErrorAsParameter("You have specified a data source field but also given a set of parameters. Fields cannot take parameters, please revise the 3rd parameter passed to the TestCaseSourceAttribute and either remove it or specify a method."));
                }
                PropertyInfo propertyInfo = memberInfo as PropertyInfo;
                if ((object)propertyInfo != null)
                {
                    return (!propertyInfo.GetGetMethod(nonPublic: true).IsStatic) ? ReturnErrorAsParameter("The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.") : ((methodParams == null) ? ((IEnumerable)propertyInfo.GetValue(null, null)) : ReturnErrorAsParameter("You have specified a data source property but also given a set of parameters. Properties cannot take parameters, please revise the 3rd parameter passed to the TestCaseSource attribute and either remove it or specify a method."));
                }
                MethodInfo methodInfo = memberInfo as MethodInfo;
                if ((object)methodInfo != null)
                {
                    return (!methodInfo.IsStatic) ? ReturnErrorAsParameter("The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.") : ((methodParams == null || methodInfo.GetParameters().Length == methodParams.Length) ? ((IEnumerable)methodInfo.Invoke(null, methodParams)) : ReturnErrorAsParameter("You have given the wrong number of arguments to the method in the TestCaseSourceAttribute, please check the number of parameters passed in the object is correct in the 3rd parameter for the TestCaseSourceAttribute and this matches the number of parameters in the target method and try again."));
                }
            }
            return null;
        }

        MethodInfo InferGenericType(MethodInfo methodInfo, object[] parameters)
        {
            var set = new HashSet<Type>();
            List<Type> genericParameters = new List<Type>();
            foreach (var item in methodInfo.GetParameters()
                .Select((x, i) => new { x.ParameterType, i })
                .Where(x => x.ParameterType.IsGenericParameter)
                .OrderBy(x => x.ParameterType.GenericParameterPosition))
            {
                if (set.Add(item.ParameterType)) // DistinctBy
                {
                    genericParameters.Add(parameters[item.i].GetType());
                }
            }

            return methodInfo.MakeGenericMethod(genericParameters.ToArray());
        }

        IEnumerable ReturnErrorAsParameter(string name)
        {
            throw new Exception(name);
        }

        System.Collections.IEnumerator ScrollLogToEndNextFrame()
        {
            yield return null;
            yield return null;
            logScrollBar.value = 0;
        }

        IEnumerator RunTestInCoroutine(KeyValuePair<string, List<TestKeyValuePair>> actionList)
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

            AppendToGraphicText("<color=yellow>" + actionList.Key + "</color>\n");
            WriteToConsole("Begin Test Class: " + actionList.Key);
            yield return null;

            var totalExecutionTime = new List<double>();
            foreach (var item2 in actionList.Value)
            {
                // setup
                try
                {
                    foreach (var setup in item2.Setups)
                    {
                        setup();
                    }

                    // before start, cleanup
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    AppendToGraphicText("<color=teal>" + item2.Key + "</color>\n");
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
                        AppendToGraphicText("OK, " + methodStopwatch.Elapsed.TotalMilliseconds.ToString("0.00") + "ms\n");
                        WriteToConsoleResult(item2.Key + ", " + methodStopwatch.Elapsed.TotalMilliseconds.ToString("0.00") + "ms", true);
                    }
                    else
                    {
                        AppendToGraphicText("<color=red>" + exception.ToString() + "</color>\n");
                        WriteToConsoleResult(item2.Key + ", " + exception.ToString(), false);
                        allGreen = false;
                        allTestGreen = false;
                    }
                }
                finally
                {
                    foreach (var teardown in item2.Teardowns)
                    {
                        teardown();
                    }
                }
            }

            AppendToGraphicText("[" + actionList.Key + "]" + totalExecutionTime.Sum().ToString("0.00") + "ms\n\n");
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

        void AppendToGraphicText(string msg)
        {
            if (!Application.isBatchMode)
            {
                if (logClear)
                {
                    logText.text = "";
                    logClear = false;
                }

                try
                {
                    logText.text += msg;
                }
                catch
                {
                    logClear = true;
                }
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

    public class TestKeyValuePair
    {
        public string Key;
        /// <summary>IEnumerator or Func[IEnumerator]</summary>
        public object Value;
        public List<Action> Setups;
        public List<Action> Teardowns;

        public TestKeyValuePair(string key, object value, List<Action> setups, List<Action> teardowns)
        {
            this.Key = key;
            this.Value = value;
            this.Setups = setups;
            this.Teardowns = teardowns;
        }
    }
}
