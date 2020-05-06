#if UNITY_EDITOR

using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class PackageExporter
{
    [MenuItem("Tools/Export Unitypackage")]
    public static void Export()
    {
        var root = "Plugins/UniTask";
        var version = Environment.GetEnvironmentVariable("UNITY_PACKAGE_VERSION");

        var versionJson = Path.Combine(Application.dataPath, root, "package.json");
        if (File.Exists(versionJson))
        {
            var v = JsonUtility.FromJson<Version>(File.ReadAllText(versionJson));
            version = v.version;
        }

        var fileName = string.IsNullOrEmpty(version) ? "UniTask.unitypackage" : $"UniTask.{version}.unitypackage";
        var exportPath = "./" + fileName;

        var path = Path.Combine(Application.dataPath, root);
        var assets = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
            .Where(x => Path.GetExtension(x) == ".cs" || Path.GetExtension(x) == ".asmdef" || Path.GetExtension(x) == ".json" || Path.GetExtension(x) == ".meta")
            .Select(x => "Assets" + x.Replace(Application.dataPath, "").Replace(@"\", "/"))
            .ToArray();

        UnityEngine.Debug.Log("Export below files" + Environment.NewLine + string.Join(Environment.NewLine, assets));

        AssetDatabase.ExportPackage(
            assets,
            exportPath,
            ExportPackageOptions.Default);

        UnityEngine.Debug.Log("Export complete: " + Path.GetFullPath(exportPath));
    }

    public class Version
    {
        public string version;
    }
}

#endif