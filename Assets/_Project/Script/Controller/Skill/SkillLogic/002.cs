using System.Collections.Generic;
using UnityEngine;

public class Skill002 : SkillBase
{
    private float damage => data.b;
    private float speed => data.c;
    private float range => data.d;

    private ProjectileEffect projectileEffect = new();

    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);
        AddEffect(projectileEffect);
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
        effectParam[EnumBase.EffectParam.projectileDamage] = damage;
        effectParam[EnumBase.EffectParam.projectileSpeed] = speed;

        projectileEffect.SetProjectileObject(SpawnProjectile());

        base.Execute();
    }

    protected override bool HasEnoughMana()
    {
        if (owner.TryGetComponent(out HeroController hero))
        {
            if (hero.GetCurrentMana() > 0)
            {
                return true;
            }
        }

        return false;
    }

    private GameObject SpawnProjectile()
    {
        var path = string.Format(PathResource.PATH_PREFAB_SKILL_ITEM, "Projectile");
        string nameObject = "HandPlayer";
        Transform handPlayer = owner.transform.Find(nameObject);

        GameObject projectileNew = PoolService.SpawnOther(path, handPlayer.position);
        return projectileNew;
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