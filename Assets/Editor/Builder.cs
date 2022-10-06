using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class Builder : MonoBehaviour
{
    [MenuItem("Windonw/Build")]
    public static void BuildAPK()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        buildPlayerOptions.scenes = new[]
        {
            "Assets/Scenes/MainMenuScene.unity",
            "Assets/Scenes/LevelGameScene.unity",
            "Assets/Scenes/GameOverScene.unity",
        };
        buildPlayerOptions.locationPathName = "builds/DoodleBuild.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build successful");
        }
        
        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }

    }

}
