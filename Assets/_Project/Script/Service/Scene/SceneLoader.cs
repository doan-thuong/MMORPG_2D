using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneService
{
    public static void LoadScene(SceneReference sceneRef)
    {
        if (sceneRef == null)
        {
            Debug.LogError("SceneReference is NULL");
            return;
        }

        if (string.IsNullOrEmpty(sceneRef.SceneName))
        {
            Debug.LogError("Scene name is empty");
            return;
        }

        SceneManager.LoadScene(sceneRef.SceneName);
    }
}