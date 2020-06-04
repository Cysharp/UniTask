#if UNITY_EDITOR
using UnityEditor;

// Settings MenuItems.

public static partial class UnitTestBuilder
{
    [MenuItem("Test/Settings/ScriptBackend/Mono", validate = true, priority = 1)]
    static bool ValidateScriptBackendMono()
    {
        Menu.SetChecked("Test/Settings/ScriptBackend/Mono", LoadOrGetDefaultSettings().ScriptBackend == ScriptingImplementation.Mono2x);
        return true;
    }

    [MenuItem("Test/Settings/ScriptBackend/Mono", validate = false, priority = 1)]
    static void ScriptBackendMono()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentScriptBackend = false;
        settings.ScriptBackend = ScriptingImplementation.Mono2x;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/ScriptBackend/IL2CPP", validate = true, priority = 2)]
    static bool ValidateScriptBackendIL2CPP()
    {
        Menu.SetChecked("Test/Settings/ScriptBackend/IL2CPP", LoadOrGetDefaultSettings().ScriptBackend == ScriptingImplementation.IL2CPP);
        return true;
    }

    [MenuItem("Test/Settings/ScriptBackend/IL2CPP", validate = false, priority = 2)]
    static void ScriptBackendIL2CPP()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentScriptBackend = false;
        settings.ScriptBackend = ScriptingImplementation.IL2CPP;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/AutoRunPlayer", validate = true, priority = 3)]
    static bool ValidateAutoRun()
    {
        Menu.SetChecked("Test/Settings/AutoRunPlayer", LoadOrGetDefaultSettings().AutoRunPlayer);
        return true;
    }

    [MenuItem("Test/Settings/AutoRunPlayer", validate = false, priority = 3)]
    static void AutoRun()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.AutoRunPlayer = !settings.AutoRunPlayer;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/Headless", validate = true, priority = 4)]
    static bool ValidateHeadless()
    {
        Menu.SetChecked("Test/Settings/Headless", LoadOrGetDefaultSettings().Headless);
        return true;
    }

    [MenuItem("Test/Settings/Headless", validate = false, priority = 4)]
    static void Headless()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.Headless = !settings.Headless;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/DisableAutoClose", validate = true, priority = 5)]
    static bool ValidateDisableAutoClose()
    {
        Menu.SetChecked("Test/Settings/DisableAutoClose", LoadOrGetDefaultSettings().DisableAutoClose);
        return true;
    }

    [MenuItem("Test/Settings/DisableAutoClose", validate = false, priority = 5)]
    static void DisableAutoClose()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.DisableAutoClose = !settings.DisableAutoClose;
        SaveSettings(settings);
    }

    // generated

    /*
     * 
  void Main()
{
var sb = new StringBuilder();

var p = 1;
foreach (var target in Enum.GetNames(typeof(BuildTarget)))
{
    var path = $"Test/Settings/BuildTarget/{target}";
    var priority = p++;

    var template = $@"
[MenuItem(""{path}"", validate = true, priority = {priority})]
static bool ValidateBuildTarget{target}()
{{
Menu.SetChecked(""{path}"", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.{target});
return true;
}}

[MenuItem(""{path}"", validate = false, priority = {priority})]
static void BuildTarget{target}()
{{
var settings = LoadOrGetDefaultSettings();
settings.UseCurrentBuildTarget = false;
settings.BuildTarget = BuildTarget.{target};
SaveSettings(settings);
}}";

    sb.AppendLine(template);
}

sb.ToString().Dump();
}

public enum BuildTarget
{
StandaloneWindows,
StandaloneWindows64,
StandaloneLinux,
StandaloneLinux64,
StandaloneOSX,
WebGL,
iOS,
Android,
WSAPlayer,
PS4,
XboxOne,
Switch,
}
    */


    [MenuItem("Test/Settings/BuildTarget/StandaloneWindows", validate = true, priority = 1)]
    static bool ValidateBuildTargetStandaloneWindows()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/StandaloneWindows", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.StandaloneWindows);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneWindows", validate = false, priority = 1)]
    static void BuildTargetStandaloneWindows()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.StandaloneWindows;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneWindows64", validate = true, priority = 2)]
    static bool ValidateBuildTargetStandaloneWindows64()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/StandaloneWindows64", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.StandaloneWindows64);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneWindows64", validate = false, priority = 2)]
    static void BuildTargetStandaloneWindows64()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.StandaloneWindows64;
        SaveSettings(settings);
    }

