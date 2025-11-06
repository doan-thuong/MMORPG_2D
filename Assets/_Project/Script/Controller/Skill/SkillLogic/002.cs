using System.Collections.Generic;
using UnityEngine;

public class Skill002 : SkillBase
{
    private float damage => data.b;
    private float speed => data.c;

    private ProjectileEffect projectileEffect = new();
    private List<GameObject> projectiles = new();

    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);
        AddEffect(projectileEffect);
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
        foreach (var proj in projectiles)
        {
            if (!proj.activeInHierarchy)
            {
                GameObject projectile = PoolService.Despawn(proj, owner.transform.position);
                return projectile;
            }
        }
        var path = string.Format(PathResource.PATH_PREFAB_SKILL_ITEM, "Projectile");

        GameObject projectileNew = PoolService.Spawn(path, owner.transform.position);
        projectiles.Add(projectileNew);
        return projectileNew;
    }
}