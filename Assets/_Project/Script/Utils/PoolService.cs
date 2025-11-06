using UnityEngine;

public abstract class Pooling
{
    public abstract T Spawn<T>(T prefab) where T : Component;
}

public static class PoolService
{
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
        gameObject.SetActive(true);

        return gameObject;
    }
}