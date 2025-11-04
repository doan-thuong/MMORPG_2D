using UnityEngine;

public class Skill002 : SkillBase
{
    private float damage => data.b;
    private float speed => data.c;

    private ProjectileEffect projectileEffect = new();

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
        var path = string.Format(PathResource.PATH_PREFAB_SKILL_ITEM, "Projectile");

        return PoolService.Spawn(path, owner.transform.position, null, null);
    }
}