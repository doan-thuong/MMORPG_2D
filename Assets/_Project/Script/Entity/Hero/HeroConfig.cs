using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroRecord
{
    public string id;
    public float damage;
    public float hp;
    public float rangeAttack;
}

[CreateAssetMenu(fileName = "hero_config", menuName = "Custom/hero_config")]
public class HeroConfig : ScriptableObject
{
    public List<HeroRecord> data = new();
}