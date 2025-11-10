using UnityEngine;

public class Skill001 : SkillBase
{
    private float damage => data.b;
    private float range => data.d;

    private DamageEffect damageEffect = new();

    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);
        AddEffect(damageEffect);
        SetRange(owner);
        EventManager.StartListeningEvent(EventName.Enemy.ENEMY_NEAREST, HandleEnemyTarget);
    }

    void OnDestroy()
    {
        EventManager.StopListeningEvent(EventName.Enemy.ENEMY_NEAREST, HandleEnemyTarget);
    }

    void HandleEnemyTarget(object data)
    {
        target = data as GameObject;
    }

    public override void Execute()
    {
        effectParam[EnumBase.EffectParam.damage] = damage;
        base.Execute();
    }

    protected override bool HasEnoughMana()
    {
        if (owner == null)
        {
            Debug.LogError("owner null");
            return false;
        }

        if (owner.TryGetComponent(out HeroController hero))
        {
            return hero.GetCurrentMana() > ManaCost;
        }

        return false;
    }

    private void SetRange(GameObject owner)
    {
        RangeController rangeCtrl = owner.GetComponentInChildren<RangeController>();
        if (rangeCtrl == null)
        {
            Debug.LogError("Get component range controller null");
            return;
        }

        rangeCtrl.maxRange = range;
    }
}