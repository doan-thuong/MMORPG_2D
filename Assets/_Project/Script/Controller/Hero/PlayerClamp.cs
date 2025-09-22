using UnityEngine;

public class PlayerClamp : MonoBehaviour
{
    private Bounds movementBounds;

    void Start()
    {
        BoxCollider2D box2D = GameObject.Find("Moveable").GetComponent<BoxCollider2D>();
        movementBounds = box2D.bounds;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, movementBounds.min.x, movementBounds.max.x);
        pos.y = Mathf.Clamp(pos.y, movementBounds.min.y, movementBounds.max.y);

        transform.position = pos;
    }
}