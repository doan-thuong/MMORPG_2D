using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    public Vector3 spawnPoint;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "enemy_spawn_config", menuName = "Custom/enemy_spawn_config")]
public class EnemySpawnConfig : ScriptableObject
{
    public List<EnemySpawnData> enemySpawns = new();
}