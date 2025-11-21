using System.Collections.Generic;
using UnityEngine;

public abstract class Pooling
{
    public abstract T Spawn<T>(T prefab) where T : Component;
}

public static class PoolService
{

    public static List<GameObject> pools = new();
    public static GameObject SpawnOther(string path, Vector3? position = null, Quaternion? rotation = null, Transform parent = null)
    {
        var prefab = ResLoader.Load<GameObject>(path);
        if (prefab == null) return null;

        GameObject item = GetObjectFromPools(prefab.name);

        Vector3 pos = position ?? Vector3.zero;
        Quaternion rot = rotation ?? Quaternion.identity;

        if (item == null)
        {
            item = Object.Instantiate(prefab, pos, rot, parent);
            item.name = prefab.name;
        }
        else
        {
            item.SetActive(true);
            item.transform.SetPositionAndRotation(pos, rot);
        }
        pools.Add(item);

        return item;
    }

    private static GameObject GetObjectFromPools(string name)
    {
        foreach (var item in pools)
        {
            if (item.name == name && !item.activeInHierarchy)
            {
                pools.Remove(item);
                return item;
            }
        }
        return null;
    }


    public static T Spawn<T>(string path, Vector3? position = null, Quaternion? rotation = null, Transform parent = null) where T : Component
    {
        var prefab = ResLoader.Load<T>(path);
        if (prefab == null) return null;

        Vector3 pos = position ?? Vector3.zero;
        Quaternion rot = rotation ?? Quaternion.identity;

        T instance = Object.Instantiate(prefab, pos, rot, parent);
        return instance;
    }

    public static GameObject Spawn(string path, Vector3? position = null, Quaternion? rotation = null, Transform parent = null)
    {
        var prefab = ResLoader.Load<GameObject>(path);
        if (prefab == null) return null;

        Vector3 pos = position ?? Vector3.zero;
        Quaternion rot = rotation ?? Quaternion.identity;

        return Object.Instantiate(prefab, pos, rot, parent);
    }

    public static GameObject Despawn(GameObject gameObject, Vector3? position = null, Quaternion? rotation = null)
    {
        gameObject.transform.SetPositionAndRotation(position ?? Vector3.zero, rotation ?? Quaternion.identity);
        gameObject.SetActive(false);

        return gameObject;
    }
}