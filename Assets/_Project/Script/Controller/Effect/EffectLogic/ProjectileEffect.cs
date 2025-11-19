using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : ISkillEffect
{
    private GameObject projectileObject;
    public void SetProjectileObject(GameObject projectileObject) { this.projectileObject = projectileObject; }

    public void Apply(GameObject owner, GameObject target, Dictionary<EnumBase.EffectParam, float> param)
    {
        if (projectileObject == null) return;

        // GameObject proj = Object.Instantiate(projectileObject, owner.transform.position, Quaternion.identity);

        if (projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.SetOwner(owner);
            projectile.SetTarget(target);

            if (param.TryGetValue(EnumBase.EffectParam.projectileSpeed, out float speed))
            {
                projectile.SetSpeed(speed);
            }

            if (param.TryGetValue(EnumBase.EffectParam.projectileDamage, out float dmg))
            {
                projectile.SetDamage(dmg);
            }
        }
    }
}