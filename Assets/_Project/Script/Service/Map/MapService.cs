using UnityEngine;

public class MapService
{
    public static MapConfig mapConfig;
    public static MapRecord mapRecord;
    public static string MapId { get; private set; }

    public static MapRecord GetMapRecord(string id)
    {
        if (mapConfig == null)
        {
            Debug.LogError("Map config is null");
            return null;
        }

        return mapConfig.data.Find(m => m.mapId == id);
    }

    public static void SetMapId(string idMap)
    {
        MapId = idMap;
    }
}