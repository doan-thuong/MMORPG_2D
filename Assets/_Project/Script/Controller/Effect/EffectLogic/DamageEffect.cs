using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : ISkillEffect
{
    public void Apply(GameObject owner, GameObject target, Dictionary<EnumBase.EffectParam, float> param)
    {
        if (param.TryGetValue(EnumBase.EffectParam.damage, out float dmg))
        {
            if (target.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(dmg);
            }
        }
    }
}