using UnityEngine;

public class HpBarFlip : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.x = target.localScale.x < 0 ? -1 * Mathf.Abs(transform.localScale.x) : 1 * Mathf.Abs(transform.localScale.x);
        transform.localScale = scale;
    }
}