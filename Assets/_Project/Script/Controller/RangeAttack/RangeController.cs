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

    // void Update()
    // {
    //     circle2D.radius += Time.deltaTime * speedScale;

    //     if (circle2D.radius >= maxRange)
    //     {
    //         circle2D.radius = 0.1f;
    //     }
    // }

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

    public GameObject GetObjectNearest()
    {
        if (dictObject.Count == 0) return null;
        return dictObject[new List<string>(dictObject.Keys)[0]];
    }
}