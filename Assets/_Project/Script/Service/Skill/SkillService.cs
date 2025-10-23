using UnityEngine;
using SkillLogic;
using System.Collections.Generic;
using System;

public static class SkillService
{
    public static SkillBase skillBase;
    public static SkillConfig skillConfig;

    private static readonly Dictionary<string, Type> skillTypes = new()
{
    { "Skill_1", typeof(Skill001) },
    { "Skill_2", typeof(Skill002) },
    // ... thêm ở đây
};


    public static SkillRecord GetSkill(string id)
    {
        if (skillConfig == null)
        {
            Debug.LogError("Skill config is null");
            return null;
        }

        return skillConfig.data.Find(r => r.id == id);
    }

    public static ISkill CreateSkill(string id)
    {
        SkillRecord record = GetSkill(id);
        if (record == null)
        {
            Debug.LogWarning($"Skill record not found for id: {id}");
            return null;
        }

        if (!skillTypes.TryGetValue(id, out var skillType))
        {
            Debug.LogWarning($"Skill type not found for id: {id}");
            return null;
        }

        var skill = ScriptableObject.CreateInstance(skillType) as ISkill;
        ((SkillBase)skill).SetData(record);
        skill.Initialize(null);
        return skill;
    }
}