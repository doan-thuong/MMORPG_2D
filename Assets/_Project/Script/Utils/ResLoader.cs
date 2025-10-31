using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ResLoader
{
    public static readonly Dictionary<string, Object> resourceCache = new();

    public static void OnDestroy()
    {
        resourceCache.Clear();
    }

    public static T Load<T>(string path, string fileName = "") where T : Object
    {
        string fullPath = !string.IsNullOrWhiteSpace(fileName) ? Path.Combine(path, fileName) : path;

        if (!resourceCache.ContainsKey(fullPath))
        {
            resourceCache.Add(fullPath, Resources.Load<T>(fullPath));
        }
        return resourceCache[fullPath] as T;
    }
}