using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameUtil.IsInLayer(collision.gameObject, layerMask))
        {
            SceneLoader.LoadScene(SceneName.Scene.SCENE_HOME);
        }
    }
}