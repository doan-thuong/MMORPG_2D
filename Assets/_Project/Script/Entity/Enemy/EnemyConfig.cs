
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyRecord
{
    public string id;
    public float damage;
    public float hp;
    public float rangeAttack;
}

[CreateAssetMenu(fileName = "enemy_config", menuName = "Custom/enemy_config")]
public class EnemyConfig : ScriptableObject
{
    public List<EnemyRecord> data = new();
}