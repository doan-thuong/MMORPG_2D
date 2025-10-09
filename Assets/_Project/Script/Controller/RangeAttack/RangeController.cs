using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    [SerializeField] private int targetLayer;
    [SerializeField] private CircleCollider2D circle2D;
    public float maxRange;
    [SerializeField] private float speedScale;
    [SerializeField] private Dictionary<string, GameObject> dictObject = new();

    void Start()
    {
        if (circle2D == null) circle2D = GetComponent<CircleCollider2D>();
        circle2D.radius = maxRange;
    }

    void Update()
    {

        if (circle2D.radius < maxRange)
        {
            circle2D.radius += Time.deltaTime * speedScale;
            // circle2D.radius = 0.1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            if (dictObject.ContainsKey(other.gameObject.GetInstanceID().ToString()))
            {
                return;
            }
            dictObject[other.gameObject.GetInstanceID().ToString()] = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            if (dictObject.ContainsKey(other.gameObject.GetInstanceID().ToString()))
            {
                dictObject.Remove(other.gameObject.GetInstanceID().ToString());
            }
        }
    }

    public bool CheckObjectInRange(GameObject orther)
    {
        return dictObject.ContainsValue(orther);
    }

    public GameObject GetObjectNearest()
    {
        if (dictObject.Count == 0) return null;

        GameObject currrentObjectNearest = null;
        float currentDisNearest = int.MaxValue;

        foreach (var enemy in dictObject.Values)
        {
            float dis = Vector3.Distance(transform.position, enemy.transform.position);

            if (dis < currentDisNearest)
            {
                currrentObjectNearest = enemy;
                currentDisNearest = dis;
            }
        }

        if (currrentObjectNearest == null) Debug.Log("current obj nearest null");

        return currrentObjectNearest;
    }
}