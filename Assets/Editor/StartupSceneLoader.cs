using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class StartupSceneLoader
{
    private const string FirstOpenKey = "THIS_IS_UNIQUE_KEY";

    static StartupSceneLoader()
    {
        if (!EditorPrefs.GetBool(FirstOpenKey, false))
        {
            // Delay until the editor is ready
            EditorApplication.delayCall += OpenStartupSceneOnce;
        }
    }

    private static void OpenStartupSceneOnce()
    {
        string startupScenePath = "Assets/Scenes/Template.unity";

        if (System.IO.File.Exists(startupScenePath))
        {
            EditorSceneManager.OpenScene(startupScenePath);
            Debug.Log($"Opened startup scene: {startupScenePath}");
        }
        else
        {
            Debug.LogWarning($"Startup scene not found at {startupScenePath}");
        }

        EditorPrefs.SetBool(FirstOpenKey, true);
    }
}