#if !UNITY_2019_2_OR_NEWER

    [MenuItem("Test/Settings/BuildTarget/StandaloneLinux", validate = true, priority = 3)]
    static bool ValidateBuildTargetStandaloneLinux()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/StandaloneLinux", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.StandaloneLinux);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneLinux", validate = false, priority = 3)]
    static void BuildTargetStandaloneLinux()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.StandaloneLinux;
        SaveSettings(settings);
    }

#endif

    [MenuItem("Test/Settings/BuildTarget/StandaloneLinux64", validate = true, priority = 4)]
    static bool ValidateBuildTargetStandaloneLinux64()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/StandaloneLinux64", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.StandaloneLinux64);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneLinux64", validate = false, priority = 4)]
    static void BuildTargetStandaloneLinux64()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.StandaloneLinux64;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneOSX", validate = true, priority = 5)]
    static bool ValidateBuildTargetStandaloneOSX()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/StandaloneOSX", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.StandaloneOSX);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/StandaloneOSX", validate = false, priority = 5)]
    static void BuildTargetStandaloneOSX()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.StandaloneOSX;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/WebGL", validate = true, priority = 6)]
    static bool ValidateBuildTargetWebGL()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/WebGL", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.WebGL);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/WebGL", validate = false, priority = 6)]
    static void BuildTargetWebGL()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.WebGL;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/iOS", validate = true, priority = 7)]
    static bool ValidateBuildTargetiOS()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/iOS", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.iOS);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/iOS", validate = false, priority = 7)]
    static void BuildTargetiOS()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.iOS;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/Android", validate = true, priority = 8)]
    static bool ValidateBuildTargetAndroid()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/Android", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.Android);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/Android", validate = false, priority = 8)]
    static void BuildTargetAndroid()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.Android;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/WSAPlayer", validate = true, priority = 9)]
    static bool ValidateBuildTargetWSAPlayer()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/WSAPlayer", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.WSAPlayer);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/WSAPlayer", validate = false, priority = 9)]
    static void BuildTargetWSAPlayer()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.WSAPlayer;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/PS4", validate = true, priority = 10)]
    static bool ValidateBuildTargetPS4()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/PS4", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.PS4);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/PS4", validate = false, priority = 10)]
    static void BuildTargetPS4()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.PS4;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/XboxOne", validate = true, priority = 11)]
    static bool ValidateBuildTargetXboxOne()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/XboxOne", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.XboxOne);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/XboxOne", validate = false, priority = 11)]
    static void BuildTargetXboxOne()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.XboxOne;
        SaveSettings(settings);
    }

    [MenuItem("Test/Settings/BuildTarget/Switch", validate = true, priority = 12)]
    static bool ValidateBuildTargetSwitch()
    {
        Menu.SetChecked("Test/Settings/BuildTarget/Switch", LoadOrGetDefaultSettings().BuildTarget == BuildTarget.Switch);
        return true;
    }

    [MenuItem("Test/Settings/BuildTarget/Switch", validate = false, priority = 12)]
    static void BuildTargetSwitch()
    {
        var settings = LoadOrGetDefaultSettings();
        settings.UseCurrentBuildTarget = false;
        settings.BuildTarget = BuildTarget.Switch;
        SaveSettings(settings);
    }








}

#endif
