using UnityEngine;

public static class SkillService
{
    public static SkillBase skillBase;
    public static SkillConfig skillConfig;

    public static SkillRecord GetSkill(string id)
    {
        if (skillConfig == null)
        {
            Debug.LogError("Skill config is null");
            return null;
        }

        return skillConfig.data.Find(r => r.id == id);
    }
}