using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillRecord
{
    public string id;
    public float a;
    public float b;
    public float c;
    public float d;
    public float e;
    public float f;
}

[CreateAssetMenu(fileName = "skill_config", menuName = "Custom/skill_config")]
public class SkillConfig : ScriptableObject
{
    public List<SkillRecord> data = new();
}