using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private GameObject myObject;
    private new EdgeCollider2D collider;
    private Dictionary<string, Vector2> pointLimit;
    private Dictionary<string, Vector2> pointCurrent;
    private Vector2 limitPosMin;
    private Vector2 limitPosMax;
    private Vector2 currentLeft;
    private Vector2 currentRight;

    void Start()
    {
        collider = myObject.GetComponent<EdgeCollider2D>();
        if (collider == null)
        {
            Debug.LogError("Collider in walkable null");
        }

        Vector3 targetPos = target.position;

        transform.position = (Vector2)targetPos;

        pointLimit = FindPointLimit();
        limitPosMin = pointLimit["minX"];
        limitPosMax = pointLimit["maxX"];
    }

    public void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position;

        pointCurrent = GetWidthCamera();

        currentLeft = pointCurrent["minX"];
        currentRight = pointCurrent["maxX"];
        var posX = targetPos.x;

        if (isReachLimitLeft(currentLeft, limitPosMin) && targetPos.x <= transform.position.x ||
            isReachLimitRight(currentRight, limitPosMax) && targetPos.x >= transform.position.x
        )
        {
            posX = transform.position.x;
        }
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    private Dictionary<string, Vector2> FindPointLimit()
    {
        Dictionary<string, Vector2> result = new();
        string minX = "minX";
        string maxX = "maxX";
        Vector2[] points = collider.points;

        Vector2 pointMin = points[0];
        Vector2 pointMax = points[0];

        foreach (var point in points)
        {
            if (point.x < pointMin.x)
            {
                pointMin = point;
            }

            if (point.x > pointMax.x)
            {
                pointMax = point;
            }
        }

        result[minX] = pointMin;
        result[maxX] = pointMax;

        return result;
    }

    private Dictionary<string, Vector2> GetWidthCamera()
    {
        Dictionary<string, Vector2> result = new();

        Vector2 camPos = Camera.main.transform.position;
        float height = Camera.main.orthographicSize * 2f;
        float width = Camera.main.aspect * height;

        float minPosX = camPos.x - width / 2;
        float maxPosX = camPos.x + width / 2;

        result["minX"] = new Vector2(minPosX, 0);
        result["maxX"] = new Vector2(maxPosX, 0);

        return result;
    }

    private bool isReachLimitLeft(Vector2 current, Vector2 limit)
    {
        return current.x < limit.x;
    }

    private bool isReachLimitRight(Vector2 current, Vector2 limit)
    {
        return current.x > limit.x;
    }
}
