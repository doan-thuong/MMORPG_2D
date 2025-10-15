using UnityEngine;

public interface ISkill
{
    float Cooldown { get; }
    void Initialize(GameObject owner);
    bool CanCast(GameObject owner);
    void Cast(GameObject owner);
}