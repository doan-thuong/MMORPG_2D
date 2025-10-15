using UnityEngine;

public abstract class SkillBase : ScriptableObject, ISkill
{
    public float cooldown;
    public float manaCost;
    protected float lastCastTime;
    public bool IsReady => Time.time >= lastCastTime + cooldown;

    public float Cooldown => cooldown;

    public bool CanCast(GameObject owner)
    {
        return IsReady;
    }

    public abstract void Cast(GameObject owner);

    public virtual void Initialize(GameObject owner) { }
}