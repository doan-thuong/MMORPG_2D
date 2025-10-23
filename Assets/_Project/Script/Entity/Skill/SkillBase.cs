using UnityEngine;

public abstract class SkillBase : ScriptableObject, ISkill
{
    public string Id => data.id;
    protected SkillRecord data;
    protected float lastCastTime;
    public bool IsReady => Time.time >= lastCastTime + Cooldown;

    public float Cooldown => data.a;
    public float ManaCost => data.c;

    public bool CanCast()
    {
        return IsReady;
    }

    public void Cast(GameObject owner)
    {
        if (!CanCast()) return;

        lastCastTime = Time.time;
        Execute();
    }

    public float CostMana()
    {
        return ManaCost;
    }

    public void SetData(SkillRecord record)
    {
        data = record;
    }

    public virtual void Initialize(GameObject owner) { }

    protected abstract void Execute();

}