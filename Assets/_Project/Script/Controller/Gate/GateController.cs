using UnityEditor;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public SceneAsset sceneAsset;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (sceneAsset == null)
        {
            Debug.LogError("Scene null");
            return;
        }
        if (GameUtil.IsInLayer(collision.gameObject, layerMask))
        {
            SceneLoader.LoadScene(sceneAsset.name);
        }
    }
}