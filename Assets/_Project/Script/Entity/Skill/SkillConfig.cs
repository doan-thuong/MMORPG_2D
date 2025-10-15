using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillRecord
{
    public string id;
    public float cooldown;
    public float damage;
}

[CreateAssetMenu(fileName = "skill_config", menuName = "Custom/skill_config")]
public class SkillConfig : ScriptableObject
{
    public List<SkillRecord> data = new();
}