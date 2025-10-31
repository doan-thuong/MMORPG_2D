using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffect
{
    void Apply(GameObject owner, GameObject target, Dictionary<EnumBase.EffectParam, float> param);
}