using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

class Builder
{
    static void BuildProject()
    {
        BuildPlayerOptions options = new BuildPlayerOptions();

        List<string> scenes = new List<string>();
        foreach(var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes.Add(scene.path);
            }
        }
        options.scenes = scenes.ToArray();

        if (!Directory.Exists("C:\\Builds\\AehyoCat"))
        {
            Directory.CreateDirectory("C:\\Builds\\AehyoCat");
        }

        options.locationPathName = @"C:\Builds\AehyoCat\AehyoCat.exe";
        options.target = BuildTarget.StandaloneWindows64;

        BuildPipeline.BuildPlayer(options);
    }
}