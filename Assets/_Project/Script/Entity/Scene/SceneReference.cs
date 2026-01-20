using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(
    fileName = "SceneReference",
    menuName = "Game/Scene Reference"
)]
public class SceneReference : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;
#endif

    [SerializeField] private string sceneName;

    public string SceneName => sceneName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
            EditorUtility.SetDirty(this);
        }
    }
#endif
}
