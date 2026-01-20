using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SceneReference targetScene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetScene == null)
        {
            Debug.LogError("Scene null");
            return;
        }
        if (GameUtil.IsInLayer(collision.gameObject, layerMask))
        {
            SceneService.LoadScene(targetScene);
        }
    }
}