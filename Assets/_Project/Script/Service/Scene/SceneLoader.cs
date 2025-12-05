using UnityEngine.SceneManagement;

public static class SceneName
{
    public static class Scene
    {
        public static string SCENE_JUNGLE = "JungleScene";
        public static string SCENE_HOME = "Home";
    }
}

public static class SceneLoader
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}