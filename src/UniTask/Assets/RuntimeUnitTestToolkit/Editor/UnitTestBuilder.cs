#if UNITY_EDITOR

using RuntimeUnitTestToolkit;
using RuntimeUnitTestToolkit.Editor;
using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class RuntimeUnitTestSettings
{
    public ScriptingImplementation ScriptBackend;
    public bool UseCurrentScriptBackend;
    public BuildTarget BuildTarget;
    public bool UseCurrentBuildTarget;

    public bool Headless;
    public bool AutoRunPlayer;
    public bool DisableAutoClose;

    public RuntimeUnitTestSettings()
    {
        UseCurrentBuildTarget = true;
        UseCurrentScriptBackend = true;
        Headless = false;
        AutoRunPlayer = true;
        DisableAutoClose = false;
    }

    public override string ToString()
    {
        return $"{ScriptBackend} {BuildTarget} Headless:{Headless} AutoRunPlayer:{AutoRunPlayer} DisableAutoClose:{DisableAutoClose}";
    }
}

// no namespace(because invoke from commandline)
public static partial class UnitTestBuilder
{
    const string SettingsKeyBase = "RuntimeUnitTest.Settings.";

    [MenuItem("Test/BuildUnitTest")]
    public static void BuildUnitTest()
    {
        var settings = new RuntimeUnitTestSettings(); // default

        string buildPath = null;

        if (Application.isBatchMode) // from commandline
        {
            settings.AutoRunPlayer = false;
            settings.DisableAutoClose = false;

            var cmdArgs = Environment.GetCommandLineArgs();
            for (int i = 0; i < cmdArgs.Length; i++)
            {
                if (string.Equals(cmdArgs[i].Trim('-', '/'), "ScriptBackend", StringComparison.OrdinalIgnoreCase))
                {
                    settings.UseCurrentScriptBackend = false;
                    var str = cmdArgs[++i];
                    if (str.StartsWith("mono", StringComparison.OrdinalIgnoreCase))
                    {
                        settings.ScriptBackend = ScriptingImplementation.Mono2x;
                    }
                    else if (str.StartsWith("IL2CPP", StringComparison.OrdinalIgnoreCase))
                    {
                        settings.ScriptBackend = ScriptingImplementation.IL2CPP;
                    }
                    else
                    {
                        settings.ScriptBackend = (ScriptingImplementation)Enum.Parse(typeof(ScriptingImplementation), str, true);
                    }
                }
                else if (string.Equals(cmdArgs[i].Trim('-', '/'), "BuildTarget", StringComparison.OrdinalIgnoreCase))
                {
                    settings.UseCurrentBuildTarget = false;
                    settings.BuildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), cmdArgs[++i], true);
                }
                else if (string.Equals(cmdArgs[i].Trim('-', '/'), "Headless", StringComparison.OrdinalIgnoreCase))
                {
                    settings.Headless = true;
                }
                else if (string.Equals(cmdArgs[i].Trim('-', '/'), "buildPath", StringComparison.OrdinalIgnoreCase))
                {
                    buildPath = cmdArgs[++i];
                }
            }
        }
        else
        {
            var key = SettingsKeyBase + Application.productName;
            var settingsValue = EditorPrefs.GetString(key, null);
            try
            {
                if (!string.IsNullOrWhiteSpace(settingsValue))
                {
                    settings = JsonUtility.FromJson<RuntimeUnitTestSettings>(settingsValue);
                }
            }
            catch
            {
                UnityEngine.Debug.LogError("Fail to load RuntimeUnitTest settings");
                EditorPrefs.SetString(key, null);
            }
        }

        if (settings.UseCurrentBuildTarget)
        {
            settings.BuildTarget = EditorUserBuildSettings.activeBuildTarget;
        }
        if (settings.UseCurrentScriptBackend)
        {
            settings.ScriptBackend = PlayerSettings.GetScriptingBackend(ToBuildTargetGroup(settings.BuildTarget));
        }

        if (buildPath == null)
        {
            buildPath = $"bin/UnitTest/{settings.BuildTarget}_{settings.ScriptBackend}/test" + GetExtensionForBuildTarget(settings.BuildTarget);
        }

        var originalScene = SceneManager.GetActiveScene().path;

        BuildUnitTest(buildPath, settings.ScriptBackend, settings.BuildTarget, settings.Headless, settings.AutoRunPlayer, settings.DisableAutoClose);

        // reopen original scene
        if (!string.IsNullOrWhiteSpace(originalScene))
        {
            EditorSceneManager.OpenScene(originalScene, OpenSceneMode.Single);
        }
        else
        {
            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        }
    }


    [MenuItem("Test/LoadUnitTestScene")]
    public static void LoadUnitTestScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        BuildUnitTestRunnerScene();
        EditorSceneManager.MarkSceneDirty(scene);
    }

    static RuntimeUnitTestSettings LoadOrGetDefaultSettings()
    {
        var key = SettingsKeyBase + Application.productName;

        var settingsValue = EditorPrefs.GetString(key, null);
        RuntimeUnitTestSettings settings = null;
        try
        {
            if (!string.IsNullOrWhiteSpace(settingsValue))
            {
                settings = JsonUtility.FromJson<RuntimeUnitTestSettings>(settingsValue);
            }
        }
        catch
        {
            UnityEngine.Debug.LogError("Fail to load RuntimeUnitTest settings");
            EditorPrefs.SetString(key, null);
            settings = null;
        }

        if (settings == null)
        {
            // default
            settings = new RuntimeUnitTestSettings
            {
                UseCurrentBuildTarget = true,
                UseCurrentScriptBackend = true,
                Headless = false,
                AutoRunPlayer = true,
            };
        }

        return settings;
    }

    static void SaveSettings(RuntimeUnitTestSettings settings)
    {
        var key = SettingsKeyBase + Application.productName;
        EditorPrefs.SetString(key, JsonUtility.ToJson(settings));
    }

    public static void BuildUnitTest(string buildPath, ScriptingImplementation scriptBackend, BuildTarget buildTarget, bool headless, bool autoRunPlayer, bool disableAutoClose)
    {
        var sceneName = "Assets/TempRuntimeUnitTestScene_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (disableAutoClose)
        {
            sceneName += "_DisableAutoClose";
        }
        sceneName += ".unity";

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        BuildUnitTestRunnerScene();

        EditorSceneManager.MarkSceneDirty(scene);
        AssetDatabase.SaveAssets();
        EditorSceneManager.SaveScene(scene, sceneName, false);
        try
        {
            Build(sceneName, buildPath, new RuntimeUnitTestSettings { ScriptBackend = scriptBackend, BuildTarget = buildTarget, Headless = headless, AutoRunPlayer = autoRunPlayer, DisableAutoClose = disableAutoClose });
        }
        finally
        {
            AssetDatabase.DeleteAsset(sceneName);
        }
    }

    public static UnitTestRunner BuildUnitTestRunnerScene()
    {
        const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        var uisprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
        var background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);

        ScrollRect buttonList;
        VerticalLayoutGroup listLayout;
        Scrollbar refListScrollbar;
        ScrollRect logList;
        Scrollbar refLogScrollbar;
        Button clearButton;
        Text logText;

        // Flutter like coded build utility

        var rootObject = new Builder<Camera>("SceneRoot")
        {
            Children = new IBuilder[] {
                    new Builder<EventSystem, StandaloneInputModule>("EventSystem"),
                    new Builder<Canvas, CanvasScaler, GraphicRaycaster>("Canvas") {
                        Component1 = { renderMode = RenderMode.ScreenSpaceOverlay },
                        Children = new IBuilder[] {
                            new Builder<HorizontalLayoutGroup, CanvasRenderer>("HorizontalSplitter") {
                                RectTransform = { anchorMin = new Vector2(0, 0), anchorMax = new Vector2(1, 1) },
                                Component1 = { childControlWidth = true, childControlHeight = true, spacing = 10 },
                                Children = new IBuilder[] {
                                    new Builder<ScrollRect, CanvasRenderer>("ButtonList", out buttonList) {
                                        RectTransform = { pivot = new Vector2(0.5f, 0.5f) },
                                        Component1 = { horizontal =false, vertical = true, movementType = ScrollRect.MovementType.Clamped },
                                        Children = new IBuilder[] {
                                            new Builder<VerticalLayoutGroup, ContentSizeFitter>("ListLayoutToAttach", out listLayout) {
                                                RectTransform = { anchorMin = new Vector2(0, 0), anchorMax = new Vector2(1, 1), pivot = new Vector2(0, 1) },
                                                Component1  = { childControlWidth = true, childControlHeight = true, childForceExpandWidth = true, childForceExpandHeight = false, spacing = 10, padding = new RectOffset(10,20,10,10) },
                                                Component2 = { horizontalFit = ContentSizeFitter.FitMode.Unconstrained, verticalFit = ContentSizeFitter.FitMode.PreferredSize },
                                                SetTarget = self => { buttonList.content = self.GetComponent<RectTransform>();  },
                                                Child = new Builder<Button, Image, LayoutElement>("ClearButton", out clearButton) {
                                                    Component2 = { sprite = uisprite, type = Image.Type.Sliced },
                                                    Component3 = { minHeight = 50 },
                                                    SetTarget = self => { self.GetComponent<Button>().targetGraphic = self.GetComponent<Graphic>(); },
                                                    Child = new Builder<Text>("ButtonText") {
                                                        RectTransform = { anchorMin = new Vector2(0, 0), anchorMax = new Vector2(1, 1), pivot = new Vector2(0.5f, 0.5f) },
                                                        Component1 = { text = "Clear", color = FromRGB(50, 50, 50), alignment = TextAnchor.MiddleCenter, fontSize = 24, lineSpacing = 1 }
                                                    }
                                                }
                                            },
                                            new Builder<Scrollbar,Image>("ListScrollbar", out refListScrollbar) {
                                                RectTransform = { anchorMin = new Vector2(1, 0), anchorMax = new Vector2(1, 1) },
                                                Component1 = { navigation = new Navigation{ mode = Navigation.Mode.None }, direction = Scrollbar.Direction.BottomToTop, size = 1.0f },
                                                Component2 = { sprite = background, type = Image.Type.Sliced },
                                                SetTarget = self => { buttonList.verticalScrollbar = self.GetComponent<Scrollbar>(); },
                                                Child = new Builder<RectTransform>("Sliding Area") {
                                                    RectTransform = { anchorMin = new Vector2(0, 0), anchorMax = new Vector2(1, 1) },
                                                    Child = new Builder<Image>("Handle") {
                                                        Component1 = { sprite = uisprite, type = Image.Type.Sliced },
                                                        SetTarget = self =>
                                                        {
                                                            refListScrollbar.targetGraphic = self.GetComponent<Graphic>();
                                                            refListScrollbar.handleRect = self.GetComponent<RectTransform>();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new Builder<ScrollRect, CanvasRenderer>("ScrollableText", out logList) {
                                        RectTransform = { pivot = new Vector2(0.5f, 0.5f) },
                                        Component1 = { horizontal =false, vertical = true, movementType = ScrollRect.MovementType.Elastic, elasticity = 0.1f },
                                        Children = new IBuilder[] {
                                            new Builder<Text, ContentSizeFitter>("Log", out logText) {
                                                RectTransform = { anchorMin = new Vector2(0, 0), anchorMax = new Vector2(1, 1), pivot = new Vector2(0, 1) },
                                                Component1  = { fontSize = 24, lineSpacing = 1, supportRichText = true, alignment = TextAnchor.UpperLeft, horizontalOverflow = HorizontalWrapMode.Wrap, verticalOverflow = VerticalWrapMode.Truncate  },
                                                Component2 = { horizontalFit = ContentSizeFitter.FitMode.Unconstrained, verticalFit = ContentSizeFitter.FitMode.PreferredSize },
                                                SetTarget = self => { logList.content = self.GetComponent<RectTransform>(); }
                                            },
                                            new Builder<Scrollbar,Image>("LogScrollbar", out refLogScrollbar) {
                                                RectTransform = { anchorMin = new Vector2(1, 0), anchorMax = new Vector2(1, 1) },
                                                Component1 = { navigation = new Navigation{ mode = Navigation.Mode.None }, direction = Scrollbar.Direction.BottomToTop, size = 1.0f },
                                                Component2 = { sprite = background, type = Image.Type.Sliced },
                                                SetTarget = self => { logList.verticalScrollbar = self.GetComponent<Scrollbar>(); },
                                                Child = new Builder<RectTransform>("Sliding Area2") {
                                                    RectTransform = { anchorMin = new Vector2(0, 0), anchorMax = new Vector2(1, 1) },
                                                    Child = new Builder<Image>("Handle2") {
                                                        Component1 = { sprite = uisprite, type = Image.Type.Sliced },
                                                        SetTarget = self =>
                                                        {
                                                            refLogScrollbar.targetGraphic = self.GetComponent<Graphic>();
                                                            refLogScrollbar.handleRect = self.GetComponent<RectTransform>();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    },
                                }
                            }
                        }
                    }
                }
        };

        // size modify after build complete:)
        {
            var rect = GameObject.Find("HorizontalSplitter").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
        }
        {
            var rect = GameObject.Find("ListLayoutToAttach").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
        }
        {
            var rect = GameObject.Find("ListScrollbar").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(30, 0);
        }
        {
            var rect = GameObject.Find("ClearButton").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
        }
        {
            var rect = GameObject.Find("Sliding Area").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(-20, -20);
        }
        {
            var rect = GameObject.Find("Handle").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(20, 20);
        }
        {
            var rect = GameObject.Find("ButtonText").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
        }
        {
            var rect = GameObject.Find("Log").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(15, 0);
            rect.offsetMax = new Vector2(-20, 0);
        }
        {
            var rect = GameObject.Find("LogScrollbar").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(-30, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(30, 0);
        }
        {
            var rect = GameObject.Find("Sliding Area2").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(-20, -20);
        }
        {
            var rect = GameObject.Find("Handle2").GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.sizeDelta = new Vector2(20, 20);
        }

        // add test script
        var runner = rootObject.GameObject.AddComponent<UnitTestRunner>();
        runner.clearButton = clearButton;
        runner.list = listLayout.gameObject.GetComponent<RectTransform>();
        runner.listScrollBar = refListScrollbar;
        runner.logText = logText;
        runner.logScrollBar = refLogScrollbar;

        return runner;
    }

    static void Build(string sceneName, string buildPath, RuntimeUnitTestSettings settings)
    {
        var options = BuildOptions.BuildScriptsOnly | BuildOptions.IncludeTestAssemblies;
        if (settings.AutoRunPlayer)
        {
            options |= BuildOptions.AutoRunPlayer;
        }
        if (settings.Headless)
        {
            options |= BuildOptions.EnableHeadlessMode;
        }

        var targetGroup = ToBuildTargetGroup(settings.BuildTarget);
        var currentBackend = PlayerSettings.GetScriptingBackend(targetGroup);
        if (currentBackend != settings.ScriptBackend)
        {
            UnityEngine.Debug.Log("Modify ScriptBackend to " + settings.ScriptBackend);
            PlayerSettings.SetScriptingBackend(targetGroup, settings.ScriptBackend);
        }

        var buildOptions = new BuildPlayerOptions
        {
            target = settings.BuildTarget,
            targetGroup = targetGroup,
            options = options,
            scenes = new[] { sceneName },
            locationPathName = buildPath
        };

        UnityEngine.Debug.Log("UnitTest Build Start, " + settings.ToString());

        var buildReport = BuildPipeline.BuildPlayer(buildOptions);

        if (currentBackend != settings.ScriptBackend)
        {
            UnityEngine.Debug.Log("Restore ScriptBackend to " + currentBackend);
            PlayerSettings.SetScriptingBackend(targetGroup, currentBackend);
        }

        if (buildReport.summary.result != BuildResult.Succeeded)
        {
            // Note: show error summary?
            // Debug.LogError(buildReport.SummarizeErrors());
            UnityEngine.Debug.LogError("UnitTest Build Failed.");
        }
        else
        {
            UnityEngine.Debug.Log("UnitTest Build Completed, binary located: " + buildOptions.locationPathName);
        }
    }

    static Color FromRGB(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    static bool IsWindows(BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.WSAPlayer:
                return true;
            default:
                return false;
        }
    }

    static string GetExtensionForBuildTarget(BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.WSAPlayer:
                return ".exe";
            case BuildTarget.StandaloneOSX:                
                return ".app";
            case BuildTarget.Android:
                return ".apk";
            default:
                return "";
        }
    }

    static BuildTargetGroup ToBuildTargetGroup(BuildTarget buildTarget)
    {
#pragma warning disable CS0618
        switch (buildTarget)
        {
#if UNITY_2017_3_OR_NEWER
            case BuildTarget.StandaloneOSX:
#else
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
#endif // UNITY_2017_3_OR_NEWER
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.StandaloneLinux64:
#if !UNITY_2019_2_OR_NEWER
            case BuildTarget.StandaloneLinux:
            case BuildTarget.StandaloneLinuxUniversal:
#endif // !UNITY_2019_2_OR_NEWER
                return BuildTargetGroup.Standalone;
            case (BuildTarget)6:
            case (BuildTarget)7:
            case BuildTarget.WebGL:
                return BuildTargetGroup.WebGL;
            case BuildTarget.iOS:
                return BuildTargetGroup.iOS;
            case BuildTarget.PS3:
                return BuildTargetGroup.PS3;
            case BuildTarget.PS4:
                return BuildTargetGroup.PS4;
            case BuildTarget.XBOX360:
                return BuildTargetGroup.XBOX360;
            case BuildTarget.Android:
                return BuildTargetGroup.Android;
            case BuildTarget.WSAPlayer:
                return BuildTargetGroup.WSA;
            case BuildTarget.WP8Player:
                return BuildTargetGroup.WP8;
            case BuildTarget.Tizen:
                return BuildTargetGroup.Tizen;
            case BuildTarget.PSP2:
                return BuildTargetGroup.PSP2;
            case BuildTarget.PSM:
                return BuildTargetGroup.PSM;
            case BuildTarget.XboxOne:
                return BuildTargetGroup.XboxOne;
            case BuildTarget.SamsungTV:
                return BuildTargetGroup.SamsungTV;
            case BuildTarget.N3DS:
                return BuildTargetGroup.N3DS;
            case BuildTarget.WiiU:
                return BuildTargetGroup.WiiU;
            case BuildTarget.tvOS:
                return BuildTargetGroup.tvOS;
            case BuildTarget.Switch:
                return BuildTargetGroup.Switch;
            case BuildTarget.Lumin:
                return BuildTargetGroup.Lumin;
            case BuildTarget.BlackBerry:
                return BuildTargetGroup.BlackBerry;
            case BuildTarget.NoTarget:
            default:
                return BuildTargetGroup.Unknown;
        }
#pragma warning restore CS0618

    }
}

#endif
