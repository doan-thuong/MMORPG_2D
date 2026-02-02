using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapRecord
{
    public string mapId;
    public List<Vector3> playSpawnPoints;
    public List<Vector3> enemySpawnPoints;
}

[CreateAssetMenu(fileName = "map_config", menuName = "Custom/map_config")]
public class MapConfig : ScriptableObject
{
    public List<MapRecord> data = new();
}