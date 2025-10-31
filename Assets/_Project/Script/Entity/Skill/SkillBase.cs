using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : ScriptableObject, ISkill
{
    public string Id => data.id;
    protected SkillRecord data;
    protected float lastCastTime;
    protected GameObject owner;
    protected GameObject target;
    protected List<ISkillEffect> skillEffects = new();
    protected Dictionary<EnumBase.EffectParam, float> effectParam = new();

    public float Cooldown => data.a;
    public float ManaCost => data.c;
    public bool IsReady => Time.time >= lastCastTime + Cooldown;

    public bool CanCast()
    {
        return IsReady && HasEnoughMana();
    }

    public bool Cast(GameObject owner, GameObject target)
    {
        if (!CanCast()) return false;

        this.owner = owner;
        this.target = target;
        lastCastTime = Time.time;

        Execute();
        return true;
    }

    public float CostMana()
    {
        return ManaCost;
    }

    protected abstract bool HasEnoughMana();

    public void SetData(SkillRecord record)
    {
        data = record;
    }

    public virtual void Initialize(GameObject owner)
    {
        this.owner = owner;
        skillEffects.Clear();
        effectParam.Clear();
    }

    public virtual void Execute()
    {
        foreach (var effect in skillEffects)
            effect.Apply(owner, target, effectParam);
    }

    protected void AddEffect(ISkillEffect effect)
    {
        skillEffects.Add(effect);
    }
}