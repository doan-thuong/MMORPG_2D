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
                DataProvocative provocative = new(target, owner);
                EventManager.EmitEvent(EventName.Enemy.ENEMY_PROVOCATIVE, provocative);
                enemy.TakeDamage(dmg);
            }
        }
    }
}